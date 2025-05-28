import axios from 'axios'

const api = axios.create({
  baseURL: 'http://localhost:5233/api'
})

export const getLetovi = () => api.get('/let')
export const createLet = (data: any) => api.post('/let', data)
export const updateLet = (id: number, data: any) => api.put(`/let/${id}`, data)
export const deleteLet = (id: number) => api.delete(`/let/${id}`)

export const getZaposlenici = () => api.get('/zaposlenik')
export const getZrakoplovi = () => api.get('/zrakoplov')
export const getPiste = () => api.get('/pista')
