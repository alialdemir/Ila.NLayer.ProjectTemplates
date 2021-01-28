namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base
{
    public interface IEntityBase<TId>
    {
        TId Id { get; set; }
    }
}