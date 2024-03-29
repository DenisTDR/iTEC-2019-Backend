using API.Base.Web.Base.Models.ViewModels;
using iTEC.App.Product;
using iTEC.App.Product.Enums;

namespace iTEC.App.Order
{
    public class CartItemViewModel : ViewModel
    {
        public ProductViewModel Product { get; set; }
        public float Quantity { get; set; }
    }
}