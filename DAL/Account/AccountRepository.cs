using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DAL.DbEntity;
using Entity;
using Model.Account;
using Model.Common;
using Model.Types;
using Utility;

namespace DAL.Account
{
    public class AccountRepository : GenericRepository<UserLogin>, IAccountRepository
    {
        public AccountRepository(iLoopEntity context) : base(context) { }

        public string RequestDeviceTaggedToken(string uniqueDeviceId)
        {
            var expiryTimeInMinutes = Convert.ToInt32(SystemConstants.TemporaryTokenExpiryTimeInMinutes);
            return Encryptor.GenerateExpiryToken(expiryTimeInMinutes, uniqueDeviceId);
        }

        public async Task<StatusData<AccountInternal>> SignUpPerson(SignUpRequestPerson request)
        {
            var result = new StatusData<AccountInternal> { Data = new AccountInternal() };
            var saltKey = Encryptor.CreateSaltKey(5);
            var hashPassword = Encryptor.CreatePasswordHash(request.Password, saltKey);
            var userIdObj = new ObjectParameter("userId", 0);
            var userGuidObj = new ObjectParameter("userGUID", 0);
            var mobileResetCodeObj = new ObjectParameter("MobileResetCode", 0);

            result.Status = (SystemDbStatus)await Task.Factory.StartNew(() => Context.PROC_USERLOGIN_CREATEPERSON(request.UserName, request.FirstName, request.LastName, (byte)request.Gender, hashPassword, saltKey, request.Email, 1, request.DateOfBirth, request.CountryCode, userIdObj, userGuidObj, mobileResetCodeObj).FirstOrDefault().GetValueOrDefault()).ConfigureAwait(false);

            if (result.Status.IsOperationSuccessful())
            {
                result.Data.UserGuid = userGuidObj.Value.ToString();
                result.Data.UserId = Convert.ToInt32(userIdObj.Value);
            }

            return result;
        }

        public async Task SendEmail(string emailRecipient, string cc, string bcc, string emailSubject, string emailBody, string emailBodyFormat, string emailImportance)
        {
            await Task.Factory.StartNew(() => Context.PROC_SEND_MAIL(emailRecipient, cc, bcc, emailSubject, emailBody, emailBodyFormat, emailImportance)).ConfigureAwait(false);
        }

        public bool IsAuthenticated(int userId, string userName, string token, string deviceId)
        {
            var user = Context.UserMultiLogins.FirstOrDefault(x => userId == x.UserID && x.UserLogin.UserName == userName && token == x.LoginToken && deviceId == x.DeviceID);
            return null != user;
        }

        public async Task<StatusData<LoginResponse>> Login(LoginRequest request)
        {
            var data = new StatusData<LoginResponse> { Status = SystemDbStatus.Selected, Data = new LoginResponse() };
            var dbStatus = new ObjectParameter("DBSTATUS", 0);
            var dbSubStatus = new ObjectParameter("DBSUBSTATUS", 0);
            var dbMessage = new ObjectParameter("DBMESSAGE", 0);
            var user = await FirstOrDefaultAsync(x => request.UserName.Equals(x.UserName));
            if (null == user)
            {
                //data.Data.LoginStatus = SystemAccountStatus.UserNotExist;
                data.Status = SystemDbStatus.Unauthorized;
                return data;
            }

            var hashedPassword = Encryptor.CreatePasswordHash(request.Password, user.PasswordSalt);
            await Task.Factory.StartNew(() => Context.PROC_USERLOGIN(request.UserName, hashedPassword, user.PasswordSalt, dbStatus, dbSubStatus, dbMessage).FirstOrDefault()).ConfigureAwait(false);
            var result = Convert.ToInt32(dbStatus.Value);
            if (result != 0)
            {
                switch ((SystemAccountSubCode)result)
                {
                    case SystemAccountSubCode.UserNotExist:
                    case SystemAccountSubCode.WrongPassword:
                    case SystemAccountSubCode.NotYetApproved:
                    case SystemAccountSubCode.Locked:
                    case SystemAccountSubCode.Deactivated:
                        {
                            data.Status = SystemDbStatus.Unauthorized;
                            //data.Data.LoginStatus = result;
                            return data;
                        }
                }
                return data;
            }

            user.LastActivityDate = DateTime.UtcNow;
            var deviceLog = user.UserMultiLogins.FirstOrDefault(x => x.DeviceID == request.DeviceId && x.BluetoothID == request.BluetoothId);
            if (deviceLog != null)
                deviceLog.LastActivity = DateTime.UtcNow;
            else
            {
                deviceLog = Context.UserMultiLogins.Add(new UserMultiLogin { UserID = user.UserId, BluetoothID = request.BluetoothId, ModelNumber = request.ModelName, DeviceID = request.DeviceId, LastActivity = DateTime.UtcNow, LoginToken = Guid.NewGuid().ToString() });
            }

            data.Data.UserId = deviceLog.UserID;
            data.Data.UserTypeId = (SystemUserType)user.UserInfo.UserTypeID;
            data.Data.Token = deviceLog.LoginToken;
            return data;
        }

        public async Task<StatusData<AccountInternal>> ForgotPassword(string userName)
        {
            var response = new StatusData<AccountInternal> { Status = SystemDbStatus.Updated };

            var user = await FirstOrDefaultAsync(x => (x.LoweredUserName == userName || x.LoweredEmail == userName)).ConfigureAwait(false);
            if (user == null)
            {
                response.Status = SystemDbStatus.NotFound;
                return response;
            }

            var userGuid = Guid.NewGuid().ToString();
            var userInfo = new AccountInternal
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserGuid = userGuid,
                FirstName = user.UserInfo.UserTypeID == (byte)SystemUserType.Person ? user.UserInfo.UserInfoPerson.FirstName : user.UserInfo.UserInfoCompany.Name,
                ImageServerAddress = SystemConstants.ImageServerAddress.ToString(),
                UrlRegistrationLink = new Uri(new Uri(SystemConstants.WebUrl.Value), "\\Account\\ResetPassword?qid=" + Encryptor.EncryptHelper("id=" + user.UserId) + "&authkey=" + HttpUtility.UrlEncode(userGuid) + "&isdirect=true".Replace("\\", "/")).ToString(),
                UrlVerificationLink = new Uri(new Uri(SystemConstants.WebUrl.Value), "\\Account\\ResetPassword?qid=" + Encryptor.EncryptHelper("id=" + user.UserId) + "&authkey=" + HttpUtility.UrlEncode(userGuid) + "&isdirect=false".Replace("\\", "/")).ToString(),
                //MobileUnlockCode = Encryptor.RandomString(4, "0123456789"),
                Email = user.LoweredEmail
            };

            user.UserGUID = userGuid;
            user.LastActivityDate = DateTime.UtcNow;

            response.Data = userInfo;
            return response;
        }
    }
}
