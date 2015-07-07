using System.Threading.Tasks;
using Model.Common;
using Model.Profile.Business;
using Model.Types;

namespace BLL.Profile
{
    public interface IProfileBusinessService
    {
        Task<StatusData<UpsertCompanyEmployeeResponse>> UpsertCompanyEmployee(UpsertCompanyEmployeeRequest request, byte mode, SystemSession session);
    }
}
