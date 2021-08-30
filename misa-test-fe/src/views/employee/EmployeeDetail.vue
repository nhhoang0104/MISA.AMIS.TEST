<template>
  <div v-show="isShowed">
    <div class="modal"></div>
    <div class="dialog">
      <div class="dialog__header">
        <div class="dialog__header__title">
          <div class="title">Thông tin nhân viên</div>
        </div>
        <div class="dialog__header__close">
          <div class="icon icon--24 icon-help m-r-6"></div>
          <div class="icon icon--24 icon-close" @click="closeForm"></div>
        </div>
      </div>
      <div class="dialog__content">
        <div class="body">
          <div class="flex p-b--27">
            <div class="w-50 p-r--26">
              <div class="flex row-input">
                <div class="w-40 p-r--6">
                  <base-input
                    @onchangeinput="onChangeInput"
                    id="EmployeeCode"
                    :value="formData.EmployeeCode"
                    :hasLabel="true"
                    label="Mã"
                    :required="true"
                  ></base-input>
                </div>
                <div class="w-60">
                  <base-input
                    @onchangeinput="onChangeInput"
                    id="FullName"
                    :value="formData.FullName"
                    :hasLabel="true"
                    label="Tên"
                    :required="true"
                  ></base-input>
                </div>
              </div>
              <div class="row-input">
                <div class="w-100">
                  <combo-box
                    label="Đơn vị"
                    :required="true"
                    :data="departmentCbb"
                    :value="formData.DepartmentId"
                    id="DepartmentId"
                  >
                    <template #combo-box-options="{options}">
                      <combo-box-option
                        v-for="item in options"
                        :key="item.id"
                        :dataSrc="item"
                        :checked="item.id === department"
                        @select-item="selectItem"
                      ></combo-box-option>
                    </template>
                  </combo-box>
                </div>
              </div>
              <div class="row-input">
                <div class="w-100">
                  <base-input
                    :value="formData.PositionName"
                    :hasLabel="true"
                    label="Chức danh"
                    @onchangeinput="onChangeInput"
                    id="PositionName"
                  ></base-input>
                </div>
              </div>
            </div>
            <div class="w-50">
              <div class="flex row-input">
                <div class="w-40 p-r--6">
                  <!-- <base-input
                    :hasLabel="true"
                    type="date"
                    label="Ngày sinh"
                    :value="formData.DateOfBirth"
                    @onchangeinput="onChangeInput"
                    id="DateOfBirth"
                  ></base-input> -->
                  <base-date-picker></base-date-picker>
                </div>
                <div class="w-60">
                  <div class="flex flex-column">
                    <div class="label p-l--10">Giới tính</div>
                    <div class="p-l--10 p-t--5 p-b--6">
                      <label
                        class="container-radio"
                        for="0"
                        @click="onChangeInput({ value: 0, id: 'Gender' })"
                      >
                        <input label="0" type="radio" value="0" />
                        <span class="radio">
                          <span class="radio-border"></span>
                          <span
                            class="radio-circle"
                            v-show="formData.Gender === 0"
                          ></span>
                        </span>
                        <span class="label-radio">Nữ</span>
                      </label>
                      <label
                        class="container-radio"
                        for="1"
                        @click="onChangeInput({ value: 1, id: 'Gender' })"
                      >
                        <input type="radio" label="1" :value="1" />
                        <span class="radio">
                          <span class="radio-border"></span>
                          <span
                            class="radio-circle"
                            v-show="formData.Gender === 1"
                          ></span>
                        </span>
                        <span class="label-radio">Nam</span>
                      </label>
                      <label
                        class="container-radio"
                        for="2"
                        @click="onChangeInput({ value: 2, id: 'Gender' })"
                      >
                        <input type="radio" label="2" :value="2" />
                        <span class="radio">
                          <span class="radio-border"></span>
                          <span
                            class="radio-circle"
                            v-show="formData.Gender === 2"
                          ></span>
                        </span>
                        <span class="label-radio">Khác</span>
                      </label>
                    </div>
                  </div>
                </div>
              </div>
              <div class="flex row-input">
                <div class="w-60 p-r--6">
                  <base-input
                    :hasLabel="true"
                    label="Số CMND"
                    @onchangeinput="onChangeInput"
                    :value="formData.IdentityNumber"
                    format="number"
                    id="IdentityNumber"
                  ></base-input>
                </div>
                <div class="w-40">
                  <base-input
                    :hasLabel="true"
                    type="date"
                    label="Ngày cấp"
                    @onchangeinput="onChangeInput"
                    :value="formData.IdentityDate"
                    id="IdentityDate"
                  ></base-input>
                </div>
              </div>
              <div class="row-input">
                <div class="w-100">
                  <base-input
                    :hasLabel="true"
                    label="Nơi cấp"
                    @onchangeinput="onChangeInput"
                    :value="formData.IdentityPlace"
                    id="IdentityPlace"
                  ></base-input>
                </div>
              </div>
            </div>
          </div>
          <div class="p-b--27">
            <div class="row-input">
              <div class="w-100">
                <base-input
                  :hasLabel="true"
                  label="Địa chỉ"
                  @onchangeinput="onChangeInput"
                  id="Address"
                  :value="formData.Address"
                ></base-input>
              </div>
            </div>
            <div class="flex row-input">
              <div class="w-25 p-r--6">
                <base-input
                  :hasLabel="true"
                  label="ĐT di động"
                  @onchangeinput="onChangeInput"
                  id="MobilePhoneNumber"
                  :value="formData.MobilePhoneNumber"
                ></base-input>
              </div>
              <div class="w-25 p-r--6">
                <base-input
                  :hasLabel="true"
                  label="ĐT cố định"
                  @onchangeinput="onChangeInput"
                  id="TelephoneNumber"
                  :value="formData.TelephoneNumber"
                ></base-input>
              </div>
              <div class="w-25">
                <base-input
                  :hasLabel="true"
                  label="Email"
                  @onchangeinput="onChangeInput"
                  id="Email"
                  format="email"
                  :value="formData.Email"
                ></base-input>
              </div>
            </div>
            <div class="flex row-input">
              <div class="w-25 p-r--6">
                <base-input
                  :hasLabel="true"
                  label="Tài khoản ngân hàng"
                  @onchangeinput="onChangeInput"
                  id="BankAccount"
                  :value="formData.BankAccount"
                ></base-input>
              </div>
              <div class="w-25 p-r--6">
                <base-input
                  :hasLabel="true"
                  label="Tên ngân hàng"
                  @onchangeinput="onChangeInput"
                  id="BankName"
                  :value="formData.BankName"
                ></base-input>
              </div>
              <div class="w-25">
                <base-input
                  :hasLabel="true"
                  label="Chi nhánh"
                  @onchangeinput="onChangeInput"
                  id="BankBranch"
                  :value="formData.BankBranch"
                ></base-input>
              </div>
            </div>
          </div>
        </div>
        <div class="footer">
          <div class="divide"></div>
          <div
            class="flex"
            style="align-items: center; justify-content: space-between;"
          >
            <base-button
              label="Hủy"
              :secondary="true"
              @click="closeForm"
            ></base-button>
            <div class="flex">
              <div class="p-l--10 p-r--10">
                <base-button
                  label="Cất"
                  :secondary="true"
                  @onclick="onSubmit"
                ></base-button>
              </div>
              <base-button label="Cất và Thêm"></base-button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <base-popup :info="popupInfo" @close="closePopup"></base-popup>
  </div>
