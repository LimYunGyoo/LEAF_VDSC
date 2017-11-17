<template>
  <div class="content centered">
    <!-- your content here -->
    <router-view></router-view>
  </div>

</template>
<script>
  export default {}
</script>
<style>
  .centered {
    position: relative;
    width: 1200px;
    left: 0;
    right: 0;
    margin-left: auto;
    margin-right: auto;
    margin-bottom: 100px;
  }
</style>
