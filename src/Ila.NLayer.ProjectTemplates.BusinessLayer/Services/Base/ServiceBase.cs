using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.DataProvider;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityBase;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Repositories.Base;
using Ila.NLayer.ProjectTemplates.Core.Models.PagedList;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;

namespace Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Base
{
    public class ServiceBase<TEntity, TRepository> : Disposable, IRepositoryBase<TEntity>, IServiceBase<TEntity, TRepository>
             where TEntity : class, IEntityBase, new()
             where TRepository : IRepositoryBase<TEntity>
    {
        #region Fields

        private readonly IDataProvider _unitOfWork;

        #endregion Fields

        #region Constructors

        public ServiceBase(IDataProvider dataProvider)
        {
            CurrentRepository = dataProvider.Repository<TEntity, TRepository>();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a current repository
        /// </summary>
        internal TRepository CurrentRepository
        {
            get; private set;
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public IQueryable<TEntity> Table => CurrentRepository.Table;

        /// <summary>
        /// Gets a NoTracking
        /// </summary>
        public IQueryable<TEntity> NoTracking => CurrentRepository.NoTracking;

        #endregion Properties

        #region Methods

        protected TRepository1 Repository<TEntity1, TRepository1>()
                    where TEntity1 : class, IEntityBase, new()
                    where TRepository1 : IRepositoryBase<TEntity1>
        {
            return _unitOfWork.Repository<TEntity1, TRepository1>();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">Primary id</param>
        public virtual void Delete(object id)
        {
            CurrentRepository.Delete(id);

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

            CurrentRepository.Delete(entities);

            SaveChanges();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return CurrentRepository.Find(predicate);
        }

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual TEntity Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var result = CurrentRepository.Insert(entity);

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

            var result = CurrentRepository.Insert(entities);

            SaveChanges();

            return result;
        }

        /// <summary>
        /// Save
        /// </summary>
        /// <returns>Number of modified rows</returns>
        public int SaveChanges()
        {
            return CurrentRepository.SaveChanges();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual TEntity Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entityModel = CurrentRepository.Update(entity);

            SaveChanges();

            return entityModel;
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id">Primary id</param>
        public TEntity GetById(object id)
        {
            return CurrentRepository.GetById(id);
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
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            return CurrentRepository.GetAllPaged(func, paging);
        }

        #endregion Methods
    }
}