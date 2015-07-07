using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.DbEntity;
using Model.Common;
using Model.Notification;

namespace DAL.Notification
{
    public interface INotificationRepository : IGenericRepository<Entity.Notification>
    {
        Task<PaginatedResponse<IEnumerable<NotificationResponse>>> GetNotificationList(int userId, int pageIndex, int pageSize);
        Task<NotificationCountResponse> GetNotificationCount(int userId, List<long> notificationRequestTypes);
    }
}
