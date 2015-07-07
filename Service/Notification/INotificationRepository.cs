using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Common;
using Model.Notification;
using Model.Types;

namespace TekTak.iLoop.Notification
{
    public interface INotificationRepository
    {
        Task<PaginatedResponse<IEnumerable<NotificationResponse>>> GetNotificationList(NotificationRequest request, int pageIndex, int pageSize, SystemSession session);
        Task<StatusData<int>> GetMessageCount(SystemSession session);
        Task<NotificationCountResponse> GetNotificationCount(List<long> notificationRequestTypes, SystemSession session);
        Task<StatusData<string>> SetNotificationAndRequest(SetNotificationInternal request, SystemSession session);
    }
}
