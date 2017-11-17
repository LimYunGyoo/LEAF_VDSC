<template>
  <div>
    <product-table getUrl="http://52.78.116.4:8000/api/search/elandmall?discount="></product-table>
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
