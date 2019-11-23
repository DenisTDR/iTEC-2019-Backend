using System.ComponentModel.DataAnnotations;

namespace iTEC.App.Profile.Enums
{
    public enum ProfileType
    {
        None,
        [Display(Name = "Cumpărător")] Buyer,
        [Display(Name = "Vânzător")] Seller,
    }
}