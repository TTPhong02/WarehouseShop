using BE_WAREHOUSE.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BE_WAREHOUSE.Infrastructure.Interfaces
{
    public interface IDbContext
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; set; }
        /// <summary>
        /// /// Thực hiện lấy tất cả thông tin 
        /// </summary>
        /// <returns>
        /// trả về tất cả thông tin 
        /// </summary>
        /// <typeparam name="Y"></typeparam>
        /// <returns></returns>
        /// Created by : TTPhong (16/03/2023)
        Task<IEnumerable<T>> GetAllAsync<T>();
        /// <summary>
        /// Thực hiện lấy thông tin bằng id 
        /// </summary>
        /// <returns>
        /// trả về thông tin theo id
        /// </returns>
        /// Created by : TTPhong (16/03/2023)
        Task<T?> GetByIdAsync<T>(Guid id);
        Task<IEnumerable< T?>> GetByIdsAsync<T>(List<Guid> ids);
        /// <summary>
        /// Thực hiện thêm mới dữ liệu 
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được thêm
        /// </returns>
        /// Created by : TTPhong (16/03/2023)
        Task<int> InsertAsync<T>(T entity);
        /// <summary>
        /// Thực hiện cập nhập dữ liệu
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được cập nhật
        /// </returns>
        /// Created by : TTPhong (16/03/2023)
        Task<int> UpdateAsync<T>(T entity, Guid id);
        /// <summary>
        /// Thực hiện xóa thông tin bằng id 
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được xóa
        /// </returns>
        /// Created by : TTPhong (16/03/2023)
        Task<int> DeleteAsync<T>(Guid id);
        /// <summary>
        /// Thực hiện xóa một vài chỉ định
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được xóa
        /// </returns>
        /// Created by : TTPhong (16/03/2023)
        Task<int> DeleteAnyAsync<T>(List<Guid> ids);
        /// <summary>
        /// Thực hiện phân trang và tìm kiếm
        /// </summary>
        /// <returns>
        /// trả về số trang và danh sách kết quả
        /// </returns>
        /// Created by : TTPhong (18/03/2024)
        Task<PagingEntity<T>> PagingAndSearchAsync<T>(int pageSize, int pageNumber, string? searchString);
        /// <summary>
        /// Thực hiện lấy mã lớn nhất
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được xóa
        /// </returns>
        /// Created by : TTPhong (18/03/2024)
        Task<string> GetMaxCodeAsync<T>();
        /// <summary>
        /// Thực hiện kiểm tra tên sản phẩm cs trùng hay không
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được xóa
        /// </returns>
        /// Created by : TTPhong (18/03/2024)
        Task<bool> CheckExistNameAsync<T>(string name);
    }
}
