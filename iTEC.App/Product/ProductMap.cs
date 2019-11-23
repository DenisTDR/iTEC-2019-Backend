using System.Linq;
using API.Base.Web.Base.Models.EntityMaps;
using AutoMapper;
using iTEC.App.Category;
using iTEC.App.Product.ProductPhoto;
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

    public class ProductMapper : EntityViewModelMap<ProductEntity, ProductViewModel>
    {
        public override void ConfigureEntityToViewModelMapper(IMapperConfigurationExpression configurationExpression)
        {
            base.ConfigureEntityToViewModelMapper(configurationExpression);
            EntityToViewModelExpression
                .ForMember(p => p.Categories, o => o.Ignore())
                .ForMember(p => p.Photos, o => o.Ignore())
                .AfterMap((e, vm) =>
                {
                    vm.Categories = e.Categories?.Select(c => Mapper.Map<CategoryViewModel>(c.Category)).ToList();

                    vm.PhotosVm = e.Photos?.Select(Mapper.Map<ProductPhotoViewModel>).ToList();
                    vm.Photos = vm.PhotosVm?.Where(photo => !photo.IsThumbnail).Select(p => p.File.Link).ToList();
                    vm.Thumbnail = vm.PhotosVm?.FirstOrDefault(photo => photo.IsThumbnail)?.File?.Link;
                });
        }
    }
}