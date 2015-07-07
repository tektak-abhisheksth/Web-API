using DAL.DbEntity;
using Model.Common;
using Model.Notification;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TekTak.iLoop.UOW.IUnitOfWork _jUnitOfWork;

        public NotificationService(IUnitOfWork unitOfWork, TekTak.iLoop.UOW.IUnitOfWork jUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _jUnitOfWork = jUnitOfWork;
        }

        public Task<PaginatedResponse<IEnumerable<NotificationResponse>>> GetNotificationList(NotificationRequest request, int pageIndex,
            int pageSize, SystemSession session)
        {
            return _jUnitOfWork.Notification.GetNotificationList(request, pageIndex, pageSize, session);
        }

        public Task<NotificationCountResponse> GetNotificationCount(List<long> notificationRequestTypes, SystemSession session)
        {
            // return _unitOfWork.Notification.GetNotificationCount(userId, notificationRequestTypes);
            return _jUnitOfWork.Notification.GetNotificationCount(notificationRequestTypes, session);
        }

        public Task<StatusData<int>> GetMessageCount(SystemSession session)
        {
            return _jUnitOfWork.Notification.GetMessageCount(session);
        }
    }
}
