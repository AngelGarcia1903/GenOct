# Requerimientos de la prueba Thona — dónde se cumple cada uno

Mapa de cada punto del documento **`Thona Seguros.pdf`** al código que lo implementa: archivo, línea, qué se hizo y qué sección de la [guía de React](P04R01_PROYECTO/FRONTEND/P04R01_REACT/GUIA_REACT.md) explica ese tipo de código.

- **Fecha:** 2026-10-06
- **Única desviación del PDF:** la tecnología. Se pide ASP.NET Framework 4.7 + MVC 5; se usó **ASP.NET Core .NET 8 (API) + React 19 (frontend)** por decisión del autor. Todo lo demás se cumple como se detalla abajo.
- **Cómo leer las referencias:**
  - 🟦 **API** = `P04R01_PROYECTO/BACKEND/P04R01_APIREST/` (C#)
  - 🟩 **WEB** = `P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/` (React)
  - **Guía §N** = sección N de `GUIA_REACT.md`
  - Los números de línea corresponden al código del 2026-10-06 (v3). Si se edita un archivo, pueden moverse unas líneas; el nombre de la función sigue sirviendo para buscar.

---

## Arquitectura en una imagen

```
Navegador (React, WEB)                         API .NET 8 (API)                         Externos
───────────────────────                        ─────────────────                         ────────
CodigoPostal.jsx ──consultarCP()──────► GET  api/CodigoPostal/{cp} ──► CodigoPostalService ──► Thona CatalogoCP (XML)
PersonaForm.jsx  ──procesarPersona()──► POST api/Persona/procesar  ──► PersonaService (reglas)
RegistroPage.jsx ──finalizarRegistro()► POST api/Persona/finalizar ──► PersonaService + CodigoPostalService ──► SQL Server Thona_PT
        ▲                                        │
        └──── services/http.js (ApiError) ◄──────┘  errores: { mensaje } / { mensaje, errores:{campo:msg} }
```

**Principio aplicado:** las reglas de negocio viven en la API (C#). El frontend repite las validaciones solo para dar retroalimentación inmediata y habilitar o deshabilitar botones (Guía §27).

---

## Característica 1 — Consulta de información a partir de un Código Postal

### Requerimientos de interfaz
| Requisito del PDF | Dónde | Qué se hizo | Guía |
|---|---|---|---|
| Un campo para capturar el CP | [🟩 CodigoPostal.jsx:63](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/CodigoPostal.jsx#L63) | `<input id="cp">` dentro de `<Campo>`, controlado por el estado `cp`. | §10, §29 |
| Un botón para realizar la consulta | [🟩 CodigoPostal.jsx:80](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/CodigoPostal.jsx#L80) | `<Boton type="submit">` con ícono y estado "Consultando". | §30, §31 |
| Una sección para mostrar resultados | [🟩 CodigoPostal.jsx:91](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/CodigoPostal.jsx#L91) | Bloque `<dl>` con estado, código y municipio, más la lista de colonias. | §7 |

### Reglas del campo
| Requisito del PDF | Dónde | Qué se hizo | Guía |
|---|---|---|---|
| Aceptar únicamente números | [🟩 CodigoPostal.jsx:30](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/CodigoPostal.jsx#L30) · [🟩 validaciones.js:7](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/utils/validaciones.js#L7) · [🟦 CodigoPostalService.cs:36](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/CodigoPostalService.cs#L36) | `onChange` elimina todo lo que no es dígito (`replace(/\D/g,'')`) y avisa "Solo se aceptan números" ([🟩 CodigoPostal.jsx:86](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/CodigoPostal.jsx#L86)). `validarCP` lo verifica en el navegador y `EsCPValido` en la API. | §21 |
| Exactamente 5 dígitos | [🟩 CodigoPostal.jsx:69](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/CodigoPostal.jsx#L69) · [🟩 validaciones.js:7](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/utils/validaciones.js#L7) · [🟦 CodigoPostalController.cs:22](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Controllers/CodigoPostalController.cs#L22) | `maxLength={5}` + `validarCP`. La API responde 400 si no son 5 dígitos. | §18 |
| Botón habilitado solo con CP válido | [🟩 CodigoPostal.jsx:25](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/CodigoPostal.jsx#L25) · [🟩 CodigoPostal.jsx:80](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/CodigoPostal.jsx#L80) | `cpValido` es estado **derivado** de `cp`; el botón usa `disabled={!cpValido}`. | §18 |
| Mensaje de validación cuando no es correcto | [🟩 CodigoPostal.jsx:63](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/CodigoPostal.jsx#L63) | `<Campo error=...>` muestra "debe tener exactamente 5 dígitos (llevas N)" después de que el usuario escribe. | §20, §29 |

### Consumo del servicio
| Requisito del PDF | Dónde | Qué se hizo | Guía |
|---|---|---|---|
| GET `https://thona-api-desarrollo.azurewebsites.net/api/CatalogoCP?CP={CP}` | [🟦 CodigoPostalService.cs:41](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/CodigoPostalService.cs#L41) · [🟦 appsettings.json:10](P04R01_PROYECTO/BACKEND/P04R01_APIREST/appsettings.json#L10) · [🟦 Program.cs:13](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Program.cs#L13) | `HttpClient` inyectado (`AddHttpClient`) con la URL base en `appsettings.json` y timeout de 15 s ([🟦 Program.cs:16](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Program.cs#L16)). | — |
| Llamada desde el frontend | [🟩 registroService.js:5](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/services/registroService.js#L5) · [🟩 CodigoPostal.jsx:38](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/CodigoPostal.jsx#L38) | `consultarCP(cp)` → `GET api/CodigoPostal/{cp}` de nuestra API, que llama a Thona. | §13 |
| Obtener nombre del estado, código del estado y colonias | [🟦 CodigoPostalService.cs:76](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/CodigoPostalService.cs#L76) · [🟦 CodigoPostalService.cs:95](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/CodigoPostalService.cs#L95) | El servicio de Thona responde **XML**; `Interpretar` lo lee con `XDocument` y lo convierte a `CodigoPostalDto` (JSON). | — |

### Salida
| Requisito del PDF | Dónde | Qué se hizo | Guía |
|---|---|---|---|
| Mostrar nombre del estado y su código | [🟩 CodigoPostal.jsx:91](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/CodigoPostal.jsx#L91) | "Estado: CIUDAD DE MEXICO", "Código de estado: 009" (también el municipio). | §7 |
| Lista seleccionable con las colonias | [🟩 CodigoPostal.jsx:109](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/CodigoPostal.jsx#L109) | `<select>` con `datos.colonias.map(...)`. La selección vive en el padre (`coloniaSel`). | §8, §19 |
| Al seleccionar, mostrar el código de la colonia | [🟩 CodigoPostal.jsx:120](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/CodigoPostal.jsx#L120) | `coloniaElegida.codigo` en un recuadro destacado. | §7, §18 |

### Manejo de errores
| Requisito del PDF | Dónde | Qué se hizo | Guía |
|---|---|---|---|
| Validar respuestas vacías o sin información | [🟦 CodigoPostalService.cs:78](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/CodigoPostalService.cs#L78) · [🟦 CodigoPostalService.cs:106](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/CodigoPostalService.cs#L106) · [🟦 CodigoPostalController.cs:29](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Controllers/CodigoPostalController.cs#L29) | Cuerpo vacío o `<COLONIAS />` sin elementos (por ejemplo, CP 99999) → `SinInformacion` → **404** "No se encontró información para el código postal 99999." | — |
| Manejar errores de comunicación | [🟦 CodigoPostalService.cs:56](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/CodigoPostalService.cs#L56) · [🟦 CodigoPostalService.cs:61](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/CodigoPostalService.cs#L61) · [🟦 CodigoPostalService.cs:49](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/CodigoPostalService.cs#L49) · [🟦 CodigoPostalService.cs:88](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/CodigoPostalService.cs#L88) | Timeout → 503. Sin conexión → 503. Thona responde error → 502. XML ilegible → 502. Cada caso tiene un mensaje claro y se registra en el log del servidor. | — |
| API local apagada o bloqueada por CORS | [🟩 http.js:22](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/services/http.js#L22) · [🟩 http.js:9](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/services/http.js#L9) | Si `fetch` falla se lanza `ApiError(status 0)` con "No se pudo conectar con la API…". | §22 |
| Mensajes claros sin que la app falle | [🟩 CodigoPostal.jsx:46](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/CodigoPostal.jsx#L46) · [🟩 CodigoPostal.jsx:87](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/CodigoPostal.jsx#L87) | `try/catch` → `<Alerta tipo="error">` con el mensaje. Los datos anteriores se limpian y la página sigue funcionando. | §13, §29 |

---

## Característica 2 — Procesamiento de información de la persona

### Requerimientos de interfaz
| Requisito del PDF | Dónde | Qué se hizo | Guía |
|---|---|---|---|
| Campo nombre completo | [🟩 PersonaForm.jsx:96](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/PersonaForm.jsx#L96) | Input controlado, con instrucción del orden (nombre(s), apellido paterno, apellido materno). | §10, §30 |
| Campo hobby | [🟩 PersonaForm.jsx:102](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/PersonaForm.jsx#L102) | Input controlado. | §10 |
| Campo fecha de nacimiento | [🟩 PersonaForm.jsx:107](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/PersonaForm.jsx#L107) | `type="date"` con `max` = hoy. | §10 |
| Campo email | [🟩 PersonaForm.jsx:112](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/PersonaForm.jsx#L112) | `type="email"`. | §10 |

### Reglas (implementadas en la API: `Services/PersonaService.cs`)
| Requisito del PDF | Dónde | Qué se hizo |
|---|---|---|
| Normalizar nombre: quitar espacios dobles | [🟦 PersonaService.cs:129](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L129) | Regex `\s+` → un espacio, más `Trim`. |
| Normalizar nombre: caracteres especiales | [🟦 PersonaService.cs:114](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L114) · [🟦 PersonaService.cs:124](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L124) | Se conservan las letras (incluidas las acentuadas y la ñ). `- . _` se convierten en espacio y el resto se elimina. Se avisa cuáles se eliminaron ("Se eliminaron caracteres no válidos: # 3"). |
| Camel case (primera letra de cada palabra en mayúscula) | [🟦 PersonaService.cs:136](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L136) | `Capitalizar` con cultura es-MX: "  maría DE la cruz " → "María De La Cruz". |
| Detectar preposiciones | [🟦 PersonaService.cs:12](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L12) · [🟦 PersonaService.cs:106](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L106) | Lista: de, del, la, las, los, el, y, e, da, das, do, dos, di, van, von, der. Se devuelven distintas y en orden de aparición. |
| Lista ordenable alfabéticamente | [🟩 Preposiciones.jsx:13](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/Preposiciones.jsx#L13) · [🟩 Preposiciones.jsx:17](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/Preposiciones.jsx#L17) | Botones A→Z, Z→A y Original. Se ordena una **copia** con `localeCompare('es')`. (Guía §25) |
| ID: 2 letras del primer apellido + 1 del segundo + 1 del nombre + fecha ddMMyy | [🟦 PersonaService.cs:167](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L167) · [🟦 PersonaService.cs:178](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L178) | Se ignoran preposiciones ("De La Cruz" → **CR**), se quitan acentos y la Ñ se toma como N. Sin segundo apellido se usa **X**. Ejemplo: "Juan Castro Flores", 10/10/1990 → **CAFJ101090**. |
| (apoyo) Separar nombre y apellidos compuestos | [🟦 PersonaService.cs:146](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L146) · [🟦 PersonaService.cs:79](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L79) | `AgruparUnidades` une cada preposición con la palabra siguiente, y los 2 últimos bloques son los apellidos: "María de los Ángeles de la Cruz Pérez" → paterno "De La Cruz". |

> ⚠️ **Nota sobre el ID:** el PDF dice "fecha ddMMyy", pero su ejemplo `CAFJ901010` está en yyMMdd ("90" no puede ser día). Se siguió la **regla escrita** (ddMMyy). Para usar el formato del ejemplo, cambia `"ddMMyy"` por `"yyMMdd"` en [🟦 PersonaService.cs:178](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L178).

### Validaciones
| Requisito del PDF | Navegador (WEB) | Servidor (API) | Guía |
|---|---|---|---|
| Email con formato | [🟩 validaciones.js:28](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/utils/validaciones.js#L28) | [🟦 PersonaService.cs:17](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L17) · [🟦 PersonaService.cs:69](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L69) | §20 |
| Fecha no futura | [🟩 validaciones.js:42](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/utils/validaciones.js#L42) | [🟦 PersonaService.cs:57](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L57) | §20 |
| Mayor de 5 años (5 cumplidos o más) | [🟩 validaciones.js:43](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/utils/validaciones.js#L43) | [🟦 PersonaService.cs:61](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L61) · [🟦 PersonaService.cs:197](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L197) | §20 |

### Resultado
| Requisito del PDF | Dónde | Qué se hizo | Guía |
|---|---|---|---|
| Nombre normalizado | [🟩 ResultadoPersona.jsx:57](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/ResultadoPersona.jsx#L57) | Se muestra en grande, junto con nombre(s), apellido paterno y apellido materno por separado. | §5 |
| ID del usuario y hobby | [🟩 ResultadoPersona.jsx:43](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/ResultadoPersona.jsx#L43) · [🟩 ResultadoPersona.jsx:64](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/ResultadoPersona.jsx#L64) | ID destacado en una tarjeta con botón **Copiar ID** ([🟩 ResultadoPersona.jsx:23](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/ResultadoPersona.jsx#L23)). | §31, §32 |
| Listado de preposiciones encontradas | [🟩 ResultadoPersona.jsx:77](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/ResultadoPersona.jsx#L77) | Componente `Preposiciones` (chips numerados). | §8, §25 |

### Manejo de errores
| Requisito del PDF | Dónde | Qué se hizo | Guía |
|---|---|---|---|
| Considerar respuestas vacías o sin información | [🟦 PersonaService.cs:36](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L36) · [🟦 PersonaService.cs:45](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L45) · [🟦 PersonaService.cs:52](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L52) · [🟦 PersonaService.cs:68](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L68) · [🟦 PersonaController.cs:36](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Controllers/PersonaController.cs#L36) | Cada campo vacío genera su mensaje. La API responde **400** `{ mensaje, errores: { campo: msg } }`. Un nombre sin apellido también se rechaza. | — |
| Errores del servidor mostrados en su campo | [🟩 PersonaForm.jsx:70](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/PersonaForm.jsx#L70) · [🟩 http.js:43](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/services/http.js#L43) | `ApiError.errores` → `erroresServidor` → aparece bajo el input correspondiente. | §22 |
| Mostrar instrucciones claras | [🟩 PersonaForm.jsx:41](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/PersonaForm.jsx#L41) · [🟩 PersonaForm.jsx:61](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/PersonaForm.jsx#L61) | Errores visibles al salir del campo (`onBlur`) o al presionar Procesar. Textos de ayuda bajo cada campo y avisos ("No se capturó segundo apellido; se usó X"). | §20 |
| Dato editado después de procesar | [🟩 PersonaForm.jsx:51](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/PersonaForm.jsx#L51) | `onCambio()` borra el resultado anterior y obliga a volver a procesar, para no finalizar datos viejos. | §19 |

---

## Característica 3 — Pantalla interactiva

| Requisito del PDF | Dónde | Qué se hizo | Guía |
|---|---|---|---|
| Botón finalizar | [🟩 AccionesRegistro.jsx:49](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/AccionesRegistro.jsx#L49) | Se habilita solo cuando se cumplen los requisitos ([🟩 AccionesRegistro.jsx:12](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/AccionesRegistro.jsx#L12)). | §18 |
| Botón reinicio | [🟩 AccionesRegistro.jsx:39](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/AccionesRegistro.jsx#L39) | `disabled={!finalizado}`. | §23 |
| No editar la información después de finalizar | [🟩 RegistroPage.jsx:108](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/pages/RegistroPage.jsx#L108) · [🟩 RegistroPage.jsx:110](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/pages/RegistroPage.jsx#L110) | `<fieldset disabled={finalizado}>` deshabilita **todos** los inputs, selects y botones de captura. Además aparece el aviso "Información finalizada" ([🟩 RegistroPage.jsx:93](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/pages/RegistroPage.jsx#L93)). | §23 |
| Reinicio no presionable sin haber finalizado | [🟩 AccionesRegistro.jsx:39](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/AccionesRegistro.jsx#L39) | El botón está deshabilitado y su `title` explica "Disponible después de finalizar". | §23 |
| Reiniciar deja todo como nuevo | [🟩 RegistroPage.jsx:68](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/pages/RegistroPage.jsx#L68) · [🟩 RegistroPage.jsx:76](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/pages/RegistroPage.jsx#L76) | Limpia el estado del padre y cambia la `key` del `<fieldset>`, así los formularios se vuelven a crear vacíos. | §24 |

### Resultado / toque personal
| Propuesta | Dónde | Qué aporta | Guía |
|---|---|---|---|
| **Guardar en la base de datos al finalizar** | [🟦 PersonaController.cs:43](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Controllers/PersonaController.cs#L43) · `BD/P04R01_BD_AlterTable_Registro.sql` | Persiste el registro completo en `Thona_PT.TBLUsuarios`: ID generado, preposiciones, CP, estado, municipio, colonia y fecha de registro. | — |
| **El servidor no confía en el navegador** | [🟦 PersonaController.cs:63](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Controllers/PersonaController.cs#L63) · [🟦 PersonaController.cs:73](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Controllers/PersonaController.cs#L73) | Al finalizar vuelve a procesar a la persona y a consultar el CP, y verifica que la colonia pertenezca al CP. | §27 |
| Error al guardar controlado | [🟦 PersonaController.cs:100](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Controllers/PersonaController.cs#L100) · [🟩 RegistroPage.jsx:62](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/pages/RegistroPage.jsx#L62) | Si falla la BD responde 500 `{ mensaje }`; el frontend muestra una alerta y un toast, y no bloquea la pantalla. | §32 |
| Indicador de pasos | [🟩 Pasos.jsx:4](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/Pasos.jsx#L4) · [🟩 RegistroPage.jsx:34](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/pages/RegistroPage.jsx#L34) | Muestra el avance 1-2-3 (Pendiente / Completado). | §18 |
| Checklist de lo que falta | [🟩 RegistroPage.jsx:29](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/pages/RegistroPage.jsx#L29) · [🟩 AccionesRegistro.jsx:24](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/AccionesRegistro.jsx#L24) | Indica exactamente qué falta para poder finalizar. | §18 |
| Confirmación antes de finalizar | [🟩 RegistroPage.jsx:142](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/pages/RegistroPage.jsx#L142) · [🟩 DialogoConfirmar.jsx:12](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/ui/DialogoConfirmar.jsx#L12) | Diálogo propio con `<dialog>` en lugar de `window.confirm`. | §33 |
| Ficha imprimible / PDF | [🟩 ResumenRegistro.jsx:30](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/ResumenRegistro.jsx#L30) | `window.print()`; con `print:hidden` solo se imprime la ficha. | §28 |
| Notificaciones | [🟩 App.jsx:55](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/App.jsx#L55) | Toasts de "Datos procesados", "Registro #N guardado" e "ID copiado". | §32 |
| Diseño responsivo y accesible | Todos los componentes | Tailwind (`sm:` / `lg:`), etiquetas `label`, `aria-*`, `role="alert"`. | §28, §34 |
| CRUD de usuarios | `pages/UsuariosPage.jsx` | Segunda pestaña para ver, editar y eliminar los registros guardados. | Parte 1 |

---

## Manejo de errores — vista por capas

| Capa | Archivo | Qué maneja |
|---|---|---|
| Servicio externo (Thona) | [🟦 CodigoPostalService.cs:41](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/CodigoPostalService.cs#L41) | Timeout, sin conexión, HTTP de error, XML inválido, respuesta vacía. Clasifica en `Ok / SinInformacion / ErrorServicio / SinComunicacion`. |
| Controladores (API) | [🟦 CodigoPostalController.cs:29](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Controllers/CodigoPostalController.cs#L29) · [🟦 PersonaController.cs:36](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Controllers/PersonaController.cs#L36) · [🟦 PersonaController.cs:100](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Controllers/PersonaController.cs#L100) | Traducen a códigos HTTP: 400 validación, 404 sin información, 502/503 servicio, 500 BD. El cuerpo siempre es `{ mensaje }` o `{ mensaje, errores }`. |
| Reglas de negocio (API) | [🟦 PersonaService.cs:25](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Services/PersonaService.cs#L25) | Junta **todos** los errores por campo en un diccionario, no solo el primero. |
| Cliente HTTP (WEB) | [🟩 http.js:9](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/services/http.js#L9) · [🟩 http.js:38](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/services/http.js#L38) | `ApiError` con `status` y `errores`. Status 0 = no hubo conexión. Lee `{ mensaje }`, `title` (ProblemDetails) o texto plano. |
| Validación inmediata (WEB) | [🟩 validaciones.js:7](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/utils/validaciones.js#L7) | Las mismas reglas que la API, para avisar al instante y habilitar botones. |
| Componentes (WEB) | [🟩 CodigoPostal.jsx:46](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/CodigoPostal.jsx#L46) · [🟩 PersonaForm.jsx:70](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/PersonaForm.jsx#L70) · [🟩 RegistroPage.jsx:62](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/pages/RegistroPage.jsx#L62) | `try/catch` → `Alerta` o mensaje bajo el campo, y toasts para avisos pasajeros. Ningún error deja la página en blanco. |

---

## Buenas prácticas aplicadas

| Práctica | Dónde | Guía |
|---|---|---|
| Separación en capas: Controllers / Services / Models (API) y pages / components / services / utils (WEB) | Estructura de carpetas | §17 |
| Inyección de dependencias (`HttpClient`, servicios, `DbContext`) | [🟦 Program.cs:13](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Program.cs#L13) · [🟦 Program.cs:20](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Program.cs#L20) | — |
| Configuración fuera del código (URL de Thona, conexión a BD, URL de la API) | `appsettings.json`, `.env` | §13 |
| Validación doble (cliente y servidor) | ver tabla de validaciones | §27 |
| Estado derivado en lugar de duplicado | [🟩 RegistroPage.jsx:29](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/pages/RegistroPage.jsx#L29) · [🟩 CodigoPostal.jsx:25](P04R01_PROYECTO/FRONTEND/P04R01_REACT/src/components/registro/CodigoPostal.jsx#L25) | §18 |
| Componentes de interfaz reutilizables | `components/ui/` | §29 |
| El `PUT` del CRUD solo actualiza campos básicos (no borra datos de Thona) | [🟦 UsuariosController.cs:84](P04R01_PROYECTO/BACKEND/P04R01_APIREST/Controllers/UsuariosController.cs#L84) | — |
| Scripts SQL idempotentes con transacción | `BD/P04R01_BD_AlterTable_Registro.sql` | — |
| Historial de cambios documentado | `HISTORIAL.md` | — |

---

## Archivos relacionados
- [HISTORIAL.md](HISTORIAL.md): historial de todos los cambios.
- [DOCUMENTACION_FRONTEND.md](P04R01_PROYECTO/FRONTEND/P04R01_REACT/DOCUMENTACION_FRONTEND.md): comandos, estructura y código completo del frontend.
- [GUIA_REACT.md](P04R01_PROYECTO/FRONTEND/P04R01_REACT/GUIA_REACT.md): manual de React con ejemplos del proyecto.
