using Model.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Xml;

namespace Utility
{
    public static class Helper
    {
        private static readonly RNGCryptoServiceProvider RngCsp = new RNGCryptoServiceProvider();
        private static readonly byte[] RandomNumber = new byte[1];

        /// <summary>
        /// Sends mail.
        /// </summary>
        /// <param name="emailRecipient">The email recipient.</param>
        /// <param name="emailSubject">The email subject.</param>
        /// <param name="emailBody">The email body.</param>
        /// <param name="isHtml">An indication as to whether the content is HTML or plain text.</param>
        /// <param name="cc">The cc email recipient.</param>
        /// <param name="bcc">The bcc email recipient.</param>
        /// <param name="priority">The priority of the mail.</param>
        /// <param name="attachments">Absolute filename of any attachments.</param>
        /// <param name="bodyImages">Absolute filename of any images embedded in the body.</param>
        public static void SendMail(string emailRecipient, string emailSubject, string emailBody, bool isHtml = true, string cc = null, string bcc = null, MailPriority priority = MailPriority.Normal, List<string> attachments = null, List<string> bodyImages = null)
        {
            var mail = new MailMessage();
            var client = new SmtpClient(SystemConstants.MailServer) { Port = SystemConstants.MailServerPort };
            mail.From = new MailAddress(SystemConstants.MailServerAddress);
            mail.To.Add(emailRecipient);
            if (!string.IsNullOrWhiteSpace(cc)) mail.CC.Add(cc);
            if (!string.IsNullOrWhiteSpace(bcc)) mail.Bcc.Add(bcc);

            var imageGuids = new List<string>();
            if (bodyImages != null && bodyImages.Any())
            {
                imageGuids.AddRange(Enumerable.Range(0, bodyImages.Count).Select(x => Guid.NewGuid().ToString()));
                emailBody = string.Format(emailBody, imageGuids.ToArray());
                var htmlView = AlternateView.CreateAlternateViewFromString(emailBody, null, "text/html");
                for (var index = 0; index < bodyImages.Count; index++)
                {
                    var bodyImage = bodyImages[index];
                    var image = HostingEnvironment.MapPath(bodyImage);
                    var imgGuid = imageGuids[index];
                    if (image != null)
                    {
                        var leftImageLink = new LinkedResource(image, "image/jpg")
                        {
                            ContentId = imgGuid,
                            TransferEncoding = TransferEncoding.Base64
                        };
                        htmlView.LinkedResources.Add(leftImageLink);
                    }
                }
                mail.AlternateViews.Add(htmlView);
            }

            mail.Body = emailBody;
            mail.Subject = emailSubject;
            mail.IsBodyHtml = isHtml;
            mail.Priority = priority;

            if (attachments != null && attachments.Any())
                foreach (var attachment in attachments)
                    mail.Attachments.Add(new Attachment(attachment));

            //SmtpServer.Credentials = new System.Net.NetworkCredential("abhisheksth@gmail.com", "your password");
            //SmtpServer.EnableSsl = true;

            client.Send(mail);
        }

        /// <summary>
        /// Generates a random number.
        /// </summary>
        /// <param name="minValue">The minimum value in the range.</param>
        /// <param name="maxValue">The maximum value in the range.</param>
        /// <returns>A pseudo-random number.</returns>
        public static int GenerateRandomCode(int minValue, int maxValue)
        {
            RngCsp.GetBytes(RandomNumber);
            if (minValue > maxValue)
            {
                var t = minValue;
                minValue = maxValue;
                maxValue = t;
            }
            var r = new Random((int)DateTime.Now.Ticks & (0x0000FFFF + RandomNumber[0]));
            return r.Next(minValue, maxValue);
        }

        /// <summary>
        /// Generates a random number.
        /// </summary>
        /// <param name="minValue">The minimum value in the range.</param>
        /// <param name="maxValue">The maximum value in the range.</param>
        /// <returns>A pseudo-random number.</returns>
        public static long GenerateRandomCode(long minValue, long maxValue)
        {
            RngCsp.GetBytes(RandomNumber);
            if (minValue > maxValue)
            {
                var t = minValue;
                minValue = maxValue;
                maxValue = t;
            }
            var r = new Random((int)DateTime.Now.Ticks & (0x0000FFFF + RandomNumber[0]));
            return (long)Math.Floor(r.NextDouble() * maxValue + minValue);
        }

        /// <summary>
        /// Generates a random number.
        /// </summary>
        /// <param name="minValue">The minimum value in the range.</param>
        /// <param name="maxValue">The maximum value in the range.</param>
        /// <returns>A pseudo-random number.</returns>
        public static float GenerateRandomCode(float minValue, float maxValue)
        {
            RngCsp.GetBytes(RandomNumber);
            if (minValue > maxValue)
            {
                var t = minValue;
                minValue = maxValue;
                maxValue = t;
            }
            var r = new Random((int)DateTime.Now.Ticks & (0x0000FFFF + RandomNumber[0]));
            return (float)Math.Floor(r.NextDouble() * maxValue + minValue);
        }

