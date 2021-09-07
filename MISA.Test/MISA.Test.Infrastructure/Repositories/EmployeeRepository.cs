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
        public EmployeeFilterPaging GetByFilterPaging(string employeeFilter, int pageSize, int pageIndex)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@EmployeeFilter", employeeFilter);
            parameters.Add("@Size", pageSize);
            parameters.Add("@Offset", pageIndex);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var employees = dbConnection.Query<Employee>("Proc_GetEmployeeFilterPaging", param: parameters, commandType: CommandType.StoredProcedure);

                var data = new EmployeeFilterPaging();
                data.Employees = (List<Employee>)employees;
                data.TotalPage = parameters.Get<Int32>("@TotalPage");
                data.TotalRecord = parameters.Get<Int32>("@TotalRecord");
                return data;
            }
        }

        #endregion

    }
}
