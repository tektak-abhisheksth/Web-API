using BLL.ClientAPI;
using DAL.DbEntity;
using Model.Account;
using Model.Common;
using System.Threading.Tasks;
using Utility;

namespace BLL.Account
{
    public partial class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TekTak.iLoop.UOW.IUnitOfWork _jUnitOfWork;

        public AccountService(IUnitOfWork unitOfWork, TekTak.iLoop.UOW.IUnitOfWork jUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _jUnitOfWork = jUnitOfWork;
        }

        public string RequestDeviceTaggedToken(string uniqueDeviceId)
        {
            return _unitOfWork.Account.RequestDeviceTaggedToken(uniqueDeviceId);
        }

        public bool ValidateSignUpToken(string token, string uniqueDeviceId)
        {
            return Encryptor.VerifyToken(token, uniqueDeviceId) == 1;
        }

        public bool SendMessage(long mobileNumber, string countryCode, string deviceId, string message, out int code)
        {
            return SendSms.SendMessage(mobileNumber, countryCode, deviceId, message, out code);
        }

        public Task<StatusData<AccountInternal>> SignUpPerson(SignUpRequestPerson request)
        {
            return _jUnitOfWork.Account.SignUpPerson(request);
        }

        public bool IsAuthenticated(int userId, string userName, string token, string deviceId, string deviceType, string transportIp)
        {
            return _jUnitOfWork.Account.IsAuthenticated(userId, userName, token, deviceId, deviceType, transportIp);
        }

        public Task<StatusData<LoginResponse>> Login(LoginRequest request)
        {
            return _jUnitOfWork.Account.Login(request);
        }

        public Task<StatusData<AccountInternal>> ForgotPassword(string userName)
        {

            //var response = await _unitOfWork.Account.ForgotPassword(userName);
            //await _unitOfWork.CommitAsync();
            //return response;

            return _jUnitOfWork.Account.ForgotPassword(userName);
        }

        public Task<string> GetPasswordResetCode(string userId, string userName)
        {
            return _jUnitOfWork.Account.GetPasswordResetCode(userId, userName);
        }

        public Task<StatusData<bool>> UserExists(string targetUser)
        {
            return _jUnitOfWork.Account.UserExists(targetUser);
        }

    }
}
