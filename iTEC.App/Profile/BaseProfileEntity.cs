using API.Base.Web.Base.Auth.Models.Entities;
using API.Base.Web.Base.Models.Entities;
using iTEC.App.Address;

namespace iTEC.App.Profile
{
    public abstract class BaseProfileEntity : Entity
    {
        public User User { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public AddressEntity Address { get; set; }
    }
}