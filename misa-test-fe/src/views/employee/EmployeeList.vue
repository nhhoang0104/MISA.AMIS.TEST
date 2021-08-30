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
          <div
            class="icon icon--24 icon-export export"
            @click="exportdData"
          ></div>
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
            @show-func="showBoxFunc"
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
      v-show="boxFunc.isShowed"
      @focusout="clickOutSide"
    >
      <div class="item__func" @click="replica">Nhân bản</div>
      <div class="item__func" @click="preDeleleteById">Xóa</div>
    </div>
  </div>
  <EmployeeDetail
    :isShowed="isShowedForm"
    :formMode="formMode"
    @close-form="closeForm"
    :employeeId="employeeIdSelected"
  />
  <base-popup :info="popupInfo" @close="closePopup"></base-popup>
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
      boxFunc: {
        id: null,
        left: 960,
        top: 260,
        isShowed: false,
      },
      isShowedForm: false,
      formMode: 0,
      employeeIdSelected: null,
      popupInfo: {
        btnLeft: null,
        btnRightFirst: null,
        btnRightSec: null,
        btnCenter: null,
        isShowed: false,
        icon: null,
        message: "",
        action: null,
        cancel: null,
      },
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
        left: `${this.boxFunc.left}px`,
        top: `${this.boxFunc.top}px`,
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
    showBoxFunc(id) {
      let position = this.$refs.table.$refs[id].$refs[
        "func"
      ].getBoundingClientRect();

      if (this.boxFunc.id === id && this.boxFunc.isShowed === true) {
        this.closeBoxFunc();
      } else {
        this.boxFunc.id = id;
        this.boxFunc.top = position.top - 110;
        this.boxFunc.isShowed = true;
      }
    },

    /**
     * đóng box func
     * CreatedBy: NHHoang (29/08/2021)
     */

    closeBoxFunc() {
      this.boxFunc.isShowed = false;
      this.boxFunc.id = null;
    },

    /**
     * click outside boxfunc
     * CreatedBy: NHHoang (29/08/2021)
     */
    clickOutSide() {
      this.boxFunc.isShowed = false;
    },

    /**
     * Hiện popup xác nhận muốn xóa không?
     * CreatedBy: NHHoang (29/08/2021)
     */

    preDeleleteById() {
      this.boxFunc.isShowed = false;
      let employee = this.employeeList.find(
        (item) => item.EmployeeId === this.boxFunc.id
      );
      let message = `Bạn có thực sự muốn xóa Nhân viên < ${
        employee ? employee.EmployeeCode : ""
      } > không?`;

      this.setPopup(
        message,
        "icon-ques",
        "Không",
        null,
        "Có",
        null,
        this.deleteById,
        null
      );
    },

    /**
     * Xóa theo id
     * CreatedBy: NHHoang (29/08/2021)
     */
    deleteById() {
      if (this.boxFunc.id) {
        EmployeeAPI.delete(this.boxFunc.id).then(() => {
          this.loadData();
        });

        this.closeBoxFunc();
      }
    },

    /**
     * Nhân bản
     * CreatedBy: NHHoang (29/08/2021)
     */
    replica() {
      this.boxFunc.isShowed = false;
      this.formMode = 3;
      this.employeeIdSelected = this.boxFunc.id;
      this.isShowedForm = true;
    },

    /**
     * Xử lý show form khi thêm mới
     * tạo mã nhân vien mới
     * CreatedBy: NHHoang (29/08/2021)
     */
    showForm(formMode) {
      this.boxFunc.isShowed = false;
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

    /**
     * Chỉnh sửa thông tin bản ghi
     * CreatedBy: NHHoang (29/08/2021)
     */
    update(id) {
      this.boxFunc.isShowed = false;
      this.employeeIdSelected = id;
      this.showForm(2);
    },

    /**
     * thiết lập popup
     * @param msg: tin nhắn
     * @param icon: tên icon
     * @param btnLeft: tên của nút bấm bên trái -> Đóng form, với giá trị null : ko có.
     * @param btnRightFrist: tên của nút bấm bên phải thứ 2 -> đóng form và thực hiện hành động cancel, với giá trị  null : ko có
     * @param btnRightSec: tên của nút bấm bên phải thứ 2 -> đóng form và thực hiện hành đọng action, với giá trị  null : ko có
     * @param center: tên của nút bấm ở giữa  -> Đóng form ~ thường là message cảnh báo, với giá trị null : ko có
     * @param action: action sẽ thực hiện nếu bấm nút btnRightSec, với giá trị null : ko có
     * @param cancel: thực hiện hành động nếu bấm nút btnRightFirst, với giá trị null : ko có
     * CreatedBy: NHHoang (29/08/2021)
     */
    setPopup(
      message,
      icon,
      btnLef = null,
      btnRightFirst = null,
      btnRightSec = null,
      btnCenter = null,
      action = null,
      cancel = null
    ) {
      this.popupInfo = {
        btnLeft: btnLef,
        btnRightFirst,
        btnRightSec,
        btnCenter,
        isShowed: true,
        icon: icon,
        message,
        action,
        cancel,
      };
    },

    /**
     * đóng popup
     * CreatedBy: NHHoang (29/08/2021)
     */
    closePopup() {
      this.popupInfo = {
        btnLeft: null,
        btnRightFirst: null,
        btnRightSec: null,
        btnCenter: null,
        isShowed: false,
        icon: null,
        message: "",
        action: null,
        cancel: null,
      };
    },

    /**
     * Export dữ liệu
     * CreatedBy: NHHoang(30/08/2021)
     */
    exportdData() {
      EmployeeAPI.export(
        this.currentPage,
        this.pageSize,
        this.employeeFilter
      ).then((response) => {
        if (response) {
          const blob = new Blob([response.data], {
            type:
              "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          });

          const link = document.createElement("a");
          link.href = URL.createObjectURL(blob);
          link.download = "Danh sách nhân viên";
          link.click();
          URL.revokeObjectURL(link.href);
        }
      });
    },
  },
};
</script>

<style lang="css"></style>
