using BE_WAREHOUSE.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.Base
{
    public interface IBaseRepository<T> 
    {
        /// <summary>
        /// Thực hiện lấy tất cả thông tin 
        /// </summary>
        /// <returns>
        /// trả về một List danh sách thông tin
        /// </returns>
        /// Created by : TTPhong (18/03/2024)
        Task<IEnumerable<T>> GetAllAsync();
        /// <summary>
        /// Thực hiện lấy thông tin bằng id 
        /// </summary>
        /// <returns>
        /// trả về thông tin theo id
        /// </returns>
        /// Created by : TTPhong (18/03/2024)
        Task<T> GetByIdAsync(Guid id);
        /// <summary>
        /// Thực hiện lấy thông tin bằng id 
        /// </summary>
        /// <returns>
        /// trả về thông tin theo id
        /// </returns>
        /// Created by : TTPhong (18/03/2024)
        Task<IEnumerable<T>> GetByIdsAsync(List<Guid> ids);
        /// <summary>
        /// Thực hiện thêm mới dữ liệu 
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được thêm
        /// </returns>
        /// Created by : TTPhong (18/03/2024)
        Task<int> InsertAsync(T entity);
        /// <summary>
        /// Thực hiện cập nhập dữ liệu
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được cập nhật
        /// </returns>
        /// Created by : TTPhong (18/03/2024)
        Task<int> UpdateAsync(T entity, Guid id);
        /// <summary>
        /// Thực hiện xóa thông tin bằng id 
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được xóa
        /// </returns>
        /// Created by : TTPhong (18/03/2024)
        Task<int> DeleteByIdAsync(Guid id);
        /// <summary>
        /// Thực hiện xóa một vài chỉ định thông tin bằng id
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được xóa
        /// </returns>
        /// Created by : TTPhong (18/03/2024)
        Task<int> DeleteAnyAsync(List<Guid> ids);
        /// <summary>
        /// Thực hiện phân trang và tìm kiếm
        /// </summary>
        /// <returns>
        /// trả về số trang và danh sách kết quả
        /// </returns>
        /// Created by : TTPhong (18/03/2024)
        Task<PagingEntity<T>> PagingAndSearchAsync(int pageSize, int pageNumber, string? searchString);
        /// <summary>
        /// Thực hiện lấy mã lớn nhất
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được xóa
        /// </returns>
        /// Created by : TTPhong (18/03/2024)
        Task<string> GetMaxCodeAsync();
        /// <summary>
        /// Thực hiện kiểm tra tên sản phẩm cs trùng hay không
        /// </summary>
        /// <returns>
        /// trả về số lượng bản ghi được xóa
        /// </returns>
        /// Created by : TTPhong (18/03/2024)
        Task<bool> CheckExistNameAsync(string name);
    }
}
