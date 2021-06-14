using Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Category;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;
using Ila.NLayer.ProjectTemplates.Core.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace Ila.NLayer.ProjectTemplates.WebApi.Controllers
{
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CategoryResponseModel category)
        {
            _categoryService.Insert(category);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetCategories([FromQuery] Paging paging)
        {
            return Ok(_categoryService.GetCategoryPagedList(paging));
        }
    }
}