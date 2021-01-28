using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Base;
using System;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.UnitOfWork
{
    public interface IDataProvider : IDisposable
    {
        int Commit<TEntity, TRepository>() where TEntity : class, IEntityBase, new()
                    where TRepository : IRepositoryBase<TEntity>;

        TRepository Repository<TEntity, TRepository>() where TEntity : class, IEntityBase, new()
                    where TRepository : IRepositoryBase<TEntity>;
    }
}