<template>
  <div>
    <div class="flex" v-show="hasLabel">
      <div class="label">{{ label }}</div>
      <div class="label-required" v-show="required">&nbsp;*</div>
    </div>
    <div class="container-input">
      <DxDateBox
        v-if="type === 'date'"
        :value="value"
        :onValueChanged="onChangeInput"
        displayFormat="dd/MM/yyyy"
        placeholder="DD/MM/YYYY"
        :useMaskBehavior="true"
        :showClearButton="false"
        class="dxDateBox"
        dateOutOfRangeMessage="Ngày chọn không được quá ngày hiện tại"
        :max="new Date()"
        :tabIndex="tabIndex"
      />
      <input
        v-else
        ref="focusMe"
        class="container-input__input"
        :class="{ 'input--icon': hasIcon }"
        :required="required"
        :type="type"
        :value="value"
        :title="title"
        :placeholder="placeholder"
        :max="`${type === 'date' ? currentDate : null}`"
        :validated="isValidated"
        :tabIndex="tabIndex"
        :maxlength="maxLength"
        @blur="blur"
        @input="onChangeInput"
      />
      <div
        class="container-input__icon icon icon--16"
        :class="iconName"
        v-show="hasIcon"
      ></div>
    </div>
  </div>
</template>

<script>
import FormatData from "@/utils/formatData";
import Validation from "@/utils/validation";
import Resource from "@/constants/resource";
import DxDateBox from "devextreme-vue/date-box";
import { locale } from "devextreme/localization";
locale("vi-VN");

export default {
  name: "base-input",

  components: {
    DxDateBox,
  },

  props: {
    id: { type: String, required: false },
    type: {
      type: String,
      default: "text",
    },
    format: {
      type: String,
      default: "text",
    },
    value: { type: [String, Date], default: "" },
    required: {
      type: Boolean,
      default: false,
    },
    hasIcon: {
      type: Boolean,
      default: false,
    },
    iconName: {
      type: String,
      default: "",
    },
    hasLabel: {
      type: Boolean,
      default: false,
    },
    label: {
      type: String,
      default: "",
    },
    placeholder: {
      type: String,
      default: "",
    },
    tabIndex: {
      type: Number,
      required: false,
    },
    maxLength: {
      type: Number,
      required: false,
      default: 255,
    },
  },

  emits: ["onchangeinput"],

  data() {
    return {
      currentDate: FormatData.formatDateInput(new Date()),
      isValidated: true,
      title: "",
      ErrorMessage: Resource.ErrorMessage,
      valueClone: "",
    };
  },

  methods: {
    /**
     * Thay đổi style khi focus
     * CreatedBy: NHHoang (27/08/2021)
     * Modified: NHHoang (01/09/2021)
     */
    focus() {
      this.$nextTick(() => this.$refs.focusMe.focus());
    },

    /**
     * Xử lý onchangeinput
     * CreatedBy: NHHoang (27/08/2021)
     */
    onChangeInput(event) {
      let tmp = null;

      if (this.type === "date") {
        tmp = event.value;
      } else {
        tmp = event.target.value;
        this.validate(tmp);
      }

      this.$emit("onchangeinput", { id: this.id, value: tmp });
    },

    /**
     * Validate các trường bắt buộc và đặc biệt (email)
     * CreatedBy: NHHoang (28/08/2021)
     * Modified: NHHoang (28/08/2021)
     */

    validate(tmp = null) {
      let value = tmp;

      if (value === null) value = this.value;
      try {
        if (this.required) {
          if (!Validation.validate(this.format, value)) {
            this.isValidated = false;
            this.title = this.ErrorMessage[this.id];
          } else {
            this.isValidated = true;
            this.title = "";
          }
        } else {
          if (value === null || value === "") {
            this.isValidated = true;
            this.title = "";
          } else {
            if (this.format === "email") {
              if (!Validation.validateEmail(value)) {
                this.isValidated = false;
                this.title = this.ErrorMessage[this.id];
              } else {
                this.isValidated = true;
                this.title = "";
              }
            }

            // if (this.format === "number") {
            //   if (!Validation.validateNumber(value)) {
            //     this.isValidated = false;
            //     this.title = this.ErrorMessage[this.id];
            //   } else {
            //     this.isValidated = true;
            //     this.title = "";
            //   }
            // }

            if (this.format === "date") {
              if (!Validation.validateDate(value)) {
                this.isValidated = false;
              } else {
                this.isValidated = true;
              }
            }
          }
        }

        return this.isValidated;
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>

<style lang="css">
@import url("../../assets/css/common/input.css");
@import url("../../assets/css/common/dateBox.css");
</style>
