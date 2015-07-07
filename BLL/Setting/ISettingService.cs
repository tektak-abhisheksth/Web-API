using Model.Common;
using Model.Setting;
using Model.Types;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Setting
{
    public interface ISettingService
    {
        Task<IEnumerable<SettingResponse>> ActiveDeviceList(SystemSession session);

        //SystemDbStatus RemoveLinkedDeviceList(ActiveDeviceRequest request);

        Task<IQueryable<UserSettingResponse>> GetSettings(byte? settingTypeId, bool isWeb, SystemSession session);

        Task<StatusData<string>> UpsertSetting(UserSettingRequest request, SystemSession session);

        Task<StatusData<string>> ChangePassword(ChangePasswordRequest request, SystemSession session);
        Task<StatusData<bool>> LogOut(List<GeneralKvPair<string, string>> request, SystemSession session);
    }
}
