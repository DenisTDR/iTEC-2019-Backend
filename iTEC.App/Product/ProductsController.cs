using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Base.Files.Models.Entities;
using API.Base.Files.Models.ViewModels;
using API.Base.Web.Base.Controllers.Api;
using API.Base.Web.Base.Database.DataLayer;
using API.Base.Web.Base.Exceptions;
using API.Base.Web.Base.Misc.PatchBag;
using AutoMapper;
using iTEC.App.Category;
using iTEC.App.Product.ProductCategory;
using iTEC.App.Product.ProductPhoto;
using iTEC.App.Profile.BuyerProfile;
using iTEC.App.Profile.SellerProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace iTEC.App.Product
{
    public class ProductsController : GenericCrudController<ProductEntity, ProductViewModel>
    {
        #region init

        private SellerProfileEntity _currentSellerProfile;

        protected SellerProfileEntity CurrentSellerProfile =>
            _currentSellerProfile ?? (_currentSellerProfile =
                DataLayer.Repo<SellerProfileEntity>().FindOne(s => s.User == CurrentUser).Result);

        private BuyerProfileEntity _currentBuyerProfile;

        protected BuyerProfileEntity CurrentBuyerProfile =>
            _currentBuyerProfile ?? (_currentBuyerProfile =
                DataLayer.Repo<BuyerProfileEntity>().FindOne(s => s.User == CurrentUser).Result);

        protected IDataLayer DataLayer => ServiceProvider.GetService<IDataLayer>();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            Repo.ChainQueryable(q => q
                .Include(p => p.Seller)
                .ThenInclude(s => s.Address)
                .Include(p => p.Categories)
                .ThenInclude(c => c.Category)
                .ThenInclude(c => c.Parent)
                .ThenInclude(c => c.Parent)
                .Include(p => p.Photos)
                .ThenInclude(pp => pp.File));
        }

        #endregion

        #region products

        [AllowAnonymous]
        public override Task<IActionResult> GetOne(string id)
        {
            return base.GetOne(id);
        }

        [AllowAnonymous]
        public override async Task<IActionResult> GetAll()
        {
            if (CurrentUserIfLoggedIn == null) return await base.GetAll();

            if (await UserManager.IsInRoleAsync(CurrentUserIfLoggedIn, "Seller"))
            {
                throw new KnownException("You can't see all products.");
            }

            if (await UserManager.IsInRoleAsync(CurrentUserIfLoggedIn, "Buyer"))
            {
                if (CurrentBuyerProfile != null)
                {
                    //filter for buyer type
                    var type = CurrentBuyerProfile.Type;
                    Repo.ChainQueryable(q => q.Where(p => p.Seller.TargetType == type));
                }

                return await base.GetAll();
            }

            throw new KnownException("Invalid roles");
        }

        [HttpGet]
        [Authorize(Roles = "Seller")]
        [ProducesResponseType(typeof(IEnumerable<ProductViewModel>), 200)]
        public async Task<IActionResult> GetOwn()
        {
            var e = await Repo.FindAll(p => p.Seller == CurrentSellerProfile);
            return Ok(Map(e));
        }

        [Authorize(Roles = "Seller")]
        public override async Task<IActionResult> Add(ProductViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var e = Mapper.Map<ProductEntity>(vm);
            e.Seller = CurrentSellerProfile;
            e = await Repo.Add(e);
            vm = Mapper.Map<ProductViewModel>(e);
            return Ok(vm);
        }


        [Authorize(Roles = "Seller")]
        public override async Task<IActionResult> Patch(ViewModelPatchBag<ProductViewModel> vmub)
        {
            var isHis = await Repo.Any(p => p.Id == vmub.Id && p.Seller == CurrentSellerProfile);

            if (!isHis)
            {
                throw new KnownException("Can't patch this product. It's not yours.");
            }

            return await base.Patch(vmub);
        }

        [Authorize(Roles = "Seller")]
        public override async Task<IActionResult> Delete(string id)
        {
            var isHis = await Repo.Any(p => p.Id == id && p.Seller == CurrentSellerProfile);

            if (!isHis)
            {
                throw new KnownException("Can't patch this product. It's not yours.");
            }

            return await base.Delete(id);
        }

        #endregion

        #region product categories

        [Authorize(Roles = "Seller")]
        [HttpPost("{productId}")]
        [ProducesResponseType(typeof(IList<CategoryViewModel>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SetCategories([FromBody] IList<CategoryViewModel> categories,
            [FromRoute] string productId)
        {
            if (categories == null)
            {
                return BadRequest();
            }

            var product = await Repo.FindOne(p => p.Id == productId && p.Seller == CurrentSellerProfile);
            if (product == null)
            {
                throw new KnownException("Can't touch this product. It's not yours.");
            }

            var productCategoriesRepo = DataLayer.Repo<ProductCategoryEntity>();
            var categoriesRepo = DataLayer.Repo<CategoryEntity>();
            var requestingIds = categories.Select(c => c?.Id);
            var existingCategories =
                (await categoriesRepo.FindAll(cat => requestingIds.Any(cId => cId == cat.Id))).ToList();
            if (existingCategories.Count != categories.Count)
            {
                throw new KnownException("Invalid category ids.");
            }

            var setProdCatIds = await productCategoriesRepo.DbSet.Where(pc => pc.Product.Id == productId)
                .Select(pc => pc.Id).ToListAsync();
            foreach (var spcId in setProdCatIds) await productCategoriesRepo.Delete(spcId);

            foreach (var existingCategory in existingCategories)
            {
                await productCategoriesRepo.Add(new ProductCategoryEntity(product, existingCategory));
            }

            return Ok(Mapper.Map<IEnumerable<CategoryViewModel>>(existingCategories));
        }

        #endregion

        #region product photos

        [Authorize(Roles = "Seller")]
        [HttpPost("{productId}")]
        [ProducesResponseType(typeof(IList<FileViewModel>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SetPhotos([FromBody] IList<FileViewModel> photos,
            [FromRoute] string productId, [FromQuery] [Required] string thumbnailId)
        {
            if (photos == null)
            {
                return BadRequest(ModelState);
            }

            var product = await Repo.FindOne(p => p.Id == productId && p.Seller == CurrentSellerProfile);
            if (product == null)
            {
                throw new KnownException("Can't touch this product. It's not yours.");
            }

            var productPhotoRepo = DataLayer.Repo<ProductPhotoEntity>();
            var filesRepo = DataLayer.Repo<FileEntity>();
            var requestingIds = photos.Select(c => c?.Id);
            var existingFiles =
                (await filesRepo.FindAll(cat => requestingIds.Any(cId => cId == cat.Id))).ToList();
            if (existingFiles.Count != photos.Count)
            {
                throw new KnownException("Invalid file ids.");
            }

            var setProdPhotoIds = await productPhotoRepo.DbSet.Where(pp => pp.Product.Id == productId)
                .Select(pc => pc.Id).ToListAsync();
            foreach (var spcId in setProdPhotoIds) await productPhotoRepo.Delete(spcId);
            foreach (var existingFile in existingFiles)
            {
                await productPhotoRepo.Add(new ProductPhotoEntity(existingFile, product,
                    existingFile.Id == thumbnailId));
            }

            return Ok(Mapper.Map<IEnumerable<FileViewModel>>(existingFiles));
        }

        [HttpPost]
        public IActionResult IgnoreThisEndpoint2([FromBody] ProductPhotoViewModel model)
        {
            return Ok();
        }

        #endregion

        #region helpers

        private void EnsureBuyerProfile()
        {
            if (CurrentBuyerProfile == null)
            {
                throw new KnownException("Nu ai un profil valid de cumpărător.");
            }
        }

        private void EnsureSellerProfile()
        {
            if (CurrentSellerProfile == null)
            {
                throw new KnownException("Nu ai un profil valid de vânzător.");
            }
        }

        #endregion
    }
}