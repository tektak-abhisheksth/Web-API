using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Common;
using Model.Company.EmployeeDepartment;
using Model.Company.UserDepartment;
using Model.CountryInfo;

namespace BLL.Data
{
    public interface IDataService
    {
        Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetNationalityList(int pageIndex, int pageSize, int? id = null, string searchString = null);
        Task<PaginatedResponse<IEnumerable<GeneralKvPair<byte, string>>>> GetIndustryList(int pageIndex, int pageSize, int? id = null, string searchString = null);
        Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetLanguageList(int pageIndex, int pageSize, int? id = null, string searchString = null);
        Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetReligionList(int pageIndex, int pageSize, int? id = null, string searchString = null);
        Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetSkillTypeList(int pageIndex, int pageSize, int? id = null, string searchString = null);
        Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetTownList(int pageIndex, int pageSize, int? id = null, string searchString = null);
        Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetAcademicInstituteList(int pageIndex, int pageSize, int? id = null, string searchString = null);
        Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetDepartmentList(int pageIndex, int pageSize, int? id = null, string searchString = null);
        Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetPositionList(int pageIndex, int pageSize, int? id = null, string searchString = null);
        Task<IQueryable<UserDepartmentResponse>> GetCompanyDepartmentList(int userId);
        Task<PaginatedResponse<IEnumerable<EmployeeDepartmentResponse>>> GetCompanyDepartmentEmployeeList(int userId, int? employeeId, int? departmentId, int pageIndex, int pageSize);
        //Task<PaginatedResponse<IEnumerable<ProductAndServiceResponse>>> GetCompanyProductAndServiceList(int userId, int pageIndex, int pageSize);
        Task<IEnumerable<UserCountry>> GetCountryList();
        Task<IEnumerable<GeneralKvPair<long, string>>> GetData(string tableName, int? id, string searchString);
        Task<IEnumerable<UserCity>> GetCities(int? id, string searchString);
    }
}
