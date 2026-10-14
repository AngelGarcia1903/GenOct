# Documentación del Frontend — P04R01_REACT

Frontend en **React** para la API `P04R01_APIREST`. Tiene dos pestañas:

1. **Registro Thona**: la prueba técnica de Thona Seguros (`Thona Seguros.pdf`):
   - Característica 1: consulta de código postal.
   - Característica 2: procesamiento de la persona.
   - Característica 3: Finalizar y Reiniciar.
2. **Usuarios (CRUD)**: el CRUD de `TBLUsuarios` que se hizo primero.

Tiene diseño propio con **Tailwind CSS**, íconos (**lucide-react**) y notificaciones (**sonner**).

> 📍 **¿Dónde se cumple cada requisito del PDF?** Ver [`REQUERIMIENTOS_THONA.md`](../../../REQUERIMIENTOS_THONA.md) (archivo, línea, qué se hizo y sección de la guía).

- **Ubicación:** `P04R01_PROYECTO\FRONTEND\P04R01_REACT\`
- **Tecnologías:** React 19, Vite 8, JavaScript (JSX), `fetch` nativo, Tailwind CSS 4, lucide-react y sonner.
- **Historial de versiones:**
  - **v1 (2026-10-06):** CRUD de usuarios.
  - **v2 (2026-10-06):** registro de la prueba Thona, servicios reorganizados y pestañas.
  - **v3 (2026-10-06):** estilos con Tailwind, componentes de interfaz (`components/ui`), indicador de pasos, diálogo de confirmación, toasts y botón Copiar ID. **Sin cambios en el backend.**

---

## 1. Comandos ejecutados

Todos en PowerShell, desde `P04R01_PROYECTO\FRONTEND\`.

| # | Comando | Qué hace |
|---|---|---|
| 1 | `node -v` y `npm -v` | Verifica que Node.js (v24) y npm (v11) estén instalados. |
| 2 | `npm create vite@latest P04R01_REACT -- --template react --no-interactive` | Crea el proyecto base de React + Vite en JavaScript. |
| 3 | `cd P04R01_REACT` | Entra a la carpeta del proyecto. |
| 4 | `npm install` | Descarga las dependencias en `node_modules`. |
| 5 | `npm run lint` | Revisa el código con `oxlint`. Quedó sin avisos. |
| 6 | `npm run build` | Compila para verificar que no haya errores. La carpeta `dist` generada se borró. |
| 7 | `npm run dev` | Servidor de desarrollo en `http://localhost:5173`. |
| 8 | `npm install tailwindcss @tailwindcss/vite lucide-react sonner` | **(v3)** Instala Tailwind CSS 4 y su plugin de Vite, los íconos y las notificaciones. |

En la v2 no se instalaron paquetes.

> ⚠️ Después de instalar Tailwind hay que **reiniciar** `npm run dev` (Ctrl+C y otra vez `npm run dev`). Si no, la página se ve sin estilos.

### Cómo correr el proyecto

1. **Base de datos:** debe existir `Thona_PT` con el script `BD\P04R01_BD_AlterTable_Registro.sql` ya ejecutado. Ya se ejecutó en tu servidor.
2. **API:** en Visual Studio, perfil **http** (`http://localhost:5283`). Necesita internet para consultar los códigos postales en Thona.
3. **Frontend:**
   ```bash
   cd "C:\Users\Angel\Desktop\TR NETWORK\PROYECTO\P04R01_BD_JAGJ\P04R01_PROYECTO\FRONTEND\P04R01_REACT"
   ```
   ```bash
   npm run dev
   ```
4. Abre `http://localhost:5173`.

> En otra computadora, ejecuta `npm install` antes de `npm run dev`.

---

## 2. Estructura de archivos

```
P04R01_REACT/
├── .env                              ← URL de la API (VITE_API_URL)
├── index.html                        ← página base; React se monta en <div id="root">
├── package.json
├── vite.config.js                    ← (v3) plugins: react() + tailwindcss()
└── src/
    ├── main.jsx                      ← punto de entrada: monta <App /> e importa index.css
    ├── index.css                     ← (v3) Tailwind + colores propios (@theme)
    ├── App.jsx                       ← (v2) pestañas: Registro Thona | Usuarios (CRUD)
    ├── pages/                        ← (v2) una "pantalla" completa por archivo
    │   ├── RegistroPage.jsx          ← (v2) prueba Thona: une las 3 características
    │   └── UsuariosPage.jsx          ← (v2) el CRUD (antes estaba en App.jsx)
    ├── components/
    │   ├── ui/                       ← (v3) piezas genéricas de interfaz
    │   │   ├── Boton.jsx             ←   botón con variantes, ícono y estado "cargando"
    │   │   ├── Tarjeta.jsx           ←   contenedor con encabezado (número de paso o ícono)
    │   │   ├── Campo.jsx             ←   etiqueta + input (children) + ayuda/error
    │   │   ├── Alerta.jsx            ←   mensajes error/aviso/info/éxito
    │   │   ├── DialogoConfirmar.jsx  ←   confirmación con <dialog>
    │   │   └── estilos.js            ←   claseInput(): clases compartidas de inputs
    │   ├── registro/                 ← (v2) piezas de la prueba Thona
    │   │   ├── Pasos.jsx             ←   (v3) indicador de avance 1-2-3
    │   │   ├── CodigoPostal.jsx      ←   Característica 1
    │   │   ├── PersonaForm.jsx       ←   Característica 2 (captura)
    │   │   ├── ResultadoPersona.jsx  ←   Característica 2 (resultado)
    │   │   ├── Preposiciones.jsx     ←   lista ordenable
    │   │   ├── AccionesRegistro.jsx  ←   Característica 3 (Finalizar / Reiniciar)
    │   │   └── ResumenRegistro.jsx   ←   toque personal: ficha imprimible
    │   ├── UsuarioForm.jsx           ← CRUD: formulario crear/editar
    │   ├── UsuariosTabla.jsx         ← CRUD: tabla
    │   └── BuscarUsuario.jsx         ← CRUD: búsqueda por ID
    ├── services/                     ← comunicación con la API
    │   ├── http.js                   ← (v2) fetch base + ApiError
    │   ├── registroService.js        ← (v2) código postal, procesar y finalizar
    │   └── usuariosService.js        ← CRUD (v2: ahora usa http.js)
    └── utils/
        └── validaciones.js           ← (v2) reglas de validación del navegador
```

**Organización:** `components/ui` contiene piezas genéricas sin lógica de negocio, `pages` contiene pantallas completas con el estado compartido, `components` contiene piezas reutilizables, `services` habla con la API y `utils` tiene funciones puras sin React.

---

## 3. Cómo se conecta todo

```
App (pestañas)
├─ RegistroPage   estado: datosCP, coloniaSel, resultado, personaEnviada, finalizado, guardando, registro, error, confirmando, intento
│   ├─ Pasos                 props: pasos
│   ├─ ResumenRegistro       props: registro              (después de finalizar)
│   ├─ <fieldset disabled={finalizado}>   ← bloquea la edición después de Finalizar
│   │   ├─ CodigoPostal      props: datos, coloniaSel, onDatos, onColonia
│   │   └─ PersonaForm       props: procesado, onProcesado, onCambio
│   ├─ ResultadoPersona      props: resultado
│   │   └─ Preposiciones     props: lista
│   ├─ AccionesRegistro      props: requisitos, finalizado, guardando, onFinalizar, onReiniciar
│   └─ DialogoConfirmar      props: abierto, titulo, mensaje, onConfirmar, onCancelar
└─ UsuariosPage (CRUD)
    ├─ UsuarioForm · UsuariosTabla · BuscarUsuario

componentes ──► services/*.js ──► http.js (fetch) ──► API .NET 8 (localhost:5283)
                                                          ├─► API de Thona (códigos postales, XML)
                                                          └─► SQL Server Thona_PT.TBLUsuarios
```

### Endpoints que usa el frontend

| Endpoint | Función | Dónde |
|---|---|---|
| `GET /api/CodigoPostal/{cp}` | `consultarCP(cp)` | `CodigoPostal` |
| `POST /api/Persona/procesar` | `procesarPersona(persona)` | `PersonaForm` |
| `POST /api/Persona/finalizar` | `finalizarRegistro(persona, cp, codColonia)` | `RegistroPage.finalizar` |
| `GET /api/usuarios` | `obtenerUsuarios()` | `UsuariosPage` |
| `GET /api/usuarios/{id}` | `obtenerUsuario(id)` | `BuscarUsuario` |
| `POST /api/usuarios` | `crearUsuario(u)` | `UsuariosPage` |
| `PUT /api/usuarios/{id}` | `actualizarUsuario(id, u)` | `UsuariosPage` |
| `DELETE /api/usuarios/{id}` | `eliminarUsuario(id)` | `UsuariosPage` |

---

## 4. Cómo se cumple cada requisito del PDF

> Resumen. El detalle con **archivo:línea**, el manejo de errores por capas y las referencias a la guía están en [`REQUERIMIENTOS_THONA.md`](../../../REQUERIMIENTOS_THONA.md).

