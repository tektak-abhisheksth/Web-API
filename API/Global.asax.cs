using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using Elmah;
using TekTak.iLoop.Kauwa;

namespace API
{
    public class WebApiApplication : HttpApplication
    {
        //private const string DummyCacheItemKey = "iLoopCache";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutofacConfig.RegisterDependencies();
            //RegisterCacheEntry();
        }

        //protected void Application_BeginRequest()
        //{
        //    if (Request.Headers.AllKeys.Contains("Origin") && Request.HttpMethod == "OPTIONS")
        //        Response.Flush();
        //}

        //private void RegisterCacheEntry()
        //{
        //    Debug.WriteLine(DateTime.Now);
        //    if (null != HttpContext.Current.Cache[DummyCacheItemKey]) return;

        //    HttpContext.Current.Cache.Add(DummyCacheItemKey, "Test", null, DateTime.Now.AddMinutes(1), TimeSpan.Zero, CacheItemPriority.Normal, this.CacheItemRemovedCallback);
        //}

        //private void CacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        //{
        //    Debug.WriteLine("Cache item callback: " + DateTime.Now);

        //    // Do the service works

        //}

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest()) HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            var sessionId = Session.SessionID;
        }

        private static bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath != null && HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/" + WebApiConfig.SystemRoutePrefix);
        }

        private static bool IsLocal()
        {
            var httpRequestMessage = HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
            if (httpRequestMessage != null)
            {
                var localFlag = httpRequestMessage.Properties["MS_IsLocal"] as Lazy<bool>;
                return localFlag != null && localFlag.Value;
            }
            return false;
        }

        #region ELMAH Error logging
        ///// <summary>
        ///// Customizes the outgoing ELMAH mail.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void ErrorMail_Mailing(object sender, ErrorMailEventArgs e)
        //{
        //    try
        //    {
        //        //if (e.Error.Exception is ApplicationException || (e.Error.Exception is HttpUnhandledException && e.Error.Exception.InnerException != null && e.Error.Exception.InnerException is ApplicationException))
        //        {
        //            //e.Mail.Priority = System.Net.Mail.MailPriority.High;
        //            //e.Mail.Subject = "iLoop Web API Exception";
        //            e.Mail.Sender = new MailAddress(e.Mail.Sender.Address, "iLoop Support");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        /// <summary>
        /// Filters the outgoing ELMAH mail to bypass mail sending for certain scenarios.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ErrorMail_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            try
            {
                if (e.Exception.GetBaseException() is SessionException
                    || e.Exception.GetBaseException() is HttpException
                    || e.Exception.GetBaseException() is WebException
                     || !IsWebApiRequest()
                    || IsLocal()
                    )
                    e.Dismiss();
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
