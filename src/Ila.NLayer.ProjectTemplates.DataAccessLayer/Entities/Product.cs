using System;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.Entities.Base.EntityBase;

namespace Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities
{
	public class Product: EntityBase<int>
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the stock quantity of the product.
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }
    }
}

