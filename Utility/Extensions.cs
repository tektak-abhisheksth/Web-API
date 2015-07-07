using Model.Base;
using Model.Common;
using Model.Types;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Utility
{
    public static class Extensions
    {
        /// <summary>
        /// Generates the system-specific response format that is unique for all responses.
        /// </summary>
        /// <typeparam name="T">The DTO data type to be serialized and sent as data object.</typeparam>
        /// <param name="request">Current request in context.</param>
        /// <param name="statusCode">The status code to be sent to the client.</param>
        /// <param name="data">The response data to be sent to the client.</param>
        /// <param name="errorMessage">The message describing the error that occured.</param>
        /// <param name="subCode">Exception code as returned by JService.</param>
        /// <returns>HTTP response message.</returns>
        private static HttpResponseMessage SystemResponse<T>(this HttpRequestMessage request, SystemStatusCode statusCode, T data, string errorMessage, int subCode) where T : class
        {
            var response = new SystemResponse<T> { Status = statusCode, Data = data, Error = errorMessage, SubStatus = subCode };
            return request.CreateResponse((HttpStatusCode)statusCode, response);
        }

        /// <summary>
        /// Generates system-formatted unique response message for the client.
        /// </summary>
        /// <typeparam name="T">The type to return to the client.</typeparam>
        /// <param name="request">Current request in context.</param>
        /// <param name="dbStatusCode">Status code as returned from database, if any.</param>
        /// <param name="data">The payload to be returned of type T.</param>
        /// <param name="forceIncludeDataOnError">Direction as to whether or not data object be sent with the response even when there is error.</param>
        /// <param name="message">Message to be returned, if any.</param>
        /// <param name="subCode">Sub status code further elaborating the status code.</param>
        /// <returns>System-formatted response message.</returns>
        public static HttpResponseMessage SystemResponse<T>(this HttpRequestMessage request, SystemDbStatus dbStatusCode, T data = null, bool forceIncludeDataOnError = false, string message = null, int subCode = 0) where T : class
        {
            var status = new StatusData<T> { Status = dbStatusCode, Data = data, SubStatus = subCode };
            return request.SystemResponse(status, forceIncludeDataOnError, message);
        }


        /// <summary>
        /// Generates system-formatted unique response message for the client.
        /// </summary>
        /// <typeparam name="T">The type to return to the client.</typeparam>
        /// <param name="request">Current request in context.</param>
        /// <param name="status">Status information as returned from database, if any.</param>
        /// <param name="forceIncludeDataOnError">Direction as to whether or not data object be sent with the response even when there is error.</param>
        /// <param name="message">Message to be returned, if any.</param>
        /// <returns>System-formatted response message.</returns>
        public static HttpResponseMessage SystemResponse<T>(this HttpRequestMessage request, StatusData<T> status, bool forceIncludeDataOnError = false, string message = null) where T : class
        {
            HttpResponseMessage msg;

            //Overriding the status code when message is null.
            if (status.Data == null && status.Status == SystemDbStatus.Selected) status.Status = SystemDbStatus.NotFound;

            //If parameter is not explicitly provided, uses database-returned message.
            message = message ?? status.Message;

            var body = forceIncludeDataOnError ? status.Data : null;

            switch (status.Status)
            {
                case SystemDbStatus.Deleted:
                case SystemDbStatus.Selected:
                case SystemDbStatus.Updated:
                case SystemDbStatus.MetaUpdated:
                    msg = request.SystemResponse(SystemStatusCode.Success, status.Data, null, status.SubStatus);
                    break;
                case SystemDbStatus.Inserted:
                case SystemDbStatus.Flushed:
                    msg = request.SystemResponse(SystemStatusCode.Created, status.Data, null, status.SubStatus);
                    break;
                case SystemDbStatus.NoContent:
                    msg = request.SystemResponse<string>(SystemStatusCode.NoContent, null, null, status.SubStatus);
                    break;
                case SystemDbStatus.NotModified:
                    msg = request.SystemResponse<string>(SystemStatusCode.NotModified, null, message, status.SubStatus);
                    break;
                case SystemDbStatus.GeneralError:
                    msg = request.SystemResponse(SystemStatusCode.ServerError, body, message ?? SystemResponseMessage.GeneralException, status.SubStatus);
                    break;
                case SystemDbStatus.Duplicate:
                    msg = request.SystemResponse(SystemStatusCode.Invalid, body, message ?? SystemResponseMessage.RequestAlreadyApproved, status.SubStatus);
                    break;
                case SystemDbStatus.Pending:
                    msg = request.SystemResponse(SystemStatusCode.Invalid, body, message ?? SystemResponseMessage.RequestAlreadyPending, status.SubStatus);
                    break;
                case SystemDbStatus.Forbidden:
                    msg = request.SystemResponse(SystemStatusCode.Forbidden, body, message ?? SystemResponseMessage.ProceedingHalted, status.SubStatus);
                    break;
                case SystemDbStatus.NotFound:
                    msg = request.SystemResponse(SystemStatusCode.Invalid, body, message ?? SystemResponseMessage.NotFound, status.SubStatus);
                    break;
                case SystemDbStatus.NotSupported:
                    msg = request.SystemResponse(SystemStatusCode.Invalid, body, message ?? SystemResponseMessage.RequestNotValid, status.SubStatus);
                    break;
                case SystemDbStatus.Idempotent: //Same as duplicate, but with potential future changes
                    msg = request.SystemResponse(SystemStatusCode.Invalid, body, message ?? SystemResponseMessage.RequestAlreadyApproved, status.SubStatus);
                    break;
                default:
                    msg = request.SystemResponse(SystemStatusCode.Unauthorized, body, message ?? SystemResponseMessage.AuthorizationFailed, status.SubStatus);
                    break;
            }
            msg.Version = new Version(1, 0);
            return msg;
        }

        /// <summary>
        /// Compares current status with given status.
        /// </summary>
        /// <param name="currentStatus">Current status.</param>
        /// <param name="status">Status to compare with.</param>
        /// <returns>Result as to whether or not both match.</returns>
        public static bool IsStatusSame(this SystemDbStatus currentStatus, SystemDbStatus status)
        {
            return currentStatus == status;
        }

        /// <summary>
        /// Obtain current user's information.
        /// </summary>
        /// <typeparam name="T">The type to which the result is expected</typeparam>
        /// <param name="request">Current request.</param>
        /// <param name="property">The property which should be returned.</param>
        /// <returns>Current user's requested property in requested format.</returns>
        public static T GetUserInfo<T>(this HttpRequestMessage request, SystemSessionEntity property)
        {
            try
            {
                var currentSession = HttpContext.Current.Session;
                if (currentSession != null) return (T)currentSession[property.ToString()];
                return default(T);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Obtain current user's aggregated session information.
        /// </summary>
        /// <param name="request">Current request.</param>
        /// <returns>Current user's information stored in the user's session.</returns>
        public static SystemSession GetSession(this HttpRequestMessage request)
        {
            var currentSession = HttpContext.Current.Session;
            if (currentSession == null) return null;
            var session = new SystemSession
            {
                UserId = Convert.ToInt32(currentSession[0]),
                UserName = currentSession[1].ToString(),
                UserTypeId = Convert.ToByte(currentSession[2]),
                DeviceTypeId = currentSession[3].ToString(),
                DeviceId = currentSession[4].ToString(),
                LoginToken = currentSession[5].ToString(),
                IUserId = Convert.ToInt32(currentSession[0])
            };
            if (session.DeviceTypeId.Equals("W", StringComparison.OrdinalIgnoreCase))
                session.Ip = HttpContext.Current.Request.UserHostAddress;
            return session;
        }

        /// <summary>
        /// Adds space before capital letters in a sentence with missing capital letters.
        /// </summary>
        public static string AddSpaceBeforeCapitalLetters(this string text)
        {
            return string.Concat(text.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }

        public static string ToPascalCase(this string s)
        {
            if (string.IsNullOrWhiteSpace(s) || !char.IsLower(s[0]))
                return s;

            var str = char.ToUpper(s[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);

            if (s.Length > 1)
                str = str + s.Substring(1);

            return str;
        }

        public static JsonSerializerSettings Clone(this JsonSerializerSettings other)
        {
            var c = new JsonSerializerSettings
            {
                ContractResolver = other.ContractResolver,
                Converters = other.Converters,
                DateFormatHandling = other.DateFormatHandling,
                DateParseHandling = other.DateParseHandling,
                DateFormatString = other.DateFormatString,
                DateTimeZoneHandling = other.DateTimeZoneHandling,
                DefaultValueHandling = other.DefaultValueHandling,
                Formatting = other.Formatting,
                MissingMemberHandling = other.MissingMemberHandling,
                NullValueHandling = other.NullValueHandling,
                ObjectCreationHandling = other.ObjectCreationHandling,
                StringEscapeHandling = other.StringEscapeHandling,
                ReferenceResolver = other.ReferenceResolver,
                Binder = other.Binder,
                CheckAdditionalContent = other.CheckAdditionalContent,
                ConstructorHandling = other.ConstructorHandling,
                Context = other.Context,
                Culture = other.Culture,
                Error = other.Error,
                FloatFormatHandling = other.FloatFormatHandling,
                FloatParseHandling = other.FloatParseHandling,
                MaxDepth = other.MaxDepth,
                MetadataPropertyHandling = other.MetadataPropertyHandling,
                PreserveReferencesHandling = other.PreserveReferencesHandling,
                ReferenceLoopHandling = other.ReferenceLoopHandling,
                TraceWriter = other.TraceWriter,
                TypeNameAssemblyFormat = other.TypeNameAssemblyFormat,
                TypeNameHandling = other.TypeNameHandling
            };
            return c;
        }
    }
}
