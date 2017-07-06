<template>
  <div class="footer" v-if="show !== '/Login'">
    2017 .Net课设 夏陈
  </div>
</template>

<script>
  export default {
    data () {
      return {
        show: ''
      }
    },
    mounted () {
      this.show = this.$route.path
    },
    watch: {
      '$route': function (to, from) {
        this.show = to.path
      }
    }
  }
</script>

<style scoped>
  .footer {
    text-align: center;
    padding: 10px 0 20px;
    color: #9ea7b4;
  }
</style>
