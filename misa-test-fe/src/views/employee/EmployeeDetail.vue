<template>
  <div v-show="isShowed">
    <div class="modal"></div>
    <div class="dialog">
      <div class="dialog__header">
        <div class="dialog__header__title flex">
          <div class="title">Thông tin nhân viên</div>
          <div class="flex flex-center p-l--20 p-r--20">
            <div><input type="checkbox" /></div>
            <div class="p-l--10">Là khách hàng</div>
          </div>
          <div class="flex flex-center p-l--20 p-r--20">
            <div><input type="checkbox" /></div>
            <div class="p-l--10">Là nhà cung cấp</div>
          </div>
        </div>
        <div class="dialog__header__close">
          <div class="icon icon--24 icon-help m-r-6"></div>
          <div class="icon icon--24 icon-close" @click="preCloseForm"></div>
        </div>
      </div>
      <div class="dialog__content">
        <div class="body">
          <div class="flex p-b--27">
            <div class="w-50 p-r--26">
              <div class="flex row-input">
                <div class="w-40 p-r--6">
                  <base-input
                    ref="EmployeeCode"
                    @onchangeinput="onChangeInput"
                    id="EmployeeCode"
                    :value="formData.EmployeeCode"
                    :hasLabel="true"
                    label="Mã"
                    placeholder="MF-999"
                    :required="true"
                    :tabIndex="1"
                    :maxLength="20"
                  ></base-input>
                </div>
                <div class="w-60">
                  <base-input
                    ref="FullName"
                    @onchangeinput="onChangeInput"
                    id="FullName"
                    :value="formData.FullName"
                    :hasLabel="true"
                    label="Tên"
                    placeholder="Nguyễn Huy Hoàng"
                    :required="true"
                    :tabIndex="2"
                    :maxLength="100"
                  ></base-input>
                </div>
              </div>
              <div class="row-input">
                <div class="w-100">
                  <combo-box
                    label="Đơn vị"
                    ref="DepartmentId"
                    :required="true"
                    placeholder="Chọn đơn vị"
                    :data="departmentCbb"
                    :value="formData.DepartmentId"
                    id="DepartmentId"
                    @select-item="selectItem"
                    :tabIndex="3"
                  >
                    <template #combo-box-options="{options,indexItem}">
                      <combo-box-option
                        v-for="(item, index) in options"
                        :key="item.id"
                        :hover="index === indexItem"
                        :dataSrc="item"
                        :checked="item.id === formData.DepartmentId"
                        @select-item="selectItem"
                      ></combo-box-option>
                    </template>
                  </combo-box>
                </div>
              </div>
              <div class="row-input">
                <div class="w-100">
                  <base-input
                    ref="PositionName"
                    :value="formData.PositionName"
                    :hasLabel="true"
                    placeholder="Nhân viên"
                    label="Chức danh"
                    @onchangeinput="onChangeInput"
                    id="PositionName"
                    :tabIndex="4"
                    :maxLength="100"
                  ></base-input>
                </div>
              </div>
            </div>
            <div class="w-50">
              <div class="flex row-input">
                <div class="w-40 p-r--6">
                  <base-input
                    :tabIndex="5"
                    :hasLabel="true"
                    ref="DateOfBirth"
                    type="date"
                    label="Ngày sinh"
                    format="date"
                    :value="formData.DateOfBirth"
                    @onchangeinput="onChangeInput"
                    id="DateOfBirth"
                  ></base-input>
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
                        <input
                          type="radio"
                          label="1"
                          :value="1"
                          :tabIndex="16"
                        />
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
                    placeholder="001200012316"
                    ref="IdentityNumber"
                    @onchangeinput="onChangeInput"
                    :value="formData.IdentityNumber"
                    format="number"
                    id="IdentityNumber"
                    :tabIndex="6"
                    :maxLength="12"
                  ></base-input>
                </div>
                <div class="w-40">
                  <base-input
                    :hasLabel="true"
                    ref="IdentityDate"
                    type="date"
                    format="date"
                    label="Ngày cấp"
                    @onchangeinput="onChangeInput"
                    :value="formData.IdentityDate"
                    id="IdentityDate"
                    :tabIndex="7"
                  ></base-input>
                </div>
              </div>
              <div class="row-input">
                <div class="w-100">
                  <base-input
                    ref="IdentityPlace"
                    :hasLabel="true"
                    label="Nơi cấp"
                    @onchangeinput="onChangeInput"
                    :value="formData.IdentityPlace"
                    id="IdentityPlace"
                    placeholder="Hà Nội"
                    :tabIndex="8"
                    :maxLength="255"
                  ></base-input>
                </div>
              </div>
            </div>
          </div>
          <div class="flex flex-column">
            <div class="row-input">
              <div class="w-100">
                <base-input
                  ref="Address"
                  :hasLabel="true"
                  label="Địa chỉ"
                  @onchangeinput="onChangeInput"
                  id="Address"
                  placeholder="Hà Nội"
                  :value="formData.Address"
                  :tabIndex="9"
                  :maxLength="255"
                ></base-input>
              </div>
            </div>
            <div class="flex row-input">
              <div class="w-25 p-r--6">
                <base-input
                  ref="MobilePhoneNumber"
                  :hasLabel="true"
                  label="ĐT di động"
                  placeholder="0349398126"
                  @onchangeinput="onChangeInput"
                  id="MobilePhoneNumber"
                  :value="formData.MobilePhoneNumber"
                  :tabIndex="10"
                  :maxLength="50"
                ></base-input>
              </div>
              <div class="w-25 p-r--6">
                <base-input
                  ref="TelephoneNumber"
                  :hasLabel="true"
                  label="ĐT cố định"
                  placeholder="(821) 707-0104"
                  @onchangeinput="onChangeInput"
                  id="TelephoneNumber"
                  :value="formData.TelephoneNumber"
                  :tabIndex="11"
                  :maxLength="50"
                ></base-input>
              </div>
              <div class="w-25">
                <base-input
                  ref="Email"
                  :hasLabel="true"
                  label="Email"
                  placeholder="nhhoanguet@gmail.com"
                  @onchangeinput="onChangeInput"
                  id="Email"
                  format="email"
                  :value="formData.Email"
                  :tabIndex="12"
                  :maxLength="50"
                ></base-input>
              </div>
            </div>
            <div class="flex row-input">
              <div class="w-25 p-r--6">
                <base-input
                  ref="BankAccount"
                  :hasLabel="true"
                  placeholder="774701"
                  label="Tài khoản ngân hàng"
                  @onchangeinput="onChangeInput"
                  id="BankAccount"
                  :value="formData.BankAccount"
                  :tabIndex="13"
                  :maxLength="25"
                ></base-input>
              </div>
              <div class="w-25 p-r--6">
                <base-input
                  ref="BankName"
                  :hasLabel="true"
                  label="Tên ngân hàng"
                  placeholder="ACB"
                  @onchangeinput="onChangeInput"
                  id="BankName"
                  :value="formData.BankName"
                  :tabIndex="14"
                  :maxLength="100"
                ></base-input>
              </div>
              <div class="w-25">
                <base-input
                  ref="BankBranch"
                  placeholder="Hà Thành"
                  :hasLabel="true"
                  label="Chi nhánh"
                  @onchangeinput="onChangeInput"
                  id="BankBranch"
                  :value="formData.BankBranch"
                  :maxLength="100"
                  :tabIndex="15"
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
                  @onclick="onSubmit(0)"
                ></base-button>
              </div>
              <base-button
                label="Cất và Thêm"
                @onclick="onSubmit(1)"
              ></base-button>
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
import ErrorMessage from "@/constants/EnumErrorMsg";
import Resource from "@/constants/Resource";

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

  emits: ["close-form", "load-data", "change-mode", "add-toast"],

  watch: {
    /**
     * Xử lý khi mở form, xét lại formData=null
     * CreateBy: NHHoang(28/08/2021)
     */
    isShowed(newVal) {
      let tmp = "";
      if (newVal) {
        if (this.formMode === 1 || this.formMode === 3) {
          EmployeeAPI.getNewEmployeeCode().then((res) => {
            this.formData.EmployeeCode = res.data;
            tmp = res.data;

            this.isFormDataChange = false;
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

            this.isFormDataChange = false;
          });
        }

        this.$refs["EmployeeCode"].focus();
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
      Resource: Resource,
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
      isFormDataChange: false,
      isLoading: false,
      typeSubmit: 0,
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
     * Tạo form mới
     * CreateBy: NHHoang(28/08/2021)
     */
    newForm() {
      this.formData = _.cloneDeep(EmployeeModel);

      Object.keys(this.$refs).forEach(
        (el) => (this.$refs[el].isValidated = true)
      );

      this.$emit("change-mode", 1);
      this.isFormDataChange = false;

      EmployeeAPI.getNewEmployeeCode().then((res) => {
        this.formData.EmployeeCode = res.data;
        this.$refs["EmployeeCode"].focus();
      });
    },

    /**
     * Xử lý chọn option trong cac combobox
     * CreateBy: NHHoang(28/08/2021)
     */
    selectItem({ id, value }) {
      this.formData[id] = value;
      this.isFormDataChange = true;
    },

    /**
     * xử lý onchangeinput
     * CreateBy: NHHoang(29/08/2021)
     */
    onChangeInput({ value, id }) {
      if (this.formData[id] !== value) {
        this.formData[id] = value;
        this.isFormDataChange = true;
      }
    },

    /**
     * action sẽ được thục hiện khi ấn cất hoặc cất thêm
     * CreateBy: NHHoang(29/08/2021)
     */
    async action() {
      let action = null;
      // validate
      let isValid = await this.validateForm();

      if (isValid) {
        // thêm mới
        if (this.formMode === 1 || this.formMode === 3) {
          action = EmployeeAPI.add(_.cloneDeep(this.formData));
        }

        // sửa
        if (this.formMode === 2) {
          action = EmployeeAPI.update(
            this.employeeId,
            _.cloneDeep(this.formData)
          );
        }
        
        //thực hiện action đã trả về ở hầm action
        if (action !== null) {
          action
            .then((res) => {
              if (res.status != 204) {
                if (this.formMode === 2) {
                  this.$emit("add-toast", {
                    type: Resource.ToastType.Success,
                    message: Resource.ToastMessage.EditSuccess,
                  });
                } else {
                  this.$emit("add-toast", {
                    type: Resource.ToastType.Success,
                    message: Resource.ToastMessage.AddSuccess,
                  });
                }

                if (this.typeSubmit === 1) {
                  this.newForm();
                  this.$emit("load-data");
                } else {
                  this.closeForm();
                  this.$emit("load-data");
                }
              }
            })
            .catch((err) => {
              if (err.response.status < 500 && err.response.status >= 400) {
                let msg = err.response.data.userMsg;
                this.$emit("add-toast", {
                  type: Resource.ToastType.Error,
                  message: msg,
                });
              }

              if (err.response.status >= 500) {
                this.$emit("add-toast", {
                  type: Resource.ToastType.Error,
                  message: Resource.ToastMesssage.ServerError,
                });
              }
            });
        }
      }
    },

    /**
     * submit form
     */
    onSubmit(type) {
      this.typeSubmit = type;
      if (this.formMode === 2 && !this.isFormDataChange) {
        if (this.typeSubmit === 1) {
          this.newForm();
        } else {
          this.closeForm();
        }
      } else {
        this.action();
      }
    },

    /**
     * validate form data
     * CreatedBy: NHHoang (28/08/2021)
     * ModifiedBy: NHHoang (01/09/2021)
     */
    async validateForm() {
      let isValidated = true;

      // validate các trường bắt buộc
      Object.keys(this.$refs).forEach((el) => {
        this.$refs[el].validate();

        if (!this.$refs[el].isValidated) {
          if (isValidated) {
            let msg = ErrorMessage[this.$refs[el].id];
            this.setPopup(
              msg,
              "icon-error",
              null,
              null,
              null,
              "Đóng",
              null,
              null
            );
          }

          isValidated = this.$refs[el].isValidated;
        }
      });

      // kiểm tra trùng mã nhân viên
      if (this.formMode === 1 || this.formMode === 3) {
        let {
          data: isCheckEmployeeExists,
        } = await EmployeeAPI.checkEmployeeExists(this.formData.EmployeeCode);

        if (isValidated && isCheckEmployeeExists) {
          isValidated = false;
          let msg = `Mã nhân viên < ${this.formData.EmployeeCode} > đã tồn tại trong hệ thống. Xin vui lòng kiểm ta lại!`;

          this.setPopup(
            msg,
            "icon-warning",
            null,
            null,
            null,
            "Đóng",
            null,
            null
          );
        }
      }

      return isValidated;
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

      Object.keys(this.$refs).forEach(
        (el) => (this.$refs[el].isValidated = true)
      );

      this.isFormDataChange = false;

      this.$emit("close-form");
    },

    /**
     * Xử lý xem data có thay đổi không và hỏi người dùng muốn lưu data ko? = popup
     * CreatedBy: NHHoang (29/08/2021)
     */
    preCloseForm() {
      this.typeSubmit = 0;
      if (this.isFormDataChange) {
        let msg = "Dữ liệu đã bị thay đổi. Bạn có muốn cất không";

        this.setPopup(
          msg,
          "icon-ques",
          "Hủy",
          "Không",
          "Có",
          null,
          this.action,
          this.closeForm
        );
      } else {
        this.closeForm();
      }
    },
  },
};
</script>

<style lang="css"></style>
