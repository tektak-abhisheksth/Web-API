using BLL.Notification;
using Model.Attribute;
using Model.Common;
using Model.Notification;
using Model.Types;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Utility;

namespace API.Controllers.Notification
{
    /// <summary>
    /// Provides APIs to handle requests related to notification.
    /// </summary>
    [MetaData]
    public partial class NotificationController : ApiController
    {
        private readonly INotificationService _service;

        /// <summary>
        ///  Provides APIs to handle requests related to notification.
        /// </summary>
        /// <param name="notificationService"></param>
        public NotificationController(INotificationService notificationService)
        {
            _service = notificationService;
        }
        /// <summary>
        /// Gets notifications and requests.
        /// </summary>
        /// <param name="request">The paginated request body.</param>
        /// <returns>Notifications' and requests' list.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Notification_Post")]
        [ResponseType(typeof(PaginatedResponse<IEnumerable<NotificationResponse>>))]
        public async Task<HttpResponseMessage> Post([FromBody] PaginatedRequest<NotificationRequest> request)
        {
            var response = await _service.GetNotificationList(request.Data, request.PageIndex, request.PageSize, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Gets the counts of notifications and requests.
        /// </summary>
        /// <param name="request">List of notification and request type Ids.</param>
        /// <returns>Notifications' and requests' counts.</returns>
        [MetaData("2015-01-29", markType: 3, aliasName: "Notification_Count")]
        [ActionName("Count")]
        [ResponseType(typeof(NotificationCountResponse))]
        public async Task<HttpResponseMessage> Post([FromBody] SingleData<List<long>> request)
        {
            var response = await _service.GetNotificationCount(request.Data, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Gets the count of the unseen message instances.
        /// </summary>
        /// <returns>Messages' count.</returns>
        [HttpGet]
        [MetaData("2015-03-31", markType: 3, aliasName: "Notification_MessageCount")]
        [ActionName("MessageCount")]
        [ResponseType(typeof(int))]
        public async Task<HttpResponseMessage> GetMessageCount()
        {
            var response = await _service.GetMessageCount(Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, new { response.Data });
        }
    }
}
