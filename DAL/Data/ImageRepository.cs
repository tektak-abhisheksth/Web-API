using Entity;
using Model.Types;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using Utility;

namespace DAL.Data
{
    public static class ImageRepository
    {
        public static string GetEventGalleryPhoto(string eventPic)
        {
            string image;
            try
            {
                image = !string.IsNullOrEmpty(eventPic) ? Path.Combine(SystemConstants.EventGalleryLocalImagePath, eventPic) : String.Format("{0}{1}", SystemConstants.EventGalleryLocalImagePath, "default.png");
            }
            catch
            {
                image = String.Format("{0}{1}", SystemConstants.LocalImagePath, "default.png");
            }
            return (File.Exists(image)) ? image : null;
        }

        public static string GetGroupPhoto(string groupPic)
        {
            string image;
            try
            {
                image = !string.IsNullOrEmpty(groupPic) ? Path.Combine(SystemConstants.GroupLocalImagePath, groupPic) : String.Format("{0}{1}", SystemConstants.GroupLocalImagePath, "default.png");
            }
            catch
            {
                image = String.Format("{0}{1}", SystemConstants.LocalImagePath, "default.png");
            }
            return (File.Exists(image)) ? image : null;
        }

        public static string GetProductPhoto(string productPic)
        {
            string image;
            try
            {
                image = !string.IsNullOrEmpty(productPic) ? Path.Combine(SystemConstants.ProductLocalImagePath, productPic) : String.Format("{0}{1}", SystemConstants.ProductLocalImagePath, "default.png");
            }
            catch
            {
                image = String.Format("{0}{1}", SystemConstants.LocalImagePath, "default.png");
            }
            return (File.Exists(image)) ? image : null;
        }

        public static string GetProfilePhoto(UserInfo userInfo, SystemImageSize size = SystemImageSize.Size40)
        {
            return GetProfilePhoto(userInfo.Picture, (SystemUserType)userInfo.UserTypeID, size);
        }

        public static string GetProfilePhoto(string imageName, SystemUserType userTypeId, SystemImageSize size)
        {
            return GetProfilePhoto(imageName, userTypeId, (int)size);
        }

        public static string GetProfilePhoto(string userPic, SystemUserType usertypeId, int targetsize = 0)
        {
            if (!string.IsNullOrWhiteSpace(userPic))
                return targetsize != 0
                           ? GetThumbnailPath(Path.Combine(SystemConstants.LocalImagePath, userPic), targetsize)
                           : Path.Combine(SystemConstants.LocalImagePath, userPic);
            return GetDefaultThumbnailPath(targetsize, usertypeId);
        }

        public static void DeleteImageFromProductLocalPath(string image)
        {
            var originalFileName = string.Concat(SystemConstants.ProductLocalImagePath, image);
            var fn = HttpContext.Current.Server.MapPath(originalFileName);
            if (File.Exists(fn)) File.Delete(fn);
        }

        public static void DeleteImageFromLocalPath(string image)
        {
            var originalFileName = string.Concat(SystemConstants.GroupLocalImagePath, image);
            var fn = HttpContext.Current.Server.MapPath(originalFileName);
            if (File.Exists(fn)) File.Delete(fn);
        }

        private static string GetThumbnailPath(string photoPath, int targetsize)
        {
            var extension = Path.GetExtension(photoPath);
            var filename = Path.GetFileNameWithoutExtension(photoPath);
            var thumbnailFilePath = Path.Combine(SystemConstants.LocalImagePath, string.Format("{0}{1}", filename, "_Thumbnail"));
            switch ((SystemImageSize)targetsize)
            {
                case SystemImageSize.Size40:
                    return string.Format("{0}/{1}{2}", thumbnailFilePath, "40X40", extension);
                case SystemImageSize.Size56:
                    return string.Format("{0}/{1}{2}", thumbnailFilePath, "56X56", extension);
                case SystemImageSize.Size94:
                    return string.Format("{0}/{1}{2}", thumbnailFilePath, "94X94", extension);
                case SystemImageSize.Size170:
                    return string.Format("{0}/{1}{2}", thumbnailFilePath, "170X170", extension);
                default:
                    return photoPath;
            }
        }

