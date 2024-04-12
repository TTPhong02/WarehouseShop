using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.Base
{
    public interface IBaseService<T> /*where T : class*/
    {
        /// <summary>
        /// Service thực hiện việc thêm mới 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by : TTPhong (18/03/2024)
        public Task<MISAServiceResult> InsertServiceAsync(string dataJson, IFormFile? imageFile);
        /// <summary>
        /// Service thực hiện việc update dư liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by : TTPhong (18/03/2024)
        public  Task<MISAServiceResult> UpdateServiceAsync(string dataJson, IFormFile? imageFile, Guid id);
    }
}
