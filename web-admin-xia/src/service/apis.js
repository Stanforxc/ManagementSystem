/**
 * Created by xiachen on 2017/6/21.
 */
import fetch from './fetch'

var add = (data) => fetch('POST', '/api/movie', data)

var getGenre = () => fetch('GET', '/api/genre', {})

var createType = (type, des) => fetch('POST', '/api/genre', {genreStyle: type, description: des, genreMovies: []})

var createDirector = (name, birth) => fetch('POST', '/api/director', {director_name: name, born_date: birth})

var getDirectors = () => fetch('GET', '/api/director', {})

var getDiectorsByMovie = (name) => fetch('GET', '/api/director', {movie_name: name})

var deleteMovie = (name) => fetch('DELETE', '/api/movie?movie_name='+ name, {})

var getAllMovies = () => fetch('GET', '/api/movie', {})

var getMovieInfo = (name) => fetch('GET', '/api/movie?movie_name=' + name, {})

var editMovie = (name, data) => fetch('PUT', '/api/movie?movie_name=' + name, data)

var addHistory = (time, operation) => fetch('POST', '/api/history', {time: time, operation: operation})

var getRecommendations = (rank) => fetch('GET', '/api/movie/rank', {rank: rank})

var register = (user_name, password) => fetch('POST', '/api/user/create', {user_name: user_name, password: password})

var login = (user_name, password) => fetch('POST', '/api/user/login', {user_name: user_name, password: password})

var getAllHistory = () => fetch('GET', '/api/history', {})

var getAllUser = () => fetch('GET', '/api/user', {})

var editUser = (user_name, password) => fetch('POST', '/api/user/change', {user_name: user_name, password: password})

export {
  add, getGenre, getDirectors, createType, createDirector, getDiectorsByMovie, deleteMovie,
  getAllMovies, getMovieInfo, editMovie, addHistory, getRecommendations, register, login,
  getAllHistory, getAllUser, editUser
}
