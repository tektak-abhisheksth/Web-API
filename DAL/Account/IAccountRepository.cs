using System.Threading.Tasks;
using DAL.DbEntity;
using Entity;
using Model.Account;
using Model.Common;

namespace DAL.Account
{
    public interface IAccountRepository : IGenericRepository<UserLogin>
    {
        string RequestDeviceTaggedToken(string uniqueDeviceId);
        Task<StatusData<AccountInternal>> SignUpPerson(SignUpRequestPerson request);
        Task SendEmail(string emailRecipient, string cc, string bcc, string emailSubject, string emailBody, string emailBodyFormat, string emailImportance);
        bool IsAuthenticated(int userId, string userName, string token, string deviceId);
        Task<StatusData<LoginResponse>> Login(LoginRequest request);
        Task<StatusData<AccountInternal>> ForgotPassword(string userName);
    }
}
