using Model.Common;
using Model.Profile;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;
using TekTak.iLoop.Profile;

namespace TekTak.iLoop.Sealed.Profile
{
    public interface IProfileRepositorySealed : IProfileRepository
    {
        Task<AboutResponse> GetUserAbout(string targetUser, SystemSession session);
        Task<StatusData<string>> UpdateUserAbout(AboutRequest request, SystemSession session);
        Task<PaginatedResponseExtended<IEnumerable<ViewerDetailResponse>, int>> GetProfileViewDetail(PaginatedRequest<GeneralKvPair<SystemProfileViewType, int>> request, SystemSession session);
        Task<PaginatedResponseExtended<IEnumerable<ViewSummaryResponse>, int>> GetProfileViewSummary(PaginatedRequest<int> request, SystemSession session);
        Task<InformationResponse<IEnumerable<ViewerPanelResponse>, int>> GetProfileViewPanel(int userId, SystemSession session);
    }
}
