using System.Linq;
using AutoMapper;
using Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Product;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;
using Ila.NLayer.ProjectTemplates.Core.Models.Response;
using Microsoft.AspNetCore.Mvc;

public class ProductController : Ila.NLayer.ProjectTemplates.WebApi.Controllers.ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetProducts([FromQuery] Paging paging)
    {
        var productList = _productService.GetProductPagedList(paging);
        return Ok(productList);
    }

    [HttpGet("{id}")]
    public IActionResult GetProductById(int id)
    {
        var product = _productService
            .NoTracking
            .Where(x => x.Id == id)
            .Select(x => _mapper.Map<ProductViewModel>(x))
            .FirstOrDefault();

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public IActionResult CreateProduct(ProductViewModel product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = _productService.Insert(product);
        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, ProductViewModel product)
    {
        if (id != product.Id)
            return BadRequest("Id mismatch");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = _productService.Update(product);
        if (!result.IsSuccess)
            return BadRequest(result);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        _productService.Delete(id);
        return NoContent(); 
    }
}