        /// <summary>
        /// Determines if the given string is Base64 encoded or not.
        /// </summary>
        /// <param name="s">The string to be verified.</param>
        /// <returns>An indication as to whether or not the string is Base64 encoded.</returns>
        public static bool IsBase64String(string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        public static DateTime RoundedMinute(DateTime dt, bool decrement)
        {
            var newDt = dt.AddMinutes(-(dt.Minute % 15)).AddMinutes(decrement ? 0 : 15);
            return DateTime.Parse(String.Format("{0:yyyy-MM-dd HH:mm:00}", (newDt)));
        }

        public static bool IsJson(string input)
        {
            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]");
        }

        public static bool IsXml(string input)
        {
            input = input.Trim();
            return input.StartsWith("<") && input.EndsWith(">");
        }

        public static bool IsEmail(string input)
        {
            return Regex.IsMatch(input, RegexPattern.Email);
        }

        public static bool UrlExists(string url)
        {
            try
            {
                new WebClient().DownloadData(url);
                return true;
            }
            catch (WebException e)
            {
                if (((HttpWebResponse)e.Response).StatusCode == HttpStatusCode.NotFound)
                    return false;
                throw;
            }
        }

        public static string NameOf<T>(Expression<Func<T>> expr)
        {
            return ((MemberExpression)expr.Body).Member.Name;
        }

        public static T Clone<T>(T source)
        {
            try
            {
                var serialized = JsonConvert.SerializeObject(source);
                return JsonConvert.DeserializeObject<T>(serialized);
            }
            catch (Exception)
            {
                return source;
            }
        }
        public static UserDeviceInfoInternal GetUserDeviceDetails(HttpRequestBase request)
        {
            var user = new UserDeviceInfoInternal();
            user.MacAddress = NetworkInterface.GetAllNetworkInterfaces()[0].GetPhysicalAddress().ToString();
            user.Browser = request.Browser.Browser;

            user.IpAddress = GetClientIP(request);
            using (var dt = GetLocation(user.IpAddress))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    user.AreaCode = dt.Rows[0]["AreaCode"].ToString();
                    user.City = dt.Rows[0]["City"].ToString();
                    //user.ClientMachineName = Dns.GetHostEntry(request.ServerVariables["remote_addr"]).HostName;
                    user.CountryCode = dt.Rows[0]["CountryCode"].ToString();
                    user.CountryName = dt.Rows[0]["Country"].ToString();
                    user.HasDaylightSavings = Convert.ToBoolean(dt.Rows[0]["HasDaylightSavings"]);
                    user.Latitude = dt.Rows[0]["Latitude"].ToString();
                    user.Longitude = dt.Rows[0]["Longitude"].ToString();
                    user.Organization = dt.Rows[0]["Organization"].ToString();
                    user.Region = dt.Rows[0]["RegionName"].ToString();
                    user.StateProvince = dt.Rows[0]["StateProvince"].ToString();
                    user.TimeZone = dt.Rows[0]["TimeZone"].ToString();
                }
            }
            return user;
        }

        public static DataTable GetLocation(string ipaddress)
        {
            ipaddress = "117.121.237.10";
            //Create a WebRequest
            //http://ws.cdyne.com/ip2geo/ip2geo.asmx/ResolveIP?ipAddress=117.121.237.10&licenseKey=0
            var rssReq = WebRequest.Create(string.Format("http://ws.cdyne.com/ip2geo/ip2geo.asmx/ResolveIP?ipAddress={0}&{1}", ipaddress, "licenseKey=0"));
            //Create a Proxy
            var px = new WebProxy(string.Format("http://ws.cdyne.com/ip2geo/ip2geo.asmx/ResolveIP?ipAddress={0}&{1}", ipaddress, "licenseKey=0"), true);
            //Assign the proxy to the WebRequest
            rssReq.Proxy = px;
            //Set the timeout in Seconds for the WebRequest
            rssReq.Timeout = 10000;
            try
            {
                //Get the WebResponse 
                var rep = rssReq.GetResponse();
                //Read the Response in a XMLTextReader
                var xtr = new XmlTextReader(rep.GetResponseStream());
                //Create a new DataSet
                var ds = new DataSet();
                //Read the Response into the DataSet
                ds.ReadXml(xtr);

                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return null;
        }

        private static string GetClientIP(HttpRequestBase Request)
        {
            string result = string.Empty;
            string ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ip))
            {
                string[] ipRange = ip.Split(',');
                int le = ipRange.Length - 1;
                result = ipRange[0];
            }
            else
            {
                result = Request.ServerVariables["REMOTE_ADDR"];
            }

            return result;
        }

        public class DbGeographyConverter : JsonConverter
        {
            private const string LATITUDE_KEY = "latitude";
            private const string LONGITUDE_KEY = "longitude";

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(DbGeography);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                    return default(DbGeography);

                var jObject = JObject.Load(reader);

                if (!jObject.HasValues || (jObject.Property(LATITUDE_KEY) == null || jObject.Property(LONGITUDE_KEY) == null))
                    return default(DbGeography);

                var wkt = string.Format(CultureInfo.InvariantCulture, "POINT({1} {0})", jObject[LATITUDE_KEY], jObject[LONGITUDE_KEY]);
                return DbGeography.FromText(wkt, DbGeography.DefaultCoordinateSystemId);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var dbGeography = value as DbGeography;

                serializer.Serialize(writer, dbGeography == null || dbGeography.IsEmpty ? null : new { latitude = dbGeography.Latitude.Value, longitude = dbGeography.Longitude.Value });
            }
        }
    }
}
