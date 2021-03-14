﻿using Ila.NLayer.ProjectTemplates.Core.Models.PagedList;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Abctract;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Base
{
    public class RepositoryBase<TEntity> : Disposable, IRepositoryBase<TEntity> where TEntity : class, IEntityBase, new()
    {
        #region Fields

        private readonly IDataProvider _dataProvider;

        #endregion Fields

        #region Constructor

        public RepositoryBase(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets a current repository
        /// </summary>
        private IRepositoryBase<TEntity> CurrentRepository
        {
            get => _dataProvider.Repository<TEntity, IRepositoryBase<TEntity>>();
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table
        {
            get => CurrentRepository.Table;
        }

        /// <summary>
        /// Gets a NoTracking
        /// </summary>
        public virtual IQueryable<TEntity> NoTracking
        {
            get => CurrentRepository.NoTracking;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id">Primary id</param>
        public virtual TEntity GetById(object id) => CurrentRepository.GetById(id);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">Primary id</param>
        public virtual void Delete(object id) => CurrentRepository.Delete(id);

        /// <summary>
        /// Delete as list
        /// </summary>
        /// <param name="entities">List of entities</param>
        public virtual void Delete(IEnumerable<TEntity> entities) => CurrentRepository.Delete(entities);

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="predicate">Query</param>
        /// <returns>Query result</returns>
        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => CurrentRepository.Find(predicate);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual TEntity Insert(TEntity entity) => CurrentRepository.Insert(entity);

        /// <summary>
        /// Insert as list
        /// </summary>
        /// <param name="entities">List of entities</param>
        /// <returns>İşlem başaarılı ise true değilse false</returns>
        public virtual bool Insert(IEnumerable<TEntity> entities) => CurrentRepository.Insert(entities);

        /// <summary>
        /// Save
        /// </summary>
        /// <returns>Değişiklik yapılan satır sayısı</returns>
        public virtual int SaveChanges() => CurrentRepository.SaveChanges();

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual TEntity Update(TEntity entity) => CurrentRepository.Update(entity);

        /// <summary>
        /// GetAllPaged
        /// </summary>
        /// <typeparam name="TModel">Result model</typeparam>
        /// <param name="func">Filter</param>
        /// <param name="paging">Paging</param>
        /// <returns>IPagedList<TModel></returns>
        public virtual IPagedList<TModel> GetAllPaged<TModel>(Func<IQueryable<TEntity>, IQueryable<TModel>> func, IPaging paging) => CurrentRepository.GetAllPaged(func, paging);

        #endregion Methods
    }
}