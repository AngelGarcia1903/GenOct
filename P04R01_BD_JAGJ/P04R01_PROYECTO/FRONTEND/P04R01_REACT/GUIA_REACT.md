# Guía de React (para quien ya sabe JS y HTML)

Esta guía explica cada concepto de React con el código real de este proyecto (`P04R01_REACT`).
La **Parte 1** usa el CRUD de usuarios y la **Parte 2** usa el Registro de la prueba Thona.

La **Parte 3** explica los estilos (Tailwind) y los componentes de interfaz.

*Historial: v1 2026-10-06 (CRUD) · v2 2026-10-06 (Parte 2: Registro Thona y servicios con `http.js`) · v3 2026-10-06 (Parte 3: estilos, íconos, toasts y diálogo).*
Lee una sección, abre el archivo que menciona y compáralo.

---

## 0. La idea principal

Con JS + HTML "normal" trabajas así:
1. Tienes un HTML ya escrito.
2. Buscas elementos (`document.getElementById`).
3. Los modificas a mano (`elemento.innerHTML = ...`, `appendChild`, etc.).

Esto se llama **programación imperativa**: le dices al navegador *cómo* cambiar la página, paso a paso.

React es **declarativo**: tú describes *cómo debe verse* la pantalla según los datos, y React se encarga de actualizar el DOM cuando los datos cambian.

```
pantalla = f(datos)
```

Nunca vas a escribir `document.getElementById` ni `innerHTML`. Solo cambias los **datos** (el *estado*) y React vuelve a dibujar lo necesario.

> "React es como la mezcla de JS y HTML". Casi: React es JavaScript. Lo que parece HTML se llama **JSX**, y es una forma cómoda de escribir llamadas a funciones de JavaScript.

---

## 1. Herramientas: Node, npm y Vite

| Herramienta | Para qué sirve | Analogía con .NET |
|---|---|---|
| **Node.js** | Ejecuta JavaScript fuera del navegador. Lo necesitan las herramientas de desarrollo. | El runtime de .NET |
| **npm** | Instala librerías (paquetes) y ejecuta scripts. | NuGet + `dotnet` CLI |
| **package.json** | Lista las dependencias y los scripts del proyecto. | El `.csproj` |
| **node_modules/** | Carpeta con las librerías descargadas. No se edita ni se sube a git. | Los paquetes en `~/.nuget` |
| **Vite** | Servidor de desarrollo + compilador. Recarga el navegador al guardar (HMR). | `dotnet watch` |

Comandos del día a día:

```bash
npm install        # descarga dependencias (una vez, o cuando cambia package.json)
npm run dev        # servidor de desarrollo en http://localhost:5173
npm run build      # genera la versión final optimizada en /dist
npm run lint       # revisa el código
```

---

## 2. ¿Cómo arranca la app?

1. El navegador abre `index.html`, que tiene un `<div id="root"></div>` vacío y carga `src/main.jsx`.
2. `main.jsx` dice: "dibuja el componente `<App />` dentro de `#root`":
   ```jsx
   createRoot(document.getElementById('root')).render(<App />)
   ```
3. `App` dibuja una página (`RegistroPage` o `UsuariosPage`), cada página dibuja a sus hijos (`CodigoPostal`, `UsuariosTabla`…), y así sucesivamente.

Es el único lugar donde se usa `document.getElementById`.

---

## 3. Componentes

Un **componente** es una **función de JavaScript que regresa JSX** (lo que se ve en pantalla).

```jsx
function Saludo() {
  return <h1>Hola</h1>
}
```

Reglas:
- El nombre **empieza con mayúscula** (`UsuariosTabla`, no `usuariosTabla`). Así React distingue tus componentes de las etiquetas HTML.
- Se usan como etiquetas: `<Saludo />`.
- Cada componente suele ir en su propio archivo `.jsx` y se exporta:
  ```jsx
  export default UsuariosTabla      // en UsuariosTabla.jsx
  import UsuariosTabla from '../components/UsuariosTabla'   // en pages/UsuariosPage.jsx
  ```

En este proyecto:

| Componente | Responsabilidad |
|---|---|
| `App` | Pestañas: decide qué página mostrar. |
| `RegistroPage` | Prueba Thona: guarda el estado compartido de las 3 características. |
| `CodigoPostal`, `PersonaForm`, `ResultadoPersona`, `Preposiciones`, `AccionesRegistro`, `ResumenRegistro` | Piezas del registro Thona (ver la Parte 2). |
| `UsuariosPage` | CRUD: guarda la lista de usuarios y coordina sus piezas. |
| `UsuarioForm` | CRUD: captura los datos para crear o editar. |
| `UsuariosTabla` | CRUD: muestra la lista. |
| `BuscarUsuario` | CRUD: busca por ID. |

> Piensa en componentes como en "piezas de LEGO": cada una hace una sola cosa y se arman entre sí.

---

## 4. JSX: diferencias con HTML

JSX se parece a HTML, pero es JavaScript. Diferencias que vas a encontrar:

| HTML | JSX | Por qué |
|---|---|---|
| `class="x"` | `className="x"` | `class` es palabra reservada en JS. |
| `for="nombre"` | `htmlFor="nombre"` | `for` también es palabra reservada. |
| `onclick="fn()"` | `onClick={fn}` | Eventos en camelCase, y se pasa **la función**, no un string. |
| `style="color: red"` | `style={{ color: 'red' }}` | `style` recibe un objeto de JS. |
| `<input>` | `<input />` | Toda etiqueta debe cerrarse. |
| `maxlength="100"` | `maxLength={100}` | Atributos en camelCase; los números van entre `{}`. |

### Las llaves `{}` = "aquí va JavaScript"

Dentro del JSX, todo lo que esté entre `{}` es una expresión de JS:

```jsx
<td>{usuario.nombre}</td>                         // muestra una variable
<h2>{usuarioEditar ? 'Editar' : 'Nuevo'}</h2>     // operador ternario
<td>{usuario.hobby ?? '—'}</td>                   // valor por defecto si es null
```

Solo **expresiones** (algo que da un valor). No puedes poner un `if` o un `for` directamente dentro de `{}`; para eso están las secciones 7 y 8.

### Un solo elemento raíz

Un componente debe regresar **un** elemento padre. Si no quieres agregar un `<div>`, usa un fragmento `<> ... </>`.

### Comentarios en JSX
```jsx
{/* así se comenta dentro del JSX */}
```

---

## 5. Props: pasar datos de padre a hijo

Las **props** son los "parámetros" de un componente. Se pasan como atributos y llegan como un objeto.

En `pages/UsuariosPage.jsx` (padre):
```jsx
<UsuariosTabla
  usuarios={usuarios}
  onEditar={iniciarEdicion}
  onEliminar={borrarUsuario}
