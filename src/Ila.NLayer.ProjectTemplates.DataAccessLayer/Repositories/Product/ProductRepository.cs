using System.Linq;
using AutoMapper;
using Ila.NLayer.ProjectTemplates.Core.Extensions;
using Ila.NLayer.ProjectTemplates.Core.Models.PagedList;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;
using Ila.NLayer.ProjectTemplates.Core.Models.Response;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.DbContext;
using Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Abctract;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Product
{
    public class ProductRepository : EfRepositoryBase<Entities.Product, IlaDbContext>, IProductRepository
    {
        private readonly IMapper _mapper;

        public ProductRepository(IEfDataProvider<IlaDbContext> dataProvider,
                                  IMapper mapper) : base(dataProvider)
        {
            _mapper = mapper;
        }

        public IPagedList<ProductViewModel> GetProductPagedList(Paging paging)
        {
            return NoTracking
                        .Select(product => _mapper.Map<ProductViewModel>(product))
                        .ToPagedList(paging);
        }
    }
}

