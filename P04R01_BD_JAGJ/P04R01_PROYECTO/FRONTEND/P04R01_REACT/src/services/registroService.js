// Servicio: llamadas HTTP de la prueba Thona (código postal, procesar persona y finalizar).
import { peticion, postJson } from './http'

// GET api/CodigoPostal/{cp} -> { cp, estado, codEstado, municipio, codMunicipio, colonias: [{ codigo, descripcion }] }
export function consultarCP(cp) {
  return peticion(`/api/CodigoPostal/${cp}`)
}

// POST api/Persona/procesar -> { nombreNormalizado, idUsuario, hobby, preposiciones, avisos, ... }
export function procesarPersona(persona) {
  return postJson('/api/Persona/procesar', persona)
}

// POST api/Persona/finalizar -> guarda en la BD y regresa el registro completo
export function finalizarRegistro(persona, cp, codColonia) {
  return postJson('/api/Persona/finalizar', { persona, cp, codColonia })
}