/>
```

En `UsuariosTabla.jsx` (hijo) se reciben con *destructuring*:
```jsx
function UsuariosTabla({ usuarios, onEditar, onEliminar }) { ... }
```

Reglas importantes:
- Las props son de **solo lectura**. El hijo nunca las modifica.
- Puedes pasar **cualquier cosa**: strings, números, arreglos, objetos... y **funciones**.
- Los datos **bajan** (padre → hijo). Para que el hijo "avise" algo al padre, el padre le pasa una función y el hijo la llama:
  ```jsx
  <button onClick={() => onEliminar(usuario.idUsuarios)}>Eliminar</button>
  ```
  Por convención, esas funciones se llaman `onAlgo` (`onEditar`, `onGuardar`, `onCancelar`).

---

## 6. Estado: `useState`

El **estado** son datos que **cambian con el tiempo** y que, al cambiar, **deben actualizar la pantalla**.

```jsx
import { useState } from 'react'

const [usuarios, setUsuarios] = useState([])
//     ↑ valor    ↑ función para cambiarlo   ↑ valor inicial
```

- `usuarios`: el valor actual.
- `setUsuarios(nuevoValor)`: cambia el valor **y le dice a React que vuelva a dibujar** el componente.

### ¿Por qué no una variable normal?

```jsx
let contador = 0
contador++   // la variable cambia, pero React NO se entera y la pantalla no se actualiza
```

Con `useState`, React sí sabe que algo cambió.

### Regla de oro: nunca modifiques el estado directamente

```jsx
formulario.nombre = 'Juan'                         // ❌ mal: React no se entera
setFormulario({ ...formulario, nombre: 'Juan' })   // ✅ bien: creas un objeto NUEVO
```

El `...formulario` (*spread*) copia todas las propiedades, y luego sobrescribes la que cambió. Con arreglos es igual: se usan `[...lista, nuevo]`, `filter` o `map`, que regresan un arreglo nuevo, en lugar de `push` o `splice`.

### Estados en `pages/UsuariosPage.jsx`

```jsx
const [usuarios, setUsuarios] = useState([])            // lista de la API
const [usuarioEditar, setUsuarioEditar] = useState(null) // null = modo "nuevo"
const [cargando, setCargando] = useState(true)
const [error, setError] = useState('')
const [mensaje, setMensaje] = useState('')
```

### ¿Dónde debe vivir el estado? ("levantar el estado")

Si **dos componentes** necesitan el mismo dato, el estado va en su **padre común**, y se les pasa por props.
- `usuarioEditar` lo necesitan la **tabla** (que lo pone al presionar "Editar") y el **formulario** (que lo muestra). Por eso vive en `UsuariosPage`.
- El ID que escribes en `BuscarUsuario` no le importa a nadie más, así que vive dentro de `BuscarUsuario`.

---

## 7. Mostrar cosas condicionalmente

No hay `if` dentro del JSX, pero hay tres formas:

```jsx
// 1) && : "si es verdadero, muestra esto"
{error && <Alerta tipo="error">{error}</Alerta>}   // Alerta: componente de ui/ (sección 29)

