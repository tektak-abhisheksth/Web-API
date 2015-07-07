using API.Areas.HelpPage;
using API.Formatters;
using API.Handlers;
using Model.WebClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using Utility;

namespace API.Controllers.WebClient
{
    /// <summary>
    /// The web controller for the API helper site.
    /// </summary>
    public class HomeController : Controller
    {
        private static readonly List<string> Tips;

        static HomeController()
        {
            Tips = ApiTips();
            //Tips.AddRange(GetMurphyLaws());
        }

        /// <summary>
        /// The landing page.
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 5)]
        public ActionResult Index()
        {
            ViewBag.Title = "iLoop Web API";

            return View();
        }

        /// <summary>
        /// The developer's page.
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 5)]
        public ActionResult Dev()
        {
            ViewBag.Title = "iLoop Developer Notes";

            return View();
        }

        /// <summary>
        /// iLoop system's conventions.
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 5)]
        public ActionResult Conventions()
        {
            ViewBag.Title = "iLoop Conventions";

            var enums = GetTypesFromNamespace(Assembly.Load("Model"), "Model.Types").Where(x => x.BaseType.Name.Equals("Enum"));
            var mdg = GlobalConfiguration.Configuration.GetModelDescriptionGenerator();
            var enumLst = enums.Select(type => mdg.GenerateEnumTypeModelDescription(type)).Where(enumInfo => enumInfo.Added.HasValue);
            return View(enumLst.OrderBy(x =>
            {
                var dff = Math.Abs((DateTime.Now - x.Added.GetValueOrDefault()).TotalDays);
                return dff >= 7 ? int.MaxValue : dff;
            }).ThenBy(x => x.Name));
        }

        /// <summary>
        /// iLoop API's request and response tracing.
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        //[OutputCache(Duration = 1)]
        public ActionResult Trace(long time = 0, int rnd = 0)
        {
            ViewBag.Title = "iLoop API Trace";
            var lastTime = new DateTime(time);

            if (Request.IsAjaxRequest() && !MessageHandler.TraceList.Any(x => x.Time > lastTime))
                return null;

            var traces = MessageHandler.TraceList.Where(x => x.Time > lastTime && x.RandomNumber != rnd).OrderByDescending(x => x.Time).ToList();

            foreach (var trace in traces)
                try
                {
                    if (trace.Request.Length > 0 && Helper.IsJson(trace.Request))
                        trace.Request =
                            JsonConvert.SerializeObject(
                                JsonConvert.DeserializeObject(trace.Request, JsonNetFormatter.Settings),
                                JsonNetFormatter.Formatting, JsonNetFormatter.Settings);
                    //if (trace.Response.Length > 0 && Helper.IsJson(trace.Response))
                    //{
                    //    trace.Response = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(trace.Response), Formatting.Indented);

                    //    //dynamic data = JObject.Parse(trace.Response);
                    //    //var str = data.Status.ToString();
                    //    //trace.Status = str;
                    //}
                }
                catch (Exception)
                { }

            if (Request.IsAjaxRequest())
                return PartialView("_Traces", traces);
            return View(traces);
        }

        /// <summary>
        /// The FAQs page.
        /// </summary>
        /// <returns></returns>
        [OutputCache(Duration = 5)]
        public ActionResult Faqs()
        {
            ViewBag.Title = "iLoop Frequently Asked Questions";

            return View();
        }

        /// <summary>
        /// iLoop API's system settings.
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        [OutputCache(Duration = 2)]
        public ActionResult Settings()
        {
            ViewBag.Title = "iLoop API Settings";
            var globalSettings = new GlobalSettings
            {
                Indent = JsonNetFormatter.Formatting == Formatting.Indented,
                UseIsoFormat = JsonNetFormatter.Settings.DateFormatHandling == DateFormatHandling.IsoDateFormat,
                RemoveDefaults = JsonNetFormatter.Settings.DefaultValueHandling == DefaultValueHandling.Ignore,
                RemoveNulls = JsonNetFormatter.Settings.NullValueHandling == NullValueHandling.Ignore,
                UseCamelCase = JsonNetFormatter.Settings.ContractResolver is CamelCasePropertyNamesContractResolver,
                TemporaryTokenExpiryTimeInMinutes = SystemConstants.TemporaryTokenExpiryTimeInMinutes
            };

            return View(globalSettings);
        }

        /// <summary>
        /// iLoop API's system settings.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        public ActionResult Settings(GlobalSettings settings)
        {
            JsonNetFormatter.Formatting = settings.Indent ? Formatting.Indented : Formatting.None;
            JsonNetFormatter.Settings.DefaultValueHandling = settings.RemoveDefaults ? DefaultValueHandling.Ignore : DefaultValueHandling.Include;
            JsonNetFormatter.Settings.NullValueHandling = settings.RemoveNulls ? NullValueHandling.Ignore : NullValueHandling.Include;
            JsonNetFormatter.Settings.DateFormatHandling = settings.UseIsoFormat ? DateFormatHandling.IsoDateFormat : DateFormatHandling.MicrosoftDateFormat;
            JsonNetFormatter.Settings.ContractResolver = settings.UseCamelCase ? new CamelCasePropertyNamesContractResolver() : new DefaultContractResolver();
            SystemConstants.TemporaryTokenExpiryTimeInMinutes = settings.TemporaryTokenExpiryTimeInMinutes;

            using (var sw = new StreamWriter(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "iLoopAPISettings.txt"), false))
                sw.Write(JsonConvert.SerializeObject(settings, Formatting.Indented));
            return null;
        }

        /// <summary>
        /// iLoop API's latest data.
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpGet]
        [OutputCache(Duration = 1)]
        public ActionResult GetData()
        {
            if (MessageHandler.TraceList != null && MessageHandler.TraceList.Any())
            {
                var latestData = MessageHandler.TraceList.LastOrDefault(x => !string.IsNullOrWhiteSpace(x.Status) && x.Status.StartsWith("2") && x.UserId > 0 && !x.DeviceTypeId.Equals("W", StringComparison.OrdinalIgnoreCase));
                if (latestData != null)
                {
                    var response = new JsonResult
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new { key = latestData.GetAuthorizationToken(), userId = latestData.UserId, userName = latestData.UserName, userTypeId = latestData.UserTypeId, deviceTypeId = latestData.DeviceTypeId, deviceId = latestData.DeviceId, loginToken = latestData.Token }
                    };
                    return response;
                }
            }
            return null;
        }

        [System.Web.Mvc.HttpGet]
        //[OutputCache(Duration = 1)]
        public ActionResult GetTip(int index = 0)
        {
            index = Enumerable.Range(0, Tips.Count).OrderBy(x => Guid.NewGuid()).FirstOrDefault(x => x != index);
            var response = new JsonResult
                   {
                       JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                       Data = new { index, tip = Tips[index] }
                   };
            return response;
        }

        [System.Web.Mvc.NonAction]
        private static IEnumerable<Type> GetTypesFromNamespace(Assembly assembly, string desiredNamespace)
        {
            return assembly.GetTypes().Where(type => type.Namespace == desiredNamespace);
        }

        [System.Web.Mvc.NonAction]
        private static List<string> ApiTips()
        {
            var lstTips = new List<string>
        {
            "Online JSON viewer <a href=\"http://jsonviewer.stack.hu/\" title=\"Go to site.\" target=\"_blank\">jsonviewer.stack.hu</a> is a site to validate and format JSON string.",
            "<strong>Dev Notes</strong> contains detailed elaboration of the technical details an API developer needs to understand.",
            "<strong>Conventions</strong> consists of meanings to system-used codes. New entries are marked as <strong>NEW</strong>.",
            "Live requests can be directly viewed via the <strong>Trace</strong> page.",
            "Each API can be independently tested via <strong>API</strong> page where all exposed APIs are categorized.",
            "During API testing, a developer can submit a bug via <strong>Report a bug</strong> along with data and other details.",
            "<strong>Change Log</strong> entails latest additions/modifications made by Web API team, enlisted by deployment dates.",
            "The <strong>API Settings</strong> page is used to tailor the system as per admin's needs.",
            "Request/Response can be compressed to reduce bandwidth. See <strong>Dev Notes</strong>.",
            "Request type can be modified (tunneling) by suffixing required verb in the URI. See <strong>Dev Notes</strong>.",
            "Both JSON and XML request/response are supported. Client can send request in one and receive response in other format if desired. See <strong>Dev Notes</strong>.",
            "For clarity, JSON response can be indented. See <strong>Settings</strong>.",
            "Default and/or null values can be excluded from response to reduce response size. See <strong>Settings</strong>.",
            "Date format can be either 2015-01-13T07:56:25.0410012Z or \\/Date(1421136153248)\\/. See <strong>Settings</strong>.",
            "Response JSON can be requested in camel case for each API (See <strong>Dev Notes</strong>) or modified for the entire system (See <strong>Settings</strong>).",
            "Mandatory request parameters can be set for all requests during API testing from top right menu (See <strong>Login Defaults</strong>).",
            "Notepad++ can be used to encode/decode Base64 strings. It also has a simple JSON viewer and formatter."
        };
            return lstTips;
        }

        [System.Web.Mvc.NonAction]
        private static IEnumerable<string> GetMurphyLaws()
        {
            var murphyTips = new List<string>
            {
                "If anything can go wrong, it will.",
                "New systems generate new problems.",
                "Any given program, when running, is obsolete.",
                "You never catch on until after the test.",
                "If it's stupid but it works, it ain't stupid.",
                "Your best idea is already copyrighted.",
                "If a program is useful, it will have to be changed.",
                "If a program is useless, it will have to be documented.",
                "Any given program will expand to fill all the available memory.",
                "The value of a program is inversely proportional to the weight of its output.",
                "Program complexity grows until it exceeds the capability of the programmer who must maintain it.",
                "At least one bug will be observed after the author leaves the organization.",
                "Bugs will appear in one part of a working program when another 'unrelated' part is modified.",
                "Undetectable errors are infinite in variety, in contrast to detectable errors, which by definition are limited.",
                "Adding manpower to a late software project makes it later.",
                "Make it possible for programmers to write programs in English, and you will find that programmers can not write in English.",
                "A working program is one that has only unobserved bugs.",
                "No matter how many resources you have, it is never enough.",
                "If a program has not crashed yet, it is waiting for a critical moment before it crashes.",
                "Software bugs are impossible to detect by anybody except the end user.",
                "All Constants are Variables.",
                "No matter how hard you work, the boss will only appear when you access the Internet.",
                "The number of bugs always exceeds the number of lines found in a program.",
                "Every non-trivial program contains at least one bug.",
                "An expert is someone brought in at the last minute to share the blame.",
                "Debugging is at least twice as hard as writing the program in the first place. So if your code is as clever as you can possibly make it, then by definition you're not smart enough to debug it.",
                "For any given software, the moment you manage to master it, a new version appears.",
                "A patch is a piece of software which replaces old bugs with new bugs.",
                "The chances of a program doing what it's supposed to do is inversely proportional to the number of lines of code used to write it.",
                "Failure is not an option, it's included with the software.",
                "The probability of bugs appearing is directly proportional to the number and importance of people watching.",
                "An employee's rank is in inverse proportion to his use of a computer.",
                "The only program that runs perfectly every time, is a virus.",
                "Make a system that even a moron can use, and only a moron will use it.",
                "The likelihood of problems occurring is inversely proportional to the amount of time remaining before the deadline.",
                "The troubleshooting guide contains the answer to every problem except yours.",
                "No matter what problem you have with your computer - Its always Microsoft's fault.",
                "A program that compile on the first run has an error in the algorithm.",
                "Walking on water and developing software to specification are easy as long as both are frozen.",
                "When designing a program to handle all possible dumb errors, nature creates a dumber user.",
                "There is an inverse relationship between an organization's hierarchy and its understanding of computers.",
                "Computers never work the way they are supposed to. Especially when nothing is wrong with them.",
                "A program will only work the way you think is should when you don't care if it does."
            };

            for (var i = 0; i < murphyTips.Count; ++i)
                murphyTips[i] += " -Murphy's law";

            return murphyTips;
        }
    }
}