### Característica 1 — Código postal (`CodigoPostal.jsx`)
| Requisito | Cómo se cumple |
|---|---|
| Campo CP, botón y sección de resultados | Un `<input id="cp">`, el botón "Consultar" y un bloque con estado, municipio y colonias. |
| Solo números | `onChange` elimina todo lo que no sea dígito (`replace(/\D/g, '')`) y avisa "Solo se aceptan números". Además lleva `inputMode="numeric"`. |
| Exactamente 5 dígitos | `maxLength={5}` y `validarCP()` exige longitud 5. |
| Botón habilitado solo con CP válido | `disabled={!cpValido \|\| consultando}` |
| Mensaje de validación | Debajo del campo: "debe tener exactamente 5 dígitos (llevas N)". |
| Mostrar estado y código | "Estado: CIUDAD DE MEXICO (código 009)", y también el municipio. |
| Lista seleccionable de colonias | Un `<select>` con `datos.colonias.map(...)`. |
| Código de la colonia seleccionada | "Código de la colonia: 000947". |
| Respuesta vacía | La API regresa 404 → "No se encontró información para el código postal 99999." |
| Error de comunicación | La API regresa 503 o 502, o la API local está apagada (status 0) → mensaje claro, sin que la página falle. |

### Característica 2 — Persona (`PersonaForm.jsx` + `ResultadoPersona.jsx` + `Preposiciones.jsx`)
| Requisito | Cómo se cumple |
|---|---|
| Campos nombre, hobby, fecha y email | Formulario controlado con los 4 campos. |
| Normalizar, camel case, preposiciones e ID | Lo hace la API (`PersonaService.cs`). El frontend muestra el resultado. |
| Lista de preposiciones ordenable | `Preposiciones.jsx`: botones A → Z, Z → A y Orden original. |
| Email con formato, fecha no futura, mayor de 5 años | `utils/validaciones.js` valida en vivo y la API vuelve a validar. Los errores del servidor se muestran en el campo correspondiente. |
| Resultado: nombre normalizado, ID, hobby y preposiciones | `ResultadoPersona.jsx`. También muestra nombre(s) y apellidos por separado, la edad y los avisos (caracteres eliminados, falta de segundo apellido). |
| Respuestas vacías e instrucciones claras | Mensajes por campo al salir de él o al presionar Procesar, y una instrucción del orden en que se escribe el nombre. |

### Característica 3 — Pantalla interactiva (`AccionesRegistro.jsx` + `RegistroPage.jsx`)
| Requisito | Cómo se cumple |
|---|---|
| Botones Finalizar y Reiniciar | Están en `AccionesRegistro`. |
| No editar después de Finalizar | `<fieldset disabled={finalizado}>` deshabilita todos los inputs, selects y botones de captura. |
| Reiniciar bloqueado si no se ha finalizado | `disabled={!finalizado}`. |
| Toque personal | 1) Indicador de pasos y checklist de lo que falta. 2) Finalizar **guarda en la BD**, y el servidor vuelve a validar todo. 3) Ficha del registro con botón **Imprimir / Guardar PDF**. 4) Diálogo de confirmación propio. 5) Si editas un dato ya procesado, el resultado se invalida. 6) Botón Copiar ID y notificaciones. 7) Diseño responsivo. |

---

## 5. Archivos y componentes, uno por uno

### 5.1 `.env`
Guarda la URL base de la API. Solo las variables `VITE_*` llegan al navegador. *Después de editarlo, reinicia `npm run dev`.*

```bash
# URL base de la API (perfil "http" de Visual Studio).
# En Vite, solo las variables que empiezan con VITE_ llegan al código del navegador.
VITE_API_URL=http://localhost:5283
```

### 5.2 `index.html`
La única página HTML. React dibuja todo dentro de `<div id="root">`.

```html
<!doctype html>
<html lang="es">
  <head>
    <meta charset="UTF-8" />
    <link rel="icon" type="image/svg+xml" href="/favicon.svg" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Usuarios - P04R01</title>
  </head>
  <body>
    <div id="root"></div>
    <script type="module" src="/src/main.jsx"></script>
  </body>
</html>
```

### 5.3 `src/main.jsx`
Punto de entrada: monta `<App />` dentro de `#root`. `StrictMode` ayuda a detectar errores en desarrollo.

```jsx
import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.jsx'
import './index.css' // estilos globales + Tailwind

// Punto de entrada: monta el componente <App /> dentro del <div id="root"> de index.html.
createRoot(document.getElementById('root')).render(
  <StrictMode>
    <App />
  </StrictMode>,
)
```

### 5.3b `vite.config.js` (v3)
Agrega el plugin de Tailwind.

```js
import react from '@vitejs/plugin-react'
import tailwindcss from '@tailwindcss/vite'
import { defineConfig } from 'vite'

// https://vite.dev/config/
export default defineConfig({
  // tailwindcss(): genera solo las clases de Tailwind que se usan en el código
  plugins: [react(), tailwindcss()],
})
```

### 5.3c `src/index.css` (v3)
Carga Tailwind, indica dónde buscar las clases (`@source`) y define la paleta `marca-*` con `@theme`.

```css
/* Tailwind CSS v4: esta línea carga el reset (preflight) y todas las clases utilitarias */
@import "tailwindcss";

/* Le dice a Tailwind en qué archivos buscar las clases que usamos (ruta relativa a este .css).
   Sin esto, la detección automática no encontraba los .jsx (la carpeta está dentro de otro repositorio git). */
@source "./";

/* Tokens propios del proyecto: se usan como clases (ej. bg-marca-600, text-marca-700) */
@theme {
  --font-sans: "Segoe UI", system-ui, -apple-system, Roboto, "Helvetica Neue", Arial, sans-serif;

  --color-marca-50: oklch(0.97 0.02 265);
  --color-marca-100: oklch(0.93 0.04 265);
  --color-marca-200: oklch(0.87 0.07 265);
  --color-marca-500: oklch(0.58 0.19 265);
  --color-marca-600: oklch(0.51 0.21 265);
  --color-marca-700: oklch(0.45 0.2 265);
  --color-marca-900: oklch(0.3 0.12 265);
}

body {
  @apply bg-slate-50 text-slate-800 antialiased;
}
```

### 5.4 `src/App.jsx` (v3)
Componente raíz: encabezado, pestañas con íconos y la página activa. Contiene el `<Toaster />` de sonner para las notificaciones.

```jsx
import { useState } from 'react'
import { Toaster } from 'sonner'
import { ClipboardList, Users, ShieldCheck } from 'lucide-react'
import RegistroPage from './pages/RegistroPage'
import UsuariosPage from './pages/UsuariosPage'

const PESTANAS = [
  { id: 'registro', texto: 'Registro Thona', Icono: ClipboardList },
  { id: 'usuarios', texto: 'Usuarios (CRUD)', Icono: Users },
]

// Componente raíz: encabezado, pestañas y la página activa. Sin react-router; basta un estado.
function App() {
  const [vista, setVista] = useState('registro') // 'registro' | 'usuarios'

  return (
    <div className="min-h-screen">
      {/* print:hidden -> no aparece al imprimir la ficha del registro */}
      <header className="border-b border-slate-200 bg-white print:hidden">
        <div className="mx-auto flex max-w-6xl flex-wrap items-center justify-between gap-4 px-4 py-4">
          <div className="flex items-center gap-3">
            <span className="flex size-10 items-center justify-center rounded-xl bg-marca-600 text-white">
              <ShieldCheck className="size-6" aria-hidden="true" />
            </span>
            <div>
              <p className="text-base font-bold leading-tight text-slate-900">Plataforma Digital</p>
              <p className="text-xs text-slate-500">Prueba técnica · Thona Seguros</p>
            </div>
          </div>

          <nav className="flex gap-1 rounded-xl bg-slate-100 p-1" aria-label="Secciones">
            {PESTANAS.map(({ id, texto, Icono }) => (
              <button
                key={id}
                type="button"
                onClick={() => setVista(id)}
                aria-current={vista === id ? 'page' : undefined}
                className={`inline-flex items-center gap-2 rounded-lg px-3 py-1.5 text-sm font-medium transition ${
                  vista === id ? 'bg-white text-marca-700 shadow-sm' : 'text-slate-600 hover:text-slate-900'
                }`}
              >
                <Icono className="size-4" aria-hidden="true" />
                {texto}
              </button>
            ))}
          </nav>
        </div>
      </header>

      <main className="mx-auto max-w-6xl px-4 py-8">
        {vista === 'registro' ? <RegistroPage /> : <UsuariosPage />}
      </main>

      {/* Contenedor de notificaciones (toasts). Se muestran con toast.success('...') desde cualquier componente */}
      <Toaster position="top-right" richColors closeButton />
    </div>
  )
}

export default App
```

