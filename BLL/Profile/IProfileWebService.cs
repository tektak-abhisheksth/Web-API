using Model.Common;
using Model.Profile;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Profile
{
    public partial interface IProfileService
    {
        Task<PaginatedResponseExtended<IEnumerable<ViewerDetailResponse>, int>> GetProfileViewDetail(PaginatedRequest<GeneralKvPair<SystemProfileViewType, int>> request, SystemSession getSession);
        Task<InformationResponse<IEnumerable<ViewerPanelResponse>, int>> GetProfileViewPanel(SystemSession getSession);
        Task<PaginatedResponseExtended<IEnumerable<ViewSummaryResponse>, int>> GetProfileViewSummary(PaginatedRequest<int> request, SystemSession getSession);
        Task<AboutResponse> GetUserAbout(string targetUser, SystemSession session);
        Task<StatusData<string>> UpdateUserAbout(AboutRequest request, SystemSession session);
        Task<UserStatusResponse> GetUserAvailability(string targetUser, SystemSession session);
        Task<StatusData<string>> UpdateUserAvailability(StatusSetRequest request, SystemSession session);
    }
}