// 2) ternario: "esto o aquello"
{cargando ? <p>Cargando...</p> : <UsuariosTabla ... />}

// 3) if normal ANTES del return (salida temprana)
if (usuarios.length === 0) {
  return <p>No hay usuarios registrados.</p>
}
```

> Cuidado con `&&` y números: `{0 && <p>hola</p>}` muestra un `0` en pantalla. Usa `{lista.length > 0 && ...}`.

---

## 8. Listas: `map` y `key`

Para dibujar una lista no se usa `for`, sino `.map()`, que convierte cada dato en JSX:

```jsx
{usuarios.map((usuario) => (
  <tr key={usuario.idUsuarios}>
    <td>{usuario.nombre}</td>
  </tr>
))}
```

- **`key`** es obligatoria y debe ser **única y estable**. Lo ideal es el ID de la base de datos.
- React usa la `key` para saber qué fila cambió, se agregó o se borró, sin redibujar toda la tabla.
- Evita usar el índice del arreglo como `key` si la lista puede reordenarse o si puedes borrar elementos.

---

## 9. Eventos

```jsx
<button onClick={cargarUsuarios}>Recargar</button>              // pasas la función
<button onClick={() => onEditar(usuario)}>Editar</button>       // función flecha si necesitas argumentos
```

⚠️ Error clásico:
```jsx
<button onClick={cargarUsuarios()}>   // ❌ la EJECUTA al dibujar, no al hacer clic
<button onClick={cargarUsuarios}>     // ✅
```

El evento llega como parámetro, igual que en JS normal:
```jsx
function manejarEnvio(evento) {
  evento.preventDefault()   // evita que el <form> recargue la página
}
```

---

## 10. Formularios controlados

En JS normal, para leer un input haces `input.value` al final. En React, el **estado es la fuente de verdad**:

```jsx
<input name="nombre" value={formulario.nombre} onChange={manejarCambio} />
```

1. El input **muestra** lo que hay en el estado (`value`).
2. Cada tecla dispara `onChange`, que **actualiza el estado**.
3. React vuelve a dibujar y el input muestra el nuevo valor.

Un solo manejador para todos los campos (`UsuarioForm.jsx`):

```jsx
function manejarCambio(evento) {
  const { name, value } = evento.target          // name="email", value="a@b.com"
  setFormulario({ ...formulario, [name]: value })   // [name] = propiedad dinámica
}
```

Por eso cada `<input>` tiene un `name` igual a la propiedad del objeto. A su vez, esas propiedades coinciden con las del modelo de C# (`nombre`, `hobby`, `fechanacimiento`, `email`).

---

## 11. Efectos: `useEffect`

Un **efecto** es código que se ejecuta **después** de que el componente se dibuja. Sirve para **sincronizar con algo externo**: una API, un temporizador, `localStorage`...

```jsx
useEffect(() => {
  obtenerUsuarios()
    .then((datos) => setUsuarios(datos))
    .catch((err) => setError(...))
    .finally(() => setCargando(false))
}, [])   // ← arreglo de dependencias
```

El arreglo de dependencias define **cuándo** se ejecuta:

| Dependencias | Se ejecuta... |
|---|---|
| `[]` | Una sola vez, al aparecer el componente (como el `DOMContentLoaded` de JS normal). |
| `[id]` | Al aparecer y cada vez que cambie `id`. |
| *(sin arreglo)* | Después de **cada** render (casi nunca lo quieres). |

> En desarrollo, `StrictMode` ejecuta los efectos **dos veces** a propósito, para detectar errores. Si ves dos peticiones GET en la pestaña Network, es normal; en producción solo hay una.

### Cuándo **NO** usar `useEffect`

Error común de principiante: usar un efecto para "copiar" props al estado:
```jsx
// ❌ antipatrón
useEffect(() => { setFormulario(usuarioEditar) }, [usuarioEditar])
```
Esto provoca un render extra. En este proyecto se resolvió con **`key`** (sección 12). El linter (`npm run lint`) avisa cuando caes en esto.

---

## 12. El truco de `key` para "reiniciar" un componente

`key` no es solo para listas. Si a un componente le cambias la `key`, React **lo destruye y lo crea de nuevo**, con su estado inicial recalculado.

En `pages/UsuariosPage.jsx`:
```jsx
<UsuarioForm key={usuarioEditar?.idUsuarios ?? 'nuevo'} usuarioEditar={usuarioEditar} ... />
```

En `UsuarioForm.jsx`, el estado inicial depende de la prop:
```jsx
const [formulario, setFormulario] = useState(() =>
  usuarioEditar ? { ...datos del usuario... } : FORMULARIO_VACIO
)
```

- Presionas "Editar" en el usuario 5 → `key="5"` → se crea el formulario con los datos del 5.
- Presionas "Editar" en el 8 → `key="8"` → se crea de nuevo con los datos del 8.
- "Cancelar" o al guardar → `key="nuevo"` → formulario vacío.

> `useState(() => ...)` (con función) calcula el valor inicial **solo la primera vez**. Con `useState(valor)` también funciona; la función solo evita repetir el cálculo en cada render.

---

## 13. Hablar con la API: `fetch`, `async/await` y servicios

`fetch` es JavaScript nativo del navegador, no es de React. Todo el acceso a la API está en `src/services/`:

- `http.js` tiene la función base `peticion()`, que hace el `fetch`, convierte el JSON y lanza errores claros.
- `usuariosService.js` y `registroService.js` solo dicen *qué* ruta llamar:

```js
// usuariosService.js
export function crearUsuario(usuario) {
  return peticion('/api/usuarios', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(usuario),    // objeto JS → texto JSON
  })
}

