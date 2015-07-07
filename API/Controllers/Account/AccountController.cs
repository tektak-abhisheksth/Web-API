using API.Controllers.WebClient;
using API.Filters;
using BLL.Account;
using Model.Account;
using Model.Attribute;
using Model.Common;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Utility;

namespace API.Controllers.Account
{
    /// <summary>
    /// Provides APIs to handle requests related to user account.
    /// </summary>
    [MetaData]
    public partial class AccountController : ApiController
    {
        private readonly IAccountService _accountService;

        /// <summary>
        /// Provides APIs to handle requests related to user account.
        /// </summary>
        /// <param name="accountService"></param>
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Provides user with a unique time-based expiring token bound to the device id.
        /// </summary>
        /// <param name="deviceId">A globally unique identifier that distinguishes the device from others.</param>
        /// <returns>Token along with expiry time in UTC.</returns>
        [HttpGet]
        [AllowAnonymous]
        [MetaData(markType: 3, aliasName: "Account_Get")]
        [ResponseType(typeof(AccountResponse))]
        public HttpResponseMessage Get(string deviceId)
        {
            if (!Validation.StringLength(deviceId, x => deviceId, 10, 200, ActionContext, ModelState))
                return ActionContext.Response;

            var genTime = DateTime.UtcNow;
            var expiresOnUtcTime = genTime.AddMinutes(SystemConstants.TemporaryTokenExpiryTimeInMinutes);
            var token = _accountService.RequestDeviceTaggedToken(deviceId);
            var tokenResponse = new AccountResponse
            {
                TempToken = token,
                GeneratedTimeUtc = genTime,
                ExpiryTimeUtc = expiresOnUtcTime
            };
            return Request.SystemResponse(SystemDbStatus.Selected, tokenResponse);
        }

        /// <summary>
        /// Accepts data from user regarding the new personal account. User is required to send the time-based expiring token in the Authorization header.
        /// </summary>
        /// <param name="request">New account information about the person.</param>
        /// <returns>Status if the registration was accepted or not.</returns>
        //[AcceptVerbs("POST", "PUT")]
        [HttpPut]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData(markType: 3, aliasName: "Account_PersonalAccount")]
        [ActionName("PersonalAccount")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> RequestPersonalAccount([FromBody]SignUpRequestPerson request)
        {
            return await SignUpPerson(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Accepts data from user regarding user login.
        /// </summary>
        /// <param name="request">Existing account information about the user.</param>
        /// <returns>System assigned user ID and login token.</returns>
        //[AcceptVerbs("POST", "PUT")]
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData(markType: 3, aliasName: "Account_Login")]
        [ActionName("Login")]
        [ResponseType(typeof(LoginResponse))]
        public async Task<HttpResponseMessage> Login([FromBody]LoginRequest request)
        {
            if (!(request.UserName.IsMatch(x => request.UserName, RegexPattern.UserName, ActionContext, ModelState)
                  && Validation.StringLength(request.UserName, x => request.UserName, 6, 30, ActionContext, ModelState))
                && !(request.UserName.IsMatch(x => request.UserName, RegexPattern.Email, ActionContext, ModelState)))
                return ActionContext.Response;

            if (request.DeviceType.Equals("W", StringComparison.OrdinalIgnoreCase))
                request.Ip = HttpContext.Current.Request.UserHostAddress;
            //request.DeviceId = Request.Headers.UserAgent.ToString();

            var response = await _accountService.Login(request).ConfigureAwait(false);
            return Request.SystemResponse(response);
        }

        /// <summary>
        /// Accepts user name or email address if the user forgets his/her/its password.
        /// </summary>
        /// <param name="request">User name or email address of the user along with device id.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData(markType: 3, aliasName: "Account_ForgotPassword")]
        [ActionName("ForgotPassword")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (!((request.UserName.IsMatch(x => request.UserName, RegexPattern.UserName, ActionContext, ModelState)
                   && Validation.StringLength(request.UserName, x => request.UserName, 6, 30, ActionContext, ModelState))
                  || request.UserName.IsMatch(x => request.UserName, RegexPattern.Email, ActionContext, ModelState)))
                return ActionContext.Response;

            var response = await _accountService.ForgotPassword(request.UserName);

            if (response.Status.IsOperationSuccessful())
            {
                var html = ViewRenderer.RenderView("~/Views/Home/OutMail/_ForgotPasswordTemplatePartial.cshtml",
                    response.Data);
                Helper.SendMail(response.Data.Email, "Reset your iLoop password", html,
                    bodyImages: new List<string> { "~/Images/iLoop.png" });
            }
            return Request.SystemResponse<string>(response.Status);
        }

        /// <summary>
        ///  Accepts user ID, user name or email address of the user to verify whether the user exists or not.
        /// </summary>
        /// <param name="request">The target user's user ID, user name or email address.</param>
        /// <returns>An indication as to whether or not the user exists.</returns>
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData("2015-05-27", markType: 3, aliasName: "Account_UserExists")]
        [ActionName("UserExists")]
        [ResponseType(typeof(bool))]
        public async Task<HttpResponseMessage> UserExists([FromBody] SingleDataAnonymous<string> request)
        {
            if (!(request.Data.IsMatch(x => request.Data, RegexPattern.UserName, ActionContext, ModelState)
                  && Validation.StringLength(request.Data, x => request.Data, 6, 30, ActionContext, ModelState))
                && !(request.Data.IsMatch(x => request.Data, RegexPattern.Email, ActionContext, ModelState)))
                return ActionContext.Response;

            var response = await _accountService.UserExists(request.Data).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, new { response.Data });
        }

        [NonAction]
        private async Task<HttpResponseMessage> SignUpPerson(SignUpRequestPerson request)
        {
            var response = await _accountService.SignUpPerson(request);
            if (response.Status.IsOperationSuccessful())
            {
                var registration = new AccountInternal
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    UserName = request.UserName,
                    ImageServerAddress = SystemConstants.ImageServerAddress.ToString(),
                    UrlRegistrationLink =
                        new Uri(new Uri(SystemConstants.WebUrl.Value),
                            "index.html#/activation/" +
                            HttpUtility.UrlEncode(Convert.ToBase64String(Encoding.UTF8.GetBytes(request.UserName))) + "/" +
                            HttpUtility.UrlEncode((Convert.ToBase64String(Encoding.UTF8.GetBytes(response.Data.UserGuid)))))
                            .ToString(),
                    UrlVerificationLink =
                        new Uri(new Uri(SystemConstants.WebUrl.Value),
                            "index.html#/activation/" +
                            HttpUtility.UrlEncode(Convert.ToBase64String(Encoding.UTF8.GetBytes(request.UserName))) + "/")
                            .ToString(),
                    UserGuid = response.Data.UserGuid,
                    UserId = response.Data.UserId
                };

                var html = ViewRenderer.RenderView("~/Views/Home/OutMail/_RegistrationTemplatePartial.cshtml",
                        registration);
                Helper.SendMail(registration.Email, "Activation Mail", html,
                    bodyImages: new List<string> { "~/Images/iLoop.png" });
            }
            return Request.SystemResponse<string>(response.Status);
        }
    }
}