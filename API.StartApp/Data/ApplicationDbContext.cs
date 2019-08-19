using API.Base.Web.Base.Database;
using Microsoft.EntityFrameworkCore;

namespace API.StartApp.Data
{
    public class ApplicationDbContext : BaseDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}