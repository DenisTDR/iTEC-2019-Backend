using API.Base.Web.Common.Controllers.Ui.Nv;
using iTEC.App.Product;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace iTEC.App.Order.OrderProduct
{
    public class OrderProductsUiController : NvGenericUiController<OrderProductEntity>
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            DataLayer.Repo<OrderEntity>().ChainQueryable(q => q.Include(o => o.Buyer));
            DataLayer.Repo<ProductEntity>().ChainQueryable(q => q.Include(s => s.Seller));
            Repo.ChainQueryable(q => q
                .Include(op => op.Order)
                .ThenInclude(op => op.Buyer)
                .Include(op => op.Product)
                .ThenInclude(op => op.Seller)
            );
        }
    }
}