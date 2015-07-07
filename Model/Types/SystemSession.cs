namespace Model.Types
{
    public class SystemSession
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public byte UserTypeId { get; set; }
        public string DeviceTypeId { get; set; }
        public string DeviceId { get; set; }
        public string LoginToken { get; set; }
        public string Ip { get; set; }
        public int IUserId { get; set; }
    }
}