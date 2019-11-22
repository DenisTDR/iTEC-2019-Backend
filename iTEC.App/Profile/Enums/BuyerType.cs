using System.ComponentModel.DataAnnotations;

namespace iTEC.App.Profile.Enums
{
    public enum BuyerType
    {
        None,
        [Display(Name = "Persoană fizică")] Private,
        [Display(Name = "Persoană juridică")] Company
    }
}