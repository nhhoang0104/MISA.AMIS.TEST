<template>
  <div>
    <div class="modal"></div>
    <div class="pop-up">
      <div class="pop-up__content">
        <div class="icon__msg">
          <div class="icon icon--48 icon-waring"></div>
        </div>
        <div class="content__msg">
          Bạn có thứ sự muốn xóa Nhân viên NV17129 không ?
        </div>
      </div>
      <div class="pop-up__line"></div>
      <div class="pop-up__footer">
        <!-- <div class="left">
          <base-button label="Hủy" :secondary="true"></base-button>
        </div>
        <div class="right-sec"><base-button label="Có"></base-button></div>
        <div class="right-first">
          <base-button label="Không" :secondary="true"></base-button>
        </div> -->
        <div class="center"><base-button label="Đóng"></base-button></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "base-popup",

  props: {
    popup: {
      type: Object,
      required: true,
    },

    action: {},
  },

  emits: ["show"],

  watch: {
    // đóng mở popup
    popup: {
      deep: true,
      immediate: true,
    },
  },

  data() {
    return {
      className: "pop-up pop-up--hidden",
    };
  },

  methods: {
    /*
      Khi ấn nút done dẽ thực hiện hành động đã được truyền vào
    */
    handleAction() {
      this.$emit("show", false);
      this.popup.action();
    },

    /*
      Khi ấn nút cancel sẽ không thực hành động đẫ được truyền vào
    */
    handleCancel() {
      this.$emit("show", false);
      this.popup.cancel();
    },
  },
};
</script>

<style lang="css">
@import url("../../assets/css/common/Popup.css");
</style>
