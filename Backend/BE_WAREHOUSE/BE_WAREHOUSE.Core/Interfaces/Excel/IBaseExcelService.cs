using BE_WAREHOUSE.Core.Model.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BE_WAREHOUSE.Core.Interfaces.Excel
{
    public interface IBaseExcelService<T>
    {
        Task<byte[]> ExportAllAsync(ExcelRequest<T> excelRequest);
        Task<byte[]> ExportListAsync(ExcelRequest<T> excelRequest);
    }
}
