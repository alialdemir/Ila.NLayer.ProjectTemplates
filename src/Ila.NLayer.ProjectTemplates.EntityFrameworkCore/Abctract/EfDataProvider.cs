using Ila.NLayer.ProjectTemplates.DataAccessLayer.DataProvider;
using Microsoft.Extensions.Logging;
using System;

namespace Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Abctract
{
    public class EfDataProvider : DataProvider
    {
        #region Fields

        private readonly ILoggerFactory _loggerFactory;

        #endregion Fields

        #region Constructors

        public EfDataProvider(IServiceProvider serviceProvider,
                              ILoggerFactory loggerFactory) : base(serviceProvider, loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        #endregion Constructors

        #region Methods

        public override int Commit<TEntity, TRepository>()
        {
            var repository = (IEfRepositoryBase<TEntity>)Repository<TEntity, TRepository>();
            if (repository == null)
                throw new ArgumentException(nameof(repository));

            using (var dbContextTransaction = repository.DbContext.Database.BeginTransaction())
            {
                try
                {
                    int result = repository.SaveChanges();

                    dbContextTransaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    _loggerFactory
                        .CreateLogger(repository.GetType().Name)
                        .LogError("Save Changes", ex);

                    dbContextTransaction.Rollback();

                    return 0;
                }
            }
        }

        #endregion Methods
    }
}