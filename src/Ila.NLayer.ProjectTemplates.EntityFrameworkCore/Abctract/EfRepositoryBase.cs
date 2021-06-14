using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityBase;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityWithDateBase;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityWithDeletableBase;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Repositories.Base;
using Ila.NLayer.ProjectTemplates.Core.Extensions;
using Ila.NLayer.ProjectTemplates.Core.Models.PagedList;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Abctract
{
    public class EfRepositoryBase<TEntity, TDbContext> : RepositoryBase<TEntity>, IEfRepositoryBase<TEntity, TDbContext> where TEntity : class, IEntityBase, new()
     where TDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        #region Fields

        private DbSet<TEntity> _dbSet;

        #endregion Fields

        #region Constructors

        public EfRepositoryBase(IEfDataProvider<TDbContext> efDataProvider) : base(efDataProvider)
        {
            DbContext = efDataProvider.DbContext;
        }

        #endregion Constructors

        #region Properties

        public TDbContext DbContext
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a NoTracking
        /// </summary>
        public override IQueryable<TEntity> NoTracking => DbSet.AsNoTracking();

        /// <summary>
        /// Gets a table
        /// </summary>
        public override IQueryable<TEntity> Table => DbSet.AsQueryable();

        /// <summary>
        /// Gets a DbSet
        /// </summary>
        private DbSet<TEntity> DbSet => _dbSet ?? (_dbSet = DbContext.Set<TEntity>());

        #endregion Properties

        #region Methods

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">Primary id</param>
        public override void Delete(object id)
        {
            var deleteEntity = DbContext.Entry(GetById(id));
            if (deleteEntity == null)
                throw new ArgumentNullException(nameof(deleteEntity));

            if (deleteEntity.Entity is IEntityWithDeletableBase)
            {
                SoftDelete(deleteEntity);
            }
            else
            {
                deleteEntity.State = EntityState.Deleted;
            }
        }

        /// <summary>
        /// Delete as list
        /// </summary>
        /// <param name="entities">List of entities</param>
        public override void Delete(IEnumerable<TEntity> entities)
        {
            Delete(entities);
        }

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="predicate">Query</param>
        /// <returns>Query result</returns>
        public override IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        /// <summary>
        /// GetAllPaged
        /// </summary>
        /// <typeparam name="TModel">Result model</typeparam>
        /// <param name="func">Filter</param>
        /// <param name="paging">Paging</param>
        /// <returns>IPagedList<TModel></returns>
        public override IPagedList<TModel> GetAllPaged<TModel>(Func<IQueryable<TEntity>, IQueryable<TModel>> func, IPaging paging)
        {
            return func(NoTracking).ToPagedList(paging);
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id">Primary id</param>
        public override TEntity GetById(object id)
        {
            var entity = DbSet.Find(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">Entity</param>
        public override TEntity Insert(TEntity entity)
        {
            if (entity is IEntityWithDateBase)
                ((IEntityWithDateBase)entity).CreatedDate = DateTime.Now;

            var addedEntity = DbContext.Entry(entity);
            addedEntity.State = EntityState.Added;

            return entity;
        }

        /// <summary>
        /// Insert as list
        /// </summary>
        /// <param name="entities">List of entities</param>
        /// <returns>If true, the transaction is successful</returns>
        public override bool Insert(IEnumerable<TEntity> entities)
        {
            DbContext.AddRange(entities);

            return true;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <returns>Number of modified rows</returns>
        public override int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">Entity</param>
        public override TEntity Update(TEntity entity)
        {
            if (entity is IEntityWithDateBase)
                ((IEntityWithDateBase)entity).ModifiedDate = DateTime.Now;

            var updatedEntity = DbContext.Entry(entity);

            updatedEntity.State = EntityState.Modified;

            return entity;
        }

        /// <summary>
        /// Entity soft delete
        /// </summary>
        /// <param name="entity">Entity</param>
        private void SoftDelete(EntityEntry<TEntity> entity)
        {
            ((IEntityWithDeletableBase)entity.Entity).Deleted = true;

            Update(entity.Entity);
        }

        #endregion Methods
    }
}