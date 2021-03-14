using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Base;

namespace Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Base
{
    public interface IServiceBase<TEntity, TRepository> where TEntity : class, IEntityBase, new() where TRepository : IRepositoryBase<TEntity>
    {
    }
}