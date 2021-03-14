using Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Category;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities;
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

        [HttpGet]
        public IActionResult Post()
        {
            _categoryService.SampleValidasiton(new Category { });

            return Ok();
        }
    }
}