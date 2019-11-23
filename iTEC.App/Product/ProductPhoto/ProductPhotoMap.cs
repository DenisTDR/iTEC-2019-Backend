using API.Base.Web.Base.Models.EntityMaps;
using AutoMapper;

namespace iTEC.App.Product.ProductPhoto
{
    public class ProductPhotoMap : EntityViewModelMap<ProductPhotoEntity, ProductPhotoViewModel>
    {
        public override void ConfigureEntityToViewModelMapper(IMapperConfigurationExpression configurationExpression)
        {
            base.ConfigureEntityToViewModelMapper(configurationExpression);
            EntityToViewModelExpression.ForMember(pp => pp.Product, opt => opt.Ignore());
        }
    }
}