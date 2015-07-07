using System.ComponentModel.DataAnnotations;
using Model.Attribute;
using Model.Base;
using Model.Types;

namespace Model.Media
{
    public class ImageRequest : RequestBase
    {
        [Required]
        public string FileId { get; set; }

        public bool AskWebp { get; set; }

        [ValidEnum]
        public SystemImageSizeCode SizeCode { get; set; }

        public bool IsProfilePicture { get; set; }

        public string UserName { get; set; }
    }

    public class ImageCropRequest : RequestBase
    {
        [Required]
        public string FileId { get; set; }

        public bool AskWebp { get; set; }

        public string UserName { get; set; }

        [Required]
        public ImageCropDetailRequest CropDetail { get; set; }
    }

    public class ImageCropDetailRequest
    {
        [Range(1, 100000)]
        public double Width { get; set; }
        [Range(1, 100000)]
        public double Height { get; set; }
        [Range(1, 100000)]
        public double Top { get; set; }
        // [Range(1, 100000)]
        public double Bottom { get; set; }
        public double Left { get; set; }
        public double Right { get; set; }
        [ValidEnum]
        public SystemImageSizeCode SizeCode { get; set; }
    }
}
