using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Model.Excel;
using BE_WAREHOUSE.Core.Model.Revenue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.Excel
{
    public interface IOrderExportExcelService:IBaseExcelService<Orders>
    {
        byte[] ExportRevenueByTime(ExcelRequest<Orders> excelRequest);
        byte[] ExportRevenueByProduct(ExcelRequest<RevenueProduct> excelRequest);
    }
}
