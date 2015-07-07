using Model.Common;
using Model.Setting;
using Model.Types;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TekTak.iLoop.Setting
{
    public interface ISettingRepository
    {
        //StatusData<GeneralKvPair<string, string>> SignOut(SystemSession sessionObject, SignOut request);

        Task<IEnumerable<SettingResponse>> ActiveDeviceList(SystemSession session);
        Task<IQueryable<UserSettingResponse>> GetSettings(byte? settingTypeId, bool isWeb, SystemSession session);
        Task<StatusData<string>> UpsertSetting(UserSettingRequest request, int mode, SystemSession session);
        Task<StatusData<string>> ChangePassword(ChangePasswordRequest request, SystemSession session);
        Task<StatusData<bool>> LogOut(List<GeneralKvPair<string, string>> request, SystemSession session);
        Task<StatusData<bool>> SignOut(SystemSession sessionObject, SignOut request);
    }
}
