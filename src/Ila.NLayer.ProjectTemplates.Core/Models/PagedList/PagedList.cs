using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Ila.NLayer.ProjectTemplates.Core.Models.PagedList
{
    [Serializable]
    public class PagedList<TModel> : Paging.Paging, IPagedList<TModel>

    {
        public PagedList(IEnumerable<TModel> items, Paging.IPaging paging, int totalItemCount = 0)
        {
            Items = items;
            TotalItemCount = totalItemCount;
            PageNumber = paging?.PageNumber;
            PageSize = paging?.PageSize;
        }

        /// <summary>
        /// Total item count
        /// </summary>
        [JsonProperty(Order = 0)]
        public int TotalItemCount { get; set; }

        /// <summary>
        /// Items
        /// </summary>
        [JsonProperty(Order = 1)]
        public IEnumerable<TModel> Items { get; set; } = new List<TModel>();
    }
}