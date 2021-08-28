<template>
  <table>
    <slot name="thead"></slot>
    <slot name="tbody"></slot>
  </table>
</template>

<script>
export default {
  name: "base-table",
};
</script>

<style lang="css" scoped>
@import url("../../../assets/css/common/Table.css");
</style>
