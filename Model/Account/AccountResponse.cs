using Model.Types;
using System;
using System.ComponentModel;

namespace Model.Account
{
    public class AccountResponse
    {
        [Description("A unique machine-specific temporary code that guarantees that the payload being received by the server on certain requests (non-logged requests) is genuine.")]
        public string TempToken { get; set; }

        [Description("The time when the token was generated.")]
        public DateTime GeneratedTimeUtc { get; set; }

        [Description("The time after which the token becomes invalid.")]
        public DateTime ExpiryTimeUtc { get; set; }
    }

    public class LoginResponse
    {
        [Description("The unique user ID of the user.")]
        public int UserId { get; set; }

        [Description("The unique user name of the user.")]
        public string UserName { get; set; }

        [Description("The type ID specifying the type of the user.")]
        public SystemUserType UserTypeId { get; set; }

        [Description("The system-generated unique login token which is part of the encrypted token required for logged-in requests.")]
        public string Token { get; set; }

        [Description("The authorization token to be sent for each closed-API calls.")]
        public string AuthorizationToken { get; set; }
    }

    public class AccountInternal
    {
        public int UserId { get; set; }
        public string UserGuid { get; set; }
        public string UrlRegistrationLink { get; set; }
        public string UrlVerificationLink { get; set; }
        public string ImageServerAddress { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}