using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityBase;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Repositories.Base;

namespace Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Base
{
    public interface IServiceBase<TEntity, TRepository>
        where TEntity : class, IEntityBase, new()
        where TRepository : IRepositoryBase<TEntity>
    {
    }
}