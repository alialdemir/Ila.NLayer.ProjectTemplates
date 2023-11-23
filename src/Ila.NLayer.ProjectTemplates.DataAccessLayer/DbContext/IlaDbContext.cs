using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.DbContext
{
    public class IlaDbContext : IdentityDbContext<User, IdentityRole, string>

    {
        public IlaDbContext(DbContextOptions<IlaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products{ get; set; }
    }

}