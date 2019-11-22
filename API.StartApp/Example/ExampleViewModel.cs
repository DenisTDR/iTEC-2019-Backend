using API.Base.Web.Base.Models.ViewModels;

namespace API.StartApp.Example
{
    public class ExampleViewModel : ViewModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Slug { get; set; }
    }
}