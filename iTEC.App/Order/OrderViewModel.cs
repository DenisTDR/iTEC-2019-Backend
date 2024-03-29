using System.Collections.Generic;
using API.Base.Web.Base.Models.ViewModels;
using iTEC.App.Order.Enums;
using iTEC.App.Order.OrderProduct;
using iTEC.App.Profile.SellerProfile;

namespace iTEC.App.Order
{
    public class OrderViewModel : ViewModel
    {
        public SellerProfileViewModel Seller { get; set; }
        public float TotalPrice { get; set; }
        public OrderState State { get; set; }
        public string PaymentInformation { get; set; }
        public List<OrderProductViewModel> Products { get; set; }
    }
}