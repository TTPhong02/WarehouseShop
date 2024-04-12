using BE_WAREHOUSE.Core.Interfaces.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_WAREHOUSE.API.Controllers.Base
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        protected IBaseService<T> _baseService;
        protected IBaseRepository<T> _baseRepository;

        public BaseController(IBaseRepository<T> baseRepository, IBaseService<T> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }

        public BaseController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await _baseRepository.GetAllAsync();
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var res = await _baseRepository.GetByIdAsync(id);
            return Ok(res);
        }
        [HttpGet("NewCode")]
        public async Task<IActionResult> GetNewCodeAsync()
        {
            var res = await _baseRepository.GetMaxCodeAsync();
            return Ok(res);
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> PagingAndSearch(int pageSize, int pageNumber, string? searchString)
        {
            var res = await _baseRepository.PagingAndSearchAsync(pageSize, pageNumber, searchString);
            return Ok(res);
        }
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromForm]string dataJson, IFormFile? imageFile)
        {
            var className = typeof(T).Name;
            var res = await _baseService.InsertServiceAsync( dataJson, imageFile);
            return StatusCode(201,res);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromForm] string dataJson, IFormFile? imageFile, Guid id) 
        { 
            var res = await _baseService.UpdateServiceAsync( dataJson, imageFile, id);
            return Ok(res);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var res = await _baseRepository.DeleteByIdAsync(id);
            return Ok(res);
        }
        [HttpDelete("ids")]
        public async Task<IActionResult> DeleteAsync([FromBody] List<Guid> ids)
        {
            var res = await _baseRepository.DeleteAnyAsync(ids);
            return Ok(res);
        }
    }
}
