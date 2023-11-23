using Ila.NLayer.ProjectTemplates.Core.Models.PagedList;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;
using Ila.NLayer.ProjectTemplates.Core.Models.Response;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.DbContext;
using Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Abctract;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Product
{
    public interface IProductRepository : IEfRepositoryBase<Entities.Product, IlaDbContext>
    {
        IPagedList<ProductViewModel> GetProductPagedList(Paging paging);
    }
}