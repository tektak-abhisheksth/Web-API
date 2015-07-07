using DAL.DbEntity;
using Entity;
using Model.Base;
using Model.Common;
using Model.Inbox;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Inbox
{
    public class InboxRepository : GenericRepository<MessageFolder>, IInboxRepository
    {
        public InboxRepository(iLoopEntity context) : base(context) { }

        public async Task<IEnumerable<InboxResponse>> GetInboxFolder(int userId)
        {
            var response = await Task.Factory.StartNew(() => FindBy(x => x.UserId == userId).Select(x => new InboxResponse { FolderId = x.FolderId, Name = x.Name, Created = x.Created, RuleCount = x.MessageRules.Count })).ConfigureAwait(false);
            return response;
        }

        //public async Task<IEnumerable<RuleResponse>> GetInboxFolderRules(int userId, int? folderId)
        //{
        //    var folders = await Task.Factory.StartNew(() => FindBy(x => x.UserId == userId && (!folderId.HasValue || x.FolderId == folderId))).ConfigureAwait(false);
        //    var response = new List<RuleResponse>();
        //    if (folders.Any())
        //        foreach (var messageFolder in folders)
        //        {
        //            response.AddRange(messageFolder.MessageRules.Select(x => new RuleResponse { MessageRuleId = x.MessageRuleId, FolderId = x.MessageFolder.FolderId, RuleTypeUser = (SystemRuleTypeUser)x.RuleTypeUser, ContactList = Context.MessageEntryContacts.Where(y => y.MessageEntryContactGuid == x.EntryContacts).Select(y => y.UserId).ToList(), GroupList = Context.MessageEntryGroups.Where(y => y.MessageEntryGroupGuid == x.EntryGroups).Select(y => y.GroupId).ToList(), RuleTypeSubject = (SystemRuleTypeSubject)x.RuleTypeSubject, Subject = x.Subject }).ToList());
        //        }
        //    return response;
        //}

        public async Task<IEnumerable<RuleResponse>> GetInboxFolderRules(int userId, int? folderId)
        {
            var folders = await Task.Factory.StartNew(() => FindBy(x => x.UserId == userId && (!folderId.HasValue || x.FolderId == folderId))).ConfigureAwait(false);
            var response = new List<RuleResponse>();
            if (folders.Any())
                foreach (var messageFolder in folders)
                {
                    response.AddRange(messageFolder.MessageRules.Select(x => new RuleResponse { MessageRuleId = x.MessageRuleId, FolderId = x.MessageFolder.FolderId, RuleTypeUser = (SystemRuleTypeUser)x.RuleTypeUser, GroupList = Context.MessageEntryGroups.Where(y => y.MessageEntryGroupGuid == x.EntryGroups).Select(y => y.GroupId).ToList(), RuleTypeSubject = (SystemRuleTypeSubject)x.RuleTypeSubject, Subject = x.Subject }).ToList());
                }
            return response;
        }

        public async Task<StatusData<GeneralKvPair<int, List<long>>>> UpsertInbox(InboxRequest request, int? folderId, SystemDbStatus dbMode)
        {
            var result = new StatusData<GeneralKvPair<int, List<long>>>();
            var folderIdObj = new ObjectParameter("FOLDERID", folderId ?? 0);
            var messageRuleIdsObj = new ObjectParameter("MESSAGERULEIDS", typeof(string));

            var rules = string.Empty;
            if (request.Rule != null && request.Rule.Any())
            {
                var sb = new StringBuilder();
                foreach (var rule in request.Rule)
                    sb.Append(rule).Append("|");
                rules = sb.ToString().Substring(0, sb.Length - 1);
            }
            result.Status = (SystemDbStatus)await Task.Factory.StartNew(() => Context.PROC_UPSERT_INBOX(request.UserId, request.Name, rules, (byte)dbMode, folderIdObj, messageRuleIdsObj).FirstOrDefault().GetValueOrDefault());

            result.Data = new GeneralKvPair<int, List<long>>();
            if (result.Status.IsOperationSuccessful())
            {
                result.Data.Id = Convert.ToInt32(folderIdObj.Value);
                rules = messageRuleIdsObj.Value.ToString();
                if (!string.IsNullOrWhiteSpace(rules))
                    result.Data.Value = (rules.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt64(x))).ToList();
            }
            return result;
        }

        public async Task<StatusData<GeneralKvPair<int, List<long>>>> UpsertRule(int userId, RequestBase request, SystemDbStatus dbMode)
        {
            var result = new StatusData<GeneralKvPair<int, List<long>>>();

            var folderId = 0;
            var messageRuleId = string.Empty;
            var ruleString = string.Empty;
            if (dbMode == SystemDbStatus.Inserted)
            {
                var req = request as RuleAddRequest;
                if (req != null)
                {
                    folderId = req.FolderId;
                    ruleString = req.Rule.ToString();
                }
            }
            else
            {
                var req = request as RuleUpdateRequest;
                if (req != null)
                {
                    messageRuleId = req.MessageRuleId.ToString();
                    ruleString = req.Rule.ToString();
                }

            }

            var folderIdObj = new ObjectParameter("FOLDERID", folderId);
            var messageRuleIdsObj = new ObjectParameter("MESSAGERULEIDS", messageRuleId);

            // var rules = ruleString.Substring(0, ruleString.Length - 1);

            result.Status = (SystemDbStatus)await Task.Factory.StartNew(() => Context.PROC_UPSERT_INBOX_RULES(userId, (byte)dbMode, ruleString, folderIdObj, messageRuleIdsObj).FirstOrDefault().GetValueOrDefault());

            result.Data = new GeneralKvPair<int, List<long>>();
            if (result.Status.IsOperationSuccessful())
            {
                result.Data.Id = Convert.ToInt32(folderIdObj.Value);
                ruleString = messageRuleIdsObj.Value.ToString();
                if (!string.IsNullOrWhiteSpace(ruleString))
                    result.Data.Value = (ruleString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt64(x))).ToList();
            }
            return result;
        }

        public async Task<SystemDbStatus> DeleteRule(long messageRuleId)
        {
            var rule = await Context.MessageRules.FirstOrDefaultAsync(x => x.MessageRuleId == messageRuleId).ConfigureAwait(false);
            if (rule != null)
            {
                Context.MessageRules.Remove(rule);
                return SystemDbStatus.Deleted;
            }
            return SystemDbStatus.NotFound;
        }
    }
}
