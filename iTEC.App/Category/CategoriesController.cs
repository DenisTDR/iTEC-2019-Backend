using API.Base.Web.Base.Controllers.Api;
using Microsoft.AspNetCore.Authorization;

namespace iTEC.App.Category
{
    [AllowAnonymous]
    public class CategoriesController : GenericReadOnlyController<CategoryEntity, CategoryViewModel>
    {
    }
}