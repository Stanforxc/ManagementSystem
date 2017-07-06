import Vue from 'vue'
import Router from 'vue-router'
import Manage from '@/components/Manage/Manage'
import Add from '@/components/Manage/Add/Add'
import DeleteAndEdit from '@/components/Manage/DeleteAndEdit/DeleteAndEdit'
import Login from '@/components/Login/Login'
import Recommendation from '@/components/Recommendation/Recommendation'
import Users from '@/components/Users/Users'
import Admin from '@/components/Admin/Admin'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      redirect: '/Manage'
    },
    {
      path: '/Manage',
      name: 'Manage',
      component: Manage,
      children: [
        {
          path: 'Add',
          name: 'Manage-Add',
          component: Add
        },
        {
          path: 'DeleteAndEdit',
          name: 'Manage-Delete-Edit',
          component: DeleteAndEdit
        }
      ]
    },
    {
      path: '/Login',
      name: 'Login',
      component: Login
    },
    {
      path: '/Recommendation',
      name: 'Recommendation',
      component: Recommendation
    },
    {
      path: '/Users',
      name: 'Users',
      component: Users
    },
    {
      path: '/Admin',
      name: 'Admin',
      component: Admin
    }
  ]
})
