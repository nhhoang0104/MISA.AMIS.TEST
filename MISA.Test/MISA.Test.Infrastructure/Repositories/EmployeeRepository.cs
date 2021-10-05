using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        #region Contructor

        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }

        #endregion

        #region Method

        /// <summary>
        /// Kiểm tra mã nhiên viên đã có trong hệ thống chưa 
        /// </summary>
        /// <param name="employeeCode">mã nhân viên</param>
        /// <returns>
        /// true - tồn tại
        /// false - chưa tồn tại
        /// </returns>
        /// CreatedBy: NHHoang (27/8/2021)
        public bool CheckEmployeeCodeExists(string employeeCode)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@EmployeeCode", employeeCode);
            dynamicParameters.Add("@IsExists", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                dbConnection.Execute("Proc_CheckEmployeeCodeExists", dynamicParameters, commandType: CommandType.StoredProcedure);
                Int32 isExists = dynamicParameters.Get<Int32>("@IsExists");
                return isExists == 1 ? true : false;
            }
        }

        /// <summary>
        /// Kiểm tra nhân viên tồn tại không ?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckEmployeeExists(Guid id)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@EmployeeId", id);
            dynamicParameters.Add("@IsExists", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                dbConnection.Execute("Proc_CheckEmployeeExists", dynamicParameters, commandType: CommandType.StoredProcedure);
                Int32 isExists = dynamicParameters.Get<Int32>("@IsExists");
                return isExists == 1 ? true : false;
            }
        }

        /// <summary>
        /// Lấy danh sach mã nhân viên
        /// </summary>
        /// <returns>
        /// Danh sách mã nhân viên
        /// </returns>
        /// CreatedBy: NHHoang (27/8/2021)
        public List<string> GetAllEmployeeCode()
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var empoyeeCodeList = dbConnection.Query<string>("Proc_GetAllEmployeeCode", commandType: CommandType.StoredProcedure);

                return (List<string>)empoyeeCodeList;
            }
        }

        /// <summary>
        /// Lấy mẫ nhân viên lớn nhát
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// CreatedBy: NHHoang (27/8/2021)
        public String GetBigEmployeeCode()
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var empoyeeCodeList = dbConnection.QueryFirstOrDefault<String>("Proc_GetBigEmployeeCode", commandType: CommandType.StoredProcedure);

                return empoyeeCodeList;
            }
        }

        /// <summary>
        /// Lấy danh sách sách nhân viên theo bộ lọc( tên nhân viên, mã nhân viên, số điện thoại) và phân trang
        /// </summary>
        /// <param name="employeeFilter">thông tin bộ lọc</param>
        /// <param name="pageSize">vị trí trang</param>
        /// <param name="pageIndex">kích cỡ của trang</param>
        /// <returns>
        /// danh sách nhân viên
        /// </returns>
        /// CreatedBy: NHHoang (27/8/2021)
        /// ModifiedBy: NHHoang (01/09/2021) - Tạo các class để lấy danh dách nhân viên, tổng số trang, tổng số bản ghi
        public List<Employee> GetByFilterPaging(string employeeFilter, int pageSize, int pageIndex)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@EmployeeFilter", employeeFilter == "" ? null : employeeFilter);
            parameters.Add("@Size", pageSize);
            parameters.Add("@Offset", pageIndex);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var employees = dbConnection.Query<Employee>("Proc_GetEmployeeFilterPaging", param: parameters, commandType: CommandType.StoredProcedure);

                return (List<Employee>)employees;
            }
        }

        /// <summary>
        /// Lấy thông tin trang
        /// </summary>
        /// <param name="employeeFilter">thông tin bộ lọc</param>
        /// <param name="pageSize">vị trí trang</param>
        /// <param name="pageIndex">kích cỡ của trang</param>
        /// <returns></returns>
        /// CreatedBy: NHHoang (01/10/2021)
        public InfoPage GetInfoPage(string employeeFilter, int pageSize, int pageIndex)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@EmployeeFilter", employeeFilter == "" ? null : employeeFilter);
            parameters.Add("@Size", pageSize);
            parameters.Add("@Offset", pageIndex);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var employees = dbConnection.Execute("Proc_GetInfoPage", param: parameters, commandType: CommandType.StoredProcedure);

                var data = new InfoPage() { TotalPage = parameters.Get<Int32>("@TotalPage"), TotalRecord = parameters.Get<Int32>("@TotalRecord") };

                return data;
            };
        }

        /// <summary>
        /// danh sách báo cáo nhân sự
        /// </summary>
        /// <param name="reportingCriteria">các tiêu chí của báo báo</param>
        /// <returns>báo cáo</returns>
        public List<Report> GetEmployeeReport(ReportingCriteria reportingCriteria)
        {
            // Cách 1
            //var parameters = new DynamicParameters();

            //parameters.Add("@Type", reportingCriteria.Type);
            //parameters.Add("@ReportFollow", reportingCriteria.ReportFollow);
            //parameters.Add("@Year", reportingCriteria.Year);

            //using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            //{
            //    var employeeReport = dbConnection.Query<Report>("Proc_GetEmployeeReport", param: parameters, commandType: CommandType.StoredProcedure);

            //    return employeeReport.ToList();
            //}

            // Cách 2
            var parameters = new DynamicParameters();

            parameters.Add("@Type", reportingCriteria.Type);
            parameters.Add("@Year", reportingCriteria.Year);
            //parameters.Add("@ReportFollow", reportingCriteria.ReportFollow);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                // Báo cáo theo tháng của năm
                if (reportingCriteria.ReportFollow == MISA.Test.Core.Enum.ReportFollow.MONTH)
                {
                    var employeeReport = dbConnection.Query<Report>("Proc_GetEmployeeReportByMonth", param: parameters, commandType: CommandType.StoredProcedure);

                    return employeeReport.ToList();
                }

                // Báo cáo theo quý của năm
                if (reportingCriteria.ReportFollow == MISA.Test.Core.Enum.ReportFollow.QUARTER)
                {
                    var employeeReport = dbConnection.Query<Report>("Proc_GetEmployeeReportByQuarter", param: parameters, commandType: CommandType.StoredProcedure);

                    return employeeReport.ToList();
                }

                // Báo cáo theo năm
                if (reportingCriteria.ReportFollow == MISA.Test.Core.Enum.ReportFollow.YEAR)
                {
                    var employeeReport = dbConnection.Query<Report>("Proc_GetEmployeeReportByYear", param: parameters, commandType: CommandType.StoredProcedure);

                    return employeeReport.ToList();
                }
            }

            return new List<Report>();
        }

        #endregion

    }
}
