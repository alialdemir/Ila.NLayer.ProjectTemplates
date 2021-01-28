using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base.EntityBase;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base.EntityWithDeletableBase
{
    public abstract class EntityWithDeletableBase<TId> : EntityBase<TId>, IEntityWithDeletableBase
    {
        public bool Deleted { get; set; }
    }
}