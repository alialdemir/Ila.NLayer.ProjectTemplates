using Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Base;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Repositories.Base;
using Ila.NLayer.ProjectTemplates.Core.Models.PagedList;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;
using Ila.NLayer.ProjectTemplates.Core.Models.Response;
using Ila.NLayer.ProjectTemplates.Core.Models.Results;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Product;

namespace Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Product
{
    public interface IProductService : IRepositoryBase<DataAccessLayer.Entities.Product>,
        IServiceBase<DataAccessLayer.Entities.Product, IProductRepository>

    {
        Result<bool> Insert(ProductViewModel product);

        Result<bool> Delete(int productId);

        Result<bool> Update(ProductViewModel product);

        IPagedList<ProductViewModel> GetProductPagedList(Paging paging);
    }
}