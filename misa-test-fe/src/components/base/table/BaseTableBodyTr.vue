<template>
  <tr :id="data.id" :checked="checked">
    <td></td>
    <td>
      <div class="container">
        <input type="checkbox" :checked="checked" />
        <span class="checkmark"></span>
      </div>
    </td>
    <td v-for="col in columns" :key="col.id" :class="col.className">
      {{ data[col.id] }}
    </td>
    <td></td>
    <td></td>
  </tr>
</template>

<script>
export default {
  name: "base-tr",

  props: {
    columns: {
      type: Array,
      required: true,
    },

    data: {
      type: Object,
      required: true,
    },

    index: {
      type: Number,
      required: true,
    },

    checked: {
      type: Boolean,
      required: true,
    },
  },

  methods: {},
};
</script>

<style lang="css"></style>
