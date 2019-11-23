using API.Base.Web.Common.Controllers.Ui.Nv;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace iTEC.App.Product
{
    public class ProductsUiController : NvGenericUiController<ProductEntity>
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
                .ThenInclude(pp => pp.File)
                .Include(p => p.Seller));
        }
    }
}