// registroService.js
export function consultarCP(cp) {
  return peticion(`/api/CodigoPostal/${cp}`)
}
```

- **`fetch` NO lanza error con 404 o 500**, solo si no hay red. Por eso `peticion()` revisa `respuesta.ok` y lanza el error a mano (ver sección 22, `ApiError`).
- Los componentes usan `try/catch` y guardan el mensaje en el estado `error` para mostrarlo.

¿Por qué un archivo de servicios separado? Por **separación de responsabilidades**. Los componentes se encargan de la pantalla; el servicio, de la comunicación. Es lo mismo que separar Controllers y Context en tu API de C#.

### Variables de entorno (`.env`)
```bash
VITE_API_URL=http://localhost:5283
```
```js
const API_URL = import.meta.env.VITE_API_URL
```
Solo las que empiezan con `VITE_` llegan al navegador. Después de editar `.env`, reinicia `npm run dev`.

---

## 14. CORS (el error que más vas a ver)

Si en la consola del navegador aparece:
```
Access to fetch at 'http://localhost:5283/...' from origin 'http://localhost:5173' has been blocked by CORS policy
```
el problema **no está en React**. La **API** no permite peticiones desde ese origen.
Se arregla en `Program.cs` de la API, agregando el origen a `WithOrigins(...)`. Ya está configurado para `http://localhost:5173`.

---

## 15. Depurar

- **F12 → Console:** errores de JavaScript y de CORS.
- **F12 → Network:** cada petición a la API, con su método, código (200, 400, 404) y la respuesta.
- **`console.log(algo)`:** funciona igual que siempre.
- **Extensión React Developer Tools** (Chrome/Edge): muestra el árbol de componentes y sus props y estado en vivo.
- Al guardar un archivo, Vite actualiza el navegador solo. Si algo se ve raro, recarga con F5.

---

## 16. Resumen rápido (cheat sheet)

