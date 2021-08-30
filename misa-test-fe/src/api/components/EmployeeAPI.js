import BaseAPI from "@/api/base/BaseAPI.js";
import BaseAPIConfig from "../base/BaseAPIConfig.js";

class EmployeeAPI extends BaseAPI {
  constructor() {
    super();
    this.controller = "Employees";
  }

  getNewEmployeeCode() {
    return BaseAPIConfig.get(`${this.controller}/NewEmployeeCode`);
  }

  getByFilterPaging(pageIndex, pageSize, employeeFilter = "") {
    return BaseAPIConfig.get(
      `${this.controller}/Filter?pageIndex=${pageIndex}&pageSize=${pageSize}&employeeFilter=${employeeFilter}`
    );
  }

  export(pageIndex, pageSize, employeeFilter = "") {
    return BaseAPIConfig.get(
      `${this.controller}/Export?pageIndex=${pageIndex}&pageSize=${pageSize}&employeeFilter=${employeeFilter}`,
      {
        responseType: "blob",
      }
    );
  }
}

export default new EmployeeAPI();
