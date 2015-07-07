using Model.Base;
using Model.Types;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Model.Friend
{
    public class FriendInformationResponse : UserInformationBaseExtendedResponse
    {
        [Description("The country code of the user.")]
        public string CountryCode { get; set; }

        [Description("The mobile number of the user.")]
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

        [Description("The address of the user.")]
        public string Address { get; set; }

        [Description("The email of the user.")]
        public string Email { get; set; }

        [IgnoreDataMember]
        public bool IsFromPhoneBook { get; set; }
    }

    public class FriendInformationCategorizedResponse
    {
        [Description("The contact tag which is renewed in the server each time a change in contact occurs.")]
        public string CTag { get; set; }

        [Description("The list of contacts that match the phone book contacts as synced with the server for the device.")]
        public IEnumerable<FriendInformationResponse> MobileContacts { get; set; }

        [Description("The list of contacts that don't match the phone book contacts as synced with the server for the device but are connected via web.")]
        public IEnumerable<FriendInformationResponse> NonMobileContacts { get; set; }
    }
}
