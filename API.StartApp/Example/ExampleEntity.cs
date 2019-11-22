using System.ComponentModel.DataAnnotations;
using API.Base.Web.Base.Models.Entities;

namespace API.StartApp.Example
{
    public class ExampleEntity : Entity, IOrderedEntity, ISlugableEntity
    {
        [Display(Name = "Numele")] public string Name { get; set; }
        public int Age { get; set; }
        public int OrderIndex { get; set; }
        public string Slug { get; set; }
    }
}