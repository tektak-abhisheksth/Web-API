using Model.Base;
using System.ComponentModel;

namespace Model.Search
{
    public class DataRequest : RequestBaseAnonymous
    {
        [Description("The optional identity of the data to be returned.")]
        public int? Id { get; set; }

        [Description("The optional search key. Ignored when ID is passed.")]
        public string SearchString { get; set; }
    }

    public class UserSearchRequest : RequestBase
    {
        public string Query { get; set; }
        public string Cursor { get; set; }
        public int Limit { get; set; }
    }
}
