namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base.EntityWithDeletableBase
{
    public interface IEntityWithDeletableBase
    {
        bool Deleted { get; set; }
    }
}