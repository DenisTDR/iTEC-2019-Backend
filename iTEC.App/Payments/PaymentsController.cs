using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using API.Base.Web.Base.Controllers.Api;
using API.Base.Web.Base.Database.DataLayer;
using API.Base.Web.Base.Database.Repository;
using API.Base.Web.Base.Exceptions;
using API.Base.Web.Base.Helpers;
using iTEC.App.Order;
using iTEC.App.Order.Enums;
using iTEC.App.Profile.BuyerProfile;
using iTEC.App.Profile.SellerProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace iTEC.App.Payments
{
    public class PaymentsController : ApiController
    {
        private string WePayAccountId => EnvVarManager.GetOrThrow("WEPAY_ACCOUNT_ID");
        private string WePayAccessToken => EnvVarManager.GetOrThrow("WEPAY_ACCESS_TOKEN");

        private HttpClient WePayHttpClient
        {
            get
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", WePayAccessToken);
                httpClient.DefaultRequestHeaders.Add("User-Agent", "iTEC-WebApi-Backend");
                return httpClient;
            }
        }


        [Authorize(Roles = "Buyer")]
        [HttpPost]
        public async Task<IActionResult> CreateCheckout([FromBody] CreateCheckoutRequestModel model,
            [FromQuery] bool force = false)
        {
            var order = await GetOrderForBuyerToPayOrThrow(model);
            if (!string.IsNullOrEmpty(order.WePayCheckoutId) && !force)
            {
                throw new KnownException(
                    "Nu poți plăti această comandă. Există deja o cerere de plată asociată pe WePay.");
            }

            var wePayRequestBody = new
            {
                account_id = WePayAccountId,
                amount = order.TotalPrice / 4.33,
                short_description = "iTEC Shop: payment for order " + order.Id,
                type = "goods",
                currency = "USD",
                hosted_checkout = new
                {
                    redirect_uri = EnvVarManager.GetOrThrow("EXTERNAL_URL") + "/payment-redirect"
                }
            };
            var wePayRequestBodyJson =
                new StringContent(JsonConvert.SerializeObject(wePayRequestBody), Encoding.UTF8, "application/json");

            var response =
                await WePayHttpClient.PostAsync("https://stage.wepayapi.com/v2/checkout/create", wePayRequestBodyJson);

            var responseText = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
                var responseJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseText);

                var checkoutId = responseJson["checkout_id"].ToString();
                var checkoutUri = (responseJson["hosted_checkout"] as JObject)?["checkout_uri"].ToString();

                order.WePayCheckoutId = checkoutId;
                order.State = OrderState.WaitingPayment;
                await DataLayer.SaveChangesAsync();
                return Ok(new {checkoutId, checkoutUri});
            }
            catch
            {
                Console.WriteLine(responseText);
                throw;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CheckoutRedirect([FromQuery] [Required] string checkout_id)
        {
            Console.WriteLine(Request.GetEncodedUrl());
            if (string.IsNullOrEmpty(checkout_id))
            {
                throw new KnownException("Invalid checkout_id");
            }

            var order = await OrdersRepo.FindOne(o => o.WePayCheckoutId == checkout_id);

            if (order == null)
            {
                throw new KnownException("Nu am găsit o comandă asociată");
            }

            var wePayRequestBody = new {checkout_id};
            var wePayRequestBodyJson = new StringContent(JsonConvert.SerializeObject(wePayRequestBody), Encoding.UTF8,
                "application/json");
            var response =
                await WePayHttpClient.PostAsync("https://stage.wepayapi.com/v2/checkout", wePayRequestBodyJson);
            var responseText = await response.Content.ReadAsStringAsync();
            try
            {
                response.EnsureSuccessStatusCode();
                var responseJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseText);
                var state = responseJson["state"].ToString();
                if (state == "released" || state == "authorized")
                {
                    order.State = OrderState.Paid;
                    await DataLayer.SaveChangesAsync();
                    await ProcessOrder();
                }

                return Ok(state);
            }
            catch
            {
                throw;
            }

            return Ok();
        }

        private async Task ProcessOrder()
        {
            // TODO: process order quantities and deliver it
            await Task.Delay(500);
        }

        #region helpers

        private async Task<OrderEntity> GetOrderForBuyerToPayOrThrow(CreateCheckoutRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new KnownException("Invalid request model!");
            }

            EnsureBuyerProfile();
            OrdersRepo.ChainQueryable(q => q.Include(o => o.Buyer));
            var order = await OrdersRepo.GetOne(model.OrderId);
            if (order == null)
            {
                throw new KnownException("Comanda nu a fost găsită!");
            }

            if (order.Buyer != CurrentBuyerProfile)
            {
                throw new KnownException("Nu poți plăti această comandă deoarece nu îți aparține!");
            }

            if (order.State == OrderState.Paid || order.State == OrderState.WaitingProcessing)
            {
                throw new KnownException("Această comandă este deja plătită!");
            }

            return order;
        }

        private IRepository<OrderEntity> OrdersRepo => DataLayer.Repo<OrderEntity>();

        #endregion

        #region profiles

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

        private SellerProfileEntity _currentSellerProfile;

        protected SellerProfileEntity CurrentSellerProfile =>
            _currentSellerProfile ?? (_currentSellerProfile =
                DataLayer.Repo<SellerProfileEntity>().FindOne(s => s.User == CurrentUser).Result);

        private BuyerProfileEntity _currentBuyerProfile;

        protected BuyerProfileEntity CurrentBuyerProfile =>
            _currentBuyerProfile ?? (_currentBuyerProfile =
                DataLayer.Repo<BuyerProfileEntity>().FindOne(s => s.User == CurrentUser).Result);

        protected IDataLayer DataLayer => ServiceProvider.GetService<IDataLayer>();

        #endregion
    }
}