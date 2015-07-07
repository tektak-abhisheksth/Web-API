using Model.Base;
using Model.Common;
using Model.Inbox;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekTak.iLoop.Helper;
using TekTak.iLoop.Kauwa;

namespace TekTak.iLoop.Inbox
{
    public class InboxRepository : IInboxRepository
    {
        protected readonly Services Client;

        public InboxRepository(Services client)
        {
            Client = client;
        }

        public async Task<IEnumerable<InboxResponse>> GetInboxFolder(SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.InboxService.listInboxFolders(session.UserId, session.GetSession())).ConfigureAwait(false);
            var result = response.Select(x => new InboxResponse
            {
                FolderId = x.FolderId,
                Name = x.Name,
                Created = Convert.ToDateTime(x.CreatedDate),
                RuleCount = x.RuleCount,
                Mute = x.Muted == 1
            });
            return result;
        }

        public async Task<IEnumerable<RuleResponse>> GetInboxFolderRules(int? folderId, SystemSession session)
        {
            var response = new List<RuleResponse>();
            var inboxRuleList = await Task.Factory.StartNew(() => (folderId == null)
                ? Client.InboxRuleService.getRules(session.UserId, session.GetSession())
                : Client.InboxService.getInboxFolderWithRules(session.UserId, (int)folderId, session.GetSession())
                    .InboxRules).ConfigureAwait(false);
            if (inboxRuleList != null)
            {
                inboxRuleList.ForEach(
                    x =>
                    {
                        if (x.RuleId > 0)
                            response.Add(new RuleResponse
                            {
                                FolderId = x.InboxId,
                                MessageRuleId = x.RuleId,
                                RuleTypeUser = (SystemRuleTypeUser)x.RuleTypeUser,
                                ContactList = x.ContactList,
                                GroupList = x.GroupList,
                                RuleTypeSubject = (SystemRuleTypeSubject)x.RuleTypeSubject,
                                Subject = x.Subject
                            });
                    });
            }
            return response;
        }

        public async Task<StatusData<GeneralKvPair<int, List<long>>>> UpsertInbox(InboxRequest request, SystemSession session)
        {
            var result = new StatusData<GeneralKvPair<int, List<long>>>();
            if (request.Rule != null && request.Rule.Any())
                request.Rule.RemoveAll(x => x.UserSelection == SystemUserSelection.None && x.RuleTypeSubject == SystemRuleTypeSubject.None);

            var inboxRequest = new Kauwa.Inbox { UserId = request.UserId, Name = request.Name, FolderId = 0, InboxRules = request.Rule != null && request.Rule.Any() ? request.Rule.Select(x => new InboxRule { TypeUserSelection = (int)x.UserSelection, RuleTypeUser = (int)x.RuleTypeUser, ContactList = x.ContactList, GroupList = x.GroupList, RuleTypeSubject = (int)x.RuleTypeSubject, Subject = x.Subject, ApplyOnOldMessage = x.ApplyOnOldMessage }).ToList() : null };

            var response = await Task.Factory.StartNew(() => Client.InboxService.createInboxWithRules(inboxRequest, session.GetSession())).ConfigureAwait(false);
            result.Status = (SystemDbStatus)response.DbStatusCode;
            result.Data = new GeneralKvPair<int, List<long>>();
            result.Message = response.DbStatusMsg;

            result.Data = new GeneralKvPair<int, List<long>>
            {
                Id = response.FolderId,
                Value = response.InboxRules != null ? response.InboxRules.Select(x => x.RuleId).ToList() : null
            };

            return result;
        }

        public async Task<StatusData<int>> UpsertInbox(InboxRequest request, int folderId, SystemSession session)
        {
            var inboxRequest = new Kauwa.Inbox { UserId = request.UserId, Name = request.Name, FolderId = folderId };

            var response = await Task.Factory.StartNew(() => Client.InboxService.updateInboxFolderName(inboxRequest, session.GetSession())).ConfigureAwait(false);

            var result = response.GetStatusData<int>();
            result.Data = folderId;
            return result;
        }

        public async Task<StatusData<string>> MuteInbox(InboxMuteRequest request, SystemSession session)
        {
            return (await Task.Factory.StartNew(() => Client.InboxService.muteInbox(session.UserId, string.Join(",", request.FolderList), request.Mute ? 1 : 0, session.GetSession())).ConfigureAwait(false)).GetStatusData<string>();
        }

