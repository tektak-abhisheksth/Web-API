using DAL.DbEntity;
using Model.Common;
using Model.Setting;
using Model.Types;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Setting
{
    public class SettingService : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TekTak.iLoop.UOW.IUnitOfWork _jUnitOfWork;

        public SettingService(IUnitOfWork unitOfWork, TekTak.iLoop.UOW.IUnitOfWork jUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _jUnitOfWork = jUnitOfWork;
        }

        public Task<IEnumerable<SettingResponse>> ActiveDeviceList(SystemSession session)
        {
            return _jUnitOfWork.Setting.ActiveDeviceList(session);
        }

        //public SystemDbStatus RemoveLinkedDeviceList(ActiveDeviceRequest request)
        //{
        //var respose = await _unitOfWork.Setting.RemoveLinkedDeviceList(request);
        //await _unitOfWork.CommitAsync();
        //return respose;
        //}

        public Task<IQueryable<UserSettingResponse>> GetSettings(byte? settingTypeId, bool isWeb, SystemSession session)
        {
            return _jUnitOfWork.Setting.GetSettings(settingTypeId, isWeb, session);
        }

        public Task<StatusData<string>> UpsertSetting(UserSettingRequest request, SystemSession session)
        {
            return _jUnitOfWork.Setting.UpsertSetting(request, (int)SystemDbStatus.Flushed, session);
        }

        public Task<StatusData<string>> ChangePassword(ChangePasswordRequest request, SystemSession session)
        {
            return _jUnitOfWork.Setting.ChangePassword(request, session);
        }

        public Task<StatusData<bool>> LogOut(List<GeneralKvPair<string, string>> request, SystemSession session)
        {
            return _jUnitOfWork.Setting.LogOut(request, session);
        }
    }
}
