using System.Threading.Tasks;
using DAL.Account;
using DAL.Category;
using DAL.Data;
using DAL.Friend;
using DAL.Inbox;
using DAL.Media;
using DAL.Notification;
using DAL.Profile;
using DAL.Search;
using DAL.Setting;

namespace DAL.DbEntity
{
    public interface IUnitOfWork
    {
        IAccountRepository Account { get; }
        IDataRequestsRepository DataRequests { get; }
        IProfileRepository Profile { get; }
        IMediaRepository Media { get; }
        ICategoryRepository Category { get; }
        IFriendRepository Friend { get; }
        ISettingRepository Setting { get; }
        ISearchRepository Search { get; }
        INotificationRepository Notification { get; }
        IInboxRepository Inbox { get; }

        int Commit();
        Task<int> CommitAsync();
    }
}
