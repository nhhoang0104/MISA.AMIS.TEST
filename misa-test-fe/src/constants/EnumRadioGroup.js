import EnumTime from "./EnumTime";
var EnumRadioGroup = EnumRadioGroup || {};

EnumRadioGroup.TypeTime = [
  {
    label: "Tháng",
    id: "MONTH",
    value: EnumTime.MONTH,
  },
  {
    label: "Quý",
    id: "QUARTER",
    value: EnumTime.QUARTER,
  },
  {
    label: "Năm",
    id: "YEAR",
    value: EnumTime.YEAR,
  },
];

export default EnumRadioGroup;
