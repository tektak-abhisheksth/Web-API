using Model.Common;
using Model.Media;
using Model.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TekTak.iLoop.Media
{
    public interface IMediaRepository
    {
        Task<string> GetUrl(ImageRequest request, SystemSession session);
        Task<List<string>> DeleteFiles(SingleData<List<string>> request, SystemSession session);
        Task<List<string>> GetAllFiles(SystemSession session);
        Task<IEnumerable<ImageDetectionResponse>> GetDetection(ImageRequest request, SystemSession session);
        Task<StatusData<bool>> Crop(ImageCropRequest cropRequest, SystemSession session);
        Task<StatusData<string>> Commit(ImageCropRequest cropRequest, SystemSession session);
        Task<StatusData<string>> ReCrop(ImageCropRequest cropRequest, SystemSession session);
        Task<StatusData<string>> RollBack(string fileId, SystemSession session);
    }
}
