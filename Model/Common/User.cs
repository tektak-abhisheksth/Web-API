using System.ComponentModel;

namespace Model.Common
{
    public class UserResponse
    {
        [Description("The unique system provided ID of the user.")]
        public int UserId { get; set; }

        [Description("The unique system provided name of the user.")]
        public string UserName { get; set; }

        [Description("The name of the user.")]
        public string Name { get; set; }

        [Description("The picture of the user.")]
        public string Picture { get; set; }
    }
}
