using AutoMapper;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityBase;
using Ila.NLayer.ProjectTemplates.Core.Models.Base;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Abctract
{
    public class MappingProfile : Profile

    {
        public MappingProfile()
        {
            CreateMap<ModelBase, EntityBase>();
            CreateMap<EntityBase, ModelBase>();
        }
    }
}