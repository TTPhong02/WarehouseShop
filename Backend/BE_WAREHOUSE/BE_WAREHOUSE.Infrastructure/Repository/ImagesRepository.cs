using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Image;
using BE_WAREHOUSE.Infrastructure.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BE_WAREHOUSE.Infrastructure.Repository
{
    public class ImagesRepository : IImagesRepository
    {
        IDbContext _dbContext;

        public ImagesRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var sql = "Delete FROM Images where ImagesId = @id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", id);
            var res = await _dbContext.Connection.ExecuteAsync(sql,parameters);
            return res;
        }

        public async Task<int> DeleteManyAsync(List<Guid> ids)
        {
            var sql = "Delete FROM Images where ImagesId IN (@ids)";
            var idsArray = "";
            DynamicParameters parameters = new DynamicParameters();
            foreach (var item in ids)
            {
                idsArray += $"{item},";
            }
            idsArray.Substring(0, idsArray.Length - 1);
            parameters.Add("@ids", idsArray);
            var res = await _dbContext.Connection.ExecuteAsync(sql, parameters);
            return res;
        }

        public async Task<IEnumerable<Images>> GetAllAsync()
        {
            var sql = $"SELECT * FROM Images";
            var res = await _dbContext.Connection.QueryAsync<Images>(sql);
            return res;
        }

        public async Task<Images?> GetByIdAsync(Guid entityId)
        {
            var procName = "SELECT * FROM Images where ProductId = @id";
            var param = new DynamicParameters();
            param.Add("@id", entityId);
            var res = await _dbContext.Connection.QuerySingleAsync<Images>(procName, param);
            return res;
        }

        public async Task<int> InsertAsync(Images image, IFormFile? imageFile)
        {
            if (imageFile?.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageFile.FileName);
                using (var stream = File.Create(path))
                {
                    await imageFile.CopyToAsync(stream);
                }
                var imagePath = $"/images/{imageFile.FileName}";
                image.ImagesPath = imagePath;
            }
            var procName = "Proc_Insert_Images";
            var res = await _dbContext.Connection.ExecuteAsync(procName, image);
            return res;
        }

        public async Task<int> UpdateAsync(Guid id, Images image, IFormFile? imageFile)
        {
            if (imageFile?.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageFile.FileName);
                using (var stream = System.IO.File.Create(path))
                {
                    await imageFile.CopyToAsync(stream);
                }
                var imagePath = $"/images/{imageFile.FileName}";
                image.ImagesPath = imagePath;
            }
            var procName = "Proc_Update_Images";
            var param = new DynamicParameters();
            param.Add("ImagePath", image.ImagesPath);
            param.Add("TableId", id);
            var res = await _dbContext.Connection.ExecuteAsync(procName, param);

            return res;
        }
    }
}
