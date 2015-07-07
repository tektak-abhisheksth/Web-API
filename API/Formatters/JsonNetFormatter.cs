using Model.WebClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using Utility;

namespace API.Formatters
{
    public class JsonNetFormatter : MediaTypeFormatter
    {
        public static Formatting Formatting = Formatting.Indented;
        private static JsonSerializer _jSerializer;
        public static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            //NullValueHandling = NullValueHandling.Ignore,
            //DefaultValueHandling = DefaultValueHandling.Ignore,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateParseHandling = DateParseHandling.None,
            //DateTimeZoneHandling = DateTimeZoneHandling.Local,
            ContractResolver = new DefaultContractResolver()
        };

        public JsonNetFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            //Settings.Converters.Add(new StringEnumConverter());
            _jSerializer = new JsonSerializer();
            _jSerializer.DateParseHandling = DateParseHandling.None;
            _jSerializer.Converters.Add(new IsoDateTimeConverter());
            //jSerializer.NullValueHandling = NullValueHandling.Ignore;

            if (File.Exists(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "iLoopAPISettings.txt")))
                using (var sr = new StreamReader(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "iLoopAPISettings.txt")))
                {
                    try
                    {
                        var settings = JsonConvert.DeserializeObject<GlobalSettings>(sr.ReadToEnd());

                        Formatting = settings.Indent ? Formatting.Indented : Formatting.None;
                        Settings.DefaultValueHandling = settings.RemoveDefaults ? DefaultValueHandling.Ignore : Settings.DefaultValueHandling;
                        Settings.NullValueHandling = settings.RemoveNulls ? NullValueHandling.Ignore : Settings.NullValueHandling;
                        Settings.DateFormatHandling = settings.UseIsoFormat ? DateFormatHandling.IsoDateFormat : Settings.DateFormatHandling;
                        Settings.ContractResolver = settings.UseCamelCase ? new CamelCasePropertyNamesContractResolver() : Settings.ContractResolver;
                        SystemConstants.TemporaryTokenExpiryTimeInMinutes = settings.TemporaryTokenExpiryTimeInMinutes;
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }


            //JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            //{
            //    Formatting = Formatting.Indented,
            //    DateFormatHandling = DateFormatHandling.IsoDateFormat,
            //    DefaultValueHandling = DefaultValueHandling.Populate,
            //    NullValueHandling = NullValueHandling.Include
            //    //ContractResolver = new CamelCasePropertyNamesContractResolver()
            //};
        }

        public override bool CanWriteType(Type type)
        {
            // don't serialize JsonValue structure use default for that
            if (type == typeof(JValue) || type == typeof(JObject) || type == typeof(JArray))
                return false;
            return true;
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream stream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var taskSource = new TaskCompletionSource<object>();

            try
            {
                using (var sr = new StreamReader(stream))
                {
                    using (var jReader = new JsonTextReader(sr))
                    {
                        var val = _jSerializer.Deserialize(jReader, type);
                        taskSource.SetResult(val);
                    }
                }
            }
            catch (Exception)
            {
                var ex = new Exception("Invalid input format.");
                taskSource.SetException(ex);
            }
            return taskSource.Task;
            //var task = Task<object>.Factory.StartNew(() =>
            //{
            //    using (var sr = new StreamReader(stream))
            //    {
            //        try
            //        {
            //            //var s = sr.ReadToEnd();
            //            //stream.Seek(0, SeekOrigin.Begin);
            //            using (var jReader = new JsonTextReader(sr))
            //            {
            //                var val = jSerializer.Deserialize(jReader, type);
            //                return val;
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            throw new HttpResponseException(HttpStatusCode.BadRequest);
            //        }
            //    }
            //});
            //return task;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContent content, TransportContext transportContext)
        {
            var formatRequest = GetFormatType();
            var task = Task.Factory.StartNew(() =>
            {
                var settings = Settings;

                if (formatRequest != null)
                {
                    settings = Settings.Clone();
                    settings.ContractResolver = formatRequest;
                }
                var json = JsonConvert.SerializeObject(value, Formatting, settings);
                var buf = Encoding.UTF8.GetBytes(json);
                stream.Write(buf, 0, buf.Length);
                stream.Flush();
            });
            return task;
        }

        private IContractResolver GetFormatType()
        {
            var formatType = HttpContext.Current.Request.Headers.Get("Format");
            if (formatType != null && formatType.Equals("C", StringComparison.OrdinalIgnoreCase))
                return new CamelCasePropertyNamesContractResolver();
            return null;
        }
    }
}