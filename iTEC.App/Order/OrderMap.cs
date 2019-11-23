using API.Base.Web.Base.Models.EntityMaps;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTEC.App.Order
{
    public class OrderMap : EntityTypeConfiguration<OrderEntity>
    {
        public override void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            base.Configure(builder);
            builder.HasMany(o => o.Products).WithOne(p => p.Order);
        }
    }
}