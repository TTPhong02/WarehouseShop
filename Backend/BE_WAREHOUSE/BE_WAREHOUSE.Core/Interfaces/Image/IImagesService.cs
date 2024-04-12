using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.Image
{
    public interface IImagesService
    {
        Task<IEnumerable<Images>> GetAllAsync();
        Task<Images> GetByIdAsync(Guid id);
        Task<int> InsertServiceAsync(Images image, IFormFile? imageFile);
        Task<int> UpdateServiceAsync(Guid id, Images image, IFormFile? imageFile);
        Task<int> DeleteServiceAsync(Guid id);
        Task<int> DeleteManyServiceAsync(List<Guid> ids);
    }
}
