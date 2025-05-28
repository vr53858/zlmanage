import { createRouter, createWebHistory } from 'vue-router'
import LetoviPage from '@/pages/LetoviPage.vue'
import ZaposleniciPage from '@/pages/ZaposleniciPage.vue'
import ZrakoploviPage from '@/pages/ZrakoploviPage.vue'

const routes = [
  {
    path: '/',
    redirect: '/letovi'
  },
  { path: '/letovi', component: LetoviPage },
  { path: '/zaposlenici', component: ZaposleniciPage },
  { path: '/zrakoplovi', component: ZrakoploviPage }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
