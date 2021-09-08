import BaseAPI from "@/api/base/baseAPI.js";

class DepartmentAPI extends BaseAPI {
  constructor() {
    super();
    this.controller = "Departments";
  }
}

export default new DepartmentAPI();
