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
          label="Xóa hàng loạt"
          @click="preDeleteList"
          :secondary="true"
          v-show="employeeIdDeleteList.length > 1"
        ></base-button>
      </div>
      <div class="right flex p-r--16">
        <div class="input-list">
          <base-input
            ref="searchBox"
            :hasIcon="true"
            iconName="icon-search"
            placeholder="Tìm kiếm theo mã, tên"
            @onchangeinput="onChangeEmployeeFiler"
          ></base-input>
        </div>
        <div class="utils-btn">
          <div class="p-l--6 p-r--6">
            <div
              title="Lấy lại dữ liệu"
              class="icon icon--24 icon-refresh refresh"
              @click="loadData"
            ></div>
          </div>
          <div class="p-l--6 p-r--6">
            <div
              title="Báo cáo nhân sự"
              class="icon icon--24 icon-report report"
              @click="openEmployeeReport"
            ></div>
          </div>
          <div class="p-l--6">
            <div
              title="Xuất ra excel"
              class="icon icon--24 icon-export export"
              @click="exportdData"
            ></div>
          </div>
        </div>
      </div>
    </div>
    <div class="content__body__grid">
      <misa-table
        :dataSource="dataSrc"
        :columns="columns"
        :employeeDeleteList="employeeIdDeleteList"
        :isSelectedAll="isSelectedAll"
        :currentPage="currentPage"
        :pageSize="pageSize"
        :totalRecord="totalRecord"
        :totalPage="totalPage"
        :pagingEnabled="true"
        @update="update"
        @select-all="selectAll"
        @check-box="checkBox"
        @replica="replica"
        @delete="preDeleteById"
        @select-page="selectPage"
        @select-size="selectPageSize"
      ></misa-table>
    </div>
  </div>

  <EmployeeDetail
    :isShowed="isShowedForm"
    :formMode="formMode"
    @change-mode="changeMode"
    @close-form="closeForm"
    :employeeId="employeeIdSelected"
    :employeeReplication="employeeReplication"
    @load-data="loadData"
    @add-toast="addToast"
  />

  <EmployeeReport
    :visible="isShowEmployeeReport"
    @close-report="closeEmployeeReport"
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
import { columns } from "@/views/employee/column.js";
import EmployeeAPI from "@/api/components/employeeAPI";
import EmployeeDetail from "./EmployeeDetailV3";
import EmployeeReport from "./EmployeeReport";
import Resource from "@/constants/resource";
import _ from "lodash";
import FormatData from "@/utils/formatData.js";

