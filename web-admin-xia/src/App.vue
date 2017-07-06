<template>
  <div id="app">
    <Bar></Bar>
    <div class="main-container">
      <router-view></router-view>
    </div>
    <Footer></Footer>
  </div>
</template>

<script>
  import Bar from './components/Bar/Bar.vue'
  import Footer from './components/Footer/Footer.vue'

  export default {
    name: 'app',
    components: {
      Bar,
      Footer
    }
  }
</script>

<style>
  #app {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    height: 100%;
  }
  html, body, div, span, applet, object, iframe,
  h1, h2, h3, h4, h5, h6, p,
  blockquote, pre, a, abbr, acronym, address, big,
  cite, code, del, dfn, em, font, img,
  ins, kbd, q, s, samp, small, strike,
  strong, sub, sup, tt, var, dl, dt, dd, ol, ul, li,
  fieldset, form, label, legend,
  table, caption, tbody, tfoot, thead, tr, th, td,
  center, u, b, i {
    margin: 0;
    padding: 0;
    border: 0;
    outline: 0;
    font-weight: normal;
    font-style: normal;
    font-size: 100%;
    font-family: inherit;
    vertical-align: baseline
  }
  body {
    line-height: 1
  }
  :focus {
    outline: 0
  }
  ol, ul {
    list-style: none
  }
  table {
    border-collapse: collapse;
    border-spacing: 0
  }
  blockquote:before, blockquote:after, q:before, q:after {
    content: ''
  }
  blockquote, q {
    quotes: "" ""
  }
  input, textarea {
    margin: 0;
    padding: 0
  }
  hr {
    margin: 0;
    padding: 0;
    border: 0;
    color: #000;
    background-color: #000;
    height: 1px
  }


  /* main */
  .main-container {
    display: flex;
    display: -webkit-flex;
    flex-direction: row;
    align-items: flex-start;
    height: 100%;
    width:100%;
    overflow-y: auto;
  }

</style>
