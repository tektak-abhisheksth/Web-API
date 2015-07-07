using Model.Types;
using System.ComponentModel;

namespace Model.Base
{
    public class UserInformationBaseResponse
    {
        [Description("The unique user ID of the user.")]
        public int UserId { get; set; }

        [Description("The first name of the user.")]
        public string FirstName { get; set; }

        [Description("The last name of the user.")]
        public string LastName { get; set; }

        [Description("The path and name of the user's image.")]
        public string Image { get; set; }
    }

    public class UserInformationBaseExtendedResponse : UserInformationBaseResponse
    {
        [Description("The unique login user name.")]
        public string UserName { get; set; }

        [Description("Any additional information about the user.")]
        public string Title { get; set; }

        [Description("The type ID specifying the type of the user.")]
        public SystemUserType UserTypeId { get; set; }
    }
}
