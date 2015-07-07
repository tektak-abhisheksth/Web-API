using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Model.Base;

namespace Model.Common
{
    public class SingleData<T> : RequestBase
    {
        [Required]
        [Description("The data for the request.")]
        public T Data { get; set; }
    }

    public class SingleDataAnonymous<T> : RequestBaseAnonymous
    {
        [Required]
        [Description("The data for the request.")]
        public T Data { get; set; }
    }
}
