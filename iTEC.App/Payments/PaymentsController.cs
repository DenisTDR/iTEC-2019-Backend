using System;
using System.Collections.Generic;
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
using iTEC.App.Profile.BuyerProfile;
using iTEC.App.Profile.SellerProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
                return httpClient;
            }
        }


        [Authorize(Roles = "Buyer")]
        [HttpPost]
        public async Task<IActionResult> CreateCheckout([FromBody] CreateCheckoutRequestModel model)
        {
            var order = await GetOrderForBuyerOrThrow(model);
            var content = new
            {
                account_id = WePayAccountId,
                amount = order.TotalPrice / 4.33,
                short_description = "iTEC Shop: payment for order " + order.Id,
                type = "goods",
                currency = "USD",
                hosted_checkout = new
                {
                    redirect_uri = EnvVarManager.GetOrThrow("EXTERNAL_URL") + "/api/Payments/CheckoutRedirect"
                }
            };
            var jsonContent =
                new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            var response =
                await WePayHttpClient.PostAsync($"https://stage.wepayapi.com/v2/checkout/create", jsonContent);
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            var responseJson = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseText);

            var checkoutId = responseJson["checkout_id"];
            var checkoutUri = responseJson["checkout_uri"];

            Console.WriteLine(checkoutId, checkoutUri);

            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CheckoutRedirect()
        {
            Console.WriteLine(Request.GetEncodedUrl());

            /*
             * 
            var formContent = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("utilizator", model.Email.Replace("@student.upt.ro", "")),
                new KeyValuePair<string, string>("parola", model.Password),
            });
            var response = await httpClient.PostAsync($"https://upt.ro/gisc/mbackend.php", formContent);
            response.EnsureSuccessStatusCode();
            var responseText = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(responseText) || responseText.StartsWith("Neautentificat"))
            {
                throw new KnownException(await TranslationsRepository.Translate("invalid-upt-account"));
            }

             */
            return Ok();
        }

        #region helpers

        private async Task<OrderEntity> GetOrderForBuyerOrThrow(CreateCheckoutRequestModel model)
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
                throw new KnownException("Comanda nu a fost găsită");
            }

            if (order.Buyer != CurrentBuyerProfile)
            {
                throw new KnownException("Nu poți plăti această comandă deoarece nu îți aparține.");
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