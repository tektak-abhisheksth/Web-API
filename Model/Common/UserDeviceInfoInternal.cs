namespace Model.Common
{
    public class UserDeviceInfoInternal
    {
        public string MacAddress { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string Organization { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AreaCode { get; set; }
        public string TimeZone { get; set; }
        public bool HasDaylightSavings { get; set; }
        public string Browser { get; set; }
        public string IpAddress { get; set; }
    }
}
