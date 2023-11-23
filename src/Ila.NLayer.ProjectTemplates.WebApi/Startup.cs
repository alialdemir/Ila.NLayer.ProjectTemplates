using System;
using System.Reflection;
using FluentValidation;
using Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Base;
using Ila.NLayer.ProjectTemplates.Core.Abctract;
using Ila.NLayer.ProjectTemplates.Core.Extensions;
using Ila.NLayer.ProjectTemplates.Core.Models.Response;
using Ila.NLayer.ProjectTemplates.Core.Validator;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.DbContext;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities;
using Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Extensions;
using Ila.NLayer.ProjectTemplates.WebApi.Filters;
using Ila.NLayer.ProjectTemplates.WebApi.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Ila.NLayer.ProjectTemplates.WebApi
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(IlaDbContext).GetTypeInfo().Assembly.GetName().Name;

            // Add framework services.
            services.AddDbContext<IlaDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(migrationsAssembly);
                                     }));

            services
                // ila n layer core dependencies
                .AddScoped<IValidationDictionary, ModelStateWrapper>()

                .AddScoped(typeof(IServiceBase<,>), typeof(ServiceBase<,>))

                // Adds services and repositories automatically as addScoped
                .AddScopedDynamic(typeof(IServiceBase<,>))

                .AddNLayerWithEntityFramework<IlaDbContext>()

                // By default it uses mock repository. Optionally use other orm tools
                //.AddNLayerWithMock()

                .AddControllers(opts => opts.Filters.Add(typeof(ModelValidateFilter)))

                .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);

            services.AddTransient<IValidator<CategoryResponseModel>, CategoryResponseModelValidator>();

            AutoMappeServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IlaDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
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

            DbInitializer.Initialize(context,userManager, roleManager);

        }
    }
}