using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.DbContext.SeedData
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<IlaDbContext>
    {
        public IlaDbContext CreateDbContext(string[] args)
        {
            string basePath = Directory.GetCurrentDirectory();

            string jsonPath = Path.Combine(basePath, "appsettings.json");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(jsonPath)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<IlaDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new IlaDbContext(optionsBuilder.Options);
        }
    }

}