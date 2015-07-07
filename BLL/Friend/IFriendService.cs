using Model.Common;
using Model.Friend;
using Model.Types;
using System.Threading.Tasks;

namespace BLL.Friend
{
    public partial interface IFriendService
    {
        Task<StatusData<string>> UpdatePhoneBookContacts(PhoneBookContactsRequest request, SystemSession session);
        Task<StatusData<FriendInformationCategorizedResponse>> GetFriends(string deviceId, string cTag, SystemSession session);
        Task<StatusData<string>> RequestFriend(FriendshipRequest request, SystemSession session);
        Task<StatusData<string>> RespondFriendRequest(FriendRespondRequest request, SystemSession session);
        Task<StatusData<string>> UnFriendRequest(FriendRequest request, SystemSession session);
    }
}
