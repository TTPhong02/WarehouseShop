using AutoMapper;
using BE_WAREHOUSE.Core.DTOs;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Excel;
using BE_WAREHOUSE.Core.Model.Excel;
using BE_WAREHOUSE.Core.Model.Revenue;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Services.Excel
{
    public class OrderExportExcelService : BaseExcelService<Orders,OrderDto>, IOrderExportExcelService
    {
        public OrderExportExcelService(IBaseRepository<Orders> repository, IMapper mapper) : base(repository,mapper)
        {
        }

        public byte[] ExportRevenueByProduct(ExcelRequest<RevenueProduct> excelRequest)
        {
            var stream = new MemoryStream();
            var columns = new string[] { "Tên sản phẩm", "Số lượng bán", "Doanh thu" };
            using (var excelPackage = new ExcelPackage(stream))
            {
                // đặt tên người tạo file
                excelPackage.Workbook.Properties.Author = "TTPhong";

                var worksheet = excelPackage.Workbook.Worksheets.Add("Báo cáo doanh thu");

                // set style mặc định cho toàn bộ file
                worksheet.Cells.Style.Font.Size = 14;
                worksheet.Cells.Style.Font.Name = "Times New Roman";

                // lấy ra số lượng cột cần dùng dựa vào số lượng header
                var countColHeader = columns.Count();

                // gán giá trị cho cell vừa merge
                worksheet.Cells[1, 1].Value = excelRequest.TitleHeader;
                // merge các column lại từ column 1 đến số column header và set style
                worksheet.Cells[1, 1, 2, countColHeader + 1].Merge = true;
                worksheet.Cells[1, 1, 2, countColHeader + 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1, 2, countColHeader + 1].Style.Font.Size = 24;
                worksheet.Cells[1, 1, 2, countColHeader + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var headerCol = 2;
                var headerRow = 3;

                worksheet.Cells[headerRow, 1].Value = "STT";

                //tạo các header từ column header đã tạo từ bên trên
                foreach (var column in columns)
                {
                    worksheet.Cells[headerRow, headerCol].Value = column;
                    headerCol++;
                }
                //set style cho header
                var cells = worksheet.Cells[headerRow, 1, headerRow, countColHeader + 1];
                //set màu thành gray
                var fill = cells.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                //căn chỉnh các border
                var border = cells.Style.Border;
                border.Bottom.Style =
                    border.Top.Style =
                    border.Left.Style =
                    border.Right.Style = ExcelBorderStyle.Thin;

                var currentRow = 4;
                var colSTT = 1;

                var properties = typeof(RevenueProduct).GetProperties();
                var totalCol = properties.ToList().Count;
                var data = excelRequest.Data;
                foreach (var item in data)
                {
                    worksheet.Cells[currentRow, 1].Value = colSTT;
                    var currentCol = 2;
                    //set style cho từng ô giá trị
                    worksheet.Cells[currentRow, currentCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    foreach (var property in properties)
                    {
                        var cellValue = property.GetValue(item);
                        if (cellValue != null)
                        {
                            if (property.PropertyType == typeof(double) || property.PropertyType == typeof(double?))
                            {
                                // Kiểm tra nếu kiểu dữ liệu là double hoặc double?
                                cellValue = ((double)cellValue).ToString("#,##0.00 đ"); // Hoặc cellValue = Convert.ToDouble(cellValue).ToString("#,##0.00 đ");
                                                                                        //set style cho từng ô giá trị
                                worksheet.Cells[currentRow, currentCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                        }
                        worksheet.Cells[currentRow, currentCol].Value = cellValue;
                        currentCol++;
                    }

                    worksheet.Cells.AutoFitColumns();
                    worksheet.Rows[currentRow].Height = 20;
                    currentRow++;
                    colSTT++;
                }

                excelPackage.Save();
                //chuyển đổi nội dung của đối tượng stream thành một mảng byte.
                var bytes = stream.ToArray();
                return bytes;
            }
        }


        public byte[] ExportRevenueByTime(ExcelRequest<Orders> excelRequest)
        {
            var ordersDto = excelRequest.Data.Select(item => MapEntityToDto(item));
            var columns = new List<string>();
            columns.Add("OrdersCode");
            columns.Add("ReminiscentName");
            columns.Add("PhoneNumber");
            columns.Add("FeeShipping");
            columns.Add("TotalAmount");
            excelRequest.Columns = columns;
            var res = ExportExcelAsync(ordersDto.ToList(), excelRequest);
            return res;
        }

        public override List<string> GetColumns()
        {
            var columns = new List<string>();
            columns.Add("OrdersCode");
            columns.Add("ReminiscentName");
            columns.Add("PhoneNumber");
            columns.Add("OrdersStatus");
            columns.Add("DeliveryStatus");
            columns.Add("DeliveryMethod");
            columns.Add("PaymentStatus");
            columns.Add("PaymentMethod");
            columns.Add("FeeShipping");
            columns.Add("TotalAmount");
            return columns;

        }
     }
}
