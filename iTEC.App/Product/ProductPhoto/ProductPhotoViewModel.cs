using System.ComponentModel.DataAnnotations;
using API.Base.Files.Models.ViewModels;
using API.Base.Web.Base.Attributes;
using API.Base.Web.Base.Attributes.GenericForm;
using API.Base.Web.Base.Models.ViewModels;

namespace iTEC.App.Product.ProductPhoto
{
    public class ProductPhotoViewModel : ViewModel
    {
        [Required] [FieldDefaultTexts] public FileViewModel File { get; set; }
        [IsReadOnly] public ProductViewModel Product { get; set; }
        [IsReadOnly] public bool IsThumbnail { get; set; }
    }
}