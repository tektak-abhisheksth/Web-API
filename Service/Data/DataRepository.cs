using Model.Common;
using Model.CountryInfo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekTak.iLoop.Kauwa;

namespace TekTak.iLoop.Data
{
    public class DataRepository : IDataRepository
    {
        protected readonly Services Client;

        public DataRepository(Services client)
        {
            Client = client;
        }

        public async Task<IEnumerable<GeneralKvPair<long, string>>> GetData(string tableName, int? id, string searchString)
        {
            var serviceRequest = new GetDataParam
            {
                Table = tableName,
                Id = id ?? 0,
                SearchTerm = searchString
            };

            var response = await Task.Factory.StartNew(() => Client.UserService.getData(serviceRequest, null)).ConfigureAwait(false);
            return response.Select(x => new GeneralKvPair<long, string>
            {
                Id = (long)x.Id,
                Value = x.Name
            });
        }

        public async Task<IEnumerable<UserCity>> GetCities(int? id, string searchString)
        {
            var response = await Task.Factory.StartNew(() => Client.UserService.getCity(id ?? 0, searchString, null)).ConfigureAwait(false);
            return response.Select(x => new UserCity
            {
                Id = x.CityId,
                Name = x.Name,
                CountryCode = x.CountryCode.CountryCode,
                CountryName = x.CountryCode.Name
                //Latitude = x.Latitude,
                //Longitude = x.Longitude
            });
        }
    }
}
