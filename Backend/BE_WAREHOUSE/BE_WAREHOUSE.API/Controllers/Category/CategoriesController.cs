using BE_WAREHOUSE.API.Controllers.Base;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_WAREHOUSE.API.Controllers.Category
{
    [ApiController]
    public class CategoriesController : BaseController<Categories>
    {
        public CategoriesController(IBaseRepository<Categories> baseRepository,IBaseService<Categories> baseService) : base(baseRepository,baseService)
        {
        }
    }
}
