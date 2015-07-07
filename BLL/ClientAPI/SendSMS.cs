using Utility;

namespace BLL.ClientAPI
{
    public static class SendSms
    {
        public static bool SendMessage(long mobileNumber, string countryCode, string deviceId, string message, out int code)
        {
            //Client API call here
            code = Helper.GenerateRandomCode(1000, 9999);
            return true;
        }
    }
}
