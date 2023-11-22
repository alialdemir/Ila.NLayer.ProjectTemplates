using System;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.DataProvider;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Repositories.Base;
using Ila.NLayer.ProjectTemplates.Core.Extensions;
using Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Abctract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Extensions
{
    public static class NLayerWithEntityFrameworkExtension
    {
        public static IServiceCollection AddNLayerWithEntityFramework<TContext>(this IServiceCollection services)
                                           where TContext : DbContext
        {
            services.AddScoped(typeof(IEfRepositoryBase<,>), typeof(EfRepositoryBase<,>));

           // services.AddScoped(typeof(IRepositoryBase<>), typeof(EfRepositoryBase<,>));

            services.AddScopedDynamic(typeof(IEfRepositoryBase<,>),typeof(TContext));

            services.AddScoped<IEfDataProvider<TContext>, EfDataProvider<TContext>>();

            services.AddScoped<IDataProvider, EfDataProvider<TContext>>();

            return services;
        }
    }
}