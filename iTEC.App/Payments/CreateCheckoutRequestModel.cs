using System.ComponentModel.DataAnnotations;
using API.Base.Web.Base.Auth.Models.HttpTransport;

namespace iTEC.App.Payments
{
    public class CreateCheckoutRequestModel : HttpTransportBaseType
    {
        [Required] public string OrderId { get; set; }
    }
}