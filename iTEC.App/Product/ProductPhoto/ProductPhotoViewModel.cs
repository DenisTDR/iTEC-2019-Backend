using API.Base.Files.Models.ViewModels;
using API.Base.Web.Base.Models.ViewModels;

namespace iTEC.App.Product.ProductPhoto
{
    public class ProductPhotoViewModel : ViewModel
    {
        public FileViewModel File { get; set; }
        public ProductViewModel Product { get; set; }
    }
}