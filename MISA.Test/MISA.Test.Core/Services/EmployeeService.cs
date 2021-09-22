using MISA.Test.Core.Attributes;
using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repositories;
using MISA.Test.Core.Interfaces.Services;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;

namespace MISA.Test.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        #region Declaration

        IEmployeeRepository _employeeRepository;

        #endregion

        #region Contructor
        public EmployeeService(IBaseRepository<Employee> baseRepository, IEmployeeRepository employeeResponsitory) : base(baseRepository)
        {
            this._employeeRepository = employeeResponsitory;
        }

        #endregion

        #region Method

        /// <summary>
        /// Kiểm tra mã nhân viên đã tồn tại chưa ?
        /// </summary>
        /// <param name="EmployeeCode">mã nhân viên</param>
        /// <returns>
        /// true - tồn tại
        /// false - chưa
        /// </returns>
        /// CreatedBy: NHHoang (27/8/2021)
        public ServiceResult CheckEmployeeCodeExists(string employeeCode)
        {
            var serviceResult = new ServiceResult();

            if (employeeCode == null || employeeCode == "")
            {
                serviceResult.IsValid = false;
                serviceResult.Messager = Resources.ErrorMsg.EmployeeCode_ErrorMsg;
            };

            serviceResult.Data = this._employeeRepository.CheckEmployeeCodeExists(employeeCode);

            return serviceResult;
        }

        /// <summary>
        /// Kiểm tra nhân viên đã tồn tại chưa ?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResult CheckEmployeeExists(string id)
        {
            var serviceResult = new ServiceResult();

            // Validate id
            if (!this.ValidateId(id))
            {
                serviceResult.IsValid = false;
                serviceResult.Messager = Resources.ErrorMsg.IDInvalid_ErrorMsg;
                return serviceResult;
            }

            serviceResult.Data = this._employeeRepository.CheckEmployeeExists(Guid.Parse(id));

            return serviceResult;
        }

        /// <summary>
        /// Lấy danh sách nhân viên
        /// </summary>
        /// <param name="employeeFilter">thông tin filter(EmployeeCode hoặc PhoneNumber hoặc FullName)</param>
        /// <param name="departmentId">id phòng ban</param>
        /// <param name="positionId">id vị trí</param>
        /// <param name="pageSize">kích cỡ tragn</param>
        /// <param name="pageIndex">id trang</param>
        /// <returns></returns>
        /// CreatedBy: NHHoang (27/8/2021)
        public ServiceResult GetByFilterPaging(string employeeFilter, int pageSize, int pageIndex)
        {
            var serviceResult = new ServiceResult();

            var tmp = string.Empty;

            if (!(employeeFilter == null || employeeFilter == ""))
            {
                tmp = employeeFilter;
            };

            serviceResult.Data = this._employeeRepository.GetByFilterPaging(tmp, pageSize, pageIndex);

            return serviceResult;
        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NHHoang (27/8/2021)
        public ServiceResult GetNewEmployeeCode()
        {
            var serviceResult = new ServiceResult();

            var lastEmployeeCode = this._employeeRepository.GetBigEmployeeCode();

            if (lastEmployeeCode == null)
            {
                serviceResult.Data = "NV-001";
                return serviceResult;
            }

            // Tách mã nhân viên thành 2 phần: mã chứ, mã số
            string prefix = String.Empty;
            string code = String.Empty;
            for (var i = 0; i < lastEmployeeCode.Length; i++)
            {
                if (Char.IsDigit(lastEmployeeCode[i])) code += lastEmployeeCode[i];
                else prefix += lastEmployeeCode[i];
            }

            string newEmploeeCode = prefix + (Int64.Parse(code) + 1).ToString();
            serviceResult.Data = newEmploeeCode;

            return serviceResult;
        }

        /// <summary>
        /// xử lý xuất dữ liệu sang file excel
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="employeeFilter">lọc theo mã hoặc tên</param>
        /// <param name="pageSize">kích cỡ</param>
        /// <param name="pageIndex">index trang</param>
        /// <returns></returns>
        /// CreatedBy: NHHoang (01/09/2021)
        /// ModifiedBy: NHHoang(01/09/2021)
        public MemoryStream Export(CancellationToken cancellationToken, string employeeFilter, int pageSize, int pageIndex)
        {
            var stream = new MemoryStream();
            EmployeeFilterPaging employees = this._employeeRepository.GetByFilterPaging(employeeFilter, pageSize, pageIndex);
            List<Employee> employeesList = employees.Employees;
            var properties = typeof(Employee).GetProperties();

            using (var package = new ExcelPackage(stream))
            {

                var workSheet = package.Workbook.Worksheets.Add("Danh Sách Nhân Viên");
                //workSheet.Cells.LoadFromCollection(employees.Employees, true);

                // Chình tiêu đề trong bảng.

                // STT
                workSheet.Cells[3, 1].Value = "STT";
                workSheet.Cells[3, 1].Style.Font.Bold = true;
                workSheet.Cells[3, 1].Style.Fill.SetBackground(Color.LightGray);
                workSheet.Cells[3, 1].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                workSheet.Cells[3, 1].Style.Border.Top.Color.SetColor(Color.Black);
                workSheet.Cells[3, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                workSheet.Cells[3, 1].Style.Border.Bottom.Color.SetColor(Color.Black);
                workSheet.Cells[3, 1].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                workSheet.Cells[3, 1].Style.Border.Left.Color.SetColor(Color.Black);
                workSheet.Cells[3, 1].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                workSheet.Cells[3, 1].Style.Border.Right.Color.SetColor(Color.Black);


                var column = 2;

                foreach (var prop in properties)
                {
                    var propMISAExport = prop.GetCustomAttributes(typeof(MISAPropExport), true);

                    //Xét các trường có được export không?
                    if (propMISAExport.Length == 1)
                    {

                        // định dạng ngày/tháng/năm
                        if (prop.PropertyType.Name.Contains(typeof(Nullable).Name) && prop.PropertyType.GetGenericArguments()[0] == typeof(DateTime))
                        {
                            workSheet.Column(column).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }
                        else
                        {
                            workSheet.Column(column).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        }

                        workSheet.Cells[3, column].Value = (propMISAExport[0] as MISAPropExport).Name;
                        workSheet.Cells[3, column].Style.Font.Bold = true;
                        workSheet.Cells[3, column].Style.Fill.SetBackground(Color.LightGray);
                        workSheet.Cells[3, column].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        workSheet.Cells[3, column].Style.Border.Top.Color.SetColor(Color.Black);
                        workSheet.Cells[3, column].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        workSheet.Cells[3, column].Style.Border.Bottom.Color.SetColor(Color.Black);
                        workSheet.Cells[3, column].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        workSheet.Cells[3, column].Style.Border.Left.Color.SetColor(Color.Black);
                        workSheet.Cells[3, column].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        workSheet.Cells[3, column].Style.Border.Right.Color.SetColor(Color.Black);

                        column++;
                    }
                }

                // Chỉnh bản ghi vào hàng, cell
                for (int i = 0; i < employeesList.Count(); i++)
                {
                    workSheet.Cells[i + 4, 1].Value = i + 1;
                    workSheet.Cells[i + 4, 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[i + 4, 1].Style.Border.Top.Color.SetColor(Color.Black);
                    workSheet.Cells[i + 4, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[i + 4, 1].Style.Border.Left.Color.SetColor(Color.Black);
                    workSheet.Cells[i + 4, 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[i + 4, 1].Style.Border.Right.Color.SetColor(Color.Black);
                    workSheet.Cells[i + 4, 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    workSheet.Cells[i + 4, 1].Style.Border.Bottom.Color.SetColor(Color.Black);

                    int col = 2;

                    foreach (var prop in properties)
                    {
                        var propMISAExport = prop.GetCustomAttributes(typeof(MISAPropExport), true);

                        //Xét các trường có được export không?
                        if (propMISAExport.Length == 1)
                        {

                            if (prop.PropertyType.Name.Contains(typeof(Nullable).Name) && prop.PropertyType.GetGenericArguments()[0] == typeof(DateTime))
                            {
                                var tmp = employeesList[i].GetType().GetProperty(prop.Name).GetValue(employeesList[i], null);
                                workSheet.Cells[i + 4, col].Value = tmp == null ? "" : Convert.ToDateTime(tmp).ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                workSheet.Cells[i + 4, col].Value = employeesList[i].GetType().GetProperty(prop.Name).GetValue(employeesList[i], null);
                            }

                            workSheet.Cells.AutoFitColumns();

                            workSheet.Cells[i + 4, col].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            workSheet.Cells[i + 4, col].Style.Border.Top.Color.SetColor(Color.Black);
                            workSheet.Cells[i + 4, col].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            workSheet.Cells[i + 4, col].Style.Border.Bottom.Color.SetColor(Color.Black);
                            workSheet.Cells[i + 4, col].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            workSheet.Cells[i + 4, col].Style.Border.Left.Color.SetColor(Color.Black);
                            workSheet.Cells[i + 4, col].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            workSheet.Cells[i + 4, col].Style.Border.Right.Color.SetColor(Color.Black);

                            col++;
                        }
                    }
                }

                // Chỉnh tiêu đề cho workSheet
                workSheet.Cells["A1:I1"].Merge = true;
                workSheet.Cells["A2:I2"].Merge = true;
                workSheet.Cells[1, 1].Value = "DANH SÁCH NHÂN VIÊN";
                workSheet.Cells[1, 1].Style.Font.Size = 16;
                workSheet.Cells[1, 1].Style.Font.Bold = true;
                workSheet.Cells[1, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                package.Save();
            }

            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Lấy thống kê của các biến đổi nhân viên
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NHHoang (20/09/2021)
        public ServiceResult GetHumanReport(ReportingCriteria reportingCriteria)
        {
            var serviceResult = new ServiceResult();
            var employeess = new List<Employee>();
            var reportList = new List<Report>();

            // Lấy danh sách nhân viên
            employeess = this._employeeRepository.GetAll();

            // Khởi tạo report
            switch (reportingCriteria.ReportFollow)
            {
                case Enum.ReportFollow.MONTH:
                    for (int i = 0; i < 12; i++)
                    {
                        var report = new Report();
                        report.Label = (i + 1).ToString();
                        reportList.Add(report);
                    }
                    break;
                case Enum.ReportFollow.QUARTER:
                    for (int i = 0; i < 4; i++)
                    {
                        var report = new Report();
                        report.Label = (i + 1).ToString();
                        reportList.Add(report);
                    }
                    break;
                default:
                    break;
            }

            // xử lý
            foreach (var employee in employeess)
            {
                if (reportingCriteria.Type == Enum.TypeReport.ADD || reportingCriteria.Type == Enum.TypeReport.MUTATE)
                {
                    var index = GetIndex(employee.CreatedDate, reportingCriteria, reportList);

                    if (index != null)
                    {
                        if (index == -1)
                        {
                            var report = new Report();
                            report.Label = GetTime(employee.CreatedDate, reportingCriteria.ReportFollow, reportingCriteria.Year).ToString();
                            report.Quantity += 1;
                            report.QuantityAdd += 1;
                            reportList.Add(report);
                        }
                        else
                        {
                            reportList[index.Value].Quantity += 1;
                            reportList[index.Value].QuantityAdd += 1;
                        }
                    }
                }

                if (reportingCriteria.Type == Enum.TypeReport.UPDATE || reportingCriteria.Type == Enum.TypeReport.MUTATE)
                {
                    var index = GetIndex(employee.ModifiedDate, reportingCriteria, reportList);

                    if (index != null)
                    {
                        if (index == -1)
                        {
                            var report = new Report();
                            report.Label = GetTime(employee.ModifiedDate, reportingCriteria.ReportFollow, reportingCriteria.Year).ToString();
                            report.Quantity += 1;
                            report.QuantityUpdate += 1;
                            reportList.Add(report);
                        }
                        else
                        {
                            reportList[index.Value].Quantity += 1;
                            reportList[index.Value].QuantityUpdate += 1;
                        }
                    }
                }

                if (reportingCriteria.Type == Enum.TypeReport.DELETE || reportingCriteria.Type == Enum.TypeReport.MUTATE)
                {
                    if (employee.IsStop)
                    {
                        var index = GetIndex(employee.ModifiedDate, reportingCriteria, reportList);

                        if (index != null)
                        {
                            if (index == -1)
                            {
                                var report = new Report();
                                report.Label = GetTime(employee.ModifiedDate, reportingCriteria.ReportFollow, reportingCriteria.Year).ToString();
                                report.Quantity += 1;
                                report.QuantityDelete += 1;
                                reportList.Add(report);
                            }
                            else
                            {
                                reportList[index.Value].Quantity += 1;
                                reportList[index.Value].QuantityDelete += 1;
                            }
                        }
                    }
                }
            }

            serviceResult.Data = reportList;

            return serviceResult;
        }

        /// <summary>
        /// Vaidate max
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// CreatedBy: NHHoang (27/8/2021)
        protected override bool ValidateEntityCode(Employee entity)
        {
            return !this._employeeRepository.CheckEmployeeCodeExists(entity.EmployeeCode);

        }

        /// <summary>
        /// Lấy năm hoặc tháng hoặc quý
        /// </summary>
        /// <param name="time">thời gian</param>
        /// <param name="reportFollow">thống kê theo</param>
        /// <param name="year">năm</param>
        /// <returns></returns>
        /// CreatedBy: NHHoang (20/09/2021)
        private Int32 GetTime(DateTime? time, Enum.ReportFollow reportFollow, Int32? year)
        {
            if (time == null) return -1;

            switch (reportFollow)
            {
                case Enum.ReportFollow.MONTH:
                    if (year == Int32.Parse(time?.Year.ToString())) return Int32.Parse(time?.ToString("MM"));
                    return -1;
                case Enum.ReportFollow.QUARTER:
                    if (year == Int32.Parse(time?.Year.ToString())) return (Int32.Parse(time?.ToString("MM")) + 2) / 3;
                    return -1;
                case Enum.ReportFollow.YEAR:
                    return Int32.Parse(time?.Year.ToString());
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Lấy index của danh sách báo cáo
        /// </summary>
        /// <param name="time">thời gian</param>
        /// <param name="reportingCriteria">tiêu chí báo cáo</param>
        /// <param name="reportList">danh sách báo cáo</param>
        /// <returns></returns>
        /// CreatedBy: NHHoang (20/09/2021)
        private Int32? GetIndex(DateTime? time, ReportingCriteria reportingCriteria, List<Report> reportList)
        {
            if (reportingCriteria.ReportFollow == Enum.ReportFollow.YEAR)
            {
                var year = GetTime(time, reportingCriteria.ReportFollow, reportingCriteria.Year);

                if (year != -1)
                {
                    var index = reportList.FindIndex(delegate (Report item)
                    {
                        return item.Label == year.ToString();
                    });

                    return index;
                }

                return null;
            }
            else
            {
                var month = GetTime(time, reportingCriteria.ReportFollow, reportingCriteria.Year);

                return month == -1 ? null : month - 1;
            }
        }

        #endregion
    }
}
