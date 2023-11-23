using AutoMapper;
using Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Base;
using Ila.NLayer.ProjectTemplates.Core.Abctract;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.DataProvider;
using Ila.NLayer.ProjectTemplates.Core.Models.PagedList;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;
using Ila.NLayer.ProjectTemplates.Core.Models.Response;
using Ila.NLayer.ProjectTemplates.Core.Models.Results;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Product;

namespace Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Product
{
    public class ProductService : ServiceBase<DataAccessLayer.Entities.Product, IProductRepository>, IProductService
    {
        private readonly IMapper _mapper;

        private readonly IValidationDictionary _validationDictionary;


        public ProductService(IDataProvider dataProvider,
                              IMapper mapper,
                              IValidationDictionary validationDictionary) : base(dataProvider)
        {
            _mapper = mapper;
            _validationDictionary = validationDictionary;
        }

        /// <summary>
        /// Deletes a product by its identifier.
        /// </summary>
        /// <param name="productId">The identifier of the product to be deleted.</param>
        /// <returns>A result indicating whether the deletion was successful.</returns>
        public Result<bool> Delete(int productId)
        {
            if (productId <= 0)
                return Result<bool>.Invalid(null);

            CurrentRepository.Delete(productId);

            SaveChanges();

            return Result<bool>.Success(true);
        }

        /// <summary>
        /// Retrieves a paged list of product models.
        /// </summary>
        /// <param name="paging">Paging information for the result.</param>
        /// <returns>A paged list of product response models.</returns>
        public IPagedList<ProductViewModel> GetProductPagedList(Paging paging)
        {
            return CurrentRepository.GetProductPagedList(paging);
        }

        /// <summary>
        /// Inserts a new product into the system.
        /// </summary>
        /// <param name="product">The product model to be inserted.</param>
        /// <returns>A result indicating whether the insertion was successful.</returns>
        public Result<bool> Insert(ProductViewModel product)
        {
            if (!_validationDictionary.Validation(product))
                return Result<bool>.Invalid(null);

            var insertedProduct = Insert(_mapper.Map<DataAccessLayer.Entities.Product>(product));
            if (insertedProduct.Id <= 0)
                return Result<bool>.Error();

            return Result<bool>.Success(true);
        }

        /// <summary>
        /// Updates an existing product in the system.
        /// </summary>
        /// <param name="product">The product model to be updated.</param>
        /// <returns>A result indicating whether the update was successful.</returns>
        public Result<bool> Update(ProductViewModel product)
        {
            if (!_validationDictionary.Validation(product))
                return Result<bool>.Invalid(null);

            Update(_mapper.Map<DataAccessLayer.Entities.Product>(product));

            return Result<bool>.Success(true);
        }
    }
}