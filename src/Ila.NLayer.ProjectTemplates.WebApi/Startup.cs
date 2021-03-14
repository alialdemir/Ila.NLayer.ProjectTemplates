using Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Base;
using Ila.NLayer.ProjectTemplates.Core.Abctract;
using Ila.NLayer.ProjectTemplates.Core.Extensions;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.DataProvider;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Base;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.UnitOfWork;
using Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Extensions;
using Ila.NLayer.ProjectTemplates.WebApi.Filters;
using Ila.NLayer.ProjectTemplates.WebApi.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ila.NLayer.ProjectTemplates.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                // ila n layer core dependencies
                .AddScoped<IDataProvider, DataProvider>()
                .AddScoped<IValidationDictionary, ModelStateWrapper>()
                .AddScoped(typeof(IServiceBase<,>), typeof(ServiceBase<,>))

                // Adds services and repositories automatically as addScoped
                .AddScopedDynamic(typeof(IRepositoryBase<>))
                .AddScopedDynamic(typeof(IServiceBase<,>))

                // By default it uses mock repository. Optionally use other orm tools
                // .AddNLayerWithEntityFramework()
                .AddNLayerWithMock()

                // In the service layer filter inserts for validation
                .AddControllers(opts => opts.Filters.Add(typeof(ModelValidateFilter)))
                .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}