using System.Collections.Generic;
using System.ComponentModel;

namespace Model.Setting
{
    public class SettingResponse
    {
        [Description("The time last activity was registered. Note: This redundant property is subject to potential future removal.")]
        public long LastActivity { get; set; }

        [Description("The unique device ID of the user.")]
        public string DeviceId { get; set; }

        [Description("The model name of the device.")]
        public string ModelName { get; set; }

        [Description("The system-generated unique login token which is part of the encrypted token required for logged-in requests.")]
        public string Token { get; set; }

        [Description("The device type.")]
        public string DeviceType { get; set; }
    }

    public class UserSettingBase
    {
        [Description("The individually-unique type ID representing a setting.")]
        public int SettingTypeId { get; set; }

        [Description("A numeric value that has special meaning for each type of setting.")]
        public int Value { get; set; }
    }


    public class UserSettingResponse : UserSettingBase
    {
        [Description("The name of the setting.")]
        public string Name { get; set; }

        [Description("The description of the setting.")]
        public string Description { get; set; }

        ////[Description("An ID referring to a logical grouping of settings. Required and used by the server only.")]
        ////public int SettingGroupId { get; set; }

        [Description("List of contacts or categories of the user (when selecting custom list of contacts or categories).")]
        public IEnumerable<int> Entries { get; set; }

        [Description("The logical grouping of relevant settings depending upon there type.")]
        public int SettingGroup { get; set; }

        [Description("The logical grouping of relevant settings depending upon there placement.")]
        public int DisplayGroup { get; set; }

        [Description("The logical grouping of relevant settings depending upon there display order.")]
        public int DisplayOrder { get; set; }

        [Description("The uninitialized value of the setting.")]
        public int DefaultValue { get; set; }
    }
}
