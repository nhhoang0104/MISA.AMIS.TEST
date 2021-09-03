using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Entities
{
    public class EmployeeFilterPaging
    {
        /// <summary>
        /// Danh sách bản ghi
        /// </summary>
        /// CreatedBy: NHHoang (01/09/2021)
        public List<Employee> Employees { get; set; }

        /// <summary>
        /// tổng số trang
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public Int32 TotalPage { get; set; }

        /// <summary>
        /// tổng số bản ghi
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public Int32 TotalRecord { get; set; }
    }
}
