using Model.Account;
using Model.Common;
using Model.Types;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TekTak.iLoop.Helper;
using TekTak.iLoop.Kauwa;
using Utility;

namespace TekTak.iLoop.Account
{
    public class AccountRepository : IAccountRepository
    {
        protected readonly Services Client;

        public AccountRepository(Services client)
        {
            Client = client;
        }

        public async Task<StatusData<AccountInternal>> SignUpPerson(SignUpRequestPerson request)
        {
            var user = new User
                  {
                      UserName = request.UserName,
                      Password = request.Password,
                      Email = request.Email,
                      // UsernameEmail = request.Email,
                      UserInfoPerson = new UserInfoPerson
                      {
                          FirstName = request.FirstName,
                          LastName = request.LastName,
                          Gender = ((byte)request.Gender).ToString(),
                          BirthDate = request.DateOfBirth.ToString("yyyy-MM-dd")
                      },
                      UserInfo = new UserInfo { CountryCode = request.CountryCode }
                  };

            var result = new StatusData<AccountInternal> { Status = SystemDbStatus.Inserted };

            var response = await Task.Factory.StartNew(() => Client.UserService.RegisterUser(user)).ConfigureAwait(false);

            result.Status = (SystemDbStatus)response.DbStatusCode;
            result.Data = new AccountInternal { UserGuid = response.UserGUID, UserId = Convert.ToInt32(response.UserId) };
            result.Message = response.DbStatusMsg;

            return result;
        }
        public async Task<StatusData<LoginResponse>> Login(LoginRequest request)
        {
            var data = new StatusData<LoginResponse> { Status = SystemDbStatus.Selected, Data = new LoginResponse() };
            var result = new StatusData<LoginResponse> { Status = SystemDbStatus.Selected };

            var user = new User { UsernameEmail = request.UserName, Password = request.Password, DeviceId = request.DeviceId, Session = new Session { Replay = true, DeviceType = request.DeviceType, PushCode = request.PushCode, TransportIp = request.Ip, ModelName = request.ModelName } };
            var response = await Task.Factory.StartNew(() => Client.UserService.Auth(user)).ConfigureAwait(false);

            data.Status = (SystemDbStatus)response.DbStatusCode;

            if (!data.Status.IsOperationSuccessful())
            {
                //switch ((SystemAccountStatus)response.DbStatusCode)
                //{
                //    case SystemAccountStatus.UserNotExist:
                //    case SystemAccountStatus.WrongPassword:
                //    case SystemAccountStatus.NotYetApproved:
                //    case SystemAccountStatus.Locked:
                //    case SystemAccountStatus.Deactivated:
                //        {
                //            data.Status = SystemDbStatus.Unauthorized;
                //            data.Data.LoginStatus = (SystemAccountStatus)response.DbStatusCode;
                //            data.Message = response.DbStatusMsg;
                //            return data;
                //        }
                //}
                //data.Status = SystemDbStatus.Unauthorized;
                data.SubStatus = response.DbSubStatusCode;
                data.Message = response.DbStatusMsg;
                return data;
            }

            result.Data = new LoginResponse
            {
                UserId = Convert.ToInt32(response.UserId),
                UserName = response.UserName,
                UserTypeId = (response.UserInfoPerson == null ? SystemUserType.Business : SystemUserType.Person),
                Token = response.Session.SessionToken
            };
            result.Data.AuthorizationToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}:{2}:{3}:{4}:{5}", result.Data.UserId, result.Data.UserName, (byte)result.Data.UserTypeId, request.DeviceType, request.DeviceId, result.Data.Token)));
            return result;
        }
        public bool IsAuthenticated(int userId, string userName, string token, string deviceId, string deviceType, string transportIp)
        {
            var session = new Session { SessionToken = token, DeviceId = deviceId, UserId = userName, IUserId = userId, DeviceType = deviceType, TransportIp = transportIp, Replay = true };
            return Client.SessionService.CheckSession(session);
        }
        public async Task<StatusData<AccountInternal>> ForgotPassword(string userName)
        {
            var response = new StatusData<AccountInternal> { Status = SystemDbStatus.Updated };

            var user = await Task.Factory.StartNew(() => Client.UserService.getUserLoginInfo(userName)).ConfigureAwait(false);
            if (user == null)
            {
                response.Status = SystemDbStatus.NotFound;
                return response;
            }

            var userGuid = await GetPasswordResetCode(user.UserId, user.UserName).ConfigureAwait(false);
            userGuid = userGuid.TrimEnd('\r', '\n');

            var userInfo = new AccountInternal
            {
                UserId = Convert.ToInt32(user.UserId),
                UserName = user.UserName,
                UserGuid = userGuid,
                FirstName = user.UserInfo.UserTypeId == (byte)SystemUserType.Person ? user.UserInfoPerson.FirstName : user.UserInforCompany.Name,
                ImageServerAddress = SystemConstants.ImageServerAddress.ToString(),
                UrlRegistrationLink = new Uri(new Uri(SystemConstants.WebUrl.Value), "index.html#/confirm-email-reset/" + HttpUtility.UrlEncode(Convert.ToBase64String(Encoding.UTF8.GetBytes(user.UserName))) + "/" + HttpUtility.UrlEncode(userGuid) + "/true").ToString(),
                UrlVerificationLink = new Uri(new Uri(SystemConstants.WebUrl.Value), "index.html#/confirm-email-reset/" + HttpUtility.UrlEncode(Convert.ToBase64String(Encoding.UTF8.GetBytes(user.UserName))) + "/" + HttpUtility.UrlEncode(userGuid) + "/false").ToString(),
                Email = user.Email
            };

            // user.UserGUID = userGuid;
            // user.LastActivityDate = DateTime.UtcNow;

            response.Status = SystemDbStatus.Updated;
            response.Data = userInfo;
            return response;
        }
        public async Task<StatusData<AccountInternal>> VerifyUser(string userNameOrEmail)
        {
            var response = new StatusData<AccountInternal> { Status = SystemDbStatus.Updated };

            var user = await Task.Factory.StartNew(() => Client.UserService.verifyUser(userNameOrEmail)).ConfigureAwait(false);
            if (user == null)
            {
                response.Status = SystemDbStatus.NotFound;
                return response;
            }


            var userInfo = new AccountInternal
            {
                UserId = Convert.ToInt32(user.UserId),
                UserName = user.UserName,
                FirstName = user.UserInfo.UserTypeId == (byte)SystemUserType.Person ? user.UserInfoPerson.FirstName : user.UserInforCompany.Name,
                ImageServerAddress = SystemConstants.ImageServerAddress.ToString(),
                UserGuid = user.UserGUID
            };

            response.Status = (SystemDbStatus)user.DbStatusCode;
            response.Data = userInfo;
            response.Message = user.DbStatusMsg;
            return response;
        }
        public async Task<StatusData<AccountInternal>> GetUserInfo(string userNameOrEmailOrId)
        {
            var response = new StatusData<AccountInternal> { Status = SystemDbStatus.Selected };

            var user = await Task.Factory.StartNew(() => Client.UserService.getUserLoginInfo(userNameOrEmailOrId)).ConfigureAwait(false);
            if (user == null)
            {
                response.Status = SystemDbStatus.NotFound;
                return response;
            }

            var userInfo = new AccountInternal
            {
                UserId = Convert.ToInt32(user.UserId),
                UserName = user.UserName,
                FirstName = user.UserInfo.UserTypeId == (byte)SystemUserType.Person ? user.UserInfoPerson.FirstName + ' ' + user.UserInfoPerson.LastName : user.UserInforCompany.Name,
                ImageServerAddress = SystemConstants.ImageServerAddress.ToString(),
                UserGuid = user.UserGUID,
                Email = user.Email
            };

            response.Status = (SystemDbStatus)user.DbStatusCode;
            response.Data = userInfo;
            response.Message = user.DbStatusMsg;
            return response;
        }
        public async Task<StatusData<string>> UpdatePassword(string oldpassword, string newpassword, SystemSession session)
        {
            var response = new StatusData<string> { Status = SystemDbStatus.Updated };

            var user = await Task.Factory.StartNew(() => Client.UserService.updatePassword(oldpassword, newpassword, session.GetSession())).ConfigureAwait(false);
            if (user == null)
            {
                response.Status = SystemDbStatus.NotFound;
                return response;
            }
            response.Status = (SystemDbStatus)user.DbStatusCode;
            response.Message = user.DbStatusMsg;
            return response;
        }
        public async Task<string> GetPasswordResetCode(string userId, string userName)
        {
            var user = new User
            {
                UserName = userName,
                UserId = userId
            };
            return await Task.Factory.StartNew(() => Client.UserService.forgotPasswordCode(user)).ConfigureAwait(false);

        }
        public async Task<StatusData<bool>> UserExists(string targetUser)
        {
            var response = new StatusData<bool> { Status = SystemDbStatus.Selected, Data = true };

            var user = await Task.Factory.StartNew(() => Client.UserService.getUserInfo(targetUser, null)).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                response.Data = false;
                return response;
            }

            return response;
        }
    }
}
