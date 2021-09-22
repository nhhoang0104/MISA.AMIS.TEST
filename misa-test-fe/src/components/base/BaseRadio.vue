<template>
  <DxRadioGroup
    :dataSource="dataSrc"
    :value="value"
    :displayExpr="displayExpr"
    :valueExpr="valueExpr"
    :layout="layout"
    @value-changed="onValueChange"
    class="misa-radio"
  />
</template>
<script>
import DxRadioGroup from "devextreme-vue/radio-group";
export default {
  name: "base-radio",

  components: {
    DxRadioGroup,
  },

  props: {
    id: {
      type: String,
      default: "",
    },
    dataSrc: {
      type: Array,
      default: null,
    },
    value: {
      type: [String, Number, Boolean],
      default: null,
    },
    displayExpr: {
      type: String,
      required: true,
    },
    valueExpr: {
      type: String,
      required: true,
    },
    layout: {
      type: String,
      default: "vertical",
    },
  },

  emits: ["select-item"],

  methods: {
    onValueChange({ value }) {
      this.$emit("select-item", {
        id: this.id,
        value,
      });
    },
  },
};
</script>
<style lang="scss">
.misa-radio {
  .dx-radiobutton {
    &.dx-state-focused:not(.dx-state-active) {
      .dx-radiobutton-icon {
        &::before {
          border: 1px solid var(--color-btn-primary) !important;
        }
      }
    }

    &.dx-state-hover {
      .dx-radiobutton-icon {
        &::before {
          border: 1px solid var(--color-btn-primary) !important;
        }
      }
    }
  }

  .dx-radiobutton-icon-dot {
    background-color: var(--color-btn-primary) !important;
  }
}
</style>
