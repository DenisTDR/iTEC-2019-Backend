using System;
using System.Threading.Tasks;
using API.Base.Web.Base.Controllers.Api;
using API.Base.Web.Base.Database.DataLayer;
using API.Base.Web.Base.Database.Repository;
using API.Base.Web.Base.Exceptions;
using API.Base.Web.Base.Misc.PatchBag;
using AutoMapper;
using iTEC.App.Address;
using iTEC.App.Profile.BuyerProfile;
using iTEC.App.Profile.SellerProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace iTEC.App.Profile
{
    public class ProfilesController : ApiController
    {
        protected IDataLayer DataLayer => ServiceProvider.GetService<IDataLayer>();
        protected IRepository<SellerProfileEntity> SellersRepo => DataLayer.Repo<SellerProfileEntity>();
        protected IRepository<BuyerProfileEntity> BuyersRepo => DataLayer.Repo<BuyerProfileEntity>();
        protected IRepository<AddressEntity> AddressesRepo => DataLayer.Repo<AddressEntity>();

        [Authorize(Roles = "Buyer, Seller")]
        [HttpGet]
        [ProducesResponseType(typeof(BuyerProfileViewModel), 200)]
        [ProducesResponseType(typeof(SellerProfileViewModel), 201)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetOwn()
        {
            if (await UserManager.IsInRoleAsync(CurrentUser, "Seller"))
            {
                return await GetProfile<SellerProfileViewModel, SellerProfileEntity>(SellersRepo);
            }

            if (await UserManager.IsInRoleAsync(CurrentUser, "Buyer"))
            {
                return await GetProfile<BuyerProfileViewModel, BuyerProfileEntity>(BuyersRepo);
            }

            return BadRequest();
        }

        private async Task<IActionResult> GetProfile<TVm, TE>(IRepository<TE> repo) where TVm : BaseProfileViewModel
            where TE : BaseProfileEntity
        {
            var profile = await repo.FindOne(p => p.User == CurrentUser);
            if (profile == null)
            {
                return NotFound();
            }

            var vm = Mapper.Map<TVm>(profile);
            return Ok(vm);
        }

        [HttpPatch]
        public async Task<IActionResult> SaveSellerProfile([FromBody] ViewModelPatchBag<SellerProfileViewModel> model)
        {
            return await SaveProfile(model, SellersRepo);
        }

        [HttpPatch]
        public async Task<IActionResult> SaveBuyerProfile([FromBody] ViewModelPatchBag<BuyerProfileViewModel> model)
        {
            return await SaveProfile(model, BuyersRepo);
        }

        private async Task<IActionResult> SaveProfile<TVm, TE>(ViewModelPatchBag<TVm> mpb, IRepository<TE> repo)
            where TVm : BaseProfileViewModel
            where TE : BaseProfileEntity
        {
            if (mpb.Model == null)
            {
                throw new KnownException("Invalid request object.");
            }
            var existing = await repo.FindOne(p => p.User == CurrentUser);
            TE e;
            if (existing == null)
            {
                e = Mapper.Map<TE>(mpb.Model);
                e.User = CurrentUser;
                e = await repo.Add(e);
            }
            else
            {
                repo.ChainQueryable(q => q.Include(p => p.Address));
                var eub = Mapper.Map<EntityPatchBag<TE>>(mpb);
                eub.Id = existing.Id;
                e = await repo.Patch(eub);
            }

            return Ok(Mapper.Map<TVm>(e));
        }

        [HttpPatch]
        public async Task<IActionResult> SaveAddress([FromBody] ViewModelPatchBag<AddressViewModel> address)
        {
            if (await UserManager.IsInRoleAsync(CurrentUser, "Seller"))
            {
                return await SaveAddress(address, SellersRepo);
            }

            if (await UserManager.IsInRoleAsync(CurrentUser, "Buyer"))
            {
                return await SaveAddress(address, BuyersRepo);
            }

            return BadRequest();
        }

        private async Task<IActionResult> SaveAddress<TE>(ViewModelPatchBag<AddressViewModel> mpb,
            IRepository<TE> repo)
            where TE : BaseProfileEntity
        {
            repo.ChainQueryable(q => q.Include(p => p.Address));
            var profile = await repo.FindOne(p => p.User == CurrentUser);
            AddressEntity e;
            if (profile == null)
            {
                throw new KnownException("You must complete your profile first.");
            }

            if (profile.Address == null)
            {
                e = Mapper.Map<AddressEntity>(mpb.Model);
                e = await AddressesRepo.Add(e);
                profile.Address = e;
                await DataLayer.SaveChangesAsync();
            }
            else
            {
                repo.ChainQueryable(q => q.Include(p => p.Address));
                Console.WriteLine(JsonConvert.SerializeObject(mpb));
                var eub = Mapper.Map<EntityPatchBag<AddressEntity>>(mpb);
                eub.Id = profile.Address.Id;
                if (eub.PropertiesToUpdate["location"])
                {
                    eub.PropertiesToUpdate["locationLat"] = eub.PropertiesToUpdate["locationLong"] = true;
                }
                Console.WriteLine(JsonConvert.SerializeObject(eub));
                e = await AddressesRepo.Patch(eub);
            }

            return Ok(Mapper.Map<AddressViewModel>(e));
        }
    }
}