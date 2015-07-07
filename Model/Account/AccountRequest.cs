using Model.Attribute;
using Model.Base;
using Model.Types;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Model.Account
{
    //[Validator(typeof(LoginRequestValidator))]
    public class LoginRequest : RequestBaseAnonymous
    {
        [Required]
        [Description("The unique login user name or email address.")]
        public string UserName { get; set; }

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

        [Required, RegularExpression(@"^[A,I,W,a,i,w]$", ErrorMessage = "Allowed values for request type are 'A', 'I' and 'W' or 'a', 'i', and 'w' only.")]
        [Description("The device type indicating the type of the target device (Android, iOS or Web).")]
        public string DeviceType { get; set; }
        [Description("The push code.")]
        public string PushCode { get; set; }

        [IgnoreDataMember]
        public string Ip { get; set; }
    }

    public class SignUpRequestPerson : RequestBaseAnonymous
    {
        [Required, StringLength(20, MinimumLength = 6), RegularExpression("^[a-zA-Z]+[-0-9a-zA-Z.]*[0-9a-zA-Z]*$", ErrorMessage = "Invalid user name.")]
        [Description("The unique login user name.")]
        public string UserName { get; set; }

        [Required, StringLength(20, MinimumLength = 6), RegularExpression(@"^[A-Za-z0-9_\\+-]+(?=.*[A-Za-z0-9._^%$#!~@,-])[\S]*$",
            ErrorMessage = "Invalid password format.")]
        [Description("The password for the user.")]
        public string Password { get; set; }

        [Required, StringLength(40, MinimumLength = 1)]
        [Description("The model name of the device.")]
        public string ModelName { get; set; }

        [Required, StringLength(20, MinimumLength = 12)]
        [Description("The bluetooth ID.")]
        public string BluetoothId { get; set; }

        [Required, StringLength(50, MinimumLength = 2), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only alphabets are allowed in the first name."), DisplayName("First Name")]
        [Description("The first name of the user.")]
        public string FirstName { get; set; }

        [Required, StringLength(50, MinimumLength = 2), RegularExpression(@"^([A-z][A-Za-z]*\s+[A-Za-z]*)|([A-z][A-Za-z]*)$", ErrorMessage = "Only alphabets are allowed in the last name.")]
        [Description("The last name of the user.")]
        public string LastName { get; set; }

        [Required, Range(typeof(DateTime), "1/1/1916", "1/1/2016")]
        [Description("The birth date of the user.")]
        public DateTime DateOfBirth { get; set; }

        // [Required, Range(1, 2)]
        [ValidEnum]
        [Description("The gender of the user.")]
        public SystemGender Gender { get; set; }

        [Required, EmailAddress, RegularExpression("^[A-Za-z0-9_\\+-]+(\\.[A-Za-z0-9_\\+-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9-]+)*\\.([A-Za-z]{2,4})$", ErrorMessage = "Invalid email address format.")]
        [Description("The email address of the user.")]
        public string Email { get; set; }

        [Required, StringLength(2, MinimumLength = 2)]
        [Description("The country code of the user.")]
        public string CountryCode { get; set; }

        ///// <summary>
        ///// The offset to determine user's time zone.
        ///// </summary>
        //[Required]
        //public DateTimeOffset Offset { get; set; }
    }

    public class ForgotPasswordRequest : RequestBaseAnonymous
    {
        [Required]
        [Description("The unique login user name.")]
        public string UserName { get; set; }
    }
}