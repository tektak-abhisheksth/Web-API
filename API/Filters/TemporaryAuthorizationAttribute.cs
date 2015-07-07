using Model.Base;
using Model.Types;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using Utility;

namespace API.Filters
{
    /// <summary>
    /// Represents an attribute that is used to handle temporary token validation after model binding.
    /// </summary>
    public class TemporaryAuthorizationAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string deviceId;
            if (actionContext.Request.Method == HttpMethod.Get)
                deviceId = (string)actionContext.ActionArguments["deviceId"];
            else
            {
                var request = (RequestBaseAnonymous)actionContext.ActionArguments.Values.FirstOrDefault();
                deviceId = request != null ? request.DeviceId : string.Empty;
            }
            if (deviceId.Length < 10 || deviceId.Length > 200)
            {
                actionContext.Response = actionContext.Request.SystemResponse<string>(SystemDbStatus.NotSupported, message: "The field deviceId must be a string with a minimum length of 10 and a maximum length of 200.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(deviceId) && actionContext.Request.Headers.Authorization != null &&
                Encryptor.VerifyToken(actionContext.Request.Headers.Authorization.ToString(), deviceId) == 1)
            {
                base.OnActionExecuting(actionContext);
                return;
            }

            actionContext.Response = actionContext.Request.SystemResponse<string>(SystemDbStatus.Unauthorized);
        }
    }
}