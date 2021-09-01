<template>
  <tr :id="data.id">
    <td></td>
    <td>
      <input type="checkbox" :checked="checked" @input="$emit('check')" />
    </td>
    <td v-for="col in columns" :key="col.id" :class="col.className">
      {{ formatText(col, data[col.id]) }}
    </td>
    <td class="text text-body-content text--center">
      <div class="func">
        <div class="func-label">
          <div @click="$emit('update')">Sửa</div>
        </div>
        <div class="func-icon" @click="$emit('show-func')" ref="func">
          <div class="icon icon--16 icon-arrow-up--blue"></div>
        </div>
      </div>
    </td>
    <td></td>
  </tr>
</template>

<script>
import FormatData from "@/utils/FormatData.js";

export default {
  name: "tr-cus",
  emits: ["show-func", "check"],
  props: {
    columns: {
      type: Array,
      required: true,
    },
    data: {
      type: Object,
      required: true,
    },
    checked: {
      type: Boolean,
      required: true,
    },
  },

  methods: {
    /**
     * Format dữ liệu
     * * CreatedBy: NHHoang (29/08/2021)
     */
    formatText(col, item) {
      let tmp = item;

      if (col.format === "date") {
        tmp = FormatData.formatDate(tmp);
      }

      return tmp;
    },

    test() {
      console.log(this.data.EmployeeId);
    },
  },
};
</script>

<style lang="css">
.func {
  position: relative;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
}

.func .select {
  display: none;
  position: absolute;
}

.func .func-label {
  color: #0075c0;
  font-weight: 600;
  font-size: 13px;
  height: 36px;
  padding: 6px 0 6px 16px;
  display: flex;
  justify-content: center;
  align-items: center;
  box-sizing: border-box;
  margin-right: 1px;
}

.func .func-label div {
  width: 26px;
  box-sizing: border-box;
  margin-right: 10px;
}

.func .func-icon {
  box-sizing: border-box;
  display: flex;
  justify-content: center;
  align-items: center;
  width: 26px;
}

.func .func-icon .func .line {
  padding: 0 6px;
}

.func .func-icon:active {
  border: 1px solid #0075c0;
}

.func .func-icon:active.select {
  display: block;
}

.func .func-label:active div {
  border: 1px solid #0075c0;
}
</style>
