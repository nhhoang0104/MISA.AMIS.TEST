<template>
  <DxPieChart
    id="pie"
    :data-source="dataSrc"
    type="doughnut"
    palette="Soft Pastel"
    class="misa-pie-chart"
  >
    <DxSize :width="width" :height="height"></DxSize>
    <DxLoadingIndicator :enabled="true" background-color="#fff">
      <DxFont family="Notosans" />
    </DxLoadingIndicator>
    <DxSeries :argument-field="argumentField" :value-field="valueField">
    </DxSeries>

    <DxTooltip
      :enabled="hasTooltip"
      :customize-tooltip="customizeTooltip"
      :opacity="0.5"
      :z-index="9999"
      :font="{ size: 16 }"
    />

    <DxLegend
      :visible="hasLegend"
      :customize-items="customizeLegendItems"
      :marker-size="10"
      :margin="0"
    >
      <DxFont family="Notosans" color="#111" :size="13" />
    </DxLegend>
  </DxPieChart>
</template>

<script>
import DxPieChart, {
  DxLegend,
  DxSeries,
  DxFont,
  DxLoadingIndicator,
  DxTooltip,
  DxSize,
} from "devextreme-vue/pie-chart";

export default {
  name: "base-pie-chart",

  components: {
    DxPieChart,
    DxLegend,
    DxSeries,
    DxTooltip,
    DxSize,
    DxFont,
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
      type: [Object, String],
      default: null,
    },
    hasTooltip: {
      type: Boolean,
      default: false,
    },
    templateToolTip: {
      type: String,
      default: "",
    },
    width: {
      type: Number,
      default: null,
    },
    height: {
      type: Number,
      default: null,
    },
  },

  methods: {
    customizeTooltip(payload) {
      return {
        text: this.templateToolTip.format(payload.argument, payload.valueText),
        fontColor: "#fff",
        color: "#111",
        borderColor: "#fff",
      };
    },

    customizeLegendItems(items) {
      if (this.legendNameList !== null)
        return items.map((item) => {
          item.text = this.legendNameList.format(item.text);
          return item;
        });
    },
  },
};
</script>

<style lang="scss"></style>
