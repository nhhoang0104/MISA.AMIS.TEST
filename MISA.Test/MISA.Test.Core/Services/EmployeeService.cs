using MISA.Test.Core.Entities;
using MISA.Test.Core.Interfaces.Repositories;
using MISA.Test.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeRepository;
        public EmployeeService(IBaseRepository<Employee> baseRepository, IEmployeeRepository employeeResponsitory) : base(baseRepository)
        {
            this._employeeRepository = employeeResponsitory;
        }
        
        /// <summary>
        /// Kiểm tra mã nhân viên đã tồn tại chưa ?
        /// </summary>
        /// <param name="EmployeeCode">mã nhân viên</param>
        /// <returns>
        /// true - tồn tại
        /// false - chưa
        /// </returns>
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
        /// Lấy danh sách nhân viên
        /// </summary>
        /// <param name="employeeFilter">thông tin filter(EmployeeCode hoặc PhoneNumber hoặc FullName)</param>
        /// <param name="departmentId">id phòng ban</param>
        /// <param name="positionId">id vị trí</param>
        /// <param name="pageSize">kích cỡ tragn</param>
        /// <param name="pageIndex">id trang</param>
        /// <returns></returns>
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
    }
}
