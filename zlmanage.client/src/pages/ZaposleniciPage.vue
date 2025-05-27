<template>
  <div class="p-6">
    <n-h1>Zaposlenici</n-h1>

    <!-- Forma -->
    <n-card title="Dodaj / Uredi zaposlenika" class="mb-6">
      <n-form :model="form" label-placement="top" class="grid grid-cols-1 md:grid-cols-2 gap-4">
        <n-form-item label="Ime">
          <n-input v-model:value="form.ime" placeholder="Unesi ime" />
        </n-form-item>

        <n-form-item label="Prezime">
          <n-input v-model:value="form.prezime" placeholder="Unesi prezime" />
        </n-form-item>

        <n-form-item label="Email">
          <n-input v-model:value="form.email" type="email" placeholder="email@primjer.com" />
        </n-form-item>

        <n-form-item label="Pozicija">
          <n-input v-model:value="form.pozicija" placeholder="Npr. Pilot" />
        </n-form-item>

        <n-form-item label="Kreirao admin">
          <n-select
            v-model:value="form.kreirao_ga_admin"
            :options="administratori.map(a => ({ label: `${a.ime} ${a.prezime}`, value: a.id_admina }))"
            placeholder="Odaberi admina"
          />
        </n-form-item>

        <div class="flex gap-2 items-end">
          <n-button type="primary" @click="handleSubmit">💾 Spremi</n-button>
          <n-button @click="resetForm">🧹 Očisti</n-button>
        </div>
      </n-form>
    </n-card>

    <!-- Tablica -->
    <n-card title="Popis zaposlenika">
      <n-data-table
        :columns="columns"
        :data="zaposlenici"
        :pagination="{ pageSize: 5 }"
        :bordered="false"
      />
    </n-card>
  </div>
</template>

<script setup lang="ts">
import {
  NCard,
  NH1,
  NForm,
  NFormItem,
  NInput,
  NSelect,
  NButton,
  NDataTable
} from 'naive-ui'
import { ref, onMounted, h } from 'vue'

const API = {
  zaposlenici: '/api/zaposlenici',
  administratori: '/api/administratori'
}

const zaposlenici = ref<any[]>([])
const administratori = ref<any[]>([])

const form = ref({
  id_zaposlenika: null,
  ime: '',
  prezime: '',
  email: '',
  pozicija: '',
  kreirao_ga_admin: null
})

async function fetchAll() {
  zaposlenici.value = await fetch(API.zaposlenici).then(res => res.json())
  administratori.value = await fetch(API.administratori).then(res => res.json())

  // Mapiranje admina na svakog zaposlenika (opcionalno ako API to ne vraća)
  zaposlenici.value = zaposlenici.value.map(z => ({
    ...z,
    admin: administratori.value.find(a => a.id_admina === z.kreirao_ga_admin)
  }))
}

function resetForm() {
  form.value = {
    id_zaposlenika: null,
    ime: '',
    prezime: '',
    email: '',
    pozicija: '',
    kreirao_ga_admin: null
  }
}

function editZaposlenik(z: any) {
  form.value = {
    id_zaposlenika: z.id_zaposlenika,
    ime: z.ime,
    prezime: z.prezime,
    email: z.email,
    pozicija: z.pozicija,
    kreirao_ga_admin: z.kreirao_ga_admin
  }
}

async function handleSubmit() {
  const method = form.value.id_zaposlenika ? 'PUT' : 'POST'
  const url = form.value.id_zaposlenika
    ? `${API.zaposlenici}/${form.value.id_zaposlenika}`
    : API.zaposlenici

  await fetch(url, {
    method,
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(form.value)
  })

  await fetchAll()
  resetForm()
}

async function deleteZaposlenik(id: number) {
  await fetch(`${API.zaposlenici}/${id}`, { method: 'DELETE' })
  await fetchAll()
}

const columns = [
  {
    title: 'Ime',
    key: 'ime'
  },
  {
    title: 'Prezime',
    key: 'prezime'
  },
  {
    title: 'Email',
    key: 'email'
  },
  {
    title: 'Pozicija',
    key: 'pozicija'
  },
  {
    title: 'Admin',
    render: (row: any) => `${row.admin?.ime || ''} ${row.admin?.prezime || ''}`
  },
  {
    title: 'Akcije',
    render: (row: any) => [
      h(NButton, { size: 'small', onClick: () => editZaposlenik(row) }, { default: () => '✏️' }),
      h(NButton, { size: 'small', type: 'error', onClick: () => deleteZaposlenik(row.id_zaposlenika) }, { default: () => '🗑️' })
    ]
  }
]

onMounted(fetchAll)
</script>
