using BE_WAREHOUSE.Core.Interfaces.Base;
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

        public BaseService(IBaseRepository<T> repository)
        {
            this.repository = repository;
        }

        public async Task<MISAServiceResult> InsertServiceAsync(T entity)
        {
            var className = typeof(T).Name;
            var newId = Guid.NewGuid();
            await ValidateBeforeInsert(entity);
            entity?.GetType()?.GetProperty($"{className}Id")?.SetValue(entity, newId);
            var res = await repository.InsertAsync(entity);
            return new MISAServiceResult {
                Success = true,
                Data = res,
                StatusCode = System.Net.HttpStatusCode.Created,
                Errors = null
            };

        }
        public async Task<MISAServiceResult> UpdateServiceAsync(T entity, Guid id)
        {
            var className = typeof(T).Name;
            await ValidateBeforeUpdate(entity);
            var res = await repository.UpdateAsync(entity,id);
            return new MISAServiceResult
            {
                Success = true,
                Data = res,
                StatusCode = System.Net.HttpStatusCode.OK,
                Errors = null
            };
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
