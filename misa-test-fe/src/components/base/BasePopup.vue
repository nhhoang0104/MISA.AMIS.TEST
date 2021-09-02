<template>
  <div v-show="info.isShowed">
    <div class="modal"></div>
    <div class="pop-up">
      <div class="pop-up__content">
        <div class="icon__msg">
          <div class="icon icon--48" :class="info.icon"></div>
        </div>
        <div class="content__msg">
          {{ info.message }}
        </div>
      </div>
      <div class="pop-up__line"></div>
      <div class="pop-up__footer">
        <div class="left" v-show="info.btnLeft !== null">
          <base-button
            :label="info.btnLeft"
            :secondary="true"
            @click="handleCancel"
          ></base-button>
        </div>
        <div class="right-sec" v-show="info.btnRightSec !== null">
          <base-button
            :label="info.btnRightSec"
            @click="handleAction"
          ></base-button>
        </div>
        <div class="right-first" v-show="info.btnRightFirst !== null">
          <base-button
            :label="info.btnRightFirst"
            :secondary="true"
            @click="$emit('close')"
          ></base-button>
        </div>
        <div class="center" v-show="info.btnCenter">
          <base-button
            :label="info.btnCenter"
            @click="$emit('close')"
          ></base-button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "base-popup",

  props: {
    info: {
      type: Object,
      required: true,
    },
  },

  emits: ["close"],

  watch: {},

  data() {
    return {
      className: "pop-up pop-up--hidden",
    };
  },

  methods: {
    /**
     * Khi ấn nút done dẽ thực hiện hành động đã được truyền vào
     * CreatedBy: NHHoang (29/08/2021)
     */
    handleAction() {
      this.$emit("close");
      if (this.info.action !== null) this.info.action();
    },

    /*
      Khi ấn nút cancel sẽ không thực hành động đẫ được truyền vào
    */
    handleCancel() {
      this.$emit("close");
      if (this.info.cancel !== null) this.info.cancel();
    },
  },
};
</script>

<style lang="css">
@import url("../../assets/css/common/Popup.css");
</style>