### 5.5 `src/services/http.js` (v2)
Función base para todas las peticiones:
- Si `fetch` falla (API apagada, CORS, sin red), lanza `ApiError` con `status = 0` y un mensaje claro.
- Lee la respuesta como texto e intenta convertirla de JSON, porque algunos endpoints regresan cuerpo vacío o texto plano.
- Si la respuesta no es correcta, toma el mensaje de `{ mensaje }` (nuestro formato), de `title` (formato automático de ASP.NET) o del texto, y adjunta `errores` por campo.

```js
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
```

### 5.6 `src/services/registroService.js` (v2)
Funciones de la prueba Thona. Cada una es una línea gracias a `http.js`.

```js
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
```

### 5.7 `src/services/usuariosService.js` (v2: usa `http.js`)
Funciones del CRUD. Antes tenía su propio `fetch` y `manejarRespuesta`; ahora reutiliza `peticion()`.

```js
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
```

### 5.8 `src/utils/validaciones.js` (v2)
Funciones puras, sin React: reciben un valor y regresan `''` si es válido o el mensaje de error. Son las mismas reglas que la API, y sirven para avisar al instante y habilitar los botones.
La fecha se construye con `'T00:00:00'` para que sea hora local; sin eso, `new Date('1995-03-07')` se interpreta en UTC y en México puede dar el día anterior.

```js
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
```

### 5.4b Componentes de interfaz `src/components/ui/` (v3)
Piezas genéricas reutilizadas por todas las pantallas (Guía §29–§33).

**`Boton.jsx`**: variantes de color, ícono opcional y spinner cuando `cargando`. Las demás props pasan al `<button>` con `{...resto}`.

```jsx
import { LoaderCircle } from 'lucide-react'

const VARIANTES = {
  primario: 'bg-marca-600 text-white shadow-sm hover:bg-marca-700 focus-visible:outline-marca-600',
  secundario: 'bg-white text-slate-700 shadow-sm ring-1 ring-inset ring-slate-300 hover:bg-slate-50 focus-visible:outline-slate-400',
  peligro: 'bg-red-600 text-white shadow-sm hover:bg-red-700 focus-visible:outline-red-600',
  exito: 'bg-emerald-600 text-white shadow-sm hover:bg-emerald-700 focus-visible:outline-emerald-600',
  fantasma: 'text-slate-600 hover:bg-slate-100 focus-visible:outline-slate-400',
}

// Botón reutilizable.
// Props propias: variante, icono (componente de lucide-react), cargando.
// Cualquier otra prop (onClick, type, disabled, title...) se pasa tal cual al <button> con {...resto}.
function Boton({ variante = 'primario', icono: Icono, cargando = false, className = '', children, ...resto }) {
  return (
    <button
      type="button"
      {...resto}
      disabled={resto.disabled || cargando}
      className={
        'inline-flex items-center justify-center gap-2 rounded-lg px-4 py-2 text-sm font-semibold transition ' +
        'focus-visible:outline-2 focus-visible:outline-offset-2 disabled:cursor-not-allowed disabled:opacity-50 ' +
        `${VARIANTES[variante]} ${className}`
      }
    >
      {cargando ? (
        <LoaderCircle className="size-4 animate-spin" aria-hidden="true" />
      ) : (
        Icono && <Icono className="size-4" aria-hidden="true" />
      )}
      {children}
    </button>
  )
}

export default Boton
```

**`Tarjeta.jsx`**: contenedor con encabezado. Si recibe `paso`, muestra el número, o una palomita cuando está `completado`.

```jsx
import { Check } from 'lucide-react'

// Contenedor blanco con encabezado. "children" es todo lo que se escribe entre <Tarjeta> y </Tarjeta>.
// Props: titulo, subtitulo, icono, paso (número opcional), completado, acciones (JSX a la derecha del título)
function Tarjeta({ titulo, subtitulo, icono: Icono, paso, completado = false, acciones, className = '', children }) {
  return (
    <section className={`rounded-2xl border border-slate-200 bg-white p-5 shadow-sm sm:p-6 ${className}`}>
      {titulo && (
        <header className="mb-5 flex items-start gap-3">
          {paso !== undefined ? (
            <span
              className={`flex size-9 shrink-0 items-center justify-center rounded-full text-sm font-bold ${
                completado ? 'bg-emerald-100 text-emerald-700' : 'bg-marca-100 text-marca-700'
              }`}
            >
              {completado ? <Check className="size-5" aria-label="Completado" /> : paso}
            </span>
          ) : (
            Icono && (
              <span className="flex size-9 shrink-0 items-center justify-center rounded-full bg-marca-100 text-marca-700">
                <Icono className="size-5" aria-hidden="true" />
              </span>
            )
          )}
          <div className="min-w-0 flex-1">
            <h2 className="flex items-center gap-2 text-lg font-semibold text-slate-900">
              {paso !== undefined && Icono && <Icono className="size-5 text-slate-400" aria-hidden="true" />}
              {titulo}
            </h2>
            {subtitulo && <p className="mt-0.5 text-sm text-slate-500">{subtitulo}</p>}
          </div>
          {acciones && <div className="shrink-0">{acciones}</div>}
        </header>
      )}
      {children}
    </section>
  )
}

export default Tarjeta
```

**`Campo.jsx`**: etiqueta + input (`children`) + ayuda o error.

```jsx
import { CircleAlert } from 'lucide-react'

// Envuelve un input con su etiqueta, texto de ayuda y mensaje de error.
// El input va como "children":  <Campo id="email" etiqueta="Email" error={...}><input id="email" /></Campo>
function Campo({ id, etiqueta, error, ayuda, children }) {
  return (
    <div className="space-y-1.5">
      <label htmlFor={id} className="block text-sm font-medium text-slate-700">
        {etiqueta}
      </label>
      {children}
      {error ? (
        <p className="flex items-start gap-1.5 text-sm text-red-600" role="alert">
          <CircleAlert className="mt-0.5 size-4 shrink-0" aria-hidden="true" />
          {error}
        </p>
      ) : (
        ayuda && <p className="text-xs text-slate-500">{ayuda}</p>
      )}
    </div>
  )
}

export default Campo
```

**`estilos.js`**: clases compartidas de inputs y selects (borde rojo si hay error).

```js
// Clases de Tailwind compartidas. Se guardan en un .js (no .jsx) porque no son componentes.

// Clases de un <input> o <select>. Si hay error, el borde y el anillo de foco se pintan de rojo.
export function claseInput(conError = false) {
  const base =
    'block w-full rounded-lg border bg-white px-3 py-2 text-sm text-slate-900 shadow-sm outline-none transition ' +
    'placeholder:text-slate-400 focus:ring-4 ' +
    'disabled:cursor-not-allowed disabled:bg-slate-100 disabled:text-slate-500'
  const color = conError
    ? 'border-red-400 focus:border-red-500 focus:ring-red-100'
    : 'border-slate-300 focus:border-marca-500 focus:ring-marca-100'
  return `${base} ${color}`
}
```

**`Alerta.jsx`**: mensajes de tipo error, aviso, info o éxito.

```jsx
import { CircleAlert, TriangleAlert, Info, CircleCheck } from 'lucide-react'

// Un objeto de configuración por tipo: colores e ícono.
const TIPOS = {
  error: { clases: 'border-red-200 bg-red-50 text-red-800', Icono: CircleAlert },
  aviso: { clases: 'border-amber-200 bg-amber-50 text-amber-800', Icono: TriangleAlert },
  info: { clases: 'border-sky-200 bg-sky-50 text-sky-800', Icono: Info },
  exito: { clases: 'border-emerald-200 bg-emerald-50 text-emerald-800', Icono: CircleCheck },
}

// Caja de mensaje. Props: tipo ('error' | 'aviso' | 'info' | 'exito'), titulo, children
function Alerta({ tipo = 'info', titulo, className = '', children }) {
  const { clases, Icono } = TIPOS[tipo]
  return (
    <div
      className={`flex gap-3 rounded-lg border px-4 py-3 text-sm ${clases} ${className}`}
      role={tipo === 'error' ? 'alert' : 'status'}
    >
      <Icono className="mt-0.5 size-5 shrink-0" aria-hidden="true" />
      <div className="min-w-0">
        {titulo && <p className="font-semibold">{titulo}</p>}
        {children && <div className={titulo ? 'mt-0.5' : ''}>{children}</div>}
      </div>
    </div>
  )
}

export default Alerta
```

**`DialogoConfirmar.jsx`**: confirmación con `<dialog>`, `useRef` y `useEffect`. Reemplaza a `window.confirm`.