```jsx
import { useState, useEffect } from 'react'

function MiComponente({ titulo, onAccion }) {          // props
  const [valor, setValor] = useState('')                // estado
  const [lista, setLista] = useState([])

  useEffect(() => {                                     // efecto al montar
    fetch('/api/algo').then(r => r.json()).then(setLista)
  }, [])

  return (
    <div>
      <h2>{titulo}</h2>                                 {/* expresión */}
      <input value={valor} onChange={e => setValor(e.target.value)} />  {/* controlado */}
      <button onClick={() => onAccion(valor)}>Enviar</button>           {/* evento → padre */}
      {lista.length === 0 && <p>Vacío</p>}              {/* condicional */}
      <ul>
        {lista.map(item => <li key={item.id}>{item.nombre}</li>)}       {/* lista */}
      </ul>
    </div>
  )
}

export default MiComponente
```

---

# Parte 2 — Conceptos usados en el Registro Thona

La pestaña "Registro Thona" (`src/pages/RegistroPage.jsx` y `src/components/registro/`) usa todo lo anterior y agrega estas ideas.

---

## 17. Organizar carpetas: pages, components, services, utils

```
src/
├── pages/        pantallas completas; guardan el estado que comparten sus piezas
├── components/   piezas que reciben datos por props
│   └── ui/       piezas genéricas de interfaz: Boton, Tarjeta, Campo, Alerta… (sección 29)
├── services/     hablan con la API (fetch)
└── utils/        funciones puras de JavaScript (sin React), por ejemplo validaciones
```

React no te obliga a ninguna estructura, pero separar responsabilidades hace que cada archivo sea pequeño y fácil de entender. Es la misma idea que `Controllers / Services / Models` en tu API de C#.

---

## 18. Estado derivado: no guardes lo que puedes calcular

Si un valor se puede **calcular** a partir del estado o de las props, **no lo guardes en otro `useState`**. Calcúlalo en cada render.

```jsx
// CodigoPostal.jsx
const [cp, setCp] = useState('')
const errorCP = validarCP(cp)       // derivado: se recalcula solo
const cpValido = errorCP === ''     // derivado

<button disabled={!cpValido}>Consultar</button>
```

```jsx
// RegistroPage.jsx: la lista de lo que falta para finalizar
const pendientes = []
if (!datosCP) pendientes.push('Consultar un código postal válido.')
if (!resultado) pendientes.push('Procesar los datos de la persona.')
```

❌ Si tuvieras `const [cpValido, setCpValido] = useState(false)`, tendrías que acordarte de actualizarlo cada vez que cambia `cp`. Tarde o temprano se desincroniza. Con el valor derivado eso no puede pasar.

---

## 19. Levantar el estado entre varios componentes

Las tres características comparten información: Finalizar necesita el CP, la colonia y la persona procesada. Por eso ese estado vive en el padre común, `RegistroPage`, y los hijos lo reciben o lo avisan por props:

```
RegistroPage  (datosCP, coloniaSel, resultado, finalizado...)
 ├─ CodigoPostal      recibe datos/coloniaSel, avisa con onDatos/onColonia
 ├─ PersonaForm       avisa con onProcesado/onCambio
 ├─ ResultadoPersona  recibe resultado
 └─ AccionesRegistro  recibe pendientes/finalizado, avisa con onFinalizar/onReiniciar
```

¿Cómo se decide qué va en el padre y qué se queda en el hijo?
- El **texto que escribes** en el CP solo le importa a `CodigoPostal` → estado **local**.
- Los **datos consultados** y la **colonia elegida** los necesita Finalizar → estado en el **padre**.
- El orden A→Z de las preposiciones solo le importa a `Preposiciones` → estado **local**.

---

## 20. Validación en vivo con campos "tocados"

Mostrar "El email es obligatorio" antes de que el usuario escriba algo es molesto. El patrón usado en `PersonaForm.jsx`:

```jsx
const [tocados, setTocados] = useState({})                // { email: true, ... }
const [intentoEnviar, setIntentoEnviar] = useState(false)

const erroresCliente = { email: validarEmail(valores.email), ... }  // derivado

function errorDe(campo) {
  const visible = tocados[campo] || intentoEnviar
  return (visible && erroresCliente[campo]) || erroresServidor[campo] || ''
}

<input name="email" onBlur={manejarBlur} ... />   // onBlur = cuando sale del campo
{mensajeError('email')}
```

- El error aparece cuando el usuario **sale del campo** (`onBlur`) o cuando **presiona Procesar**.
- Los errores que regresa la API (`err.errores.email`) se muestran en el mismo lugar.
- `noValidate` en el `<form>` desactiva los globos de validación del navegador, para mostrar solo los mensajes propios.

