using API.Base.Web.Base.Models.ViewModels;
using iTEC.App.Category;

namespace iTEC.App.Product.ProductCategory
{
    public class ProductCategoryViewModel : ViewModel
    {
        public ProductViewModel Product { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}