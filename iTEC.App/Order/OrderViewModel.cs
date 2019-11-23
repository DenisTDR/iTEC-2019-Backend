using API.Base.Web.Base.Models.ViewModels;
using iTEC.App.Profile.SellerProfile;

namespace iTEC.App.Order
{
    public class OrderViewModel : ViewModel
    {
        public SellerProfileViewModel Seller { get; set; }
        public float TotalPrice { get; set; }
        public bool Paid { get; set; }
        public string PaymentInformation { get; set; }
    }
}