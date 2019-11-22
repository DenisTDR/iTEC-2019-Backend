using API.Base.Web.Common.Controllers.Ui.Nv;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace iTEC.App.Profile.BuyerProfile
{
    public class BuyerProfilesUiController : NvGenericUiController<BuyerProfileEntity>
    {
        public BuyerProfilesUiController()
        {
            UseInheritedProperties = true;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            Repo.ChainQueryable(q => q.Include(b => b.User).Include(b => b.Address));
        }

        protected override void SetListColumns()
        {
            AddListColumn(x => x.User);
            AddListColumn(x => x.Address);
            AddListColumn(x => x.Name);
            AddListColumn(x => x.PhoneNumber);
            AddListColumn(x => x.Type);
        }
    }
}