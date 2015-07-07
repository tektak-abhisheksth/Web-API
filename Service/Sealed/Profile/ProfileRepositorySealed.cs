using Model.Common;
using Model.Profile;
using Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekTak.iLoop.Helper;
using TekTak.iLoop.Kauwa;
using TekTak.iLoop.Profile;

namespace TekTak.iLoop.Sealed.Profile
{
    public sealed class ProfileRepositorySealed : ProfileRepository, IProfileRepositorySealed
    {
        public ProfileRepositorySealed(Services client) : base(client) { }

        public async Task<AboutResponse> GetUserAbout(string targetUser, SystemSession session)
        {
            AboutResponse response = null;
            var result = await Task.Factory.StartNew(() => Client.UserService.getUserAbout(targetUser, session.GetSession())).ConfigureAwait(false);

            if (result != null)
            {
                response = new AboutResponse
                {
                    Headline = result.HeadLine,
                    About = result.About
                };
            }
            return response;
        }

        public async Task<StatusData<string>> UpdateUserAbout(AboutRequest request, SystemSession session)
        {
            var response = (await Task.Factory.StartNew(() => Client.UserService.updateUserAbout(session.UserId.ToString(), request.Headline, request.About, session.GetSession())).ConfigureAwait(false)).GetStatusData<string>();
            return response;
        }

        public async Task<PaginatedResponseExtended<IEnumerable<ViewSummaryResponse>, int>> GetProfileViewSummary(PaginatedRequest<int> request, SystemSession session)
        {
            var serviceRequest = new ProfileView
            {
                UserId = request.UserId,
                ViewType = request.Data,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };

            var response = await Task.Factory.StartNew(() => Client.UserService.profileViewSummary(serviceRequest, session.GetSession())).ConfigureAwait(false);
            var data = response.ProfileViews.Select(x => new ViewSummaryResponse
            {
                TypeId = x.TypeId,
                Name = x.FirstName,
                ViewCount = x.ViewersCount,
                NewViewCount = x.NewViews,
                LastViewed = Convert.ToDateTime(x.ViewDate)
            });
            var result = new PaginatedResponseExtended<IEnumerable<ViewSummaryResponse>, int>
            {
                Page = data,
                HasNextPage = response.HasNextPage,
                Information = response.ViewCount
            };
            return result;
        }

        public async Task<PaginatedResponseExtended<IEnumerable<ViewerDetailResponse>, int>> GetProfileViewDetail(PaginatedRequest<GeneralKvPair<SystemProfileViewType, int>> request, SystemSession session)
        {
            var serviceRequest = new ProfileView
            {
                UserId = request.UserId,
                ViewType = (byte)request.Data.Id,
                TypeId = request.Data.Value,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };

            var response = await Task.Factory.StartNew(() => Client.UserService.profileViewDetail(serviceRequest, session.GetSession())).ConfigureAwait(false);
            var data = response.ProfileViews.Select(x => new ViewerDetailResponse
            {
                UserId = x.UserId,
                UserTypeId = (SystemUserType)x.UserTypeId,
                UserName = x.Username,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Image = x.Picture,
                FriendshipStatus = (SystemFriendshipStatus)x.IsConnected,
                Observed = x.Observed,
                Title = x.Title,
                ViewedDate = Convert.ToDateTime(x.ViewDate)
            });
            var result = new PaginatedResponseExtended<IEnumerable<ViewerDetailResponse>, int>
            {
                Page = data,
                HasNextPage = response.HasNextPage,
                Information = response.ViewCount
            };
            return result;
        }

        public async Task<InformationResponse<IEnumerable<ViewerPanelResponse>, int>> GetProfileViewPanel(int userId, SystemSession session)
        {
            var serviceRequest = new ProfileView
            {
                UserId = userId
            };

            var response = await Task.Factory.StartNew(() => Client.UserService.profileViewPanel(serviceRequest, session.GetSession())).ConfigureAwait(false);
            var data = response.ProfileViews.Select(x => new ViewerPanelResponse
            {
                UserId = x.UserId,
                UserTypeId = (SystemUserType)x.UserTypeId,
                UserName = x.Username,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Image = x.Picture,
                Observed = x.Observed,
                Title = x.Title,
                ViewedDate = Convert.ToDateTime(x.ViewDate),
                Position = x.PositionName,
                ViewerCount = x.ViewersCount
            });
            var result = new InformationResponse<IEnumerable<ViewerPanelResponse>, int>
            {
                Page = data,
                Information = response.TotalTypes
            };
            return result;
        }
    }
}
