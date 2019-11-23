using API.Base.Web.Base.Attributes.GenericForm;
using iTEC.App.Profile.Enums;

namespace iTEC.App.Profile.SellerProfile
{
    public class SellerProfileViewModel : BaseProfileViewModel
    {
        [FieldDefaultTexts] public BuyerType TargetType { get; set; }
    }
}