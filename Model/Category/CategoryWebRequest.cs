using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Category
{
    public class CopyCategory : CategoryFriends
    {
        [Required]
        [Description("The individually-unique list of category IDs of the user.")]
        public List<int> TargetCategories { get; set; }

        [Required]
        [Description("An indication as to whether or not remove given contacts from source category.")]
        public bool RemoveFromSource { get; set; }
    }

    public class FriendsInCategoryRequest
    {
        [Required, Range(1, int.MaxValue)]
        [Description("The individually-unique category ID of the user.")]
        public int CategoryId { get; set; }

        [Required]
        [Description("An indication as to whether or not revert the result, i.e., return all other categories except the one specified.")]
        public bool InvertCategorySearch { get; set; }
    }
}
