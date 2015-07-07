using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Model.Types;

namespace Model.Base
{
    /// <summary>
    /// The final response being sent to the client.
    /// </summary>
    /// <typeparam name="T">The data being returned.</typeparam>
    public class SystemResponse<T> where T : class
    {
        [Required]
        [Description("The status of the operation.")]
        public SystemStatusCode Status { get; set; }

        [Description("The data to be returned to the client.")]
        public T Data { get; set; }

        [Description("Any encountered error during the operation.")]
        public string Error { get; set; }

        [Description("The sub status code, representing code providing contextual elaborative reason on the basis of which further actions might be taken.")]
        public int SubStatus { get; set; }
    }
}