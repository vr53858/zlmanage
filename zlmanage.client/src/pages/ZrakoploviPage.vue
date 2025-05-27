<template>
  <div class="p-6">
    <n-h1>Zrakoplovi</n-h1>

    <!-- Forma -->
    <n-card title="Dodaj / Uredi zrakoplov" class="mb-6">
      <n-form :model="form" label-placement="top" class="grid grid-cols-1 md:grid-cols-2 gap-4">
        <n-form-item label="Model">
          <n-input v-model:value="form.model" placeholder="Npr. Boeing 737" />
        </n-form-item>

        <n-form-item label="Registracija">
          <n-input v-model:value="form.registracija" placeholder="Npr. 9A-ABC" />
        </n-form-item>

        <div class="flex gap-2 items-end col-span-2">
          <n-button type="primary" @click="handleSubmit">💾 Spremi</n-button>
          <n-button @click="resetForm">🧹 Očisti</n-button>
        </div>
      </n-form>
    </n-card>

    <!-- Tablica -->
    <n-card title="Popis zrakoplova">
      <n-data-table
        :columns="columns"
        :data="zrakoplovi"
        :pagination="{ pageSize: 5 }"
        :bordered="false"
      />
    </n-card>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, h } from 'vue'
import {
  NCard,
  NForm,
  NFormItem,
  NInput,
  NButton,
  NDataTable,
  NH1
} from 'naive-ui'

const API = '/api/zrakoplovi'

const zrakoplovi = ref<any[]>([])

const form = ref({
  id_zrakoplova: null,
  model: '',
  registracija: ''
})

async function fetchAll() {
  zrakoplovi.value = await fetch(API).then(res => res.json())
}

function resetForm() {
  form.value = {
    id_zrakoplova: null,
    model: '',
    registracija: ''
  }
}

function editZrakoplov(z: any) {
  form.value = { ...z }
}

async function handleSubmit() {
  const method = form.value.id_zrakoplova ? 'PUT' : 'POST'
  const url = form.value.id_zrakoplova
    ? `${API}/${form.value.id_zrakoplova}`
    : API

  await fetch(url, {
    method,
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(form.value)
  })

  await fetchAll()
  resetForm()
}

async function deleteZrakoplov(id: number) {
  await fetch(`${API}/${id}`, { method: 'DELETE' })
  await fetchAll()
}

const columns = [
  {
    title: 'ID',
    key: 'id_zrakoplova'
  },
  {
    title: 'Model',
    key: 'model'
  },
  {
    title: 'Registracija',
    key: 'registracija'
  },
  {
    title: 'Akcije',
    render: (row: any) => [
      h(NButton, { size: 'small', onClick: () => editZrakoplov(row) }, { default: () => '✏️' }),
      h(NButton, { size: 'small', type: 'error', onClick: () => deleteZrakoplov(row.id_zrakoplova) }, { default: () => '🗑️' })
    ]
  }
]

onMounted(fetchAll)
</script>
