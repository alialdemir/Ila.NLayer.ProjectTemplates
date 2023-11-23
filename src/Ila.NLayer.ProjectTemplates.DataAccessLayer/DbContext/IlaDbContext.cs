using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.DbContext
{
    public class IlaDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public IlaDbContext(DbContextOptions<IlaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products{ get; set; }
    }

}