```jsx
import { useEffect, useRef } from 'react'
import { TriangleAlert } from 'lucide-react'
import Boton from './Boton'

// Ventana de confirmación con el elemento nativo <dialog> (reemplaza a window.confirm).
// Props: abierto, titulo, mensaje, textoConfirmar, peligro, onConfirmar, onCancelar
function DialogoConfirmar({ abierto, titulo, mensaje, textoConfirmar = 'Confirmar', peligro = false, onConfirmar, onCancelar }) {
  // useRef guarda una referencia al elemento <dialog> real del DOM
  const ref = useRef(null)

  // Efecto: sincroniza la prop "abierto" con el diálogo del navegador (showModal / close).
  useEffect(() => {
    const dialogo = ref.current
    if (abierto && !dialogo.open) dialogo.showModal()
    if (!abierto && dialogo.open) dialogo.close()
  }, [abierto])

  return (
    <dialog
      ref={ref}
      // La tecla Esc dispara "cancel": lo tratamos igual que presionar Cancelar
      onCancel={(e) => {
        e.preventDefault()
        onCancelar()
      }}
      className="m-auto w-[min(28rem,calc(100%-2rem))] rounded-2xl bg-white p-6 shadow-xl backdrop:bg-slate-900/50"
    >
      <div className="flex gap-4">
        <span
          className={`flex size-10 shrink-0 items-center justify-center rounded-full ${
            peligro ? 'bg-red-100 text-red-600' : 'bg-marca-100 text-marca-700'
          }`}
        >
          <TriangleAlert className="size-5" aria-hidden="true" />
        </span>
        <div>
          <h3 className="text-base font-semibold text-slate-900">{titulo}</h3>
          <p className="mt-1 text-sm text-slate-600">{mensaje}</p>
        </div>
      </div>
      <div className="mt-6 flex justify-end gap-2">
        <Boton variante="secundario" onClick={onCancelar}>Cancelar</Boton>
        <Boton variante={peligro ? 'peligro' : 'primario'} onClick={onConfirmar}>
          {textoConfirmar}
        </Boton>
      </div>
    </dialog>
  )
}

export default DialogoConfirmar
```

### 5.9 `src/pages/RegistroPage.jsx` (v3)
El "cerebro" de la prueba:
- Guarda el estado que comparten las 3 características.
- `pendientes` se **calcula** en cada render (estado derivado) y decide si Finalizar se habilita.
- `requisitos` y `pasos` se calculan en cada render: alimentan el checklist y el indicador de avance.
- Finalizar abre el `DialogoConfirmar` (`confirmando = true`); al confirmar, `finalizar()` llama a la API y, si sale bien, marca `finalizado = true`. Eso bloquea el `<fieldset>` y muestra el resumen.
- `reiniciar()` limpia todo y aumenta `intento`. Como `intento` es la `key` del `<fieldset>`, React vuelve a crear `CodigoPostal` y `PersonaForm` vacíos.

```jsx
import { useState } from 'react'
import { toast } from 'sonner'
import { Lock } from 'lucide-react'
import Pasos from '../components/registro/Pasos'
import CodigoPostal from '../components/registro/CodigoPostal'
import PersonaForm from '../components/registro/PersonaForm'
import ResultadoPersona from '../components/registro/ResultadoPersona'
import AccionesRegistro from '../components/registro/AccionesRegistro'
import ResumenRegistro from '../components/registro/ResumenRegistro'
import Alerta from '../components/ui/Alerta'
import DialogoConfirmar from '../components/ui/DialogoConfirmar'
import { finalizarRegistro } from '../services/registroService'

// Página de la prueba Thona. Une las 3 características y guarda el estado que comparten.
function RegistroPage() {
  const [datosCP, setDatosCP] = useState(null)           // respuesta de api/CodigoPostal
  const [coloniaSel, setColoniaSel] = useState('')       // código de la colonia elegida
  const [resultado, setResultado] = useState(null)       // respuesta de api/Persona/procesar
  const [personaEnviada, setPersonaEnviada] = useState(null) // lo que se mandó a procesar (se reenvía al finalizar)
  const [finalizado, setFinalizado] = useState(false)
  const [guardando, setGuardando] = useState(false)
  const [registro, setRegistro] = useState(null)         // respuesta de api/Persona/finalizar
  const [error, setError] = useState('')
  const [confirmando, setConfirmando] = useState(false)  // diálogo "¿Seguro que deseas finalizar?"
  const [intento, setIntento] = useState(0)              // se usa como "key" para reiniciar los formularios

  // Estado derivado: requisitos para finalizar (también son las instrucciones para el usuario)
  const cpListo = Boolean(datosCP && coloniaSel)
  const requisitos = [
    { texto: datosCP ? 'Código postal consultado' : 'Consultar un código postal válido', cumplido: Boolean(datosCP) },
    { texto: 'Seleccionar una colonia', cumplido: Boolean(coloniaSel) },
    { texto: 'Procesar los datos de la persona', cumplido: Boolean(resultado) },
  ]
  const pasos = [
    { titulo: 'Código postal', completado: cpListo },
    { titulo: 'Datos de la persona', completado: Boolean(resultado) },
    { titulo: 'Finalizar', completado: finalizado },
  ]

  function manejarDatosCP(datos) {
    setDatosCP(datos)
    setColoniaSel('') // colonias nuevas -> la selección anterior ya no aplica
  }

  function manejarProcesado(res, datosEnviados) {
    setResultado(res)
    setPersonaEnviada(datosEnviados)
    toast.success('Datos procesados', { description: `ID generado: ${res.idUsuario}` })
  }

  async function finalizar() {
    setConfirmando(false)
    setGuardando(true)
    setError('')
    try {
      const guardado = await finalizarRegistro(personaEnviada, datosCP.cp, coloniaSel)
      setRegistro(guardado)
      setFinalizado(true)
      toast.success(`Registro #${guardado.idRegistro} guardado`)
    } catch (err) {
      setError(err.message)
      toast.error('No se pudo finalizar')
    } finally {
      setGuardando(false)
    }
  }

  function reiniciar() {
    setDatosCP(null)
    setColoniaSel('')
    setResultado(null)
    setPersonaEnviada(null)
    setRegistro(null)
    setError('')
    setFinalizado(false)
    setIntento((n) => n + 1) // nueva key -> CodigoPostal y PersonaForm se vuelven a crear vacíos
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }

  return (
    <div className="space-y-6">
      <div className="print:hidden">
        <h1 className="text-2xl font-bold tracking-tight text-slate-900">Registro de usuario</h1>
        <p className="mt-1 text-sm text-slate-500">
          Consulta tu código postal, captura tus datos y finaliza para guardar el registro.
        </p>
      </div>

      <div className="print:hidden">
        <Pasos pasos={pasos} />
      </div>

      {finalizado && (
        <div className="print:hidden">
          <Alerta tipo="info" titulo="Información finalizada">
            <span className="inline-flex items-center gap-1.5">
              <Lock className="size-3.5" aria-hidden="true" />
              Los datos quedaron bloqueados y ya no se pueden editar.
            </span>
          </Alerta>
        </div>
      )}

      {registro && <ResumenRegistro registro={registro} />}

      {/* fieldset disabled deshabilita TODOS los inputs y botones de adentro: así ya no se puede editar.
          key={intento}: al reiniciar, React vuelve a crear los formularios vacíos. */}
      <fieldset
        key={intento}
        disabled={finalizado || guardando}
        className={`grid min-w-0 items-start gap-6 lg:grid-cols-2 print:hidden ${finalizado ? 'opacity-75' : ''}`}
      >
        <CodigoPostal
          datos={datosCP}
          coloniaSel={coloniaSel}
          onDatos={manejarDatosCP}
          onColonia={setColoniaSel}
        />
        <PersonaForm
          procesado={Boolean(resultado)}
          onProcesado={manejarProcesado}
          onCambio={() => setResultado(null)}
        />
      </fieldset>

      {resultado && (
        <div className="print:hidden">
          <ResultadoPersona resultado={resultado} />
        </div>
      )}

      {error && <Alerta tipo="error" titulo="No se pudo finalizar">{error}</Alerta>}

      <AccionesRegistro
        requisitos={requisitos}
        finalizado={finalizado}
        guardando={guardando}
        onFinalizar={() => setConfirmando(true)}
        onReiniciar={reiniciar}
      />

      <DialogoConfirmar
        abierto={confirmando}
        titulo="¿Finalizar el registro?"
        mensaje="Se guardará en la base de datos y ya no podrás editar la información."
        textoConfirmar="Sí, finalizar"
        onConfirmar={finalizar}
        onCancelar={() => setConfirmando(false)}
      />
    </div>
  )
}

export default RegistroPage
```

### 5.9b `src/components/registro/Pasos.jsx` (v3)
Indicador de avance de 3 pasos (Pendiente / Completado).

```jsx
import { Check } from 'lucide-react'

// Indicador de avance (stepper). Props: pasos -> [{ titulo, completado }]
function Pasos({ pasos }) {
  return (
    <ol className="grid gap-3 sm:grid-cols-3">
      {pasos.map((paso, i) => (
        <li
          key={paso.titulo}
          className={`flex items-center gap-3 rounded-xl border px-4 py-3 transition ${
            paso.completado ? 'border-emerald-200 bg-emerald-50' : 'border-slate-200 bg-white'
          }`}
        >
          <span
            className={`flex size-8 shrink-0 items-center justify-center rounded-full text-sm font-bold ${
              paso.completado ? 'bg-emerald-600 text-white' : 'bg-slate-100 text-slate-500'
            }`}
          >
            {paso.completado ? <Check className="size-4" aria-hidden="true" /> : i + 1}
          </span>
          <div className="min-w-0">
            <p className="text-sm font-semibold text-slate-900">{paso.titulo}</p>
            <p className={`text-xs ${paso.completado ? 'text-emerald-700' : 'text-slate-500'}`}>
              {paso.completado ? 'Completado' : 'Pendiente'}
            </p>
          </div>
        </li>
      ))}
    </ol>
  )
}

