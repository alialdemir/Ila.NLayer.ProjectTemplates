using Ila.NLayer.ProjectTemplates.Core.Models.Base;

namespace Ila.NLayer.ProjectTemplates.Core.Models.Response
{
    public class ProductViewModel:ModelBase
	{

        /// <summary>
        /// Gets or sets the id of the product.
        /// </summary>
        public int Id { get; set; }

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

