using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.DbEntity;
using Entity;
using Model.Common;
using Model.Setting;
using Model.Types;

namespace DAL.Setting
{
    public interface ISettingRepository : IGenericRepository<UserLogin>
    {
        Task<IEnumerable<SettingResponse>> ActiveDeviceList(int userId);

        Task<StatusData<Dictionary<string, string>>> RemoveLinkedDeviceList(ActiveDeviceRequest request);

        Task<IQueryable<UserSettingResponse>> GetSettings(int userId, byte? settingTypeId);

        Task<SystemDbStatus> UpsertSetting(UserSettingRequest request);

        Task<SystemDbStatus> ChangePassword(ChangePasswordRequest request);
    }
}
