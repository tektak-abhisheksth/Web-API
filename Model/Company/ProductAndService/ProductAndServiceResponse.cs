using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Company.ProductAndService
{
    public class ProductAndServiceResponse
    {
        [Description("A unique identifier uniquely representing the entry.")]
        public long CompanyProductAndServiceId { get; set; }

        [Required, StringLength(100, MinimumLength = 5)]
        [Description("The name of product or service.")]
        public string Title { get; set; }

        [Required, StringLength(10000, MinimumLength = 10)]
        [Description("The description of product or service.")]
        public string Description { get; set; }

        [Url]
        [Description("The image of product or service.")]
        public string Image { get; set; }

        [Description("Last updated timestamp.")]
        public DateTime LastChanged { get; set; }
    }
}
