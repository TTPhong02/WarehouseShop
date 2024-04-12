using BE_WAREHOUSE.Core.Interfaces.Image;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_WAREHOUSE.API.Controllers.Images
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        IImagesService imagesService;

        public ImagesController(IImagesService imagesService)
        {
            this.imagesService = imagesService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await imagesService.GetAllAsync();
            return Ok(res);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetbyIdAsync(Guid id)
        {
            var res = await imagesService.GetByIdAsync(id);
            return Ok(res);
        }
    }
}
