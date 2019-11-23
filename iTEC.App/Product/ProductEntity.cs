using System.Collections.Generic;
using API.Base.Web.Base.Attributes;
using API.Base.Web.Base.Models.Entities;
using iTEC.App.Product.Enums;
using iTEC.App.Product.ProductCategory;
using iTEC.App.Product.ProductPhoto;
using iTEC.App.Profile.SellerProfile;

namespace iTEC.App.Product
{
    public class ProductEntity : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public float Price { get; set; }
        public SellingUnit Unit { get; set; }

        public SellerProfileEntity Seller { get; set; }
        public float AvailableUnits { get; set; }
        [IsReadOnly] public IList<ProductPhotoEntity> Photos { get; set; }
        [IsReadOnly] public IList<ProductCategoryEntity> Categories { get; set; }

        public override string ToString()
        {
            return (Seller != null ? Seller + ": " : "") + Name;
        }
    }
}