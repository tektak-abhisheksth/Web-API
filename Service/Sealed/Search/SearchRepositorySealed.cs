using TekTak.iLoop.Search;

namespace TekTak.iLoop.Sealed.Search
{
    public class SearchRepositorySealed : SearchRepository, ISearchRepositorySealed
    {
        public SearchRepositorySealed(Services client)
            : base(client)
        { }
    }
}
