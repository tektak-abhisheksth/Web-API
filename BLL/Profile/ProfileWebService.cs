using Model.Common;
using Model.Profile;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Profile
{
    public partial class ProfileService
    {
        public Task<AboutResponse> GetUserAbout(string targetUser, SystemSession session)
        {
            return _jUnitOfWork.Profile.GetUserAbout(targetUser, session);
        }

        public Task<StatusData<string>> UpdateUserAbout(AboutRequest request, SystemSession session)
        {
            return _jUnitOfWork.Profile.UpdateUserAbout(request, session);
        }

        public Task<PaginatedResponseExtended<IEnumerable<ViewerDetailResponse>, int>> GetProfileViewDetail(PaginatedRequest<GeneralKvPair<SystemProfileViewType, int>> request, SystemSession getSession)
        {
            // return _unitOfWork.Profile.GetProfileViewDetail(request);
            return _jUnitOfWork.Profile.GetProfileViewDetail(request, getSession);
        }

        public Task<InformationResponse<IEnumerable<ViewerPanelResponse>, int>> GetProfileViewPanel(SystemSession getSession)
        {
            //return _unitOfWork.Profile.GetProfileViewPanel(getSession.UserId);
            return _jUnitOfWork.Profile.GetProfileViewPanel(getSession.UserId, getSession);
        }

        public Task<PaginatedResponseExtended<IEnumerable<ViewSummaryResponse>, int>> GetProfileViewSummary(PaginatedRequest<int> request, SystemSession getSession)
        {
            //return _unitOfWork.Profile.GetProfileViewSummary(request);
            return _jUnitOfWork.Profile.GetProfileViewSummary(request, getSession);
        }
    }
}
