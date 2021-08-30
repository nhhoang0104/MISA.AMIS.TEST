export default class Validation {
  /**
   * validate
   * @param {*} type loại value
   * @param {*} value giá trị
   * @returns true - pass, false - failed
   * CreatedBy: NHHoang (28/08/2021)
   */
  static validate(type, value) {
    if (type === "email") {
      return Validation.validateEmail(value);
    }

    return Validation.validateCommon(value);
  }

  /**
   *
   * @param {*} value  validate các trường bắt buộc
   * @returns true - pass, false - failed
   * CreatedBy: NHHoang (28/08/2021)
   */
  static validateCommon(value) {
    return value !== "" && value !== null && value !== undefined;
  }

  /**
   *validate email
   * @param {*} value  giá trị<e<email></email>l></email>l></email>l></email>l></email></email>
   * @returns true - pass, false - failed
   * CreatedBy: NHHoang (28/08/2021)
   */
  static validateEmail(value) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(value).toLowerCase());
  }

  /**
   * validate số
   * @param {*} value  giá trị
   * @returns true - pass, false - failed
   * CreatedBy: NHHoang (28/08/2021)
   */
  static validateNumber(value) {
    const re = /^\d*$/;
    return re.test(String(value).toLowerCase());
  }
}
