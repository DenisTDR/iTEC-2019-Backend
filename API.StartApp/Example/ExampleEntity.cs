using System.ComponentModel.DataAnnotations;
using API.Base.Web.Base.Models.Entities;

namespace API.StartApp.Models.Entities
{
    public class ExampleEntity : Entity
    {
        [Display(Name = "Numele")]
        public string Name { get; set; }
        public int Age { get; set; }
    }
}