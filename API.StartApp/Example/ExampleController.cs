using System.Threading.Tasks;
using API.Base.Web.Base.Controllers.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.StartApp.Example
{
    public class ExampleController : GenericCrudController<ExampleEntity, ExampleViewModel>
    {
        [AllowAnonymous]
        public override Task<IActionResult> GetAll()
        {
            return base.GetAll();
        }
    }
}