using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Base.Web.Base.Controllers.Api;
using API.Base.Web.Base.Database.DataLayer;
using API.Base.Web.Base.Database.Repository;
using API.Base.Web.Base.Exceptions;
using iTEC.App.Product;
using iTEC.App.Product.Enums;
using iTEC.App.Profile.BuyerProfile;
using iTEC.App.Profile.SellerProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace iTEC.App.Order
{
    public class OrdersController : ApiController
    {
        protected IDataLayer DataLayer => ServiceProvider.GetService<IDataLayer>();
        protected IRepository<ProductEntity> ProductsRepo => DataLayer.Repo<ProductEntity>();


        private SellerProfileEntity _currentSellerProfile;

        protected SellerProfileEntity CurrentSellerProfile =>
            _currentSellerProfile ?? (_currentSellerProfile =
                DataLayer.Repo<SellerProfileEntity>().FindOne(s => s.User == CurrentUser).Result);

        private BuyerProfileEntity _currentBuyerProfile;

        protected BuyerProfileEntity CurrentBuyerProfile =>
            _currentBuyerProfile ?? (_currentBuyerProfile =
                DataLayer.Repo<BuyerProfileEntity>().FindOne(s => s.User == CurrentUser).Result);

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ProductsRepo.ChainQueryable(q => q.Include(p => p.Seller));
        }

        [Authorize(Roles = "Buyer")]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] IList<CartItemViewModel> cart)
        {
            await ValidateCart(cart);
            
            

            Console.WriteLine(JsonConvert.SerializeObject(cart));
            return Ok();
        }

        private async Task ValidateCart(IList<CartItemViewModel> cart)
        {
            if (cart == null || cart.Any(m => m == null ||
                                              string.IsNullOrEmpty(m.Product?.Id) ||
                                              Math.Abs(m.Price) < 0.000001 || Math.Abs(m.Quantity) < 0.000001 ||
                                              m.Unit == SellingUnit.None))
            {
                throw new KnownException(
                    "Informațiile din coșul de cumpărături sunt invalide. Verifică-le și încearcă din nou.");
            }

            var productIds = cart.Select(ci => ci.Product.Id);
            var existingProductsCount = await ProductsRepo.Queryable.CountAsync(p => productIds.Any(id => id == p.Id));
            if (existingProductsCount != cart.Count)
            {
                throw new KnownException("O parte din (sau toate) produsele din coșul din cumpărături nu (mai) există în baza de date.");
            }

            var validProductTargetType =
                await ProductsRepo.Queryable.AllAsync(p => p.Seller.TargetType == CurrentBuyerProfile.Type);

            if (!validProductTargetType)
            {
                throw new KnownException(
                    "Produsele din coșul de cumpărături nu vin de la vânzători care vând către tipul tău de cumpărător (" +
                    CurrentBuyerProfile.Type + ")");
            }
        }
    }
}