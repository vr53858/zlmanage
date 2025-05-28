<template>
  <n-layout-header style="display: flex; justify-content: flex-start; gap: 12px; padding: 12px;">
    <n-button @click="goTo('zrakoplovi')" size="medium">Zrakoplovi</n-button>
    <n-button @click="goTo('letovi')" size="medium">Letovi</n-button>
  </n-layout-header>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
const router = useRouter()

function goTo(routeName: string) {
  router.push({ name: routeName })
}
</script>

<style scoped>
/* Optional: style your header */
</style>
