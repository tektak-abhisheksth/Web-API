using Model.Common;
using Model.Profile.Personal;
using Model.Types;
using System.Threading.Tasks;
using TekTak.iLoop.Helper;
using TekTak.iLoop.Kauwa;
using EmployeeWorkSchedule = TekTak.iLoop.Kauwa.EmployeeWorkSchedule;

namespace TekTak.iLoop.Profile
{
    public class ProfilePersonalRepository : IProfilePersonalRepository
    {
        protected readonly Services Client;

        public ProfilePersonalRepository(Services client)
        {
            Client = client;
        }

        public async Task<StatusData<string>> UpdateEmployeeWorkSchedule(EmployeeWorkScheduleUpdateRequest request, SystemSession session)
        {
            var serviceRequest = new EmployeeWorkSchedule
            {
                PersonEmpId = request.PersonEmploymentId,
                ScheduleType = request.ScheduleType == SystemWorkSchedule.Fixed,
                Text = string.Join("|", request.Schedules)
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.setEmployeWorkSchedule(serviceRequest, session.GetSession())).ConfigureAwait(false);
            var data = new StatusData<string> { Status = (SystemDbStatus)response.DbStatusCode, Message = response.DbStatusMsg, SubStatus = response.DbSubStatusCode };
            return data;
        }

        public async Task<StatusData<long>> AddEmployment(AddEmployeeRequest request, SystemSession session)
        {
            var serviceRequest = new Employment
            {
                Mode = (byte)SystemDbStatus.Updated,
                City = new City { CityId = request.CityId },
                PersonEmploymentId = 0,
                //RequestId = request.RequestId != 0 ? request.RequestId : 0,
                UserId = session.UserId,
                EmployeeInfo = new CompanyEmployeeInfo { CompanyId = request.CompanyId ?? 0, CompanyName = request.CompanyName, Position = request.Position, StartDate = request.StartDate.ToString(), EndDate = request.EndDate != null ? request.EndDate.ToString() : null, EmployeeTypeId = (int)request.EmployeeTypeId }
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.upsertEmployeementHistory(serviceRequest, session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<long> { Status = (SystemDbStatus)response.DbStatus.DbStatusCode, Message = response.DbStatus.DbStatusMsg, SubStatus = response.DbStatus.DbSubStatusCode };
            if (!request.EndDate.HasValue && result.Status.IsOperationSuccessful())
            {
                result.Data = response.PersonEmploymentId;
                var empSch = new EmployeeWorkScheduleUpdateRequest { PersonEmploymentId = response.PersonEmploymentId, ScheduleType = request.WorkSchedule.ScheduleType, Schedules = request.WorkSchedule.Schedules };
                await UpdateEmployeeWorkSchedule(empSch, session).ConfigureAwait(false);
            }

            return result;
        }

        public async Task<StatusData<string>> UpdateEmployment(UpdateEmployeeRequest request, SystemSession session)
        {
            var serviceRequest = new Employment
            {
                Mode = (byte)SystemDbStatus.Updated,
                City = new City { CityId = request.CityId },
                PersonEmploymentId = request.PersonEmploymentId,
                // RequestId = request.RequestId != 0 ? request.RequestId : 0,
                UserId = session.UserId,
                EmployeeInfo = new CompanyEmployeeInfo { CompanyId = request.CompanyId ?? 0, CompanyName = request.CompanyName, Position = request.Position, StartDate = request.StartDate.ToString(), EndDate = request.EndDate != null ? request.EndDate.ToString() : null, EmployeeTypeId = (int)request.EmployeeTypeId }
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.upsertEmployeementHistory(serviceRequest, session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<string> { Status = (SystemDbStatus)response.DbStatus.DbStatusCode, Message = response.DbStatus.DbStatusMsg, SubStatus = response.DbStatus.DbSubStatusCode };

            if (!request.EndDate.HasValue && result.Status.IsOperationSuccessful())
            {
                var empSch = new EmployeeWorkScheduleUpdateRequest { PersonEmploymentId = response.PersonEmploymentId, ScheduleType = request.WorkSchedule.ScheduleType, Schedules = request.WorkSchedule.Schedules };
                await UpdateEmployeeWorkSchedule(empSch, session).ConfigureAwait(false);
            }

            return result;
        }

        public async Task<StatusData<string>> DeleteEmployment(SingleData<int> request, SystemSession session)
        {
            var serviceRequest = new Employment
            {
                UserId = session.UserId,
                Mode = (byte)SystemDbStatus.Deleted,
                PersonEmploymentId = request.Data
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.upsertEmployeementHistory(serviceRequest, session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<string> { Status = (SystemDbStatus)response.DbStatus.DbStatusCode, Message = response.DbStatus.DbStatusMsg, SubStatus = response.DbStatus.DbSubStatusCode };

            return result;
        }

    }
}
