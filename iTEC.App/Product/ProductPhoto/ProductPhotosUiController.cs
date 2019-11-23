using API.Base.Web.Common.Controllers.Ui.Nv;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace iTEC.App.Product.ProductPhoto
{
    public class ProductPhotosUiController : NvGenericUiController<ProductPhotoEntity>
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            Repo.ChainQueryable(q => q.Include(pp => pp.File).Include(pp => pp.Product));
        }
    }
}