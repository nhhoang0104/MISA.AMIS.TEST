import BaseAPIConfig from "./baseAPIConfig.js";

export default class BaseAPI {
  constructor() {
    this.controller = null;
  }
  /**
   * Phương thức lấy tất cả dữ liệu
   * CreatedBy: NHHoang (28/08/2021)
   */
  getAll() {
    return BaseAPIConfig.get(`${this.controller}`);
  }

  /**
   * Phương thức thêm mới nhân với
   * @param {*} body
   * CreatedBy: NHHoang (28/08/2021)
   */
  add(body) {
    return BaseAPIConfig.post(`${this.controller}`, body);
  }

  /**
   * Phương thức lấy dữ liệu  theo id
   * @param {*} id
   * CreatedBy: NHHoang (28/08/2021)
   */

  getById(id) {
    return BaseAPIConfig.get(`${this.controller}/${id}`);
  }

  /**
   * Hàm lấy dữ liệu phân trang
   * @param {*} payload
   * CreatedBy: NHHoang (28/08/2021)
   */
  paging(payload) {
    return BaseAPIConfig.post(`${this.controller}/paging`, payload);
  }

  /**
   * Hàm cập nhật dữ liệu
   * @param {*} id
   * @param {*} body
   * CreatedBy: NHHoang (28/08/2021)
   */
  update(id, body) {
    return BaseAPIConfig.put(`${this.controller}/${id}`, body);
  }

  /**
   * Hàm xóa bản ghi
   * @param {*} id
   * CreatedBy: NHHoang (28/08/2021)
   */
  delete(id) {
    return BaseAPIConfig.delete(`${this.controller}/${id}`);
  }

  /**
   * xóa nhiều
   * @param {*} idList danh sách id
   * CreatedBy: NHHoang (28/08/2021)
   */
  deleteList(idList) {
    let tmp = "";
    idList.forEach((id) => {
      tmp += `entitiesId=${id}&`;
    });
    return BaseAPIConfig.delete(`${this.controller}?${tmp}`);
  }
}
