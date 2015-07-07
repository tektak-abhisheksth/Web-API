using Model.Account;
using Model.Common;
using System.Threading.Tasks;
using Model.Profile.Personal;
using TekTak.iLoop.Account;

namespace TekTak.iLoop.Sealed.Account
{
    public interface IAccountRepositorySealed : IAccountRepository
    {
        Task<StatusData<AccountInternal>> SignUpBusiness(SignUpRequestBusiness request);
        Task<StatusData<string>> Activate(ActivateUser request);
        Task<StatusData<BaseInfoResponse>> ResetPassword(ResetPasswordRequest request);
    }
}