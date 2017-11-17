<template>
  <div>
    <product-table getUrl="http://localhost:3222/api/search/wemakeprice?discount="></product-table>
  </div>
</template>
<script>
  import ProductTable from '../common/ProductTable.vue'
  export default {
    components: {
      ProductTable
    }
  }
</script>
<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
