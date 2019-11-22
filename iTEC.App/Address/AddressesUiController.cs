using API.Base.Web.Common.Controllers.Ui.Nv;
using iTEC.App.Profile;

namespace iTEC.App.Address
{
    public class AddressesUiController : NvGenericUiController<AddressEntity>
    {
        protected override void SetListColumns()
        {
            AddListColumn(x => x.Address);
            AddListColumn(x => x.LocationX);
            AddListColumn(x => x.LocationY);
        }
    }
}