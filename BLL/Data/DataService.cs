using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DbEntity;
using Model.Common;
using Model.Company.EmployeeDepartment;
using Model.Company.UserDepartment;
using Model.CountryInfo;

namespace BLL.Data
{
    public class DataRequestService : IDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TekTak.iLoop.UOW.IUnitOfWork _jUnitOfWork;
        public DataRequestService(IUnitOfWork unitOfWork, TekTak.iLoop.UOW.IUnitOfWork jUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _jUnitOfWork = jUnitOfWork;
        }

        public Task<IEnumerable<UserCity>> GetCities(int? id, string searchString)
        {
            return _jUnitOfWork.Data.GetCities(id, searchString);
        }

        public Task<IEnumerable<GeneralKvPair<long, string>>> GetData(string tableName, int? id, string searchString)
        {
            return _jUnitOfWork.Data.GetData(tableName, id, searchString);

        }

        public Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetNationalityList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            return _unitOfWork.DataRequests.GetNationalityList(pageIndex, pageSize, id, searchString);
        }

        public Task<PaginatedResponse<IEnumerable<GeneralKvPair<byte, string>>>> GetIndustryList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            return _unitOfWork.DataRequests.GetIndustryList(pageIndex, pageSize, id, searchString);
        }

        public Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetLanguageList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            return _unitOfWork.DataRequests.GetLanguageList(pageIndex, pageSize, id, searchString);
        }

        public Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetReligionList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            return _unitOfWork.DataRequests.GetReligionList(pageIndex, pageSize, id, searchString);
        }

        public Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetSkillTypeList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            return _unitOfWork.DataRequests.GetSkillTypeList(pageIndex, pageSize, id, searchString);
        }

        public Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetTownList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            return _unitOfWork.DataRequests.GetTownList(pageIndex, pageSize, id, searchString);
        }

        //public Task<IEnumerable<GeneralKvPair<int, string>>> GetAcademicConcentrationList(int pageIndex, int pageSize, int? id, string searchString, SystemSession session)
        //{
        //    // return _unitOfWork.DataRequests.GetAcademicConcentrationList(pageIndex, pageSize, id, searchString);
        //    return _jUnitOfWork.Data.GetData("ACADEMICCONCENTRATION", id, searchString, session);

        //}

        public Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetAcademicInstituteList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            return _unitOfWork.DataRequests.GetAcademicInstituteList(pageIndex, pageSize, id, searchString);
        }

        public Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetDepartmentList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            return _unitOfWork.DataRequests.GetDepartmentList(pageIndex, pageSize, id, searchString);
        }

        public Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetPositionList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            return _unitOfWork.DataRequests.GetPositionList(pageIndex, pageSize, id, searchString);
        }

        public Task<IQueryable<UserDepartmentResponse>> GetCompanyDepartmentList(int userId)
        {
            return _unitOfWork.DataRequests.GetCompanyDepartmentList(userId);
        }

        public Task<PaginatedResponse<IEnumerable<EmployeeDepartmentResponse>>> GetCompanyDepartmentEmployeeList(int userId, int? employeeId, int? departmentId, int pageIndex, int pageSize)
        {
            return _unitOfWork.DataRequests.GetCompanyDepartmentEmployeeList(userId, employeeId, departmentId, pageIndex, pageSize);
        }

        public Task<IEnumerable<UserCountry>> GetCountryList()
        {
            return _unitOfWork.DataRequests.GetCountryList();
        }
    }
}
