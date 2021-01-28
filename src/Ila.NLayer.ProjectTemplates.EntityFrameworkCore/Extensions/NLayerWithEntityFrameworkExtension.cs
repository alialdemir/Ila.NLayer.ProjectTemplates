using Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Base;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.UnitOfWork;
using Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Abctract;
using Microsoft.Extensions.DependencyInjection;

namespace Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Extensions
{
    public static class NLayerWithEntityFrameworkExtension
    {
        public static IServiceCollection AddNLayerWithEntityFramework(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEfRepositoryBase<>), typeof(EfRepositoryBase<>));

            services.AddScoped(typeof(IRepositoryBase<>), typeof(EfRepositoryBase<>));

            services.AddScoped<IDataProvider, EfDataProvider>();

            return services;
        }
    }
}