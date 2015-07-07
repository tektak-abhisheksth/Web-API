
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Common;
using Model.Profile.Business;
using Model.Types;

namespace TekTak.iLoop.Profile
{
    public interface IProfileBusinessRepository
    {
        Task<PaginatedResponse<IEnumerable<EmployeeViewResponse>>> GetEmployees(EmployeeViewRequest request, int pageIndex, int pageSize, SystemSession session);
    }
}
