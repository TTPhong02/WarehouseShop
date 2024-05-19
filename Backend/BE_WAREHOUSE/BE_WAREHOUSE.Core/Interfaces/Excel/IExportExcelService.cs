using BE_WAREHOUSE.Core.Model.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BE_WAREHOUSE.Core.Interfaces.Excel
{
    public interface IExportExcelService<T,TEntity>
    {
        public byte[] ExportExcelAsync(IEnumerable<T> data, ExcelRequest<TEntity> excelRequest);
    }
}
