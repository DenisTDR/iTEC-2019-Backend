using System.Threading.Tasks;
using API.Base.Web.Base.Controllers.Api;
using API.StartApp.Models.Entities;
using API.StartApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.StartApp.Controllers.API
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