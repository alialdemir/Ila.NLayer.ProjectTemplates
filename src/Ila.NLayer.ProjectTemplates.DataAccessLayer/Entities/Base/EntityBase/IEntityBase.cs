namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities.Base
{
    public interface IEntityBase
    {
    }

    public interface IEntityBase<TId> : IEntityBase
    {
        TId Id { get; set; }
    }
}