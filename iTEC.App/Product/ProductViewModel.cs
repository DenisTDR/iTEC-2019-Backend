using System.Collections.Generic;
using API.Base.Web.Base.Models.ViewModels;
using iTEC.App.Category;
using iTEC.App.Product.Enums;
using iTEC.App.Profile.SellerProfile;

namespace iTEC.App.Product
{
    public class ProductViewModel : ViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public float Price { get; set; }
        public SellingUnit Unit { get; set; }

        public SellerProfileViewModel Seller { get; set; }
        public float AvailableUnits { get; set; }

        public string Thumbnail { get; set; }
        public IList<string> Photos { get; set; }
        public IList<CategoryViewModel> Categories { get; set; }
    }
}