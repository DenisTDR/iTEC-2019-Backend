using API.Base.Web.Common.Controllers.Ui.Nv;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace iTEC.App.Product.ProductCategory
{
    public class ProductCategoriesUiController : NvGenericUiController<ProductCategoryEntity>
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            Repo.ChainQueryable(q => q
                .Include(pc => pc.Category)
                .ThenInclude(c => c.Parent).ThenInclude(c => c.Parent)
                .Include(pc => pc.Product));
        }
    }
}