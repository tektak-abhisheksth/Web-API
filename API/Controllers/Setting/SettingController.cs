using BLL.Setting;
using Model.Attribute;
using Model.Common;
using Model.Setting;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Utility;

namespace API.Controllers.Setting
{
    /// <summary>
    /// Provides APIs to handle requests related to setting.
    /// </summary>
    [MetaData]
    public partial class SettingController : ApiController
    {
        private readonly ISettingService _service;

        /// <summary>
        /// Provides APIs to handle requests related to setting.
        /// </summary>
        /// <param name="service"></param>
        public SettingController(ISettingService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets the information of user's active devices.
        /// </summary>
        /// <returns>Last activity, device ID and model name of the user's active device.</returns>
        [HttpGet]
        [MetaData(markType: 3, aliasName: "Setting_GetActiveDevices")]
        [ActionName("ActiveDevices")]
        [ResponseType(typeof(IEnumerable<SettingResponse>))]
        public async Task<HttpResponseMessage> ActiveDevices()
        {
            var response = await _service.ActiveDeviceList(Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }


        ///// <summary>
        ///// Allows to sign out active device from the server. Finalized code.
        ///// </summary>
        ///// <param name="request">Active device information.</param>
        ///// <returns>The status of the operation.</returns>
        //[HttpDelete]
        //[ActionName("ActiveDevice")]
        //public async Task<HttpResponseMessage> SignOut([FromBody]SignOut request)
        //{
        //    var response = _service.SignOut(request);
        //    var cacheTokens = response.Data.Select(x => Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}:{2}:{3}:{4}",
        //        Request.GetUserInfo<int>(SystemSessionEntity.UserId),
        //        Request.GetUserInfo<string>(SystemSessionEntity.UserName),
        //        Request.GetUserInfo<byte>(SystemSessionEntity.UserTypeId),
        //        x.Value,
        //        x.Key
        //        ))));

        //    cacheTokens.ForEach(MemoryCache.Delete);
        //    return Request.SystemResponse<string>(response.Status);
        //}

        ///// <summary>
        ///// Allows to sign out active device(s) from the server.
        ///// </summary>
        ///// <returns>The status of the operation.</returns>
        //[HttpDelete]
        //[MetaData("2015-01-01")]
        //[ActionName("ActiveDevice")]
        //[ResponseType(typeof(string))]
        //public HttpResponseMessage SignOut([FromBody]SingleData<List<GeneralKvPair<string, string>>> request)
        //{
        //    if (!Validation.IsEnumerablePopulated(request.Data, x => request.Data, ActionContext, ModelState))
        //        return ActionContext.Response;

        //    foreach (var item in request.Data)
        //    {
        //        var serviceRequest = new SignOut
        //        {
        //            UserName = Request.GetUserInfo<string>(SystemSessionEntity.UserName),
        //            UserId = Request.GetUserInfo<int>(SystemSessionEntity.UserId),
        //            DeviceId = item.Id,
        //            Token = item.Value
        //        };

        //        var response = _service.SignOut(Request.GetSession(), serviceRequest);
        //        var cacheToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}:{2}:{3}:{4}",
        //             Request.GetUserInfo<int>(SystemSessionEntity.UserId),
        //             Request.GetUserInfo<string>(SystemSessionEntity.UserName),
        //             Request.GetUserInfo<byte>(SystemSessionEntity.UserTypeId),
        //             response.Data.Id,
        //             response.Data.Value)));
        //        MemoryCache.Delete(cacheToken);
        //    }
        //    return Request.SystemResponse<string>(SystemDbStatus.Deleted);
        //}

        /// <summary>
        /// Allows to sign out active device(s) from the server.
        /// </summary>
        /// <param name="request">List of device IDs and login tokens.</param>
        /// <returns>The status of the operation.</returns>
        [HttpDelete]
        [MetaData(markType: 3, aliasName: "Setting_DeleteActiveDevice")]
        [ActionName("ActiveDevice")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> SignOut([FromBody]SingleData<List<GeneralKvPair<string, string>>> request)
        {
            if (!Validation.IsEnumerablePopulated(request.Data, x => request.Data, ActionContext, ModelState))
                return ActionContext.Response;

            var userId = Request.GetUserInfo<int>(SystemSessionEntity.UserId);
            var userName = Request.GetUserInfo<string>(SystemSessionEntity.UserName);
            var userTypeId = Request.GetUserInfo<byte>(SystemSessionEntity.UserTypeId);
            var deviceTypeId = Request.GetUserInfo<byte>(SystemSessionEntity.DeviceTypeId);

            var response = await _service.LogOut(request.Data, Request.GetSession()).ConfigureAwait(false);

            if (response.Data)
                foreach (var cacheToken in request.Data.Select(kv => Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}:{2}:{3}:{4}:{5}", userId, userName, userTypeId, deviceTypeId, kv.Id, kv.Value)))))
                    MemoryCache.Delete(cacheToken);

            return Request.SystemResponse(response.Status, new { response.Data });
        }

        /// <summary>
        /// Gets user's mobile settings.
        /// </summary>
        /// <param name="settingTypeId">The system provided setting type ID of the user (optional).</param>
        /// <param name="isWeb">An indication as to whether or not the request is designated for web.</param>
        /// <returns>Current settings of personal user.</returns>
        [HttpGet]
        [MetaData(markType: 3, aliasName: "Setting_Get")]
        [ResponseType(typeof(IQueryable<UserSettingResponse>))]
        public async Task<HttpResponseMessage> Get(byte? settingTypeId = null, bool isWeb = false)
        {
            var response = await _service.GetSettings(settingTypeId, isWeb, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Allows to update user settings.
        /// </summary>
        /// <param name="request">Updated information about user settings.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Setting_Post")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Post([FromBody] UserSettingRequest request)
        {
            var response = await _service.UpsertSetting(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Accepts data from user regarding changing the password.
        /// </summary>
        /// <param name="request">Existing password and new password of the user.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Setting_ChangePassword")]
        [ActionName("ChangePassword")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            //var devices = await _service.ActiveDeviceList(Request.GetSession()).ConfigureAwait(false); //Removal of all sessions require accurate device ids for web.
            var response = await _service.ChangePassword(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

    }
}
