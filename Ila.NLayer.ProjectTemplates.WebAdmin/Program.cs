using System;
using System.Reflection;
using AutoMapper;
using FluentValidation;
using Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Base;
using Ila.NLayer.ProjectTemplates.Core.Abctract;
using Ila.NLayer.ProjectTemplates.Core.Extensions;
using Ila.NLayer.ProjectTemplates.Core.Models.Response;
using Ila.NLayer.ProjectTemplates.Core.Validator;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Abctract;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.DbContext;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities;
using Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Extensions;
using Ila.NLayer.ProjectTemplates.WebAdmin.Filters;
using Ila.NLayer.ProjectTemplates.WebAdmin.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddControllersWithViews();

var migrationsAssembly = typeof(IlaDbContext).GetTypeInfo().Assembly.GetName().Name;
 
// Add framework services.
services.AddDbContext<IlaDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                         sqlServerOptionsAction: sqlOptions =>
                         {
                             sqlOptions.MigrationsAssembly(migrationsAssembly);
                         }));


services.AddIdentity<User, IdentityRole>(x =>
{
    x.Password.RequireDigit = false;
    x.Password.RequiredLength = 3;
    x.Password.RequireLowercase = false;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<IlaDbContext>()
    .AddDefaultTokenProviders();

services.AddScoped<UserManager<User>>();

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

//services.AddSession();
services.AddAuthorization();

services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.ShouldMapMethod = (m => false);
    mc.AddProfile(new MappingProfile());
    mc.CreateMap<Category, CategoryResponseModel>();
    mc.CreateMap<CategoryResponseModel, Category>();
});

IMapper mapper = mappingConfig.CreateMapper();
services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.None,
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

var context = services.BuildServiceProvider()
                  .GetService<IlaDbContext>();

var userManager = services.BuildServiceProvider()
                  .GetService<UserManager<User>>();

var roleManager = services.BuildServiceProvider()
                  .GetService<RoleManager<IdentityRole>>();

DbInitializer.Initialize(context, userManager, roleManager);

app.Run();

