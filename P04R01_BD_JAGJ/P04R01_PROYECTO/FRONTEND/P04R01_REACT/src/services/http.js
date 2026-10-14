// Función base para hablar con la API. Todos los servicios la usan.

// La URL base sale del archivo .env (VITE_API_URL). Si no existe, usa la del perfil http de la API.
export const API_URL = import.meta.env.VITE_API_URL ?? 'http://localhost:5283'

// Error con más información que un Error normal:
//   status  -> código HTTP (0 si ni siquiera hubo conexión)
//   errores -> errores por campo que manda la API: { email: '...', hobby: '...' }
export class ApiError extends Error {
  constructor(mensaje, status, errores = {}) {
    super(mensaje)
    this.status = status
    this.errores = errores
  }
}

// Hace la petición, convierte la respuesta de JSON y lanza ApiError si algo salió mal.
export async function peticion(ruta, opciones = {}) {
  let respuesta
  try {
    respuesta = await fetch(`${API_URL}${ruta}`, opciones)
  } catch {
    // fetch solo falla así cuando no hay conexión (API apagada, CORS, sin red)
    throw new ApiError('No se pudo conectar con la API. ¿Está corriendo en ' + API_URL + '?', 0)
  }

  // POST, PUT y DELETE del CRUD regresan Ok() sin cuerpo, por eso se lee como texto primero.
  const texto = await respuesta.text()
  let datos = null
  if (texto) {
    try {
      datos = JSON.parse(texto)
    } catch {
      datos = texto // la API a veces regresa el error como texto plano (BadRequest(ex.Message))
    }
  }

  if (!respuesta.ok) {
    let mensaje = `Error ${respuesta.status}`
    if (typeof datos === 'string' && datos) mensaje = datos
    else if (datos?.mensaje) mensaje = datos.mensaje   // nuestro formato: { mensaje, errores }
    else if (datos?.title) mensaje = datos.title       // formato automático de ASP.NET (ProblemDetails)
    throw new ApiError(mensaje, respuesta.status, datos?.errores ?? {})
  }
  return datos
}

// Atajo para enviar JSON por POST
export function postJson(ruta, cuerpo) {
  return peticion(ruta, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(cuerpo),
  })
}
