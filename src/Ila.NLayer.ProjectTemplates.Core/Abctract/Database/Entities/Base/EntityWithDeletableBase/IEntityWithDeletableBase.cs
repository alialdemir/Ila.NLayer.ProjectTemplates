namespace Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityWithDeletableBase
{
    public interface IEntityWithDeletableBase
    {
        bool Deleted { get; set; }
    }
}