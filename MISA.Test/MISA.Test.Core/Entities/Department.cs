using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Entities
{
    public class Department : BaseEnitiy
    {
        #region Property

        /// <summary>
        /// Khóa chính
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mô tả về phòng ban
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string Description { get; set; }

        #endregion
    }
}
