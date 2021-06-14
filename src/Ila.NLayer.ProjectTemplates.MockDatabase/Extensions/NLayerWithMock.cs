using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Repositories.Base;
using Ila.NLayer.ProjectTemplates.MockDatabase.Abctract;
using Microsoft.Extensions.DependencyInjection;

namespace Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Extensions
{
    public static class NLayerWithMock
    {
        public static IServiceCollection AddNLayerWithMock(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryBase<>), typeof(MockRepositoryBase<>));

            return services;
        }
    }
}