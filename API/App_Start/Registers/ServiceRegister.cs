using Autofac;
using BLL.Account;
using BLL.Category;
using BLL.Chat;
using BLL.Data;
using BLL.Friend;
using BLL.Inbox;
using BLL.Media;
using BLL.Notification;
using BLL.Profile;
using BLL.Search;
using BLL.Setting;

namespace API.Registers
{
    public static class ServiceRegister
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<DataRequestService>().As<IDataService>();
            builder.RegisterType<ProfileService>().As<IProfileService>();
            builder.RegisterType<ProfilePersonalService>().As<IProfilePersonalService>();
            builder.RegisterType<ProfileBusinessService>().As<IProfileBusinessService>();
            builder.RegisterType<MediaService>().As<IMediaService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<FriendService>().As<IFriendService>();
            builder.RegisterType<SettingService>().As<ISettingService>();
            builder.RegisterType<SearchService>().As<ISearchService>();
            builder.RegisterType<NotificationService>().As<INotificationService>();
            builder.RegisterType<InboxService>().As<IInboxService>();
            builder.RegisterType<ChatService>().As<IChatService>();
        }
    }
}