export default Pasos
```

### 5.10 `src/components/registro/CodigoPostal.jsx` (v3)
Característica 1. El texto del CP es estado **local**; los datos consultados y la colonia elegida viven en el padre, porque Finalizar los necesita.

```jsx
import { useState } from 'react'
import { MapPin, Search } from 'lucide-react'
import { consultarCP } from '../../services/registroService'
import { validarCP } from '../../utils/validaciones'
import Tarjeta from '../ui/Tarjeta'
import Campo from '../ui/Campo'
import Boton from '../ui/Boton'
import Alerta from '../ui/Alerta'
import { claseInput } from '../ui/estilos'

// Característica 1: consulta de información a partir de un Código Postal.
// Props:
//   datos       -> respuesta de la API (o null si aún no hay consulta válida). Vive en el padre.
//   coloniaSel  -> código de la colonia seleccionada ('' si ninguna)
//   onDatos     -> avisa al padre cuando llegan datos nuevos (o null para limpiarlos)
//   onColonia   -> avisa al padre qué colonia se seleccionó
function CodigoPostal({ datos, coloniaSel, onDatos, onColonia }) {
  const [cp, setCp] = useState('')
  const [tocado, setTocado] = useState(false)       // para no mostrar errores antes de que escriba algo
  const [avisoLetras, setAvisoLetras] = useState('') // si intentó escribir algo que no es número
  const [consultando, setConsultando] = useState(false)
  const [error, setError] = useState('')

  // Estado DERIVADO: se calcula en cada render a partir de "cp"; no se guarda en otro useState.
  const errorCP = validarCP(cp)
  const cpValido = errorCP === ''

  function manejarCambio(evento) {
    const escrito = evento.target.value
    const soloNumeros = escrito.replace(/\D/g, '') // regla: aceptar únicamente números
    setAvisoLetras(escrito !== soloNumeros ? 'Solo se aceptan números; se ignoraron los demás caracteres.' : '')
    setCp(soloNumeros)
    setTocado(true)
    setError('')
    if (datos) onDatos(null) // si cambia el CP, los resultados anteriores ya no aplican
  }

  async function consultar(evento) {
    evento.preventDefault()
    if (!cpValido) return
    setConsultando(true)
    setError('')
    try {
      const respuesta = await consultarCP(cp)
      onDatos(respuesta)
    } catch (err) {
      // 404 sin información, 503 sin comunicación, 502 error del servicio, 0 API apagada...
      setError(err.message)
      onDatos(null)
    } finally {
      setConsultando(false)
    }
  }

  const coloniaElegida = datos?.colonias.find((c) => c.codigo === coloniaSel)
  const completado = Boolean(datos && coloniaSel)

  return (
    <Tarjeta paso={1} completado={completado} icono={MapPin} titulo="Código postal"
      subtitulo="Consulta el estado y las colonias de tu código postal.">
      <form onSubmit={consultar} className="flex items-start gap-2">
        <div className="flex-1">
          <Campo id="cp" etiqueta="Código postal" error={tocado && !cpValido ? errorCP : ''}
            ayuda="5 dígitos, solo números.">
            <input
              id="cp"
              inputMode="numeric"
              autoComplete="postal-code"
              maxLength={5}
              placeholder="Ej. 06700"
              value={cp}
              onChange={manejarCambio}
              onBlur={() => setTocado(true)}
              aria-invalid={tocado && !cpValido}
              className={`${claseInput(tocado && !cpValido)} font-mono text-base tracking-widest`}
            />
          </Campo>
        </div>
        {/* El botón solo se habilita cuando el CP es válido. mt-7 lo alinea con el input (debajo de la etiqueta) */}
        <Boton type="submit" icono={Search} cargando={consultando} disabled={!cpValido} className="mt-7">
          {consultando ? 'Consultando' : 'Consultar'}
        </Boton>
      </form>

      <div className="mt-4 space-y-3">
        {avisoLetras && <Alerta tipo="aviso">{avisoLetras}</Alerta>}
        {error && <Alerta tipo="error" titulo="No se pudo consultar">{error}</Alerta>}

        {datos && (
          <>
            <dl className="grid grid-cols-2 gap-3 rounded-xl bg-slate-50 p-4 text-sm">
              <div>
                <dt className="text-xs font-medium uppercase tracking-wide text-slate-500">Estado</dt>
                <dd className="font-semibold text-slate-900">{datos.estado}</dd>
              </div>
              <div>
                <dt className="text-xs font-medium uppercase tracking-wide text-slate-500">Código de estado</dt>
                <dd className="font-mono font-semibold text-slate-900">{datos.codEstado}</dd>
              </div>
              <div className="col-span-2">
                <dt className="text-xs font-medium uppercase tracking-wide text-slate-500">Municipio</dt>
                <dd className="text-slate-900">
                  {datos.municipio} <span className="font-mono text-slate-500">({datos.codMunicipio})</span>
                </dd>
              </div>
            </dl>

            <Campo id="colonia" etiqueta={`Colonia (${datos.colonias.length} encontradas)`}>
              <select id="colonia" value={coloniaSel} onChange={(e) => onColonia(e.target.value)}
                className={claseInput()}>
                <option value="">— Selecciona una colonia —</option>
                {datos.colonias.map((colonia) => (
                  <option key={colonia.codigo} value={colonia.codigo}>
                    {colonia.descripcion}
                  </option>
                ))}
              </select>
            </Campo>

            {coloniaElegida && (
              <p className="flex items-center justify-between rounded-lg border border-marca-200 bg-marca-50 px-4 py-2.5 text-sm">
                <span className="text-marca-900">Código de la colonia</span>
                <span className="font-mono text-base font-bold text-marca-700">{coloniaElegida.codigo}</span>
              </p>
            )}
          </>
        )}
      </div>
    </Tarjeta>
  )
}

export default CodigoPostal
```

### 5.11 `src/components/registro/PersonaForm.jsx` (v3)
Característica 2 (captura):
- Los errores del navegador se calculan en cada render.
- Se muestran cuando el campo ya fue "tocado" (`onBlur`) o después de presionar Procesar.
- Los errores del servidor (`err.errores`) se muestran en su campo.
- Cualquier cambio llama a `onCambio()` para que el padre descarte el resultado anterior.

```jsx
import { useState } from 'react'
import { UserRound, Sparkles } from 'lucide-react'
import { procesarPersona } from '../../services/registroService'
import {
  validarNombre,
  validarHobby,
  validarFechaNacimiento,
  validarEmail,
  hoyISO,
} from '../../utils/validaciones'
import Tarjeta from '../ui/Tarjeta'
import Campo from '../ui/Campo'
import Boton from '../ui/Boton'
import Alerta from '../ui/Alerta'
import { claseInput } from '../ui/estilos'

const VALORES_INICIALES = { nombreCompleto: '', hobby: '', fechaNacimiento: '', email: '' }

