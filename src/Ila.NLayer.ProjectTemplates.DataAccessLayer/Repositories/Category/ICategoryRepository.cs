using Ila.NLayer.ProjectTemplates.Core.Models.PagedList;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;
using Ila.NLayer.ProjectTemplates.Core.Models.Response;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.DbContext;
using Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Abctract;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Category
{
    public interface ICategoryRepository : IEfRepositoryBase<Entities.Category, IlaDbContext>
    {
        IPagedList<CategoryResponseModel> GetCategoryPagedList(Paging paging);
    }
}