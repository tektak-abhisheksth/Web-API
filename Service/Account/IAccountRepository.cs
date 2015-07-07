using Model.Account;
using Model.Common;
using Model.Types;
using System.Threading.Tasks;

namespace TekTak.iLoop.Account
{
    public interface IAccountRepository
    {
        Task<StatusData<AccountInternal>> SignUpPerson(SignUpRequestPerson request);
        Task<StatusData<LoginResponse>> Login(LoginRequest request);
        bool IsAuthenticated(int userId, string userName, string token, string deviceId, string deviceType, string transportIp);
        Task<StatusData<AccountInternal>> ForgotPassword(string userName);
        Task<StatusData<string>> UpdatePassword(string oldpassword, string newpassword, SystemSession session);
        Task<StatusData<AccountInternal>> VerifyUser(string userNameOrEmail);
        Task<string> GetPasswordResetCode(string userId, string userName);
        Task<StatusData<AccountInternal>> GetUserInfo(string userNameOrEmailOrId);
        Task<StatusData<bool>> UserExists(string targetUser);
    }
}