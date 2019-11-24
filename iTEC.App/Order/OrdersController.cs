using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Base.Web.Base.Controllers.Api;
using API.Base.Web.Base.Database.DataLayer;
using API.Base.Web.Base.Database.Repository;
using API.Base.Web.Base.Exceptions;
using AutoMapper;
using iTEC.App.Order.OrderProduct;
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
        protected IRepository<OrderEntity> OrdersRepo => DataLayer.Repo<OrderEntity>();


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
            OrdersRepo.ChainQueryable(q => q
                .Include(o => o.Buyer)
                .Include(o => o.Products)
                .ThenInclude(p => p.Product.Seller.Address)
                .Include(o => o.Products)
                .ThenInclude(p => p.Product.Categories)
                .ThenInclude(c => c.Category.Parent)
            );
        }


        [Authorize(Roles = "Seller, Buyer")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderViewModel>), 200)]
        public async Task<IActionResult> GetOwn()
        {
            if (await UserManager.IsInRoleAsync(CurrentUser, "Buyer"))
            {
                EnsureBuyerProfile();
                OrdersRepo.ChainQueryable(q => q.Where(o => o.Buyer == CurrentBuyerProfile));
            }

            if (await UserManager.IsInRoleAsync(CurrentUser, "Seller"))
            {
                EnsureSellerProfile();
                throw new KnownException("Can't filter orders for a seller yet.");
            }

            var orders = await OrdersRepo.GetAll();

            return Ok(Mapper.Map<IEnumerable<OrderViewModel>>(orders));
        }

        [Authorize(Roles = "Buyer")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderViewModel), 200)]
        public async Task<IActionResult> GetOne([FromRoute] [Required] string id)
        {
            if (await UserManager.IsInRoleAsync(CurrentUser, "Buyer"))
            {
                EnsureBuyerProfile();
                OrdersRepo.ChainQueryable(q => q.Where(o => o.Buyer == CurrentBuyerProfile));
                var order = await OrdersRepo.GetOne(id);
                if (order == null)
                {
                    return NotFound();
                }

                return Ok(Mapper.Map<OrderViewModel>(order));
            }

            return NotFound();
        }

        [Authorize(Roles = "Buyer")]
        [HttpPost]
        [ProducesResponseType(typeof(OrderViewModel), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PlaceOrder([FromBody] IList<CartItemViewModel> cart)
        {
            EnsureBuyerProfile();

            var order = await TransposeCartToOrder(cart);
            order = await OrdersRepo.Add(order);

            return Ok(Mapper.Map<OrderViewModel>(order));
        }

        private async Task<OrderEntity> TransposeCartToOrder(IList<CartItemViewModel> cart)
        {
            await ValidateCart(cart);

            var productIds = cart.Select(ci => ci.Product.Id);
            var products = (await ProductsRepo.FindAll(p => productIds.Any(id => id == p.Id))).ToList();
            var ops = (from cartItem in cart
                    let product = products.First(p => p.Id == cartItem.Product.Id)
                    select new OrderProductEntity
                    {
                        Product = product, UnitPrice = product.Price, Unit = product.Unit, Quantity = cartItem.Quantity
                    })
                .ToList();

            return new OrderEntity
                {Buyer = CurrentBuyerProfile, TotalPrice = ops.Sum(op => op.Quantity * op.UnitPrice), Products = ops};
        }

        private async Task ValidateCart(IList<CartItemViewModel> cart)
        {
            if (cart == null || cart.Any(m =>
                    m == null || string.IsNullOrEmpty(m.Product?.Id) || Math.Abs(m.Quantity) < 0.000001))
            {
                throw new KnownException(
                    "Informațiile din coșul de cumpărături sunt invalide. Verifică-le și încearcă din nou.");
            }

            var productIds = cart.Select(ci => ci.Product.Id);
            var existingProducts =
                await ProductsRepo.Queryable.Where(p => productIds.Any(id => id == p.Id)).ToListAsync();
            if (existingProducts.Count != cart.Count)
            {
                throw new KnownException(
                    "O parte din (sau toate) produsele din coșul din cumpărături nu (mai) există în baza de date.");
            }

            foreach (var existingProduct in existingProducts)
            {
                if (existingProduct.AvailableUnits < cart.First(ci => ci.Product.Id == existingProduct.Id).Quantity)
                {
                    throw new KnownException(
                        "Cantitatea de '" + existingProduct.Name +
                        "' este mai mare decât cea disponibilă în acest moment. Te rugăm să verifici și să actualizezi coșul de cumpărături. ");
                }
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
    }
}