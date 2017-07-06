<template>
  <div class="background">
    <div class="breadcrumb">
      <Breadcrumb>
        <Breadcrumb-item>管理</Breadcrumb-item>
        <Breadcrumb-item>增删改查</Breadcrumb-item>
      </Breadcrumb>
    </div>
    <div class="main-container">
      <div class="loading" v-if="loading">
        <div>
          <Spin fix style="padding-bottom: 40px">加载中...</Spin>
        </div>
      </div>

      <Input v-model="searchInput" class="search" @on-enter="search">
        <span slot="prepend">搜索</span>
        <Button slot="append" icon="ios-search" @click="search"></Button>
      </Input>

      <Button type="error" class="delete" @click="deleteDialog = true">删除</Button>

      <transition name="slide-fade">
        <Table border :columns="heads" :data="showData" @on-selection-change="selectionChange" class="table" v-if="show"></Table>
      </transition>

      <Page :total="data.length" class="pages" @on-change="changePage" :page-size="5"></Page>
    </div>
    <Modal
      v-model="showContent"
      title="剧情">
      <p>{{content}}</p>
    </Modal>
    <Modal
      v-model="edit"
      title="修改"
      @on-ok="onSubmit">
      <Form :model="form" :label-width="80">
        <Form-item label="电影名称">
          <Input v-model="form.movie_name" placeholder="请输入电影名称"></Input>
        </Form-item>
        <Form-item label="评分">
          <Rate v-model="form.star"></Rate>
        </Form-item>
        <Form-item label="票价">
          <Input v-model="form.price" placeholder="票价" type="number"></Input>
        </Form-item>
        <Form-item label="片长">
          <Input v-model="form.runtime" placeholder="片长" type="number"></Input>
        </Form-item>
        <Form-item label="剧情">
          <Input v-model="form.description" type="textarea" :autosize="{minRows: 2,maxRows: 5}" placeholder="请输入剧情..."></Input>
        </Form-item>
      </Form>
    </Modal>
    <Modal
      v-model="deleteDialog"
      title="删除"
      @on-ok="deleteMovie">
      <p>是否确定删除？</p>
    </Modal>
    <Modal
      v-model="addActorDialog"
      title="添加演员"
      @on-ok="form.actors.push(newActor);newActor = '';">
      <Input placeholder="请输入演员名称" v-model="newActor"></Input>
    </Modal>
    <Modal
      v-model="info"
      title="详情"
      @on-ok="info = false">
      <Form :model="infoForm" :label-width="80">
        <Form-item label="电影名称">
          <p style="font-size: 1.5em">{{infoForm.movie_name}}</p>
        </Form-item>
        <Form-item label="评分">
          <Rate :value="infoForm.star"></Rate>
        </Form-item>
        <Form-item label="票价">
          <p>{{infoForm.price}} $</p>
        </Form-item>
        <Form-item label="片长">
          <p>{{infoForm.runtime}} min</p>
        </Form-item>
        <Form-item label="剧情">
          <p>{{infoForm.description}}</p>
        </Form-item>
      </Form>
    </Modal>
  </div>

</template>

