using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Test.Core.Interfaces.Repositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Test.Infrastructure.Repositories
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity>
    {
        #region Field
        private readonly string _modelName;

        protected readonly string _connectionString;
        #endregion

        #region Method

        public BaseRepository(IConfiguration configuration)
        {
            this._modelName = typeof(MISAEntity).Name;
            this._connectionString = configuration.GetConnectionString("sqlConnection");
        }

        /// <summary>
        /// Thêm mới bản ghi vào database
        /// </summary>
        /// <param name="entity">thông tin thực thể</param>
        /// <returns>
        /// 1 - thêm thành công
        /// </returns>
        /// CreatedBy: NHHoang (27/8/2021)
        public int Add(MISAEntity entity)
        {
            // Khởi tạo dynamic parameters
            var parameters = new DynamicParameters();

            //Đọc từng properties của class MISAEntity:
            var props = entity.GetType().GetProperties();

            //Duyệt từng properties:
            foreach (var prop in props)
            {
                //Lấy tên của prop:
                var propName = prop.Name;

                //Lấy giá trị của prop trong đối tượng:
                var propValue = prop.GetValue(entity);

                //Thêm param tương ứng với mỗi prop của đối tượng:
                parameters.Add($"@{propName}", propValue);
            }

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var rowEffect = dbConnection.Execute($"Proc_Insert{this._modelName}", param: parameters, commandType: CommandType.StoredProcedure);

                return rowEffect;
            }
        }

        /// <summary>
        /// Xóa 1 bản ghi theo id trong database
        /// </summary>
        /// <param name="id">id thực thể</param>
        /// <returns>
        /// 1 - xóa thành công
        /// </returns>
        /// CreatedBy: NHHoang (27/8/2021)
        public int DeleteById(Guid id)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add($"@{this._modelName}Id", id);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var rowEffect = dbConnection.Execute($"Proc_Delete{this._modelName}ById", param: param, commandType: CommandType.StoredProcedure);

                return rowEffect;
            }
        }

        /// <summary>
        /// Xóa nhiều bản ghi theo id trong database
        /// </summary>
        /// <param name="entitiesId">danh sach id</param>
        /// <returns>
        /// Chiều dài bản ghi - xóa thành công
        /// </returns>
        /// CreatedBy: NHHoang (27/8/2021)
        public int DeleteEntities(List<Guid> entitiesId)
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var idList = string.Empty;
                foreach (var id in entitiesId)
                {
                    idList += $"'{id.ToString()}',";
                }

                idList = idList.Remove(idList.Length - 1);

                var sqlCmd = $"DELETE FROM {this._modelName} WHERE {this._modelName}Id IN ({idList})";

                var rowEffect = dbConnection.Execute(sqlCmd);

                return rowEffect;
            }
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>
        /// Danh sách bản ghi
        /// </returns>
        /// CreatedBy: NHHoang (27/8/2021)
        public List<MISAEntity> GetAll()
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var entities = dbConnection.Query<MISAEntity>($"Proc_Get{this._modelName}s", commandType: CommandType.StoredProcedure);

                return (List<MISAEntity>)entities;
            }
        }

        /// <summary>
        /// Lấy thông tin bản ghi theo id từ database
        /// </summary>
        /// <param name="id">id entity</param>
        /// <returns></returns>
        /// CreatedBy: NHHoang (27/8/2021)
        public MISAEntity GetById(Guid id)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add($"@{this._modelName}Id", id);

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var entity = dbConnection.QueryFirstOrDefault<MISAEntity>($"Proc_Get{this._modelName}ById", param: param, commandType: CommandType.StoredProcedure);

                return entity;
            }
        }

        /// <summary>
        /// Cập nhật bản ghi theo id
        /// </summary>
        /// <param name="id">id thực thể</param>
        /// <param name="entity">thông tin bản ghi</param>
        /// <returns>
        /// 1 - thêm thành công
        /// </returns>
        /// CreatedBy: NHHoang (27/8/2021)
        public int Update(Guid id, MISAEntity entity)
        {
            var parameters = new DynamicParameters();

            //Đọc từng properties:
            var props = entity.GetType().GetProperties();

            //Duyệt từng properties:
            foreach (var prop in props)
            {
                //Lấy tên của prop:
                var propName = prop.Name;

                //Lấy giá trị của prop trong đối tượng:
                var propValue = prop.GetValue(entity);

                //Lấy kiểu dữ liệu của prop:
                var propType = prop.GetType();

                //Thêm param tương ứng với mỗi prop của đối tượng
                if (propName == $"{this._modelName}Id")
                {
                    parameters.Add($"@{propName}", id);
                }
                else
                {
                    parameters.Add($"@{propName}", propValue);
                }

            }

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var rowEffect = dbConnection.Execute($"Proc_Update{this._modelName}", param: parameters, commandType: CommandType.StoredProcedure);

                return rowEffect;
            }
        }

        #endregion
    }
}
