using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Image;
using BE_WAREHOUSE.Core.Services.Image;
using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BE_WAREHOUSE.Core.Services.Base
{
    public class BaseService<T> : IBaseService<T> where T : class 
    {
        protected IBaseRepository<T> repository;
        protected IImagesService imagesService;

        public BaseService(IBaseRepository<T> repository, IImagesService imagesService)
        {
            this.repository = repository;
            this.imagesService = imagesService;
        }

        public virtual async Task<MISAServiceResult> InsertServiceAsync(string dataJson, IFormFile? imageFile)
        {
            var className = typeof(T).Name;
            var newId = Guid.NewGuid();
            var entity = JsonConvert.DeserializeObject<T>(dataJson);
            await ValidateBeforeInsert(entity);
            entity?.GetType()?.GetProperty($"{className}Id")?.SetValue(entity, newId);
            if (imageFile != null && imageFile.Length > 0)
            {
                var res = await repository.InsertAsync(entity);
                if (res > 0)
                {
                    var image = new Images();
                    image?.GetType()?.GetProperty($"{className}Id")?.SetValue(image, newId);
                    await imagesService.InsertServiceAsync(image,imageFile);
                }
                return new MISAServiceResult
                {
                    Success = true,
                    Data = res,
                    StatusCode = System.Net.HttpStatusCode.Created,
                    Errors = null
                };
            }
            else
            {
                var result  = await repository.InsertAsync(entity);
                return new MISAServiceResult {
                    Success = true,
                    Data = result,
                    StatusCode = System.Net.HttpStatusCode.Created,
                    Errors = null
                };
            }

        }
        public virtual async Task<MISAServiceResult> UpdateServiceAsync(string dataJson, IFormFile? imageFile, Guid id)
        {
            var className = typeof(T).Name;
            var entity = JsonConvert.DeserializeObject<T>(dataJson);
            await ValidateBeforeUpdate(entity);
            //entity?.GetType()?.GetProperty($"{className}Id")?.SetValue(entity, id);
            if (imageFile != null && imageFile.Length > 0)
            {
                var res = await repository.UpdateAsync(entity,id );
                if (res > 0)
                {
                    var image = new Images();
                    await imagesService.UpdateServiceAsync(id, image, imageFile);
                }
                return new MISAServiceResult
                {
                    Success = true,
                    Data = res,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Errors = null
                };

            }
            else
            {

                var result = await repository.UpdateAsync(entity,id);
                return  new MISAServiceResult
                {
                    Success = true,
                    Data = result,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Errors = null
                };
            }
            
        }
        public virtual async Task ValidateBeforeInsert(T entity)
        {
            await Task.CompletedTask;
        }

        public virtual async Task ValidateBeforeUpdate(T entity)
        {
            await Task.CompletedTask;
        }
    }
}
