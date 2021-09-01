<template>
  <thead>
    <tr>
      <th></th>
      <th>
        <input
          type="checkbox"
          :checked="checked"
          @input="$emit('select-all', !checked)"
        />
      </th>
      <th v-for="col in columns" :key="col.id" :class="col.className">
        {{ col.label }}
      </th>
      <th class="text text-body-content text--center">CHỨC NĂNG</th>
      <th></th>
    </tr>
  </thead>
</template>

<script>
export default {
  name: "base-table-head",
  props: {
    columns: {
      type: Array,
      required: true,
    },
    checked: {
      type: Boolean,
      required: true,
    },
  },

  emits: ["select-all"],
};
</script>

<style lang="css"></style>
