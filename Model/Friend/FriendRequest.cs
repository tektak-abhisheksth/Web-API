using Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Friend
{
    #region Phone Book Contacts
    public class PhoneBookContactsRequest : RequestBase
    {
        [StringLength(36, MinimumLength = 36)]
        [Description("The contact tag which is renewed in the server each time a change in contact occurs.")]
        public string CTag { get; set; }
        [Description("List of contacts to be added.")]

        public List<PhoneBookContact> Add;
        [Description("List of contacts to be removed.")]

        public List<PhoneBookContact> Delete;

        [Description("An instruction which forces removal of all existing contacts pertaining to that device ID and to that user.")]
        public bool Flush { get; set; }
    }

    public class PhoneBookContact : IEquatable<PhoneBookContact>
    {
        [Required, StringLength(2, MinimumLength = 2)]
        [Description("The country code.")]
        public string CountryCode { get; set; }

        [Required, Range(10000000, 999999999999999)]
        [Description("The mobile number.")]
        public long MobileNumber { get; set; }

        public bool Equals(PhoneBookContact x)
        {
            return x.CountryCode.Equals(CountryCode) && x.MobileNumber.Equals(MobileNumber);
        }

        public override int GetHashCode()
        {
            return CountryCode.GetHashCode() ^ MobileNumber.GetHashCode();
        }
    }
    #endregion

    #region Request friendship

    public class FriendRequest : RequestBase
    {
        [Required]
        [Description("The unique system provided ID of the friend.")]
        public string FriendId { get; set; }
    }
    public class FriendshipRequest : FriendRequest
    {
        [Required, Range(0, int.MaxValue)]
        [Description("The individually-unique category ID of the user.")]
        public int CategoryId { get; set; }
    }
    #endregion

    #region Respond to friend request
    public class FriendRespondRequest : FriendshipRequest
    {
        [Required]
        [Description("An indication as to whether or not the request is being accepted.")]
        public bool Accept { get; set; }
    }
    #endregion
}
