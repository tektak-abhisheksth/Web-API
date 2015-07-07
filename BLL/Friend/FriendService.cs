using DAL.DbEntity;
using Model.Common;
using Model.Friend;
using Model.Types;
using System.Threading.Tasks;

namespace BLL.Friend
{
    public partial class FriendService : IFriendService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TekTak.iLoop.UOW.IUnitOfWork _jUnitOfWork;

        public FriendService(IUnitOfWork unitOfWork, TekTak.iLoop.UOW.IUnitOfWork jUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _jUnitOfWork = jUnitOfWork;
        }

        public Task<StatusData<string>> UpdatePhoneBookContacts(PhoneBookContactsRequest request, SystemSession session)
        {

            //return _unitOfWork.Friend.SyncPhoneBook(request);
            return _jUnitOfWork.Friend.SyncPhoneBook(request, session);
        }

        public Task<StatusData<FriendInformationCategorizedResponse>> GetFriends(string deviceId, string cTag, SystemSession session)
        {
            return _jUnitOfWork.Friend.GetFriends(deviceId, cTag, session);
        }

        public Task<StatusData<string>> RequestFriend(FriendshipRequest request, SystemSession session)
        {
            return _jUnitOfWork.Friend.RequestFriend(request, session);
        }

        public Task<StatusData<string>> RespondFriendRequest(FriendRespondRequest request, SystemSession session)
        {

            //return _unitOfWork.Friend.RespondFriendRequest(userId, friendId, isAccepted, categoryId);
            return _jUnitOfWork.Friend.RespondFriendRequest(request, session);
        }

        public Task<StatusData<string>> UnFriendRequest(FriendRequest request, SystemSession session)
        {
            return _jUnitOfWork.Friend.UnFriendRequest(request, session);
        }
    }
}
