<template>
  <div>
    <div class="flex" v-show="hasLabel">
      <div class="label">{{ label }}</div>
      <div class="label-required" v-show="required">&nbsp;*</div>
    </div>
    <div
      class="combo-box"
      :class="isShowed ? 'combo-box--active' : ''"
      :validate="isValidated"
      @blur="outFocusInput"
    >
      <input
        type="text"
        @focus="onFocusInput"
        v-model="textSearch"
        @blur="outFocusInput"
        :tabIndex="tabIndex"
      />
      <div class="combo-box__toggle" @click="show">
        <div class="icon icon--16 icon-arrow-dropdown"></div>
      </div>
      <div class="combo-box__select" v-show="isShowed">
        <div class="flex title">
          <div class="w-30 p-l--10 p-r--10">Mã đơn vị</div>
          <div class="w-70 p-l--10 p-r--10">Tên đơn vị</div>
        </div>
        <slot name="combo-box-options" :options="dataClone"></slot>
        <div v-show="dataClone.length === 0 ? true : false" class="no-content">
          Không có dữ liệu
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import _ from "lodash";

export default {
  name: "combo-box",

  props: {
    label: {
      type: String,
      required: true,
    },
    required: {
      type: Boolean,
      default: false,
    },
    hasLabel: {
      type: Boolean,
      default: false,
    },
    value: {
      type: String,
    },
    id: {
      type: String,
      required: true,
    },
    data: {
      type: Array,
      required: true,
    },
    tabIndex: {
      type: Number,
      required: false,
    },
  },

  provide() {
    return {
      pkey: this.$props.id,
    };
  },

  data() {
    return {
      isShowed: false,
      dataClone: _.cloneDeep(this.data),
      textSearch: "",
      valueSelected: "",
      isValidated: true,
    };
  },

  created() {
    /**
     * click ouside
     * CreateBy: NHHoang (01/09/2021)
     * */
    window.addEventListener("click", (e) => {
      if (!this.$el.contains(e.target)) {
        this.isShowed = false;
      }
    });
  },

  watch: {
    /**
     * Xử lý style combobox khi đóng(hoặc mở) combobox
     * CreatedBy: NHHoang (29/08/2021)
     */
    isShowed(value) {
      if (value) {
        this.dataClone = _.cloneDeep(this.data);
      }
    },

    data: {
      deep: true,
      handler(newVal) {
        this.dataClone = newVal;
        let tmp = this.dataClone.find((item) => item.id === this.value);

        if (tmp) this.textSearch = tmp.label;
      },
    },

    /**
     * Lấy label theo props.value từ dataClone
     * CreatedBy: NHHoang (29/08/2021)
     */
    value: {
      immediate: true,
      handler(newVal) {
        this.validate(newVal);
        let tmp = this.dataClone.find((item) => item.id === newVal);
        if (tmp) this.textSearch = tmp?.label;
        else this.textSearch = null;

        if (this.isShowed === true) this.isShowed = false;
      },
    },

    /**
     * search
     * CreatedBy: NHHoang (29/08/2021)
     */
    textSearch(newVal) {
      const text = _.trim(newVal);

      if (text === "" || text === undefined || text === null) {
        this.dataClone = _.cloneDeep(this.data);
      } else {
        this.dataClone = _.filter(
          this.data,
          (item) => item.label.toLowerCase().indexOf(text.toLowerCase()) > -1
        );
      }
    },
  },

  computed: {
    /*
      Tìm kiếm các option theo text search từ dataClone.
    */
    getOptions() {
      const text = _.trim(this.textSearch);

      if (text === "" || text === undefined || text === null) {
        return _.cloneDeep(this.dataClone);
      } else {
        return _.filter(
          this.dataClone,
          (item) => item.label.toLowerCase().indexOf(text.toLowerCase()) > -1
        );
      }
    },
  },

  methods: {
    /**
     * xử lý sự kiện đóng hoặc mở combo-box
     * CreatedBy: NHHoang (29/08/2021)
     */
    show() {
      this.isShowed = !this.isShowed;
    },

    /**
     * xử lý outfocus vào combobox
     * CreatedBy: NHHoang (29/08/2021)
     */
    outFocus() {
      this.isShowed = false;
    },

    /**
     * xử lý outfocus vào input
     * CreatedBy: NHHoang (29/08/2021)
     */
    outFocusInput() {
      let tmp = this.dataClone.find((item) => item.id === this.value);
      this.textSearch = tmp.label;

      setTimeout(() => {
        this.isShowed = false;
      }, 100);
    },

    /**
     * xử lý focus vào input
     * CreatedBy: NHHoang (29/08/2021)
     */
    onFocusInput() {
      this.isShowed = true;
    },

    /**
     * validate du lieu
     * CreatedBy: NHHoang (29/08/2021)
     */
    validate(value = null) {
      let tmp = value === null ? this.value : value;
      if (this.required) {
        if (tmp === null) this.isValidated = false;
        else this.isValidated = true;
      }
    },
  },
};
</script>

<style lang="css" scoped>
@import url("../../../assets/css/common/ComboBox.css");
</style>
