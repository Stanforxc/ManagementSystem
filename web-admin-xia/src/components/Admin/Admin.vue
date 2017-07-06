<template>
  <div class="background">
    <div class="breadcrumb">
      <Breadcrumb>
        <Breadcrumb-item>管理员</Breadcrumb-item>
        <Breadcrumb-item>修改密码</Breadcrumb-item>
      </Breadcrumb>
    </div>
    <div class="main-container">
      <div class="loading" v-if="loading">
        <div>
          <Spin fix style="padding-bottom: 40px">加载中...</Spin>
        </div>
      </div>
      <Table border :columns="heads" :data="history"></Table>
    </div>
  </div>
</template>

<script>

  import {getAllHistory} from '../../service/apis'

  export default {
    data () {
      return {
        title: '历史',
        loading: false,
        oldPwd: '',
        newPwd: '',
        rePwd: '',
        history: [],
        heads: [
          {
            title: '时间',
            key: 'time',
            align: 'center',
            width: 150
          },
          {

            title: '操作',
            key: 'operation',
            align: 'center',
            width: 150
          }
        ]
      }
    },
    beforeCreate () {
      if(!this.$session.exists()) {
        this.$router.push('/Login')
      }
    },
    methods: {
      onSubmit () {

      }
    },
    mounted () {
      document.title = this.title

      getAllHistory().then(res => {
        this.history = res
      }).catch(err => {

      })
    }
  }
</script>

<style scoped>
  .background {
    background-color: #F5F7F9;
    width: 100%;
    height: 100%;
  }

  .loading {
    width: 80%;
    height: calc(80% - 50px);
    position: absolute;
    display: flex;
    display: -webkit-flex;
    flex-direction: column;
    align-items: center;
    opacity: 0.9;
    z-index: 10000;
  }

  .loading > div {
    display: flex;
    display: -webkit-flex;
    flex-direction: row;
    align-items: center;
  }

  .breadcrumb {
    padding: 10px 15px 0;
    padding-right: 88px;
    position: relative;
    float: left;
    margin-left: 30px;
  }

  .main-container {
    background-color: white;
    width: 80%;
    height: 80%;
    margin: 50px auto;
    display: flex;
    display: -webkit-flex;
    flex-direction: column;
    align-items: center;
  }

  .form {
    margin-top: 60px;
  }
</style>
