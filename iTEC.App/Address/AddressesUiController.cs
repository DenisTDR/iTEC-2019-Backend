using API.Base.Web.Common.Controllers.Ui.Nv;

namespace iTEC.App.Address
{
    public class AddressesUiController : NvGenericUiController<AddressEntity>
    {
        protected override void SetListColumns()
        {
            AddListColumn(x => x.Address);
            AddListColumn(x => x.LocationLat);
            AddListColumn(x => x.LocationLong);
        }
    }
}