using Model.Base;
using Model.Types;
using System.ComponentModel;

namespace Model.Friend
{
    public class WebFriendInformationResponse : UserInformationBaseExtendedResponse
    {

        [Description("The current work-availability status of the user.")]
        public SystemUserAvailabilityCode StatusTypeId { get; set; }

        [Description("The mobile number of the user.")]
        public string MobileNumber { get; set; }

        [Description("The friendship status with the user.")]
        public SystemFriendshipStatus FriendshipStatus { get; set; }

        [Description("The number of mutual friend(s) of the user.")]
        public int MutualFriend { get; set; }

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
    }
}
