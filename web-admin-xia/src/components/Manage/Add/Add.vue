<template>
  <div class="background">
    <div class="breadcrumb">
      <Breadcrumb>
        <Breadcrumb-item>管理</Breadcrumb-item>
        <Breadcrumb-item>添加</Breadcrumb-item>
      </Breadcrumb>
    </div>
    <div class="main-container">
      <div class="loading" v-if="loading">
        <div>
          <Spin fix style="padding-bottom: 40px">加载中...</Spin>
        </div>
      </div>
      <Form :model="form" :label-width="80">
        <Form-item label="电影名称">
          <Input v-model="form.movie_name" placeholder="请输入电影名称"></Input>
        </Form-item>
        <Form-item label="类型">
          <Button type="primary" shape="circle" icon="plus" @click="showAddTypeDialog"></Button>
          <br>
          <Tag type="border" closable v-for="(type,index) in form.movieGenres" :key="index" @on-close="deleteType(index)">{{type.genreStyle}}</Tag>
        </Form-item>
        <Form-item label="上映日期">
          <Row>
            <Col span="12">
              <Date-picker type="date" ref="dater" placeholder="选择上映日期" format="yyyy-MM-dd" v-model="form.online_time"></Date-picker>
            </Col>
          </Row>
        </Form-item>
        <Form-item label="导演">
          <Button type="primary" shape="circle" icon="plus" @click="showAddDirectorDialog"></Button>
          <br>
          <Tag type="border" closable v-for="(director,index) in form.movieDirectors" :key="index" @on-close="deleteDirector(index)">{{director.director}}</Tag>
        </Form-item>
        <Form-item label="主演">
          <Button type="primary" shape="circle" icon="plus" @click="showAddActorDialog"></Button>
          <br/><br/>
          <Tag type="border" closable v-for="(actor,index) in form.cast" :key="index" @on-close="deleteActor(index)">{{actor}}</Tag>
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
        <Form-item>
          <Button type="success" @click="onSubmit">提交</Button>
        </Form-item>
      </Form>
    </div>
    <Modal
      v-model="addActorDialog"
      title="添加演员"
      @on-ok="ok"
      @on-cancel="cancel">
      <Input placeholder="请输入演员名称" v-model="newActor"></Input>
    </Modal>
    <Modal
      v-model="addDirectorDialog"
      title="添加导演"
      @on-ok="okDir"
      @on-cancel="cancel">
      <Row>
        <Select v-model="selectDir" placeholder="请选择导演">
          <Option :value="dir.director_name" v-for="(dir,index) in directors">{{dir.director_name}}</Option>
        </Select>
        <br>
        新建导演
        <Input placeholder="请输入导演名称" v-model="newDirector"></Input>
      </Row>
      <br>
      <Row>
        <Date-picker type="date" ref="dater" placeholder="选择导演生日" format="yyyy-MM-dd" v-model="newDirectorDes"></Date-picker>
      </Row>
    </Modal>
    <Modal
      v-model="addTypeDialog"
      title="添加类型"
      @on-ok="addType">
      <Row>
      <Select v-model="select" placeholder="请选择电影类型">
        <Option :value="type.genreStyle" v-for="(type, index) in types">{{type.genreStyle}}</Option>
      </Select>
      <br>
      新建类型
      <Input placeholder="请输入类型名称" v-model="newType"></Input>
      </Row>
      <br>
      <Row>
        <Input placeholder="描述" v-model="newTypeDes" type="textarea"></Input>
      </Row>
    </Modal>
  </div>
</template>

