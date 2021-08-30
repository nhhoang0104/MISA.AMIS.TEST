<template>
  <div>
    <div class="flex" v-show="hasLabel">
      <div class="label">{{ label }}</div>
      <div class="label-required" v-show="required">&nbsp;*</div>
    </div>
    <div class="ms-input">
      <input
        :class="className"
        :required="required"
        :type="type"
        :value="value"
        :title="title"
        :placeholder="placeholder"
        :max="`${type === 'date' ? currentDate : null}`"
        @mouseover="mouseOver"
        @mouseout="mouseOut"
        @focus="focus"
        @blur="blur"
        @input="onChangeInput"
      />
      <div
        class="ms-input__icon icon icon--16"
        :class="iconName"
        v-show="hasIcon"
      ></div>
    </div>
  </div>
</template>

<script>
import FormatData from "@/utils/FormatData";

export default {
  name: "base-input",

  props: {
    id: { type: String, required: false },

    type: {
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
    title: {
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
    let tmp = "ms-input__input";

    if (this.hasIcon) {
      tmp = "ms-input__input input--icon";
    }

    return {
      currentDate: FormatData.formatDateInput(new Date()),
      className: tmp,
      classNameDefault: tmp,
      classNameHover: tmp + " ms-input__input__hover",
      classNameFocus: tmp + " ms-input__input__focus",
      isFocus: false,
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

      this.$emit("onchangeinput", { id: this.id, value: tmp });
    },
  },
};
</script>

<style lang="css">
@import url("../../assets/css/common/Input.css");
</style>
