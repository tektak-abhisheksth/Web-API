using Model.Account;
using Model.Common;
using Model.Profile.Personal;
using Model.Types;
using System;
using System.Threading.Tasks;
using TekTak.iLoop.Account;
using TekTak.iLoop.Kauwa;

namespace TekTak.iLoop.Sealed.Account
{
    public sealed class AccountRepositorySealed : AccountRepository, IAccountRepositorySealed
    {
        public AccountRepositorySealed(Services client)
            : base(client)
        { }

        public async Task<StatusData<AccountInternal>> SignUpBusiness(SignUpRequestBusiness request)
        {
            var user = new User
            {
                UserName = request.UserName,
                Password = request.Password,
                Email = request.Email,
                UserInforCompany = new UserInfoCompany
                {
                    Name = request.CompanyName,
                    Date = request.EstablishedDate.ToString("yyyy-MM-dd"),
                    OwnershipType = new OwnershipType { Id = request.OwnershipTypeId },
                    Industry = new Industry { IndustryId = request.CompanyType }

                },
                UserInfo = new UserInfo { CountryCode = request.CountryCode, Url = request.CompanyUrl }
            };

            var result = new StatusData<AccountInternal> { Status = SystemDbStatus.Inserted };

            var response = await Task.Factory.StartNew(() => Client.UserService.RegisterBusinessUser(user)).ConfigureAwait(false);

            result.Status = (SystemDbStatus)response.DbStatusCode;
            result.Data = new AccountInternal { UserGuid = response.UserGUID, UserId = Convert.ToInt32(response.UserId) };
            result.Message = response.DbStatusMsg;

            return result;
        }

        public async Task<StatusData<string>> Activate(ActivateUser request)
        {
            var response = await Task.Factory.StartNew(() => Client.UserService.activateUser(request.UserName, request.UserGuid)).ConfigureAwait(false);
            var data = new StatusData<string> { Status = (SystemDbStatus)response.DbStatusCode, Message = response.DbStatusMsg, SubStatus = response.DbSubStatusCode };
            return data;
        }

        // SingleData<GeneralKvPair<long, int>> request

        //public async Task<StatusData<string>> ResetPassword(string resetPasswordCode, string newpassword)
        //{
        //    var response = new StatusData<string> { Status = SystemDbStatus.Updated };

        //    var user = await Task.Factory.StartNew(() => Client.UserService.resetPassword(resetPasswordCode, newpassword)).ConfigureAwait(false);
        //    if (user == null)
        //    {
        //        response.Status = SystemDbStatus.NotFound;
        //        return response;
        //    }
        //    response.Status = (SystemDbStatus)user.DbStatusCode;
        //    response.Message = user.DbStatusMsg;
        //    return response;
        //}

        public async Task<StatusData<BaseInfoResponse>> ResetPassword(ResetPasswordRequest request)
        {
            var response = new StatusData<BaseInfoResponse> { Status = SystemDbStatus.NotFound };

            var user = await Task.Factory.StartNew(() => Client.UserService.resetPassword(request.RequestCode, request.Password)).ConfigureAwait(false);
            if (user == null)
                return response;

            response.Status = (SystemDbStatus)user.DbStatusCode;
            response.Message = user.DbStatusMsg;

            if (response.Status.IsOperationSuccessful())
            {
                var result = await Task.Factory.StartNew(() => Client.UserService.getUserInfo(request.UserName, null)).ConfigureAwait(false);

                if (result.UserId > 0)
                {
                    var uInfo = new BaseInfoResponse
                    {
                        UserId = Convert.ToInt32(result.UserId),
                        UserName = result.UserName,
                        UserTypeId = (SystemUserType)result.UserTypeId,
                        Picture = result.Picture,
                        Title = result.Title,
                        Email = result.Email,
                        FirstName = result.FirstName,
                        LastName = result.LastName
                    };
                    response.Data = uInfo;
                }
            }

            return response;
        }

    }
}
