<template>
  <div class="layout" v-if="show !== '/Login'">
    <Menu mode="horizontal" theme="dark" active-name="manage" @on-select="onSelect">
      <div class="layout-logo">
        <p>电影信息管理系统</p>
      </div>
      <div class="layout-nav">
        <Menu-item name="manage">
          <Icon type="edit"></Icon>
          管理
        </Menu-item>
        <Menu-item name="recommendation">
          <Icon type="ios-film"></Icon>
          推荐
        </Menu-item>
        <Menu-item name="user-manage">
          <Icon type="person-add"></Icon>
          用户管理
        </Menu-item>
        <Menu-item name="admin">
          <Icon type="bookmark"></Icon>
          历史
        </Menu-item>
        <Menu-item name="logout">
          <Icon type="log-out"></Icon>
          登出
        </Menu-item>
      </div>
    </Menu>
  </div>
</template>

<script>
  export default {
    data () {
      return {
        show: ''
      }
    },
    methods: {
      onSelect (name) {
        if (name === 'manage') {
          this.$router.push({name: 'Manage-Add'})
        }else if (name === 'recommendation') {
          this.$router.push({name: 'Recommendation'})
        }else if (name === 'user-manage') {
          this.$router.push({name: 'Users'})
        }else if (name === 'admin') {
          this.$router.push({name: 'Admin'})
        }else if (name === 'logout') {
          this.$session.destroy()
          this.$router.push({name: 'Login'})
        }
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

<style scoped src="./Bar.css"></style>
