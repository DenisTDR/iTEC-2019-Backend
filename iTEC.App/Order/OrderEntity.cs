using System.Collections.Generic;
using API.Base.Web.Base.Attributes;
using API.Base.Web.Base.Models.Entities;
using iTEC.App.Order.OrderProduct;
using iTEC.App.Profile.BuyerProfile;
using iTEC.App.Profile.SellerProfile;

namespace iTEC.App.Order
{
    public class OrderEntity : Entity
    {
        public BuyerProfileEntity Buyer { get; set; }
        public float TotalPrice { get; set; }
        public bool Paid { get; set; }
        public string PaymentInformation { get; set; }

        [IsReadOnly] public IList<OrderProductEntity> Products { get; set; }

        public override string ToString()
        {
            return Buyer?.Name ?? "Buyer not loaded";
        }
    }
}