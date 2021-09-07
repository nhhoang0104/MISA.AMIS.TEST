<template lang="html">
  <div class="menu scroller" :class="isMinimize ? 'minimize' : ''">
    <menu-item
      v-for="item in listMenuItem"
      :key="item.id"
      :item="item"
    ></menu-item>
  </div>
</template>

<script>
import MenuItem from "./MenuItem.vue";
import { ListMenuItem } from "@/constants/listMenuItem.js";

export default {
  name: "the-menu",
  props: {
    isMinimize: {
      type: Boolean,
      required: true,
    },
  },
  components: {
    MenuItem,
  },
  data() {
    return { listMenuItem: ListMenuItem };
  },
};
</script>

<style lang="css" scoped>
@import url("../../assets/css/layout/menu.css");
</style>
