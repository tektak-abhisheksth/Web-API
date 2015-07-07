using API.Controllers.Category;
using API.Controllers.Chat;
using API.Controllers.Friend;
using API.Controllers.Inbox;
using API.Controllers.Media;
using API.Controllers.Notification;
using API.Controllers.Profile;
using API.Controllers.Search;
using API.Controllers.Setting;
using API.Filters;
using Autofac;
using Autofac.Integration.WebApi;
using BLL.Account;

namespace API.Registers
{
    public static class AuthorizationRegister
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.Register(c => new AuthorizationAttribute(c.Resolve<IAccountService>())).AsWebApiAuthorizationFilterFor<ProfileController>().InstancePerLifetimeScope();
            builder.Register(c => new AuthorizationAttribute(c.Resolve<IAccountService>())).AsWebApiAuthorizationFilterFor<ProfilePersonalController>().InstancePerLifetimeScope();
            builder.Register(c => new AuthorizationAttribute(c.Resolve<IAccountService>())).AsWebApiAuthorizationFilterFor<ProfileBusinessController>().InstancePerLifetimeScope();
            builder.Register(c => new AuthorizationAttribute(c.Resolve<IAccountService>())).AsWebApiAuthorizationFilterFor<MediaController>().InstancePerLifetimeScope();
            builder.Register(c => new AuthorizationAttribute(c.Resolve<IAccountService>())).AsWebApiAuthorizationFilterFor<CategoryController>().InstancePerLifetimeScope();
            builder.Register(c => new AuthorizationAttribute(c.Resolve<IAccountService>())).AsWebApiAuthorizationFilterFor<FriendController>().InstancePerLifetimeScope();
            builder.Register(c => new AuthorizationAttribute(c.Resolve<IAccountService>())).AsWebApiAuthorizationFilterFor<SettingController>().InstancePerLifetimeScope();
            builder.Register(c => new AuthorizationAttribute(c.Resolve<IAccountService>())).AsWebApiAuthorizationFilterFor<SearchController>().InstancePerLifetimeScope();
            builder.Register(c => new AuthorizationAttribute(c.Resolve<IAccountService>())).AsWebApiAuthorizationFilterFor<NotificationController>().InstancePerLifetimeScope();
            builder.Register(c => new AuthorizationAttribute(c.Resolve<IAccountService>())).AsWebApiAuthorizationFilterFor<InboxController>().InstancePerLifetimeScope();
            builder.Register(c => new AuthorizationAttribute(c.Resolve<IAccountService>())).AsWebApiAuthorizationFilterFor<ChatController>().InstancePerLifetimeScope();

            //builder.Register(c => new AuthorizationAttribute(c.Resolve<IAccountService>())).AsWebApiAuthorizationFilterFor<DataController>().InstancePerLifetimeScope();
        }
    }
}