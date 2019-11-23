using API.Base.Web.Base.Attributes.GenericForm;
using iTEC.App.Profile.Enums;

namespace iTEC.App.Profile.BuyerProfile
{
    public class BuyerProfileViewModel : BaseProfileViewModel
    {
        [FieldDefaultTexts] public BuyerType Type { get; set; }
    }
}