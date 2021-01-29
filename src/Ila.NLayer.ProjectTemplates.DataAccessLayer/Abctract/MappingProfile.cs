using AutoMapper;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Abctract
{
    public class MappingProfile : Profile

    {
        public MappingProfile()
        {
            CreateMap<Core.Models.Base.ModelBase, Entities.Base.EntityBase.EntityBase>();
            CreateMap<Entities.Base.EntityBase.EntityBase, Core.Models.Base.ModelBase>();
        }
    }
}