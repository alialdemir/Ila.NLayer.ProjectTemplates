using AutoMapper;
using Ila.NLayer.ProjectTemplates.Core.Models.Response;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Ila.NLayer.ProjectTemplates.WebApi
{
    public partial class Startup
    {
        public IServiceCollection AutoMappeServices(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.CreateMap<Category, CategoryResponseModel>();
                mc.CreateMap<CategoryResponseModel, Category>();
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}