using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityBase;

namespace Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityWithDeletableBase
{
    public abstract class EntityWithDeletableBase<TId> : EntityBase<TId>, IEntityWithDeletableBase
    {
        public bool Deleted { get; set; }
    }
}