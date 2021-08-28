<template lang="html">
  <div class="menu-item">
    <div class="menu-item__icon">
      <div class="icon icon--24" :class="item.iconName"></div>
    </div>
    <div class="menu-item__label">{{ item.label }}</div>
  </div>
</template>

<script>
export default {
  name: "menu-item",
  props: {
    item: {
      type: Object,
      required: true,
    },
  },
};
</script>

<style lang="css"></style>