<script>

  import {deleteMovie,getAllMovies, getMovieInfo, editMovie, addHistory} from '../../../service/apis'

  export default {
    data () {
      return {
        title: '删除和修改',
        heads: [
          {
            type: 'selection',
            align: 'center'
          },
          {
            title: '名称',
            key: 'movie_name'
          },
          {
            title: '主演',
            key: 'cast'
          },
          {
            title: '上映时间',
            key: 'online_time'
          },
          {
            title: '评分',
            key: 'star'
          },
          {
            title: '票价',
            key: 'price'
          },
          {
            title: '片长',
            key: 'runtime'
          },
          {
            title: '操作',
            key: 'action',
            align: 'center',
            render: (h, params) => {
              return h('div', [
                h('Button', {
                  props: {
                    type: 'ghost',
                    size: 'small'
                  },
                  on: {
                    click: () => {
                      this.curIndex = params.index
                      this.edit = true
                      this.form = this.showData[params.index]
                      console.log(this.form)
                    }
                  }
                }, '修改'),
                h('Button', {
                  props: {
                    type: 'success',
                    size: 'small'
                  },
                  on: {
                    click: () => {
                      this.curIndex = params.index
                      this.info = true
                      getMovieInfo(this.showData[params.index].movie_name).then(res => {
                        this.infoForm = res
                        console.log(res)
                      }).catch(err => {
                        console.log(err)
                      })
                    }
                  }
                }, '详情')
              ])
            }
          }
        ],
        data: [],
        showData: [],
        showContent: false,
        content: '',
        edit: false,
        delete: false,
        info: false,
        addActorDialog:false,
        newActor: '',
        curIndex: -1,
        form: {},
        infoForm: {},
        loading: false,
        deleteDialog: false,
        selections: [],
        searchInput: '',
        type: 'all',
        show: true
      }
    },
    methods: {
      /**
       * Show actors
       * @param h
       * @param row
       * @returns {Array}
       */
      showStyles (h, row) {
        let styles = []
        for (let i = 0; i < row.movieGenres.length; i++){
          styles.push(h('p', row.movieGenres[i]))
        }
        return styles
      },
      /**
       * For editing
       */
      onSubmit () {
        this.loading = true

        editMovie(this.form.movie_name, this.form).then(res => {
          let time = new Date()
          addHistory(time.getFullYear()+'-'+(time.getMonth()+1).toString()+'-'+time.getDate()+ 'T00:00:00', this.$session.get('username') + 'edit ' + this.form.movie_name).then(res => {
            console.log(res)
          }).catch(err => {
            console.log(err)
          })
          console.log(res)
          this.loading = false
          this.$Message.success('成功')
        }).catch(err => {
          console.log(err)
          this.loading = false
          this.$Message.error('失败')
        })
      },
      deleteActor (index) {
        this.form.actors.splice(index, 1)
      },
      showAddActorDialog () {
        this.addActorDialog = true
      },
      /**
       * Change page
       * @param index
       */
      changePage (index) {
        // TODO: 分页请求
        console.log(index)
        if (index * 5 - index + 5 < this.data.length){
          this.showData = this.data.slice(index * 5 - 1, index * 5 + 4)
        }else {
          this.showData = this.data.slice(index * 5 - 1, this.data.length - 1)
        }
      },
      /**
       * For deleting
       */
      async deleteMovie () {
        let tmp = this.data
        for (let i = 0; i < this.selections.length; i++) {
          deleteMovie(this.selections[i].movie_name).then(res => {
            let time = new Date()
            addHistory(time.getFullYear()+'-'+(time.getMonth()+1).toString()+'-'+time.getDate()+ 'T00:00:00', this.$session.get('username') + 'delete ' + this.selections[i].movie_name).then(res => {
              console.log(res)
            }).catch(err => {
              console.log(err)
            })
            console.log(res)
            this.$Message.success(this.selections[i].movie_name + '删除成功!')
            getAllMovies().then(res => {
              this.data = res
              if (this.data.length < 5){
                this.showData = this.data
              }else {
                this.showData = this.data.slice(0, 5)
              }
            }).catch(err => {})
          }).catch(err => {
            this.$Message.error(this.selections[i].movie_name + '删除失败!')
          })
        }
      },
      selectionChange (selection) {
        console.log(selection)
        this.selections = selection
      },
      /**
       * Search
       */
      search () {
        this.loading = true
        // TODO: 查询
        let arr = []
        let reg = new RegExp(this.searchInput);
        for (let i = 0; i < this.data.length; i++) {
          if (this.data[i].movie_name.match(reg)){
            arr.push(this.data[i])
          }
        }
        this.showData = arr
        this.loading = false

      }
    },
    beforeCreate () {
      if (!this.$session.exists()) {
        this.$router.push('/Login')
      }
    },
    mounted () {
      document.title = this.title

      getAllMovies().then(res => {
        console.log(res)
        this.data = res
        if (this.data.length < 5){
          this.showData = this.data
        }else {
          this.showData = this.data.slice(0, 5)
        }
      }).catch(err => {
        console.log(err)
      })

    }
  }
</script>

<style scoped src="./DeleteAndEdit.css"></style>
