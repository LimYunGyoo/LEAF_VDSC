<template>
  <div class="wrapper">
    <img src="../../assets/images/logo.png">
    <top-nav></top-nav>
    <center-content></center-content>
  </div>
</template>

<script>
  import TopNav from './TopNav.vue'
  import CenterContent from './CenterContent.vue'
  export default {
    components: {
      TopNav,
      CenterContent
    }
  }

</script>
