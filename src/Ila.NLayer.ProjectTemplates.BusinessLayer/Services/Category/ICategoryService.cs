using Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Base;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Base;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Category;

namespace Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Category
{
    public interface ICategoryService : IRepositoryBase<DataAccessLayer.Entities.Category>, IServiceBase<DataAccessLayer.Entities.Category, ICategoryRepository>
    {
        void SampleValidasiton(DataAccessLayer.Entities.Category category);
    }
}