using System.ComponentModel.DataAnnotations;
using API.Base.Web.Base.Models.ViewModels;

namespace iTEC.App.Address
{
    public class AddressViewModel : ViewModel
    {
        [DataType(DataType.MultilineText)] public string Address { get; set; }

        public GeoLocation Location { get; set; }
    }
}