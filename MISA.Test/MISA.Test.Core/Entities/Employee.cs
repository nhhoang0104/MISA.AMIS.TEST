using MISA.Test.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Entities
{
    public class Employee
    {
        #region Property

        /// <summary>
        /// Khóa chính
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public Guid EmployeeId { get; set; }

        [MISAPropExport]
        [Description("Mã nhân viên")]
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string EmployeeCode { get; set; }

        [MISAPropExport]
        [Description("Họ và tên")]
        /// <summary>
        /// Ho ten day du
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string FullName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string Address { get; set; }

        /// <summary>
        /// Id giới tính
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public int? Gender { get; set; }

        [MISAPropExport]
        [Description("Giới tính")]
        /// <summary>
        /// Tên giới tính
        /// 0 - Nữ
        /// 1 - Name
        /// 2 - Khác
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string GenderName { get; set; }

        [MISAPropExport]
        [Description("Ngày sinh")]
        /// <summary>
        /// Ngày sinh
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string MobilePhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// CMTND hoặc CCCD
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string IdentityNumber { get; set; }

        /// <summary>
        /// Ngày làm CMDTND hoắc CCCD
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp CMTND hoắc CCCD
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string IdentityPlace { get; set; }

        [MISAPropExport]
        [Description("Chức danh")]
        /// <summary>
        /// Tên vị tri
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string PositionName { get; set; }

        /// <summary>
        /// Id phòng ban
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public Guid? DepartmentId { get; set; }

        [MISAPropExport]
        [Description("Tên đơn vị")]
        /// <summary>
        /// Tên phòng ban, đơn vị
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string DepartmentName { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string DepartmentCode { get; set; }

        [MISAPropExport]
        [Description("Số tài khoản")]
        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string BankAccount { get; set; }

        [MISAPropExport]
        [Description("Tên ngân hàng")]
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string BankName { get; set; }

        /// <summary>
        /// Tên chi nhánh ngân hàng
        /// </summary>
        /// CreatedBy: NHHoang (27/8/2021)
        public string BankBranch { get; set; }

        #endregion
    }
}
