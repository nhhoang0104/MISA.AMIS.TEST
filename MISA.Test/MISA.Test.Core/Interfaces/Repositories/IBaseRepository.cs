using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Core.Interfaces.Repositories
{
    public interface IBaseRepository<MISAEntity>
    {
        /// <summary>
        /// Lấy tất cả bản ghi từ database
        /// </summary>
        /// <returns>
        /// Danh sách bản ghi
        /// </returns>
        /// CreatedBy: NHHoang (27/8/2021)
        List<MISAEntity> GetAll();

        /// <summary>
        /// Lấy thông tin bản ghi theo id từ database
        /// </summary>
        /// <param name="id">id entity</param>
        /// <returns></returns>
        /// CreatedBy: NHHoang (27/8/2021)
        MISAEntity GetById(Guid id);

        /// <summary>
        /// Thêm mới bản ghi vào database
        /// </summary>
        /// <param name="entity">thông tin thực thể</param>
        /// <returns>
        /// 1 - thêm thành công
        /// </returns>
        /// CreatedBy: NHHoang (27/8/2021)
        Int32 Add(MISAEntity entity);

        /// <summary>
        /// Cập nhật bản ghi theo id trong database
        /// </summary>
        /// <param name="id">id thực thể</param>
        /// <param name="entity">thông tin bản ghi</param>
        /// <returns>
        /// 1 - thêm thành công
        /// </returns>
        /// CreatedBy: NHHoang (27/8/2021)
        Int32 Update(Guid id, MISAEntity entity);

        /// <summary>
        /// Xóa 1 bản ghi theo id trong database
        /// </summary>
        /// <param name="id">id thực thể</param>
        /// <returns>
        /// 1 - xóa thành công
        /// </returns>
        /// CreatedBy: NHHoang (27/8/2021)
        Int32 DeleteById(Guid id);

        /// <summary>
        /// Xóa nhiều bản ghi theo id trong database
        /// </summary>
        /// <param name="entitiesId">danh sach id</param>
        /// <returns>
        /// Chiều dài bản ghi - xóa thành công
        /// </returns>
        /// CreatedBy: NHHoang (27/8/2021)
        Int32 DeleteEntities(List<Guid> entitiesId);
    }
}
