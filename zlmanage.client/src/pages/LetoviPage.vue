<template>
  <div class="p-6">
    <n-h1>Letovi</n-h1>

    <!-- Forma -->
    <n-card title="Dodaj / Uredi let" class="mb-6">
      <n-form :model="form" label-placement="top" class="grid grid-cols-1 md:grid-cols-2 gap-4">
        <n-form-item label="Datum polaska">
          <n-date-picker v-model:value="form.datum_polaska" type="date" />
        </n-form-item>

        <n-form-item label="Datum dolaska">
          <n-date-picker v-model:value="form.datum_dolaska" type="date" />
        </n-form-item>

        <n-form-item label="Zrakoplov">
          <n-select
            v-model:value="form.id_zrakoplova"
            :options="zrakoplovi.map(z => ({ label: `${z.model} (${z.registracija})`, value: z.id_zrakoplova }))"
            placeholder="Odaberi zrakoplov"
          />
        </n-form-item>

        <n-form-item label="Pista">
          <n-select
            v-model:value="form.id_piste"
            :options="piste.map(p => ({ label: `${p.oznaka} (${p.duljina}m)`, value: p.id_piste }))"
            placeholder="Odaberi pistu"
          />
        </n-form-item>

        <n-form-item label="Zaposlenik">
          <n-select
            v-model:value="form.kreirao_ga"
            :options="zaposlenici.map(z => ({ label: `${z.ime} ${z.prezime}`, value: z.id_zaposlenika }))"
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
      <n-data-table
        :columns="columns"
        :data="letovi"
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
import { ref, onMounted } from 'vue'

const mockZaposlenici = [
  { id_zaposlenika: 1, ime: 'Ivan', prezime: 'Ivić' },
  { id_zaposlenika: 2, ime: 'Ana', prezime: 'Anić' }
]

const mockZrakoplovi = [
  { id_zrakoplova: 1, model: 'Airbus A320', registracija: '9A-ABC' },
  { id_zrakoplova: 2, model: 'Boeing 737', registracija: '9A-XYZ' }
]

const mockPiste = [
  { id_piste: 1, oznaka: '12L', duljina: 2500 },
  { id_piste: 2, oznaka: '30R', duljina: 3000 }
]

const mockLetovi = [
  {
    broj_leta: 101,
    datum_polaska: '2025-06-01',
    datum_dolaska: '2025-06-01',
    id_zrakoplova: 1,
    id_piste: 2,
    kreirao_ga: 1
  },
  {
    broj_leta: 102,
    datum_polaska: '2025-06-02',
    datum_dolaska: '2025-06-02',
    id_zrakoplova: 2,
    id_piste: 1,
    kreirao_ga: 2
  }
]

const letovi = ref<any[]>([])
const zrakoplovi = ref<any[]>([])
const piste = ref<any[]>([])
const zaposlenici = ref<any[]>([])

const form = ref({
  broj_leta: null,
  datum_polaska: null,
  datum_dolaska: null,
  id_zrakoplova: null,
  id_piste: null,
  kreirao_ga: null
})

function fetchAll() {
  zaposlenici.value = mockZaposlenici
  zrakoplovi.value = mockZrakoplovi
  piste.value = mockPiste
  letovi.value = mockLetovi.map(l => ({
    ...l,
    zrakoplov: mockZrakoplovi.find(z => z.id_zrakoplova === l.id_zrakoplova),
    pista: mockPiste.find(p => p.id_piste === l.id_piste),
    kreirao: mockZaposlenici.find(z => z.id_zaposlenika === l.kreirao_ga)
  }))
}

function resetForm() {
  form.value = {
    broj_leta: null,
    datum_polaska: null,
    datum_dolaska: null,
    id_zrakoplova: null,
    id_piste: null,
    kreirao_ga: null
  }
}

function handleSubmit() {
  if (form.value.broj_leta) {
    const index = letovi.value.findIndex(l => l.broj_leta === form.value.broj_leta)
    if (index !== -1) {
      letovi.value[index] = {
        ...form.value,
        zrakoplov: zrakoplovi.value.find(z => z.id_zrakoplova === form.value.id_zrakoplova),
        pista: piste.value.find(p => p.id_piste === form.value.id_piste),
        kreirao: zaposlenici.value.find(z => z.id_zaposlenika === form.value.kreirao_ga)
      }
    }
  } else {
    letovi.value.push({
      broj_leta: Date.now(),
      ...form.value,
      zrakoplov: zrakoplovi.value.find(z => z.id_zrakoplova === form.value.id_zrakoplova),
      pista: piste.value.find(p => p.id_piste === form.value.id_piste),
      kreirao: zaposlenici.value.find(z => z.id_zaposlenika === form.value.kreirao_ga)
    })
  }
  resetForm()
}

function editLet(l: any) {
  form.value = {
    broj_leta: l.broj_leta,
    datum_polaska: l.datum_polaska,
    datum_dolaska: l.datum_dolaska,
    id_zrakoplova: l.id_zrakoplova,
    id_piste: l.id_piste,
    kreirao_ga: l.kreirao_ga
  }
}

function deleteLet(id: number) {
  letovi.value = letovi.value.filter(l => l.broj_leta !== id)
}

const columns = [
  {
    title: 'Broj',
    key: 'broj_leta'
  },
  {
    title: 'Polazak',
    key: 'datum_polaska'
  },
  {
    title: 'Dolazak',
    key: 'datum_dolaska'
  },
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
    render: (row: any) => `${row.kreirao?.ime} ${row.kreirao?.prezime}`
  },
  {
    title: 'Akcije',
    render: (row: any) => [
      h(NButton, { size: 'small', onClick: () => editLet(row) }, { default: () => '✏️' }),
      h(NButton, { size: 'small', type: 'error', onClick: () => deleteLet(row.broj_leta) }, { default: () => '🗑️' })
    ]
  }
]

onMounted(fetchAll)
</script>
