using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base.EntityWithDeletableBase;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Abctract
{
    public class EfRepositoryBase<TEntity> : RepositoryBase<TEntity>, IEfRepositoryBase<TEntity> where TEntity : class, IEntityBase, new()
    {
        #region Fields

        private DbContext _context;
        private DbSet<TEntity> _dbSet;

        #endregion Fields

        #region Constructors

        public EfRepositoryBase(DbContext dbFactory)
        {
            _context = dbFactory;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a DbSet
        /// </summary>
        private DbSet<TEntity> DbSet => _dbSet ?? (_dbSet = _context.Set<TEntity>());

        /// <summary>
        /// Gets a table
        /// </summary>
        public override IQueryable<TEntity> Table => DbSet.AsQueryable();

        /// <summary>
        /// Gets a NoTracking
        /// </summary>
        public override IQueryable<TEntity> NoTracking => DbSet.AsNoTracking();

        #endregion Properties

        #region Methods

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">Primary id</param>
        public override void Delete(object id)
        {
            var deleteEntity = _context.Entry(GetById(id));
            if (deleteEntity == null)
                throw new ArgumentNullException(nameof(deleteEntity));

            if (deleteEntity is IEntityWithDeletableBase)
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
            foreach (IEnumerable<TEntity> entity in entities)// TODO: Performance?
            {
                Delete(entity);
            }
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
        /// Find
        /// </summary>
        /// <param name="predicate">Query</param>
        /// <returns>Query result</returns>
        public override IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">Entity</param>
        public override TEntity Insert(TEntity entity)
        {
            var addedEntity = _context.Entry(entity);
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
            _context.AddRange(entities);

            return true;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <returns>Number of modified rows</returns>
        public override int SaveChanges()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">Entity</param>
        public override TEntity Update(TEntity entity)
        {
            var updatedEntity = _context.Entry(entity);

            updatedEntity.State = EntityState.Modified;

            return entity;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void SoftDelete(EntityEntry<TEntity> entity)
        {
            ((IEntityWithDeletableBase)entity.Entity).Deleted = true;

            Update(entity.Entity);
        }

        #endregion Methods
    }
}