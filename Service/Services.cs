using System;
using TekTak.iLoop.Kauwa;
using Thrift.Protocol;
using Thrift.Transport;

namespace TekTak.iLoop
{
    public class Services : IDisposable
    {
        public SessionService.Client SessionService { get; private set; }
        public UserService.Client UserService { get; private set; }
        public SettingService.Client SettingService { get; set; }
        public ChatService.Client ChatService { get; private set; }
        public ChatUserInfoService.Client ChatUserInfoService { get; private set; }
        public ChatGroupService.Client ChatGroupService { get; private set; }
        public InboxService.Client InboxService { get; private set; }
        public InboxRuleService.Client InboxRuleService { get; set; }
        public ElifService.Client ElifService { get; set; }
        public SearchService.Client SearchService { get; set; }
        public NotificationService.Client NotificationService { get; set; }
        public Services(TTransport transport)
        {
            SessionService = new SessionService.Client(new TMultiplexedProtocol(new TBinaryProtocol(new TFramedTransport(transport)), Utility.Helper.NameOf(() => SessionService)));
            UserService = new UserService.Client(new TMultiplexedProtocol(new TBinaryProtocol(new TFramedTransport(transport)), Utility.Helper.NameOf(() => UserService)));
            SettingService = new SettingService.Client(new TMultiplexedProtocol(new TBinaryProtocol(new TFramedTransport(transport)), Utility.Helper.NameOf(() => SettingService)));
            ChatService = new ChatService.Client(new TMultiplexedProtocol(new TBinaryProtocol(new TFramedTransport(transport)), Utility.Helper.NameOf(() => ChatService)));
            ChatUserInfoService = new ChatUserInfoService.Client(new TMultiplexedProtocol(new TBinaryProtocol(new TFramedTransport(transport)), Utility.Helper.NameOf(() => ChatUserInfoService)));
            ChatGroupService = new ChatGroupService.Client(new TMultiplexedProtocol(new TBinaryProtocol(new TFramedTransport(transport)), Utility.Helper.NameOf(() => ChatGroupService)));
            InboxService = new InboxService.Client(new TMultiplexedProtocol(new TBinaryProtocol(new TFramedTransport(transport)), Utility.Helper.NameOf(() => InboxService)));
            InboxRuleService = new InboxRuleService.Client(new TMultiplexedProtocol(new TBinaryProtocol(new TFramedTransport(transport)), Utility.Helper.NameOf(() => InboxRuleService)));
            ElifService = new ElifService.Client(new TMultiplexedProtocol(new TBinaryProtocol(new TFramedTransport(transport)), Utility.Helper.NameOf(() => ElifService)));
            SearchService = new SearchService.Client(new TMultiplexedProtocol(new TBinaryProtocol(new TFramedTransport(transport)), Utility.Helper.NameOf(() => SearchService)));
            NotificationService = new NotificationService.Client(new TMultiplexedProtocol(new TBinaryProtocol(new TFramedTransport(transport)), Utility.Helper.NameOf(() => NotificationService)));
        }

        public void Dispose()
        {
            SessionService.Dispose();
            UserService.Dispose();
            SettingService.Dispose();
            ChatService.Dispose();
        }
    }
}