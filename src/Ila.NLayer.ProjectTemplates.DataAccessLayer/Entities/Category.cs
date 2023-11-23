using System.Collections.Generic;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityBase;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities
{
    public class Category : EntityBase<int>
    {
        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the products of the category.
        /// </summary>
        public List<Product> Products { get; set; }
    }
}