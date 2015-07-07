namespace BLL.ClientAPI
{

    //public static class Smsapi
    //{
    //    public static bool SendSms(string destMobile, string message)
    //    {
    //        var link = @"http://api.cs-networks.net:9011/bin/send";
    //        var userName = "harris85443";
    //        var password = "hf77w8nef";
    //        var srcAddress = "iLoop";

    //        var temp = new StringBuilder();
    //        temp.AppendFormat("USERNAME={0}&PASSWORD={1}&SOURCEADDR={2}&DESTADDR={3}&MESSAGE={4}", userName, password, srcAddress, destMobile, message.Replace(" ", "+"));
    //        var param = temp.ToString();

    //        var client = new HttpClient();
    //        var response = client.GetAsync(link + "?" + param).Result;
    //        response.Content.ReadAsStringAsync();
    //        try
    //        {
    //            return response.StatusCode == HttpStatusCode.OK;

    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }
    //    }
    //}
}
