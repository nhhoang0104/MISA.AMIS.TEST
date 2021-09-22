using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Entities
{
    public class Report
    {
        /// <summary>
        /// Nhãn
        /// </summary>
        /// CreatedBy: nhhoang (20/09/2021)
        public string Label { get; set; }

        /// <summary>
        ///  Số lượng biến đổi
        /// </summary>
        /// CreatedBy: nhhoang (20/09/2021)
        public Int32 Quantity { get; set; } = 0;

        /// <summary>
        /// Số lượng biến đổi thêm
        /// </summary>
        /// CreatedBy: nhhoang (20/09/2021)
        public Int32 QuantityAdd { get; set; } = 0;

        /// <summary>
        /// Số lượng biến đổi sửa
        /// </summary>
        /// CreatedBy: nhhoang (20/09/2021)
        public Int32 QuantityUpdate { get; set; } = 0;

        /// <summary>
        /// Số lượng biến đổi xóa
        /// </summary>
        /// CreatedBy: nhhoang (20/09/2021)
        public Int32 QuantityDelete { get; set; } = 0;
    }
}
