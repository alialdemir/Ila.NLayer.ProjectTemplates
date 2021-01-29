namespace Ila.NLayer.ProjectTemplates.Core.Models.Paging
{
    public interface IPaging
    {
        int? PageNumber { get; set; }
        int? PageSize { get; set; }
    }
}