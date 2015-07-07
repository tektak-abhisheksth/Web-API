using Model.Common;
using Model.Friend;
using Model.Types;
using System.Threading.Tasks;

namespace TekTak.iLoop.Friend
{
    public interface IFriendRepository
    {
        Task<StatusData<string>> SyncPhoneBook(PhoneBookContactsRequest request, SystemSession session);
        Task<StatusData<FriendInformationCategorizedResponse>> GetFriends(string deviceId, string cTag, SystemSession session);

        Task<StatusData<string>> RequestFriend(FriendshipRequest request, SystemSession session);

        Task<StatusData<string>> RespondFriendRequest(FriendRespondRequest request, SystemSession session);

        Task<StatusData<string>> UnFriendRequest(FriendRequest request, SystemSession session);
    }
}
