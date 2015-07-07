using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.CountryInfo
{
    public class UserCity
    {
        [Required, Range(1, int.MaxValue)]
        [Description("The unique system provided ID of the city.")]
        public int Id { get; set; }

        [Required]
        [Description("The name of the city.")]
        public string Name { get; set; }

        [Required, StringLength(2, MinimumLength = 2)]
        [Description("The country code of the country where the city resides.")]
        public string CountryCode { get; set; }

        [Description("The name of the country where the city resides.")]
        public string CountryName { get; set; }

        //[Required, Range(-90, 90)]
        //[Description("The latitude.")]
        //public double Latitude { get; set; }

        //[Required, Range(-180, 180)]
        //[Description("The longitude.")]
        //public double Longitude { get; set; }
    }
}
