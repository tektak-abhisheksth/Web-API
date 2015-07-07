using Model.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Common
{
    public class PaginatedRequest<T> : RequestBase
    {
        [Required]
        [Description("Any information necessary, which affects the paginated response.")]
        public T Data { get; set; }

        [Required, Range(0, int.MaxValue)]
        [Description("The zero-based index of the page being requested.")]
        public int PageIndex { get; set; }

        [Required, Range(0, 100)]
        [Description("The number of results being requested for the page (On some APIs, a value of 0 returns entire list).")]
        public int PageSize { get; set; }
    }

    public class PaginatedResponse<T>
    {
        [Description("The paginated response.")]
        public T Page { get; set; }
        //[Description("The zero-based index of the page being requested.")]
        //public int? PageIndex { get; set; }
        //[Description("The number of results being requested for the page.")]
        //public int? PageSize { get; set; }
        [Description("An indication as to whether or not more results exist.")]
        public bool HasNextPage { get; set; }
    }

    public class PaginatedResponseExtended<T, I> : PaginatedResponse<T>
    {
        [Description("Additional information and/or meta data that supplements the paginated response.")]
        public I Information { get; set; }
    }
}
