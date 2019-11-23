using API.Base.Web.Common.Controllers.Ui.Nv;
using iTEC.App.Product;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace iTEC.App.Order
{
    public class OrdersUiController : NvGenericUiController<OrderEntity>
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            Repo.ChainQueryable(q => q.Include(o => o.Buyer));
        }

        protected override void SetListColumns()
        {
            AddListColumn(o => o.Buyer);
            AddListColumn(o => o.TotalPrice);
            AddListColumn(o => o.Paid);
        }
    }
}