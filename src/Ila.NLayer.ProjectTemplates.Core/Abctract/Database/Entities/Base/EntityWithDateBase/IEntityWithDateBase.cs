using System;

namespace Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityWithDateBase
{
    public interface IEntityWithDateBase
    {
        DateTime? CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}