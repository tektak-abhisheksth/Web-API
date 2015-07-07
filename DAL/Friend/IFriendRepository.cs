using DAL.DbEntity;
using Model.Base;
using Model.Common;
using Model.Friend;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Friend
{
    public interface IFriendRepository : IGenericRepository<Entity.Friend>
    {
        Task<StatusData<string>> SyncPhoneBook(PhoneBookContactsRequest request);
        Task<StatusData<FriendInformationCategorizedResponse>> GetFriends(int userId, string deviceId, string cTag);
        Task<SystemDbStatus> RequestFriend(int userId, int friendId, int categoryId = 0);
        //Task<StatusData<string>> RespondFriendRequest(int userId, int friendId, bool isAccepted = true, int categoryId = 0);
        Task<StatusData<string>> UnFriendRequest(int userId, int friendId);
        //Task<SystemDbStatus> UpdatePhoneBookContacts(PhoneBookContactsRequest request);
        //Task<SystemDbStatus> RemovePhoneBookContacts(PhoneBookContactsRequest request);
        //Task<StatusData<string>> RandomizeCtag(int userId);
        Task<PaginatedResponseExtended<IEnumerable<UserInformationBaseExtendedResponse>, int>> MutualFriends(PaginatedRequest<string> request);
    }
}
