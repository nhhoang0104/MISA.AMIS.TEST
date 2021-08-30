<template>
  <div
    class="combo-box__option"
    :value="dataSrc.id"
    :checked="checked"
    @click="selectItem(dataSrc.id)"
  >
    <div class="w-30 p-l--10 p-r--10">{{ dataSrc.code }}</div>
    <div class="w-70 p-l--10 p-r--10">{{ dataSrc.label }}</div>
  </div>
</template>

<script>
export default {
  name: "combo-box-option",
  emits: ["select-item"],
  inject: ["pkey"],
  props: {
    dataSrc: {
      type: Object,
      requied: true,
    },
    checked: {
      type: Boolean,
      requied: false,
      default: true,
    },
  },
  methods: {
    /*
      Xử lý chọn option trong dropdown
    */
    selectItem(value) {
      this.$emit("select-item", { id: this.pkey, value });
    },
  },
};
</script>
