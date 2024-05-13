using BE_WAREHOUSE.API.Controllers.Base;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_WAREHOUSE.API.Controllers.Category
{
    [ApiController]
    public class CategoriesController : BaseController<Categories>
    {
        ICategoriesRepository _categoriesRepository;
        public CategoriesController(IBaseRepository<Categories> baseRepository,IBaseService<Categories> baseService , ICategoriesRepository categoriesRepository) : base(baseRepository,baseService)
        {
            _categoriesRepository = categoriesRepository;
        }
        [HttpGet("Slug/{slug}")]
        public async Task<IActionResult> GetCategoriesBySlug(string slug)
        {
            var res = await _categoriesRepository.GetCategoriesBySlug(slug);
            return Ok(res);
        }
    }
}
