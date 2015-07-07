using API.Filters;
using API.Formatters;
using API.Handlers;
using Elmah.Contrib.WebApi;
using FluentValidation;
using FluentValidation.WebApi;
using Microsoft.AspNet.WebApi.MessageHandlers.Compression;
using Microsoft.AspNet.WebApi.MessageHandlers.Compression.Compressors;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using Thrift;
using Utility;
using WebApiThrottle;

namespace API
{
    public static class WebApiConfig
    {
        public const string SystemRoutePrefix = "api/v1";
        public static Type[] ThriftExceptionTypes;
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            //Register routes
            config.Routes.MapHttpRoute("SimpleControllerWithAction", SystemRoutePrefix + "/{controller}/{action}", null, new { action = @"^((?!Post)(?!Put)(?!Get)(?!Delete)(?!Options).)*$" });
            config.Routes.MapHttpRoute("SimpleControllerWithSpecifiedActionVerb", SystemRoutePrefix + "/{controller}/{action}", null, new { action = @"^((Post)|(Put)|(Get)|(Delete)|(Options))*$" });
            config.Routes.MapHttpRoute("ComplexControllerWithActionAndSpecifiedActionVerb", SystemRoutePrefix + "/{controller}/{action}/{overrideverb}", null, new { action = @"^((?!Post)(?!Put)(?!Get)(?!Delete)(?!Patch)(?!Options).)*$", overrideverb = @"^((Post)|(Put)|(Get)|(Delete)|(Options))*$" });
            config.Routes.MapHttpRoute("SimpleControllerWithActionVerbDelete", SystemRoutePrefix + "/{controller}", new { action = "Delete" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) });
            config.Routes.MapHttpRoute("SimpleControllerWithActionVerbGet", SystemRoutePrefix + "/{controller}", new { action = "Get" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            config.Routes.MapHttpRoute("SimpleControllerWithActionVerbPost", SystemRoutePrefix + "/{controller}", new { action = "Post" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
            config.Routes.MapHttpRoute("SimpleControllerWithActionVerbPut", SystemRoutePrefix + "/{controller}", new { action = "Put" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) });
            config.Routes.MapHttpRoute("SimpleControllerWithActionVerbOptions", SystemRoutePrefix + "/{controller}", new { action = "Options" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Options) });

            //Add specific site(s) in the first "*".
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            config.Formatters.Insert(0, new JsonNetFormatter());

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/plain"));

            config.MessageHandlers.Add(new MethodOverrideHandler());
            config.MessageHandlers.Add(new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));
            config.MessageHandlers.Add(new MessageHandler());
            config.Services.Replace(typeof(IExceptionHandler), new ResponseExceptionHandler());
            config.MessageHandlers.Add(new ThrottlingHandler { Policy = new ThrottlePolicy(SystemConstants.CallQuotaPerSecond) { IpThrottling = true }, Repository = new CacheRepository() });

            //config.MessageHandlers.Add(new ResponseExceptionHandler());
            GlobalConfiguration.Configuration.Filters.Add(new ElmahHandleErrorApiAttribute());

            //config.Filters.Add(new Filters.ApiAuthorizeAttribute());
            config.Filters.Add(new ValidationAttribute());
            config.Filters.Add(new ExceptionFilterAttribute());
            //config.Formatters.Add(new FileMediaFormatter<DTO.Models.SignUp.SignUpRequest>());

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            config.Formatters.XmlFormatter.Indent = true;
            config.Formatters.XmlFormatter.UseXmlSerializer = true;

            FluentValidationModelValidatorProvider.Configure(config);
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            ThriftExceptionTypes = Assembly.Load("Service").GetTypes().Where(t => t.IsSubclassOf(typeof(TException))).ToArray();
        }
    }
}
