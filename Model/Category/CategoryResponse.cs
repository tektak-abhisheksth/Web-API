using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Category
{
    public class CategoryResponse
    {
        [Required]
        [Description("The individually-unique category ID of the user.")]
        public int CategoryId { get; set; }

        [Required, StringLength(100, MinimumLength = 1)]
        [Description("The name of the category.")]
        public string Name { get; set; }

        [StringLength(1000, MinimumLength = 1)]
        [Description("An optional description of the category.")]
        public string Description { get; set; }

        [Description("An indication as to whether or not the category is a system-generated category.")]
        public bool IsSystemDefaultCategory { get; set; }

        [Description("The list of friends in the category.")]
        public IEnumerable<int> Friends { get; set; }
    }
}
