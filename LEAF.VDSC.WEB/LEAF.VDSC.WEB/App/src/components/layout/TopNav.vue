<template>
  <nav class="navbar navbar-default">
    <ul>
      <li><router-link to="Elandmall">Elandmall</router-link></li>
      <li><router-link to="Gmarket">Gmarket</router-link></li>
      <li><router-link to="TMon">TMon</router-link></li>
      <li><router-link to="Wemakeprice">Wemakeprice</router-link></li>
    </ul>
  </nav>
</template>
<script>
  export default {}
</script>
<style scoped>
  ul {
    list-style-type: none;
    padding-left: 0px;
  }
  ul li {
    display: inline;
    border-left: 1px solid #c0c0c0;
    padding: 0px 10px 0px 10px;
    margin: 5px 0px 5px 0px;
  }

</style>
