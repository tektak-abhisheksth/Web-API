using TekTak.iLoop.Sealed.Account;
using TekTak.iLoop.Sealed.Category;
using TekTak.iLoop.Sealed.Chat;
using TekTak.iLoop.Sealed.Data;
using TekTak.iLoop.Sealed.Friend;
using TekTak.iLoop.Sealed.Inbox;
using TekTak.iLoop.Sealed.Media;
using TekTak.iLoop.Sealed.Notification;
using TekTak.iLoop.Sealed.Profile;
using TekTak.iLoop.Sealed.Search;
using TekTak.iLoop.Sealed.Setting;

namespace TekTak.iLoop.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ServiceClientAdapter _adapter;
        private readonly Services _client;

        public UnitOfWork()
        {
            _adapter = new ServiceClientAdapter();
            _client = _adapter.Connect();
        }

        #region Services accessible via UnitOfWork

        public IAccountRepositorySealed Account { get { return new AccountRepositorySealed(_client); } }
        public ICategoryRepositorySealed Category { get { return new CategoryRepositorySealed(_client); } }
        public IChatRepositorySealed Chat { get { return new ChatRepositorySealed(_client); } }
        public IDataRepositorySealed Data { get { return new DataRepositorySealed(_client); } }
        public IFriendRepositorySealed Friend { get { return new FriendRepositorySealed(_client); } }
        public IInboxRepositorySealed Inbox { get { return new InboxRepositorySealed(_client); } }
        public IMediaRepositorySealed Media { get { return new MediaRepositorySealed(_client); } }
        public INotificationRepositorySealed Notification { get { return new NotificationRepositorySealed(_client); } }
        public IProfileRepositorySealed Profile { get { return new ProfileRepositorySealed(_client); } }
        public IProfileBusinessRepositorySealed ProfileBusiness { get { return new ProfileBusinessRepositorySealed(_client); } }
        public IProfilePersonalRepositorySealed ProfilePersonal { get { return new ProfilePersonalRepositorySealed(_client); } }
        public ISettingRepositorySealed Setting { get { return new SettingRepositorySealed(_client); } }
        public ISearchRepositorySealed Search { get { return new SearchRepositorySealed(_client); } }
        #endregion

        public Services GetServices()
        {
            return _client;
        }

        public void Dispose()
        {
            _client.Dispose();
            _adapter.Dispose();
        }
    }
}