        //public async Task<StatusData<int>> DeleteInbox(InboxRequest request, int folderId, SystemSession session)
        //{
        //    var inboxRequest = new Kauwa.Inbox { UserId = request.UserId, FolderId = folderId };

        //    var serviceResponse = await Task.Factory.StartNew(() => _client.InboxService.deleteInboxFolder(inboxRequest, session.GetSession()));

        //    var response = serviceResponse.GetStatusData<int>();
        //    response.Data = folderId;
        //    return response;
        //}

        public async Task<StatusData<string>> DeleteInbox(List<int> folderList, SystemSession session)
        {
            var serviceResponse = await Task.Factory.StartNew(() => Client.InboxService.deleteInboxFolder(string.Join(",", folderList), session.GetSession())).ConfigureAwait(false);
            var response = serviceResponse.GetStatusData<string>();
            return response;
        }

        public async Task<StatusData<GeneralKvPair<int, List<long>>>> UpsertRule(RequestBase request, SystemDbStatus dbMode, SystemSession session)
        {
            var result = new StatusData<GeneralKvPair<int, List<long>>>();

            if (dbMode == SystemDbStatus.Inserted)
            {
                var req = request as RuleAddRequest;

                var ruleRequest = new Kauwa.Inbox { UserId = request.UserId, FolderId = req.FolderId, InboxRules = new List<InboxRule> { new InboxRule { TypeUserSelection = (int)req.Rule.UserSelection, RuleTypeUser = (int)req.Rule.RuleTypeUser, ContactList = req.Rule.ContactList, GroupList = req.Rule.GroupList, RuleTypeSubject = (int)req.Rule.RuleTypeSubject, Subject = req.Rule.Subject, ApplyOnOldMessage = req.Rule.ApplyOnOldMessage } } };

                var response = await Task.Factory.StartNew(() => Client.InboxRuleService.createInboxRules(ruleRequest, session.GetSession())).ConfigureAwait(false);
                result.Status = (SystemDbStatus)response.DbStatusCode;

                result.Data = new GeneralKvPair<int, List<long>>
                {
                    Id = response.FolderId,
                    Value = response.InboxRules != null ? response.InboxRules.Select(x => x.RuleId).ToList() : null
                };

                result.SubStatus = response.DbSubStatusCode;
                result.Message = response.DbStatusMsg;
                return result;
            }
            else
            {
                var req = request as RuleUpdateRequest;

                var ruleRequest = new Kauwa.Inbox { UserId = request.UserId, FolderId = req.FolderId, InboxRules = new List<InboxRule> { new InboxRule { RuleId = req.MessageRuleId, TypeUserSelection = (int)req.Rule.UserSelection, RuleTypeUser = (int)req.Rule.RuleTypeUser, ContactList = req.Rule.ContactList, GroupList = req.Rule.GroupList, RuleTypeSubject = (int)req.Rule.RuleTypeSubject, Subject = req.Rule.Subject, ApplyOnOldMessage = req.Rule.ApplyOnOldMessage } } };

                result = (await Task.Factory.StartNew(() => Client.InboxRuleService.updateInboxRules(ruleRequest, session.GetSession())).ConfigureAwait(false)).GetStatusData<GeneralKvPair<int, List<long>>>();
                return result;
            }
        }

        //public async Task<StatusData<long>> DeleteRule(long messageRuleId, SystemSession session)
        //{
        //    var ruleRequest = new Kauwa.Inbox { UserId = session.UserId, InboxRules = new List<InboxRule> { new InboxRule { RuleId = messageRuleId } } };
        //    var response = (await Task.Factory.StartNew(() => Client.InboxRuleService.deleteInboxRules(ruleRequest, session.GetSession())).ConfigureAwait(false)).GetStatusData<long>();
        //    response.Data = messageRuleId;
        //    return response;
        //}

        public async Task<StatusData<long>> DeleteRule(SingleData<GeneralKvPair<long, int>> request, SystemSession session)
        {
            var ruleRequest = new Kauwa.Inbox { UserId = session.UserId, FolderId = request.Data.Value, InboxRules = new List<InboxRule> { new InboxRule { RuleId = request.Data.Id } } };
            var response = (await Task.Factory.StartNew(() => Client.InboxRuleService.deleteInboxRules(ruleRequest, session.GetSession())).ConfigureAwait(false)).GetStatusData<long>();
            response.Data = request.Data.Id;
            return response;
        }


    }
}
