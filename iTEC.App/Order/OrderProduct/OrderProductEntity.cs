using API.Base.Web.Base.Models.Entities;
using iTEC.App.Product;
using iTEC.App.Product.Enums;

namespace iTEC.App.Order.OrderProduct
{
    public class OrderProductEntity : Entity
    {
        public OrderEntity Order { get; set; }
        public ProductEntity Product { get; set; }
        public float UnitPrice { get; set; }
        public float Quantity { get; set; }
        public SellingUnit Unit { get; set; }
    }
}