### Funciones que regresan JSX vs componentes dentro de componentes

```jsx
// ❌ No declares un componente DENTRO de otro: React lo recrea en cada render
function PersonaForm() {
  function Error({ campo }) { ... }
  return <Error campo="email" />
}

// ✅ Usa una función normal que regresa JSX (o saca el componente a su propio archivo)
function PersonaForm() {
  function mensajeError(campo) { return <div>...</div> }
  return <>{mensajeError('email')}</>
}
```

---

## 21. Inputs que filtran lo que se escribe

Regla del PDF: el CP acepta **únicamente números**. Como el input es controlado, puedes "limpiar" el valor antes de guardarlo:

```jsx
function manejarCambio(evento) {
  const escrito = evento.target.value
  const soloNumeros = escrito.replace(/\D/g, '')   // \D = todo lo que NO es dígito
  setCp(soloNumeros)                               // el input mostrará solo los números
}
```

Esto es imposible con un input no controlado sin manipular el DOM. Es una de las ventajas de los formularios controlados.

---

## 22. Errores con más información: `ApiError`

`http.js` define una clase que extiende `Error`:

```js
export class ApiError extends Error {
  constructor(mensaje, status, errores = {}) {
    super(mensaje)
    this.status = status     // 404, 503, 0 si no hubo conexión...
    this.errores = errores   // { email: '...', hobby: '...' }
  }
}
```

Así los componentes pueden decidir según el tipo de error:

```jsx
catch (err) {
  if (err.status === 404) setMensaje('No existe...')
  setErroresServidor(err.errores)   // errores por campo
}
```

---

## 23. Bloquear un formulario completo: `<fieldset disabled>`

Regla del PDF: después de **Finalizar** ya no se puede editar. En lugar de poner `disabled` en cada input, se usa HTML nativo:

```jsx
<fieldset disabled={finalizado}>
  <CodigoPostal ... />
  <PersonaForm ... />
</fieldset>
```

Un `<fieldset disabled>` deshabilita **todos** los `input`, `select`, `textarea` y `button` que tenga adentro, aunque estén en componentes hijos. React no tiene que saber nada; es HTML.

Botones con reglas (`AccionesRegistro.jsx`):
```jsx
<button disabled={!puedeFinalizar}>Finalizar</button>
<button disabled={!finalizado}>Reiniciar</button>   // no se puede reiniciar sin finalizar
```

---

## 24. Reiniciar una sección completa con `key`

En la sección 12 viste que cambiar la `key` de un componente lo vuelve a crear. `RegistroPage` lo usa para **Reiniciar**:

```jsx
const [intento, setIntento] = useState(0)

function reiniciar() {
  setDatosCP(null)            // limpia el estado del padre
  setResultado(null)
  setFinalizado(false)
  setIntento((n) => n + 1)    // nueva key...
}

<fieldset key={intento}>      {/* ...y los hijos se crean de nuevo, vacíos */}
```

Sin esto, tendrías que pasarle a cada hijo una prop o función para "limpiarse". Con `key`, React descarta el estado interno de `CodigoPostal` y `PersonaForm` (el CP escrito, los valores del formulario, los campos tocados) de una sola vez.

`setIntento((n) => n + 1)` usa la **forma de función** de `setState`: recibe el valor anterior. Úsala cuando el nuevo valor depende del anterior.

---

## 25. Ordenar sin modificar las props

```jsx
// Preposiciones.jsx
let visibles = [...lista]                         // COPIA
if (orden === 'asc') visibles.sort((a, b) => a.localeCompare(b, 'es'))
```

- `sort()` **modifica** el arreglo original. Si hicieras `lista.sort()`, estarías modificando una prop, y eso está prohibido (sección 5).
- `localeCompare(b, 'es')` ordena con reglas del español (acentos, ñ).

---

## 26. Navegación simple sin librerías

`App.jsx` cambia entre dos pantallas con un estado:

```jsx
const [vista, setVista] = useState('registro')
{vista === 'registro' ? <RegistroPage /> : <UsuariosPage />}
```

Al cambiar de pestaña, la página anterior se **desmonta** y pierde su estado. Para tener URLs (`/registro`, `/usuarios`) y que funcionen los botones atrás y adelante del navegador, se usa `react-router`.

---

## 27. El servidor siempre vuelve a validar

