using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityBase;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Repositories.Base;

namespace Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Abctract
{
    public interface IEfRepositoryBase<TEntity, TDbContext> : IRepositoryBase<TEntity>
                                           where TEntity : class, IEntityBase, new()
                                           where TDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        TDbContext DbContext { get; }

    }
}