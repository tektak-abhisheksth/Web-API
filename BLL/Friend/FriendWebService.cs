using Model.Base;
using Model.Common;
using Model.Friend;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Friend
{
    public partial class FriendService
    {
        public Task<PaginatedResponseExtended<IEnumerable<UserInformationBaseExtendedResponse>, int>> GetMutualFriends(PaginatedRequest<string> request, SystemSession session)
        {
            return _jUnitOfWork.Friend.GetMutualFriends(request, session);
        }

        public Task<PaginatedResponse<IEnumerable<WebFriendInformationResponse>>> GetWebFriends(PaginatedRequest<string> request, SystemSession session)
        {
            return _jUnitOfWork.Friend.GetWebFriends(request, session);
        }
    }
}
