using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Image;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Services.Image
{
    public class ImagesService : IImagesService
    {
        protected IImagesRepository imagesRepository;

        public ImagesService(IImagesRepository imagesRepository)
        {
            this.imagesRepository = imagesRepository;
        }

        public async Task<int> DeleteManyServiceAsync(List<Guid> ids)
        {
            var res = await  imagesRepository.DeleteManyAsync(ids);
            return res;
        }

        public async Task<int> DeleteServiceAsync(Guid id)
        {
            var res = await  imagesRepository.DeleteAsync(id);
            return res;
        }

        public async Task<IEnumerable<Images>> GetAllAsync()
        {
            var res = await  imagesRepository.GetAllAsync();
            return res;
        }

        public async Task<Images> GetByIdAsync(Guid id)
        {
            var res = await  imagesRepository.GetByIdAsync(id);
            return res;
        }

        public async Task<int> InsertServiceAsync(Images image, IFormFile? imageFile)
        {
            var res = await imagesRepository.InsertAsync(image, imageFile);
            return res;
        }

        public async Task<int> UpdateServiceAsync(Guid id, Images image, IFormFile? imageFile)
        {
            var res = await imagesRepository.UpdateAsync(id, image, imageFile);
            return res;
        }
    }
}
