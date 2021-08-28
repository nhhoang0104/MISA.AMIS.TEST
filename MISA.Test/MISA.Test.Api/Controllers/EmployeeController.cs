using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Test.Core.Attributes;
using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.Test.Api.Controllers
{
    public class EmployeeController : BaseController<Employee>
    {
        IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService) : base(employeeService)
        {
            this._employeeService = employeeService;
        }

        /// <summary>
        /// Lấy dữ liệu theo bộ lộc và phân trang
        /// </summary>
        /// <param name="employeeFilter">thông tin filter(EmployeeCode hoặc PhoneNumber hoặc FullName</param>
        /// <param name="departmentId">id phòng ban</param>
        /// <param name="positionId">id vị trí</param>
        /// <param name="pageSize">kích cỡ trang</param>
        /// <param name="pageIndex">index trang</param>
        /// <returns>danh sách nhân viên</returns>
        [HttpGet("Filter")]
        public IActionResult GetByFilterPaging([FromQuery] string employeeFilter, [FromQuery] int pageSize, [FromQuery] int pageIndex)
        {
            try
            {
                var serviceResult = _employeeService.GetByFilterPaging(employeeFilter, pageSize, pageIndex);

                if (serviceResult.IsValid == true)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                else
                {
                    var errObj = new
                    {
                        devMsg = serviceResult.Messager,
                        userMsg = serviceResult.Messager,
                    };

                    return BadRequest(errObj);
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
                };

                return StatusCode(500, errObj);
            }
        }

        /// <summary>
        /// Kiểm tra mã nhân viên đã tồn tại chưa.
        /// </summary>
        /// <param name="employeeCode">mã nhân viên</param>
        /// <returns></returns>
        [HttpGet("CheckEmployeeExists/{employeeCode}")]
        public IActionResult CheckEmployeeExists(string employeeCode)
        {
            try
            {
                var serviceResult = this._employeeService.CheckEmployeeCodeExists(employeeCode);
                if (!serviceResult.IsValid)
                {
                    var errObj = new
                    {
                        devMsg = serviceResult.Messager,
                        userMsg = serviceResult.Messager,
                    };

                    return BadRequest(errObj);
                }

                return StatusCode(200, serviceResult.Data);
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
                };

                return StatusCode(500, errObj);
            }
        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới</returns>
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var serviceResult = this._employeeService.GetNewEmployeeCode();

                return StatusCode(200, serviceResult.Data);
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
                };

                return StatusCode(500, errObj);
            }
        }

        /// <summary>
        /// emport du lieu
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("Export")]
        public async Task<IActionResult> Export(CancellationToken cancellationToken)
        {
            // query data from database  
            await Task.Yield();

            var customers = new List<Employee>();

            var stream = new MemoryStream();

            var properties = typeof(Employee).GetProperties();

            using (var package = new ExcelPackage(stream))
            {

                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(customers, true);
                var column = 1;

                foreach (var prop in properties)
                {
                    var propMISAExport = prop.GetCustomAttributes(typeof(MISAPropExport), true);

                    workSheet.Cells.AutoFitColumns();

                    // xet cac truong export hay ko?
                    if (propMISAExport.Length > 0)
                    {
                        workSheet.Column(column).Hidden = true;
                    }

                    // dinh dang ngay thang nam
                    if (prop.PropertyType.Name.Contains(typeof(Nullable).Name) && prop.PropertyType.GetGenericArguments()[0] == typeof(DateTime))
                    {
                        workSheet.Column(column).Style.Numberformat.Format = "mm/dd/yyyy";
                    }

                    column++;
                }

                package.Save();
            }

            stream.Position = 0;
            string excelName = $"DanhSachNhanVien.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
