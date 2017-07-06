<template>
  <div class="background">
    <div :class="['login-form', register ? 'active':'']">
      <h1>{{text}}</h1>
      <div class="username">
        <label for="username">账号</label>
        <input type="text" placeholder="" id="username" v-model="username">
      </div>
      <div class="password">
        <label for="password">密码</label>
        <input type="password" placeholder="" id="password" v-model="password" @keyup.enter="onSubmit">
      </div>
      <div class="password" v-if="register">
        <label for="re-password">确认密码</label>
        <input type="password" placeholder="" id="re-password" v-model="repassword">
      </div>
      <Button type="success" size="large" class="login-btn" @click="onSubmit">{{text}}</Button>
      <br><br>
      <a @click="register = !register">{{aText}}</a>
    </div>
  </div>
</template>

<script>
  import fetch from '../../service/fetch'
  import {addHistory, register, login} from '../../service/apis'

  export default {
    data () {
      return {
        title: '登录',
        username: '',
        password: '',
        repassword: '',
        text: '登录',
        aText: '还没有账号，注册一个',
        register: false
      }
    },
    methods: {
      async onSubmit () {
        if (this.register) {
          register(this.username, this.password).then(res => {
            this.$Message.success('创建成功')
            this.$session.start()
            this.$session.set('username', this.username)
            this.$router.push({name: 'Manage-Add'})
          }).catch(err => {
            this.$Message.error('创建失败')
          })
        }else {


          login(this.username, this.password).then(res => {

            let time = new Date()

            addHistory(time.getFullYear()+'-'+(time.getMonth()+1).toString()+'-'+time.getDate()+ 'T00:00:00', this.username + ' Login').then(res => {
              console.log(res)
            }).catch(err => {
              console.log(err)
            })
            this.$session.start()
            this.$session.set('username', this.username)
            this.$router.push({name: 'Manage-Add'})
          }).catch(err => {
            this.$Message.error('账号或密码错误')
          })
        }
      }
    },
    beforeCreate () {
      if (this.$session.exists()) {
        this.$router.push('/Manage/Add')
      }
    },
    mounted () {
      document.title = this.title

      let data = {
        "movie_name": "alien9",
        "online_time": "1996-6-23T00:00:00",
        "star": 5,
        "cast":"xiachen/vv",
        "price":50,
        "runtime":"forever",
        "description":"That's what I owe you.",
        "movieDirectors": [
          {
            "movie_Id": "alien9",
            "director": "xiachen",
            "description":"default"
          },
          {
            "movie_Id": "alien9",
            "director": "vv",
            "description":"default"
          }
        ],
        "movieGenres": [
          {
            "movie_Id": "alien9",
            "genreStyle": "action",
            "description":"default"
          },
          {
            "movie_Id": "alien9",
            "genreStyle": "comic",
            "description":"default"
          }
        ]
      }

      let test = () => fetch('POST', '/api/movie', data)

      test().then(res => {
        console.log(res)
      }).catch(err => {
        console.log(err)
      })

    },
    watch: {
      register: function (val) {
        if (val === true) {
          this.text = '注册'
          this.aText = '登录'
        }else {
          this.text = '登录'
          this.aText = '还没有账号，注册一个'
        }
      }
    }
  }
</script>

<style scoped src="./Login.css"></style>
