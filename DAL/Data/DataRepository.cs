using DAL.DbEntity;
using Entity;
using Model.Common;
using Model.Company.EmployeeDepartment;
using Model.Company.UserDepartment;
using Model.CountryInfo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class DataRequestsRepository : GenericRepository<UserLogin>, IDataRequestsRepository
    {
        public DataRequestsRepository(iLoopEntity context) : base(context) { }

        public async Task<IQueryable<GeneralKvPair<byte, string>>> GetChatNetworkList(int? id, string searchString = null)
        {
            return
               await Task.Factory.StartNew(() => Context.ChatNetworks.Where(
                   x =>
                       (!id.HasValue && searchString == null) || (id.HasValue && x.ChatNetworkID == id.Value)
                       || (!id.HasValue && x.Name.StartsWith(searchString))).Select(
                           x => new GeneralKvPair<byte, string> { Id = x.ChatNetworkID, Value = x.Name }).OrderBy(
                               x => x.Id)).ConfigureAwait(false);
        }

        public async Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetNationalityList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            var page = new PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>
             {
                 Page = await Task.Factory.StartNew(() => Context.Nationalities.Where(x => (!id.HasValue && searchString == null) || (id.HasValue && x.NationalityID == id.Value)
                                                                                           || (!id.HasValue && x.Name.StartsWith(searchString))).Select(x => new GeneralKvPair<int, string> { Id = x.NationalityID, Value = x.Name }).OrderBy(x => x.Value).Skip(pageIndex * pageSize).Take(pageSize + 1).ToList()).ConfigureAwait(false)
             };
            page.HasNextPage = page.Page.Count() > pageSize;
            page.Page = page.Page.Take(pageSize);
            return page;
        }

        public async Task<UserCity> GetCityInformation(int? id = null, string searchString = null)
        {
            return
               await Task.Factory.StartNew(() => Context.Cities.Where(
                   x =>
                       (!id.HasValue && searchString == null) || (id.HasValue && x.CityID == id.Value)
                       || (!id.HasValue && x.Name.StartsWith(searchString))).Select(
                           x =>
                               new UserCity
                               {
                                   Id = x.CityID,
                                   Name = x.Name,
                                   CountryCode = x.CountryCode
                                   //Latitude = (double)x.Latitude,
                                   //Longitude = (double)x.Longitude
                               }).FirstOrDefault()).ConfigureAwait(false);
        }

        public async Task<PaginatedResponse<IEnumerable<GeneralKvPair<byte, string>>>> GetIndustryList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            var page = new PaginatedResponse<IEnumerable<GeneralKvPair<byte, string>>>
             {
                 Page = await Task.Factory.StartNew(() => Context.Industries.Where(
                     x => (!id.HasValue && searchString == null) || (id.HasValue && x.IndustryID == id.Value) || (!id.HasValue && x.Name.StartsWith(searchString))).Select(x => new GeneralKvPair<byte, string> { Id = x.IndustryID, Value = x.Name }).OrderBy(x => x.Value).Skip(pageIndex * pageSize).Take(pageSize + 1).ToList()).ConfigureAwait(false)
             };
            page.HasNextPage = page.Page.Count() > pageSize;
            page.Page = page.Page.Take(pageSize);
            return page;
        }

        public async Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetLanguageList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            var page = new PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>
             {
                 Page = await Task.Factory.StartNew(() => Context.Languages.Where(x => (!id.HasValue && searchString == null) || (id.HasValue && x.LanguageID == id.Value)
                                                                                       || (!id.HasValue && x.Name.StartsWith(searchString))).Select(x => new GeneralKvPair<int, string> { Id = x.LanguageID, Value = x.Name }).OrderBy(x => x.Value).Skip(pageIndex * pageSize).Take(pageSize + 1).ToList()).ConfigureAwait(false)
             };
            page.HasNextPage = page.Page.Count() > pageSize;
            page.Page = page.Page.Take(pageSize);
            return page;
        }

        public async Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetReligionList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            var page = new PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>
            {
                Page = await Task.Factory.StartNew(() => Context.Religions.Where(
                    x => (!id.HasValue && searchString == null) || (id.HasValue && x.ReligionID == id.Value) || (!id.HasValue && x.Name.StartsWith(searchString))).Select(
                        x => new GeneralKvPair<int, string> { Id = x.ReligionID, Value = x.Name }).OrderBy(x => x.Value).Skip(pageIndex * pageSize).Take(pageSize + 1).ToList()).ConfigureAwait(false)
            };
            page.HasNextPage = page.Page.Count() > pageSize;
            page.Page = page.Page.Take(pageSize);
            return page;
        }

        public async Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetSkillTypeList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            var page = new PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>
            {
                Page = await Task.Factory.StartNew(() => Context.SkillTypes.Where(x =>
                    (!id.HasValue && searchString == null) || (id.HasValue && x.SkillTypeID == id.Value) || (!id.HasValue && x.Name.StartsWith(searchString))).Select(
                        x => new GeneralKvPair<int, string> { Id = x.SkillTypeID, Value = x.Name }).OrderBy(x => x.Value).Skip(pageIndex * pageSize).Take(pageSize + 1).ToList()).ConfigureAwait(false)
            };
            page.HasNextPage = page.Page.Count() > pageSize;
            page.Page = page.Page.Take(pageSize);
            return page;
        }

        public async Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetTownList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            var page = new PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>
             {
                 Page = await Task.Factory.StartNew(() => Context.Towns.Where(x => (!id.HasValue && searchString == null) || (id.HasValue && x.TownID == id.Value) || (!id.HasValue && x.Name.StartsWith(searchString))).Select(x => new GeneralKvPair<int, string> { Id = x.TownID, Value = x.Name }).OrderBy(x => x.Id).Skip(pageIndex * pageSize).Take(pageSize + 1).ToList()).ConfigureAwait(false)
             };
            page.HasNextPage = page.Page.Count() > pageSize;
            page.Page = page.Page.Take(pageSize);
            return page;
        }

        public async Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetAcademicConcentrationList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            //return Context.AcademicConcentrations.Where(x => (!id.HasValue && searchString == null) || (id.HasValue && x.AcademicConcentrationID == id.Value)
            //        || (!id.HasValue && x.Name.StartsWith(searchString))).Select(x => new GeneralKvPair<int, string> { Id = x.AcademicConcentrationID, Value = x.Name }).OrderBy(x => x.Id);

            var page = new PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>
            {
                Page = await Task.Factory.StartNew(() => Context.AcademicConcentrations.Where(x => (!id.HasValue && searchString == null) || (id.HasValue && x.AcademicConcentrationID == id.Value)
                                                                                                   || (!id.HasValue && x.Name.StartsWith(searchString))).Select(x => new GeneralKvPair<int, string> { Id = x.AcademicConcentrationID, Value = x.Name }).OrderBy(x => x.Value).Skip(pageIndex * pageSize).Take(pageSize + 1).ToList()).ConfigureAwait(false)
            };
            page.HasNextPage = page.Page.Count() > pageSize;
            page.Page = page.Page.Take(pageSize);
            return page;
        }

        public async Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetAcademicInstituteList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            var page = new PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>
            {
                Page = await Task.Factory.StartNew(() => Context.AcademicInstitutes.Where(
                    x =>
                        (!id.HasValue && searchString == null) || (id.HasValue && x.AcademicInstituteID == id.Value)
                        || (!id.HasValue && x.Name.StartsWith(searchString)))
                    .Select(x => new GeneralKvPair<int, string> { Id = x.AcademicInstituteID, Value = x.Name })
                    .OrderBy(x => x.Value).Skip(pageIndex * pageSize).Take(pageSize + 1).ToList()).ConfigureAwait(false)
            };
            page.HasNextPage = page.Page.Count() > pageSize;
            page.Page = page.Page.Take(pageSize);
            return page;
        }

        public async Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetDepartmentList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            var page = new PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>
            {
                Page = await Task.Factory.StartNew(() => Context.Departments.Where(x => (!id.HasValue && searchString == null) || (id.HasValue && x.DepartmentID == id.Value)
                                                                                        || (!id.HasValue && x.Name.StartsWith(searchString))).Select(x => new GeneralKvPair<int, string> { Id = x.DepartmentID, Value = x.Name }).OrderBy(x => x.Value).Skip(pageIndex * pageSize).Take(pageSize + 1).ToList()).ConfigureAwait(false)
            };
            page.HasNextPage = page.Page.Count() > pageSize;
            page.Page = page.Page.Take(pageSize);
            return page;
        }

        public async Task<PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>> GetPositionList(int pageIndex, int pageSize, int? id = null, string searchString = null)
        {
            var page = new PaginatedResponse<IEnumerable<GeneralKvPair<int, string>>>
            {
                Page = await Task.Factory.StartNew(() => Context.Positions.Where(x => (!id.HasValue && searchString == null) || (id.HasValue && x.PositionID == id.Value)
                                                                                      || (!id.HasValue && x.Name.StartsWith(searchString))).Select(x => new GeneralKvPair<int, string> { Id = x.PositionID, Value = x.Name }).OrderBy(x => x.Value).Skip(pageIndex * pageSize).Take(pageSize + 1).ToList()).ConfigureAwait(false)
            };
            page.HasNextPage = page.Page.Count() > pageSize;
            page.Page = page.Page.Take(pageSize);
            return page;
        }

        public async Task<IQueryable<UserDepartmentResponse>> GetCompanyDepartmentList(int userId)
        {
            var result =
             await Task.Factory.StartNew(() => Context.CompanyDepartments.Where(x => x.UserID == userId).Select(x => new UserDepartmentResponse
             {
                 Department = new GeneralKvPair<int, string> { Id = x.DepartmentID, Value = x.Department.Name },
                 AssignedByUser =
                     x.AssignedByUserID.HasValue
                         ? new GeneralKvPair<int, string>
                         {
                             Id = x.AssignedByUserID.Value,
                             Value = x.UserInfoCompany1.Name
                         }
                         : null,
                 Added = x.Added
             })).ConfigureAwait(false);
            return result.OrderBy(x => x.Department.Value);
        }

        public async Task<PaginatedResponse<IEnumerable<EmployeeDepartmentResponse>>> GetCompanyDepartmentEmployeeList(int userId, int? employeeId, int? departmentId, int pageIndex, int pageSize)
        {
            var page = new PaginatedResponse<IEnumerable<EmployeeDepartmentResponse>>
            {
                Page = await Task.Factory.StartNew(() => Context.CompanyDepartmentEmployees.Where(x => (x.UserID == userId && ((!employeeId.HasValue || x.EmployeeID == employeeId)
                                                                                                                               && (!departmentId.HasValue || x.DepartmentID == departmentId))))
                    .Select(x => new EmployeeDepartmentResponse
                    {
                        DepartmentId = x.DepartmentID,
                        Employee =
                            new GeneralKvPair<int, string>
                            {
                                Id = x.EmployeeID,
                                Value = x.UserInfoPerson.FirstName + " " + x.UserInfoPerson.LastName
                            },
                        Added = x.Added
                    }).OrderBy(x => x.DepartmentId)
                    .ThenBy(x => x.Employee)
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize + 1)
                    .ToList()).ConfigureAwait(false)
            };
            page.HasNextPage = page.Page.Count() > pageSize;
            page.Page = page.Page.Take(pageSize);
            return page;
        }

        //public async Task<PaginatedResponse<IEnumerable<ProductAndServiceResponse>>> GetCompanyProductAndServiceList(int userId, int pageIndex, int pageSize)
        //{
        //    var page = new PaginatedResponse<IEnumerable<ProductAndServiceResponse>>
        //    {
        //        PageIndex = pageIndex,
        //        PageSize = pageSize,
        //        Page = await Task.Factory.StartNew(() => Context.CompanyProductAndServices.Where(
        //            x => x.UserID == userId)
        //            .OrderByDescending(x => x.LastChanged)
        //            .Skip(pageIndex * pageSize)
        //            .Take(pageSize + 1)
        //            .ToList()
        //            .Select(x => new ProductAndServiceResponse
        //            {
        //                CompanyProductAndServiceId = x.CompanyProductAndServiceID,
        //                Title = x.Title,
        //                Description = x.Description,
        //                Image = ImageRepository.GetProductPhoto(x.Image),
        //                LastChanged = x.LastChanged
        //            }))
        //    };
        //    page.HasNextPage = page.Page.Count() > pageSize;
        //    page.Page = page.Page.Take(pageSize);
        //    return page;
        //}

        public async Task<IEnumerable<UserCountry>> GetCountryList()
        {
            return await Task.Factory.StartNew(() => Context.Countries.Where(x => x.ZipCode != null).OrderBy(x => x.Name).Select(
                x => new UserCountry { CountryCode = x.CountryCode, Name = x.Name, ZipCode = x.ZipCode })).ConfigureAwait(false);
        }
    }
}
