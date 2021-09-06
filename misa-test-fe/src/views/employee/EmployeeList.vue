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
      <div class="left">
        <base-button
          label="Xóa nhiều"
          @click="preDeleteList"
          :secondary="true"
          v-show="employeeIdDeleteList.length > 1"
        ></base-button>
      </div>
      <div class="right flex p-r--16">
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
    </div>
    <div
      class="content__body__grid"
      :class="employeeList.length === 0 ? 'no-data' : ''"
    >
      <base-table>
        <template v-slot:thead>
          <base-table-head
            :columns="columns"
            :checked="isSelectedAll"
            @select-all="selectAll"
          ></base-table-head>
        </template>
        <template v-slot:tbody>
          <base-table-body
            ref="table"
            :columns="columns"
            :data="employeeList"
            :employeeDeleteList="employeeIdDeleteList"
            @check-box="checkBox"
            @show-func="showBoxFunc"
            @update="update"
          ></base-table-body>
        </template>
      </base-table>
      <div class="pagination" v-show="employeeList.length !== 0">
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
      class="no-data flex flex-column flex-center"
      :class="{ hidden: isLoading || employeeList.length !== 0 }"
    >
      <img src="../../assets/img/bg_nodata.svg" />
      <div>Không có dữ liệu</div>
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
    @change-mode="changeMode"
    @close-form="closeForm"
    :employeeId="employeeIdSelected"
    @load-data="loadData"
    @add-toast="addToast"
  />
  <base-toast-message
    v-for="(item, index) in toastList"
    :toast="item"
    :key="index"
    :index="index"
  ></base-toast-message>
  <base-popup :info="popupInfo" @close="closePopup"></base-popup>
  <base-loader :isLoading="isLoading"></base-loader>
</template>

