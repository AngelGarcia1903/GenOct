// Servicio: llamadas HTTP del CRUD de usuarios (api/usuarios).
// Los componentes NO usan fetch directamente; llaman a estas funciones.
import { peticion } from './http'

const RUTA = '/api/usuarios'

// GET api/usuarios -> lista de usuarios
export function obtenerUsuarios() {
  return peticion(RUTA)
}

// GET api/usuarios/{id} -> un usuario
export function obtenerUsuario(id) {
  return peticion(`${RUTA}/${id}`)
}

// POST api/usuarios -> crea un usuario
export function crearUsuario(usuario) {
  return peticion(RUTA, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(usuario),
  })
}

// PUT api/usuarios/{id} -> actualiza un usuario
export function actualizarUsuario(id, usuario) {
  return peticion(`${RUTA}/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(usuario),
  })
}

// DELETE api/usuarios/{id} -> elimina un usuario
export function eliminarUsuario(id) {
  return peticion(`${RUTA}/${id}`, { method: 'DELETE' })
}
