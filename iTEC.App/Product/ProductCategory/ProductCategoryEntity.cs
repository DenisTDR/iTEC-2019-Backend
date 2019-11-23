using API.Base.Web.Base.Models.Entities;
using iTEC.App.Category;

namespace iTEC.App.Product.ProductCategory
{
    public class ProductCategoryEntity : Entity
    {
        public ProductCategoryEntity()
        {
        }

        public ProductCategoryEntity(ProductEntity product, CategoryEntity category)
        {
            Product = product;
            Category = category;
        }

        public ProductEntity Product { get; set; }
        public CategoryEntity Category { get; set; }

        public override string ToString()
        {
            return Category.ToString();
        }
    }
}