using TekTak.iLoop.Notification;

namespace TekTak.iLoop.Sealed.Notification
{
    public sealed class NotificationRepositorySealed : NotificationRepository, INotificationRepositorySealed
    {
        public NotificationRepositorySealed(Services client)
            : base(client)
        { }
    }
}
