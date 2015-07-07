using Autofac.Integration.WebApi;
using BLL.Account;
using Model.Base;
using Model.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Xml.Linq;
using Utility;

namespace API.Filters
{
    /// <summary>
    /// Authorizes any user for every requests.
    /// </summary>
    public class AuthorizationAttribute : IAutofacAuthorizationFilter
    {
        private readonly IAccountService _service;
        public const int Entries = 6;

        /// <summary>
        /// Authorizes any user for every requests.
        /// </summary>
        /// <param name="service"></param>
        public AuthorizationAttribute(IAccountService service)
        {
            _service = service;
        }

        /// <summary>
        /// Passes or fails authentication, based on whether you provide a valid application key in the http headers of the request.
        /// </summary>
        /// <param name="actionContext">Action filter context.</param>
        public void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
                    return;

                var credentials = ProcessAuthorizationToken(actionContext.Request, Entries);
                if (credentials != null && credentials.Count == Entries)
                {
                    var authenticationToken = actionContext.Request.Headers.FirstOrDefault(x => x.Key.Equals("Authorization", StringComparison.OrdinalIgnoreCase)).Value.First();
                    var body = actionContext.Request.Content.ReadAsStringAsync().Result;

                    if (actionContext.Request.Method != HttpMethod.Get && !string.IsNullOrWhiteSpace(body) && !actionContext.Request.Content.IsMimeMultipartContent())
                    //NOT (If GET request or request without any body or body having image/audio/video content)
                    {
                        try
                        {
                            RequestBase req = null;
                            if (Helper.IsJson(body))
                                req = JsonConvert.DeserializeObject<RequestBase>(body);
                            else
                            {
                                var doc = XDocument.Load(new StringReader(body));
                                var xElements = doc.Descendants().Elements() as IList<XElement> ?? doc.Descendants().Elements().ToList();
                                var userIdData = xElements.FirstOrDefault(x => x.Name == "UserId");
                                var deviceIdData = xElements.FirstOrDefault(x => x.Name == "DeviceId");
                                if (userIdData != null && deviceIdData != null)
                                {
                                    req = new RequestBase
                                    {
                                        UserId = Convert.ToInt32(userIdData.Value),
                                        DeviceId = deviceIdData.Value
                                    };
                                }
                            }
                            if (req != null && (!req.UserId.ToString(CultureInfo.InvariantCulture).Equals(credentials[(int)SystemSessionEntity.UserId]) || !req.DeviceId.Equals(credentials[(int)SystemSessionEntity.DeviceId])))
                            {
                                actionContext.Response = actionContext.Request.SystemResponse<string>(SystemDbStatus.Unauthorized);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            actionContext.Response = actionContext.Request.SystemResponse(SystemDbStatus.GeneralError, ex.Message, false, SystemResponseMessage.NonInheritingRequestBase);
                            return;
                        }
                    }

                    if (MemoryCache.GetValue(authenticationToken) != null && SaveToSession(actionContext, credentials)) return;

                    var dvcTyp = credentials[(int)SystemSessionEntity.DeviceTypeId];

                    var userExists = _service.IsAuthenticated(Convert.ToInt32(credentials[(int)SystemSessionEntity.UserId]), credentials[(int)SystemSessionEntity.UserName], credentials[(int)SystemSessionEntity.LoginToken], credentials[(int)SystemSessionEntity.DeviceId], dvcTyp, dvcTyp.Equals("W", StringComparison.OrdinalIgnoreCase) ? System.Web.HttpContext.Current.Request.UserHostAddress : null);

                    if (userExists)
                    {
                        SaveToSession(actionContext, credentials);
                        MemoryCache.Add(authenticationToken, credentials[(int)SystemSessionEntity.UserId], DateTime.Now.AddMinutes(SystemConstants.CacheExpiryTimeInMinutes));
                        return;
                    }
                }
                actionContext.Response = actionContext.Request.SystemResponse<string>(SystemDbStatus.Unauthorized);
            }
            catch (Exception ex)
            {
                actionContext.Response = actionContext.Request.SystemResponse<string>(SystemDbStatus.GeneralError, null, false, ex.Message);
            }
        }

        private static bool SaveToSession(HttpActionContext actionContext, IReadOnlyList<string> credentials)
        {
            var session = Encryptor.GetHttpContextWrapper(actionContext.Request);
            if (session.Session != null)
            {
                session.Session.Add(SystemSessionEntity.UserId.ToString(), Convert.ToInt32(credentials[(int)SystemSessionEntity.UserId]));
                session.Session.Add(SystemSessionEntity.UserName.ToString(), credentials[(int)SystemSessionEntity.UserName]);
                session.Session.Add(SystemSessionEntity.UserTypeId.ToString(), Convert.ToByte(credentials[(int)SystemSessionEntity.UserTypeId]));
                session.Session.Add(SystemSessionEntity.DeviceTypeId.ToString(), credentials[(int)SystemSessionEntity.DeviceTypeId]);
                session.Session.Add(SystemSessionEntity.DeviceId.ToString(), credentials[(int)SystemSessionEntity.DeviceId]);
                session.Session.Add(SystemSessionEntity.LoginToken.ToString(), credentials[(int)SystemSessionEntity.LoginToken]);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Extracts decoded components of Base64-encoded string.
        /// </summary>
        /// <param name="request">Current request in context.</param>
        /// <param name="length">The length of the components to be returned.</param>
        /// <returns>Data components of encoded string.</returns>
        public static List<string> ProcessAuthorizationToken(HttpRequestMessage request, int length)
        {
            try
            {
                var authKey = request.Headers.FirstOrDefault(x => x.Key.Equals("Authorization", StringComparison.OrdinalIgnoreCase));
                string authenticationToken;

                if (authKey.Value != null && !string.IsNullOrEmpty(authenticationToken = authKey.Value.First()))
                {
                    if (Helper.IsBase64String(authenticationToken))
                    {
                        var credential = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                        var credentials = credential.Split(':').ToList();
                        if (credentials.Count > length)
                        {
                            var crExtra = credentials.Take(4).ToList();
                            crExtra.Add(string.Join(":", Enumerable.Range(4, credentials.Count - length + 1).Select(x => credentials[x])));
                            crExtra.Add(credentials.Last());
                            credentials = crExtra;
                        }
                        return credentials;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}