Las validaciones de `utils/validaciones.js` sirven para la **experiencia del usuario**: avisan al instante y habilitan los botones. Pero cualquiera puede saltárselas (F12, Postman…). Por eso la API vuelve a validar todo en `PersonaService.cs`, y al **finalizar** vuelve a procesar a la persona y a consultar el CP antes de guardar.

> Regla: **el frontend valida por comodidad; el backend valida por seguridad.**

---

# Parte 3 — Estilos y componentes de interfaz

Librerías instaladas: **Tailwind CSS 4** (estilos), **lucide-react** (íconos) y **sonner** (notificaciones).

```bash
npm install tailwindcss @tailwindcss/vite lucide-react sonner
```

---

## 28. Tailwind CSS: estilos con clases

En lugar de escribir CSS en otro archivo, Tailwind te da **clases pequeñas** que se combinan en el `className`:

```jsx
<section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
```

| Clase | CSS equivalente |
|---|---|
| `p-6` | `padding: 1.5rem` |
| `rounded-2xl` | `border-radius: 1rem` |
| `bg-white` / `text-slate-500` | color de fondo / color de texto |
| `flex items-center gap-2` | `display:flex; align-items:center; gap:.5rem` |
| `grid lg:grid-cols-2` | grid de 1 columna, y de 2 a partir de pantallas grandes |

**Prefijos (variantes):** aplican la clase solo en cierta condición.
- `sm:` `lg:` → a partir de cierto ancho de pantalla (diseño responsivo). Ejemplo: `grid gap-6 lg:grid-cols-2` en `RegistroPage.jsx`.
- `hover:` `focus:` `disabled:` → estados. Ejemplo: `disabled:opacity-50` en `ui/Boton.jsx`.
- `print:hidden` → oculta el elemento al imprimir. Así, al imprimir solo sale la ficha (`ResumenRegistro`).

**Clases condicionales:** como `className` es un string de JS, se arma con template literals:
```jsx
className={`rounded-full ${completado ? 'bg-emerald-100' : 'bg-marca-100'}`}
```

**Configuración** (3 piezas):
1. `vite.config.js`: el plugin `tailwindcss()`.
2. `src/index.css`: `@import "tailwindcss";`, `@source "./";` y `@theme { --color-marca-600: ... }`, que crea clases propias como `bg-marca-600`.
3. `src/main.jsx`: `import './index.css'`.

> ⚠️ **Lección aprendida:** si instalas Tailwind con `npm run dev` corriendo, **reinicia el servidor** (Ctrl+C y `npm run dev`). Si no, solo verás el reset de estilos y ninguna clase. La línea `@source "./";` le indica a Tailwind dónde buscar las clases; sin ella, la detección automática no encontraba los `.jsx`, porque esta carpeta está dentro de otro repositorio git.

---

## 29. Componentes de interfaz reutilizables y `children`

En `src/components/ui/` hay piezas genéricas que no saben nada de usuarios ni de códigos postales: `Boton`, `Tarjeta`, `Campo`, `Alerta` y `DialogoConfirmar`. Se escriben los estilos **una vez** y se reutilizan en todas partes.

`children` es una prop especial: es **todo lo que escribes entre la etiqueta de apertura y la de cierre**.

```jsx
// Uso (PersonaForm.jsx)
<Campo id="email" etiqueta="Email" error={errorDe('email')}>
  <input id="email" ... />              {/* ← esto es children */}
</Campo>

// Definición (ui/Campo.jsx)
function Campo({ id, etiqueta, error, children }) {
  return (
    <div>
      <label htmlFor={id}>{etiqueta}</label>
      {children}                         {/* aquí se dibuja el input */}
      {error && <p>{error}</p>}
    </div>
  )
}
```

`Tarjeta` funciona igual: todo el contenido de cada sección es su `children`.

---

## 30. "El resto" de las props y esparcir props

**Recibir el resto** (`ui/Boton.jsx`):
```jsx
function Boton({ variante, icono, cargando, children, ...resto }) {
  return <button {...resto}>{children}</button>
}
```
`...resto` junta todas las props que no se nombraron (`onClick`, `type`, `disabled`, `title`…), y `{...resto}` se las pone al `<button>`. Así `Boton` acepta cualquier atributo de un botón normal sin tener que declararlos uno por uno.

**Esparcir un objeto como props** (`PersonaForm.jsx`):
```jsx
function propsDe(campo) {
  return { id: campo, name: campo, value: valores[campo], onChange: manejarCambio, ... }
}
<input {...propsDe('email')} type="email" />
```
Es lo mismo que escribir `id="email" name="email" value={...} onChange={...}`, pero sin repetirlo en los 4 inputs.

