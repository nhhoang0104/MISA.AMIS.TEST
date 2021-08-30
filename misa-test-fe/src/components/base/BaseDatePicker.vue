<template>
  <div>
    <div class="flex" v-show="hasLabel">
      <div class="label">{{ label }}</div>
      <div class="label-required" v-show="required">&nbsp;*</div>
    </div>
    <DxDateBox
      v-model="valueClone"
      displayFormat="dd/MM/yyyy"
      :height="height"
      placeholder="DD/MM/YYYY"
      :useMaskBehavior="true"
      :width="width"
      :showClearButton="false"
      class="dxDateBox"
      :max="new Date()"
    />
  </div>
</template>

<script>
import DxDateBox from "devextreme-vue/date-box";
import { locale } from "devextreme/localization";
locale("vi-VN");

export default {
  name: "base-date-picker",
  components: {
    DxDateBox,
  },
  props: {},
  data() {
    return {
      valueClone: "",
    };
  },
};
</script>

<style lang="css">
@import url("../../assets/css/common/DateBox.css");
</style>
