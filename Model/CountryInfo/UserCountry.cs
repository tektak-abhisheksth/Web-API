using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.CountryInfo
{
    public class UserCountry
    {
        [Required, StringLength(2, MinimumLength = 2)]
        [Description("The country code.")]
        public string CountryCode { get; set; }

        [Required]
        [Description("The country name.")]
        public string Name { get; set; }

        [Required]
        [Description("The country zip code.")]
        public string ZipCode { get; set; }
    }
}
