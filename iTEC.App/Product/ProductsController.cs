using System.Threading.Tasks;
using API.Base.Web.Base.Controllers.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace iTEC.App.Product
{
    public class ProductsController : GenericCrudController<ProductEntity, ProductViewModel>
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            Repo.ChainQueryable(q => q
                .Include(p => p.Categories)
                .ThenInclude(c => c.Category)
                .ThenInclude(c => c.Parent)
                .ThenInclude(c => c.Parent)
                .Include(p => p.Photos)
                .ThenInclude(pp => pp.File));
        }

        [AllowAnonymous]
        public override Task<IActionResult> GetOne(string id)
        {
            return base.GetOne(id);
        }

        [AllowAnonymous]
        public override Task<IActionResult> GetAll()
        {
            return base.GetAll();
        }
    }
}