using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityBase;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities
{
    public class Category : EntityBase<int>
    {
        public string Name { get; set; }
    }
}