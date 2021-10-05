<template>
  <div class="pagination">
    <div class="pagination__left">
      <div class="text">
        Tổng số:
        <b>{{ totalRecord }}</b> bản ghi
      </div>
    </div>
    <div class="pagination__right">
      <div class="page-size">
        <DxSelectBox
          :value="pageSize"
          :data-source="data"
          value-expr="value"
          display-expr="label"
          @value-changed="selectSize"
          class="misa-select-box"
        >
        </DxSelectBox>
      </div>
      <div class="page-index">
        <button
          class="prev"
          :disabled="currentPage === 1"
          @click="onClickPrevPage"
        >
          Trước
        </button>
        <div class="flex index">
          <button
            :class="isPageSelected(1)"
            @click="onClickPage(1)"
            v-show="currentPage >= maxVisibleButtons"
          >
            1
          </button>
          <div v-show="currentPage >= maxVisibleButtons + 1" class="dots">
            ...
          </div>
          <button
            v-for="page in pages"
            :key="page"
            :class="isPageSelected(page)"
            @click="onClickPage(page)"
          >
            {{ page }}
          </button>
          <div
            v-show="totalPage >= currentPage + maxVisibleButtons"
            class="dots"
          >
            ...
          </div>
          <button
            :class="isPageSelected(totalPage)"
            @click="onClickPage(totalPage)"
            v-show="totalPage >= currentPage + maxVisibleButtons - 1"
          >
            {{ totalPage }}
          </button>
        </div>
        <button
          class="next"
          :disabled="currentPage === totalPage"
          @click="onClickNextPage"
        >
          Sau
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import { DxSelectBox } from "devextreme-vue/select-box";

export default {
  components: { DxSelectBox },
  name: "base-pagination",

  props: {
    currentPage: {
      type: Number,
      required: true,
    },
    totalRecord: {
      type: Number,
      required: true,
    },
    totalPage: {
      type: Number,
      required: true,
    },
    pageSize: {
      type: Number,
      required: true,
    },
  },

  emits: ["select-page", "select-size"],

  data() {
    return {
      maxVisibleButtons: 3,

      data: [
        {
          value: 100,
          label: "100 nhân viên/trang",
        },
        {
          value: 200,
          label: "200 nhân viên/trang",
        },
        {
          value: 300,
          label: "300 nhân viên/trang",
        },
        {
          value: 400,
          label: "400 nhân viên/trang",
        },
        {
          value: 500,
          label: "500 nhân viên/trang",
        },
      ],

      indexTmp: Math.floor(this.currentPage / 3),
    };
  },

  computed: {
    /**
     * tao index dau
     * CreatedBy: NHHoang (29/08/2021)
     */
    startPage: function() {
      if (this.currentPage === 1) {
        return 1;
      }

      if (this.currentPage === this.totalPage) {
        return this.totalPage - this.maxVisibleButtons + 1;
      }

      return this.currentPage - 1 > 0 ? this.currentPage - 1 : 1;
    },

    /**
     * tạo index cuối
     * CreatedBy: NHHoang (29/08/2021)
     */
    endPage: function() {
      return Math.min(
        this.startPage + this.maxVisibleButtons - 1,
        this.totalPage
      );
    },

    /**
     * tạo các index từ index đầu(startPage) đến index cuối(endPage)
     * CreatedBy: NHHoang (29/08/2021)
     */
    pages: function() {
      const range = [];

      for (let i = this.startPage; i <= this.endPage; i += 1) {
        range.push(i);
      }

      if (range.length === 0) return [1];

      return range;
    },
  },

  methods: {
    /**
     * Chọn index trang ngẫu nhiễn từ range
     * CreatedBy: NHHoang (29/08/2021)
     */
    onClickPage(page = 1) {
      this.$emit("select-page", page);
    },

    /**
     * chọn index trang tiếp theo của index hiện tại
     * CreatedBy: NHHoang (29/08/2021)
     */

    onClickNextPage() {
      this.$emit("select-page", this.currentPage + 1);
    },

    /**
     * chọn index trang phía sau index hiện tại
     * CreatedBy: NHHoang (29/08/2021)
     */
    onClickPrevPage() {
      this.$emit("select-page", this.currentPage - 1);
    },

    /**
     * xét index trang chọn thì class selected
     * CreatedBy: NHHoang (29/08/2021)
     */
    isPageSelected: function(page) {
      if (this.currentPage === page) {
        return "selected";
      }

      return "";
    },

    /**
     * chọn kích cỡ trang
     * CreatedBy: NHHoang (29/08/2021)
     */
    selectSize(e) {
      this.$emit("select-size", e.value);
    },
  },
};
</script>

<style lang="css">
@import url("../../assets/css/common/pagination.css");
</style>
