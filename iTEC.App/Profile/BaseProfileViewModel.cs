using API.Base.Web.Base.Attributes;
using API.Base.Web.Base.Attributes.GenericForm;
using API.Base.Web.Base.Models.ViewModels;
using iTEC.App.Address;

namespace iTEC.App.Profile
{
    public class BaseProfileViewModel : ViewModel
    {
        [FieldDefaultTexts] public string Name { get; set; }
        [FieldDefaultTexts] public string PhoneNumber { get; set; }
        [IsReadOnly] public AddressViewModel Address { get; set; }
    }
}