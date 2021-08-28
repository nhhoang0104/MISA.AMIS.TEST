using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Entities
{
    public class BaseEnitiy
    {
        #region Property

        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string ModifiedBy { get; set; }

        #endregion
    }
}
