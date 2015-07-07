using Model.Base;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Category
{
    public class CategoryRequest : RequestBase
    {
        [Required, StringLength(100, MinimumLength = 1), RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "Category name contains invalid character(s).")]
        [Description("The name of the category.")]
        public string Name { get; set; }

        [StringLength(1000, MinimumLength = 1)]
        [Description("An optional description of the category.")]
        public string Description { get; set; }
    }

    public class CategoryUpdateRequest : CategoryRequest
    {
        [Required, Range(1, int.MaxValue)]
        [Description("The individually-unique category ID of the user.")]
        public int CategoryId { get; set; }
    }

    public class CategoryAddRequest : CategoryRequest
    {
        [Description("An optional list of friends to be added to the category.")]
        public List<int> Friends { get; set; }

        //public int CategoryId { get; set; }
    }

    public class CategoryFriends : RequestBase
    {
        [Required, Range(1, int.MaxValue)]
        [Description("The individually-unique category ID of the user.")]
        public int CategoryId { get; set; }

        [Required]
        [Description("The list of friends in the category.")]
        public List<int> Friends { get; set; }
    }

    public class DeleteCategory : RequestBase
    {
        [Required]
        [Description("The individually-unique list of category IDs of the user.")]
        public List<int> CategoryList { get; set; }
    }
}
