using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected IDbContext _dbContext;

        public BaseRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CheckExistNameAsync(string name)
        {
            var res = await _dbContext.CheckExistNameAsync<T>(name);
            return res;
        }

        public async Task<int> DeleteAnyAsync(List<Guid> ids)
        {
            var res = await _dbContext.DeleteAnyAsync<T>(ids);
            return res;
        }

        public async Task<int> DeleteByIdAsync(Guid id)
        {
            var res = await _dbContext.DeleteAsync<T>(id);
            return res;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var res = await _dbContext.GetAllAsync<T>();
            return res;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var res = await _dbContext.GetByIdAsync<T>(id);
            return res;
        }

        public async Task<string> GetMaxCodeAsync()
        {
            var res = await _dbContext.GetMaxCodeAsync<T>();
            return res;
        }

        public async Task<int> InsertAsync(T entity)
        {
            var res = await _dbContext.InsertAsync(entity);
            return res;
        }

        public async Task<PagingEntity<T>> PagingAndSearchAsync(int pageSize, int pageNumber, string? searchString)
        {
            var res = await _dbContext.PagingAndSearchAsync<T>(pageSize, pageNumber, searchString);
            return res;
        }

        public async Task<int> UpdateAsync(T entity, Guid id)
        {
            var res = await _dbContext.UpdateAsync(entity, id);
            return res;
        }
    }
}
