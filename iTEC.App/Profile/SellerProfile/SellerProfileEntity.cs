using API.Base.Web.Base.Auth.Models.Entities;
using iTEC.App.Address;
using iTEC.App.Profile.Enums;

namespace iTEC.App.Profile.SellerProfile
{
    public class SellerProfileEntity : BaseProfileEntity
    {
        public User User { get; set; }
        public BuyerType TargetType { get; set; }
        public AddressEntity Address { get; set; }
    }
}