using Ila.NLayer.ProjectTemplates.Core.Models.PagedList;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Abctract;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Base;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Base
{
    public class ServiceBase<TEntity, TRepository> : Disposable, IRepositoryBase<TEntity>
             where TEntity : class, IEntityBase, new()
             where TRepository : IRepositoryBase<TEntity>
    {
        #region Fields

        private readonly IDataProvider _unitOfWork;

        #endregion Fields

        #region Constructors

        public ServiceBase(IDataProvider unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion Constructors

        #region Properties

        private TRepository Repository
        {
            get
            {
                return _unitOfWork.Repository<TEntity, TRepository>();
            }
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public IQueryable<TEntity> Table => Repository.Table;

        /// <summary>
        /// Gets a NoTracking
        /// </summary>
        public IQueryable<TEntity> NoTracking => Repository.NoTracking;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">Primary id</param>
        public virtual void Delete(object id)
        {
            Repository.Delete(id);

            SaveChanges();
        }

        /// <summary>
        /// Delete as list
        /// </summary>
        /// <param name="entities">List of entities</param>
        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            Repository.Delete(entities);

            SaveChanges();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Repository.Find(predicate);
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual TEntity Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var result = Repository.Insert(entity);

            SaveChanges();

            return result;
        }

        /// <summary>
        /// Insert as list
        /// </summary>
        /// <param name="entities">List of entities</param>
        /// <returns>If true, the transaction is successful</returns>

        public virtual bool Insert(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            var result = Repository.Insert(entities);

            SaveChanges();

            return result;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <returns>Number of modified rows</returns>
        public int SaveChanges()
        {
            return Repository.SaveChanges();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual TEntity Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entityModel = Repository.Update(entity);

            SaveChanges();

            return entityModel;
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id">Primary id</param>
        public TEntity GetById(object id)
        {
            return Repository.GetById(id);
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
            return Repository.GetAllPaged(func, paging);
        }

        #endregion Methods
    }
}