// Característica 2: captura y procesamiento de la persona.
// Props:
//   procesado                             -> true si ya hay un resultado vigente (para la palomita del paso)
//   onProcesado(resultado, datosEnviados) -> la API procesó bien a la persona
//   onCambio()                            -> el usuario modificó algo; el resultado anterior ya no aplica
function PersonaForm({ procesado, onProcesado, onCambio }) {
  const [valores, setValores] = useState(VALORES_INICIALES)
  const [tocados, setTocados] = useState({})                 // campos que el usuario ya visitó
  const [intentoEnviar, setIntentoEnviar] = useState(false)  // después de presionar "Procesar" se muestran todos los errores
  const [erroresServidor, setErroresServidor] = useState({}) // errores por campo que regresó la API
  const [errorGeneral, setErrorGeneral] = useState('')
  const [procesando, setProcesando] = useState(false)

  // Estado derivado: los errores se recalculan en cada render con los valores actuales.
  const erroresCliente = {
    nombreCompleto: validarNombre(valores.nombreCompleto),
    hobby: validarHobby(valores.hobby),
    fechaNacimiento: validarFechaNacimiento(valores.fechaNacimiento),
    email: validarEmail(valores.email),
  }
  const hayErrores = Object.values(erroresCliente).some((e) => e !== '')

  function errorDe(campo) {
    const visible = tocados[campo] || intentoEnviar
    return (visible && erroresCliente[campo]) || erroresServidor[campo] || ''
  }

  function manejarCambio(evento) {
    const { name, value } = evento.target
    setValores({ ...valores, [name]: value })
    setErroresServidor({ ...erroresServidor, [name]: '' })
    setErrorGeneral('')
    onCambio()
  }

  function manejarBlur(evento) {
    setTocados({ ...tocados, [evento.target.name]: true })
  }

  async function manejarEnvio(evento) {
    evento.preventDefault()
    setIntentoEnviar(true)
    if (hayErrores) return // las instrucciones ya se muestran debajo de cada campo

    const datos = { ...valores, fechaNacimiento: valores.fechaNacimiento || null }
    setProcesando(true)
    setErrorGeneral('')
    try {
      const resultado = await procesarPersona(datos)
      onProcesado(resultado, datos)
    } catch (err) {
      setErroresServidor(err.errores ?? {})
      setErrorGeneral(err.message)
    } finally {
      setProcesando(false)
    }
  }

  // Atributos comunes de cada input: valor, manejadores y estilo según si tiene error.
  // Se "esparcen" en el input con {...propsDe('email')}
  function propsDe(campo) {
    const error = errorDe(campo)
    return {
      id: campo,
      name: campo,
      value: valores[campo],
      onChange: manejarCambio,
      onBlur: manejarBlur,
      'aria-invalid': error !== '',
      className: claseInput(error !== ''),
    }
  }

  return (
    <Tarjeta paso={2} completado={procesado} icono={UserRound} titulo="Datos de la persona"
      subtitulo="Normalizamos tu nombre y generamos tu ID de usuario.">
      <form onSubmit={manejarEnvio} noValidate className="space-y-4">
        <Campo id="nombreCompleto" etiqueta="Nombre completo" error={errorDe('nombreCompleto')}
          ayuda="Orden: nombre(s), apellido paterno, apellido materno.">
          <input {...propsDe('nombreCompleto')} maxLength={100} autoComplete="name"
            placeholder="Ej. maría de la cruz pérez" />
        </Campo>

        <Campo id="hobby" etiqueta="Hobby" error={errorDe('hobby')}>
          <input {...propsDe('hobby')} maxLength={150} placeholder="Ej. leer novelas" />
        </Campo>

        <div className="grid gap-4 sm:grid-cols-2">
          <Campo id="fechaNacimiento" etiqueta="Fecha de nacimiento" error={errorDe('fechaNacimiento')}
            ayuda="Debes tener al menos 5 años.">
            <input {...propsDe('fechaNacimiento')} type="date" max={hoyISO()} />
          </Campo>

          <Campo id="email" etiqueta="Email" error={errorDe('email')}>
            <input {...propsDe('email')} type="email" maxLength={100} autoComplete="email"
              placeholder="nombre@dominio.com" />
          </Campo>
        </div>

        {errorGeneral && <Alerta tipo="error">{errorGeneral}</Alerta>}

        <Boton type="submit" icono={Sparkles} cargando={procesando} className="w-full sm:w-auto">
          {procesando ? 'Procesando' : 'Procesar'}
        </Boton>
      </form>
    </Tarjeta>
  )
}

export default PersonaForm
```

### 5.12 `src/components/registro/ResultadoPersona.jsx` (v3)
Muestra el resultado del procesamiento y usa `Preposiciones`. El ID va destacado, con un botón **Copiar ID** (`navigator.clipboard` + toast).

```jsx
import { useState } from 'react'
import { toast } from 'sonner'
import { IdCard, Copy, Check, Sparkles } from 'lucide-react'
import Preposiciones from './Preposiciones'
import Tarjeta from '../ui/Tarjeta'
import Alerta from '../ui/Alerta'

// Un dato del resultado: etiqueta pequeña arriba y valor abajo
function Dato({ etiqueta, children }) {
  return (
    <div>
      <dt className="text-xs font-medium uppercase tracking-wide text-slate-500">{etiqueta}</dt>
      <dd className="mt-0.5 text-sm text-slate-900">{children}</dd>
    </div>
  )
}

// Resultado de la Característica 2: nombre normalizado, ID, hobby y preposiciones.
// Props: resultado -> lo que regresó POST api/Persona/procesar
function ResultadoPersona({ resultado }) {
  const [copiado, setCopiado] = useState(false)

  async function copiarId() {
    try {
      await navigator.clipboard.writeText(resultado.idUsuario)
      setCopiado(true)
      toast.success(`ID ${resultado.idUsuario} copiado`)
      setTimeout(() => setCopiado(false), 2000)
    } catch {
      toast.error('No se pudo copiar el ID')
    }
  }

  return (
    <Tarjeta icono={Sparkles} titulo="Resultado del procesamiento" subtitulo="Revisa los datos antes de finalizar.">
      <div className="grid gap-6 lg:grid-cols-[minmax(0,18rem)_1fr]">
        {/* ID destacado */}
        <div className="flex flex-col justify-between rounded-xl bg-gradient-to-br from-marca-600 to-marca-900 p-5 text-white">
          <div className="flex items-center gap-2 text-sm text-marca-100">
            <IdCard className="size-5" aria-hidden="true" />
            ID del usuario
          </div>
          <p className="my-4 font-mono text-3xl font-bold tracking-wider">{resultado.idUsuario}</p>
          <button
            type="button"
            onClick={copiarId}
            className="inline-flex w-fit items-center gap-1.5 rounded-lg bg-white/15 px-3 py-1.5 text-xs font-medium transition hover:bg-white/25"
          >
            {copiado ? <Check className="size-3.5" aria-hidden="true" /> : <Copy className="size-3.5" aria-hidden="true" />}
            {copiado ? 'Copiado' : 'Copiar ID'}
          </button>
        </div>

        <div className="space-y-4">
          <div>
            <p className="text-xs font-medium uppercase tracking-wide text-slate-500">Nombre normalizado</p>
            <p className="text-xl font-semibold text-slate-900">{resultado.nombreNormalizado}</p>
          </div>

          <dl className="grid gap-4 sm:grid-cols-3">
            <Dato etiqueta="Nombre(s)">{resultado.nombres}</Dato>
            <Dato etiqueta="Apellido paterno">{resultado.apellidoPaterno}</Dato>
            <Dato etiqueta="Apellido materno">{resultado.apellidoMaterno || <span className="text-slate-400">(no capturado)</span>}</Dato>
            <Dato etiqueta="Hobby">{resultado.hobby}</Dato>
            <Dato etiqueta="Email">{resultado.email}</Dato>
            <Dato etiqueta="Edad">{resultado.edad} años</Dato>
          </dl>

          {resultado.avisos.length > 0 && (
            <Alerta tipo="aviso" titulo="Avisos">
              <ul className="list-inside list-disc">
                {resultado.avisos.map((aviso) => <li key={aviso}>{aviso}</li>)}
              </ul>
            </Alerta>
          )}

          <Preposiciones lista={resultado.preposiciones} />
        </div>
      </div>
    </Tarjeta>
  )
}

export default ResultadoPersona
```

### 5.13 `src/components/registro/Preposiciones.jsx` (v3)
Lista ordenable. El orden elegido es estado local, y la lista ordenada se calcula a partir de una **copia** (`[...lista]`), porque `sort()` modifica el arreglo y las props no se deben modificar.

```jsx
import { useState } from 'react'
import { ArrowDownAZ, ArrowDownZA, ListOrdered } from 'lucide-react'

const ORDENES = [
  { id: 'asc', texto: 'A → Z', Icono: ArrowDownAZ },
  { id: 'desc', texto: 'Z → A', Icono: ArrowDownZA },
  { id: 'original', texto: 'Original', Icono: ListOrdered },
]

// Lista de preposiciones encontradas en el nombre, con opción de ordenarla alfabéticamente.
// Props: lista -> arreglo de strings, en el orden en que aparecen en el nombre
function Preposiciones({ lista }) {
  const [orden, setOrden] = useState('original') // 'original' | 'asc' | 'desc'

  // Estado derivado: la lista ordenada se calcula al dibujar.
  // [...lista] crea una COPIA: sort() modifica el arreglo y las props no se deben modificar.
  let visibles = [...lista]
  if (orden === 'asc') visibles.sort((a, b) => a.localeCompare(b, 'es'))
  if (orden === 'desc') visibles.sort((a, b) => b.localeCompare(a, 'es'))

  return (
    <div className="rounded-xl border border-slate-200 p-4">
      <div className="flex flex-wrap items-center justify-between gap-3">
        <h3 className="text-sm font-semibold text-slate-900">
          Preposiciones encontradas <span className="text-slate-500">({lista.length})</span>
        </h3>

        {lista.length > 0 && (
          // Control segmentado: el botón del orden activo se ve "presionado" (aria-pressed)
          <div className="inline-flex rounded-lg bg-slate-100 p-0.5" role="group" aria-label="Ordenar preposiciones">
            {ORDENES.map(({ id, texto, Icono }) => (
              <button
                key={id}
                type="button"
                onClick={() => setOrden(id)}
                aria-pressed={orden === id}
                className={`inline-flex items-center gap-1.5 rounded-md px-2.5 py-1 text-xs font-medium transition ${
                  orden === id ? 'bg-white text-marca-700 shadow-sm' : 'text-slate-600 hover:text-slate-900'
                }`}
              >
                <Icono className="size-3.5" aria-hidden="true" />
                {texto}
              </button>
            ))}
          </div>
        )}
      </div>

      {lista.length === 0 ? (
        <p className="mt-2 text-sm text-slate-500">No se encontraron preposiciones en el nombre.</p>
      ) : (
        <ol className="mt-3 flex flex-wrap gap-2">
          {visibles.map((p, i) => (
            <li key={p} className="inline-flex items-center gap-1.5 rounded-full bg-marca-50 px-3 py-1 text-sm font-medium text-marca-700 ring-1 ring-inset ring-marca-200">
              <span className="text-xs text-marca-500">{i + 1}.</span>
              {p}
            </li>
          ))}
        </ol>
      )}
    </div>
  )
}

