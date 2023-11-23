using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.DbContext
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<IlaDbContext>
    {
        public IlaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IlaDbContext>();
            // Console.WriteLine("env: "+Environment.GetEnvironmentVariable("ConnectionStrings:DefaultConnection"));
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=IlaDb;User Id=sa;Password=password123__");

            return new IlaDbContext(optionsBuilder.Options);
        }
    }

}