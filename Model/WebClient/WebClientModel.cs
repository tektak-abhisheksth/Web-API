using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.WebClient
{
    public sealed class Trace
    {
        public string Uri { get; set; }
        public string Action { get; set; }
        public string FriendlyURI { get; set; }
        public string RequestType { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int UserTypeId { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Status { get; set; }
        public string DeviceTypeId { get; set; }
        public string DeviceId { get; set; }
        public string Token { get; set; }
        public DateTime Time { get; set; }
        public string Title { get; set; }
        public long? RequestBodyLength { get; set; }
        public long? ResponseBodyLength { get; set; }
        public int RandomNumber { get; set; }

        public string GetAuthorizationToken()
        {
            return UserId <= 0 ? string.Empty : Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}:{2}:{3}:{4}:{5}", UserId, UserName, UserTypeId, DeviceTypeId, DeviceId, Token)));
        }
    }

    public class GlobalSettings
    {
        [Display(Name = "Indent JSON response.", Description = "Indents the response JSON.")]
        public bool Indent { get; set; }
        [Display(Name = "Remove properties having null values.", Description = "Elements for which value is null will not be sent.")]
        public bool RemoveNulls { get; set; }
        [Display(Name = "Remove properties having default values of respective types.", Description = "Default values for respective types will not be sent.")]
        public bool RemoveDefaults { get; set; }
        [Display(Name = "Use ISO 8601 format for dates.", Description = "ISO 8601 format will be used for dates.")]
        public bool UseIsoFormat { get; set; }
        [Display(Name = "Use strict camel case.", Description = "Response properties will be converted to camel case (this is overridden when Format header is explicitly stated). Default setting will be used when unchecked.")]
        public bool UseCamelCase { get; set; }
        [Display(Name = "Time in minutes for temporary token expiration.", Description = "The time after which the generated temporary token will be invalid.")]
        public int TemporaryTokenExpiryTimeInMinutes { get; set; }
    }
}