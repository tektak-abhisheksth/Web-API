using Model.Common;
using Model.Media;
using Model.Types;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TekTak.iLoop.Helper;
using TekTak.iLoop.Kauwa;

namespace TekTak.iLoop.Media
{
    public class MediaRepository : IMediaRepository
    {
        protected readonly Services Client;

        public MediaRepository(Services client)
        {
            Client = client;
        }

        public async Task<string> GetUrl(ImageRequest request, SystemSession session)
        {
            var serviceRequest = new Elif { FileId = request.FileId, AskWebp = request.AskWebp, IsProfilePic = request.IsProfilePicture, SizeCodes = (SizedCodes)request.SizeCode, Username = request.UserName };
            var response = await Task.Factory.StartNew(() => Client.ElifService.getUrl(serviceRequest, session.GetSession())).ConfigureAwait(false);
            //  return string.IsNullOrWhiteSpace(response) ? null : response;
            return response;
        }

        public async Task<List<string>> DeleteFiles(SingleData<List<string>> request, SystemSession session)
        {
            var serviceRequest = new List<Elif>();
            request.Data.ForEach(x => serviceRequest.Add(new Elif { FileId = x, Username = session.UserName }));
            var response = await Task.Factory.StartNew(() => Client.ElifService.deleteFiles(serviceRequest, session.GetSession())).ConfigureAwait(false);
            return response;
        }

        public async Task<List<string>> GetAllFiles(SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.ElifService.getMyFiles(session.GetSession())).ConfigureAwait(false);
            return response;
        }

        public async Task<IEnumerable<ImageDetectionResponse>> GetDetection(ImageRequest request, SystemSession session)
        {
            var serviceRequest = new Elif { FileId = request.FileId, Username = request.UserName };
            var response = await Task.Factory.StartNew(() => Client.ElifService.getDetection(serviceRequest, session.GetSession())).ConfigureAwait(false);
            var result = response.Select(x => new ImageDetectionResponse
            {
                X = x.X,
                Y = x.Y,
                Height = x.Height,
                Width = x.Width
            });
            return result;
        }

        ////public async Task<StatusData<bool>> Crop(ImageCropRequest cropRequest, SystemSession session)
        ////{
        ////    var serviceRequest = new Elif { FileId = cropRequest.FileId, AskWebp = cropRequest.AskWebp, Username = cropRequest.UserName };
        ////    var serviceCropRequest = new ElifImageCrop { Width = cropRequest.CropDetail.Width, Height = cropRequest.CropDetail.Height, Top = cropRequest.CropDetail.Top, Bottom = cropRequest.CropDetail.Bottom, Left = cropRequest.CropDetail.Left, Right = cropRequest.CropDetail.Right, SizeCodes = (SizedCodes)cropRequest.CropDetail.SizeCode };
        ////    var response = await Task.Factory.StartNew(() => Client.ElifService.doCrop(serviceRequest, serviceCropRequest, session.GetSession())).ConfigureAwait(false);
        ////    var result = new StatusData<bool> { Status = response ? SystemDbStatus.Updated : SystemDbStatus.NotModified, Data = response };
        ////    return result;
        ////}

        public async Task<StatusData<bool>> Crop(ImageCropRequest cropRequest, SystemSession session)
        {
            var result = new StatusData<bool> { Status = SystemDbStatus.Updated };
            var serviceRequest = new Elif { FileId = cropRequest.FileId, AskWebp = cropRequest.AskWebp, Username = cropRequest.UserName };
            var serviceCropRequest = new ElifImageCrop { Width = cropRequest.CropDetail.Width, Height = cropRequest.CropDetail.Height, Top = cropRequest.CropDetail.Top, Bottom = cropRequest.CropDetail.Bottom, Left = cropRequest.CropDetail.Left, Right = cropRequest.CropDetail.Right, SizeCodes = (SizedCodes)cropRequest.CropDetail.SizeCode };
            result.Data = await Task.Factory.StartNew(() => Client.ElifService.doCrop(serviceRequest, serviceCropRequest, session.GetSession())).ConfigureAwait(false);
            return result;
        }

        public async Task<StatusData<string>> Commit(ImageCropRequest cropRequest, SystemSession session)
        {
            var serviceCropRequest = new ElifImageCrop { Width = cropRequest.CropDetail.Width, Height = cropRequest.CropDetail.Height, Top = cropRequest.CropDetail.Top, Bottom = cropRequest.CropDetail.Bottom, Left = cropRequest.CropDetail.Left, Right = cropRequest.CropDetail.Right, SizeCodes = (SizedCodes)cropRequest.CropDetail.SizeCode };
            var response = await Task.Factory.StartNew(() => Client.ElifService.commit(cropRequest.FileId, serviceCropRequest, session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = response ? SystemDbStatus.Updated : SystemDbStatus.NotModified };
            return result;
        }

        public async Task<StatusData<string>> ReCrop(ImageCropRequest cropRequest, SystemSession session)
        {
            var serviceCropRequest = new ElifImageCrop { Width = cropRequest.CropDetail.Width, Height = cropRequest.CropDetail.Height, Top = cropRequest.CropDetail.Top, Bottom = cropRequest.CropDetail.Bottom, Left = cropRequest.CropDetail.Left, Right = cropRequest.CropDetail.Right, SizeCodes = (SizedCodes)cropRequest.CropDetail.SizeCode };
            var response = await Task.Factory.StartNew(() => Client.ElifService.reCrop(cropRequest.FileId, serviceCropRequest, session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = response ? SystemDbStatus.Updated : SystemDbStatus.NotModified };
            return result;
        }
        public async Task<StatusData<string>> RollBack(string fileId, SystemSession session)
        {
            var response = await Task.Factory.StartNew(() => Client.ElifService.rollback(fileId, session.GetSession())).ConfigureAwait(false);
            var result = new StatusData<string> { Status = response ? SystemDbStatus.Updated : SystemDbStatus.NotModified };
            return result;
        }
    }
}
