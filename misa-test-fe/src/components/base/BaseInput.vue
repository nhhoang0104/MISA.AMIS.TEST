<template lang="html">
  <div>
    <div class="flex" v-show="hasLabel">
      <div class="label">{{ label }}</div>
      <div class="label-required" v-show="required">&nbsp;*</div>
    </div>
    <div class="ms-input">
      <div></div>
      <input
        :class="className"
        :required="required"
        type="text"
        :title="title"
        :placeholder="placeholder"
        @mouseover="mouseOver"
        @mouseout="mouseOut"
        @focus="focus"
        @blur="blur"
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
export default {
  name: "base-input",

  props: {
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

  data() {
    let tmp = "ms-input__input";

    if (this.hasIcon) {
      tmp = "ms-input__input input--icon";
    }

    return {
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
  },
};
</script>

<style lang="css">
@import url("../../assets/css/common/Input.css");
</style>
