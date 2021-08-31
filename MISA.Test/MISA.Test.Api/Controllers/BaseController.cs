using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Test.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Test.Api.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class BaseController<MISAEntity> : ControllerBase
    {
        IBaseService<MISAEntity> _baseService;
        public BaseController(IBaseService<MISAEntity> baseService)
        {
            this._baseService = baseService;
        }

        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>danh sách barn </returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var serviceResult = this._baseService.GetAll();

                if (serviceResult.IsValid == true)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                else
                {
                    var errObj = new
                    {
                        devMsg = serviceResult.Messager,
                        userMsg = serviceResult.Messager,
                    };

                    return BadRequest(errObj);
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
                };

                return StatusCode(500, errObj);
            }
        }

        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <param name="id">id thực thể</param>
        /// <returns>thông tin thực thể</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var serviceResult = this._baseService.GetById(id);

                if (serviceResult.IsValid == true)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                else
                {
                    var errObj = new
                    {
                        devMsg = serviceResult.Messager,
                        userMsg = serviceResult.Messager,
                    };

                    return BadRequest(errObj);
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
                };

                return StatusCode(500, errObj);
            }
        }

        /// <summary>
        /// Thêm mới 1 bản ghi vào database
        /// </summary>
        /// <param name="entity">thông tin thực thể</param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult Add(MISAEntity entity)
        {
            try
            {
                var serviceResult = this._baseService.Add(entity);

                if (serviceResult.IsValid == true)
                {
                    if ((int)serviceResult.Data > 0)
                    {
                        return StatusCode(201, serviceResult.Data);
                    }
                    else
                    {
                        return StatusCode(204);
                    }

                }
                else
                {
                    var errObj = new
                    {
                        devMsg = serviceResult.Messager,
                        userMsg = serviceResult.Messager,
                    };

                    return BadRequest(errObj);
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
                };

                return StatusCode(500, errObj);
            }
        }

        /// <summary>
        ///Sửa đổi thông tin của đối tượng
        /// </summary>
        /// <param name="id">id thực thể</param>
        /// <param name="entity">thông tin thực thể</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(string id, MISAEntity entity)
        {
            try
            {
                var serviceResult = this._baseService.Update(id, entity);

                if (serviceResult.IsValid == true)
                {
                    return StatusCode(200, serviceResult.Data);
                }
                else
                {
                    var errObj = new
                    {
                        devMsg = serviceResult.Messager,
                        userMsg = serviceResult.Messager,
                    };

                    return BadRequest(errObj);
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
                };

                return StatusCode(500, errObj);
            }
        }

        /// <summary>
        /// Xóa nhiều theo id
        /// </summary>
        /// <param name="entitiesId">danh sách id của entites</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteEntities([FromQuery] List<string> entitiesId)
        {
            try
            {
                var serviceResult = this._baseService.DeleteEntites(entitiesId);

                if (serviceResult.IsValid == true)
                {
                    if ((int)serviceResult.Data > 0)
                    {
                        return StatusCode(200, serviceResult.Data);
                    }
                    else
                    {
                      
                        return StatusCode(204);
                    }
                }
                else
                {
                    var errObj = new
                    {
                        devMsg = serviceResult.Messager,
                        userMsg = serviceResult.Messager,
                    };

                    return BadRequest(errObj);
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
                };

                return StatusCode(500, errObj);
            }
        }

        /// <summary>
        /// Xóa theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteById(string id)
        {
            try
            {
                var serviceResult = this._baseService.DeleteById(id);

                if (serviceResult.IsValid == true)
                {
                    if ((int)serviceResult.Data > 0)
                    {
                        return StatusCode(200, serviceResult.Data);
                    }
                    else
                    {
                        return StatusCode(204);
                    }
                }
                else
                {
                    var errObj = new
                    {
                        devMsg = serviceResult.Messager,
                        userMsg = serviceResult.Messager,
                    };

                    return BadRequest(errObj);
                }
            }
            catch (Exception e)
            {
                var errObj = new
                {
                    devMsg = e.Message,
                    userMsg = "Có lỗi xảy ra! vui lòng liên hệ với MISA.",
                };

                return StatusCode(500, errObj);
            }
        }
    }
}
