using API.Controllers.WebClient;
using API.Filters;
using Model.Account;
using Model.Attribute;
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
    public partial class AccountController
    {
        /// <summary>
        /// Accepts data from user regarding the new business account. User is required to send the time-based expiring token in the Authorization header.
        /// </summary>
        /// <param name="request">New account information about the company.</param>
        /// <returns>Status if the registration was accepted or not.</returns>
        //[AcceptVerbs("POST", "PUT")]
        [HttpPut]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData("2015-05-25", markType: 1, aliasName: "Account_BusinessAccount")]
        [ActionName("BusinessAccount")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> RequestBusinessAccount([FromBody]SignUpRequestBusiness request)
        {
            return await SignUpBusiness(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Accepts data from user to activate the user.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData("2015-06-26", markType: 1, aliasName: "Account_Activate")]
        [ActionName("Activate")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Activate([FromBody] ActivateUser request)
        {
            var response = await _accountService.Activate(request).ConfigureAwait(false);
            return Request.SystemResponse<string>(response.Status);
        }

        /// <summary>
        /// Accepts information from user to reset the password.
        /// </summary>
        /// <param name="request">The password reset information.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [AllowAnonymous, TemporaryAuthorization]
        [MetaData("2015-06-30", markType: 1, aliasName: "Account_ResetPassword")]
        [ActionName("ResetPassword")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            request.RequestCode = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.RequestCode));
            var responseTask = _accountService.ResetPassword(request);
            var userDeviceInfoDetails = Helper.GetUserDeviceDetails((Request.Properties["MS_HttpContext"] as HttpContextWrapper).Request);
            var response = await responseTask;

            if (response.Status.IsOperationSuccessful())
            {
                var userInfo = response.Data;

                var model = new ResetPasswordInternal
                {
                    FirstName = userInfo.FirstName,
                    Browser = userDeviceInfoDetails.Browser,
                    IpAddress = userDeviceInfoDetails.IpAddress,
                    MacAddress = userDeviceInfoDetails.MacAddress,
                    City = userDeviceInfoDetails.City,
                    CountryCode = userDeviceInfoDetails.CountryCode,
                    Day = DateTime.UtcNow.DayOfWeek.ToString(),
                    Date = DateTime.UtcNow.Date.ToShortDateString(),
                    Time = string.Format("{0:HH:mm:ss tt}", DateTime.UtcNow),
                    Email = userInfo.Email
                };

                var html = ViewRenderer.RenderView("~/Views/Home/OutMail/_ResetPasswordTemplatePartial.cshtml", model);
                Helper.SendMail(model.Email, "Activation Mail", html,
                    bodyImages: new List<string> { "~/Images/iLoop.png" });
            }
            return Request.SystemResponse<string>(response.Status);
        }


        [NonAction]
        private async Task<HttpResponseMessage> SignUpBusiness(SignUpRequestBusiness request)
        {
            var response = await _accountService.SignUpBusiness(request);
            if (response.Status.IsOperationSuccessful())
            {
                var registration = new AccountInternal
                {
                    Email = request.Email,
                    FirstName = request.CompanyName,
                    UserName = request.UserName,
                    ImageServerAddress = SystemConstants.ImageServerAddress.ToString(),
                    UrlRegistrationLink =
                        new Uri(new Uri(SystemConstants.WebUrl.Value), "\\Account\\Activation".Replace("\\", "/")).ToString(),
                    UrlVerificationLink =
                        new Uri(new Uri(SystemConstants.WebUrl.Value), "\\Account\\Activate".Replace("\\", "/")).ToString(),
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