        private static string GetDefaultThumbnailPath(int targetsize, SystemUserType usertypeId)
        {
            var prefixOnUserType = usertypeId == SystemUserType.Person ? "p" : "c";
            var perfixOnUserTypeextension = usertypeId == SystemUserType.Person ? ".jpg" : ".png";
            return targetsize != 0 ? string.Format("{0}/{1}/{2}{3}{4}", SystemConstants.LocalImagePath, "default_Thumbnail", prefixOnUserType, targetsize, perfixOnUserTypeextension) : String.Format("{0}{1}", SystemConstants.LocalImagePath, usertypeId == SystemUserType.Person ? "Defaultpersonal.jpg" : "Defaultcompany.png");
        }

        public static Image CropImage(Image image, int height, int width, int startAtX = 0, int startAtY = 0)
        {
            try
            {
                //check the image height against our desired image height
                if (image.Height < height) height = image.Height;

                if (image.Width < width) width = image.Width;

                //create a bitmap window for cropping
                var bmPhoto = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(72, 72);

                //create a new graphics object from our image and set properties
                var grPhoto = Graphics.FromImage(bmPhoto);
                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;

                //now do the crop
                grPhoto.DrawImage(image, new Rectangle(0, 0, width, height), startAtX, startAtY, width, height, GraphicsUnit.Pixel);

                // Save out to memory and get an image from it to send back out the method.
                var mm = new MemoryStream();
                bmPhoto.Save(mm, ImageFormat.Jpeg);
                image.Dispose();
                bmPhoto.Dispose();
                grPhoto.Dispose();
                var outImage = Image.FromStream(mm);

                return outImage;
            }
            catch (Exception ex)
            {
                throw new Exception("Error cropping image, the error was: " + ex.Message);
            }
        }

        //Hard resize attempts to resize as close as it can to the desired size and then crops the excess
        public static Image HardResizeImage(int width, int height, Image image)
        {
            var resized = width > height ? ResizeImage(width, width, image) : ResizeImage(height, height, image);
            var output = CropImage(resized, height, width);
            //return the original resized image
            return output;
        }

        //Image resizing
        public static Image ResizeImage(int maxWidth, int maxHeight, Image image)
        {
            var width = image.Width;
            var height = image.Height;
            if (width > maxWidth || height > maxHeight)
            {
                //The flips are in here to prevent any embedded image thumbnails -- usually from cameras
                //from displaying as the thumbnail image later, in other words, we want a clean
                //resize, not a grainy one.
                image.RotateFlip(RotateFlipType.Rotate180FlipX);
                image.RotateFlip(RotateFlipType.Rotate180FlipX);

                float ratio;
                if (width > height)
                {
                    ratio = width / (float)height;
                    width = maxWidth;
                    height = Convert.ToInt32(Math.Round(width / ratio));
                }
                else
                {
                    ratio = height / (float)width;
                    height = maxHeight;
                    width = Convert.ToInt32(Math.Round(height / ratio));
                }

                //return the resized image
                return image.GetThumbnailImage(width, height, null, IntPtr.Zero);
            }
            //return the original resized image
            return image;
        }


        public static bool FileIsWebFriendlyImage(Stream stream)
        {
            try
            {
                //Read an image from the stream...
                var i = Image.FromStream(stream);

                //Move the pointer back to the beginning of the stream
                stream.Seek(0, SeekOrigin.Begin);

                if (ImageFormat.Jpeg.Equals(i.RawFormat))
                    return true;
                return ImageFormat.Png.Equals(i.RawFormat) || ImageFormat.Gif.Equals(i.RawFormat);
            }
            catch
            {
                return false;
            }
        }

        public static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            var imageBytes = Convert.FromBase64String(base64String);
            var ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            var image = Image.FromStream(ms, true);
            return image;
        }
    }
}