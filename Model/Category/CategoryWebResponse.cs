
using Model.Base;
using System.ComponentModel;

namespace Model.Category
{
    public class FriendsInCategoryResponse : UserInformationBaseExtendedResponse
    {
        [Description("The email of the user.")]
        public string Email { get; set; }
    }
}
