<template>
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
        <div v-show="totalPage > currentPage + maxVisibleButtons" class="dots">
          ...
        </div>
        <button
          :class="isPageSelected(totalPage)"
          @click="onClickPage(totalPage)"
          v-show="totalPage > currentPage + maxVisibleButtons"
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
          value: 10,
          label: "10 nhân viên/trang",
        },
        {
          value: 20,
          label: "20 nhân viên/trang",
        },
        {
          value: 30,
          label: "30 nhân viên/trang",
        },
        {
          value: 40,
          label: "40 nhân viên/trang",
        },
        {
          value: 50,
          label: "50 nhân viên/trang",
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
@import url("../../assets/css/common/Pagination.css");

input.dx-texteditor-input {
  padding: 6px 0px 6px 12px !important;
  height: 18px !important;
  box-sizing: border-box;
}

.dx-overlay-content.dx-popup-normal.dx-resizable.dx-dropdowneditor-overlay-flipped {
  transform: translate(0px, -162px) !important;
}

.dx-texteditor.dx-editor-outlined {
  border-color: #babec5 !important;
  box-sizing: border-box;
  border-radius: 2px !important;
}

.dx-texteditor.dx-state-focused.dx-editor-outlined {
  border-color: #2ca01c !important;
}

.dx-item.dx-list-item.dx-list-item-selected {
  color: #fff !important;
  background-color: #2ca01c !important;
}

.dx-list-item.dx-state-focused {
  color: #2ca01c !important;
  background-color: #f8f8f8 !important;
}

.dx-item.dx-list-item.dx-state-active {
  color: #fff !important;
  background-color: #2ca01c !important;
}

.dx-item.dx-list-item.dx-state-hover {
  color: #2ca01c !important;
  background-color: #f8f8f8 !important;
}

.dx-overlay-content.dx-popup-normal.dx-resizable.dx-dropdowneditor-overlay-flipped {
  border-radius: 0px !important;
  box-shadow: none !important;
  border: 1px solid #babec5 !important;
}

.dx-dropdowneditor-icon {
  border-radius: 0px !important;
  border: none !important;
}

.dx-dropdowneditor-icon:hover {
  background-color: #e0e0e0 !important;
}

.dx-texteditor-input {
  min-height: 32px !important;
  font-size: 13px !important;
  font-family: "Notosans" !important;
}

.dx-item-content.dx-list-item-content {
  font-family: "Notosans" !important;
}

.dx-item.dx-list-item {
  padding: 0px 14px 0px 10px !important;
}
</style>
