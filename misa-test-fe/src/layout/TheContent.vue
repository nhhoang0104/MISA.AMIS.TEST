<template lang="html">
  <div class="content" :class="isMinimize ? 'minimize' : ''">
    <EmployeeList />
  </div>
</template>

<script>
import EmployeeList from "@/views/employee/EmployeeList.vue";

export default {
  name: "the-content",
  components: { EmployeeList },
  props: {
    isMinimize: {
      type: Boolean,
      required: true,
    },
  },
};
</script>

<style lang="css">
@import url("../assets/css/layout/Content.css");
</style>
