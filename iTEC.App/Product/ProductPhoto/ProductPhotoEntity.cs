using System.IO;
using API.Base.Files.Models.Entities;
using API.Base.Web.Base.Models.Entities;

namespace iTEC.App.Product.ProductPhoto
{
    public class ProductPhotoEntity : Entity
    {
        public ProductPhotoEntity()
        {
        }

        public ProductPhotoEntity(FileEntity file, ProductEntity product, bool isThumbnail = false)
        {
            File = file;
            Product = product;
            IsThumbnail = isThumbnail;
        }

        public FileEntity File { get; set; }
        public ProductEntity Product { get; set; }
        public bool IsThumbnail { get; set; }

        public override string ToString()
        {
            if (File == null || string.IsNullOrEmpty(File.SubDirectory) || string.IsNullOrEmpty(File.Name) ||
                string.IsNullOrEmpty(File.Extension))
            {
                return "nope :(";
            }

            File.Link = Path.Combine("/content", File.SubDirectory, File.Name + "." + File.Extension)
                .Replace("\\", "/");
            return "<a href=\"" + File.Link + "\" target=\"_blank\">link</a>";
        }
    }
}