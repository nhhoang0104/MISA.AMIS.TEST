using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Entities
{
    public class ServiceResult
    {
        #region Property

        /// <summary>
        /// 
        /// </summary>
        /// CreatedBy: NHHoang(27/8/2021)
        public bool IsValid { get; set; } = true;

        /// <summary>
        /// thông tin kết quả
        /// </summary>
        /// CreatedBy: NHHoang(27/8/2021)
        public object Data { get; set; }

        /// <summary>
        /// Tin nhắn
        /// </summary>
        /// CreatedBy: NHHoang(27/8/2021)
        public string Messager { get; set; }

        #endregion
    }
}
