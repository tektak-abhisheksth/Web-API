using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Common;
using Model.Profile.Business;
using Model.Types;
using TekTak.iLoop.Helper;
using TekTak.iLoop.Kauwa;
using TekTak.iLoop.Profile;
using DepartmentResponse = Model.Profile.Business.DepartmentResponse;

namespace TekTak.iLoop.Sealed.Profile
{
    public sealed class ProfileBusinessRepositorySealed : ProfileBusinessRepository, IProfileBusinessRepositorySealed
    {
        public ProfileBusinessRepositorySealed(Services client)
            : base(client)
        { }

        public async Task<StatusData<string>> UpdateBasicContactCompany(BasicContactBusinessRequest request, SystemSession session)
        {
            var serviceRequest = new BusinessBasicDetails
            {
                UserId = session.UserId,
                About = request.About,
                CompanyName = request.CompanyName,
                Street = request.Street,
                Town = request.Town,
                CityId = request.CityId,
                Phone = request.Phone,
                Fax = request.Fax,
                Email = request.Email,
                Web = request.Web
            };
            var response = (await Task.Factory.StartNew(() => Client.UserService.setBusinessBaiscDetail(serviceRequest, session.GetSession())).ConfigureAwait(false)).GetStatusData<string>();
            return response;
        }

        public async Task<PaginatedResponse<IEnumerable<CompanyReviewResponse>>> GetCompanyReviews(SingleData<int> request, int pageIndex, int pageSize, SystemSession session)
        {
            var serviceRequest = new CompanyReview
            {
                CompanyId = request.Data,
                OffSet = pageIndex,
                PageSize = pageSize

            };
            var response = await Task.Factory.StartNew(() => Client.UserService.getCompanyReviews(serviceRequest, session.GetSession())).ConfigureAwait(false);
            var result = new PaginatedResponse<IEnumerable<CompanyReviewResponse>>
            {
                HasNextPage = response.HasNextPage,
                Page = response.CompanyReviews.Select(
                 x => new CompanyReviewResponse
                 {
                     CompanyReviewGuid = x.CompanyReviewGUID != null ? Guid.Parse(x.CompanyReviewGUID) : Guid.Empty,
                     User = new UserResponse
                     {
                         UserId = x.UserId,
                         UserName = x.Username,
                         Name = x.Name,
                         Picture = x.PictureUrl
                     },
                     Star = x.Star,
                     Title = x.Title,
                     Comment = x.Comment,
                     DateCommented = x.DateCommented != null ? Convert.ToDateTime(x.DateCommented) : (DateTime?)null,
                     IsApproved = x.IsApproved
                 })

            };
            return result;
        }

        public async Task<StatusData<string>> UpsertCompanyReview(CompanyReviewRequest request, int mode, SystemSession session)
        {
            var serviceRequest = new CompanyReview
            {
                UserId = session.UserId,
                CompanyId = request.CompanyId,
                Mode = mode,
                Star = request.Star,
                Title = request.Title,
                Comment = request.Comment,
                CompanyReviewGUID = request.CompanyReviewGuid

            };
            var response = (await Task.Factory.StartNew(() => Client.UserService.setCompanyReview(serviceRequest, session.GetSession())).ConfigureAwait(false)).GetStatusData<string>();
            return response;
        }

        /// <summary>
        /// Enlists employees of the current company(UDF FNGETCOMPANYEMPLOYEES)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CompanyEmployeeResponse>> GetCompanyEmployeeList(CompanyEmployeeRequest request, SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.UserService.viewCompanyEmployee(request.TargetUser, request.SearchTerm, session.GetSession())).ConfigureAwait(false);

            var result = response.Select(x => new CompanyEmployeeResponse
            {
                CompanyId = x.CompanyId,
                EmployeeId = x.EmployeeId,
                UserName = x.Username,
                StartDate = x.StartDate,
                PositionId = x.PositionId,
                Position = x.PositionName,
                Rating = x.Rating,
                CompanyUserId = x.CompanyUserId,
                Name = x.CompanyName,
                EmployeeTypeId = x.EmployeeTypeId,
                IsExecutiveBody = x.IsExecutiveBody
            });
            return result;
        }

        public async Task<IEnumerable<CompanyDepartmentEmployeeViewResponse>> GetCompanyDepartmentEmployee(CompanyDepartmentEmployeeViewRequest request, SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.UserService.getCompanyDepartmentEmployees(request.TargetUser, request.DepartmentId.ToString(), session.GetSession())).ConfigureAwait(false);

