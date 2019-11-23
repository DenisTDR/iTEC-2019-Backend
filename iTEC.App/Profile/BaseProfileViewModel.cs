using API.Base.Web.Base.Attributes.GenericForm;
using API.Base.Web.Base.Models.ViewModels;
using iTEC.App.Address;

namespace iTEC.App.Profile
{
    public class BaseProfileViewModel : ViewModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [FieldDefaultTexts] public AddressViewModel Address { get; set; }
    }
}