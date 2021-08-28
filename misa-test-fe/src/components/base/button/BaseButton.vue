<template>
  <button
    type="button"
    class="btn"
    :class="[className, circle ? 'btn--circle' : '']"
    @click="$emit('onclick')"
    :disabled="disable"
  >
    <div class="btn__text">Thêm mới nhân viên</div>
  </button>
</template>

<script>
export default {
  name: "base-button",
  emits: ["onclick"],
  props: {
    className: {
      default: "",
    },
    circle: {
      type: Boolean,
      default: false,
    },
    disable: {
      type: Boolean,
      default: false,
    },
  },
};
</script>

<style lang="css">
@import url("../../../assets/css/common/Button.css");
</style>
