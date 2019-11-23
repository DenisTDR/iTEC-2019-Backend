using System.Net;
using System.Threading.Tasks;
using API.Base.Web.Base.Controllers.Api;
using API.Base.Web.Base.Database.DataLayer;
using API.Base.Web.Base.Database.Repository;
using AutoMapper;
using iTEC.App.Profile.BuyerProfile;
using iTEC.App.Profile.SellerProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace iTEC.App.Profile
{
    public class ProfilesController : ApiController
    {
        protected IDataLayer DataLayer => ServiceProvider.GetService<IDataLayer>();
        protected IRepository<SellerProfileEntity> SellersRepo => DataLayer.Repo<SellerProfileEntity>();
        protected IRepository<BuyerProfileEntity> BuyersRepo => DataLayer.Repo<BuyerProfileEntity>();

        [Authorize(Roles = "Buyer, Seller")]
        [HttpGet]
        [ProducesResponseType(typeof(BuyerProfileViewModel), 200)]
        [ProducesResponseType(typeof(SellerProfileViewModel), 201)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOwn()
        {
            if (await UserManager.IsInRoleAsync(CurrentUser, "Seller"))
            {
                var profile = await SellersRepo.FindOne(p => p.User == CurrentUser);
                if (profile == null)
                {
                    return NotFound();
                }

                var vm = Mapper.Map<SellerProfileViewModel>(profile);
                return Ok(vm);
            }

            if (await UserManager.IsInRoleAsync(CurrentUser, "Buyer"))
            {
                var profile = await BuyersRepo.FindOne(p => p.User == CurrentUser);
                if (profile == null)
                {
                    return NotFound();
                }

                var vm = Mapper.Map<BuyerProfileViewModel>(profile);
                return Ok(vm);
            }

            return BadRequest();
        }

        protected async Task<BaseProfileEntity> GetProfile<T>() where T : BaseProfileEntity
        {
            var repo = DataLayer.Repo<T>();
            return await repo.FindOne(p => p.User == CurrentUser);
        }
    }
}