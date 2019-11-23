using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Base.Web.Base.Controllers.Api;
using API.Base.Web.Base.Database.DataLayer;
using API.Base.Web.Base.Exceptions;
using API.Base.Web.Base.Misc.PatchBag;
using AutoMapper;
using iTEC.App.Category;
using iTEC.App.Product.ProductCategory;
using iTEC.App.Profile.SellerProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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

        protected IDataLayer DataLayer => ServiceProvider.GetService<IDataLayer>();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            Repo.ChainQueryable(q => q
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
        public override Task<IActionResult> GetAll()
        {
            return base.GetAll();
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
                throw new KnownException("Invalid categories");
            }

            var setProdCatIds = await productCategoriesRepo.DbSet.Where(pc => pc.Product.Id == productId)
                .Select(pc => pc.Id).ToListAsync();
            foreach (var spcId in setProdCatIds) await productCategoriesRepo.Delete(spcId);

            foreach (var existingCategory in existingCategories)
            {
                await productCategoriesRepo.Add(new ProductCategoryEntity(product, existingCategory));
            }

            return Ok(existingCategories);
        }

        #endregion
    }
}