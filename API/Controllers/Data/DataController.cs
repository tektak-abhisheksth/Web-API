using API.Filters;
using BLL.Data;
using Model.Attribute;
using Model.Common;
using Model.CountryInfo;
using Model.Search;
using Model.Types;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Utility;

namespace API.Controllers.Data
{
    /// <summary>
    /// Provides APIs for accessing general data.
    /// </summary>
    [MetaData]
    public partial class DataController : ApiController
    {
        private readonly IDataService _service;

        /// <summary>
        /// Provides APIs for accessing general data.
        /// </summary>
        public DataController(IDataService service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns the list of all available academic concentrations.
        /// </summary>
        /// <param name="request">The search parameters.</param>
        /// <returns>System provided ID and name of the academic concentrations.</returns>
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData("2015-05-25", markType: 3, aliasName: "Data_AcademicConcentrations")]
        [ActionName("AcademicConcentrations")]
        // [CacheOutputUntilToday]
        [ResponseType(typeof(IEnumerable<GeneralKvPair<long, string>>))]
        public async Task<HttpResponseMessage> AcademicConcentrations(DataRequest request)
        {
            return await CallGetData(request, SystemDataTable.AcademicConcentration).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the list of all available academic institutes.
        /// </summary>
        /// <param name="request">The search parameters.</param>
        /// <returns>System provided ID and name of the academic institutes.</returns>
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData("2015-05-25", markType: 3, aliasName: "Data_AcademicInstitutes")]
        [ActionName("AcademicInstitutes")]
        // [CacheOutputUntilToday]
        [ResponseType(typeof(IEnumerable<GeneralKvPair<long, string>>))]
        public async Task<HttpResponseMessage> AcademicInstitutes(DataRequest request)
        {
            return await CallGetData(request, SystemDataTable.AcademicInstitute).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the list of available chat networks.
        /// </summary>
        /// <param name="request">The search parameters.</param>
        /// <returns>System provided ID and name of chat networks.</returns>
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData("2015-05-25", markType: 3, aliasName: "Data_ChatNetworks")]
        [ActionName("ChatNetworks")]
        //[CacheOutputUntilToday]
        [ResponseType(typeof(IQueryable<GeneralKvPair<long, string>>))]
        public async Task<HttpResponseMessage> ChatNetworks(DataRequest request)
        {
            return await CallGetData(request, SystemDataTable.ChatNetwork).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the available city name, country code, longitude and latitude from the given city ID.
        /// </summary>
        /// <param name="request">The search parameters.</param>
        /// <returns>City name, country code, longitude and latitude.</returns>
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData("2015-05-25", markType: 3, aliasName: "Data_Cities")]
        [ActionName("Cities")]
        //[CacheOutputUntilToday]
        [ResponseType(typeof(IEnumerable<UserCity>))]
        public async Task<HttpResponseMessage> Cities(DataRequest request)
        {
            var response = new StatusData<IEnumerable<UserCity>> { Status = SystemDbStatus.Selected };

            if (request.Id == null && request.SearchString == null)
                return Request.SystemResponse<string>(SystemDbStatus.NotSupported, message: "Please provide city Id or search term.");
            response.Data = await _service.GetCities(request.Id, request.SearchString).ConfigureAwait(false);

            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Returns all available country names with their zip codes and country codes.
        /// </summary>
        /// <param name="deviceId">The unique device ID of the user.</param>
        /// <returns>List of country names, zip codes and country codes.</returns>
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData("2015-05-25", markType: 3, aliasName: "Data_Countries")]
        [ActionName("Countries")]
        [ResponseType(typeof(IEnumerable<UserCountry>))]
        public async Task<HttpResponseMessage> CountryList(string deviceId)
        {
            var response = new StatusData<IEnumerable<UserCountry>> { Status = SystemDbStatus.Unauthorized };
            if (Request.Headers.Authorization != null && Encryptor.VerifyToken(Request.Headers.Authorization.ToString(), deviceId) == 1)
            {
                response.Status = SystemDbStatus.Selected;
                response.Data = await _service.GetCountryList().ConfigureAwait(false);
            }
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Returns the list of all available departments.
        /// </summary>
        /// <param name="request">The search parameters.</param>
        /// <returns>System provided ID and name of the departments.</returns>
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData("2015-05-25", markType: 3, aliasName: "Data_Departments")]
        [ActionName("Departments")]
        // [CacheOutputUntilToday]
        [ResponseType(typeof(IEnumerable<GeneralKvPair<long, string>>))]
        public async Task<HttpResponseMessage> Departments(DataRequest request)
        {
            return await CallGetData(request, SystemDataTable.Department).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the list of all available industries.
        /// </summary>
        /// <param name="request">The search parameters.</param>
        /// <returns>System provided ID and name of the industries.</returns>
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData("2015-05-25", markType: 3, aliasName: "Data_Industries")]
        [ActionName("Industries")]
        //[CacheOutputUntilToday]
        [ResponseType(typeof(IEnumerable<GeneralKvPair<long, string>>))]
        public async Task<HttpResponseMessage> Industries(DataRequest request)
        {
            return await CallGetData(request, SystemDataTable.Industry).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the list of all available languages.
        /// </summary>
        /// <param name="request">The search parameters.</param>
        /// <returns>System provided ID and name of the languages.</returns>
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData("2015-05-25", markType: 3, aliasName: "Data_Languages")]
        [ActionName("Languages")]
        //[CacheOutputUntilToday]
        [ResponseType(typeof(IEnumerable<GeneralKvPair<long, string>>))]
        public async Task<HttpResponseMessage> Languages(DataRequest request)
        {
            return await CallGetData(request, SystemDataTable.Language).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the list of all available nationalities.
        /// </summary>
        /// <param name="request">The search parameters.</param>
        /// <returns>System provided ID and name of the nationality.</returns>
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData("2015-05-25", markType: 3, aliasName: "Data_Nationalities")]
        [ActionName("Nationalities")]
        //[CacheOutputUntilToday]
        [ResponseType(typeof(IEnumerable<GeneralKvPair<long, string>>))]
        public async Task<HttpResponseMessage> Nationalities(DataRequest request)
        {
            return await CallGetData(request, SystemDataTable.Nationality).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the list of all available positions.
        /// </summary>
        /// <param name="request">The search parameters.</param>
        /// <returns>System provided ID and name of the positions.</returns>
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData("2015-05-25", markType: 3, aliasName: "Data_Positions")]
        [ActionName("Positions")]
        //[CacheOutputUntilToday]
        [ResponseType(typeof(IEnumerable<GeneralKvPair<long, string>>))]
        public async Task<HttpResponseMessage> Positions(DataRequest request)
        {
            return await CallGetData(request, SystemDataTable.Position).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the list of all available religions.
        /// </summary>
        /// <param name="request">The search parameters.</param>
        /// <returns>System provided ID and name of the religions.</returns>
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData("2015-05-25", markType: 3, aliasName: "Data_Religions")]
        [ActionName("Religions")]
        //[CacheOutputUntilToday]
        [ResponseType(typeof(IEnumerable<GeneralKvPair<long, string>>))]
        public async Task<HttpResponseMessage> Religions(DataRequest request)
        {
            return await CallGetData(request, SystemDataTable.Religion).ConfigureAwait(false);
        }

