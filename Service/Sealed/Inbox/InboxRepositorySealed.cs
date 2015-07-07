using TekTak.iLoop.Inbox;

namespace TekTak.iLoop.Sealed.Inbox
{
    public sealed class InboxRepositorySealed : InboxRepository, IInboxRepositorySealed
    {

        public InboxRepositorySealed(Services client)
            : base(client)
        { }
    }
}
