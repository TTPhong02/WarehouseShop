using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Infrastructure.DbContext
{
    public class MariaDbContext : IDbContext
    {
        public IDbConnection Connection { get;}
        public IDbTransaction Transaction { get; set; }

        public MariaDbContext(IConfiguration config)
        {
            Connection = new MySqlConnection(config.GetConnectionString("DatabaseMariaDb" + ""));
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var className = typeof(T).Name;
            var sql = $"SELECT * FROM view_{className}";
            var res = await Connection.QueryAsync<T>(sql);
            return res;
        }

        public async Task<T?> GetByIdAsync<T>(Guid id)
        {
            var className = typeof(T).Name;
            var sql = $"SELECT * FROM view_{className} where {className}Id = @{className}Id";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@{className}Id", id);
            var res = await Connection.QueryFirstOrDefaultAsync<T>(sql, param:dynamicParameters);
            return res;
        }

        public async Task<int> InsertAsync<T>(T entity)
        {
            var className = typeof(T).Name;
            var procedure = $"Proc_Insert_{className}";
            var res = await Connection.ExecuteAsync(procedure, entity);
            return res;
        }

        public async Task<int> UpdateAsync<T>(T entity, Guid id)
        {
            var className = typeof(T).Name;
            var procedure = $"Proc_Update_{className}";
            entity?.GetType()?.GetProperty($"{className}Id")?.SetValue(entity, id);
            var res = await Connection.ExecuteAsync(procedure,entity);
            return res;
        }

        public async Task<int> DeleteAsync<T>(Guid id)
        {
            var className = typeof(T).Name;
            var sql = $"DELETE FROM {className} where {className}Id = @{className}Id";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@{className}Id", id);
            var res = await Connection.ExecuteAsync(sql, dynamicParameters);
            return res;
        }

        public async Task<int> DeleteAnyAsync<T>(List<Guid> ids)
        {
            var className = typeof(T).Name;
            var sql = $"DELETE FROM {className} WHERE {className}Id IN @ListId";
            var res = await Connection.ExecuteAsync(sql, new { ListId = ids });
            return res;
        }

        public async Task<PagingEntity<T>> PagingAndSearchAsync<T>(int pageSize, int pageNumber, string? searchString)
        {
            var className = typeof(T).Name;
            var procedure = $"Proc_Filter_{className}";

            PagingEntity<T> pagingEntity = new PagingEntity<T>();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageNumber", pageNumber);
            parameters.Add("@searchString", searchString);

            parameters.Add("@totalRecord", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            parameters.Add("@totalPage", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var res = await Connection.QueryAsync<T>(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

            pagingEntity.Data = res;
            pagingEntity.TotalRecord = parameters.Get<int>("@totalRecord");
            pagingEntity.TotalPage = parameters.Get<int>("@totalPage");

            return pagingEntity;
        }

        public async Task<string> GetMaxCodeAsync<T>()
        {
            var className = typeof(T).Name; 
            var procedure = $"Proc_GetMaxCode_{className}";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@MaxCode", dbType: DbType.String, direction: ParameterDirection.Output);

            await Connection.QueryAsync<T>(sql: procedure, parameters, commandType: CommandType.StoredProcedure);



            var MaxCode = GetPrefixNewCode(className) + "-" + parameters.Get<string>("@MaxCode");
            return MaxCode;
        }
        public string GetPrefixNewCode(string tableName)
        {
            switch (tableName)
            {
                case "Categories":
                    return "DM";
                case "Product":
                    return "SP";
                case "Orders":
                    return "OD";
                default:
                    return "";
            }
        }

        public Task<bool> CheckExistNameAsync<T>(string name)
        {
            throw new NotImplementedException();
        }
    }
}
