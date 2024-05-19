using BE_WAREHOUSE.Core.Interfaces.Excel;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_WAREHOUSE.Core.Enum;
using BE_WAREHOUSE.Core.Model.Excel;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using BE_WAREHOUSE.Core.Helper;

namespace BE_WAREHOUSE.Core.Services.Excel
{
    public abstract class ExcelService<TDto, TEntity> : IExportExcelService<TDto, TEntity>
    {
        public byte[] ExportExcelAsync(IEnumerable<TDto> data, ExcelRequest<TEntity> excelRequest)
        {
            var properties = typeof(TDto).GetProperties();
            var stream = new MemoryStream();
            using (var excelPackage = new ExcelPackage(stream))
            {
                // đặt tên người tạo file
                excelPackage.Workbook.Properties.Author = "LQHUY";

                var worksheet = excelPackage.Workbook.Worksheets.Add(excelRequest.WorksheetName);

                // set style mặc định cho toàn bộ file
                worksheet.Cells.Style.Font.Size = 14;
                worksheet.Cells.Style.Font.Name = "Times New Roman";

                // lấy ra số lượng cột cần dùng dựa vào số lượng header
                var countColHeader = excelRequest.Columns.Count();

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
                foreach (var column in excelRequest.Columns)
                {
                    foreach (var property in properties)
                    {
                        if (property.Name == column)
                        {
                            //gán giá trị
                            var displayNameAttribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), true) as DisplayNameAttribute[];
                            worksheet.Cells[headerRow, headerCol].Value = displayNameAttribute[0].DisplayName;
                            headerCol++;
                        }
                    }
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

                SetCellValueEnterFileExcel(data, excelRequest.Columns, worksheet, properties);

                excelPackage.Save();

                //chuyển đổi nội dung của đối tượng stream thành một mảng byte.
                var bytes = stream.ToArray();
                return bytes;

            }
        }

        private void SetCellValueEnterFileExcel(IEnumerable<TDto> data, List<string> columns, ExcelWorksheet worksheet, PropertyInfo[] properties)
        {
            var currentRow = 4;
            var colSTT = 1;
            var totalCol = columns.Count();
            double totalAmount = 0;
            //gán giá trị cho từng dòng và cột

            foreach (var item in data)
            {
                worksheet.Cells[currentRow, 1].Value = colSTT;
                var currentCol = 2;
                worksheet.Cells[currentRow, 1, currentRow, totalCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                foreach (var column in columns)
                {
                    foreach (var property in properties)
                    {

                        if (column.ToLower() == property.Name.ToLower())
                        {
                            var cellValue = property.GetValue(item);
                            if (cellValue != null)
                            {
                                if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                                {
                                    // Kiểm tra và định dạng nếu kiểu dữ liệu là DateTime hoặc DateTime?
                                    DateTime? nullableDateTime = cellValue as DateTime?;
                                    if (nullableDateTime.HasValue)
                                    {
                                        // Chuyển đổi giá trị thành DateTime trước khi định dạng
                                        DateTime actualDateTime = nullableDateTime.Value;
                                        cellValue = actualDateTime.ToString("dd/MM/yyyy HH:mm");
                                    }
                                    else
                                    {
                                        cellValue = ""; // Hoặc giá trị mặc định khác nếu thích
                                    }
                                }
                            }
                            if (cellValue != null)
                            {
                                if (property.PropertyType == typeof(double) || property.PropertyType == typeof(double?))
                                {
                                    if (column == "TotalAmount")
                                    {
                                        totalAmount += (double)cellValue;
                                    }
                                    // Kiểm tra nếu kiểu dữ liệu là double hoặc double?
                                    cellValue = ((double)cellValue).ToString("#,##0.00 đ"); // Hoặc cellValue = Convert.ToDouble(cellValue).ToString("#,##0.00 đ");
                                    //thiết lập giá trị tiền nằm bên phải
                                    worksheet.Cells[currentRow, currentCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                }
                            }
                            if (property.PropertyType == typeof(OrderStatus) || property.PropertyType == typeof(OrderStatus?))
                            {
                                cellValue = ValueFormat.GetOrderStatusFormat((OrderStatus)cellValue);
                            }
                            if (property.PropertyType == typeof(DeliveryStatus) || property.PropertyType == typeof(DeliveryStatus?))
                            {
                                cellValue = ValueFormat.GetDeliveryStatusFormat((DeliveryStatus)cellValue);
                            }
                            if (property.PropertyType == typeof(PaymentStatus) || property.PropertyType == typeof(PaymentStatus?))
                            {
                                cellValue = ValueFormat.GetPaymentStatusFormat((PaymentStatus)cellValue);
                            }
                            if (property.PropertyType == typeof(PaymentMethod) || property.PropertyType == typeof(PaymentMethod?))
                            {
                                cellValue = ValueFormat.GetPaymentMethodFormat((PaymentMethod)cellValue);
                            }
                            if (property.PropertyType == typeof(DeliveryMethod) || property.PropertyType == typeof(DeliveryMethod?))
                            {
                                cellValue = ValueFormat.GetDeliveryMethodFormat((DeliveryMethod)cellValue);
                            }

                            //gán giá trị cho từng cell                      
                            worksheet.Cells[currentRow, currentCol].Value = cellValue;
                            currentCol++;
                            break;
                        }
                    }
                }



                //set style cho từng ô giá trị

                worksheet.Cells.AutoFitColumns();
                worksheet.Rows[currentRow].Height = 20;

                colSTT++;
                currentRow++;

            }
            var propertyExist = properties.FirstOrDefault(p => p.Name == "TotalAmount");
            if (propertyExist != null)
            {
                // merge các column lại từ column 1 đến số column header và set style
                worksheet.Cells[currentRow, 1, currentRow, totalCol].Merge = true;
                worksheet.Cells[currentRow, 1, currentRow, totalCol].Style.Font.Bold = true;
                worksheet.Cells[currentRow, 1, currentRow, totalCol].Style.Font.Size = 14;
                worksheet.Cells[currentRow, 1, currentRow, totalCol].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[currentRow, 1].Value = string.Format("Tổng cộng: ");
                worksheet.Cells[currentRow, 1, currentRow, totalCol + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[currentRow, 1, currentRow, totalCol + 1].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                worksheet.Cells[currentRow, 1, currentRow, totalCol + 1].Style.Font.Bold = true;

                var startCellAddress = worksheet.Cells[4, totalCol + 1].Address;
                var endCellAddress = worksheet.Cells[currentRow - 1, totalCol + 1].Address;
                worksheet.Cells[currentRow, totalCol + 1].Value = ((double)totalAmount).ToString("#,##0.00 đ");
            }
        }
    }
}
