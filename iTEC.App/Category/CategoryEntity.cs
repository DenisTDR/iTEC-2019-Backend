using API.Base.Web.Base.Models.Entities;

namespace iTEC.App.Category
{
    public class CategoryEntity : Entity
    {
        public string Name { get; set; }
        public CategoryEntity Parent { get; set; }

        public override string ToString()
        {
            return (Parent != null ? Parent + "->" : "") + Name;
        }
    }
}