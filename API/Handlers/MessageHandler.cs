using API.Areas.HelpPage;
using API.Filters;
using Model.Types;
using Model.WebClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Description;
using Utility;

namespace API.Handlers
{
    /// <summary>
    /// Handles tailoring of incoming requests (earliest) and outgoing responses (last) in the pipeline.
    /// </summary>
    public class MessageHandler : DelegatingHandler
    {
        public static List<Trace> TraceList = new List<Trace>();
        public static List<string> APIUriList = new List<string>();

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content.Headers.ContentType == null)
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //A disposable tracker code (not to be moved to production).
            var trace = ParseRequest(request);
            var t = await base.SendAsync(request, cancellationToken);
            ParseResponse(trace, t);

            return t;
            //.ContinueWith(
            //    task => {
            //                if (task.Exception != null)
            //                {
            //                    trace.Response = task
            //                }
            //    },
            //    TaskContinuationOptions.OnlyOnFaulted
            //);
            //return await base.SendAsync(request, cancellationToken);
        }

        public static Trace ParseRequest(HttpRequestMessage request)
        {
            try
            {
                var trace = new Trace
                {
                    Uri = request.RequestUri.ToString(),
                    Request = request.Content.ReadAsStringAsync().Result,
                    Time = DateTime.Now,
                    Title =
                        string.Format("IP Address: {0}\nUser Agent: {1}", HttpContext.Current.Request.UserHostAddress,
                            HttpContext.Current.Request.UserAgent),
                    RequestType = request.Method.Method,
                    RequestBodyLength = request.Content.Headers.ContentLength,
                    RandomNumber = Helper.GenerateRandomCode(int.MinValue, int.MaxValue)
                };

                int userId;
                var credentials = AuthorizationAttribute.ProcessAuthorizationToken(request, AuthorizationAttribute.Entries);
                if (credentials != null && credentials.Count == AuthorizationAttribute.Entries &&
                    int.TryParse(credentials[(int)SystemSessionEntity.UserId], out userId))
                {
                    trace.UserId = userId;
                    trace.UserName = credentials[(int)SystemSessionEntity.UserName];
                    trace.UserTypeId = Convert.ToInt32(credentials[(int)SystemSessionEntity.UserTypeId]);
                    trace.Token = credentials[(int)SystemSessionEntity.LoginToken];
                    trace.DeviceTypeId = credentials[(int)SystemSessionEntity.DeviceTypeId];
                    trace.DeviceId = credentials[(int)SystemSessionEntity.DeviceId];
                }
                return trace;
            }
            catch (Exception)
            {
            }
            return null;
        }

        public static void ParseResponse(Trace trace, HttpResponseMessage response)
        {
            try
            {
                var responseBody = response.Content;
                trace.Response = responseBody.ReadAsStringAsync().Result;
                trace.Status = ((int)response.StatusCode).ToString();
                if (trace.Response.Length > 0 && !trace.Status.Equals("429"))
                {
                    trace.ResponseBodyLength = responseBody.Headers.ContentLength;
                    var ad = response.RequestMessage.GetActionDescriptor();
                    if (ad != null)
                    {
                        var action = new ApiDescription
                        {
                            HttpMethod = response.RequestMessage.Method,
                            RelativePath = response.RequestMessage.RequestUri.PathAndQuery.Substring(1)
                        };
                        var fid = action.GetFriendlyId();
                        trace.FriendlyURI = APIUriList.FirstOrDefault(x => x.Equals(fid, StringComparison.OrdinalIgnoreCase));
                        if (string.IsNullOrWhiteSpace(trace.FriendlyURI))
                            trace.FriendlyURI = APIUriList.FirstOrDefault(x => x.StartsWith(fid, StringComparison.OrdinalIgnoreCase));
                        trace.Action = ad.ControllerDescriptor.ControllerName + " / " + ad.ActionName;
                    }
                    if (TraceList.Count >= 64)
                        TraceList.RemoveAt(0);
                    TraceList.Add(trace);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}