using API.Base.Web.Base.Models.EntityMaps;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTEC.App.Product
{
    public class ProductMap : EntityTypeConfiguration<ProductEntity>
    {
        public override void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            base.Configure(builder);
            builder.HasMany(p => p.Photos).WithOne(pi => pi.Product);
        }
    }
}