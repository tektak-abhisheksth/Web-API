using Model.Common;
using Model.Profile.Business;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekTak.iLoop.Helper;
using TekTak.iLoop.Kauwa;

namespace TekTak.iLoop.Profile
{
    public class ProfileBusinessRepository : IProfileBusinessRepository
    {
        protected readonly Services Client;

        public ProfileBusinessRepository(Services client)
        {
            Client = client;
        }

        public async Task<PaginatedResponse<IEnumerable<EmployeeViewResponse>>> GetEmployees(EmployeeViewRequest request, int pageIndex, int pageSize, SystemSession session)
        {
            var serviceRequest = new Employees
            {
                UserId = request.TargetUserId,
                FilterId = request.FilterId,
                Offset = pageIndex,
                PageSize = pageSize,
                SearchTerm = request.SearchTerm
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.getEmployees(serviceRequest, session.GetSession())).ConfigureAwait(false);
            var result = new PaginatedResponse<IEnumerable<EmployeeViewResponse>>
            {
                HasNextPage = response.HasNextPage,
                Page = response.CompanyEmployeeInfos.Select(
                    x => new EmployeeViewResponse
                    {
                        User = new UserResponse
                        {
                            UserId = x.UserId,
                            UserName = x.Username,
                            Name = x.Firstname + " " + x.Lastname,
                            Picture = x.Picture
                        },
                        Position = new GeneralKvPair<int?, string>
                        {
                            Id = x.PositionId,
                            Value = x.Position
                        },
                        EmploymentId = x.EmploymentId,
                        EmployeeTypeId = x.EmployeeTypeId,
                        EmploymentDate = new BeginEndDate
                        {
                            BeginDate = Convert.ToDateTime(x.StartDate),
                            EndDate = x.EndDate != null ? Convert.ToDateTime(x.EndDate) : (DateTime?)null
                        },
                        IsExecutiveBody = x.IsExecutiveBody,
                        Added = x.Added != null ? Convert.ToDateTime(x.Added) : (DateTime?)null,
                        IsApproved = x.ApprovedStatus
                    })
            };
            return result;
        }
    }
}
