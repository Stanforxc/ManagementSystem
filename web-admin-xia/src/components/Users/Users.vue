<template>
  <div class="background">
    <div class="breadcrumb">
      <Breadcrumb>
        <Breadcrumb-item>用户管理</Breadcrumb-item>
        <Breadcrumb-item>用户管理</Breadcrumb-item>
      </Breadcrumb>
    </div>
    <div class="main-container">
      <div class="loading" v-if="loading">
        <div>
          <Spin fix style="padding-bottom: 40px">加载中...</Spin>
        </div>
      </div>
      <Input v-model="searchInput" class="search" @on-enter="search">
        <Button slot="append" icon="ios-search" @click="search"></Button>
      </Input>

      <Table border :columns="heads" :data="data"></Table>
    </div>
    <Modal
      v-model="showEditDialog"
      title="修改"
      @on-ok="onSubmit">
        <Form :model="form" :label-width="50">
          <Form-item label="用户名">
            <Input v-model="form.user_name"></Input>
          </Form-item>
          <Form-item label="密码">
            <Input v-model="password" type="password"></Input>
          </Form-item>
          <Form-item label="确定密码">
            <Input v-model="repassword" type="password"></Input>
          </Form-item>
        </Form>
    </Modal>
  </div>
</template>

<script>

  import {getAllUser, editUser} from '../../service/apis'

  export default {
    data () {
      return {
        title: '用户管理',
        searchInput: '',
        loading: false,
        heads: [
          {
            title: '用户名',
            key: 'user_name',
            align: 'center',
            width: 150
          },
          {
            title: '操作',
            key: 'action',
            align: 'center',
            width: 80,
            render: (h, params) => {
              return h('div', [
                h('Button', {
                  props: {
                    type: 'ghost',
                    size: 'small'
                  },
                  on: {
                    click: () => {
                      this.form = this.data[params.index]
                      this.showEditDialog = true
                    }
                  }
                }, '修改')
              ])
            }
          }
        ],
        data: [],
        showEditDialog: false,
        form: {},
        password: '',
        repassword: ''
      }
    },
    methods: {
      /**
       * Search
       */
      search () {
        this.loading = true
        // TODO: 查询
      },
      onSubmit () {
        editUser(this.form.user_name, this.password).then(res => {
          console.log(res)
          this.$Message.success('修改成功')
        }).catch(err => {
          this.$Message.error('修改失败')
        })
      }
    },
    beforeCreate () {
      if (!this.$session.exists()) {
        this.$router.push('/Login')
      }
    },
    mounted () {
      document.title = this.title

      getAllUser().then(res => {
        console.log(res)
        this.data = res
      }).catch(err => {
        console.log(err)
      })
    }
  }
</script>

<style scoped src="./Users.css"></style>
