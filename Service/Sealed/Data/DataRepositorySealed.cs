
using TekTak.iLoop.Data;

namespace TekTak.iLoop.Sealed.Data
{
    public sealed class DataRepositorySealed : DataRepository, IDataRepositorySealed
    {
        public DataRepositorySealed(Services client)
            : base(client)
        { }
    }
}
