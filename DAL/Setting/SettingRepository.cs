using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DAL.DbEntity;
using Entity;
using Model.Common;
using Model.Setting;
using Model.Types;
using Utility;

namespace DAL.Setting
{
    public class SettingRepository : GenericRepository<UserLogin>, ISettingRepository
    {
        public SettingRepository(iLoopEntity context) : base(context) { }

        public async Task<IEnumerable<SettingResponse>> ActiveDeviceList(int userId)
        {
            var resultDb = await Task.Factory.StartNew(() => FirstOrDefault(x => x.UserId == userId).UserMultiLogins.ToList());

            var result = resultDb.Select(x => new SettingResponse
               {
                   LastActivity = Convert.ToInt64(x.LastActivity),
                   DeviceId = x.DeviceID,
                   ModelName = x.ModelNumber
               });
            return result.OrderByDescending(x => x.LastActivity);
        }

        public async Task<StatusData<Dictionary<string, string>>> RemoveLinkedDeviceList(ActiveDeviceRequest request)
        {
            var result = new StatusData<Dictionary<string, string>> { Status = SystemDbStatus.Deleted, Data = new Dictionary<string, string>() };

            foreach (var deviceId in request.DeviceIds.Distinct())
            {
                var userDevice = await Context.UserMultiLogins.FirstOrDefaultAsync(y => y.UserID == request.UserId && y.DeviceID == deviceId);
                if (userDevice != null)
                {
                    result.Data.Add(userDevice.LoginToken, deviceId);
                    await Task.Factory.StartNew(() => Context.UserMultiLogins.Remove(userDevice)).ConfigureAwait(false);
                }
            }

            await Task.Factory.StartNew(() => Context.UserMobileContacts.RemoveRange(Context.UserMobileContacts.Where(x => x.UserID == request.UserId && request.DeviceIds.Contains(x.DeviceID)))).ConfigureAwait(false);
            return result;
        }

        public async Task<IQueryable<UserSettingResponse>> GetSettings(int userId, byte? settingTypeId)
        {
            var user = await FirstOrDefaultAsync(x => x.UserId == userId).ConfigureAwait(false);
            IQueryable<UserSettingResponse> result;
            if (user.UserInfo.UserTypeID == (byte)SystemUserType.Person)
            {
                result =
                  await Task.Factory.StartNew(() => Context.SettingPersons.Where(x => userId == x.UserId && 2 == x.SettingTypePerson.Visible && (!settingTypeId.HasValue || settingTypeId == x.SettingTypeId))
                      .OrderBy(x => x.SettingTypePerson.DisplayOrder)
                      .Select(
                          x =>
                              new UserSettingResponse
                              {
                                  SettingTypeId = x.SettingTypeId,
                                  Name = x.SettingTypePerson.Name,
                                  Value = x.Value,
                                 // SettingGroupId = x.SettingTypePerson.SettingGroup,
                                  Entries = Context.SettingReferencePersons.Where(y => new[] { (int)SystemSettingValue.Category, (int)SystemSettingValue.Contact }.Contains(x.Value) && x.ReferenceToken != null && y.ReferenceToken == x.ReferenceToken).Select(y => y.EntryList)
                              })).ConfigureAwait(false);
            }
            else
            {
                result =
              await Task.Factory.StartNew(() => Context.SettingCompanies.Where(x => userId == x.UserId && 2 == x.SettingTypeCompany.Visible && (!settingTypeId.HasValue || settingTypeId == x.SettingTypeId))
                  .OrderBy(x => x.SettingTypeCompany.DisplayOrder)
                  .Select(
                      x =>
                          new UserSettingResponse
                          {
                              SettingTypeId = x.SettingTypeId,
                              Name = x.SettingTypeCompany.Name,
                              Value = x.Value,
                             // SettingGroupId = x.SettingTypeCompany.SettingGroup,
                              Entries = Context.SettingReferenceCompanies.Where(y => new[] { (int)SystemSettingValue.Category, (int)SystemSettingValue.Contact }.Contains(x.Value) && x.ReferenceToken != null && y.ReferenceToken == x.ReferenceToken).Select(y => y.EntryList)
                          })).ConfigureAwait(false);
            }
            return result;
        }

        public async Task<SystemDbStatus> UpsertSetting(UserSettingRequest request)
        {
            return await Task.Factory.StartNew(() => (SystemDbStatus)Context.PROC_UPSERT_CUSTOM_LIST(request.UserId, (byte)request.SettingTypeId, (int)SystemDbStatus.Updated, request.Value, string.Join(",", request.Entries.Distinct()), string.Empty).FirstOrDefault().GetValueOrDefault()).ConfigureAwait(false);
        }

        public async Task<SystemDbStatus> ChangePassword(ChangePasswordRequest request)
        {
            var user = await FirstOrDefaultAsync(x => x.UserId == request.UserId).ConfigureAwait(false);
            if (user != null)
            {
                var oldHashedPassword = Encryptor.CreatePasswordHash(request.Password, user.PasswordSalt);
                if (oldHashedPassword != user.Password)
                    return SystemDbStatus.Unauthorized;
                if (request.NewPassword == request.Password)
                    return SystemDbStatus.NotModified;

                var saltKey = Encryptor.CreateSaltKey(5);
                var hashPassword = Encryptor.CreatePasswordHash(request.NewPassword, saltKey);
                user.Password = hashPassword;
                user.PasswordSalt = saltKey;
                user.UserGUID = Guid.NewGuid().ToString();
                user.LastPasswordChangedDate = DateTime.UtcNow;
            }
            return SystemDbStatus.Updated;
        }

    }
}
