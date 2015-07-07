using System;
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
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepositorySealed Account { get; }
        ICategoryRepositorySealed Category { get; }
        IChatRepositorySealed Chat { get; }
        IDataRepositorySealed Data { get; }
        IFriendRepositorySealed Friend { get; }
        IInboxRepositorySealed Inbox { get; }
        IMediaRepositorySealed Media { get; }
        INotificationRepositorySealed Notification { get; }
        IProfileRepositorySealed Profile { get; }
        IProfileBusinessRepositorySealed ProfileBusiness { get; }
        IProfilePersonalRepositorySealed ProfilePersonal { get; }
        ISettingRepositorySealed Setting { get; }
        ISearchRepositorySealed Search { get; }
    }
}
