using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace API.Handlers
{
    /// <summary>
    /// Performs an intercept on incoming request, an does an method override as per user's request.
    /// Removed for now, as tunneling isn't suitable with System's Route Config. Also, PUT, POST and DELETE have separate models so tunneling to another action is not a good behavior.
    /// </summary>
    public class MethodOverrideHandler : DelegatingHandler
    {
        private const string Header = "X-HTTP-Method-Override";

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //if (request.Method == HttpMethod.Options)
            //    return Task<HttpResponseMessage>.Factory.StartNew(() =>
            //    {
            //        var response = new HttpResponseMessage(HttpStatusCode.OK);
            //        response.Headers.Add("Access-Control-Allow-Origin", "*");
            //        var v =
            //            request.Headers.FirstOrDefault(
            //                x => x.Key.Equals("Access-Control-Request-Method", StringComparison.OrdinalIgnoreCase))
            //                .Value;
            //        response.Headers.Add("Access-Control-Allow-Methods", v);
            //        return response;
            //    });

            var verbs = new[] { "Post", "Put", "Get", "Delete" };
            var last = request.RequestUri.AbsolutePath.Split('/').LastOrDefault();
            var isDirectVerb = (verbs.Any(x => x.Equals(last, StringComparison.OrdinalIgnoreCase)));
            if (isDirectVerb || request.Headers.Contains(Header))
            {
                var method = isDirectVerb ? last : request.Headers.GetValues(Header).FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(method))
                    request.Method = new HttpMethod(method);
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}