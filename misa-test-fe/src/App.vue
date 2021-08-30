<template lang="html">
  <div>
    <the-header :isMinimize="isMinimize" @minimize="minimize"></the-header>
    <the-menu :isMinimize="isMinimize"></the-menu>
    <the-content :isMinimize="isMinimize"></the-content>
  </div>
</template>

<script>
import TheHeader from "@/layout/TheHeader.vue";
import TheMenu from "@/layout/menu/TheMenu.vue";
import TheContent from "@/layout/TheContent.vue";
export default {
  components: {
    "the-header": TheHeader,
    "the-menu": TheMenu,
    "the-content": TheContent,
  },
  data() {
    return {
      isMinimize: false,
    };
  },
  methods: {
    /**
     * Thu nhỏ menu
     * CreateeBy: NHHoang (28/08/2020)
     */
    minimize() {
      this.isMinimize = !this.isMinimize;
    },
  },
};
</script>

<style lang="css">
@import url("./assets/css/common/Main.css");
</style>
