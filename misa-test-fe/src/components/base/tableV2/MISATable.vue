<template>
  <div>
    <DxDataGrid
      :data-source="dataSource"
      class="misa-table"
      :show-borders="false"
      :show-column-lines="true"
      :show-row-lines="true"
      :hover-state-enabled="true"
      noDataText=""
      key-expr="EmployeeId"
      @rowDblClick="editData"
      :allow-column-resizing="true"
      :column-resizing-mode="currentMode"
      :column-min-width="50"
      :column-auto-width="true"
      :focused-row-enabled="true"
      v-model:focused-row-key="focusedRowKey"
    >
      <DxColumnChooser :enabled="false" />
      <DxPaging :enabled="false" />
      <DxSorting mode="none" />
      <DxColumnFixing :enabled="true" />
      <DxScrolling
        :scroll-by-content="true"
        :scroll-by-thumb="true"
        mode="virtual"
      />
      <DxSelection show-check-boxes-mode="none" mode="none" />

      <DxColumn
        caption=""
        :fixed="true"
        width="40"
        cell-template="checkboxCell"
        header-cell-template="checkboxHeader"
      />

      <DxColumn
        v-for="column in columns"
        :key="column['ColId']"
        :caption="column['ColName']"
        :data-field="column['ColId']"
        :fixed="column['ColFixed']"
        :width="column['ColWidth']"
        :alignment="column['ColAlignment']"
        :fixedPosition="column['ColPosFixed']"
        :cell-template="column['ColCellTemplate']"
      />

      <template #checkboxHeader>
        <div>
          <input
            type="checkbox"
            :checked="isSelectedAll"
            @input="$emit('select-all', !isSelectedAll)"
          />
        </div>
      </template>

      <template #checkboxCell="{data}">
        <div>
          <input
            type="checkbox"
            :checked="
              employeeDeleteList.indexOf(data.key) === -1 ? false : true
            "
            @input="$emit('check-box', data.key)"
          />
        </div>
      </template>

      <template #functionCell="{data}">
        <div class="misa-table__func-cell">
          <div
            class="misa-table__func-cell--text"
            @click="$emit('update', data.key)"
          >
            Sửa
          </div>
          <DxSelectBox
            class="misa-table__dropdown"
            item-template="comboboxItem"
            :items="boxFunc"
            @value-changed="onValueChanged($event, data.key)"
            drop-down-button-template="dropdownIcon"
          >
            <template #comboboxItem="{ data: boxFunc }">
              <div class="misa-table__dropdown-item">
                {{ boxFunc["itemName"] }}
              </div>
            </template>
            <template #dropdownIcon>
              <div class="icon icon--16 icon-arrow-up--blue"></div>
            </template>
          </DxSelectBox>
        </div>
      </template>
    </DxDataGrid>
    <base-pagination
      v-if="pagingEnabled"
      :currentPage="currentPage"
      :pageSize="pageSize"
      :totalRecord="totalRecord"
      :totalPage="totalPage"
      @select-page="selectPage"
      @select-size="selectPageSize"
    ></base-pagination>
  </div>
</template>

<script>
import {
  DxColumn,
  DxColumnFixing,
  DxDataGrid,
  DxPaging,
  DxSorting,
  DxSelection,
  DxScrolling,
  DxColumnChooser,
} from "devextreme-vue/data-grid";

import { DxSelectBox } from "devextreme-vue/select-box";
import EnumBoxFunc from "@/constants/EnumBoxFunc";
import Resource from "@/constants/resource";

export default {
  name: "misa-table",

  components: {
    DxDataGrid,
    DxColumn,
    DxColumnFixing,
    DxPaging,
    DxSorting,
    DxSelection,
    DxScrolling,
    DxSelectBox,
    DxColumnChooser,
  },

  props: {
    dataSource: {
      type: Array,
    },
    columns: {
      type: Array,
      required: true,
    },
    employeeDeleteList: {
      type: Array,
    },
    isSelectedAll: {
      type: Boolean,
      default: false,
    },
    pagingEnabled: {
      type: Boolean,
      default: false,
    },
    currentPage: {
      type: Number,
      default: 0,
    },
    totalRecord: {
      type: Number,
      default: 0,
    },
    totalPage: {
      type: Number,
      default: 0,
    },
    pageSize: {
      type: Number,
      default: 0,
    },
  },

  emits: [
    "update",
    "check-box",
    "select-all",
    "replica",
    "delete",
    "select-page",
    "select-size",
  ],

  data() {
    return {
      boxFunc: EnumBoxFunc,
      actionType: Resource.FormMode,
      resizingModes: ["nextColumn", "widget"],
      currentMode: "nextColumn",
      focusedRowKey: null,
    };
  },

  methods: {
    editData(data) {
      this.$emit("update", data.key);
    },

    onValueChanged({ value }, id) {
      if (value.itemId === this.actionType.Delete) {
        this.$emit("delete", id);
      }

      if (value.itemId === this.actionType.Replica) {
        this.$emit("replica", id);
      }
    },

    selectPage(page) {
      this.$emit("select-page", page);
    },

    selectPageSize(size) {
      this.$emit("select-size", size);
    },
  },
};
</script>

