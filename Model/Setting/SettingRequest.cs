using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Model.Base;

namespace Model.Setting
{
    public class ActiveDeviceRequest : RequestBase
    {
        [Description("The list of active devices.")]
        public List<string> DeviceIds { get; set; }
    }

    public class UserSettingRequest : RequestBase
    {
        [Required]
        [Description("The individually-unique type ID representing a setting.")]
        public int SettingTypeId { get; set; }

        //[ValidEnum]
        [RegularExpression(@"^\d*$", ErrorMessage = "Invalid value format.")]
        [Description("A numeric value that has special meaning for each type of setting.")]
        public int Value { get; set; }

        [Description("An ID referring to a logical grouping of settings. Required and used by the server only.")]
        public int SettingGroupId { get; set; }

        [Description("List of contacts or categories of the user (when selecting custom list of contacts or categories).")]
        public List<int> Entries { get; set; }
    }

    public class ChangePasswordRequest : RequestBase
    {
        /// <summary>
        /// The current password of the user.
        /// </summary>
        [Required, StringLength(20, MinimumLength = 6), RegularExpression(@"^[A-Za-z0-9_\\+-]+(?=.*[A-Za-z0-9._^%$#!~@,-])[\S]*$", ErrorMessage = "Invalid password format.")]
        [Description("The existing password of the user.")]
        public string Password { get; set; }

        /// <summary>
        /// The new password of the user.
        /// </summary>
        [Required, StringLength(20, MinimumLength = 6), RegularExpression(@"^[A-Za-z0-9_\\+-]+(?=.*[A-Za-z0-9._^%$#!~@,-])[\S]*$", ErrorMessage = "Invalid password format.")]
        [Description("The new password of the user.")]
        public string NewPassword { get; set; }
    }

    public class SignOut : RequestBase
    {
        [IgnoreDataMember]
        public string UserName { get; set; }

        [Description("The system-generated unique login token which is part of the encrypted token required for logged-in requests.")]
        public string Token { get; set; }
    }
}
