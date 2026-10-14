// Validaciones del lado del navegador. Son las mismas reglas que aplica la API;
// aquí sirven para avisar al usuario al instante y habilitar/deshabilitar botones.
// Cada función regresa '' si el valor es válido, o el mensaje de error.

export const EDAD_MINIMA = 5

export function validarCP(cp) {
  if (cp === '') return 'Escribe un código postal.'
  if (!/^\d+$/.test(cp)) return 'El código postal solo acepta números.'
  if (cp.length !== 5) return `El código postal debe tener exactamente 5 dígitos (llevas ${cp.length}).`
  return ''
}

export function validarNombre(nombre) {
  const limpio = nombre.trim()
  if (limpio === '') return 'El nombre completo es obligatorio.'
  if (limpio.split(/\s+/).length < 2) return 'Escribe tu nombre y al menos un apellido (ej. Juan Pérez López).'
  if (limpio.length > 100) return 'El nombre no puede tener más de 100 caracteres.'
  return ''
}

export function validarHobby(hobby) {
  if (hobby.trim() === '') return 'El hobby es obligatorio.'
  if (hobby.trim().length > 150) return 'El hobby no puede tener más de 150 caracteres.'
  return ''
}

export function validarEmail(email) {
  const limpio = email.trim()
  if (limpio === '') return 'El email es obligatorio.'
  if (!/^[^@\s]+@[^@\s]+\.[^@\s]{2,}$/.test(limpio)) return 'El email no tiene un formato válido (ej. nombre@dominio.com).'
  return ''
}

// fecha llega del <input type="date"> como "yyyy-MM-dd"
export function validarFechaNacimiento(fecha) {
  if (fecha === '') return 'La fecha de nacimiento es obligatoria.'
  const nacimiento = new Date(fecha + 'T00:00:00') // T00:00 = hora local, evita desfases por zona horaria
  const hoy = new Date()
  hoy.setHours(0, 0, 0, 0)
  if (Number.isNaN(nacimiento.getTime()) || nacimiento.getFullYear() < 1900) return 'La fecha de nacimiento no es válida.'
  if (nacimiento > hoy) return 'La fecha de nacimiento no puede ser futura.'
  if (calcularEdad(nacimiento, hoy) < EDAD_MINIMA) return `Debes tener al menos ${EDAD_MINIMA} años cumplidos.`
  return ''
}

export function calcularEdad(nacimiento, hoy) {
  let edad = hoy.getFullYear() - nacimiento.getFullYear()
  const aunNoCumple =
    hoy.getMonth() < nacimiento.getMonth() ||
    (hoy.getMonth() === nacimiento.getMonth() && hoy.getDate() < nacimiento.getDate())
  if (aunNoCumple) edad--
  return edad
}

// Fecha de hoy en formato "yyyy-MM-dd" (para el atributo max del input date)
export function hoyISO() {
  const hoy = new Date()
  const mes = String(hoy.getMonth() + 1).padStart(2, '0')
  const dia = String(hoy.getDate()).padStart(2, '0')
  return `${hoy.getFullYear()}-${mes}-${dia}`
}
