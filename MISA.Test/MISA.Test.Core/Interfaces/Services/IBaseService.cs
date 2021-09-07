using MISA.Test.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Interfaces.Services
{
    public interface IBaseService<MISAEntity>
    {
        #region Interface

        /// <summary>
        /// Xử lý nghiệp vụ (validateId) trước khi lấy thông tin nhân viên theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// CreatedBy: NHHoang(27/8/2021)
        ServiceResult GetById(string id);

        /// <summary>
        /// Xử lý nghiệp vụ trước lấy tất cả danh sách bản ghi
        /// </summary>
        /// <returns>
        /// </returns>
        /// CreatedBy: NHHoang(27/8/2021)
        ServiceResult GetAll();

        /// <summary>
        /// Xử lý nghiệp vụ validate các dữ liệu đầu vào trước khi thêm bản ghi mới
        /// </summary>
        /// <param name="entity">thông tin thực thể</param>
        /// <returns></returns>
        /// CreatedBy: NHHoang(27/8/2021)
        ServiceResult Add(MISAEntity entity);

        /// <summary>
        /// Xử lý nghiệp vụ validate các dữ liệu đầu,id vào trước khi cập nhật bản ghi
        /// </summary>
        /// <param name="id">id thực thể</param>
        /// <param name="entity">thông tin thực thể</param>
        /// <returns></returns>
        /// CreatedBy: NHHoang(27/8/2021)
        ServiceResult Update(string id, MISAEntity entity);

        /// <summary>
        ///  Xử lý nghiệp vụ trước xóa các bản ghi(validate Id)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// CreatedBy: NHHoang(27/8/2021)
        ServiceResult DeleteEntites(List<string> entitiesId);

        /// <summary>
        /// Xóa bản ghi theo id(validate Id)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// CreatedBy: NHHoang(27/8/2021)
        ServiceResult DeleteById(string id);

        #endregion

    }
}
