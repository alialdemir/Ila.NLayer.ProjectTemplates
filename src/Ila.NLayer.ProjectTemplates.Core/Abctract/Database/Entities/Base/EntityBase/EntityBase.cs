namespace Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityBase
{
    public class EntityBase : IEntityBase
    {
    }

    public class EntityBase<TId> : IEntityBase<TId>
    {
        public TId Id { get; set; }
    }
}