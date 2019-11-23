using API.Base.Web.Base.Models.ViewModels;
using iTEC.App.Product;
using iTEC.App.Product.Enums;

namespace iTEC.App.Order.OrderProduct
{
    public class OrderProductViewModel : ViewModel
    {
        public ProductViewModel Product { get; set; }
        public float UnitPrice { get; set; }
        public float Quantity { get; set; }
        public SellingUnit Unit { get; set; }
    }
}