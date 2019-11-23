using System;
using System.Collections.Generic;
using API.Base.Web.Base.ApiBuilder;
using API.Base.Web.Base.Models;
using API.Base.Web.Common.Consumables.Models;
using API.Base.Web.Common.FAQ;
using API.Base.Web.Common.OgMetadata;
using API.Base.Web.Common.Partners.Models.Entities;
using API.Base.Web.Common.ReferenceTrack;
using API.Base.Web.Common.Subscriber;
using iTEC.App.Address;
using iTEC.App.Category;
using iTEC.App.Order;
using iTEC.App.Order.OrderProduct;
using iTEC.App.Product;
using iTEC.App.Product.ProductCategory;
using iTEC.App.Product.ProductPhoto;
using iTEC.App.Profile.BuyerProfile;
using iTEC.App.Profile.SellerProfile;

namespace iTEC.App
{
    public class ItecAppApiSpecifications : ApiSpecifications
    {
        public override List<Type> DisableEntityStacks()
        {
            return new List<Type>
            {
                typeof(PartnerEntity), typeof(PartnerTierEntity), typeof(PartnerTypeEntity),
                typeof(ConsumableEntity), typeof(ConsumedRecordEntity), typeof(ReferenceTrackEntity),
                typeof(SubscriberEntity), typeof(OgMetadataEntity), typeof(FaqEntity)
            };
        }

        public override List<AdminDashboardSection> RegisterAdminDashboardSections()
        {
            return new List<AdminDashboardSection>
            {
                new AdminDashboardSection("iTEC Shop: People", new List<AdminDashboardLink>
                {
                    new AdminDashboardLink("Sellers", typeof(SellerProfilesUiController)),
                    new AdminDashboardLink("Buyers", typeof(BuyerProfilesUiController)),
                    new AdminDashboardLink("Addresses", typeof(AddressesUiController)),
                }),
                new AdminDashboardSection("iTEC Shop: Products", new List<AdminDashboardLink>
                {
                    new AdminDashboardLink("Categories", typeof(CategoriesUiController)),
                    new AdminDashboardLink("Products", typeof(ProductsUiController)),
                    new AdminDashboardLink("Products Photos", typeof(ProductPhotosUiController)),
                    new AdminDashboardLink("Product Categories", typeof(ProductCategoriesUiController)),
                }),
                new AdminDashboardSection("iTEC Shop: Shopping", new List<AdminDashboardLink>
                {
                    new AdminDashboardLink("Orders", typeof(OrdersUiController)),
                    new AdminDashboardLink("Order Products", typeof(OrderProductsUiController)),
                })
            };
        }
    }
}