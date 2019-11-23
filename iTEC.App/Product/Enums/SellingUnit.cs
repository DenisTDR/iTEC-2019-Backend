using System.ComponentModel.DataAnnotations;

namespace iTEC.App.Product.Enums
{
    public enum SellingUnit
    {
        None,
        [Display(Name = "Kilogram")]
        Kg,
        [Display(Name = "Litru")]
        Liter,
        [Display(Name = "Bucată")]
        Item
    }
}