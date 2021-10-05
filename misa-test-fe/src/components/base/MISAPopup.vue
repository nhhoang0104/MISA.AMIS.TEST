<template>
  <DxPopup
    v-if="type === 1"
    :visible="isShowed"
    :width="width"
    :max-width="maxWidth"
    :height="height"
    :max-height="maxHeight"
    :show-title="hasTitle"
    :show-close-button="false"
    :close-on-outside-click="
      clickOutsideEnable ? closeOnOutsideClick : clickOutsideEnable
    "
    :resize-enabled="resizeEnabled"
    :drag-enabled="dragEnabled"
    title-template="title"
    content-template="content"
    class="misa-popup"
    accessKey="misa-popup"
  >
    <template #title>
      <div><slot name="popup-title"></slot></div>
    </template>
    <DxScrollView
      width="100%"
      :height="`calc(100% - ${heightFooter}px)`"
      :disabled="!scrollEnabled"
    >
      <slot name="popup-content"></slot>
    </DxScrollView>

    <div class="popup__footer" :style="`height: ${heightFooter}px`">
      <slot name="popup-footer"></slot>
    </div>
  </DxPopup>
  <DxPopover
    v-else
    :visible="isShowed"
    :width="width"
    :max-width="maxWidth"
    :height="height"
    :max-height="maxHeight"
    :show-title="hasTitle"
    :show-close-button="false"
    :close-on-outside-click="
      clickOutsideEnable ? closeOnOutsideClick : clickOutsideEnable
    "
    :position="position"
    :drag-enabled="dragEnabled"
    :resize-enabled="resizeEnabled"
    :target="target"
    title-template="title"
    content-template="content"
    class="misa-popup"
    accessKey="misa-popup"
  >
    <template #title>
      <div><slot name="popup-title"></slot></div>
    </template>
    <DxScrollView
      width="100%"
      :height="`calc(100% - ${heightFooter}px)`"
      :disabled="!scrollEnabled"
    >
      <slot name="popup-content"></slot>
    </DxScrollView>

    <div class="popup__footer" :style="`height: ${heightFooter}px`">
      <slot name="popup-footer"></slot>
    </div>
  </DxPopover>
</template>

<script>
import { DxPopup } from "devextreme-vue/popup";
import { DxScrollView } from "devextreme-vue/scroll-view";
import { DxPopover } from "devextreme-vue/popover";
export default {
  name: "misa-popup",
  components: {
    DxPopup,
    DxPopover,
    DxScrollView,
  },

  props: {
    type: {
      type: Number,
      default: 1,
    },
    isShowed: {
      type: Boolean,
      required: true,
    },
    width: {
      type: [Number, String],
      default: null,
    },
    height: {
      type: [Number, String],
      default: null,
    },
    maxWidth: {
      type: [Number, String],
      default: null,
    },
    maxHeight: {
      type: [Number, String],
      default: null,
    },
    heightFooter: {
      type: Number,
      default: 0,
    },
    hasTitle: {
      type: Boolean,
      default: false,
    },
    hasFooter: {
      type: Boolean,
      default: false,
    },
    scrollEnabled: {
      type: Boolean,
      default: false,
    },
    dragEnabled: {
      type: Boolean,
      default: false,
    },
    resizeEnabled: {
      type: Boolean,
      default: false,
    },
    clickOutsideEnable: {
      type: Boolean,
      default: false,
    },
    target: {
      type: String,
      default: "",
    },
    position: {
      type: String,
      default: "bottom",
    },
  },

  emits: ["close-form"],

  data() {
    return {};
  },

  methods: {
    /**
     * Xử lý click outside popup
     * CreatedBy: nhhoang(20/09/2021)
     */
    closeOnOutsideClick(e) {
      let target = e.target.children[0];
      if (target) {
        if (target.getAttribute("accesskey") === "misa-popup") {
          this.$emit("close-form");
          return true;
        }
      }

      return false;
    },
  },
};
</script>

<style lang="scss">
.misa-popup {
  .dx-overlay-content {
    border: 0px !important;
  }

  &.dx-overlay-shader {
    background-color: rgba(0, 0, 0, 0.5);
  }

  .dx-popup-title {
    padding: 0;
    border-bottom: 0px;
  }

  .dx-popup-content {
    padding: 0px;
    position: relative;
  }

  .popup__footer {
    position: absolute;
    bottom: 0;
    left: 0;
    width: 100%;
  }
}
</style>
