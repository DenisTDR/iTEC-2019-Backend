using System;
using System.Collections.Generic;
using API.Base.Web.Base.ApiBuilder;
using API.Base.Web.Base.Models;
using API.Base.Web.Common.Consumables.Models;
using API.Base.Web.Common.FAQ;
using API.Base.Web.Common.Partners.Models.Entities;
using API.Base.Web.Common.ReferenceTrack;
using API.Base.Web.Common.Subscriber;
using iTEC.App.Address;
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
                typeof(SubscriberEntity)
            };
        }

        public override List<AdminDashboardSection> RegisterAdminDashboardSections()
        {
            return new List<AdminDashboardSection>
            {
                new AdminDashboardSection("iTEC Shop", new List<AdminDashboardLink>
                {
                    new AdminDashboardLink("Sellers", typeof(SellerProfilesUiController)),
                    new AdminDashboardLink("Buyers", typeof(BuyerProfilesUiController)),
                    new AdminDashboardLink("Addresses", typeof(AddressesUiController)),
                }),
            };
        }
    }
}