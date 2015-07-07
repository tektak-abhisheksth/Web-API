using Model.Base;
using Model.Types;
using System;
using System.ComponentModel;

namespace Model.Profile
{
    public class ViewerDetailResponse : UserInformationBaseExtendedResponse
    {
        [Description("The time the profile was last viewed by the viewer.")]
        public DateTime? ViewedDate { get; set; }

        [Description("The friendship status with the viewer.")]
        public SystemFriendshipStatus FriendshipStatus { get; set; }

        [Description("An indication as to whether or not this view information has been seen by current user.")]
        public bool Observed { get; set; }
    }

    public class ViewerPanelResponse : UserInformationBaseExtendedResponse
    {
        [Description("The time the profile was last viewed by the viewer.")]
        public DateTime? ViewedDate { get; set; }

        [Description("The viewer's count.")]
        public int ViewerCount { get; set; }

        [Description("The position of the viewer.")]
        public string Position { get; set; }

        [Description("An indication as to whether or not this view information has been seen by current user.")]
        public bool Observed { get; set; }
    }

    public class ViewSummaryResponse
    {
        [Description("The unique key representing the position/skill against which profile view was made.")]
        public int TypeId { get; set; }

        [Description("The name of the position/skill.")]
        public string Name { get; set; }

        [Description("The total views for the given position/skill.")]
        public int ViewCount { get; set; }

        [Description("The new views for the given position/skill.")]
        public int NewViewCount { get; set; }

        [Description("The time the last view was made.")]
        public DateTime LastViewed { get; set; }
    }
}
