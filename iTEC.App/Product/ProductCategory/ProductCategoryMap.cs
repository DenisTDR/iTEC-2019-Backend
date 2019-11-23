using API.Base.Web.Base.Models.EntityMaps;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iTEC.App.Product.ProductCategory
{
    public class ProductCategoryMap : EntityTypeConfiguration<ProductCategoryEntity>
    {
        public override void Configure(EntityTypeBuilder<ProductCategoryEntity> builder)
        {
            base.Configure(builder);
            builder.HasOne(pc => pc.Product).WithMany(p => p.Categories);
        }
    }
}