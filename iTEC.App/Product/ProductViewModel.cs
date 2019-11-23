using System.Collections.Generic;
using API.Base.Web.Base.Attributes;
using API.Base.Web.Base.Attributes.GenericForm;
using API.Base.Web.Base.Models.ViewModels;
using iTEC.App.Category;
using iTEC.App.Product.Enums;
using iTEC.App.Product.ProductPhoto;
using iTEC.App.Profile.SellerProfile;
using Newtonsoft.Json;

namespace iTEC.App.Product
{
    public class ProductViewModel : ViewModel
    {
        [FieldDefaultTexts] public string Name { get; set; }

        [FieldDefaultTexts] public string Description { get; set; }

        [FieldDefaultTexts] public float Price { get; set; }
        [FieldDefaultTexts] public SellingUnit Unit { get; set; }

        [FieldDefaultTexts] public float AvailableUnits { get; set; }

        [IsReadOnly] public SellerProfileViewModel Seller { get; set; }

        [IsReadOnly] public string Thumbnail { get; set; }
        [IsReadOnly] public IList<string> Photos { get; set; }
        [IsReadOnly] public IList<CategoryViewModel> Categories { get; set; }
        [JsonIgnore] [IsReadOnly] public IList<ProductPhotoViewModel> PhotosVm { get; set; }
    }
}