using Model.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Account
{
    public class SignUpRequestBusiness : RequestBaseAnonymous
    {
        [Required, StringLength(20, MinimumLength = 6), RegularExpression("^[a-zA-Z]+[-0-9a-zA-Z.]*[0-9a-zA-Z]*$", ErrorMessage = "Invalid user name.")]
        [Description("The unique login user name.")]
        public string UserName { get; set; }

        /// <summary>
        /// The password of the user.
        /// </summary>
        [Required, StringLength(20, MinimumLength = 6), RegularExpression(@"^[A-Za-z0-9_\\+-]+(?=.*[A-Za-z0-9._^%$#!~@,-])[\S]*$",
            ErrorMessage = "Invalid password format.")]
        [Description("The password of the user.")]
        public string Password { get; set; }

        [Required, StringLength(40, MinimumLength = 1)]
        [Description("The model name of the device.")]
        public string ModelName { get; set; }

        [Required, StringLength(20, MinimumLength = 12)]
        [Description("The bluetooth ID.")]
        public string BluetoothId { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        [Description("The display name of the user.")]
        public string CompanyName { get; set; }

        [Required, Range(typeof(DateTime), "1/1/1900", "1/1/2017")]
        [Description("The established date of the company.")]
        public DateTime EstablishedDate { get; set; }


        [Required, EmailAddress, RegularExpression("^[A-Za-z0-9_\\+-]+(\\.[A-Za-z0-9_\\+-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9-]+)*\\.([A-Za-z]{2,4})$")]
        [Description("The email address of the user.")]
        public string Email { get; set; }

        [Required, StringLength(2, MinimumLength = 2)]
        [Description("The country code of the user.")]
        public string CountryCode { get; set; }

        [Required]
        [Description("The type of industry.")]
        public int CompanyType { get; set; }

        [Required]
        [Description("The ownership type of the company.")]
        public int OwnershipTypeId { get; set; }

        [RegularExpression(@"^(([\w]+:)?//)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$")]
        [Description("The website address of the company.")]
        public string CompanyUrl { get; set; }

    }

    public class ActivateUser : RequestBaseAnonymous
    {
        [Required, StringLength(20, MinimumLength = 6), RegularExpression("^[a-zA-Z]+[-0-9a-zA-Z.]*[0-9a-zA-Z]*$", ErrorMessage = "Invalid user name.")]
        [Description("The unique login user name.")]
        public string UserName { get; set; }

        [Required]
        [Description("The system generated unique GUID for the user.")]
        public string UserGuid { get; set; }
    }

    public class ResetPasswordRequest : RequestBaseAnonymous
    {
        [Required, StringLength(20, MinimumLength = 6), RegularExpression("^[a-zA-Z]+[-0-9a-zA-Z.]*[0-9a-zA-Z]*$", ErrorMessage = "Invalid user name.")]
        [Description("The unique login user name.")]
        public string UserName { get; set; }

        [Required]
        [Description("The request token that was sent in the mail for Forgot Password.")]
        public string RequestCode { get; set; }

        [Required, StringLength(20, MinimumLength = 6), RegularExpression(@"^[A-Za-z0-9_\\+-]+(?=.*[A-Za-z0-9._^%$#!~@,-])[\S]*$",
           ErrorMessage = "Invalid password format.")]
        [Description("The new password of the user.")]
        public string Password { get; set; }
    }

    public class ResetPasswordInternal
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Browser { get; set; }
        public string MacAddress { get; set; }
        public string IpAddress { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string Day { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}