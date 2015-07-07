using System;
using System.Threading.Tasks;
using DAL.DbEntity;
using Entity;
using Model.Types;

namespace DAL.Media
{
    public class MediaRepository : GenericRepository<UserInfo>, IMediaRepository
    {
        public MediaRepository(iLoopEntity context) : base(context) { }

        public async Task<SystemDbStatus> UploadProfilePicture(int userId, string fileName)
        {
            var user = await FirstOrDefaultAsync(x => x.UserID == userId).ConfigureAwait(false);
            user.Picture = fileName;
            user.LastUpdated = DateTime.UtcNow;
            return SystemDbStatus.Updated;
        }

        public async Task<SystemDbStatus> SaveFileName(string fileId, SystemSession session)
        {
            var user = await FirstOrDefaultAsync(x => x.UserID == session.UserId).ConfigureAwait(false);
            if (user != null)
            {
                user.Picture = fileId;
                return SystemDbStatus.Updated;
            }
            return SystemDbStatus.NotModified;
        }
    }
}
