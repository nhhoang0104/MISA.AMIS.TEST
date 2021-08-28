<template lang="html">
  <div class="content">
    <div class="content__header">
      <div class="content__header__title">Nhân viên</div>
      <div class="content__header__btn">
        <base-button> </base-button>
      </div>
    </div>
    <div class="content__body">
      <div class="content__body__toolbar">
        <div class="input-list">
          <base-input :hasIcon="true" iconName="icon-search"></base-input>
        </div>
        <div class="utils-btn">
          <div class="p-l--6 p-r--6">
            <div class="icon icon--24 icon-refresh"></div>
          </div>
          <div class="p-l--6">
            <div class="icon icon--24 icon-export"></div>
          </div>
        </div>
      </div>
      <div class="content__body__grid">
        <base-table>
          <template v-slot:thead>
            <base-table-head
              :columns="columns"
              :checked="false"
            ></base-table-head>
          </template>
          <template v-slot:tbody>
            <base-table-body
              :columns="columns"
              :data="employeeList"
              :employeeDeleteList="employeeIdDeleteList"
              @check-box="checkbox"
              @dblclick="viewInfo"
            ></base-table-body>
          </template>
        </base-table>
        <div class="pagination"></div>
      </div>
    </div>
    <div v-show="true"><EmployeeDetail /></div>
  </div>
</template>

<script>
import { columns } from "@/views/employee/Column.js";
import EmployeeAPI from "@/api/components/EmployeeAPI";
import EmployeeDetail from "./EmployeeDetail.vue";

export default {
  components: { EmployeeDetail },
  data() {
    return {
      columns: columns,
      employeeList: [],
      employeeIdDeleteList: [],
      pageIndex: 1,
      pageSize: 10,
    };
  },

  created() {
    EmployeeAPI.getByFilterPaging(1, 10).then((res) => {
      this.employeeList = res.data.employees;
    });
  },
};
</script>

<style lang="css"></style>