export default Preposiciones
```

### 5.14 `src/components/registro/AccionesRegistro.jsx` (v3)
Característica 3: los botones y sus reglas, más el checklist de `requisitos`. No tiene estado; todo llega por props.

```jsx
import { Flag, RotateCcw, CircleCheck, Circle, Lock } from 'lucide-react'
import Tarjeta from '../ui/Tarjeta'
import Boton from '../ui/Boton'

// Característica 3: botones Finalizar y Reiniciar.
// Reglas:
//   - Finalizar solo se habilita cuando se cumplen todos los requisitos y aún no se ha finalizado.
//   - Reiniciar NO se puede presionar si no se ha presionado Finalizar.
// Props: requisitos ([{ texto, cumplido }]), finalizado, guardando, onFinalizar, onReiniciar
function AccionesRegistro({ requisitos, finalizado, guardando, onFinalizar, onReiniciar }) {
  const todoListo = requisitos.every((r) => r.cumplido)
  const puedeFinalizar = todoListo && !finalizado && !guardando

  let subtitulo = 'Completa los pasos para poder finalizar.'
  if (todoListo) subtitulo = 'Todo listo. Al finalizar se guardará el registro y ya no podrás editarlo.'
  if (finalizado) subtitulo = 'Registro finalizado. Presiona Reiniciar para capturar uno nuevo.'

  return (
    <Tarjeta paso={3} completado={finalizado} icono={Flag} titulo="Finalizar" subtitulo={subtitulo}
      className="print:hidden">
      <div className="flex flex-col gap-5 sm:flex-row sm:items-end sm:justify-between">
        {/* Checklist: lo que falta (o ya se cumplió) para poder finalizar */}
        <ul className="space-y-1.5">
          {requisitos.map((r) => (
            <li key={r.texto} className={`flex items-center gap-2 text-sm ${r.cumplido ? 'text-emerald-700' : 'text-slate-500'}`}>
              {r.cumplido
                ? <CircleCheck className="size-4" aria-hidden="true" />
                : <Circle className="size-4" aria-hidden="true" />}
              {r.texto}
            </li>
          ))}
        </ul>

        <div className="flex gap-2">
          <Boton
            variante="secundario"
            icono={RotateCcw}
            onClick={onReiniciar}
            disabled={!finalizado}
            title={finalizado ? 'Empezar un registro nuevo' : 'Disponible después de finalizar'}
          >
            Reiniciar
          </Boton>
          <Boton
            variante="exito"
            icono={finalizado ? Lock : Flag}
            onClick={onFinalizar}
            cargando={guardando}
            disabled={!puedeFinalizar}
            title={puedeFinalizar ? 'Guardar y bloquear la información' : 'Completa los pasos pendientes'}
          >
            {finalizado ? 'Finalizado' : guardando ? 'Guardando' : 'Finalizar'}
          </Boton>
        </div>
      </div>
    </Tarjeta>
  )
}

export default AccionesRegistro
```

### 5.15 `src/components/registro/ResumenRegistro.jsx` (v3)
Toque personal: la ficha del registro guardado, con botón para imprimir o guardar como PDF (`window.print()`).

```jsx
import { CircleCheck, Printer } from 'lucide-react'
import Boton from '../ui/Boton'

// Fila de la ficha: etiqueta a la izquierda, valor a la derecha
function Fila({ etiqueta, children, mono = false }) {
  return (
    <div className="grid grid-cols-3 gap-4 px-5 py-3 text-sm">
      <dt className="font-medium text-slate-500">{etiqueta}</dt>
      <dd className={`col-span-2 text-slate-900 ${mono ? 'font-mono' : ''}`}>{children}</dd>
    </div>
  )
}

// Toque personal: "ficha" del registro guardado, lista para imprimir o guardar como PDF.
// Props: registro -> lo que regresó POST api/Persona/finalizar
function ResumenRegistro({ registro }) {
  const { persona } = registro
  const fecha = registro.fechaRegistro ? new Date(registro.fechaRegistro).toLocaleString('es-MX') : ''

  return (
    <section className="overflow-hidden rounded-2xl border border-emerald-200 bg-white shadow-sm print:border-slate-300 print:shadow-none">
      <header className="flex flex-wrap items-center justify-between gap-4 bg-emerald-50 px-5 py-4">
        <div className="flex items-center gap-3">
          <CircleCheck className="size-8 text-emerald-600" aria-hidden="true" />
          <div>
            <h2 className="text-lg font-semibold text-emerald-900">Registro #{registro.idRegistro} guardado</h2>
            <p className="text-sm text-emerald-700">Fecha de registro: {fecha}</p>
          </div>
        </div>
        <Boton variante="secundario" icono={Printer} onClick={() => window.print()} className="print:hidden">
          Imprimir / Guardar PDF
        </Boton>
      </header>

      <dl className="divide-y divide-slate-100">
        <Fila etiqueta="ID de usuario" mono>{persona.idUsuario}</Fila>
        <Fila etiqueta="Nombre">{persona.nombreNormalizado}</Fila>
        <Fila etiqueta="Hobby">{persona.hobby}</Fila>
        <Fila etiqueta="Fecha de nacimiento">{persona.fechaNacimiento} ({persona.edad} años)</Fila>
        <Fila etiqueta="Email">{persona.email}</Fila>
        <Fila etiqueta="Preposiciones">{persona.preposiciones.join(', ') || '—'}</Fila>
        <Fila etiqueta="Código postal" mono>{registro.cp}</Fila>
        <Fila etiqueta="Estado">{registro.estado} ({registro.codEstado})</Fila>
        <Fila etiqueta="Municipio">{registro.municipio}</Fila>
        <Fila etiqueta="Colonia">{registro.colonia} ({registro.codColonia})</Fila>
      </dl>
    </section>
  )
}

export default ResumenRegistro
```

### 5.16 `src/pages/UsuariosPage.jsx` (v3)
El CRUD. En la v2 se movió desde `App.jsx`. En la v3 usa toasts para los éxitos y `DialogoConfirmar` para eliminar (antes era `window.confirm`), y resalta la fila que se está editando.

```jsx
import { useState, useEffect } from 'react'
import { toast } from 'sonner'
import { Users, RefreshCw } from 'lucide-react'
import UsuarioForm from '../components/UsuarioForm'
import UsuariosTabla from '../components/UsuariosTabla'
import BuscarUsuario from '../components/BuscarUsuario'
import Tarjeta from '../components/ui/Tarjeta'
import Boton from '../components/ui/Boton'
import Alerta from '../components/ui/Alerta'
import DialogoConfirmar from '../components/ui/DialogoConfirmar'
import {
  obtenerUsuarios,
  crearUsuario,
  actualizarUsuario,
  eliminarUsuario,
} from '../services/usuariosService'

