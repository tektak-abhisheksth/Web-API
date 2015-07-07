using Model.Base;
using Model.Common;
using Model.Friend;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Friend
{
    public partial interface IFriendService
    {
        Task<PaginatedResponseExtended<IEnumerable<UserInformationBaseExtendedResponse>, int>> GetMutualFriends(PaginatedRequest<string> request, SystemSession session);

        Task<PaginatedResponse<IEnumerable<WebFriendInformationResponse>>> GetWebFriends(PaginatedRequest<string> request, SystemSession session);
    }
}
