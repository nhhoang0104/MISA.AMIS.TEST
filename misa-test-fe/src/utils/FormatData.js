import moment from "moment";

export default class FormatData {
  /*
    định dạng ngày tháng năm để render lên table
  */

  static formatDate(date) {
    if (!date) return "";

    return moment(new Date(date)).format("DD/MM/YYYY");
  }

  /*
    định dạng năm tháng ngáy để post api
  */

  static formatDateInput(date) {
    if (!date) return null;

    return moment(new Date(date)).format("YYYY-MM-DD");
  }
}
