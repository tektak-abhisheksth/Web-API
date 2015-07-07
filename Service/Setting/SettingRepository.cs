using Model.Common;
using Model.Setting;
using Model.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekTak.iLoop.Helper;
using TekTak.iLoop.Kauwa;

namespace TekTak.iLoop.Setting
{
    public class SettingRepository : ISettingRepository
    {
        protected readonly Services Client;

        public SettingRepository(Services client)
        {
            Client = client;
        }

        //public StatusData<GeneralKvPair<string, string>> SignOut(SystemSession sessionObject, SignOut request)
        //{
        //    var result = new StatusData<GeneralKvPair<string, string>>
        //    {
        //        Status = SystemDbStatus.Deleted,
        //        Data = new GeneralKvPair<string, string>()
        //    };
        //    var session = new Session { SessionToken = request.Token, DeviceId = request.DeviceId, UserId = request.UserName, Replay = true };
        //    //var user = new User { Session = new Session { UserId = request.UserId.ToString(), SessionToken = request.LoginToken, DeviceId = request.DeviceId, Replay = true }, UsernameEmail = request.UserName };
        //    //var aa = _client.Service.Kill(user);
        //    _client.SessionService.killSession(session);
        //    result.Data.Id = request.DeviceId;
        //    result.Data.Value = request.Token;

        //    return result;
        //}

        public async Task<StatusData<bool>> SignOut(SystemSession sessionObject, SignOut request)
        {
            var result = new StatusData<bool>
            {
                Status = SystemDbStatus.Deleted
            };
            var session = new Session { SessionToken = request.Token, DeviceId = request.DeviceId, UserId = request.UserName, Replay = true };
            result.Data = await Task.Factory.StartNew(() => Client.SessionService.killSession(session)).ConfigureAwait(false);
            return result;
        }

        public async Task<StatusData<bool>> LogOut(List<GeneralKvPair<string, string>> request, SystemSession session)
        {
            var result = new StatusData<bool> { Status = SystemDbStatus.Deleted };
            var serviceRequest = JsonConvert.SerializeObject(request.Select(x => new { authToken = x.Value }));
            result.Data = await Task.Factory.StartNew(() => Client.SessionService.sessionKickOut(session.GetSession(), serviceRequest)).ConfigureAwait(false);

            return result;
        }

        public async Task<IEnumerable<SettingResponse>> ActiveDeviceList(SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.SessionService.getSession(session.GetSession())).ConfigureAwait(false);
            var result = response.Select(x => new SettingResponse
            {
                LastActivity = x.LastActivity,
                DeviceId = x.DeviceId,
                ModelName = x.ModelName,
                Token = x.SessionToken,
                DeviceType = x.DeviceType
            });
            return result;
        }

        public async Task<IQueryable<UserSettingResponse>> GetSettings(byte? settingTypeId, bool isWeb, SystemSession session)
        {
            var response = new List<UserSettingResponse>();
            if (settingTypeId != null)
            {
                var serviceSingleResponse = await Task.Factory.StartNew(() => Client.SettingService.getSettingForMobile(session.UserId, isWeb ? 1 : 2, (int)settingTypeId, session.GetSession())).ConfigureAwait(false);
                return new List<UserSettingResponse>
                {
                    new UserSettingResponse
                    {
                        SettingTypeId = (byte) serviceSingleResponse.SettingTypePerson.SettingTypeId,
                        Name = serviceSingleResponse.SettingTypePerson.Name,
                        Value = serviceSingleResponse.SettingValue,
                      // SettingGroupId = serviceSingleResponse.SettingTypePerson.SettingGroup,
                        Entries = serviceSingleResponse.Entries != null ? serviceSingleResponse.Entries.ConvertAll(Convert.ToInt32): null,
                        Description = serviceSingleResponse.SettingTypePerson.Description,
                        SettingGroup = serviceSingleResponse.SettingTypePerson.SettingGroup,
                       DisplayGroup = serviceSingleResponse.SettingTypePerson.DisplayGroup,
                      DisplayOrder = serviceSingleResponse.SettingTypePerson.DisplayOrder,
                      DefaultValue = serviceSingleResponse.SettingTypePerson.DefaultValue
                    }}.AsQueryable();
            }
            var serviceResponse = await Task.Factory.StartNew(() => Client.SettingService.getSettingsForMobile(session.UserId, isWeb ? 1 : 2, session.GetSession())).ConfigureAwait(false);
            serviceResponse.ForEach(x => response.Add(new UserSettingResponse
            {
                SettingTypeId = (byte)x.SettingTypePerson.SettingTypeId,
                Name = x.SettingTypePerson.Name,
                Value = x.SettingValue,
                // SettingGroupId = x.SettingTypePerson.SettingGroup,
                Entries = x.Entries != null ? x.Entries.ConvertAll(Convert.ToInt32) : null,
                Description = x.SettingTypePerson.Description,
                SettingGroup = x.SettingTypePerson.SettingGroup,
                DisplayGroup = x.SettingTypePerson.DisplayGroup,
                DisplayOrder = x.SettingTypePerson.DisplayOrder,
                DefaultValue = x.SettingTypePerson.DefaultValue

            }));
            return response.AsQueryable();
        }

        public async Task<StatusData<string>> UpsertSetting(UserSettingRequest request, int mode, SystemSession session)
        {
            List<string> distinctEntries = null;
            if (request.Entries != null)
                distinctEntries = request.Entries.Distinct().ToList().ConvertAll(Convert.ToString);
            var serviceRequest = new SettingPerson
            {
                UserId = session.UserId.ToString(),
                SettingTypePerson = new SettingTypePerson
                {
                    SettingTypeId = (byte)request.SettingTypeId,
                    SettingGroup = request.SettingGroupId
                },
                SettingValue = request.Value,
                Entries = distinctEntries,
                Mode = mode
            };

            var response = (await Task.Factory.StartNew(() => Client.SettingService.updateSetting(serviceRequest, session.GetSession())).ConfigureAwait(false)).GetStatusData<string>();
            return response;
        }

        public async Task<StatusData<string>> ChangePassword(ChangePasswordRequest request, SystemSession session)
        {
            return (await Task.Factory.StartNew(() => Client.UserService.updatePassword(request.Password, request.NewPassword, session.GetSession())).ConfigureAwait(false)).GetStatusData<string>();
        }
    }
}
