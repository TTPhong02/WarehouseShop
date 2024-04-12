using BE_WAREHOUSE.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.Image
{
    public interface IImagesRepository
    {
        Task<IEnumerable<Images>> GetAllAsync();
        Task<Images?> GetByIdAsync(Guid entityId);
        Task<int> InsertAsync(Images image, IFormFile? imageFile);
        Task<int> UpdateAsync(Guid id, Images image, IFormFile? imageFile);
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteManyAsync(List<Guid> ids);
    }
}
