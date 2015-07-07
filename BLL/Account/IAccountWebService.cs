using Model.Account;
using Model.Common;
using System.Threading.Tasks;

namespace BLL.Account
{
    public partial interface IAccountService
    {
        Task<StatusData<AccountInternal>> SignUpBusiness(SignUpRequestBusiness request);
        Task<StatusData<string>> Activate(ActivateUser request);
    }
}
