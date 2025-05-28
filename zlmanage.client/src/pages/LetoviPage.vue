<template>
  <div class="p-6">
    <n-h1>
      Letovi
<!--      <n-button @click="toZrakoplovi" size="medium" class="ml-4">-->
<!--        Zrakoplovi-->
<!--      </n-button>-->
    </n-h1>
    <!-- Forma -->
    <n-card title="Dodaj / Uredi let" class="mb-6">
      <n-form :model="form" label-placement="top" class="grid grid-cols-1 md:grid-cols-2 gap-4">
        <n-form-item label="Datum polaska">
          <n-date-picker v-model:value="form.datumPolaska" type="date" />
        </n-form-item>

        <n-form-item label="Datum dolaska">
          <n-date-picker v-model:value="form.datumDolaska" type="date" />
        </n-form-item>

        <n-form-item label="Zrakoplov">
          <n-select
            v-model:value="form.idZrakoplova"
            :options="zrakoplovi.map(z => ({ label: `${z.model} (${z.registracija})`, value: z.idZrakoplova }))"
            placeholder="Odaberi zrakoplov"
          />
        </n-form-item>

        <n-form-item label="Pista">
          <n-select
            v-model:value="form.idPiste"
            :options="piste.map(p => ({ label: `${p.oznaka} (${p.duljina}m)`, value: p.idPiste }))"
            placeholder="Odaberi pistu"
          />
        </n-form-item>

        <n-form-item label="Zaposlenik">
          <n-select
            v-model:value="form.kreiraoGa"
            :options="zaposlenici.map(z => ({ label: `${z.pozicija}`, value: z.idZaposlenika }))"
            placeholder="Odaberi zaposlenika"
          />
        </n-form-item>

        <div class="flex gap-2 items-end">
          <n-button type="primary" @click="handleSubmit">💾 Spremi</n-button>
          <n-button @click="resetForm">🧹 Očisti</n-button>
        </div>
      </n-form>
    </n-card>

    <!-- Tablica -->
    <n-card title="Popis letova">
      <n-input
        v-model:value="searchQuery"
        placeholder="Pretraži po broju leta, zrakoplovu, pisti..."
        clearable
        class="mb-4"
      />
      <n-data-table
        :columns="columns"
        :data="filteredLetovi"
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
  NSelect,
  NDatePicker,
  NButton,
  NDataTable
} from 'naive-ui'
import {ref, onMounted, Fragment} from 'vue'
import { h } from 'vue'

import { useRouter } from 'vue-router'
const router = useRouter()

const toZrakoplovi = () => {
  router.push({ name: '/zrakoplovi' })
}

const searchQuery = ref('')

import { computed } from 'vue'

const filteredLetovi = computed(() => {
  if (!searchQuery.value) return letovi.value
  const q = searchQuery.value.toLowerCase()
  return letovi.value.filter(l =>
    l.brojLeta?.toString().includes(q) ||
    l.zrakoplov?.model.toLowerCase().includes(q) ||
    l.zrakoplov?.registracija.toLowerCase().includes(q)
    || l.pista?.oznaka.toLowerCase().includes(q) ||
    l.kreirao?.pozicija.toLowerCase().includes(q)
  )
})

import {
  getLetovi,
  createLet,
  updateLet,
  deleteLet,
  getZaposlenici,
  getZrakoplovi,
  getPiste
} from '@/api/letovi'

const letovi = ref<any[]>([])
const zrakoplovi = ref<any[]>([])
const piste = ref<any[]>([])
const zaposlenici = ref<any[]>([])

const form = ref({
  brojLeta: null,
  datumPolaska: null,
  datumDolaska: null,
  idZrakoplova: null,
  idPiste: null,
  kreiraoGa: null
})

async function fetchAll() {
  const [letoviRes, zapRes, zrakRes, pisteRes] = await Promise.all([
    getLetovi(),
    getZaposlenici(),
    getZrakoplovi(),
    getPiste()
  ])

  zaposlenici.value = zapRes.data
  zrakoplovi.value = zrakRes.data
  piste.value = pisteRes.data

  letovi.value = letoviRes.data.map((l: any) => ({
    ...l,
    zrakoplov: zrakoplovi.value.find(z => z.idZrakoplova === l.idZrakoplova),
    pista: piste.value.find(p => p.idPiste === l.idPiste),
    kreirao: zaposlenici.value.find(z => z.idZaposlenika === l.kreiraoGa)
  }))
}

function resetForm() {
  form.value = {
    brojLeta: null,
    datumPolaska: null,
    datumDolaska: null,
    idZrakoplova: null,
    idPiste: null,
    kreiraoGa: null
  }
}

async function handleSubmit() {
  const request = {
    datumPolaska: form.value.datumPolaska ? new Date(form.value.datumPolaska): null,
    datumDolaska: form.value.datumDolaska ? new Date(form.value.datumDolaska) : null,
    idZrakoplova: form.value.idZrakoplova,
    idPiste: form.value.idPiste,
    kreiraoGa: form.value.kreiraoGa,
    ...(form.value.brojLeta && { brojLeta: form.value.brojLeta })
  }

  if (form.value.brojLeta) {
    await updateLet(form.value.brojLeta, request)
  } else {
    await createLet(request)
  }

  await fetchAll()
  resetForm()
}

async function adeleteLet(id: number) {
  await deleteLet(id)
  await fetchAll()
}

function editLet(l: any) {
  form.value = {
    brojLeta: l.brojLeta,
    datumPolaska: l.datumPolaska,
    datumDolaska: l.datumDolaska,
    idZrakoplova: l.idZrakoplova,
    idPiste: l.idPiste,
    kreiraoGa: l.kreiraoGa
  }
}

const columns = [
  {
    title: 'Broj',
    key: 'brojLeta'
  },
  {
    title: 'Polazak',
    render: (row: any) => {
      const date = new Date(row.datumPolaska)
      return `${date.toLocaleDateString()} ${date.toLocaleTimeString()}`
    }
  },
  {
    title: 'Dolazak',
    render: (row: any) => {
      const date = new Date(row.datumDolaska)
      return `${date.toLocaleDateString()} ${date.toLocaleTimeString()}`
    }  },
  {
    title: 'Zrakoplov',
    render: (row: any) => `${row.zrakoplov?.model} (${row.zrakoplov?.registracija})`
  },
  {
    title: 'Pista',
    render: (row: any) => row.pista?.oznaka
  },
  {
    title: 'Zaposlenik',
    render: (row: any) => `${row.kreirao?.pozicija}`
  },
  {
    title: 'Akcije',
    render: (row: any) =>
      h(Fragment, null, [
        h(NButton, { size: 'small', onClick: () => editLet(row) }, { default: () => '✏️' }),
        h(NButton, {
          size: 'small',
          type: 'error',
          class: 'ml-2',
          onClick: () => adeleteLet(row.brojLeta)
        }, { default: () => '🗑️' })
      ])
  }
]

onMounted(fetchAll)
</script>
