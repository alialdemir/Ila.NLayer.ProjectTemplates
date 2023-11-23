using AutoMapper;
using Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Product;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;
using Ila.NLayer.ProjectTemplates.Core.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace Ila.NLayer.ProjectTemplates.WebAdmin.Controllers;

public class ProductController : WebApi.Controllers.ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductController(IProductService productService,
        IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    public IActionResult Index([FromQuery] Paging paging)
    {
        var productList = _productService.GetProductPagedList(paging);
        

        return View(productList);
    }

    public IActionResult Create()
    {
        return View();
    }


    public IActionResult Edit([FromRoute]int id)
    {
        var product = _productService
            .NoTracking
            .Where(x => x.Id == id)
            .Select(x=> _mapper.Map<ProductViewModel>(x))
            .FirstOrDefault();
        if (product == null)
            return RedirectToAction("Index");

        return View(product);
    }

    [HttpPost]
    public IActionResult Create(ProductViewModel product)
    {
        if (!ModelState.IsValid)
            return View(product);

        var result = _productService.Insert(product);
        if (result.IsSuccess)
            return RedirectToAction("Index");

        return View(result);
    }

    [HttpPost]
    public IActionResult Edit(ProductViewModel product)
    {
        if (!ModelState.IsValid)
            return View(product);

        var result = _productService.Update(product);
        if (result.IsSuccess)
            return RedirectToAction("Index");


        return View(result);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
       _productService.Delete(id);

        return RedirectToAction("Index");
    }
}

