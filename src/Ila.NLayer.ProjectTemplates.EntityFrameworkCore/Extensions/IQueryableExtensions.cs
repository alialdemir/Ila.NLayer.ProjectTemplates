using Ila.NLayer.ProjectTemplates.Core.Models.PagedList;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;
using System.Collections.Generic;
using System.Linq;

namespace Ila.NLayer.ProjectTemplates.EntityFrameworkCore.Extensions
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Paged List
        /// </summary>
        /// <typeparam name="TModel">Result model</typeparam>
        /// <param name="source">Source</param>
        /// <param name="paging">Paging</param>
        /// <returns>Paged TModel</returns>
        public static IPagedList<TModel> ToPagedList<TModel>(this IQueryable<TModel> source, IPaging paging)
        {
            if (source == null)
                return new PagedList<TModel>(new List<TModel>(), paging);

            if (paging == null)
                paging = new Paging();

            var count = source.Count();

            var data = new List<TModel>();

            if (IsGetData(count, paging))
                data.AddRange(source.Skip(((Paging)paging).Offset).Take(paging.PageSize.Value).ToList());

            return new PagedList<TModel>(data, paging, count);
        }

        private static bool IsGetData(long count, IPaging paging)
        {
            if (paging.PageNumber <= 0 || paging.PageSize <= 0)
                return false;

            return !((paging.PageNumber - 1) > count / paging.PageSize);
        }
    }
}