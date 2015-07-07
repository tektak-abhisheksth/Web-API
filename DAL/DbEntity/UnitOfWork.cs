using System;
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
using Entity;

namespace DAL.DbEntity
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private iLoopEntity Context { get; set; }

        public UnitOfWork()
        {
            CreateContext();
        }

        #region Models accessible via UnitOfWork

        public IAccountRepository Account { get { return new AccountRepository(Context); } }

        public IDataRequestsRepository DataRequests { get { return new DataRequestsRepository(Context); } }

        public IProfileRepository Profile { get { return new ProfileRepository(Context); } }

        public IMediaRepository Media { get { return new MediaRepository(Context); } }

        public ICategoryRepository Category { get { return new CategoryRepository(Context); } }

        public IFriendRepository Friend { get { return new FriendRepository(Context); } }

        public ISettingRepository Setting { get { return new SettingRepository(Context); } }

        public ISearchRepository Search { get { return new SearchRepository(Context); } }

        public INotificationRepository Notification { get { return new NotificationRepository(Context); } }

        public IInboxRepository Inbox { get { return new InboxRepository(Context); } }
        #endregion

        public int Commit()
        {
            return Context.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return Context.SaveChangesAsync();
        }

        private void CreateContext()
        {
            Context = new iLoopEntity();
        }

        public iLoopEntity GetContext()
        {
            return Context;
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
