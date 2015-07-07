using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace API.Filters
{
    public class ForceHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var request = actionContext.Request;

            if (request.RequestUri.Scheme == Uri.UriSchemeHttps) return;

            const string html = "Https is required.";

            if (request.Method.Method == "GET")
            {
                actionContext.Response = request.CreateResponse(HttpStatusCode.Found);
                actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");

                var httpsNewUri = new UriBuilder(request.RequestUri) { Scheme = Uri.UriSchemeHttps, Port = 443 };

                actionContext.Response.Headers.Location = httpsNewUri.Uri;
            }
            else
            {
                actionContext.Response = request.CreateResponse(HttpStatusCode.NotFound);
                actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");
            }
        }
    }
}