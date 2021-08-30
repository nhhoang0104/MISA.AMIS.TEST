<template>
  <DxDateBox
    v-model="cloneModel"
    displayFormat="dd/MM/yyyy"
    :height="200"
    placeholder="DD/MM/YYYY"
    :useMaskBehavior="true"
    :width="200"
    :showClearButton="true"
    class="dxDateBox"
  />
</template>

<script>
import DxDateBox from "devextreme-vue/date-box";
export default {
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

<style lang=""></style>
