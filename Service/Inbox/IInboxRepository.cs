
using Model.Base;
using Model.Common;
using Model.Inbox;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TekTak.iLoop.Inbox
{
    public interface IInboxRepository
    {
        Task<IEnumerable<InboxResponse>> GetInboxFolder(SystemSession session);

        Task<StatusData<GeneralKvPair<int, List<long>>>> UpsertInbox(InboxRequest request, SystemSession session);
        Task<StatusData<int>> UpsertInbox(InboxRequest request, int folderId, SystemSession session);
        Task<StatusData<string>> DeleteInbox(List<int> folderId, SystemSession session);
        Task<StatusData<GeneralKvPair<int, List<long>>>> UpsertRule(RequestBase request, SystemDbStatus dbMode, SystemSession session);
        Task<StatusData<long>> DeleteRule(SingleData<GeneralKvPair<long, int>> request, SystemSession session);
        Task<IEnumerable<RuleResponse>> GetInboxFolderRules(int? folderId, SystemSession session);
        Task<StatusData<string>> MuteInbox(InboxMuteRequest request, SystemSession session);
    }
}
