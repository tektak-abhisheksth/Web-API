using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Base
{
    public class RequestBase : RequestBaseAnonymous
    {
        [Required, Range(1, int.MaxValue)]
        [Description("The unique system provided ID of the user.")]
        public int UserId { get; set; }
    }

    public class RequestBaseAnonymous
    {
        [Required, StringLength(200, MinimumLength = 10)]
        [Description("The unique device ID of the user.")]
        public string DeviceId { get; set; }
    }
}