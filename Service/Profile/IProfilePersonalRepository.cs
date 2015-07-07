using Model.Common;
using Model.Profile.Personal;
using Model.Types;
using System.Threading.Tasks;

namespace TekTak.iLoop.Profile
{
    public interface IProfilePersonalRepository
    {
        Task<StatusData<string>> UpdateEmployeeWorkSchedule(EmployeeWorkScheduleUpdateRequest request, SystemSession session);
        Task<StatusData<long>> AddEmployment(AddEmployeeRequest request, SystemSession session);
        Task<StatusData<string>> UpdateEmployment(UpdateEmployeeRequest request, SystemSession session);
        Task<StatusData<string>> DeleteEmployment(SingleData<int> request, SystemSession session);
    }
}
