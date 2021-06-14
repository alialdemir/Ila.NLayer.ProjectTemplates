using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.DataProvider;

namespace Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Abctract
{

    public interface IEfDataProvider<TDbContext> : IDataProvider where TDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        TDbContext DbContext { get; }
    }
}
