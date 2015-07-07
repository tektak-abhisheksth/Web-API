using Model.Account;
using Model.Common;
using Model.Profile.Personal;
using System.Threading.Tasks;

namespace BLL.Account
{
    public partial class AccountService
    {
        public Task<StatusData<AccountInternal>> SignUpBusiness(SignUpRequestBusiness request)
        {
            return _jUnitOfWork.Account.SignUpBusiness(request);
        }

        public Task<StatusData<string>> Activate(ActivateUser request)
        {
            return _jUnitOfWork.Account.Activate(request);
        }

        public Task<StatusData<BaseInfoResponse>> ResetPassword(ResetPasswordRequest request)
        {
            return _jUnitOfWork.Account.ResetPassword(request);
        }
    }
}
