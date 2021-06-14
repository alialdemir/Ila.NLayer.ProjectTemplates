using System.Linq;
using AutoMapper;
using Ila.NLayer.ProjectTemplates.Core.Extensions;
using Ila.NLayer.ProjectTemplates.Core.Models.PagedList;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;
using Ila.NLayer.ProjectTemplates.Core.Models.Response;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.DbContext;
using Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Abctract;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Category
{
    public class CategoryRepository : EfRepositoryBase<Entities.Category, IlaDbContext>, ICategoryRepository
    {
        private readonly IMapper _mapper;

        public CategoryRepository(IEfDataProvider<IlaDbContext> dataProvider,
                                  IMapper mapper) : base(dataProvider)
        {
            _mapper = mapper;
        }

        public IPagedList<CategoryResponseModel> GetCategoryPagedList(Paging paging)
        {
            return NoTracking
                        .Select(category => _mapper.Map<CategoryResponseModel>(category))
                        .ToPagedList(paging);
        }
    }
}