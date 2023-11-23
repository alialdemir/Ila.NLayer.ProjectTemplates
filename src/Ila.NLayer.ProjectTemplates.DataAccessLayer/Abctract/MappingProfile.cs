using AutoMapper;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityBase;
using Ila.NLayer.ProjectTemplates.Core.Models.Base;
using Ila.NLayer.ProjectTemplates.Core.Models.Response;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Abctract
{
    public class MappingProfile : Profile

    {
        public MappingProfile()
        {
            CreateMap<ModelBase, EntityBase>();
            CreateMap<EntityBase, ModelBase>();

            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, ProductViewModel>();

            CreateMap<Category, CategoryResponseModel>();
            CreateMap<CategoryResponseModel, Category>();
        }
    }
}