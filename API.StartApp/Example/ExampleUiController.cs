using System.Threading.Tasks;
using API.Base.Web.Base.Controllers.Ui;
using API.Base.Web.Base.Database.Repository.Helpers;
using API.Base.Web.Common.Controllers.Ui.Nv;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.StartApp.Example
{
    public class ExampleUiController : NvGenericUiController<ExampleEntity>
    {
    
    }
}