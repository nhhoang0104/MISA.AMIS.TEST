<template>
  <DxDateBox
    v-model="cloneModel"
    displayFormat="dd/MM/yyyy"
    :height="32"
    placeholder="DD/MM/YYYY"
    :useMaskBehavior="true"
    :width="200"
    :showClearButton="false"
    class="dxDateBox"
    :max="new Date()"
  />
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
  data() {
    return {
      cloneModel: "",
    };
  },
};
</script>

<style lang="css">
@import url("../../assets/css/common/DateBox.css");
</style>
