using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Entities
{
    public class PagingEntity<T> 
    {
        #region Properties
        /// <summary>
        /// Tổng số trang 
        /// </summary>
        /// Created By: TTPhong(24/01/2024)
        public int TotalPage { get; set; }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        /// Created By: TTPhong(24/01/2024)
        public int TotalRecord { get; set; }

        /// <summary>
        /// Danh sách bản ghi 
        /// </summary>
        /// Created By: TTPhong(24/01/2024)
        public IEnumerable<T> Data { get; set; }
        #endregion
    }
}
