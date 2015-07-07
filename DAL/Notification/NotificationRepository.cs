using DAL.DbEntity;
using Entity;
using Model.Common;
using Model.Notification;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Notification
{
    public class NotificationRepository : GenericRepository<Entity.Notification>, INotificationRepository
    {
        public NotificationRepository(iLoopEntity context) : base(context) { }

        public async Task<PaginatedResponse<IEnumerable<NotificationResponse>>> GetNotificationList(int userId, int pageIndex, int pageSize)
        {
            var hasNextPage = new ObjectParameter("HASNEXTPAGE", false);
            var page = new PaginatedResponse<IEnumerable<NotificationResponse>>
            {
                Page = await Task.Factory.StartNew(() => Context.PROC_GET_NOTIFANDREQ_MOB(userId, pageIndex, pageSize, hasNextPage)
                    .Select(x => new NotificationResponse
                    {
                        NotificationId = x.NotificationId,
                        NotifierId = x.UserId,
                        NotifierUserName = x.UserName,
                        ReadDate = x.ReadDate,
                        TypeId = x.TypeId,
                        SentDate = x.SentDate
                    }).ToList()).ConfigureAwait(false)
            };
            page.HasNextPage = Convert.ToBoolean(hasNextPage.Value);
            page.Page = page.Page.Take(pageSize);
            return page;
        }

        public async Task<NotificationCountResponse> GetNotificationCount(int userId, List<long> notificationRequestTypes)
        {
            var hasNoNotifications = notificationRequestTypes == null || !notificationRequestTypes.Any();
            var notificationCount = await Task.Factory.StartNew(() => Context.Notifications.Count(x => (x.UserID == userId && x.ReadDate == null && (hasNoNotifications || notificationRequestTypes.Contains(x.NotificationTypeID))))).ConfigureAwait(false);
            var requestCount = await Task.Factory.StartNew(() => Context.Requests.Count(x => (x.RequestedToID == userId && x.ReadDate == null && (hasNoNotifications || notificationRequestTypes.Contains(x.RequestTypeID))))).ConfigureAwait(false);

            return new NotificationCountResponse { NotificationCount = notificationCount, RequestCount = requestCount };
        }
    }
}
