using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityBase;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Repositories.Base;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;

namespace Ila.NLayer.ProjectTemplates.MockDatabase.Abctract
{
    public class MockRepositoryBase<TEntity> : Disposable, IRepositoryBase<TEntity> where TEntity : class, IEntityBase, new()
    {
        private readonly List<TEntity> _mock = new List<TEntity>();
        public IQueryable<TEntity> Table => _mock.AsQueryable();

        public IQueryable<TEntity> NoTracking => _mock.AsQueryable();

        public void Delete(object id)
        {
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _mock.AsQueryable();
        }

        public Core.Models.PagedList.IPagedList<TModel> GetAllPaged<TModel>(Func<IQueryable<TEntity>, IQueryable<TModel>> func, IPaging paging)
        {
            return new Core.Models.PagedList.PagedList<TModel>(new List<TModel>(), paging);
        }

        public TEntity GetById(object id)
        {
            return _mock.FirstOrDefault();
        }

        public TEntity Insert(TEntity entity)
        {
            return entity;
        }

        public bool Insert(IEnumerable<TEntity> entities)
        {
            return true;
        }

        public int SaveChanges()
        {
            return 1;
        }

        public TEntity Update(TEntity entity)
        {
            return entity;
        }
    }
}