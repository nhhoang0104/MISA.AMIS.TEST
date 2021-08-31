<template>
  <transition name="slide">
    <div
      class="toast"
      :class="{
        'toast--success': toast.type == Resource.ToastType.Success,
        'toast--warning': toast.type == Resource.ToastType.Warning,
        'toast--error': toast.type == Resource.ToastType.Error,
      }"
      :style="{ top: `${10 + ((index * 48 + 5 * index) % 540)}px` }"
      v-show="isShowed"
    >
      <div class="toast__icon">
        <div
          class="icon icon--24"
          :class="{
            'icon-toast-success': toast.type == Resource.ToastType.Success,
            'icon-toast-warning': toast.type == Resource.ToastType.Warning,
            'icon-toast-error': toast.type == Resource.ToastType.Error,
          }"
        ></div>
      </div>
      <div class="toast__message">{{ toast.message }}</div>
    </div>
  </transition>
</template>

<script>
import Resource from "@/constants/Resource";
export default {
  name: "base-toast-message",

  props: {
    toast: {
      type: Object,
      required: true,
    },
    index: {
      type: Number,
      required: true,
    },
  },

  data() {
    return {
      isShowed: false,
      Resource: Resource,
    };
  },

  created() {
    this.showToast();
  },

  methods: {
    showToast() {
      this.isShowed = true;
      setTimeout(() => {
        this.isShowed = false;
      }, 3000);
    },
  },
};
</script>

<style lang="css">
@import url("../../assets/css/common/Toast.css");

.slide-leave-active,
.slide-enter-active {
  transition: all 0.3s;
}

.slide-enter,
.slide-leave-to {
  transform: translateX(100%);
  opacity: 0;
}
</style>
