using API.Base.Web.Base.Models.Entities;

namespace iTEC.App.Profile
{
    public abstract class BaseProfileEntity : Entity
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}