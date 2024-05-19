using BE_WAREHOUSE.API.Controllers.Base;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Enum;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Excel;
using BE_WAREHOUSE.Core.Interfaces.Order;
using BE_WAREHOUSE.Core.Model.Excel;
using BE_WAREHOUSE.Core.Model.Revenue;
using BE_WAREHOUSE.Core.Services.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace BE_WAREHOUSE.API.Controllers.Order
{
    [ApiController]
    public class OrdersController : BaseController<Orders>
    {
        IOrdersService _ordersService;
        IOrdersRepository _ordersRepository;
        IOrderExportExcelService _orderExportExcelService;
        public OrdersController(IBaseRepository<Orders> baseRepository, IBaseService<Orders> baseService, IOrdersService ordersService, IOrdersRepository ordersRepository, IOrderExportExcelService orderExportExcelService) : base(baseRepository, baseService)
        {
            _ordersService = ordersService;
            _ordersRepository = ordersRepository;
            _orderExportExcelService= orderExportExcelService;
        }
        [HttpPost("Checkout")]
        public async Task<IActionResult> CheckOutAsync(OrderData orderData) 
        {
            var res = await _ordersService.CheckoutService(orderData);
            return StatusCode(201,res);
        }
        [HttpGet("UsersId/{id}")]
        public async Task<IActionResult> GetOrderByUsersId(Guid id)
        {
            var res = await _ordersService.GetOrderByUsersId(id);
            return Ok(res);
        }
        [HttpPost("RevenueYear")]
        public async Task<IActionResult> GetRevenueByYear(List<DateTime> Date)
        {
            var res = await _ordersRepository.GetRevenueDataByYear(Date);
            return Ok(res);
        }
        [HttpPost("RevenueDate")]
        public async Task<IActionResult> GetRevenueByDate(List<DateTime> Date)
        {
            var res = await _ordersRepository.GetRevenueDataByDate(Date);
            return Ok(res);
        }
        [HttpPost("RevenueMonth")]
        public async Task<IActionResult> GetRevenueByMonth(List<DateTime> Date)
        {
            var res = await _ordersRepository.GetRevenueDataByMonth(Date);
            return Ok(res);
        }
        [HttpGet("RevenueMonthOfYear/{Date}")]
        public async Task<IActionResult> GetRevenueByMonthOfYear(DateTime Date)
        {
            var res = await _ordersRepository.GetRevenueDataByMonthOfYear(Date);
            return Ok(res);
        }
        [HttpPost("OrdersTime")]
        public async Task<IActionResult> GetOrderByTime(List<DateTime> Date)
        {
            var res = await _ordersRepository.GetOrderDataByTime(Date);
            return Ok(res);
        }
        [HttpGet("Revenue")]
        public async Task<IActionResult> GetTotalRevenue() {
            var res = await _ordersRepository.GetSumRevenue();
            return Ok(res);
        }
        [HttpGet("RevenueTotalYear/{date}")]
        public async Task<IActionResult> GetTotalRevenueYear(DateTime date)
        {
            var res = await _ordersRepository.GetSumRevenueByYear(date);
            return Ok(res);
        }
        [HttpGet("RevenueTotalMonth/{date}")]
        public async Task<IActionResult> GetTotalRevenueMonth(DateTime date)
        {
            var res = await _ordersRepository.GetSumRevenueByMonth(date);
            return Ok(res);
        }
        [HttpGet("RevenueTotalDate/{date}")]
        public async Task<IActionResult> GetTotalRevenueDate(DateTime date)
        {
            var res = await _ordersRepository.GetSumRevenueByDate(date);
            return Ok(res);
        }
        [HttpGet("UserMostOrder")]
        public async Task<IActionResult> GetUserMostOrder()
        {
            var res = await _ordersRepository.GetUserMostOrders();
            return Ok(res);
        }
        [HttpGet("TotalOrder/{type}")]
        public async Task<IActionResult> GetTotalOrder(string type)
        {
            var res = await _ordersRepository.GetNumberTotalOrder(type);
            return Ok(res);
        }
        [HttpGet("OrderStatus/{status}")]
        public async Task<IActionResult> GetTotalOrderByStatus(OrderStatus status)
        {
            var res = await _ordersRepository.GetOrdersByStatus(status);
            return Ok(res);
        }
        [HttpGet("FilterOrderByStatus")]
        public async Task<IActionResult> FilterOrderByStatus(int pageSize, int pageNumber, int? orderStatus, int? paymentStatus, int? deliveryStatus, string? searchString)
        {
            var res = await _ordersRepository.FilterOrderByStatus(pageSize, pageNumber, orderStatus, paymentStatus, deliveryStatus, searchString);
            return Ok(res);
        }
        [HttpPost("Export")]
        public async Task<IActionResult> ExportToExcel([FromBody] ExcelRequest<Orders> excelRequest)
        {
            var contenType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var fileName = "Danh sách đơn hàng";
            if (excelRequest.EntityIds?.Count() > 0)
            {
                var bytes = await _orderExportExcelService.ExportListAsync(excelRequest);
                return File(bytes, contenType, fileName);
            }
            var res = await _orderExportExcelService.ExportAllAsync(excelRequest);

            return File(res, contenType, fileName);
        }

        [HttpPost("Export/[action]")]
        public IActionResult ExportRevenueByTime(ExcelRequest<Orders> excelRequest)
        {
            var contenType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var fileName = "Thống kê doanh thu";

            var res = _orderExportExcelService.ExportRevenueByTime(excelRequest);

            return File(res, contenType, fileName);
        }

        [HttpPost("Export/[action]")]
        public IActionResult ExportRevenueByProduct(ExcelRequest<RevenueProduct> excelRequest)
        {
            var contenType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var fileName = "Thống kê doanh thu";

            var res = _orderExportExcelService.ExportRevenueByProduct(excelRequest);

            return File(res, contenType, fileName);
        }
    }
}
