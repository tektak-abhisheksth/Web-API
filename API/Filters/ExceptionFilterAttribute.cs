using Model.Base;
using Model.Types;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace API.Filters
{
    /// <summary>
    /// Represents an attribute that is used to handle exception during action processing.
    /// </summary>
    public class ExceptionFilterAttribute : System.Web.Http.Filters.ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionContext)
        {
            //Log errors here
            //Elmah.ErrorLog.GetDefault(HttpContext.Current).Log(new Elmah.Error(actionContext.Exception));
            var exceptionCode = 0;

            var errMsg = actionContext.Exception.Message;
            SystemStatusCode status;
            switch (errMsg)
            {
                case "Invalid input format.":
                    status = SystemStatusCode.Invalid;
                    break;
                default:
                    status = SystemStatusCode.ServerError;
                    break;
            }

            if (WebApiConfig.ThriftExceptionTypes.FirstOrDefault(x => x == actionContext.Exception.GetType()) != null)
            {
                dynamic e = actionContext.Exception;
                exceptionCode = Convert.ToInt32(e.Code);
                var exp = ServiceExceptions.JExceptions.FirstOrDefault(x => x.ExceptionCode == exceptionCode);
                if (exp != null)
                {
                    errMsg = exp.Message;
                    status = (SystemStatusCode)exp.StatusCode;
                }
            }

            actionContext.Response = actionContext.Request.CreateResponse((HttpStatusCode)status, new SystemResponse<string> { Status = status, Data = null, Error = errMsg, SubStatus = exceptionCode });

            //actionContext.Response.ReasonPhrase = "Server Exception";

            //throw new HttpResponseException(new HttpResponseMessage(SystemStatusCode.Error)
            //{               
            //    Content = new StringContent(actionContext.Exception.Message),
            //    ReasonPhrase = "APIException";
            //});
        }
    }
}