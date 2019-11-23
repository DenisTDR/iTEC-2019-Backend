using System.Threading.Tasks;
using API.Base.Web.Base.Auth.Controllers;
using API.Base.Web.Base.Auth.Jwt;
using API.Base.Web.Base.Auth.Models.Entities;
using API.Base.Web.Base.Auth.Models.HttpTransport;
using API.Base.Web.Base.Helpers;
using iTEC.App.Profile.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Internal;

namespace iTEC.App.Auth
{
    public class AuthController : AuthBasicController<LoginRequestModel>
    {
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtFactory jwtFactory,
            IEmailHelper emailHelper) : base(userManager, signInManager, jwtFactory, emailHelper)
        {
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public override async Task<IActionResult> Register(RegisterRequestModel model, string pathPrefix)
        {
            await Task.Delay(500);
            return NotFound();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterShop([FromBody] ShopRegisterRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            model.FirstName = null;
            model.LastName = null;
            var response = await base.Register(model, "/");
            var role = model.Type.ToString();
            var user = await UserManager.FindByEmailAsync(model.Email);
            var roleResult = await UserManager.AddToRoleAsync(user, role);

            return response;
        }
    }
}