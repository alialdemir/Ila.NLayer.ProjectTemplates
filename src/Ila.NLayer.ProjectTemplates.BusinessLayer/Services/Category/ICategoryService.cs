using Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Base;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Repositories.Base;
using Ila.NLayer.ProjectTemplates.Core.Models.PagedList;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;
using Ila.NLayer.ProjectTemplates.Core.Models.Response;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Category;

namespace Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Category
{
    public interface ICategoryService : IRepositoryBase<DataAccessLayer.Entities.Category>,
        IServiceBase<DataAccessLayer.Entities.Category, ICategoryRepository>

    {
        void Insert(CategoryResponseModel category);
        IPagedList<CategoryResponseModel> GetCategoryPagedList(Paging paging);
    }
}