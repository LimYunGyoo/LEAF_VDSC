<template>
  <div>
    <select class="selectpicker" v-model="selected" @change="changeDiscount(selected)">
      <option disabled value="">Please select one</option>
      <option value="50">50%</option>
      <option value="60">60%</option>
      <option value="70">70%</option>
      <option value="80">80%</option>
      <option value="90">90%</option>
    </select>
    <div id="table">

      <div class="row header">
        <span class="cell col01">Discount</span>
        <span class="cell col02">Detail</span>
      </div>
      <div class="row" v-for="product in products">
        <span class="cell col01">{{ product.discount }}%</span>
        <span class="cell col02">{{ product.detail }}</span>
      </div>
    </div>
</div>
</template>
<script>
  export default {
    data() {
      return {
        products: null,
        selected: null
      }
    },
    methods: {
      getElandmallProducts(discount) {
        console.log(this.getUrl);
        this.$http.get(window.baseUrl + this.getUrl + discount).then(response => {
          // get body data
          this.products = response.body;
        }, response => {
          // error callback
        });
      },
      changeDiscount(discount) {
        this.getElandmallProducts(discount)
      }
    },
    mounted: function () {
      this.selected = 50;
      this.getElandmallProducts(this.selected);
    },
    props: {
      getUrl: {
        type: String,
        default() { return this.getUrl; }
      }
    }
  }
</script>
<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  #table {
    display: table;
    width: 100%;
    font-size: 10pt;
  }

  .row {
    display: table-row;
    width: auto;
  }

  .header {
    font-weight: bold;
  }

  .cell {
    display: table-cell;
    padding: 4px;
    border-bottom: 1px solid #DDD;
  }

  .col01 {
    width: 20%;
  }

  .col02 {
    width: 80%;
  }
</style>