---

## 31. Íconos como componentes (lucide-react)

Cada ícono es un componente:
```jsx
import { Search } from 'lucide-react'
<Search className="size-4" aria-hidden="true" />
```

Para **pasar un ícono por props**, se pasa el componente (sin `< />`) y se renombra con mayúscula al recibirlo:
```jsx
<Boton icono={Search}>Buscar</Boton>

function Boton({ icono: Icono }) {        // icono → Icono (mayúscula = componente)
  return Icono && <Icono className="size-4" />
}
```
React necesita la mayúscula para saber que `<Icono />` es un componente y no una etiqueta HTML (sección 3).

---

## 32. Notificaciones (toasts) con sonner

1. Se coloca **una vez** el contenedor en `App.jsx`: `<Toaster position="top-right" richColors />`.
2. Desde **cualquier** componente:
```jsx
import { toast } from 'sonner'
toast.success('Registro #15 guardado')
toast.error('No se pudo finalizar')
```

Se usan para avisos pasajeros, como "guardado" o "copiado". Los **errores que el usuario debe corregir** se quedan fijos en pantalla (`Alerta` o un mensaje bajo el campo), porque un toast desaparece.

---

## 33. `useRef` y `<dialog>`: confirmar sin `window.confirm`

`window.confirm` bloquea la página y no se puede estilizar. `ui/DialogoConfirmar.jsx` usa el elemento nativo `<dialog>`:

```jsx
const ref = useRef(null)                  // 1) referencia al elemento real del DOM

useEffect(() => {                         // 2) sincroniza la prop "abierto" con el DOM
  if (abierto && !ref.current.open) ref.current.showModal()
  if (!abierto && ref.current.open) ref.current.close()
}, [abierto])

return <dialog ref={ref}>...</dialog>     // 3) React llena ref.current con el <dialog>
```

- **`useRef`** guarda un valor que **no** provoca un nuevo render al cambiar. Su uso más común es acceder a un elemento del DOM, como `document.getElementById` pero al estilo React.
- Este **sí** es un buen uso de `useEffect` (sección 11): sincronizar React con algo externo, en este caso la API `showModal()` del navegador.
- El padre controla el diálogo con estado: `const [confirmando, setConfirmando] = useState(false)` y `<DialogoConfirmar abierto={confirmando} ... />`.

---

## 34. Accesibilidad básica

| Qué | Dónde | Para qué |
|---|---|---|
| `<label htmlFor="cp">` + `<input id="cp">` | `ui/Campo.jsx` | Al hacer clic en la etiqueta se enfoca el input, y los lectores de pantalla leen la etiqueta. |
| `aria-invalid` | inputs con error | Anuncia que el campo tiene un error. |
| `role="alert"` | `Alerta` (tipo error), mensajes de campo | El lector de pantalla lee el error en cuanto aparece. |
| `aria-hidden="true"` | íconos decorativos | Evita que se lean íconos que no aportan información. |
| `aria-pressed` / `aria-current` | botones de orden / pestañas | Indica cuál opción está activa. |
| `aria-label` | botones que solo tienen ícono (editar/eliminar) | Da un nombre al botón. |

---

## 35. Siguientes pasos para aprender

1. **Ejercicio:** agrega una columna "Edad" a la tabla del CRUD, calculada a partir de `fechanacimiento` (estado derivado, sección 18).
2. **Ejercicio:** agrega un `<input>` para filtrar la tabla por nombre. Necesita estado para el texto y un `usuarios.filter(...)` antes del `map`.
3. **Ejercicio:** en `CodigoPostal`, si la consulta regresa una sola colonia, selecciónala automáticamente. Pista: en `consultar()`, después de `onDatos(respuesta)`, llama a `onColonia(...)`.
4. **Ejercicio:** en `ResumenRegistro`, agrega un botón "Copiar email", igual al "Copiar ID" de `ResultadoPersona.jsx` (`navigator.clipboard.writeText` + `toast.success`).
5. **Ejercicio (estilos):** agrega modo oscuro con la variante `dark:` de Tailwind (por ejemplo, `bg-white dark:bg-slate-900`).
6. **Rutas:** `react-router`.
7. **Pruebas automáticas:** Vitest + React Testing Library.
8. **Documentación oficial** (en español): https://es.react.dev/learn. Empieza por "Inicio rápido", "Pensar en React" y "Elegir la estructura del estado".
