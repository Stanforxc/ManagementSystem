<template>
  <div class="main-container">
    <Menu mode="horizontal" active-name="1" @on-select="onSelect">
      <div class="second-menu">
        <Menu-item name="add">添加</Menu-item>
        <Menu-item name="delete/edit">增删改查</Menu-item>
      </div>
    </Menu>
    <router-view></router-view>
  </div>
</template>
<script>
  export default {
    data () {
      return {}
    },
    methods: {
      onSelect (name) {
        if (name === 'add') {
          this.$router.push({name: 'Manage-Add'})
        }else if (name === 'delete/edit') {
          this.$router.push({name: 'Manage-Delete-Edit'})
        }
      }
    },
    mounted () {

    }
  }
</script>

<style scoped src="./Manage.css"></style>

