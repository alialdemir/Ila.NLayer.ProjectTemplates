using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base.EntityBase;
using System;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base.EntityWithDateBase
{
    public abstract class EntityWithDateBase<TId> : EntityBase<TId>, IEntityWithDateBase
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}