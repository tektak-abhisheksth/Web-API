using DAL.DbEntity;
using Model.Common;
using Model.Media;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Media
{
    public class MediaService : IMediaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TekTak.iLoop.UOW.IUnitOfWork _jUnitOfWork;

        public MediaService(IUnitOfWork unitOfWork, TekTak.iLoop.UOW.IUnitOfWork jUnitOfWork)
        {
            _unitOfWork = unitOfWork;
            _jUnitOfWork = jUnitOfWork;
        }

        //public async Task<SystemDbStatus> UploadProfilePicture(int userId, string fileName)
        //{
        //    var response = await _unitOfWork.Media.UploadProfilePicture(userId, fileName);
        //    await _unitOfWork.CommitAsync();
        //    return response;
        //}

        public Task<string> GetUrl(ImageRequest request, SystemSession session)
        {
            return _jUnitOfWork.Media.GetUrl(request, session);
        }

        public Task<List<string>> DeleteFiles(SingleData<List<string>> request, SystemSession session)
        {
            return _jUnitOfWork.Media.DeleteFiles(request, session);
        }

        public Task<List<string>> GetAllFiles(SystemSession session)
        {
            return _jUnitOfWork.Media.GetAllFiles(session);
        }

        public Task<IEnumerable<ImageDetectionResponse>> GetDetection(ImageRequest request, SystemSession session)
        {
            return _jUnitOfWork.Media.GetDetection(request, session);
        }

        public Task<StatusData<bool>> Crop(ImageCropRequest cropRequest, SystemSession session)
        {
            return _jUnitOfWork.Media.Crop(cropRequest, session);
        }

        public Task<StatusData<string>> Commit(ImageCropRequest cropRequest, SystemSession session)
        {
            return _jUnitOfWork.Media.Commit(cropRequest, session);
        }

        public Task<StatusData<string>> ReCrop(ImageCropRequest cropRequest, SystemSession session)
        {
            return _jUnitOfWork.Media.ReCrop(cropRequest, session);
        }

        public Task<StatusData<string>> RollBack(string fileId, SystemSession session)
        {
            return _jUnitOfWork.Media.RollBack(fileId, session);
        }
        public async Task<SystemDbStatus> SaveFileName(string fileId, SystemSession session)
        {
            var status = await _unitOfWork.Media.SaveFileName(fileId, session);
            await _unitOfWork.CommitAsync();
            return status;
        }
    }
}
