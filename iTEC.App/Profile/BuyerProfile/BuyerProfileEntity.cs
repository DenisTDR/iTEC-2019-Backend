using API.Base.Web.Base.Auth.Models.Entities;
using iTEC.App.Address;
using iTEC.App.Profile.Enums;

namespace iTEC.App.Profile.BuyerProfile
{
    public class BuyerProfileEntity : BaseProfileEntity
    {
        public User User { get; set; }
        public BuyerType Type { get; set; }

        public AddressEntity Address { get; set; }
    }
}