using BLL.Media;
using Model.Attribute;
using Model.Common;
using Model.Media;
using Model.Types;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Utility;

namespace API.Controllers.Media
{
    /// <summary>
    /// Provides APIs to handle requests related to media objects.
    /// </summary>
    [MetaData]
    public partial class MediaController : ApiController
    {
        private readonly IMediaService _service;

        /// <summary>
        /// Provides APIs to handle requests related to media objects.
        /// </summary>
        /// <param name="service"></param>
        public MediaController(IMediaService service)
        {
            _service = service;
        }

        /// <summary>
        /// Accepts data from user to update profile picture name.
        /// </summary>
        /// <param name="request">The file name.</param>
        /// <returns>Status of the operation.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Media_Post")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Post([FromBody] SingleData<string> request)
        {
            if (!Validation.Required(request.Data, x => request.Data, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _service.SaveFileName(request.Data, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse<string>(response);
        }

        /// <summary>
        /// Provides user with file url.
        /// </summary>
        /// <returns>File url.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Media_UrlPull")]
        [ActionName("UrlPull")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Post([FromBody] ImageRequest request)
        {
            var response = await _service.GetUrl(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Accepts data from user to delete files.
        /// </summary>
        /// <param name="request">The list of file names.</param>
        /// <returns>List of file IDs.</returns>
        [HttpDelete]
        [MetaData(markType: 3, aliasName: "Media_Delete")]
        [ResponseType(typeof(List<string>))]
        public async Task<HttpResponseMessage> Delete([FromBody] SingleData<List<string>> request)
        {
            if (!Validation.IsEnumerablePopulated(request.Data, x => request.Data, ActionContext, ModelState))
                return ActionContext.Response;

            var response = await _service.DeleteFiles(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Provides user with the list of file urls.
        /// </summary>
        /// <returns>List of file urls.</returns>
        [HttpGet]
        [MetaData(markType: 3, aliasName: "Media_Get")]
        [ResponseType(typeof(List<string>))]
        public async Task<HttpResponseMessage> Get()
        {
            var response = await _service.GetAllFiles(Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Accepts data from user to get detection of image on face.
        /// </summary>
        /// <param name="request">The request body.</param>
        /// <returns>The detection of image on face.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Media_FaceDetection")]
        [ResponseType(typeof(IEnumerable<ImageDetectionResponse>))]
        public async Task<HttpResponseMessage> FaceDetection([FromBody] ImageRequest request)
        {
            var response = await _service.GetDetection(request, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(SystemDbStatus.Selected, response);
        }

        /// <summary>
        /// Accepts data from user to crop the image.
        /// </summary>
        /// <param name="cropRequest">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData(markType: 3, aliasName: "Media_Crop")]
        [ResponseType(typeof(bool))]
        public async Task<HttpResponseMessage> Crop([FromBody] ImageCropRequest cropRequest)
        {
            var response = await _service.Crop(cropRequest, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse(response.Status, new { response.Data });
        }

        /// <summary>
        /// Accepts data from user to commit the image.
        /// </summary>
        /// <param name="cropRequest">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-07-01", markType: 1, aliasName: "Media_Commit")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Commit([FromBody] ImageCropRequest cropRequest)
        {
            var response = await _service.Commit(cropRequest, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse<string>(response.Status);
        }

        /// <summary>
        /// Accepts data from user to recrop the image.
        /// </summary>
        /// <param name="cropRequest">The request body.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-07-01", markType: 1, aliasName: "Media_ReCrop")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> ReCrop([FromBody] ImageCropRequest cropRequest)
        {
            var response = await _service.ReCrop(cropRequest, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse<string>(response.Status);
        }

        /// <summary>
        /// Accepts file ID for user to roll back the image.
        /// </summary>
        /// <param name="request">The file ID.</param>
        /// <returns>The status of the operation.</returns>
        [HttpPost]
        [MetaData("2015-07-01", markType: 1, aliasName: "Media_RollBack")]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> RollBack([FromBody] SingleData<string> request)
        {
            var response = await _service.RollBack(request.Data, Request.GetSession()).ConfigureAwait(false);
            return Request.SystemResponse<string>(response.Status);
        }

        ///// <summary>
        ///// Allows to change profile picture.
        ///// </summary>
        ///// <returns>The status of the operation.</returns>
        //[HttpPost]
        //[ActionName("UploadProfilePicture")]
        //[ResponseType(typeof(string))]
        //public async Task<HttpResponseMessage> UploadProfilePicture(int left = 0, int top = 0, int right = 0, int bottom = 0)
        //{
        //    var userId = Request.GetUserInfo<int>(SystemSessionEntity.UserId);
        //    var userName = Request.GetUserInfo<string>(SystemSessionEntity.UserName);

        //    if (Request.Content.IsMimeMultipartContent() && HttpContext.Current.Request.Files.Count > 0)
        //    {
        //        var postedFile = HttpContext.Current.Request.Files[0];
        //        var image = new WebImage(postedFile.InputStream);

        //        if (!Utility.Validation.Range(left, x => left, 0, image.Width, ActionContext, ModelState)
        //            || !Utility.Validation.Range(top, x => top, 0, image.Height, ActionContext, ModelState)
        //            || !Utility.Validation.Range(right, x => right, left + 1, image.Width, ActionContext, ModelState)
        //            ||
        //            !Utility.Validation.Range(bottom, x => bottom, top + 1, image.Height, ActionContext, ModelState)
        //            )
        //            return ActionContext.Response;

        //        var fileExtn = Path.GetExtension(postedFile.FileName).ToLower();
        //        //var root = HttpContext.Current.Server.MapPath("~/Images/");
        //        const string root = @"C://websites//iLoop//Images//Profile//";
        //        //var fileName = Path.Combine(SystemConstants.LocalImagePath,
        //        //    string.Format("{0}_{1}{2}", userName, userId, fileExtn));

        //        var fileName = Path.Combine(root,
        //           string.Format("{0}_{1}{2}", userName, userId, fileExtn));

        //        image.Crop(top, left, image.Height - bottom, image.Width - right);

        //        //var filePath = Path.Combine(root, postedFile.FileName);
        //        var filePath = Path.Combine(root, string.Format("{0}_{1}{2}", userName, userId, fileExtn));
        //        image.Save(filePath);

        //        //var thumbnailImagePath =
        //        //    SystemConstants.LocalImagePath + string.Format("{0}_{1}", userName, userId) + "_Thumbnail";
        //        var thumbnailImagePath =
        //           root + string.Format("{0}_{1}", userName, userId) + "_Thumbnail";

        //        //TODO: Access directory creation across server.
        //        if (!Directory.Exists(thumbnailImagePath))
        //            Directory.CreateDirectory(thumbnailImagePath);

        //        foreach (var size in (SystemImageSize[])Enum.GetValues(typeof(SystemImageSize)))
        //            ResizeCropSizes(thumbnailImagePath, fileExtn, fileName, size);

        //        var response = await _service.UploadProfilePicture(userId, string.Format("{0}_{1}{2}", userName, userId, fileExtn));

        //        return Request.SystemResponse<string>(response);
        //    }


        //    return Request.SystemResponse<string>(SystemDbStatus.NotSupported);
        //}

        //[NonAction]
        //private void ResizeCropSizes(string thumbnailImagePath, string fileExtn, string originalFilePath, SystemImageSize size)
        //{
        //    var savedimage = new WebImage(originalFilePath);
        //    // Create size
        //    savedimage.Resize((int)size, (int)size, true, false);
        //    var filesize56 = Path.Combine(thumbnailImagePath, (string.Format("{0}X{1}", ((int)size), ((int)size)) + fileExtn));
        //    savedimage.Save(filesize56);
        //}
    }
}
