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

        #region Declaration

        IEmployeeService _employeeService;

        #endregion

        #region Methods
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
        /// CreatedBy: NHHoang (27/8/2021)
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
        /// CreatedBy: NHHoang (27/8/2021)
        [HttpGet("CheckEmployeeCodeExists/{employeeCode}")]
        public IActionResult CheckEmployeeCodeExists(string employeeCode)
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
        /// Kiểm tra nhân viên đã tồn tại chưa.
        /// </summary>
        /// <param name="id">mã nhân viên</param>
        /// <returns></returns>
        /// CreatedBy: NHHoang (27/8/2021)
        [HttpGet("CheckEmployeeExists/{id}")]
        public IActionResult CheckEmployeeExists(string id)
        {
            try
            {
                var serviceResult = this._employeeService.CheckEmployeeExists(id);
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
        /// CreatedBy: NHHoang (27/8/2021)
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
        /// xuất dữ liệu file excel
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// CreatedBy: NHHoang (27/8/2021)
        /// CreatedBy: NHHoang (02/09/2021)
        [HttpGet("Export")]
        public IActionResult Export(CancellationToken cancellationToken, [FromQuery] string employeeFilter, [FromQuery] int pageSize, [FromQuery] int pageIndex)
        {

            var stream = this._employeeService.Export(cancellationToken, employeeFilter, pageSize, pageIndex);
            string excelName = $"DanhSachNhanVien.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        #endregion
    }
}
