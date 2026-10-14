# HISTORIAL

Historial de cambios del proyecto **P04R01_BD_JAGJ** (API REST C# + base de datos SQL Server `Thona_PT` + frontend React).

---

## 2026-10-12 — Manual de APIs v1.1 (correcciones de uso real)

Se aplicaron las correcciones de `P07R01\CORRECCIONES_MANUAL_API.md`, detectadas al construir la API `CamionesAPI` siguiendo el manual. El resto del contenido se conservó igual.

| # | Corrección | Dónde (manual v1.1) |
|---|---|---|
| 1 | Se quitaron todas las menciones a la "lista original" / "pasos originales". El capítulo 13 ahora es solo "Observaciones sobre el proyecto P04R01_APIREST" (se eliminó la tabla comparativa). | Caps. 2, 4, 5.1, 6.2, 8.2, 11, 13 e índice |
| 2 | Forma B de NuGet: versión exacta (`-Version 8.0.31`) porque la consola no acepta `8.0.*`, más `-ProjectName` y un Error común sobre «Proyecto predeterminado» en soluciones con varios proyectos. | 4.2 |
| 3 | El proyecto Web API se identifica por el **globo azul**, no por el ícono de C#. | 5.1 |
| 4 | Nueva sección de los `using` de las anotaciones, con el atajo Ctrl+. | 5.3 (las siguientes se recorrieron: 5.4–5.8, con sus referencias actualizadas) |
| 5 | Relaciones: la FK como `int` basta, la navegación es opcional, no se pone `[Key]` en una FK y el orden de las clases no importa. | 5.6 |
| 6 | Error común: dentro de la cadena de conexión se usa `=`, no `:`. | 7.2 |
| 7 | Error común: el nombre de la conexión va entre comillas (CS0103). También se aclaró en 1.2 que las comillas dobles se conservan al reemplazar un marcador. | 8.2 y 1.2 |
| 8 | Explicación de qué es CORS: origen, quién lo aplica y los dos lugares donde se configura. | 8.3 |
| 9 | Dónde va el campo `private readonly` y el constructor dentro del controlador. | 9.3 |
| 10 | Errores comunes al copiar la plantilla CRUD: CS0029 en GetPorId, el nombre del DbSet y los `using` agregados por error. | 9.4 |
| 11 | El patrón de los 5 métodos y los 4 pasos de los que reciben `id`. | 9.5 |

- Ajuste de formato: el título de cada bloque de código ya no puede quedar separado de su código al final de una página.
- El manual pasó de 44 a 45 páginas. La versión 1.0 quedó respaldada fuera del proyecto, en la carpeta temporal de esta sesión.
- `Mapa_Visual_API_ASPNET_Core.pdf` no necesitó cambios: no menciona la lista original y sus referencias (capítulos y secciones 8.2 y 9.3) siguen siendo válidas.

---

## 2026-10-11 — Manual para crear APIs con ASP.NET Core

- **Nuevo:** `Manual_API_ASPNET_Core.pdf` (raíz del proyecto, 44 páginas). Manual paso a paso, construido a partir de la lista de 9 pasos del autor y del proyecto `P04R01_APIREST`, corregido y ampliado (Paso 0 de requisitos, Paso 9 de pruebas, solución de problemas, equivalencias por base de datos, frontend, versión de .NET y lenguaje, apéndices con plantillas).
- Las plantillas de código del manual se compilaron en proyectos de prueba temporales (.NET 8 con EF Core 8, y .NET 10 con OpenAPI + Swagger UI) antes de publicarlo.
- **Sin cambios** en la API, el frontend ni la base de datos.
- **Nuevo:** `Mapa_Visual_API_ASPNET_Core.pdf` (raíz, 4 hojas horizontales), complemento visual del manual: (1) mapa mental de los 10 pasos con el capítulo del manual de cada uno, (2) recorrido de una petición y cómo se crea el DbContext, (3) archivos y nombres que deben coincidir, (4) qué cambia según la base de datos, el frontend, la versión o la herramienta, más una guía rápida del CRUD.

---

## 2026-10-06 — Diseño del frontend (v3) y trazabilidad de requerimientos

### Resumen
- **Backend y base de datos: sin cambios en esta versión.**
- Frontend con diseño propio: Tailwind CSS 4, íconos (lucide-react) y notificaciones (sonner). La lógica de la v2 se mantiene.
- Nuevo **`REQUERIMIENTOS_THONA.md`** (raíz del proyecto): cada requisito del PDF con su archivo:línea, qué se hizo, manejo de errores por capas y la sección de `GUIA_REACT.md` que lo explica.
- Nota: en la carpeta `BD\` el script de tablas ahora se llama `P04R01_BD_CreateTables.sql` (antes `P03R01_…`). El cambio lo hizo el autor.

### Comando ejecutado (en `P04R01_PROYECTO\FRONTEND\P04R01_REACT\`)
```bash
npm install tailwindcss @tailwindcss/vite lucide-react sonner
```
Versiones: tailwindcss 4.3.3, @tailwindcss/vite 4.3.3, lucide-react 1.52.0, sonner 2.0.8. Resultado: 0 vulnerabilidades.

> ⚠️ Después de instalar Tailwind hay que reiniciar `npm run dev`; si no, la página se ve sin estilos.

### Archivos (ruta relativa a `P04R01_PROYECTO\FRONTEND\P04R01_REACT\`)
| Archivo | Acción | Detalle |
|---|---|---|
| `package.json` / `package-lock.json` | Modificado | Las 4 dependencias nuevas. |
| `vite.config.js` | Modificado | Plugin `tailwindcss()`. |
| `src/index.css` | Nuevo | `@import "tailwindcss"`, `@source "./"` y la paleta `marca-*` en `@theme`. Sin `@source`, Tailwind no detectaba las clases, porque la carpeta está dentro de otro repositorio git. |
| `src/main.jsx` | Modificado | `import './index.css'`. |
| `src/components/ui/Boton.jsx`, `Tarjeta.jsx`, `Campo.jsx`, `Alerta.jsx`, `DialogoConfirmar.jsx`, `estilos.js` | Nuevos | Componentes de interfaz reutilizables. |
| `src/components/registro/Pasos.jsx` | Nuevo | Indicador de avance 1-2-3. |
| `src/App.jsx` | Modificado | Encabezado, pestañas con íconos y `<Toaster />`. |
| `src/pages/RegistroPage.jsx` | Modificado | Diseño en dos columnas, indicador de pasos, `requisitos` (checklist), `DialogoConfirmar` en lugar de `window.confirm`, toasts y `print:hidden`. |
| `src/components/registro/*.jsx` | Modificados | Mismo comportamiento con el nuevo diseño. `AccionesRegistro` ahora recibe `requisitos` (antes `pendientes`). `PersonaForm` recibe `procesado`. `ResultadoPersona` agrega **Copiar ID**. |
| `src/pages/UsuariosPage.jsx`, `src/components/UsuarioForm.jsx`, `UsuariosTabla.jsx`, `BuscarUsuario.jsx` | Modificados | Diseño nuevo, toasts, `DialogoConfirmar` para eliminar y fila en edición resaltada. |
| `DOCUMENTACION_FRONTEND.md` | Actualizado | v3. |
| `GUIA_REACT.md` | Actualizado | **Parte 3** (§28–§34): Tailwind, `children`, `...resto`, íconos, toasts, `useRef` + `<dialog>`, accesibilidad. |

### Archivo nuevo en la raíz
| Archivo | Detalle |
|---|---|
| `REQUERIMIENTOS_THONA.md` | Trazabilidad PDF → código, con enlaces a archivo:línea. |

### Verificación
- `npm run lint` y `npm run build` sin avisos ni errores.
- Revisión visual en escritorio (1280 px) y celular (375 px) en una instancia aparte de Vite (puerto 5180), que ya se detuvo. El servidor `npm run dev` del autor (5173) no se tocó; **hay que reiniciarlo** para que cargue Tailwind.

---

## 2026-10-06 — Prueba técnica Thona Seguros (frontend React)

### Resumen
- Nueva pestaña **Registro Thona** con las 3 características del PDF. El CRUD anterior quedó en la pestaña **Usuarios (CRUD)**.
- **Toque personal:**
  - Lista de lo que falta para finalizar.
  - Confirmación antes de finalizar.
  - Al finalizar se guarda en la BD, y el servidor vuelve a validar todo.
  - Ficha del registro con botón **Imprimir / Guardar PDF**.
  - Si se edita un dato ya procesado, hay que volver a procesar.
- Sin paquetes npm nuevos. `npm run lint` y `npm run build` sin avisos.
- Detalle completo con el código: `P04R01_PROYECTO\FRONTEND\P04R01_REACT\DOCUMENTACION_FRONTEND.md` (v2).
- Guía de React actualizada con la **Parte 2** (secciones 17–28): `GUIA_REACT.md`.

### Archivos (ruta relativa a `P04R01_PROYECTO\FRONTEND\P04R01_REACT\`)
| Archivo | Acción | Detalle |
|---|---|---|
| `src/App.jsx` | Reescrito | Ahora solo son pestañas: Registro Thona y Usuarios (CRUD). |
| `src/pages/UsuariosPage.jsx` | Nuevo | El CRUD que antes estaba en `App.jsx`. Solo se ajustaron imports y nombres. |
| `src/pages/RegistroPage.jsx` | Nuevo | Estado compartido de la prueba, `<fieldset disabled>` después de Finalizar y Reiniciar con `key`. |
| `src/components/registro/CodigoPostal.jsx` | Nuevo | Característica 1: solo números, 5 dígitos, botón condicionado, estado y colonias, código de colonia, errores. |
| `src/components/registro/PersonaForm.jsx` | Nuevo | Característica 2: captura con validación en vivo y errores del servidor por campo. |
| `src/components/registro/ResultadoPersona.jsx` | Nuevo | Nombre normalizado, ID, hobby, avisos. |
| `src/components/registro/Preposiciones.jsx` | Nuevo | Lista ordenable A→Z / Z→A / original. |
| `src/components/registro/AccionesRegistro.jsx` | Nuevo | Característica 3: Finalizar y Reiniciar con sus reglas. |
| `src/components/registro/ResumenRegistro.jsx` | Nuevo | Ficha del registro guardado, imprimible. |
| `src/services/http.js` | Nuevo | `peticion()`, `postJson()` y la clase `ApiError` (status + errores por campo). |
| `src/services/registroService.js` | Nuevo | `consultarCP`, `procesarPersona`, `finalizarRegistro`. |
| `src/services/usuariosService.js` | Modificado | Ahora usa `http.js`; antes tenía su propio `fetch`. |
| `src/utils/validaciones.js` | Nuevo | Las mismas reglas que la API (CP, nombre, hobby, email, fecha, edad ≥ 5). |
| `src/components/BuscarUsuario.jsx` | Modificado | Detecta el 404 con `error.status === 404`. |
| `DOCUMENTACION_FRONTEND.md` | Actualizado | v2: estructura, requisitos del PDF contra la implementación, código y pruebas. |
| `GUIA_REACT.md` | Actualizado | Sección 13 (servicios) y Parte 2 nueva. |

### Pruebas en navegador (API + BD real)
- Letras en el CP se ignoran con aviso; con menos de 5 dígitos aparece mensaje y el botón queda deshabilitado; `06700` muestra el estado, el municipio, las 2 colonias y el código de la colonia; `99999` muestra "No se encontró información…".
- Persona: vacíos, email inválido y menor de 5 años con mensaje por campo; nombre desordenado → normalizado, ID `CRPM070395`, aviso de caracteres eliminados, preposiciones ordenables.
- Editar después de procesar invalida el resultado. Finalizar guarda (fila verificada en SQL) y bloquea los 8 controles de captura. Reiniciar solo se habilita después de finalizar y deja todo vacío.
- El registro de prueba se borró de la BD.

---

## 2026-10-06 — Prueba técnica Thona Seguros (backend y BD)

### Contexto y decisiones
- El documento `Thona Seguros.pdf` pide 3 características: consulta de código postal, procesamiento de persona y pantalla con Finalizar/Reiniciar.
- ⚠️ **Decisión del autor:** la prueba pide **ASP.NET Framework 4.7 + MVC 5**, pero se decidió seguir con el stack actual (**ASP.NET Core .NET 8 + React**).
- La API de Thona (`GET https://thona-api-desarrollo.azurewebsites.net/api/CatalogoCP?CP={CP}`) regresa **XML**. Un CP inexistente regresa 200 con `<COLONIAS />` vacío; un CP con letras regresa 500.
- Las reglas viven en la API (C#) para que el servidor no dependa de lo que mande el navegador. React también valida, pero solo para la experiencia de usuario.
- **ID de usuario:** el PDF dice "fecha ddMMyy", pero su ejemplo `CAFJ901010` está en yyMMdd ("90" no puede ser día). Se siguió la regla escrita (**ddMMyy**). Se cambia en una línea de `Services\PersonaService.cs` (`GenerarId`).
- **"Mayor de 5 años"** se interpretó como tener al menos 5 años cumplidos.
- **Camel case:** se aplica a cada palabra, incluidas las preposiciones ("María De La Cruz"), como dice la regla literal.

### Base de datos (carpeta `BD\`)
| Archivo | Acción |
|---|---|
| `P04R01_BD_AlterTable_Registro.sql` | **Nuevo.** Agrega a `TBLUsuarios`: `idUsuarioGenerado`, `preposiciones`, `cp`, `codEstado`, `estado`, `municipio`, `codColonia`, `colonia` y `fechaRegistro` (con DEFAULT GETDATE()). Todas aceptan NULL y el script es idempotente. **Ya se ejecutó en `Thona_PT`.** |

Orden de ejecución completo: `P04R01_BD_CreateDataBase.sql` → `P03R01_BD_CreateTables.sql` → `P04R01_BD_AlterTable_Registro.sql` → `P04R01_BD_InitialCharge.sql` (opcional).

### API (ruta relativa a `P04R01_PROYECTO\BACKEND\P04R01_APIREST\`)
| Archivo | Acción | Detalle |
|---|---|---|
| `Models\CodigoPostalDto.cs` | Nuevo | `CodigoPostalDto`, `ColoniaDto` y `ErrorDto` (formato de error `{ mensaje }`). |
| `Services\CodigoPostalService.cs` | Nuevo | Llama a Thona con `HttpClient` (timeout de 15 s), convierte el XML a DTO y clasifica el resultado: Ok, SinInformacion, ErrorServicio o SinComunicacion. |
| `Controllers\CodigoPostalController.cs` | Nuevo | `GET api/CodigoPostal/{cp}` → 200 con datos / 400 CP inválido / 404 sin información / 502 error del servicio / 503 sin comunicación. |
| `Models\PersonaDto.cs` | Nuevo | `PersonaRequest`, `PersonaResultado`, `FinalizarRequest`, `RegistroDto` y `ValidacionDto` (`{ mensaje, errores: {campo: msg} }`). |
| `Services\PersonaService.cs` | Nuevo | Normaliza el nombre (quita caracteres especiales y espacios dobles), lo pone en camel case, detecta preposiciones, agrupa apellidos compuestos ("De La Cruz") y genera el ID. Valida campos obligatorios, email, fecha no futura y edad ≥ 5. |
| `Controllers\PersonaController.cs` | Nuevo | `POST api/Persona/procesar` (solo procesa) y `POST api/Persona/finalizar` (vuelve a validar la persona, vuelve a consultar el CP, confirma que la colonia pertenece al CP y guarda en `TBLUsuarios`). |
| `Models\TBLUsuarios.cs` | Modificado | Propiedades nuevas, todas opcionales. `fechaRegistro` lleva `[DatabaseGenerated(Computed)]` para que la llene SQL Server. |
| `Controllers\UsuariosController.cs` | Modificado | `PUT` ahora carga el registro y solo actualiza `nombre`, `hobby`, `fechanacimiento` y `email`. Antes reemplazaba la fila completa y habría borrado las columnas nuevas. Si el id no existe regresa 404. |
| `Program.cs` | Modificado | `AddHttpClient<CodigoPostalService>` y `AddScoped<PersonaService>`. |
| `appsettings.json` | Modificado | Sección `ThonaApi:BaseUrl`. |
| `P04R01_APIREST.http` | Modificado | Peticiones de prueba para `CodigoPostal`, `Persona/procesar` y `Persona/finalizar`. Ojo: `finalizar` sí guarda en la BD. |

### Pruebas realizadas (API corriendo + BD real)
- CP `06700` → 200 con estado CIUDAD DE MEXICO (009) y colonias ROMA / ROMA NORTE. CP `99999` → 404 "No se encontró información…". `abc12` y `123` → 400. URL de Thona inválida → 503 "No se pudo conectar…".
- `"  juan   castro  flores "` (10/10/1990) → "Juan Castro Flores", ID `CAFJ101090`.
- `"maría de los ángeles DE LA cruz pérez"` → apellido paterno "De La Cruz", ID `CRPM070395`, preposiciones `de, los, la`.
- `"José Luis#3 Muñoz-Ñúñez"` → aviso de caracteres eliminados, ID `MUNJ310100`. `"Ana López"` → ID `LOXA011288` (X por falta de segundo apellido).
- Vacíos, email inválido, fecha futura y menor de 5 años → 400 con un mensaje por campo.
- Finalizar: colonia de otro CP → 400; CP sin información → 400; datos correctos → 200 y fila guardada con todas las columnas. Un `PUT` del CRUD sobre esa fila conservó las columnas de Thona. La fila de prueba se borró.

---

## 2026-10-06 — Creación del frontend en React

### Resumen
- Se creó el frontend en `P04R01_PROYECTO\FRONTEND\P04R01_REACT\` con React 19 + Vite 8, sin estilos.
- Hace el CRUD completo de usuarios contra la API: listar, buscar por ID, crear, editar y eliminar.
- Se probó de punta a punta en el navegador contra la base de datos real. El registro de prueba se borró al terminar.
- Detalle completo (comandos, componentes y código): `P04R01_PROYECTO\FRONTEND\P04R01_REACT\DOCUMENTACION_FRONTEND.md`.
- Guía de aprendizaje de React: `P04R01_PROYECTO\FRONTEND\P04R01_REACT\GUIA_REACT.md`.

### Archivos modificados en la API
Ruta relativa a `P04R01_PROYECTO\BACKEND\P04R01_APIREST\`.

#### `Program.cs` (raíz de la API)
- **Problema:** CORS solo permitía `http://localhost:4200` (Angular), y React con Vite corre en `http://localhost:5173`. El navegador habría bloqueado todas las peticiones del frontend.
- **Cambio:** `policy.WithOrigins("http://localhost:4200")` → `policy.WithOrigins("http://localhost:4200", "http://localhost:5173")`.

### Archivos del frontend
Ruta relativa a `P04R01_PROYECTO\FRONTEND\P04R01_REACT\`.

| Archivo | Acción |
|---|---|
| `.env` | Nuevo: URL de la API (`VITE_API_URL`). |
| `src/services/usuariosService.js` | Nuevo: llamadas `fetch` a la API. |
| `src/components/UsuarioForm.jsx` | Nuevo: formulario para crear y editar. |
| `src/components/UsuariosTabla.jsx` | Nuevo: tabla con la lista. |
| `src/components/BuscarUsuario.jsx` | Nuevo: búsqueda por ID. |
| `src/App.jsx` | Reescrito: componente principal. |
| `src/main.jsx` | Modificado: se quitó el import de `index.css`. |
| `index.html` | Modificado: `lang="es"` y título. |
| `src/App.css`, `src/index.css`, `src/assets/*` | Eliminados (de la plantilla de Vite, sin estilos por ahora). |
| `DOCUMENTACION_FRONTEND.md`, `GUIA_REACT.md` | Nuevos: documentación. |

---

## 2026-10-06 — Verificación de compatibilidad API ↔ Base de datos

### Resultado de la verificación

| Prueba | Resultado |
|---|---|
| La base de datos `Thona_PT` existe en el servidor `Angelgar` | ✅ |
| La tabla `dbo.TBLUsuarios` existe y sus columnas coinciden con el modelo `Models/TBLUsuarios.cs` (nombre, tipo y si acepta nulos) | ✅ |
| La API compila (0 errores) | ✅ |
| La API se conecta a la BD con la cadena `Conexion` (autenticación de Windows) | ✅ |
| `GET /api/usuarios` lista los registros | ✅ |
| `GET /api/usuarios/{id}` regresa uno; con un id que no existe regresa 404 | ✅ |
| `POST`, `PUT` y `DELETE /api/usuarios` escriben en la BD | ✅ (se creó, se editó y se borró un registro de prueba; no quedó basura en la tabla) |
| Perfil `http` de Visual Studio (puertos 5283 y 7374) | ❌ antes / ✅ después de los cambios |
| CORS para Angular (`http://localhost:4200`) con PUT/DELETE | ❌ antes / ✅ después de los cambios |

### Archivos modificados

Todas las rutas son relativas a la carpeta de la API:
`P04R01_PROYECTO\BACKEND\P04R01_APIREST\`

No se modificó ningún script SQL de la carpeta `BD\`.

#### 1. `Properties\launchSettings.json` (carpeta `Properties`)
- **Problema A:** en el perfil `http`, `applicationUrl` tenía un espacio después del `;`:
  `"http://localhost:5283; http://localhost:7374"`.
  Al arrancar, la API se caía con este error:
  `Unrecognized scheme in server address ' http://localhost:7374'`.
- **Cambio A:** se quitó el espacio → `"http://localhost:5283;http://localhost:7374"`.
- **Problema B:** el archivo tenía líneas comentadas con `//`. Visual Studio las tolera, pero el JSON no admite comentarios, y al ejecutar `dotnet run` el archivo no se podía leer: la API arrancaba en el puerto 5000, en modo *Production* y sin Swagger.
- **Cambio B:** se quitaron las dos líneas comentadas. Estos eran sus valores, por si los necesitas de nuevo:
  - `//"launchUrl": "swagger",`
  - `//"applicationUrl": "http://localhost:5283",`

#### 2. `Program.cs` (raíz de la API)
- **Problema:** la política CORS `AllowAnyHeader()` estaba escrita dos veces y faltaba `AllowAnyMethod()`. Por eso el navegador bloqueaba desde Angular las peticiones **PUT** y **DELETE** (el preflight no devolvía `Access-Control-Allow-Methods`).
- **Cambio:** el segundo `.AllowAnyHeader()` se cambió por `.AllowAnyMethod()`.

#### 3. `appsettings.json` (raíz de la API)
- **Problema:** la cadena de conexión funcionaba, pero se contradecía a sí misma (`Trusted_Connection=false` e `Integrated Security=True` son la misma opción con valores opuestos) y tenía un espacio en `Database=Thona_PT ;`.
- **Cambio:** se limpió sin cambiar su comportamiento:
  - Antes: `Server=Angelgar;Database=Thona_PT ; Trusted_Connection=false;Encrypt=True;MultipleActiveResultSets=true;Integrated Security=True;TrustServerCertificate=True;`
  - Después: `Server=Angelgar;Database=Thona_PT;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=true;`

#### 4. `Controllers\UsuariosController.cs` (carpeta `Controllers`)
- **Cambio:** en `Get(int id)` se quitó un `throw;` que estaba después de `return BadRequest(...)`. Nunca se ejecutaba (código inalcanzable).

#### 5. `P04R01_APIREST.http` (raíz de la API)
- **Cambio:** se agregaron dos peticiones de prueba, `GET /api/usuarios` y `GET /api/usuarios/1`, para probar desde Visual Studio. Antes solo tenía la de `weatherforecast`.

### Recomendaciones (no aplicadas)
- **Versión de Entity Framework:** el proyecto es `net8.0`, pero usa `Microsoft.EntityFrameworkCore.SqlServer` y `.Tools` en la versión **6.0.0**. Funciona, pero EF Core 6 ya no tiene soporte. Se recomienda actualizar ambos paquetes a **8.0.x** desde el Administrador de paquetes NuGet.
- **Nombre del script SQL:** `BD\P03R01_BD_CreateTables.sql` usa el prefijo `P03` y los demás usan `P04`. Si es un error, conviene renombrarlo a `P04R01_BD_CreateTables.sql`.
- **Orden de ejecución de los scripts:** `P04R01_BD_CreateDataBase.sql` → `P03R01_BD_CreateTables.sql` → `P04R01_BD_InitialCharge.sql`. Los de `BD\CONTINGENCIA\` borran la tabla o la base de datos; úsalos solo para reiniciar.
- **Perfil `https`:** como `Program.cs` usa `UseHttpsRedirection()`, conviene que Angular llame a la URL `https://localhost:7202` al usar ese perfil. Si llama a la URL http, la redirección rompe el preflight de CORS.
- **Saltos en `idUsuarios`** (por ejemplo, de 4 a 1003): son normales en SQL Server cuando se reinicia el servicio (caché de IDENTITY). No es un error.