<script>

  import {add, getGenre, getDirectors, createType, createDirector, getDiectorsByMovie, addHistory} from '../../../service/apis'

  export default {
    data () {
      return {
        title: '添加电影',
        addActorDialog: false,
        addTypeDialog: false,
        addDirectorDialog: false,
        types: [],
        directors: [],
        select: '',
        selectDir: '',
        form: {
          movie_name: '',
          movieGenres: [],
          cast: [],
          movieDirectors: [],
          star: 0,
          online_time:'',
          price: 50,
          runtime: 0,
          description: ''
        },
        newActor: '',
        newDirector: '',
        newDirectorDes: '',
        newType: '',
        newTypeDes: '',
        loading: false
      }
    },
    methods: {
      /**
       * For adding actor
       */
      showAddActorDialog () {
        this.addActorDialog = true
      },
      showAddTypeDialog () {
        this.addTypeDialog = true
      },
      addType () {
        if (this.newType !== ''){

          createType(this.newType, this.newTypeDes).then(res => {
            console.log(res)
          }).catch(err => {
            console.log(err)
          })

          this.form.movieGenres.push({
            movie_Id: this.form.movie_name,
            genreStyle: this.newType,
            description: this.newTypeDes
          })
          this.newType = ''
          this.newTypeDes = ''
        }else {
          this.form.movieGenres.push({
            movie_Id: this.form.movie_name,
            genreStyle: this.select,
            description: this.newTypeDes
          })
          this.select = ''
        }
      },
      ok () {
        this.form.cast.push(this.newActor)
        this.newActor = ''
      },
      cancel () {

      },
      deleteActor (index) {
        this.form.cast.splice(index, 1)
      },
      deleteType (index) {
        this.form.movieGenres.splice(index, 1)
      },
      /**
       * For adding dirctor
       */
      showAddDirectorDialog () {
        this.addDirectorDialog = true
      },
      deleteDirector (index) {
        this.form.movieDirectors.splice(index, 1)
      },
      okDir () {
        if (this.newDirector !== ''){
          this.form.movieDirectors.push({
            movie_Id: this.form.movie_name,
            director: this.newDirector,
            description: this.newDirectorDes
          })


          this.newDirectorDes = this.newDirectorDes.getFullYear() + '-' + (this.newDirectorDes.getMonth() + 1).toString() +
            '-' + this.newDirectorDes.getDate() + 'T00:00:00'

          console.log(this.newDirectorDes)

          createDirector(this.newDirector, this.newDirectorDes).then(res => {
            console.log(res)
          }).catch(err => {
            console.log(err)
          })


          this.newDirector = ''
          this.newDirectorDes= ''
        }else {
          this.form.movieDirectors.push({
            movie_Id: this.form.movie_name,
            director: this.selectDir,
            description: this.newDirectorDes
          })
          this.selectDir = ''
        }
      },
      /**
       * For submitting
       */
      onSubmit () {
        this.loading = true

        this.form.online_time = this.form.online_time.getFullYear() + '-' + (this.form.online_time.getMonth() + 1).toString() +
            '-' + this.form.online_time.getDate() + 'T00:00:00'

        let actors = ''
        for (let i = 0; i < this.form.cast.length; i++){
          if (i === this.form.cast.length - 1){
            actors += this.form.cast[i]
          }else {
            actors += this.form.cast[i] + '/'
          }
        }

        this.form.cast = actors

        this.form.runtime = this.form.runtime.toString()

        console.log(this.form)

        add(this.form).then(res => {
          console.log(res)

          let time = new Date()
          addHistory(time.getFullYear()+'-'+(time.getMonth()+1).toString()+'-'+time.getDate()+ 'T00:00:00', this.$session.get('username') + 'add ' + this.form.movie_name).then(res => {
            console.log(res)
          }).catch(err => {
            console.log(err)
          })

          this.loading = false
          if (res !== '-1') {
            this.$Message.success('成功了呦~~')
          }else if (res === 'movie exist') {
            this.$Message.error('电影已经存在了呦~~')
          } else if (res === '-1'){
            this.$Message.error('信息走丢了哟~~')
          }

          this.form.cast = []
          this.form.movieDirectors = []
          this.form.movieGenres = []

        }).catch(err => {
          console.log(err)
          this.loading = false
          this.$Message.error('服务器故障了呦~~')
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

      getDirectors().then(res => {
        console.log(res)
        this.directors = res
      }).catch(err => {
        console.log(err)
      })

      getGenre().then(res => {
        console.log(res)
        this.types = res
      }).catch(err => {
        console.log(err)
      })

    }
  }
</script>

<style scoped src="./Add.css"></style>
