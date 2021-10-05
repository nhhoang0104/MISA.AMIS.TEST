<template>
  <div>
    <misa-popup
      :isShowed="visible"
      width="100%"
      :minWidth="700"
      :maxWidth="900"
      height="100%"
      :maxHeight="480"
      :hasTitle="true"
      :scrollEnabled="true"
      :resizeEnabled="true"
      :dragEnabled="true"
      class="chart"
    >
      <template #popup-title>
        <div class="chart__title">
          <div class="title">Biến động nhân sự</div>
          <div class="utils_btn">
            <div
              class="icon icon--24 icon-refresh-2"
              @click="loadDataChart"
            ></div>
            <div
              class="icon icon--24 icon-setting"
              id="box-report"
              @click="clickBox"
            ></div>
            <div class="icon icon--24 icon-close" @click="closeReport"></div>
          </div>
        </div>
      </template>
      <template #popup-content>
        <div class="chart__content">
          <base-chart
            v-if="
              typeChart === EnumTypeChart.Bar ||
                typeChart === EnumTypeChart.Line
            "
            :dataSrc="dataSrc"
            :valueField="valueField"
            argumentField="Label"
            :hasLegend="true"
            :legendNameList="legendNameList"
            :hasTooltip="true"
            :templateToolTip="templateToolTip"
            :type="typeChart"
            :width="700"
            :height="400"
            :color="color"
          ></base-chart>
          <base-pie-chart
            v-else
            :dataSrc="dataSrc"
            :valueField="valueField"
            argumentField="Label"
            :hasLegend="true"
            :legendNameList="legendNameList"
            :hasTooltip="true"
            :templateToolTip="templateToolTip"
            :width="500"
            :height="400"
          >
          </base-pie-chart>
        </div>
      </template>
    </misa-popup>

    <misa-popup
      :isShowed="isShowed"
      :width="400"
      :type="2"
      height="100%"
      :maxHeight="374"
      :hasScroll="true"
      :hasFooter="true"
      :clickOutsideEnable="false"
      :heightFooter="60"
      position="bottom"
      target="#box-report"
    >
      <template #popup-content>
        <div class="box">
          <div class="box__content">
            <div class="box__item">
              <div class="item__title">Báo cáo biến đổi nhân sự</div>
              <base-select-box
                :dataSrc="reportTypeList"
                :value="reportingCriteria.Type"
                displayExpr="label"
                valueExpr="value"
                @select-item="selectTypeReport"
              ></base-select-box>
            </div>
            <div class="box__item">
              <div class="item__title">Loại biểu đồ</div>
              <base-select-box
                :dataSrc="typeChartList"
                :value="typeChart"
                displayExpr="label"
                valueExpr="value"
                @select-item="selectTypeChart"
              ></base-select-box>
            </div>
            <div class="w-80 box__item">
              <div class="item__title">Thống kê theo</div>
              <base-radio
                :dataSrc="dataSrcTime"
                :value="reportingCriteria.ReportFollow"
                displayExpr="label"
                valueExpr="value"
                layout="horizontal"
                @select-item="selectReportFollow"
              ></base-radio>
            </div>
            <div class="w-25 box__item">
              <div class="item__title">Năm</div>
              <base-input
                v-show="reportingCriteria.ReportFollow !== EnumTime.YEAR"
                type="number"
                format="number"
                :min="0"
                @onchangeinput="onChangeYear"
                :value="reportingCriteria.Year"
              ></base-input>
            </div>
          </div>
        </div>
      </template>
      <template #popup-footer>
        <div class="box__footer">
          <base-button
            label="Hủy"
            :secondary="true"
            @click="closeBox"
          ></base-button>
          <base-button label="Lưu" @click="submit"></base-button>
        </div>
      </template>
    </misa-popup>

    <base-loader :isLoading="isLoading"></base-loader>
  </div>
</template>

<script>
import EmployeeAPI from "@/api/components/employeeAPI";
import EnumRadioGroup from "@/constants/EnumRadioGroup";
import EnumSelectBox from "@/constants/EnumSelectBox";
import EnumTime from "@/constants/EnumTime";
import EnumTypeChart from "@/constants/EnumTypeChart";
import Resource from "@/constants/resource";

