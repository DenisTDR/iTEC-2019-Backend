using API.Base.Web.Base.Attributes;
using API.Base.Web.Base.Attributes.GenericForm;
using API.Base.Web.Base.Auth.Models.HttpTransport;
using iTEC.App.Profile.Enums;

namespace iTEC.App.Auth
{
    public class ShopRegisterRequestModel : RegisterRequestModel
    {
        [RequireNotDefault(typeof(BuyerType))]
        [FieldDefaultTexts]
        public ProfileType Type { get; set; }

        [IsReadOnly] public new string FirstName { get; set; }

        [IsReadOnly] public new string LastName { get; set; }
    }
}