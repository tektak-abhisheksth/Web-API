using DAL.DbEntity;
using Model.Base;
using Model.Common;
using Model.Inbox;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Inbox
{
    public class InboxService : IInboxService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TekTak.iLoop.UOW.IUnitOfWork _jUnitOfWork;

        public InboxService(IUnitOfWork unitOfWork, TekTak.iLoop.UOW.IUnitOfWork jUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _jUnitOfWork = jUnitOfWork;
        }

        public Task<IEnumerable<InboxResponse>> GetInboxFolder(SystemSession session)
        {
            //    return _unitOfWork.Inbox.GetInboxFolder(userId);
            return _jUnitOfWork.Inbox.GetInboxFolder(session);
        }

        public Task<IEnumerable<RuleResponse>> GetInboxFolderRules(int? folderId, SystemSession session)
        {
            return _jUnitOfWork.Inbox.GetInboxFolderRules(folderId, session);
        }

        public Task<StatusData<GeneralKvPair<int, List<long>>>> UpsertInbox(InboxRequest request, int? folderId, SystemDbStatus dbMode, SystemSession session)
        {
            return _jUnitOfWork.Inbox.UpsertInbox(request, session);
        }

        public Task<StatusData<int>> UpsertInbox(InboxRequest request, int folderId, SystemSession session)
        {
            return _jUnitOfWork.Inbox.UpsertInbox(request, folderId, session);
        }

        public Task<StatusData<string>> MuteInbox(InboxMuteRequest request, SystemSession session)
        {
            return _jUnitOfWork.Inbox.MuteInbox(request, session);
        }
        public Task<StatusData<string>> DeleteInbox(List<int> folderList, SystemSession session)
        {
            return _jUnitOfWork.Inbox.DeleteInbox(folderList, session);
        }
        public Task<StatusData<GeneralKvPair<int, List<long>>>> UpsertRule(RequestBase request, SystemDbStatus dbMode, SystemSession session)
        {
            return _jUnitOfWork.Inbox.UpsertRule(request, dbMode, session);
        }

        public Task<StatusData<long>> DeleteRule(SingleData<GeneralKvPair<long, int>> request, SystemSession session)
        {
            return _jUnitOfWork.Inbox.DeleteRule(request, session);
        }
    }
}
