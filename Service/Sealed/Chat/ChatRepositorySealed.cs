using TekTak.iLoop.Chat;

namespace TekTak.iLoop.Sealed.Chat
{
    public sealed class ChatRepositorySealed : ChatRepository, IChatRepositorySealed
    {
        public ChatRepositorySealed(Services client)
            : base(client)
        { }
    }
}
