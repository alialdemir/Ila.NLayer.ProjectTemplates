using System.Collections.Generic;

namespace Ila.NLayer.ProjectTemplates.Core.Models.PagedList
{
    public interface IPagedList<TModel>
    {
        /// <summary>
        /// Total item count
        /// </summary>
        int TotalItemCount { get; set; }

        /// <summary>
        /// Items
        /// </summary>
        IEnumerable<TModel> Items { get; set; }
    }
}