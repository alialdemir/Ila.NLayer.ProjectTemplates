using System;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityBase;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Repositories.Base;

namespace Ila.NLayer.ProjectTemplates.Core.Abctract.Database.DataProvider
{

    public interface IDataProvider : IDisposable
    {
        int Commit<TEntity, TRepository>() where TEntity : class, IEntityBase, new()
                    where TRepository : IRepositoryBase<TEntity>;

        TRepository Repository<TEntity, TRepository>() where TEntity : class, IEntityBase, new()
                    where TRepository : IRepositoryBase<TEntity>;
    }
}