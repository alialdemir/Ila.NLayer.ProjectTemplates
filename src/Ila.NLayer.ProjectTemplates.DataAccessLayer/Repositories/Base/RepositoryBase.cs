using Ila.NLayer.ProjectTemplates.DataAccessLayer.Abctract;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Base
{
    public abstract class RepositoryBase<TEntity> : Disposable, IRepositoryBase<TEntity> where TEntity : class, IEntityBase, new()
    {
        #region Methods

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id">Primary id</param>
        public abstract TEntity GetById(object id);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">Primary id</param>
        public abstract void Delete(object id);

        /// <summary>
        /// Delete as list
        /// </summary>
        /// <param name="entities">List of entities</param>
        public abstract void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="predicate">Query</param>
        /// <returns>Query result</returns>
        public abstract IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">Entity</param>
        public abstract TEntity Insert(TEntity entity);

        /// <summary>
        /// Insert as list
        /// </summary>
        /// <param name="entities">List of entities</param>
        /// <returns>İşlem başaarılı ise true değilse false</returns>
        public abstract bool Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Save
        /// </summary>
        /// <returns>Değişiklik yapılan satır sayısı</returns>
        public abstract int SaveChanges();

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">Entity</param>
        public abstract TEntity Update(TEntity entity);

        #endregion Methods

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public abstract IQueryable<TEntity> Table { get; }

        /// <summary>
        /// Gets a NoTracking
        /// </summary>
        public abstract IQueryable<TEntity> NoTracking { get; }

        #endregion Properties
    }
}