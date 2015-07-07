using System.Threading.Tasks;
using DAL.DbEntity;
using Entity;
using Model.Types;

namespace DAL.Media
{
    public interface IMediaRepository : IGenericRepository<UserInfo>
    {
        Task<SystemDbStatus> UploadProfilePicture(int userId, string fileName);
        Task<SystemDbStatus> SaveFileName(string fileId, SystemSession session);
    }
}
