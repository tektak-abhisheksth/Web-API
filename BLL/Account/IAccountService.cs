using Model.Account;
using Model.Common;
using System.Threading.Tasks;
using Model.Profile.Personal;

namespace BLL.Account
{
    public partial interface IAccountService
    {
        string RequestDeviceTaggedToken(string uniqueDeviceId);

        bool ValidateSignUpToken(string token, string uniqueDeviceId);

        bool SendMessage(long mobileNumber, string countryCode, string deviceId, string message, out int code);

        Task<StatusData<AccountInternal>> SignUpPerson(SignUpRequestPerson request);

        bool IsAuthenticated(int userId, string userName, string token, string deviceId, string deviceType, string transportIp);

        Task<StatusData<LoginResponse>> Login(LoginRequest request);

        Task<StatusData<AccountInternal>> ForgotPassword(string userName);
        Task<string> GetPasswordResetCode(string userId, string userName);
        Task<StatusData<bool>> UserExists(string targetUser);
        Task<StatusData<BaseInfoResponse>> ResetPassword(ResetPasswordRequest request);
    }
}
