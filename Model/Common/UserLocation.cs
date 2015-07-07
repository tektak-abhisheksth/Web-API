using Model.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Common
{
    public class GeoLocationRequest : RequestBase
    {
        [Required, Range(-90, 90)]
        [Description("The latitude.")]
        public double Latitude { get; set; }

        [Required, Range(-180, 180)]
        [Description("The longitude.")]
        public double Longitude { get; set; }
    }

    public class UserLocationRequest : GeoLocationRequest
    {
        [Required]
        [Description("The offset information.")]
        public DateTimeOffset Offset { get; set; }
    }
}
