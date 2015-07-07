using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Model.Types;
using TekTak.iLoop.Kauwa;
using Utility;

namespace API.Handlers
{
    /// <summary>
    /// Handles and formats all exceptions to produce customized response.
    /// </summary>
    public class ResponseExceptionHandler : ExceptionHandler
    {
        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }

        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var result = context.ExceptionContext.Response;
            var trace = MessageHandler.ParseRequest(request);

            if (result == null)
            {
                string err;
                try
                {
                    err = context.ExceptionContext.Exception.InnerException.InnerException.Message;
                }
                catch (Exception)
                {
                    err = "Server initialization exception.";
                }
                //context.ExceptionContext.Response = context.Request.SystemResponse<string>(SystemDbStatus.GeneralError, null, false, err, 25);
                var response = context.Request.SystemResponse<string>(SystemDbStatus.GeneralError, null, false, err, 25);
                MessageHandler.ParseResponse(trace, response);
                throw new HttpResponseException(response);
            }

            if (result.StatusCode == HttpStatusCode.InternalServerError && string.IsNullOrEmpty(result.Version.ToString()))
            {
                var exceptionResult = string.Format("Response exception: Path({0}) Status({1}) ", request.RequestUri, result.StatusCode);

                if (result.Content != null)
                {
                    var exceptionReadTask =
                        result.Content.ReadAsStringAsync();

                    exceptionReadTask.Wait(cancellationToken);
                    exceptionResult = string.Concat(exceptionResult, "Message: ", exceptionReadTask.Result);
                }
                var exceptionCode = (context.ExceptionContext.Exception is UserException) ? (context.ExceptionContext.Exception as UserException).Code : 0;
                context.ExceptionContext.Response = result.RequestMessage.SystemResponse<string>(SystemDbStatus.GeneralError, null, false, exceptionResult, exceptionCode);
                MessageHandler.ParseResponse(trace, context.ExceptionContext.Response);
            }
            return null;
        }

        //private class GeneralExceptionHandler : IHttpActionResult
        //{
        //    public HttpRequestMessage Request { get; set; }

        //    public string Content { get; set; }

        //    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        //    {
        //        HttpResponseMessage response =
        //                         new HttpResponseMessage(HttpStatusCode.InternalServerError);
        //        response.Content = new StringContent(Content);
        //        response.RequestMessage = Request;
        //        return Task.FromResult(response);
        //    }
        //}
    }
}

//protected override Task<HttpResponseMessage> SendAsync(
//    HttpRequestMessage request,
//    CancellationToken cancellationToken)
//{
//    return base
//        .SendAsync(request, cancellationToken)
//        .ContinueWith(response =>
//        {
//            var result = response.Result;
//            if (result.StatusCode == HttpStatusCode.InternalServerError && string.IsNullOrEmpty(result.Version.ToString()))
//            {
//                var exceptionResult = string.Format(
//                     "Response exception: Path({0}) Status({1}) ",
//                     request.RequestUri,
//                     result.StatusCode);

//                if (result.Content != null)
//                {
//                    var exceptionReadTask =
//                           result.Content.ReadAsStringAsync();

//                    exceptionReadTask.Wait(cancellationToken);
//                    exceptionResult = string.Concat(exceptionResult, "Message: ", exceptionReadTask.Result);
//                }

//                return result.RequestMessage.SystemResponse<string>(SystemDbStatus.GeneralError, null, exceptionResult);
//            }

//            return result;
//        }, cancellationToken);
//}
