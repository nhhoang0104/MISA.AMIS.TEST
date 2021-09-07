<template>
  <button
    type="button"
    class="btn"
    :class="secondary ? 'btn-secondary' : ''"
    @click="$emit('onclick')"
    :disabled="disable"
  >
    <div class="btn__text">{{ label }}</div>
  </button>
</template>

<script>
export default {
  name: "base-button",
  emits: ["onclick"],
  props: {
    label: {
      type: String,
    },
    disable: {
      type: Boolean,
      default: false,
    },
    secondary: {
      type: Boolean,
      default: false,
    },
  },
};
</script>

<style lang="css">
@import url("../../../assets/css/common/button.css");
</style>
