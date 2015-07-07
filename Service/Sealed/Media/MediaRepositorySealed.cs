using TekTak.iLoop.Media;

namespace TekTak.iLoop.Sealed.Media
{
    public sealed class MediaRepositorySealed : MediaRepository, IMediaRepositorySealed
    {
        public MediaRepositorySealed(Services client)
            : base(client)
        { }
    }
}
