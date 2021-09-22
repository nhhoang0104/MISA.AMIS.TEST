<template>
  <DxSelectBox
    :dataSource="dataSrc"
    :displayExpr="displayExpr"
    :valueExpr="valueExpr"
    :value="value"
    @value-changed="onValueChange"
    class="misa-select-box"
  />
</template>

<script>
import DxSelectBox from "devextreme-vue/select-box";
export default {
  name: "base-select-box",
  components: {
    DxSelectBox,
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
.misa-select-box {
  &.dx-texteditor {
    &.dx-state-hover {
      border-color: #2ca01c;
    }

    &.dx-state-focused {
      border-color: #2ca01c !important;
    }

    &.dx-state-active {
      border-color: #2ca01c !important;
    }

    input {
      color: #111;
      font-family: "Notosans";
    }
  }
}
.dx-overlay-wrapper {
  .dx-overlay-content {
    .dx-popup-content {
      .dx-item.dx-list-item {
        &.dx-state-focused,
        &.dx-state-hover,
        &.dx-state-active,
        &.dx-list-item-selected {
          background-color: #2ca01c !important;

          .dx-item-content {
            color: #fff;
            font-family: "Notosans";
          }
        }

        .dx-item-content {
          color: #111;
          font-family: "Notosans";
        }
      }
    }
  }
}
</style>