export default {
  components: { EmployeeDetail, EmployeeReport },

  data() {
    return {
      columns: columns,
      employeeList: [],
      dataSrc: [],
      employeeIdDeleteList: [],
      currentPage: 1,
      employeeFilter: "",
      pageSize: 10,
      totalPage: 10,
      totalRecord: 100,
      isShowedForm: false,
      formMode: 0,
      employeeIdSelected: null,
      employeeReplication: null,
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
      timeoutSearch: null,
      timeoutRemoveToastList: null,
      Resource,
      isShowEmployeeReport: false,
    };
  },

  created() {
    /**
     * Lấy dữ liệu
     * CreatedBy: NHHoang (31/08/2021)
     */
    this.loadData();
  },

  mounted() {
    /**
     * Khi vào trang sẽ tự động foucs vào searchBox.
     */
    this.$refs["searchBox"].focus();
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
        }, 2000);
      },
    },
  },

  computed: {
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
          if (res.status === Resource.StatusCode.Success) {
            this.toastList.push({
              type: Resource.ToastType.Success,
              message: Resource.ToastMessage.LoadSuccess,
            });

            this.employeeList = _.cloneDeep(res.data.Employees);
            this.dataSrc = res.data.Employees.map((item) => {
              item.DateOfBirth = FormatData.formatDate(item.DateOfBirth);
              return item;
            });

            this.totalRecord = res.data.TotalRecord;
            this.totalPage = res.data.TotalPage;
            this.isLoading = false;
          }
        })
        .catch((err) => {
          this.isLoading = false;

          if (
            err.response.status >= Resource.StatusCode.BadRequest &&
            err.response.status < Resource.StatusCode.Server
          ) {
            this.toastList.push({
              type: Resource.ToastType.Error,
              message: err.response.data.userMsg,
            });
          }

          if (err.response.status >= Resource.StatusCode.Server) {
            this.toastList.push({
              type: Resource.ToastType.Error,
              message: Resource.ToastMessage.ServerError,
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
      try {
        clearTimeout(this.timeoutSearch);

        this.timeoutSearch = setTimeout(() => {
          this.currentPage = 1;
          this.employeeFilter = value;
          this.loadData();
        }, 300);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Hiện popup xác nhận muốn xóa không?
     * CreatedBy: NHHoang (29/08/2021)
     */
    preDeleteById(id) {
      let employee = this.employeeList.find((item) => item.EmployeeId === id);

      let message = Resource.PopupMessage.DeleteById.format(
        employee ? employee.EmployeeCode : ""
      );

      this.setPopup(
        message,
        "icon-warning",
        "Không",
        null,
        "Có",
        null,
        () => this.deleteById(id),
        null
      );
    },

    /**
     * Xóa theo id
     * CreatedBy: NHHoang (29/08/2021)
     * CreatedBy: NHHoang (31/08/2021)
     */
    deleteById(id = null) {
      if (id) {
        //loại bỏ khỏi danh sách xóa nhiều
        try {
          let index = this.employeeIdDeleteList.indexOf(id);

          if (index !== -1) this.employeeIdDeleteList.splice(index, 1);
        } catch (error) {
          console.log(error);
        }

        this.isLoading = true;
        EmployeeAPI.delete(id)
          .then((res) => {
            this.isLoading = false;
            if (res.status === Resource.StatusCode.Success) {
              this.toastList.push({
                type: Resource.ToastType.Success,
                message: Resource.ToastMessage.DeleteSuccess,
              });

              this.loadData();
            }

            if (res.status === Resource.StatusCode.NoContent) {
              this.setPopup(
                Resource.ToastMessage.EmployeeExistError,
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
          .catch((err) => {
            this.isLoading = false;

            if (
              err.response.status >= Resource.StatusCode.BadRequest &&
              err.response.status < Resource.StatusCode.Server
            ) {
              this.toastList.push({
                type: Resource.ToastType.Error,
                message: err.response.data.userMsg,
              });
            }

            if (err.response.status >= Resource.StatusCode.Server) {
              this.toastList.push({
                type: Resource.ToastType.Error,
                message: Resource.ToastMessage.ServerError,
              });
            }
          });
      }
    },

    /**
     * Nhân bản
     * CreatedBy: NHHoang (29/08/2021)
     */
    replica(id) {
      this.employeeReplication = _.find(this.employeeList, {
        EmployeeId: id,
      });

      this.formMode = Resource.FormMode.Replica;
      this.isShowedForm = true;
    },

    /**
     * Xử lý show form khi thêm mới
     * CreatedBy: NHHoang (29/08/2021)
     */
    showForm(formMode) {
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
    async update(id) {
      this.isLoading = true;
      let isExists = await this.checkEmployeeExists(id);
      this.isLoading = false;
      if (isExists) {
        this.employeeIdSelected = id;
        this.showForm(Resource.FormMode.Update);
      }
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
      this.popupInfo.isShowed = false;

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
          this.isLoading = false;
          if (res.status === Resource.StatusCode.Success) {
            this.toastList.push({
              type: Resource.ToastType.Success,
              message: Resource.ToastMessage.DeleteSuccess,
            });

            this.employeeIdDeleteList = [];
            this.loadData();
          }

          if (res.status === Resource.StatusCode.NoContent) {
            this.setPopup(
              Resource.ToastMessage.DeleteListError,
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
        .catch((err) => {
          this.isLoading = false;

          if (
            err.response.status >= Resource.StatusCode.BadRequest &&
            err.response.status < Resource.StatusCode.Server
          ) {
            this.toastList.push({
              type: Resource.ToastType.Error,
              message: err.response.data.userMsg,
            });
          }

          if (err.response.status >= Resource.StatusCode.Server) {
            this.toastList.push({
              type: Resource.ToastType.Error,
              message: Resource.ToastMessage.ServerError,
            });
          }
        });
    },

    /**
     * hiển thị popup hỏi xem muốn xóa nhiều hay không
     * CreatedBy: NHHoang(31/08/2021)
     */
    preDeleteList() {
      this.setPopup(
        Resource.PopupMessage.DeleteList,
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

    /**
     * Kiểm tra nhân viên có tồn tại không
     * CreatedBy: NHHoang (28/08/2021)
     */
    async checkEmployeeExists(id) {
      let { data: isExists } = await EmployeeAPI.checkEmployeeExists(id);

      if (!isExists) {
        this.setPopup(
          Resource.ToastMessage.EmployeeExistError,
          "icon-error",
          null,
          null,
          null,
          "Đóng",
          null,
          null
        );
      }

      return isExists;
    },

    /**
     * Mở báo cáo nhân sự
     * CreatedBy: NHHoang (22/09/2021)
     */
    openEmployeeReport() {
      this.isShowEmployeeReport = true;
    },

    /**
     * Đóng báo cáo nhân sự
     * CreatedBy: NHHoang (22/09/2021)
     */
    closeEmployeeReport() {
      this.isShowEmployeeReport = false;
    },
  },
};
</script>

<style lang="css"></style>
