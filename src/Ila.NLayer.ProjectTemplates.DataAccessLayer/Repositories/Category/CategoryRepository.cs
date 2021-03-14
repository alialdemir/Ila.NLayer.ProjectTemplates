using Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Base;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.UnitOfWork;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Category
{
    public class CategoryRepository : RepositoryBase<Entities.Category>, ICategoryRepository
    {
        #region Constructor

        public CategoryRepository(IDataProvider dataProvider) : base(dataProvider)
        {
        }

        #endregion Constructor
    }
}