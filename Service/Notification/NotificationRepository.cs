using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Common;
using Model.Notification;
using Model.Types;
using TekTak.iLoop.Helper;
using TekTak.iLoop.Kauwa;

namespace TekTak.iLoop.Notification
{
    public class NotificationRepository : INotificationRepository
    {
        protected readonly Services Client;

        public NotificationRepository(Services client)
        {
            Client = client;
        }

        public async Task<PaginatedResponse<IEnumerable<NotificationResponse>>> GetNotificationList(NotificationRequest request, int pageIndex, int pageSize, SystemSession session)
        {
            var serviceRequest = new NotificationMob { TypeId = request.FilterType, RequestDirection = request.RequestDirection, NrTypes = request.NotificationRequestTypes, DisplayOnlyId = (int)request.NotificationRequestId.GetValueOrDefault(), SetAsRead = true, PageIndex = pageIndex, PageSize = pageSize, UserId = session.UserId, UserName = session.UserName };
            var response = await Task.Factory.StartNew(() => Client.UserService.getAllNotifications(serviceRequest, session.GetSession())).ConfigureAwait(false);
            var result =
                new PaginatedResponse<IEnumerable<NotificationResponse>>
                {
                    HasNextPage = response.HasNextPage,
                    Page =
                        response.Notifications.Select(
                            x =>
                                new NotificationResponse
                                {
                                    NotificationId = x.NotificationId,
                                    NotifierId = x.UserId,
                                    NotifierUserName = x.UserName,
                                    ReadDate = Convert.ToDateTime(x.ReadDate),
                                    SentDate = Convert.ToDateTime(x.SentDate),
                                    TypeId = x.TypeId,
                                    Type = x.Type,
                                    Name = x.Name,
                                    Picture = x.PictureUrl,
                                    Title = x.Title,
                                    Notification = x.Notification,
                                    MutualFriendId = x.MutualFrdId,
                                    MutualFriendName = x.MutualFrdName,
                                    MutualFriendCount = x.MutualFrdCount,
                                    GroupId = x.GroupId,
                                    GroupName = x.GroupName,
                                    EventId = x.EventId,
                                    EventName = x.EventName,
                                    ClickedDate = Convert.ToDateTime(x.ClickDate)
                                })
                };

            return result;
        }

        public async Task<NotificationCountResponse> GetNotificationCount(List<long> notificationRequestTypes, SystemSession session)
        {
            var serviceRequest = string.Join(",", notificationRequestTypes);
            var response = await Task.Factory.StartNew(() => Client.UserService.getNotificationRequestCount(serviceRequest, session.GetSession())).ConfigureAwait(false);
            return new NotificationCountResponse
            {
                NotificationCount = response.NotificationCount,
                RequestCount = response.RequestCount
            };
        }

        public async Task<StatusData<int>> GetMessageCount(SystemSession session)
        {
            var result = new StatusData<int>();
            var response = await Task.Factory.StartNew(() => Client.NotificationService.messageCount(session.GetSession())).ConfigureAwait(false);
            result.Data = response;
            return result;
        }

        public async Task<StatusData<string>> SetNotificationAndRequest(SetNotificationInternal request, SystemSession session)
        {
            var response = (await Task.Factory.StartNew(() => Client.NotificationService.setNotificationAndRequest(session.UserId, string.Join(",", request.NotificationId), request.FilterType, request.IsClicked, session.GetSession())).ConfigureAwait(false)).GetStatusData<string>();
            return response;
        }
    }
}
