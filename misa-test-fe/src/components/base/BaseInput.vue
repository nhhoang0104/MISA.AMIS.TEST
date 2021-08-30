<template>
  <div>
    <div class="flex" v-show="hasLabel">
      <div class="label">{{ label }}</div>
      <div class="label-required" v-show="required">&nbsp;*</div>
    </div>
    <div class="container-input">
      <input
        :class="className"
        :required="required"
        :type="type"
        :value="value"
        :title="title"
        :placeholder="placeholder"
        :max="`${type === 'date' ? currentDate : null}`"
        :validated="isValidated"
        @mouseover="mouseOver"
        @mouseout="mouseOut"
        @focus="focus"
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
import FormatData from "@/utils/FormatData";
import Validation from "@/utils/Validation";
import ErrorMessage from "@/constants/EnumErrorMsg";

export default {
  name: "base-input",

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
    value: { type: String, default: "" },
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
  },

  emits: ["onchangeinput"],

  data() {
    let tmp = "container-input__input";

    if (this.hasIcon) {
      tmp = "container-input__input input--icon";
    }

    return {
      currentDate: FormatData.formatDateInput(new Date()),
      className: tmp,
      classNameDefault: tmp,
      classNameHover: tmp + " container-input__input__hover",
      classNameFocus: tmp + " container-input__input__focus",
      isFocus: false,
      isValidated: true,
      title: "",
    };
  },

  methods: {
    /**
     * Thay đổi style khi mouse hover
     * CreatedBy: NHHoang (27/08/2021)
     */
    mouseOver() {
      if (!this.isFocus) {
        this.className = this.classNameHover;
      }
    },

    /**
     * Thay đổi style khi mouse out
     * CreatedBy: NHHoang (27/08/2021)
     */
    mouseOut() {
      if (!this.isFocus) {
        this.className = this.classNameDefault;
      }
    },

    /**
     * Thay đổi style khi focus
     * CreatedBy: NHHoang (27/08/2021)
     */
    focus() {
      this.className = this.classNameFocus;
      this.isFocus = true;
    },

    /**
     * Thay đổi style khi blur
     * CreatedBy: NHHoang (27/08/2021)
     */
    blur() {
      this.isFocus = false;
      this.className = this.classNameDefault;
    },

    /**
     * Xử lý onchangeinput
     * CreatedBy: NHHoang (27/08/2021)
     */
    onChangeInput(event) {
      let tmp = event.target.value;

      this.validate(tmp);

      this.$emit("onchangeinput", { id: this.id, value: tmp });
    },

    /**
     * Validate các trường bắt buộc và đặc biệt (email)
     * CreatedBy: NHHoang (28/08/2021)
     * Modified: NHHoang (28/08/2021)
     */

    validate(value) {
      if (this.required) {
        if (!Validation.validate(this.format, value)) {
          this.isValidated = false;
          this.title = ErrorMessage[this.id];
        } else {
          this.isValidated = true;
          this.title = "";
        }
      } else {
        if (value === null || value.trim() === "") {
          this.isValidated = true;
          this.title = "";
        } else {
          if (this.format === "email") {
            if (!Validation.validateEmail(value)) {
              this.isValidated = false;
              this.title = ErrorMessage[this.id];
            } else {
              this.isValidated = true;
              this.title = "";
            }
          }

          if (this.format === "number") {
            if (!Validation.validateNumber(value)) {
              this.isValidated = false;
              this.title = ErrorMessage[this.id];
            } else {
              this.isValidated = true;
              this.title = "";
            }
          }
        }
      }
    },
  },
};
</script>

<style lang="css">
@import url("../../assets/css/common/Input.css");
</style>
