using Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Base;
using Ila.NLayer.ProjectTemplates.Core.Abctract;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Category;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.UnitOfWork;

namespace Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Category
{
    public class CategoryService : ServiceBase<DataAccessLayer.Entities.Category, ICategoryRepository>, ICategoryService
    {
        #region Fields

        private readonly IValidationDictionary _validationDictionary;

        #endregion Fields

        #region Constructor

        public CategoryService(IDataProvider dataProvider, IValidationDictionary validationDictionary) : base(dataProvider)
        {
            _validationDictionary = validationDictionary;
        }

        #endregion Constructor

        #region Methods

        public void SampleValidasiton(DataAccessLayer.Entities.Category category)
        {
            if (string.IsNullOrEmpty(category.Name))
            {
                _validationDictionary.AddError(nameof(category.Name), "Category name cannot be empty.");

                return;
            }

            Insert(category);
        }

        #endregion Methods
    }
}