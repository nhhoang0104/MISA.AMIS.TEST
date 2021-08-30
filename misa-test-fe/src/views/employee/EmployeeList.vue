<template>
  <div class="content__header">
    <div class="content__header__title">Nhân viên</div>
    <div class="content__header__btn">
      <base-button
        label="Thêm mới nhân viên"
        @click="showForm(1)"
      ></base-button>
    </div>
  </div>
  <div class="content__body">
    <div class="content__body__toolbar">
      <div class="input-list">
        <base-input
          :hasIcon="true"
          iconName="icon-search"
          placeholder="Tìm kiếm theo mã, tên"
          @onchangeinput="onChangeEmployeeFiler"
        ></base-input>
      </div>
      <div class="utils-btn">
        <div class="p-l--6 p-r--6">
          <div
            class="icon icon--24 icon-refresh refresh"
            @click="refresh"
          ></div>
        </div>
        <div class="p-l--6">
          <div class="icon icon--24 icon-export export"></div>
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
            ref="table"
            :columns="columns"
            :data="employeeList"
            :employeeDeleteList="employeeIdDeleteList"
            @check-box="null"
            @show-func="showFunc"
            @update="update"
          ></base-table-body>
        </template>
      </base-table>
      <div class="pagination">
        <base-pagination
          :currentPage="currentPage"
          :pageSize="pageSize"
          :totalRecord="totalRecord"
          :totalPage="totalPage"
          @select-page="selectPage"
          @select-size="selectPageSize"
        ></base-pagination>
      </div>
    </div>
    <div
      ref="box-func"
      class="box__func"
      :style="positionFuc"
      v-show="posFunc.isShowed"
      @focusout="clickOutSide"
    >
      <div class="item__func" @click="replica">Nhân bản</div>
      <div class="item__func" @click="remove">Xóa</div>
    </div>
  </div>
  <EmployeeDetail
    :isShowed="isShowedForm"
    :formMode="formMode"
    @close-form="closeForm"
    :employeeId="employeeIdSelected"
  />
  <div v-show="true"><base-popup></base-popup></div>
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
      currentPage: 1,
      employeeFilter: "",
      pageSize: 10,
      totalPage: 10,
      totalRecord: 100,
      timeoutItem: null,
      posFunc: {
        id: null,
        left: 960,
        top: 260,
        isShowed: false,
      },
      isShowedForm: false,
      formMode: 0,
      employeeIdSelected: null,
    };
  },

  created() {
    /**
     * Lấy dữ liệu
     */
    this.loadData();
  },

  computed: {
    positionFuc() {
      return {
        left: `${this.posFunc.left}px`,
        top: `${this.posFunc.top}px`,
      };
    },
  },

  methods: {
    /**
     * refresh data
     * CreatedBy: NHHoang (29/08/2021)
     */
    refresh() {
      this.currentPage = 1;
      this.loadData();
    },

    /**
     * Lấy dữ liệu
     * CreatedBy: NHHoang (29/08/2021)
     */
    loadData() {
      EmployeeAPI.getByFilterPaging(
        this.currentPage,
        this.pageSize,
        this.employeeFilter
      ).then((res) => {
        this.employeeList = res.data.employees;
        this.totalRecord = res.data.totalRecord;
        this.totalPage = res.data.totalPage;
      });
    },

    /**
     * Chọn pageIndex
     * CreatedBy: NHHoang (29/08/2021)
     */
    selectPage(page) {
      this.currentPage = page;
      this.loadData();
    },

    /**
     * Chọn pageSize
     * CreatedBy: NHHoang (29/08/2021)
     */
    selectPageSize(size) {
      this.currentPage = 1;
      this.pageSize = size;
      this.loadData();
    },

    /**
     * Xử lý thay đổi employeeFilter
     * cleartimeout -> tránh rơi vào vòng callback ~ gọi nhiều lên be
     * CreatedBy: NHHoang (29/08/2021)
     */
    onChangeEmployeeFiler({ value }) {
      clearTimeout(this.timeoutItem);

      this.timeoutItem = setTimeout(() => {
        this.currentPage = 1;
        this.employeeFilter = value;
        this.loadData();
      }, 300);
    },

    /**
     * Mở hộp tính năng
     * CreatedBy: NHHoang (29/08/2021)
     */
    showFunc(id) {
      let position = this.$refs.table.$refs[id].$refs[
        "func"
      ].getBoundingClientRect();

      if (this.posFunc.id === id) {
        this.posFunc.isShowed = false;
        this.posFunc.id = null;
      } else {
        this.posFunc.id = id;
        this.posFunc.top = position.top - 110;
        this.posFunc.isShowed = true;
      }
    },

    /**
     * click outside boxfunc
     * CreatedBy: NHHoang (29/08/2021)
     */
    clickOutSide() {
      this.posFunc.isShowed = false;
    },

    /**
     * Xóa theo id
     * CreatedBy: NHHoang (29/08/2021)
     */
    remove() {
      this.posFunc.isShowed = false;
      if (this.posFunc.id) {
        EmployeeAPI.delete(this.posFunc.id).then(() => {
          this.loadData();
        });
      }
    },

    /**
     * Nhân bản
     * CreatedBy: NHHoang (29/08/2021)
     */
    replica() {
      this.posFunc.isShowed = false;
      this.formMode = 3;
      this.employeeIdSelected = this.posFunc.id;
      this.isShowedForm = true;
    },

    /**
     * Xử lý show form khi thêm mới
     * tạo mã nhân vien mới
     * CreatedBy: NHHoang (29/08/2021)
     */
    showForm(formMode) {
      this.posFunc.isShowed = false;
      this.isShowedForm = true;
      this.formMode = formMode;
    },

    /**
     * đóng form
     * CreatedBy: NHHoang (29/08/2021)
     */

    closeForm() {
      this.isShowedForm = false;
    },

    update(id) {
      this.posFunc.isShowed = false;
      this.employeeIdSelected = id;
      this.showForm(2);
    },
  },
};
</script>

<style lang="css"></style>
