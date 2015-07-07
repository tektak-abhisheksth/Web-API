using System.ComponentModel;
using Model.Types;

namespace Model.Common
{
    public class StatusData<T>
    {
        [Description("The Status of the operation.")]
        public SystemDbStatus Status { get; set; }

        [Description("The data to be supplied to the client.")]
        public T Data { get; set; }

        [Description("Any sub status code further elaborating the Status code.")]
        public int SubStatus { get; set; }

        [Description("Error description as returned by persistent storage.")]
        public string Message { get; set; }
    }
}