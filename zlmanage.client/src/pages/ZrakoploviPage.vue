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
      <n-input
        v-model:value="searchQuery"
        placeholder="Pretraži po modelu ili registraciji..."
        clearable
        class="mb-4"
      />
      <n-data-table
        :columns="columns"
        :data="filteredZrakoplovi"
        :pagination="{ pageSize: 5 }"
        :bordered="false"
      />
    </n-card>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, computed, h } from 'vue'
import axios from 'axios'
import {
  NCard,
  NForm,
  NFormItem,
  NInput,
  NButton,
  NDataTable,
  NH1
} from 'naive-ui'

const API = 'http://localhost:5233/api/zrakoplov'

const zrakoplovi = ref<any[]>([])
const searchQuery = ref('')

const form = ref({
  idZrakoplova: null,
  model: '',
  registracija: ''
})

async function fetchAll() {
  try {
    const response = await axios.get(API)
    zrakoplovi.value = response.data
  } catch (error) {
    console.error('Greška kod dohvaćanja zrakoplova:', error)
  }
}

const filteredZrakoplovi = computed(() => {
  if (!searchQuery.value) return zrakoplovi.value
  const query = searchQuery.value.toLowerCase()
  return zrakoplovi.value.filter(z =>
    z.model.toLowerCase().includes(query) ||
    z.registracija.toLowerCase().includes(query)
  )
})

function resetForm() {
  form.value = {
    idZrakoplova: null,
    model: '',
    registracija: ''
  }
}

function editZrakoplov(z: any) {
  form.value = {
    idZrakoplova: Number(z.idZrakoplova),  // ensure it's a number
    model: z.model,
    registracija: z.registracija
  }
}

async function handleSubmit() {
  const method = form.value.idZrakoplova ? 'put' : 'post'
  const url = form.value.idZrakoplova ? `${API}/${form.value.idZrakoplova}` : API

  try {
    await axios({
      method,
      url,
      data: form.value,
      headers: { 'Content-Type': 'application/json' }
    })
    await fetchAll()
    resetForm()
  } catch (error) {
    console.error('Greška kod spremanja zrakoplova:', error)
  }
}

async function deleteZrakoplov(id: number) {
  try {
    await axios.delete(`${API}/${id}`)
    await fetchAll()
  } catch (error) {
    console.error('Greška kod brisanja zrakoplova:', error)
  }
}

const columns = [
  {
    title: 'ID',
    key: 'idZrakoplova'
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
    key: 'actions',
    render: (row: any) =>
      h('div', { class: 'flex gap-2' }, [
        h(NButton, { size: 'small', onClick: () => editZrakoplov(row) }, { default: () => '✏️' }),
        h(NButton, {
          size: 'small',
          type: 'error',
          onClick: () => deleteZrakoplov(row.idZrakoplova)
        }, { default: () => '🗑️' })
      ])
  }
]

onMounted(fetchAll)
</script>
