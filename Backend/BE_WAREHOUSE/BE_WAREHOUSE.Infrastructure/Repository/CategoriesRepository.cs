using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Category;
using BE_WAREHOUSE.Infrastructure.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Infrastructure.Repository
{
    public class CategoriesRepository : BaseRepository<Categories>,ICategoriesRepository
    {
        public CategoriesRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Categories> GetCategoriesBySlug(string slug)
        {
            var sql = $"SELECT * FROM Categories WHERE CategoriesSlug = '{slug}'";
            var res = await _dbContext.Connection.QueryFirstOrDefaultAsync<Categories>(sql);
            return res;
        }
    }
}
