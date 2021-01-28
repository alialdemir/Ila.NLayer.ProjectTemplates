using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Base;

namespace Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Abctract
{
    public interface IEfRepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntityBase, new()
    {
    }
}