using MISA.Test.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository:IBaseRepository<Employee>
    {
        /// <summary>
        /// Lấy danh sách sách nhân viên theo bộ lọc( tên nhân viên, mã nhân viên, số điện thoại) và phân trang
        /// </summary>
        /// <param name="employeeFilter">thông tin bộ lọc</param>
        /// <param name="pageSize">vị trí trang</param>
        /// <param name="pageIndex">kích cỡ của trang</param>
        /// <returns>
        /// danh sách nhân viên, tổng số trang, tổng số bản ghi
        /// </returns>
        /// CreatedBy: NHHoang (27/8/2021)
        EmployeeFilterPaging GetByFilterPaging(string employeeFilter, Int32 pageSize, Int32 pageIndex);

        /// <summary>
        /// Kiểm tra mã nhiên viên đã có trong hệ thống chưa 
        /// </summary>
        /// <param name="employeeCode">mã nhân viên</param>
        /// <returns>
        /// true - tồn tại
        /// false - chưa tồn tại
        /// </returns>
        /// CreatedBy: NHHoang (27/8/2021)
        bool CheckEmployeeCodeExists(string employeeCode);

        /// <summary>
        /// Lấy danh sach mã nhân viên
        /// </summary>
        /// <returns>
        /// Danh sách mã nhân viên
        /// </returns>
        /// CreatedBy: NHHoang (27/8/2021)
        List<string> GetAllEmployeeCode();

        /// <summary>
        /// Lấy mẫ nhân viên lớn nhát
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// CreatedBy: NHHoang (27/8/2021)
        String GetBigEmployeeCode();
    }
}
