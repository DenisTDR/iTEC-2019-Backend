using API.Base.Web.Base.Models.ViewModels;

namespace iTEC.App.Category
{
    public class CategoryViewModel : ViewModel
    {
        public string Name { get; set; }
        public CategoryViewModel Parent { get; set; }

        public string FullName => (Parent != null ? Parent.FullName + ": " : "") + Name;
    }
}