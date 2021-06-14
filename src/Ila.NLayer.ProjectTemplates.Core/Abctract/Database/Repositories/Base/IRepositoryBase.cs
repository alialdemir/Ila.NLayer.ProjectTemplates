using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityBase;
using Ila.NLayer.ProjectTemplates.Core.Models.PagedList;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;

namespace Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Repositories.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class, IEntityBase, new()
    {
        #region Methods

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">Primary id</param>
        void Delete(object id);

        /// <summary>
        /// Delete as list
        /// </summary>
        /// <param name="entities">List of entities</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="predicate">Query</param>
        /// <returns>Query result</returns>
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">Entity</param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Insert as list
        /// </summary>
        /// <param name="entities">List of entities</param>
        /// <returns>If true, the transaction is successful</returns>
        bool Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// Save
        /// </summary>
        /// <returns>Number of modified rows</returns>
        int SaveChanges();

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">Entity</param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id">Primary id</param>
        TEntity GetById(object id);

        /// <summary>
        /// GetAllPaged
        /// </summary>
        /// <typeparam name="TModel">Result model</typeparam>
        /// <param name="func">Filter</param>
        /// <param name="paging">Paging</param>
        /// <returns>IPagedList<TModel></returns>
        IPagedList<TModel> GetAllPaged<TModel>(Func<IQueryable<TEntity>, IQueryable<TModel>> func, IPaging paging);

        #endregion Methods

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// Gets a NoTracking
        /// </summary>
        IQueryable<TEntity> NoTracking { get; }

        #endregion Properties
    }
}