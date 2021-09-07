<template lang="html">
  <div class="content" :class="isMinimize ? 'minimize' : ''">
    <router-view></router-view>
  </div>
</template>

<script>
export default {
  name: "the-content",
  props: {
    isMinimize: {
      type: Boolean,
      required: true,
    },
  },
};
</script>

<style lang="css">
@import url("../assets/css/layout/content.css");
</style>
