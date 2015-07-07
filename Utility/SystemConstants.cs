using System;
using System.Configuration;
using System.Security.Policy;

namespace Utility
{
    public static class SystemConstants
    {
        public static int TemporaryTokenExpiryTimeInMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["TemporaryTokenExpiryTimeInMinutes"]);
        public static readonly int CacheExpiryTimeInMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["CacheExpiryTimeInMinutes"]);
        public static int CallQuotaPerSecond = Convert.ToInt32(ConfigurationManager.AppSettings["CallQuotaPerSecond"]);
        public static readonly Url WebUrl = new Url(ConfigurationManager.AppSettings["iLoopWeb"]);
        public static readonly Url ImageServerAddress = new Url(ConfigurationManager.AppSettings["iLoopImageServer"]);

        public static readonly string MailServer = ConfigurationManager.AppSettings["MailServer"];
        public static readonly int MailServerPort = Convert.ToInt32(ConfigurationManager.AppSettings["MailServerPort"]);
        public static readonly string MailServerAddress = ConfigurationManager.AppSettings["MailServerAddress"];

        /// <summary>
        /// The Host address to connect to.
        /// </summary>
        public static readonly string JHost = ConfigurationManager.AppSettings["JHost"];
        /// <summary>
        /// The connection port used.
        /// </summary>
        public static readonly int JHostPort = Convert.ToInt32(ConfigurationManager.AppSettings["JHostPort"]);
    }
}
