<template>
  <tbody>
    <tr-cus
      :ref="item.EmployeeId"
      v-for="item in data"
      :key="item.EmployeeId"
      :columns="columns"
      :data="item"
      :checked="
        employeeDeleteList.indexOf(item.EmployeeId) === -1 ? false : true
      "
      @check="$emit('check-box', item.EmployeeId)"
      @dblclick="doubleClick(item.EmployeeId)"
      @show-func="$emit('show-func', item.EmployeeId)"
      @update="doubleClick(item.EmployeeId)"
    ></tr-cus>
  </tbody>
</template>

<script>
import BaseTableBodyTr from "./BaseTableBodyTr";

export default {
  name: "base-table-body",

  components: {
    "tr-cus": BaseTableBodyTr,
  },

  emits: ["check-box", "update", "show-func"],

  props: {
    data: {
      type: Array,
      required: true,
    },
    columns: {
      type: Array,
      required: true,
    },
    employeeDeleteList: {
      type: Array,
      required: true,
    },
  },

  methods: {
    /**
     * check box row
     * CreatedBy: NHHoang (29/08/2021)
     */
    checkBox(id) {
      this.$emit("check-box", id);
    },

    /**
     * xử lý double click vào row để xem thông tin chi tiết
     * CreatedBy: NHHoang (29/08/2021)
     */
    doubleClick(id) {
      this.$emit("update", id);
    },
  },
};
</script>

<style lang="css"></style>