<script>
import { columns } from "@/views/employee/Column.js";
import EmployeeAPI from "@/api/components/EmployeeAPI";
import EmployeeDetail from "./EmployeeDetail.vue";
import Resource from "@/constants/Resource";
import _ from "lodash";

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
      isLoading: false,
      toastList: [],
      Resource: Resource,
      timeoutSearch: null,
      timeoutRemoveToastList: null,
    };
  },

  created() {
    /**
     * Lấy dữ liệu
     * CreatedBy: NHHoang (31/08/2021)
     */
    this.loadData();
  },

  watch: {
    /**
     * Xóa danh sách toastList sau 3 đối với phần tử cuối cùng.
     * CreatedBy: NHHoang(01/09/2021)
     */

    toastList: {
      deep: true,
      immediate: true,
      handler: function() {
        clearTimeout(this.timeoutRemoveToastList);

        this.timeoutRemoveToastList = setTimeout(() => {
          if (this.toastList.length > 0) this.toastList = [];
        }, 3000);
      },
    },
  },

  computed: {
    /**
     * Xét vị trí của box func
     * CreatedBy: NHHoang (29/08/2021)
     */
    positionFuc() {
      return {
        left: `${this.boxFunc.left}px`,
        top: `${this.boxFunc.top}px`,
      };
    },

    /*
     * xét checkbox chọn tất cả. true- chọn. false - không
     * CreatedBy: NHHoang (31/08/2021)
     */
    isSelectedAll() {
      if (this.employeeIdDeleteList.length === 0) return false;

      var isSelectedAll = true;

      this.employeeList.forEach((employee) => {
        if (this.employeeIdDeleteList.indexOf(employee.EmployeeId) === -1) {
          isSelectedAll = false;
        }
      });

      return isSelectedAll;
    },
  },

  methods: {
    /**
     * refresh data
     * CreatedBy: NHHoang (29/08/2021)
     */
    refresh() {
      this.loadData();
    },

    /**
     * Lấy dữ liệu
     * CreatedBy: NHHoang (29/08/2021)
     */
    loadData() {
      this.isLoading = true;

      EmployeeAPI.getByFilterPaging(
        this.currentPage,
        this.pageSize,
        this.employeeFilter
      )
        .then((res) => {
          if (res.status != 204) {
            this.toastList.push({
              type: Resource.ToastType.Success,
              message: Resource.ToastMessage.LoadSuccess,
            });

            this.employeeList = res.data.Employees;
            this.totalRecord = res.data.TotalRecord;
            this.totalPage = res.data.TotalPage;
            this.isLoading = false;
          }
        })
        .catch((err) => {
          if (err.response?.status >= 500) {
            this.toastList.push({
              type: Resource.ToastType.Error,
              message: Resource.ToastMesssage.ServerError,
            });
          }
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
      clearTimeout(this.timeoutSearch);

      this.timeoutSearch = setTimeout(() => {
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
        "icon-warning",
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
     * CreatedBy: NHHoang (31/08/2021)
     */
    deleteById() {
      if (this.boxFunc.id) {
        //loại bỏ khỏi danh sách xóa nhiều
        let index = this.employeeIdDeleteList.indexOf(this.boxFunc.id);

        if (index !== -1) this.employeeIdDeleteList.splice(index, 1);

        this.isLoading = true;
        EmployeeAPI.delete(this.boxFunc.id)
          .then((res) => {
            if (res.status !== 204) {
              this.toastList.push({
                type: Resource.ToastType.Success,
                message: Resource.ToastMessage.DeleteSuccess,
              });

              this.loadData();
            }

            if (res.status === 204) {
              this.setPopup(
                Resource.ToastMessage.DeleteError,
                "icon-error",
                null,
                null,
                null,
                "Đóng",
                null,
                null
              );
            }
          })
          .catch(() => {
            this.toastList.push({
              type: Resource.ToastType.Error,
              message: Resource.ToastMessage.ServerError,
            });
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
     * formMode = 1 thêm mới
     * formMode = 2 sửa
     * formMode = 3 nhân bản
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

    /**
     * Thêm mới toastmessage
     * CreatedBy: NHHoang(31/08/2021)
     */
    addToast({ type, message }) {
      this.toastList.push({
        type,
        message,
      });
    },

    /**
     * Xóa toast mesage
     * CreatedBy: NHHoang(31/08/2021)
     */
    removeToast(id) {
      this.toastList = _.remove(this.toastList, {
        id,
      });
    },

    /**
     * Check box
     * CreatedBy: NHHoang(31/08/2021)
     */
    checkBox(id) {
      let index = this.employeeIdDeleteList.indexOf(id);

      if (index === -1) this.employeeIdDeleteList.push(id);
      else this.employeeIdDeleteList.splice(index, 1);
    },

    /**
     * Chọn tất cả
     * CreatedBy: NHHoang(31/08/2021)
     */
    selectAll(isCheckedAll = false) {
      if (isCheckedAll) {
        this.employeeList.forEach((employee) => {
          if (this.employeeIdDeleteList.indexOf(employee.EmployeeId) === -1) {
            this.employeeIdDeleteList.push(employee.EmployeeId);
          }
        });
      } else {
        let tmp = _.map(this.employeeList, "EmployeeId");
        this.employeeIdDeleteList = this.employeeIdDeleteList.filter((id) => {
          return !tmp.includes(id);
        });
      }
    },

    /**
     * Xóa nhiều
     * CreatedBy: NHHoang(31/08/2021)
     */
    deleteList() {
      this.isLoading = true;
      EmployeeAPI.deleteList(this.employeeIdDeleteList)
        .then((res) => {
          if (res.status != 204) {
            this.toastList.push({
              type: Resource.ToastType.Success,
              message: Resource.ToastMessage.DeleteSuccess,
            });
            this.employeeIdDeleteList = [];
            this.loadData();
          }
        })
        .catch(() => {
          this.toastList.push({
            type: Resource.ToastType.Error,
            message: Resource.ToastMessage.ServerError,
          });
        });
    },

    /**
     * hiển thị popup hỏi xem muốn xóa nhiều hay không
     * CreatedBy: NHHoang(31/08/2021)
     */
    preDeleteList() {
      let message = `Bạn có thực sự muốn xóa các nhân viên không?`;

      this.setPopup(
        message,
        "icon-warning",
        "Không",
        null,
        "Có",
        null,
        this.deleteList,
        null
      );
    },

    /**
     * Thay đổi formMode
     * CreatedBy: NHHoang(02/09/2021)
     */
    changeMode(mode) {
      this.formMode = mode;
    },
  },
};
</script>

<style lang="css"></style>
