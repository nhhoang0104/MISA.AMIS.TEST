<template lang="html">
  <EmployeeList />
</template>

<script>
import EmployeeList from "@/views/employee/EmployeeList.vue";

export default {
  name: "the-content",
  components: { EmployeeList },
};
</script>

<style lang="css">
@import url("../assets/css/layout/Content.css");
</style>
