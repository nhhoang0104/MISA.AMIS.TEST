import EnumTypeChart from "./EnumTypeChart";
import EnumTypeReport from "./EnumTypeReport";

var EnumSelectBox = EnumSelectBox || {};

EnumSelectBox.TypeReport = [
  {
    label: "Thêm",
    id: "ADD",
    value: EnumTypeReport.ADD,
  },
  {
    label: "Sửa",
    id: "UPDATE",
    value: EnumTypeReport.UPDATE,
  },
  {
    label: "Xóa",
    id: "DELETE",
    value: EnumTypeReport.DELETE,
  },
  { label: "Biến đổi", id: "MUTATE", value: EnumTypeReport.MUTATE },
];

EnumSelectBox.TypeChart = [
  {
    label: "Biểu đồ cột",
    value: EnumTypeChart.Bar,
  },
  {
    label: "Biểu đồ hình tròn",
    value: EnumTypeChart.Pie,
  },
  {
    label: "Biểu đồ đường",
    value: EnumTypeChart.Line,
  },
];

export default EnumSelectBox;