// Página del CRUD de usuarios. Guarda el estado compartido (la lista de usuarios)
// y se lo pasa a los componentes hijos por props.
function UsuariosPage() {
  const [usuarios, setUsuarios] = useState([])            // lista que viene de la API
  const [usuarioEditar, setUsuarioEditar] = useState(null) // usuario seleccionado para editar
  const [cargando, setCargando] = useState(true)
  const [error, setError] = useState('')
  const [idEliminar, setIdEliminar] = useState(null)       // id pendiente de confirmar en el diálogo

  // Vuelve a pedir la lista a la API (después de crear, editar, eliminar o con "Recargar").
  async function cargarUsuarios() {
    try {
      const datos = await obtenerUsuarios()
      setUsuarios(datos)
      setError('')
    } catch (err) {
      setError(`No se pudo cargar la lista. ¿La API está corriendo? (${err.message})`)
    } finally {
      setCargando(false)
    }
  }

  // useEffect con [] vacío: se ejecuta UNA vez, cuando el componente aparece en pantalla.
  // Aquí se usa la promesa directamente (.then/.catch) para que los setState ocurran
  // cuando llega la respuesta, no en el momento en que corre el efecto.
  useEffect(() => {
    obtenerUsuarios()
      .then((datos) => setUsuarios(datos))
      .catch((err) => setError(`No se pudo cargar la lista. ¿La API está corriendo? (${err.message})`))
      .finally(() => setCargando(false))
  }, [])

  // Crear o actualizar, según si hay un usuario en edición. Regresa true si salió bien.
  async function guardarUsuario(datos) {
    setError('')
    try {
      if (usuarioEditar) {
        await actualizarUsuario(usuarioEditar.idUsuarios, datos)
        toast.success('Usuario actualizado')
        setUsuarioEditar(null)
      } else {
        await crearUsuario(datos)
        toast.success('Usuario creado')
      }
      await cargarUsuarios() // la API no regresa el registro, así que volvemos a pedir la lista
      return true
    } catch (err) {
      setError(err.message)
      return false
    }
  }

  function iniciarEdicion(usuario) {
    setError('')
    setUsuarioEditar(usuario)
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }

  async function confirmarEliminar() {
    const id = idEliminar
    setIdEliminar(null)
    setError('')
    try {
      await eliminarUsuario(id)
      toast.success(`Usuario #${id} eliminado`)
      if (usuarioEditar?.idUsuarios === id) setUsuarioEditar(null)
      await cargarUsuarios()
    } catch (err) {
      setError(err.message)
    }
  }

  return (
    <div className="space-y-6">
      <div>
        <h1 className="text-2xl font-bold tracking-tight text-slate-900">Usuarios</h1>
        <p className="mt-1 text-sm text-slate-500">Alta, consulta, edición y baja de la tabla TBLUsuarios.</p>
      </div>

      {error && <Alerta tipo="error">{error}</Alerta>}

      <div className="grid items-start gap-6 lg:grid-cols-[22rem_1fr]">
        <div className="space-y-6">
          {/* key: cuando cambia (otro usuario o "nuevo"), React crea el formulario de nuevo con sus datos */}
          <UsuarioForm
            key={usuarioEditar?.idUsuarios ?? 'nuevo'}
            usuarioEditar={usuarioEditar}
            onGuardar={guardarUsuario}
            onCancelar={() => setUsuarioEditar(null)}
          />
          <BuscarUsuario />
        </div>

        <Tarjeta
          icono={Users}
          titulo="Lista de usuarios"
          subtitulo={cargando ? 'Cargando…' : `${usuarios.length} registros`}
          acciones={<Boton variante="secundario" icono={RefreshCw} onClick={cargarUsuarios}>Recargar</Boton>}
        >
          {cargando ? (
            <p className="py-8 text-center text-sm text-slate-500">Cargando...</p>
          ) : (
            <UsuariosTabla
              usuarios={usuarios}
              idSeleccionado={usuarioEditar?.idUsuarios}
              onEditar={iniciarEdicion}
              onEliminar={setIdEliminar}
            />
          )}
        </Tarjeta>
      </div>

      <DialogoConfirmar
        abierto={idEliminar !== null}
        peligro
        titulo={`¿Eliminar el usuario #${idEliminar}?`}
        mensaje="Esta acción no se puede deshacer."
        textoConfirmar="Eliminar"
        onConfirmar={confirmarEliminar}
        onCancelar={() => setIdEliminar(null)}
      />
    </div>
  )
}

export default UsuariosPage
```

### 5.17 Componentes del CRUD (v1)
- `UsuarioForm.jsx`: formulario controlado para crear y editar. Usa el truco de `key` para reiniciarse.
- `UsuariosTabla.jsx`: tabla sin estado que recibe la lista y los manejadores por props.
- `BuscarUsuario.jsx`: búsqueda por ID. En la v2 detecta el 404 con `error.status === 404`.
- En la v3 los tres usan `Tarjeta`, `Campo`, `Boton` y `Alerta`, y la tabla tiene botones de ícono.

```jsx
import { useState } from 'react'
import { Search } from 'lucide-react'
import { obtenerUsuario } from '../services/usuariosService'
import Tarjeta from './ui/Tarjeta'
import Boton from './ui/Boton'
import Alerta from './ui/Alerta'
import { claseInput } from './ui/estilos'

// Componente: busca un usuario por su ID (usa GET api/usuarios/{id}).
// Maneja su propio estado porque nadie más necesita estos datos.
function BuscarUsuario() {
  const [id, setId] = useState('')
  const [resultado, setResultado] = useState(null)
  const [mensaje, setMensaje] = useState('')

  async function buscar(evento) {
    evento.preventDefault()
    setResultado(null)
    setMensaje('')
    try {
      const usuario = await obtenerUsuario(id)
      setResultado(usuario)
    } catch (error) {
      // La API regresa 404 cuando el id no existe.
      setMensaje(error.status === 404 ? `No existe el usuario con ID ${id}.` : error.message)
    }
  }

  return (
    <Tarjeta icono={Search} titulo="Buscar por ID">
      <form onSubmit={buscar} className="flex gap-2">
        <input type="number" min="1" value={id} onChange={(e) => setId(e.target.value)} required
          placeholder="Ej. 3" aria-label="ID del usuario" className={claseInput()} />
        <Boton type="submit" variante="secundario" icono={Search}>Buscar</Boton>
      </form>

      {mensaje && <Alerta tipo="aviso" className="mt-4">{mensaje}</Alerta>}

      {resultado && (
        <dl className="mt-4 space-y-2 rounded-xl bg-slate-50 p-4 text-sm">
          <div className="flex justify-between gap-4"><dt className="text-slate-500">ID</dt><dd className="font-mono">{resultado.idUsuarios}</dd></div>
          <div className="flex justify-between gap-4"><dt className="text-slate-500">Nombre</dt><dd className="text-right font-medium">{resultado.nombre}</dd></div>
          <div className="flex justify-between gap-4"><dt className="text-slate-500">Hobby</dt><dd className="text-right">{resultado.hobby ?? '—'}</dd></div>
          <div className="flex justify-between gap-4"><dt className="text-slate-500">Nacimiento</dt><dd>{resultado.fechanacimiento.slice(0, 10)}</dd></div>
          <div className="flex justify-between gap-4"><dt className="text-slate-500">Email</dt><dd className="truncate">{resultado.email}</dd></div>
        </dl>
      )}
    </Tarjeta>
  )
}

export default BuscarUsuario
```

---

## 6. Cambios en la API relacionados con el frontend

Ruta: `P04R01_PROYECTO\BACKEND\P04R01_APIREST\`. El detalle completo está en `HISTORIAL.md`.

- **v1:** CORS permite `http://localhost:5173` (Vite) en `Program.cs`.
- **v2:**
  - Endpoints `api/CodigoPostal/{cp}`, `api/Persona/procesar` y `api/Persona/finalizar`.
  - Formato de error `{ mensaje }` o `{ mensaje, errores: { campo: msg } }`.
  - El `PUT` del CRUD ya no borra las columnas del registro Thona.

---

## 7. Pruebas realizadas (v2)

**v3 (estilos):** `npm run lint` y `npm run build` sin avisos. Revisión visual en escritorio (1280 px) y celular (375 px) de las dos pestañas. La lógica no cambió respecto a la v2; los props nuevos (`requisitos`, `procesado`, `idSeleccionado`) se verificaron con el linter y el build.

Con la API y `npm run dev` corriendo, en el navegador:

| Prueba | Resultado |
|---|---|
| Al abrir: Consultar, Finalizar y Reiniciar deshabilitados; lista de pendientes visible | ✅ |
| Escribir `06a7` en el CP → la letra se ignora; aviso "Solo se aceptan números" y "llevas 3" | ✅ |
| Completar `06700` → Consultar se habilita; muestra CIUDAD DE MEXICO (009), municipio y 2 colonias | ✅ |
| Seleccionar ROMA NORTE → "Código de la colonia: 000947" | ✅ |
| CP `99999` → "No se encontró información para el código postal 99999." | ✅ |
| Procesar con todo vacío → un mensaje por campo | ✅ |
| Fecha de 2024 y email `maria@test` → "al menos 5 años" y "formato válido" | ✅ |
| Nombre `"  maría de los ángeles   DE LA cruz pérez#1 "` → "María De Los Ángeles De La Cruz Pérez", ID `CRPM070395`, aviso por `# 1`, preposiciones de/los/la | ✅ |
| Ordenar preposiciones A → Z (de, la, los), Z → A (los, la, de) y original (de, los, la) | ✅ |
| Editar el hobby después de procesar → el resultado desaparece y Finalizar se bloquea; al volver a procesar se habilita | ✅ |
| Finalizar → registro guardado en la BD con todas las columnas; los 8 controles de captura quedan deshabilitados; Reiniciar se habilita; ficha visible | ✅ |
| Reiniciar → todo vacío, Reiniciar deshabilitado de nuevo | ✅ |
| Pestaña CRUD → lista los usuarios, incluido el del registro Thona | ✅ |
| `npm run lint` / `npm run build` | ✅ sin avisos ni errores |

El registro de prueba se borró de la BD. El único error en la consola es el 404 del CP 99999, y es esperado.

---

## 8. Pendientes sugeridos

- Modo oscuro (`dark:` de Tailwind).
- Pruebas automáticas: xUnit para `PersonaService` y Vitest + Testing Library para los componentes.
- Evitar registros duplicados (por ejemplo, un índice único por email).
