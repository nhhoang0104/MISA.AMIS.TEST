using MISA.Test.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.Test.Core.Interfaces.Services
{
    public interface IEmployeeService:IBaseService<Employee>
    {
        #region Interface

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
        /// Modified: NHHoang (01/09/2021)
        ServiceResult GetByFilterPaging(string employeeFilter, Int32 pageSize, Int32 pageIndex);

        /// <summary>
        /// Kiểm tra mã nhân viên đã tồn tại chưa ?
        /// </summary>
        /// <param name="EmployeeCode">mã nhân viên</param>
        /// <returns>
        /// true - tồn tại
        /// false - chưa
        /// CreatedBy: NHHoang (27/8/2021)
        /// </returns>
        ServiceResult CheckEmployeeCodeExists(string EmployeeCode);

        /// <summary>
        /// Kiểm tra nhân viên tồn tại không  ?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceResult CheckEmployeeExists(string id);

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NHHoang (27/8/2021)
        ServiceResult GetNewEmployeeCode();

        /// <summary>
        /// xuất dữ liệu
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: NHHoang (01/09/2021)
        MemoryStream Export(CancellationToken cancellationToken, string employeeFilter, int pageSize, int pageIndex);

        #endregion
    }
}
