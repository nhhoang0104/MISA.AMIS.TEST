<template>
  <div>
    <div class="modal"></div>
  </div>
</template>

<script>
import ClipLoader from "vue-spinner/src/ClipLoader.vue";

export default {
  name: "base-loader",
  components: {
    "clip-loader": ClipLoader,
  },
  props: {
    isLoading: {
      type: Boolean,
      required: true,
    },
  },
};
</script>

<style lang="css">
@import url("../../css/common/Loader.css");
</style>