<style lang="scss">
.misa-table {
  font-family: "NotoSans", sans-serif !important;
  font-size: 13px !important;
  max-height: calc(100vh - 267px);
  padding-right: 30px !important;
  padding-left: 20px !important;

  .dx-datagrid-table {
    border: none;
  }

  .dx-datagrid-headers {
    border-bottom: none;

    .dx-datagrid-content {
      margin-bottom: 0;
    }

    .dx-header-row {
      background-color: #eceef1;

      td {
        height: 34px !important;
        color: #111;
        padding: 0px 10px !important;
        border-color: #c7c7c7 !important;
        box-sizing: border-box !important;

        .dx-datagrid-text-content {
          padding-right: 12px;
          font-family: "Notosans-Bold", sans-serif !important;
          margin-top: 8.5px;
          border-left: 0 !important;
        }

        &:first-child {
          & .dx-datagrid-text-content {
            margin-top: 2.5px;
          }
        }

        &:last-child {
          border-left: 1px solid;
          border-right: none !important;
          border-color: #c7c7c7 !important;
        }
      }
    }
  }

  .dx-datagrid-rowsview {
    border-top: 0 !important;

    .dx-datagrid-content {
      margin-bottom: 0;

      .dx-datagrid-table {
        .dx-column-lines {
          border: none;

          td {
            height: 47px !important;
            box-sizing: border-box !important;
            color: #111;
            padding: 0px 10px !important;
            border-color: #c7c7c7 !important;
            padding: 14px 10px 5px 10px !important;
            border-bottom: 1px solid;
            border-right: 1px dotted;
            border-left: 1px dotted;

            &:first-child {
              padding: 9px 10px 5px 10px !important;
            }

            &:last-child {
              border-left: 1px dotted #c7c7c7 !important;
              border-right: none !important;
            }
          }
        }
      }
    }
  }

  .dx-datagrid-borders .dx-datagrid-rowsview,
  .dx-datagrid-headers + .dx-datagrid-rowsview {
    border-top: none;
  }

  .dx-pointer-events-none {
    border: none !important;
  }

  .dx-datagrid-rowsview
    .dx-row-focused.dx-data-row
    .dx-command-edit:not(.dx-focused)
    .dx-link,
  .dx-datagrid-rowsview .dx-row-focused.dx-data-row > td:not(.dx-focused),
  .dx-datagrid-rowsview .dx-row-focused.dx-data-row > tr > td:not(.dx-focused) {
    background-color: #f8f8f8;
  }

  .dx-datagrid-content .dx-datagrid-table .dx-row > td:first-child,
  .dx-datagrid-content .dx-datagrid-table .dx-row > tr > td:first-child {
    & .dx-datagrid-text-content {
      display: flex;
      align-items: center;
      justify-content: center;
    }
  }

  .dx-datagrid-rowsview .dx-selection.dx-row:not(.dx-row-focused) > td,
  .dx-datagrid-rowsview .dx-selection.dx-row:not(.dx-row-focused) > tr > td,
  .dx-datagrid-rowsview .dx-selection.dx-row:not(.dx-row-focused):hover > td,
  .dx-datagrid-rowsview
    .dx-selection.dx-row:not(.dx-row-focused):hover
    > tr
    > td {
    background-color: #f3f8f8;
    color: #111;
    border-bottom-color: #babec5 !important;
  }

  &__func-cell {
    width: auto;
    display: flex;
    gap: 10px;
    align-items: center;
    justify-content: center;
    &--text {
      color: #0075c0;
      font-family: "NotoSans-SemiBold", sans-serif !important;
      cursor: pointer;
    }
  }
}

.misa-table {
  .dx-texteditor-input-container {
    display: none;
  }

  .dx-texteditor-buttons-container {
    width: 26px;
    height: 16px;
    .dx-button-normal.dx-button-mode-contained.dx-widget.dx-dropdowneditor-button {
      display: flex;
      justify-content: center;
      align-items: center;
      border: 0px;
    }
  }

  .dx-texteditor.dx-editor-outlined {
    background: transparent;
    border: none;
    border-radius: 0;
  }

  .dx-dropdowneditor-button {
    width: 26px;
    min-width: 26px;
  }
}

.dx-dropdownlist-popup-wrapper.dx-popup-wrapper .dx-overlay-content {
  width: auto !important;
}

.misa-table__dropdown-item {
  width: 120px !important;
}
</style>
