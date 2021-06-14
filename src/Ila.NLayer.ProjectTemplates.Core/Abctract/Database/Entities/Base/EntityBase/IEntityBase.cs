namespace Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityBase
{
    public interface IEntityBase
    {
    }

    public interface IEntityBase<TId> : IEntityBase
    {
        TId Id { get; set; }
    }
}