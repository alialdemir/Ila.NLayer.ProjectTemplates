using System.Reflection;
using AutoMapper;
using FluentValidation;
using Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Base;
using Ila.NLayer.ProjectTemplates.Core.Abctract;
using Ila.NLayer.ProjectTemplates.Core.Models.Response;
using Ila.NLayer.ProjectTemplates.Core.Validator;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.DbContext;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities;
using Ila.NLayer.ProjectTemplates.WebAdmin.Filters;
using Ila.NLayer.ProjectTemplates.WebAdmin.Helpers;
using Ila.NLayer.ProjectTemplates.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Extensions;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Abctract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var migrationsAssembly = typeof(IlaDbContext).GetTypeInfo().Assembly.GetName().Name;

// Add framework services.
builder.Services.AddDbContext<IlaDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                         sqlServerOptionsAction: sqlOptions =>
                         {
                             sqlOptions.MigrationsAssembly(migrationsAssembly);
                         }));

builder.Services
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

builder.Services.AddTransient<IValidator<CategoryResponseModel>, CategoryResponseModelValidator>();


var mappingConfig = new MapperConfiguration(mc =>
{
    mc.ShouldMapMethod = (m => false);
    mc.AddProfile(new MappingProfile());
    mc.CreateMap<Category, CategoryResponseModel>();
    mc.CreateMap<CategoryResponseModel, Category>();
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

