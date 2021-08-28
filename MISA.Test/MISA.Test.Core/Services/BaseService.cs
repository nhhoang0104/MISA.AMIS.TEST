using MISA.Test.Core.Attributes;
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
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        IBaseRepository<MISAEntity> _baseRepository;
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            this._baseRepository = baseRepository;
        }


        /// <summary>
        /// Xử lý nghiệp vụ validate các dữ liệu đầu vào trước khi thêm bản ghi mới
        /// </summary>
        /// <param name="entity">thông tin thực thể</param>
        /// <returns></returns>
        /// CreatedBy: NHHoang(27/8/2021)
        public ServiceResult Add(MISAEntity entity)
        {
            var serviceResult = new ServiceResult();

            if (!this.ValidatePropRequired(entity))
            {
                serviceResult.IsValid = false;
                serviceResult.Messager = Resources.ErrorMsg.PropRequired_ErrorMsg;
                return serviceResult;
            }

            serviceResult.Data = this._baseRepository.Add(entity);

            return serviceResult;
        }

        /// <summary>
        /// Xóa bản ghi theo id(validate Id)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// CreatedBy: NHHoang(27/8/2021)
        public ServiceResult DeleteById(string id)
        {
            var serviceResult = new ServiceResult();

            // Validate id
            if (!this.ValidateId(id))
            {
                serviceResult.IsValid = false;
                serviceResult.Messager = Resources.ErrorMsg.IDInvalid_ErrorMsg;
                return serviceResult;
            }

            serviceResult.Data = this._baseRepository.DeleteById(Guid.Parse(id));

            return serviceResult;
        }

        /// <summary>
        ///  Xử lý nghiệp vụ trước xóa các bản ghi(validate Id)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// CreatedBy: NHHoang(27/8/2021)
        public ServiceResult DeleteEntites(List<string> entitiesId)
        {
            var serviceResult = new ServiceResult();

            var tmp = new List<Guid>();
            foreach (var id in entitiesId)
            {
                // Validate id
                if (!this.ValidateId(id))
                {
                    serviceResult.IsValid = false;
                    serviceResult.Messager = Resources.ErrorMsg.IDInvalid_ErrorMsg;
                    return serviceResult;
                }

                tmp.Add(Guid.Parse(id));
            }

            serviceResult.Data = this._baseRepository.DeleteEntities(tmp);

            return serviceResult;
        }

        /// <summary>
        /// Xử lý nghiệp vụ trước lấy tất cả danh sách bản ghi
        /// </summary>
        /// <returns>
        /// </returns>
        /// CreatedBy: NHHoang(27/8/2021)
        public ServiceResult GetAll()
        {
            var serviceResult = new ServiceResult();

            serviceResult.Data = this._baseRepository.GetAll();

            return serviceResult;
        }

        /// <summary>
        /// Xử lý nghiệp vụ (validateId) trước khi lấy thông tin nhân viên theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// CreatedBy: NHHoang(27/8/2021)
        public ServiceResult GetById(string id)
        {
            var serviceResult = new ServiceResult();

            // Validate id
            if (!this.ValidateId(id))
            {
                serviceResult.IsValid = false;
                serviceResult.Messager = Resources.ErrorMsg.IDInvalid_ErrorMsg;
                return serviceResult;
            }

            serviceResult.Data = this._baseRepository.GetById(Guid.Parse(id));

            return serviceResult;
        }

        /// <summary>
        /// Xử lý nghiệp vụ validate các dữ liệu đầu,id vào trước khi cập nhật bản ghi
        /// </summary>
        /// <param name="id">id thực thể</param>
        /// <param name="entity">thông tin thực thể</param>
        /// <returns></returns>
        /// CreatedBy: NHHoang(27/8/2021)
        public ServiceResult Update(string id, MISAEntity entity)
        {
            var serviceResult = new ServiceResult();

            // Validate id
            if (!this.ValidateId(id))
            {
                serviceResult.IsValid = false;
                serviceResult.Messager = Resources.ErrorMsg.IDInvalid_ErrorMsg;
                return serviceResult;
            }

            //validate các trường bắt buộc
            if (!this.ValidatePropRequired(entity))
            {
                serviceResult.IsValid = false;
                serviceResult.Messager = Resources.ErrorMsg.PropRequired_ErrorMsg;
                return serviceResult;
            }

            serviceResult.Data = this._baseRepository.Update(Guid.Parse(id), entity);

            return serviceResult;
        }

        /// <summary>
        /// Validate id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>
        /// true - id hợp lệ
        /// false - id không hợp lệ
        /// </returns>
        private bool ValidateId(string id)
        {
            Guid tmp;

            return Guid.TryParse(id.ToString(), out tmp);
        }

        /// <summary>
        /// Validate các trường bắt buộc đã được điền đủ chưa?
        /// </summary>
        /// <param name="entity">thông tin thực thể</param>
        /// <returns>
        /// true - các trường đã được điền đầy đủ
        /// false - các trường bắt buộc chưa điền đầy đủ
        /// </returns>
        private bool ValidatePropRequired(MISAEntity entity)
        {
            var isValid = true;

            var properties = typeof(MISAEntity).GetProperties();

            foreach (var prop in properties)
            {
                var propVal = prop.GetValue(entity);
                var propName = prop.Name;
                var propMISARequired = prop.GetCustomAttributes(typeof(MISARequired), true);

                if (propMISARequired.Length > 0)
                {
                    if (prop.PropertyType == typeof(string) && (propName == null || propVal.ToString() == String.Empty))
                    {
                        isValid = false;
                    }
                }

            }

            return isValid;
        }
    }
}