</template>

<script>
import { EmployeeModel } from "@/models/EmployeeModel";
import _ from "lodash";
import DepartmentAPI from "@/api/components/DepartmentAPI";
import EmployeeAPI from "@/api/components/EmployeeAPI";
import FormatData from "@/utils/FormatData.js";

export default {
  props: {
    isShowed: {
      type: Boolean,
      default: false,
    },

    formMode: {
      type: Number,
      default: 0,
    },

    employeeId: {
      type: [String, null],
      default: null,
    },
  },

  emits: ["closeForm"],

  watch: {
    isShowed(newVal) {
      let tmp = "";
      if (newVal) {
        if (this.formMode === 1 || this.formMode === 3) {
          EmployeeAPI.getNewEmployeeCode().then((res) => {
            this.formData.EmployeeCode = res.data;
            tmp = res.data;
          });
        }

        if (this.formMode === 2 || this.formMode === 3) {
          EmployeeAPI.getById(this.employeeId).then((res) => {
            this.formData = res.data;

            if (this.formMode === 3) this.formData.EmployeeCode = tmp;

            this.formData.DateOfBirth = FormatData.formatDateInput(
              res.data.DateOfBirth
            );

            this.formData.IdentityDate = FormatData.formatDateInput(
              res.data.IdentityDate
            );
          });
        }
      }
    },
  },

  data() {
    return {
      formData: _.cloneDeep(EmployeeModel),
      test: [
        { id: "0", code: 0, label: "Nữ" },
        { id: "1", code: 1, label: "Nam" },
        { id: "2", code: 2, label: "Khác" },
      ],
      department: "1",
      departmentCbb: [],
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
    DepartmentAPI.getAll().then((res) => {
      this.departmentCbb = _.map(res.data, (item) => {
        return {
          id: item.DepartmentId,
          code: item.DepartmentCode,
          label: item.DepartmentName,
        };
      });
    });
  },

  methods: {
    /**
     * Xử lý chọn option trong cac combobox
     * CreateBy: NHHoang(28/08/2021)
     */
    selectItem({ id, value }) {
      this.formData[id] = value;
    },

    /**
     * xử lý onchangeinput
     * CreateBy: NHHoang(29/08/2021)
     */
    onChangeInput({ value, id }) {
      this.formData[id] = value;
    },

    onSubmit() {
      console.log(this.formData);
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
     * đóng form
     * CreatedBy: NHHoang (29/08/2021)
     */
    closeForm() {
      this.formData = _.cloneDeep(EmployeeModel);
      this.$emit("close-form");
    },
  },
};
</script>

<style lang="css"></style>
