﻿using System;
using System.Collections;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityBase;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace Ila.NLayer.ProjectTemplates.Core.Abctract.Database.DataProvider
{
    public class DataProvider : Disposable, IDataProvider
    {
        #region Fields

        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceProvider _serviceProvider;
        private Hashtable _repositories;

        #endregion Fields

        #region Constructors

        public DataProvider(IServiceProvider serviceProvider,
                            ILoggerFactory loggerFactory)
        {
            _serviceProvider = serviceProvider;
            _loggerFactory = loggerFactory;
        }

        #endregion Constructors

        #region Methods

        public virtual int Commit<TEntity, TRepository>() where TEntity : class, IEntityBase, new()
                    where TRepository : IRepositoryBase<TEntity>
        {
            var repository = Repository<TEntity, TRepository>();

            try
            {
                return repository.SaveChanges();
            }
            catch (Exception ex)
            {
                _loggerFactory
                    .CreateLogger(repository.GetType().Name)
                    .LogError("Save Changes", ex);

                return 0;
            }
        }

        public TRepository Repository<TEntity, TRepository>() where TEntity : class, IEntityBase, new()
                    where TRepository : IRepositoryBase<TEntity>
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TRepository);

            if (_repositories.ContainsKey(type.Name))
            {
                return (TRepository)_repositories[type.Name];
            }

            var repository = _serviceProvider.GetService(type);

            if (repository != null)
                _repositories.Add(type.Name, repository);

            return (TRepository)_repositories[type.Name];
        }

        protected override void Dispose(bool disposing)
        {
            if (_repositories != null && _repositories.Values != null)
            {
                foreach (IDisposable repository in _repositories.Values)
                {
                    repository.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #endregion Methods
    }
}