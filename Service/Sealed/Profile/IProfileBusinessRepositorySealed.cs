using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Common;
using Model.Profile.Business;
using Model.Types;
using TekTak.iLoop.Profile;

namespace TekTak.iLoop.Sealed.Profile
{
    public interface IProfileBusinessRepositorySealed : IProfileBusinessRepository
    {
        Task<IEnumerable<CompanyEmployeeResponse>> GetCompanyEmployeeList(CompanyEmployeeRequest request, SystemSession session);
        Task<StatusData<string>> UpdateBasicContactCompany(BasicContactBusinessRequest request, SystemSession session);
        Task<StatusData<string>> UpsertCompanyReview(CompanyReviewRequest request, int mode, SystemSession session);
        Task<StatusData<string>> UpsertCompanyDepartmentEmployee(CompanyDepartmentEmployeeRequest request, int mode, SystemSession session);
        Task<StatusData<DepartmentResponse>> UpsertDepartment(DepartmentRequest request, int mode, SystemSession session);
        Task<PaginatedResponse<IEnumerable<CompanyReviewResponse>>> GetCompanyReviews(SingleData<int> request, int pageIndex, int pageSize, SystemSession session);
        Task<IEnumerable<EmployeeRatingViewResponse>> GetEmployeeRating(EmployeeRatingViewRequest request, SystemSession session);
        Task<StatusData<string>> UpsertEmployeeRating(EmployeeRatingRequest request, SystemSession session);
        Task<IEnumerable<CompanyDepartmentViewResponse>> GetCompanyDepartment(string company, SystemSession session);
        Task<PaginatedResponse<IEnumerable<ResignationViewResponse>>> GetResignationRequest(int pageIndex, int pageSize, SystemSession session);
        Task<IEnumerable<CompanyTreeViewResponse>> GetCompanyTree(CompanyTreeViewRequest request, SystemSession session);
        Task<IEnumerable<CompanyEmployeeViewResponse>> GetCompanyEmployee(string employee, SystemSession session);
        Task<IEnumerable<CompanyDepartmentEmployeeViewResponse>> GetCompanyDepartmentEmployee(CompanyDepartmentEmployeeViewRequest request, SystemSession session);
        Task<StatusData<UpsertCompanyEmployeeResponse>> UpsertCompanyEmployee(UpsertCompanyEmployeeRequest request, byte mode, SystemSession session);
    }
}
