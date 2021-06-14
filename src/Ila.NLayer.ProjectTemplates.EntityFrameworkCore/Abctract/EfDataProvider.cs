using System;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.DataProvider;
using Microsoft.Extensions.Logging;

namespace Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Abctract
{
    public class EfDataProvider<TDbContext> : DataProvider, IEfDataProvider<TDbContext> where TDbContext : Microsoft.EntityFrameworkCore.DbContext
    {

        private readonly ILoggerFactory _loggerFactory;


        public EfDataProvider(IServiceProvider serviceProvider,
                              TDbContext dbContext,
                              ILoggerFactory loggerFactory) : base(serviceProvider, loggerFactory)
        {
            _loggerFactory = loggerFactory;
            DbContext = dbContext;
        }

        public TDbContext DbContext { get; private set; }


        public override int Commit<TEntity, TRepository>()
        {
            var repository = (IEfRepositoryBase<TEntity, TDbContext>)Repository<TEntity, TRepository>();
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
    }
}