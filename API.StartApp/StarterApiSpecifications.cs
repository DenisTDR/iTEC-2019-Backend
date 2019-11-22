using API.Base.Web.Base.ApiBuilder;
using API.Base.Web.Base.Database;
using API.Base.Web.Common.Database;
using Microsoft.Extensions.DependencyInjection;

namespace API.StartApp
{
    public class StarterApiSpecifications : ApiSpecifications
    {
        public StarterApiSpecifications()
        {
            AddDbSeederAction = services =>
            {
                services.AddTransient<IDataSeeder, CommonDataSeeder<CommonDataSeedType>>();
            };
        }
    }
}