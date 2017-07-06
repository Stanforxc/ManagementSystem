<template>
  <div class="background">
    <div class="breadcrumb">
      <Breadcrumb>
        <Breadcrumb-item>推荐</Breadcrumb-item>
        <Breadcrumb-item>推荐</Breadcrumb-item>
      </Breadcrumb>
    </div>
    <div class="main-container">
      <Tabs type="card" class="tabs" style="overflow-y: auto; margin: 20px; padding: 10px" v-if="show">
        <Tab-pane label="1星">
          <Row gutter="10">
            <Col span="12" v-for="(movie,index) in movies[0]">
            <Card class="my-card">
              <p slot="title">{{movie.movie_name}}</p>
              <br>
              <p><span>导演：</span>{{movie.movieDirectors[0].director}}</p>
              <br>
              <p><span>上映时间：</span>{{movie.online_time}}</p>
              <br>
              <p><span>星级：</span><Rate v-model="movie.star"></Rate></p>
              <br>
              <p><span>票价：</span>{{movie.price}}$</p>
              <br>
              <p><span>片长</span>{{movie.runtime}}</p>
              <br>
              <p><span>剧情描述：</span>{{movie.description}}</p>
            </Card>
            </Col>
          </Row>
        </Tab-pane>
        <Tab-pane label="2星">
          <Row gutter="10">
            <Col span="12" v-for="(movie,index) in movies[1]">
            <Card class="my-card">
              <p slot="title">{{movie.movie_name}}</p>
              <br>
              <p><span>导演：</span>{{movie.movieDirectors[0].director}}</p>
              <br>
              <p><span>上映时间：</span>{{movie.online_time}}</p>
              <br>
              <p><span>星级：</span><Rate v-model="movie.star"></Rate></p>
              <br>
              <p><span>票价：</span>{{movie.price}}$</p>
              <br>
              <p><span>片长</span>{{movie.runtime}}</p>
              <br>
              <p><span>剧情描述：</span>{{movie.description}}</p>
            </Card>
            </Col>
          </Row>
        </Tab-pane>
        <Tab-pane label="3星">
          <Row gutter="10">
            <Col span="12" v-for="(movie,index) in movies[2]">
            <Card>
              <p slot="title">{{movie.movie_name}}</p>
              <br>
              <p><span>导演：</span>{{movie.movieDirectors[0].director}}</p>
              <br>
              <p><span>上映时间：</span>{{movie.online_time}}</p>
              <br>
              <p><span>星级：</span><Rate v-model="movie.star"></Rate></p>
              <br>
              <p><span>票价：</span>{{movie.price}}$</p>
              <br>
              <p><span>片长</span>{{movie.runtime}}</p>
              <br>
              <p><span>剧情描述：</span>{{movie.description}}</p>
            </Card>
            </Col>
          </Row>
        </Tab-pane>
        <Tab-pane label="4星">
          <Row gutter="10">
            <Col span="12" v-for="(movie,index) in movies[3]">
            <Card class="my-card">
              <p slot="title">{{movie.movie_name}}</p>
              <br>
              <p><span>导演：</span>{{movie.movieDirectors[0].director}}</p>
              <br>
              <p><span>上映时间：</span>{{movie.online_time}}</p>
              <br>
              <p><span>星级：</span><Rate v-model="movie.star"></Rate></p>
              <br>
              <p><span>票价：</span>{{movie.price}}$</p>
              <br>
              <p><span>片长</span>{{movie.runtime}}</p>
              <br>
              <p><span>剧情描述：</span>{{movie.description}}</p>
            </Card>
            </Col>
          </Row>
        </Tab-pane>
        <Tab-pane label="5星">
          <Row gutter="10">
            <Col span="12" v-for="(movie,index) in movies[4]">
            <Card class="my-card">
              <p slot="title">{{movie.movie_name}}</p>
              <br>
              <p><span>导演：</span>{{movie.movieDirectors[0].director}}</p>
              <br>
              <p><span>上映时间：</span>{{movie.online_time}}</p>
              <br>
              <p><span>星级：</span><Rate v-model="movie.star"></Rate></p>
              <br>
              <p><span>票价：</span>{{movie.price}}$</p>
              <br>
              <p><span>片长</span>{{movie.runtime}}</p>
              <br>
              <p><span>剧情描述：</span>{{movie.description}}</p>
            </Card>
            </Col>
          </Row>
        </Tab-pane>
      </Tabs>

    </div>
  </div>
</template>

<script>

  import {getRecommendations} from '../../service/apis'

  export default {
    data () {
      return {
        title: '电影推荐',
        movies: [[], [], [], [], []],
        show: false
      }
    },
    beforeCreate () {
      if (!this.$session.exists()) {
        this.$router.push('/Login')
      }
    },
    mounted () {
      document.title = this.title

      for (let i = 1; i <= 5; i++){
        getRecommendations(i).then(res => {
          this.movies[i - 1] = res
          console.log(res)
        }).catch(err => {
          console.log(err)
        })
      }

      setTimeout(() => this.show = true, 2000)
    }
  }
</script>

<style scoped src="./Recommendation.css"></style>
