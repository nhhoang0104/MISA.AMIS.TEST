<template>
  <div class="misa-chart">
    <DxChart
      :data-source="dataSrc"
      theme="generic.light"
      :customize-label="customizeLabel"
      palette="Soft"
    >
      <DxSize :width="width" :height="height"></DxSize>
      <DxCommonSeriesSettings :type="type" />
      <DxLoadingIndicator :enabled="true" background-color="#fff">
        <DxFont family="Notosans" />
      </DxLoadingIndicator>

      <DxArgumentAxis :tick-interval="1">
        <DxLabel format="decimal" :font="{ family: 'Notosans' }" />
      </DxArgumentAxis>

      <DxValueAxis :max-value-margin="0.05">
        <DxTitle text="Số lượng">
          <DxFont family="Notosans" color="#111" />
        </DxTitle>
        <DxLabel :font="{ family: 'Notosans' }" />
      </DxValueAxis>

      <DxSeries
        v-for="(arg, index) in valueField"
        :key="index"
        :value-field="arg"
        :name="arg"
        :color="color[index]"
        :argument-field="argumentField"
      />

      <DxLegend :visible="hasLegend" :customize-items="customizeLegendItems">
        <DxFont family="Notosans" color="#111" />
      </DxLegend>

      <DxTooltip
        :enabled="hasTooltip"
        :customize-tooltip="customizeTooltip"
        :opacity="0.5"
        :z-index="9999"
        :font="{ size: 16 }"
      />
    </DxChart>
  </div>
</template>

<script>
import DxChart, {
  DxLegend,
  DxSeries,
  DxTooltip,
  DxArgumentAxis,
  DxValueAxis,
  DxLabel,
  DxTitle,
  DxFont,
  DxSize,
  DxCommonSeriesSettings,
  DxLoadingIndicator,
} from "devextreme-vue/chart";

export default {
  name: "base-chart",
  components: {
    DxFont,
    DxTitle,
    DxChart,
    DxArgumentAxis,
    DxLabel,
    DxValueAxis,
    DxLegend,
    DxSeries,
    DxTooltip,
    DxSize,
    DxCommonSeriesSettings,
    DxLoadingIndicator,
  },

  props: {
    dataSrc: {
      type: Array,
      required: true,
    },
    argumentField: {
      type: [Array, String],
      default: null,
    },
    valueField: {
      type: [Array, String],
      default: null,
    },
    hasLegend: {
      type: Boolean,
      default: false,
    },
    legendNameList: {
      type: Object,
      default: null,
    },
    templateToolTip: {
      type: String,
      default: "",
    },
    hasTooltip: {
      type: Boolean,
      default: false,
    },
    width: {
      type: Number,
      default: null,
    },
    height: {
      type: Number,
      default: null,
    },
    type: {
      type: String,
      default: "bar",
    },
    color: {
      type: [Array, String],
      default: null,
    },
  },

  data() {
    return {};
  },

  methods: {
    /**
     * custom tooltip
     * nhhoang (20/09/2021)
     */
    customizeTooltip(payload) {
      let text = "";

      text = this.templateToolTip.format(payload.argument, payload.valueText);

      return {
        text,
        fontColor: "#fff",
        color: "#111",
        borderColor: "#fff",
      };
    },

    /**
     * custom nhẫn trên cột
     * nhhoang (20/09/2021)
     */
    customizeLabel() {
      return {
        visible: true,
        backgroundColor: "none",
        font: { color: "#111" },
        customizeText: function({ valueText }) {
          return `${valueText}`;
        },
      };
    },

    /**
     * custome chú thích của biểu đồ
     * nhhoang (20/09/2021)
     */
    customizeLegendItems(items) {
      if (this.legendNameList !== null)
        return items.map((item) => {
          item.text = this.legendNameList[item.text];
          return item;
        });
    },
  },
};
</script>

<style lang="scss"></style>
