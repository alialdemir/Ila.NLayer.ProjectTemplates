using System;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base.EntityWithDateBase
{
    public interface IEntityWithDateBase
    {
        DateTime? CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}