export default {
  data() {
    return {
      dataSrc: [],
      dataSrcTime: EnumRadioGroup.TypeTime,
      reportTypeList: EnumSelectBox.TypeReport,
      argumentField: null,
      EnumTime,
      Resource,
      EnumTypeChart,
      valueField: null,
      legendNameList: null,
      templateToolTip: null,
      reportingCriteria: {
        Type: 1,
        ReportFollow: 1,
        Year: 2021,
      },
      color: ["#35bf22", "#7098f0", "#fccc77"],
      typeChartList: EnumSelectBox.TypeChart,
      typeChart: EnumTypeChart.Pie,
      typeChartTmp: EnumTypeChart.Bar,
      isShowed: false,
      isLoading: false,
    };
  },

  props: {
    visible: {
      type: Boolean,
      default: false,
    },
  },

  emits: ["close-report"],

  watch: {
    visible(newVal) {
      if (newVal) {
        this.loadDataChart();
      }
    },
  },

  methods: {
    /**
     * Đóng báo cáo
     */
    closeReport() {
      this.closeBox();
      this.$emit("close-report");
    },
    /**
     * Lấy dữ liệu báo cáo biển đổi nhân sự từ api
     * CreatedBy: nhhoang(20/09/2021)
     */
    loadDataChart() {
      this.isLoading = true;
      EmployeeAPI.getHumanReport(this.reportingCriteria)
        .then((res) => {
          this.typeChart = this.typeChartTmp;
          this.dataSrc = res.data;

          this.valueField = this.getValueField(
            this.typeChart,
            this.reportingCriteria.Type
          );

          this.legendNameList = this.getLegendNameList(
            this.typeChart,
            this.reportingCriteria.ReportFollow
          );

          this.templateToolTip = this.getTemplateToolTip(
            this.reportingCriteria.ReportFollow
          );

          this.isLoading = false;
        })
        .catch((e) => {
          console.log(e);
          this.isLoading = false;
        });
    },

    /**
     * Chọn loại báo cáo
     * CreatedBy: nhhoang(20/09/2021)
     */
    selectTypeReport({ value }) {
      this.reportingCriteria.Type = value;
    },

    /**
     * Chọn loại biểu đồ
     * CreatedBy: nhhoang(20/09/2021)
     */
    selectTypeChart({ value }) {
      this.typeChartTmp = value;
    },

    /**
     * Chọn báo cáo theo năm hoặc tháng hoặc quý
     * CreatedBy: nhhoang(20/09/2021)
     */
    selectReportFollow({ value }) {
      this.reportingCriteria.ReportFollow = value;
    },

    /**
     * Thay đổi năm
     * CreatedBy: nhhoang(20/09/2021)
     */
    onChangeYear({ value }) {
      this.reportingCriteria.Year = value;
    },

    /**
     * Lấy danh sách valueField
     * CreatedBy: nhhoang(20/09/2021)
     */
    getValueFieldList(typeChart) {
      let res = [];

      Resource.ChartValueField.forEach((valueField) => {
        if (valueField.key === typeChart) res = valueField.value;
      });

      return res;
    },

    /**
     * Lấy valueField cho biểu đồ
     * CreatedBy: nhhoang(20/09/2021)
     */
    getValueField(typeChart, typeReport) {
      let valueFieldList = this.getValueFieldList(typeChart);
      let res = null;

      valueFieldList.forEach((field) => {
        if (field.key === typeReport) res = field.value;
      });

      return res;
    },

    /**
     * Lấy danh sách chú thích biểu đồ
     * CreatedBy: nhhoang(20/09/2021)
     */
    getLegendNameList(typeChart, typeReportFollow) {
      let res = null;

      Resource.NameLegendList.forEach((legend) => {
        if (typeChart === legend.key) {
          if (typeChart === EnumTypeChart.Pie) {
            switch (typeReportFollow) {
              case EnumTime.MONTH:
                res = legend.value.MONTH;
                break;
              case EnumTime.QUARTER:
                res = legend.value.QUARTER;
                break;
              case EnumTime.YEAR:
                res = legend.value.YEAR;
                break;
            }
          } else res = legend.value;
        }
      });

      return res;
    },

    /**
     * Lấy mẫu tooltip cho biểu đồ
     * @param value: giá trị
     * CreatedBy: nhhoang(20/09/2021)
     */
    getTemplateToolTip(value) {
      let res = "";

      Resource.NameChartToolTip.forEach((tooltip) => {
        if (tooltip.key === value) res = tooltip.value;
      });

      return res;
    },

    /**
     *
     */
    submit() {
      this.closeBox();
      this.loadDataChart();
    },

    /**
     * Click box các tiêu chí báo cáo
     * CreatedBy: nhhoang(20/09/2021)
     */
    clickBox() {
      this.isShowed = !this.isShowed;
    },

    /**
     * Đóng box các tiêu chí báo cáo
     * CreatedBy: nhhoang(20/09/2021)
     */
    closeBox() {
      this.isShowed = false;
    },
  },
};
</script>

<style lang="scss" scoped>
.chart {
  &__title {
    display: flex;
    justify-content: space-between;
    padding: 10px;
    height: 60px;
    box-sizing: border-box;

    .title {
      font-family: "Notosans-Bold";
      font-size: 20px;
    }

    .utils_btn {
      display: flex;
      justify-content: center;
      gap: 10px;
    }
  }

  &__content {
    display: flex;
    justify-content: center;
    padding: 0px 10px;
  }
}

.box {
  padding: 16px 16px 0 16px;
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  box-sizing: border-box;

  &__content {
    width: 100%;

    .box__item {
      margin-bottom: 14px;

      .item__title {
        font-family: "Notosans-Bold";
        font-size: 14px;
        margin-bottom: 5px;
      }
    }
  }
}

.box__footer {
  display: flex;
  align-items: center;
  justify-content: flex-end;
  gap: 20px;
  padding: 0 16px;
  height: 60px;
  width: 100%;
}
</style>
