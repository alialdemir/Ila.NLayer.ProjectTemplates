using Ila.NLayer.ProjectTemplates.Core.Models.PagedList;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Abctract;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base.EntityWithDeletableBase;
using Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Abctract
{
    public class EfRepositoryBase<TEntity> : Disposable, IEfRepositoryBase<TEntity> where TEntity : class, IEntityBase, new()
    {
        #region Fields

        private DbSet<TEntity> _dbSet;

        #endregion Fields

        #region Constructors

        public EfRepositoryBase(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a DbSet
        /// </summary>
        private DbSet<TEntity> DbSet => _dbSet ?? (_dbSet = DbContext.Set<TEntity>());

        /// <summary>
        /// Gets a table
        /// </summary>
        public IQueryable<TEntity> Table => DbSet.AsQueryable();

        /// <summary>
        /// Gets a NoTracking
        /// </summary>
        public IQueryable<TEntity> NoTracking => DbSet.AsNoTracking();

        public DbContext DbContext
        {
            get;
            private set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">Primary id</param>
        public void Delete(object id)
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
        public void Delete(IEnumerable<TEntity> entities)
        {
            foreach (IEnumerable<TEntity> entity in entities)// TODO: Performance?
            {
                Delete(entity);
            }
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id">Primary id</param>
        public TEntity GetById(object id)
        {
            var entity = DbSet.Find(id);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return entity;
        }

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="predicate">Query</param>
        /// <returns>Query result</returns>
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">Entity</param>
        public TEntity Insert(TEntity entity)
        {
            var addedEntity = DbContext.Entry(entity);
            addedEntity.State = EntityState.Added;

            return entity;
        }

        /// <summary>
        /// Insert as list
        /// </summary>
        /// <param name="entities">List of entities</param>
        /// <returns>If true, the transaction is successful</returns>
        public bool Insert(IEnumerable<TEntity> entities)
        {
            DbContext.AddRange(entities);

            return true;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <returns>Number of modified rows</returns>
        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">Entity</param>
        public TEntity Update(TEntity entity)
        {
            var updatedEntity = DbContext.Entry(entity);

            updatedEntity.State = EntityState.Modified;

            return entity;
        }

        /// <summary>
        /// GetAllPaged
        /// </summary>
        /// <typeparam name="TModel">Result model</typeparam>
        /// <param name="func">Filter</param>
        /// <param name="paging">Paging</param>
        /// <returns>IPagedList<TModel></returns>
        public IPagedList<TModel> GetAllPaged<TModel>(Func<IQueryable<TEntity>, IQueryable<TModel>> func, IPaging paging)
        {
            return func(NoTracking).ToPagedList<TModel>(paging);
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