using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Base;
using Model.Common;
using Model.Inbox;
using Model.Types;

namespace DAL.Inbox
{
    public interface IInboxRepository
    {
        Task<IEnumerable<InboxResponse>> GetInboxFolder(int userId);
        Task<IEnumerable<RuleResponse>> GetInboxFolderRules(int userId, int? folderId);
        Task<StatusData<GeneralKvPair<int, List<long>>>> UpsertInbox(InboxRequest request, int? folderId, SystemDbStatus dbMode);
        Task<StatusData<GeneralKvPair<int, List<long>>>> UpsertRule(int userId, RequestBase request, SystemDbStatus dbMode);
        Task<SystemDbStatus> DeleteRule(long messageRuleId);
    }
}
