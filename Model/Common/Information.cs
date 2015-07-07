using System.ComponentModel;

namespace Model.Common
{
    public class InformationResponse<T, I>
    {
        [Description("The main response.")]
        public T Page { get; set; }

        [Description("Additional information and/or meta data that supplements the response.")]
        public I Information { get; set; }
    }
}
