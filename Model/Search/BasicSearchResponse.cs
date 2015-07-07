using Model.Base;
using Model.Types;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model.Search
{
    public class BasicSearchResponse : UserInformationBaseExtendedResponse
    {
        [Description("The country code.")]
        public string CountryCode { get; set; }

        [Description("The mobile number.")]
        public string MobileNumber { get; set; }

        [Description("The current work-availability status of the user.")]
        public SystemUserAvailabilityCode StatusTypeId { get; set; }

        [Description("The friendship status with the user.")]
        public SystemFriendshipStatus FriendshipStatus { get; set; }

        [Description("The setting-driven indication as to whether or not the friend allows the user to send messages.")]
        public bool CanMessage { get; set; }

        [Description("The setting-driven indication as to whether or not the friend allows the user to forward messages.")]
        public bool AllowMessageForwarding { get; set; }

        [Description("The setting-driven indication as to whether or not the friend allows the user to send friendship request.")]
        public bool CanReceiveConnectionRequest { get; set; }

        [Description("The setting-driven indication as to whether or not the friend allows the user to add in chat groups.")]
        public bool AllowAddingInChatGroup { get; set; }

        [Description("An indication that the contact also matches the phone book contacts as synced with the server for the device.")]
        public bool IsFromPhoneBook { get; set; }

        [Description("The number of common approved friends with the user.")]
        public int MutualFriends { get; set; }
    }

    public class UserSearchResponse
    {
        public IEnumerable<UserInformationBaseExtendedResponse> User { get; set; }
        public string Cursor { get; set; }
        public int Limit { get; set; }
        public long Count { get; set; }
    }
}