        /// <summary>
        /// Returns the list of all available skill types.
        /// </summary>
        /// <param name="request">The search parameters.</param>
        /// <returns>System provided ID and name of the skill types.</returns>
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData("2015-05-25", markType: 3, aliasName: "Data_SkillTypes")]
        [ActionName("SkillTypes")]
        //[CacheOutputUntilToday]
        [ResponseType(typeof(IEnumerable<GeneralKvPair<long, string>>))]
        public async Task<HttpResponseMessage> SkillTypes(DataRequest request)
        {
            return await CallGetData(request, SystemDataTable.SkillType).ConfigureAwait(false);
        }

        ///// <summary>
        ///// Returns the list of all available towns.
        ///// </summary>
        ///// <param name="request">The search parameters.</param>
        ///// <returns>System provided ID and name of the towns.</returns>
        //[HttpPost]
        //[MetaData("2015-05-25", markType: 3, aliasName: "Data_Towns")]
        //[ActionName("Towns")]
        ////[CacheOutputUntilToday]
        //[ResponseType(typeof(IEnumerable<GeneralKvPair<int, string>>))]
        //public async Task<HttpResponseMessage> Towns(DataSearchRequest request)
        //{
        //    return await CallGetData(request, SystemDataTable.Town).ConfigureAwait(false);
        //}


        [NonAction]
        private async Task<HttpResponseMessage> CallGetData(DataRequest request, string tableName)
        {
            var response = await _service.GetData(tableName, request.Id, request.SearchString).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }
    }
}