            var result = response.Select(x => new CompanyDepartmentEmployeeViewResponse
            {
                User = new UserResponse
                {
                    UserId = x.UserId,
                    UserName = x.Username,
                    Name = x.Name,
                    Picture = x.Picture
                },
                Department = new GeneralKvPair<int?, string>
                {
                    Id = x.DepartmentId,
                    Value = x.DepartmentName
                },
                Added = x.AddedDate != null ? Convert.ToDateTime(x.AddedDate) : (DateTime?)null
            });
            return result;
        }

        public async Task<StatusData<string>> UpsertCompanyDepartmentEmployee(CompanyDepartmentEmployeeRequest request, int mode, SystemSession session)
        {
            var serviceRequest = new DepartmentEmployees
            {
                UserId = session.UserId,
                Mode = mode,
                DepartmentId = request.DepartmentId,
                EmployeeIds = string.Join(",", request.EmployeeIds)
            };
            var response = (await Task.Factory.StartNew(() => Client.UserService.upsertDepartmentEmployees(serviceRequest, session.GetSession())).ConfigureAwait(false)).GetStatusData<string>();
            return response;
        }

        public async Task<IEnumerable<CompanyDepartmentViewResponse>> GetCompanyDepartment(string company, SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.UserService.getCompanyDepartment(company, session.GetSession())).ConfigureAwait(false);

            var result = response.Select(x => new CompanyDepartmentViewResponse
            {
                Department = new GeneralKvPair<int?, string>
                 {
                     Id = x.DepartmentId,
                     Value = x.Name
                 },
                AssignedByUserId = x.AssignedByUserId,
                Added = x.AddedDate != null ? Convert.ToDateTime(x.AddedDate) : (DateTime?)null,
                EmployeeCount = x.EmployeeCount
            });
            return result;
        }

        public async Task<StatusData<DepartmentResponse>> UpsertDepartment(DepartmentRequest request, int mode, SystemSession session)
        {
            var serviceRequest = new Department
            {
                DepartmentId = request.DepartmentId,
                FromCompanyId = request.FromCompanyId,
                ToCompanyId = request.ToCompanyId,
                DepartmentName = request.DepartmentName,
                EmployeeIds = request.EmployeeIds,
                Mode = mode
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.upsertDepartment(serviceRequest, session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<DepartmentResponse> { Status = (SystemDbStatus)response.DbStatus.DbStatusCode, Message = response.DbStatus.DbStatusMsg, SubStatus = response.DbStatus.DbSubStatusCode };
            if (result.Status.IsOperationSuccessful())
            {
                result.Data = new DepartmentResponse
                {
                    DepartmentId = response.DepartmentId
                };
                // result.Data.DepartmentId = response.DepartmentId;
            }
            return result;
        }

        public async Task<IEnumerable<EmployeeRatingViewResponse>> GetEmployeeRating(EmployeeRatingViewRequest request, SystemSession session)
        {
            var serviceRequest = new EmployeeRatings
            {
                UserId = request.TargetUserId,
                EmployeeIds = request.EmployeeId != null ? string.Join(",", request.EmployeeId) : null,
                Rating = request.Rating
            };

            var response = await Task.Factory.StartNew(() => Client.UserService.getEmployeeRatings(serviceRequest, session.GetSession())).ConfigureAwait(false);

            var result = response.Select(x => new EmployeeRatingViewResponse
            {
                CompanyUserRatingId = x.CompanyUserRatingId,
                User = new UserResponse
                {
                    UserId = x.UserId,
                    UserName = x.Username,
                    Name = x.Name,
                    Picture = x.Picture
                },
                Rating = x.Rating,
                Added = x.AddedDate != null ? Convert.ToDateTime(x.AddedDate) : (DateTime?)null,
                Positon = x.Position
            });
            return result;
        }

        public async Task<StatusData<string>> UpsertEmployeeRating(EmployeeRatingRequest request, SystemSession session)
        {
            var serviceRequest = new Skill
            {
                UserId = session.UserId,
                EmployeeIds = request.EmployeeId != null ? string.Join(",", request.EmployeeId) : null,
                Rating = request.Rating
            };
            var response = (await Task.Factory.StartNew(() => Client.UserService.upsertEmployeeRating(serviceRequest, session.GetSession())).ConfigureAwait(false)).GetStatusData<string>();
            return response;
        }

        public async Task<PaginatedResponse<IEnumerable<ResignationViewResponse>>> GetResignationRequest(int pageIndex, int pageSize, SystemSession session)
        {
            var serviceRequest = new Employees
            {
                UserId = session.UserId,
                Offset = pageIndex,
                PageSize = pageSize
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.getResignationRequest(serviceRequest, session.GetSession())).ConfigureAwait(false);
            var result = new PaginatedResponse<IEnumerable<ResignationViewResponse>>
            {
                HasNextPage = response.HasNextPage,
                Page = response.Employees.Select(
                 x => new ResignationViewResponse
                 {
                     PersonEmploymentId = x.PersonEmploymentId,
                     User = new UserResponse
                     {
                         UserId = x.UserId,
                         UserName = x.Username,
                         Name = x.Name,
                         Picture = x.Picture
                     },
                     Position = x.Position,
                     EndDate = x.EndDate != null ? Convert.ToDateTime(x.EndDate) : (DateTime?)null
                 })
            };
            return result;
        }

        public async Task<IEnumerable<CompanyTreeViewResponse>> GetCompanyTree(CompanyTreeViewRequest request, SystemSession session)
        {
            var serviceRequest = new CompanyTree
            {
                UserId = session.UserId,
                IgnoreSisters = request.IgnoreSisters,
                ShowChildCompanyOnly = request.ShowChildCompaniesOnly
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.getCompanyTree(serviceRequest, session.GetSession())).ConfigureAwait(false);

            var result = response.Select(x => new CompanyTreeViewResponse
            {
                UserId = x.UserId,
                ChildId = x.ChildId,
                Name = x.Name,
                Level = x.Level,
                IsRequestee = x.IsRequestee
            });
            return result;
        }

        public async Task<IEnumerable<CompanyEmployeeViewResponse>> GetCompanyEmployee(string employee, SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.UserService.getCompanyEmployee(session.UserId, employee, session.GetSession())).ConfigureAwait(false);
            var result = response.Select(x => new CompanyEmployeeViewResponse
            {
                User = new UserResponse
                {
                    UserId = x.UserId,
                    UserName = x.Username,
                    Name = x.Name,
                    Picture = x.Picture
                },
                Position = new GeneralKvPair<int?, string>
                {
                    Id = x.PositionId,
                    Value = x.Position
                },
                IsExecutiveBody = x.IsExecutiveBody,
                EmploymentDate = new BeginEndDate
                {
                    BeginDate = Convert.ToDateTime(x.StartDate),
                    EndDate = x.EndDate != null ? Convert.ToDateTime(x.EndDate) : (DateTime?)null
                },
                EmployeeTypeId = x.EmployeeTypeId,
                Added = x.Added != null ? Convert.ToDateTime(x.Added) : (DateTime?)null,
                IsApproved = x.ApprovedStatus

            });
            return result;
        }

        public async Task<StatusData<UpsertCompanyEmployeeResponse>> UpsertCompanyEmployee(UpsertCompanyEmployeeRequest request, byte mode, SystemSession session)
        {
            var serviceRequest = new CompanyEmployeeInfo
            {
                UserId = session.UserId,
                Username = session.UserName,
                Mode = mode,
                EmployeeId = request.EmployeeId,
                EmployeeTypeId = request.EmployeeTypeId,
                PositionId = request.PositionId,
                StartDate = request.StartDate.ToString(),
                EndDate = request.EndDate.ToString(),
                IsExecutiveBody = request.IsExecutiveBody,
                IsPromoted = request.IsPromoted
            };
            var response = await Task.Factory.StartNew(() => Client.UserService.upsertCompanyEmployee(serviceRequest, session.GetSession())).ConfigureAwait(false);

            var result = new StatusData<UpsertCompanyEmployeeResponse> { Status = (SystemDbStatus)response.DbStatus.DbStatusCode, Message = response.DbStatus.DbStatusMsg, SubStatus = response.DbStatus.DbSubStatusCode };
            if (result.Status.IsOperationSuccessful())
            {
                result.Data = new UpsertCompanyEmployeeResponse
                {
                    CompanyUserId = response.CompanyUserId
                };
            }
            return result;
        }
    }
}
