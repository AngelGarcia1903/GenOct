# Correcciones al Manual API ASP.NET Core

Archivo: `Manual_API_ASPNET_Core.pdf` (versión 1.0, octubre 2026)
Origen: errores y dudas encontrados al seguir el manual paso a paso con el proyecto P07R01 (`CamionesAPI`).

**Regla general:** el manual no debe mencionar una "lista original" o "pasos originales". La única referencia externa permitida es el proyecto **P04R01_APIREST**, que se cita como el proyecto base del manual.

---

## 1. Quitar todas las referencias a la "lista original"

| Sección | Dice | Debe decir |
|---|---|---|
| 2 (Paso 0), intro | "Este paso no estaba en la lista original, pero sin él los demás fallan." | "Sin estos requisitos, los demás pasos fallan." |
| 4 (Paso 2), intro | "En la lista original este era el Paso 4, pero **va antes** de crear el DbContext: sin estos paquetes, DbContext, DbSet y UseSqlServer no existen y el código no compila." | "Instala estos paquetes **antes** de crear los modelos y el DbContext: sin ellos, `DbContext`, `DbSet` y `UseSqlServer` no existen y el código no compila." |
| 5.1, Error común | "en la lista original decía «clic en la solución». Las carpetas se agregan al proyecto..." | "Las carpetas se agregan al **proyecto**, no a la solución. Si las agregas a la solución, se crean «carpetas de solución», que son virtuales y no compilan código." |
| 6.2, título | "Qué va dentro (este es el archivo del Paso 5 original)" | "Qué va dentro" |
| 6.2, primer párrafo | "Todo lo que la lista original describía en el Paso 5 («importar librerías, heredar, crear constructor, crear el DbSet») se escribe en este archivo..." | "En este archivo se importan las librerías, se hereda de `DbContext`, se crea el constructor y se declara un `DbSet` por tabla: `Context/AppDbContext.cs`." |
| 6.2, Error común | "en la lista original decía «heredar a la clase constructora el AppDbContext». Lo correcto es:..." | "La clase `AppDbContext` **hereda de** `DbContext` (no al revés), y además tiene un **constructor** que recibe las opciones." |
| 8.2, título | "8.2 Registrar el DbContext (lo que en la lista decía «crear el builder para agregar contexto»)" | "8.2 Registrar el DbContext" |
| 8.2, índice (pág. 3) | Mismo título largo en el índice. | "8.2 Registrar el DbContext" |
| 11 (Paso 9), intro | "Este paso faltaba en la lista original. Una API no está terminada hasta probar cada endpoint." | "Una API no está terminada hasta probar cada endpoint." |
| 13, capítulo completo | "Revisión de los pasos originales" + tabla que compara la lista original con el manual. | **Eliminar la tabla.** Conservar solo lo de 13.1 como capítulo propio: "13. Observaciones sobre el proyecto P04R01_APIREST". |
| Índice (págs. 2–4) | Entradas 6.2, 8.2 y 13 con los títulos anteriores. | Actualizar con los títulos nuevos. |

---

## 2. Paso 2, sección 4.2, Forma B: el comando falla

### Error 1: el asterisco en la versión
**Dice:**
```
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.*
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.*
```
**Qué pasa:** la Consola del Administrador de paquetes **no acepta comodines** y responde:
```
Install-Package : Error al analizar la entrada del parámetro de versión 8.0.* en una versión semántica válida.
```
**Debe decir:** usar la versión **exacta**, la misma en ambos paquetes:
```
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.31 -ProjectName «Proyecto»
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.31 -ProjectName «Proyecto»
```
Y agregar la nota: *"La versión exacta más reciente se consulta en nuget.org (pestaña Versions del paquete) o en la lista Versión de la Forma A. El comodín `8.0.*` solo funciona en la terminal (Forma C, `dotnet add package`)."*

### Error 2: se instala en el proyecto equivocado
**Qué pasa:** si la solución tiene varios proyectos (por ejemplo, un n-capas con Web Forms en .NET Framework 4.8), la consola instala en el proyecto que aparece en **«Proyecto predeterminado»**, que puede no ser la API:
```
...respecto al proyecto 'CamionesWeb', con el destino '.NETFramework,Version=v4.8.1'
No se pudo instalar el paquete ... el paquete no contiene referencias de ensamblado
o archivos de contenido compatibles con dicho marco.
```
**Debe decir**, como recuadro **Error común** antes de los comandos:
> **Error común:** antes de escribir los comandos, en la parte de arriba de la consola cambia la lista **«Proyecto predeterminado»** al proyecto de tu API. Si la solución tiene otros proyectos (Web Forms, WinForms, bibliotecas de clases), la consola instala en el que esté seleccionado, y si es de .NET Framework la instalación falla. Otra forma: agrega `-ProjectName «Proyecto»` al final de cada comando.

---

## 3. Paso 3, sección 5.1: el icono del proyecto

**Dice:** "clic derecho sobre el **proyecto** (el nodo con el ícono de C#, debajo de la solución)".

**Qué pasa:** en Visual Studio 2022, un proyecto **ASP.NET Core Web API** no muestra el ícono de C#. Muestra un **globo azul** (ícono de proyecto web). El ícono de C# aparece en otros tipos de proyecto, como bibliotecas de clases o consola.

**Debe decir:** "clic derecho sobre el **proyecto** de la API: el nodo con el nombre de tu proyecto, debajo de la solución. En un proyecto ASP.NET Core Web API su ícono es un **globo azul** (proyecto web)."

---

## 4. Paso 3: los `using` de los modelos (secciones 5.3 y 5.4)

**Qué pasa:** al crear la clase (Agregar → Clase…), Visual Studio genera solo el `namespace` y la clase vacía. **No agrega** los `using` de las anotaciones. En cuanto escribes `[Key]`, `[Table]`, etc., aparecen subrayados en rojo hasta que importas el `using`. Hoy el manual los muestra dentro de la plantilla (5.4), pero no dice que hay que escribirlos ni para qué sirven, y la explicación aparece al final de la tabla de anotaciones, después de los tipos de dato.

**Debe decir:** agregar una sección nueva **antes** de "Qué lleva cada clase" (antes de escribir propiedades):

> ### Antes de escribir las propiedades: los `using`
> Al crear la clase, Visual Studio **no** agrega estos `using`. Escríbelos tú, arriba del `namespace`:
> ```csharp
> using System.ComponentModel.DataAnnotations;         // [Key], [Required], [StringLength]
> using System.ComponentModel.DataAnnotations.Schema;  // [Table], [Column], [ForeignKey], [DatabaseGenerated]
> ```
> | `using` | Para qué | ¿Cuándo hace falta? |
> |---|---|---|
> | `System.ComponentModel.DataAnnotations` | Anotaciones de **validación y llave**: `[Key]`, `[Required]`, `[StringLength]`, `[Range]`. | Casi siempre (todas las tablas llevan `[Key]` si la llave no se llama `Id`). |
> | `System.ComponentModel.DataAnnotations.Schema` | Anotaciones de **mapeo a la BD**: `[Table]`, `[Column]`, `[ForeignKey]`, `[DatabaseGenerated]`. | Cuando usas alguna de esas anotaciones. |
>
> **Atajo:** si escribes `[Key]` sin el `using`, aparece subrayado en rojo. Pon el cursor encima y presiona **Ctrl+.** (control + punto). Visual Studio ofrece agregar el `using` correcto. Los tipos básicos (`DateTime`, `List<>`) no necesitan `using`, porque la plantilla trae *ImplicitUsings* activado.

---

## 5. Paso 3, sección 5.5: relaciones (llaves foráneas), falta claridad

**Qué pasa:** la sección pasa directo al código con propiedades de navegación en los dos sentidos (hija → padre y padre → lista de hijas), sin explicar:
- que las propiedades de navegación son **opcionales**;
- que en C# no importa el orden en que se crean las clases;
- que con solo la columna FK (`int`) la API ya funciona.

**Debe decir:** agregar antes del código:
> - Para guardar y leer registros **basta con la columna FK** como `int` (por ejemplo, `public int IdChofer { get; set; }`). La base de datos ya valida la llave foránea; EF no necesita más.
> - Las **propiedades de navegación** (`Chofer`, `List<Rutas>`) son **opcionales**. Sirven para traer los datos del registro relacionado en la misma consulta con `.Include(...)`.
> - **No pongas `[Key]` en una llave foránea.** `[Key]` es solo para la llave primaria de la tabla.
> - En C# el orden de las clases no importa: una clase puede usar otra que creas después. El subrayado rojo desaparece cuando las dos existen.
> - Recomendación para empezar: navegación **solo en la tabla hija** (o ninguna). Con navegación en los dos sentidos aparece el error de ciclo JSON (ver el Error común de esta sección).

---

## 6. Paso 5, sección 7.2: la cadena de conexión usa `=` (no `:`)

**Qué pasa:** en `appsettings.json` hay dos formatos mezclados, y es fácil confundirlos:
- El **JSON** separa nombre y valor con **dos puntos**: `"conexion": "..."`.
- **Dentro** de la cadena de conexión, cada opción se escribe `Clave=Valor`, con **signo igual**, y se separa con **punto y coma**.

Escribir `Server:Angelgar;Database:GenOctCCR;...` compila, pero la API falla al conectarse con:
```
Format of the initialization string does not conform to specification starting at index 0.
```

**Debe decir**, como recuadro **Error común** en 7.2:
> **Error común:** dentro de la cadena de conexión se usa `=`, no `:`. El `:` solo separa el nombre de la conexión de su valor en el JSON.
> ```json
> "ConnectionStrings": {
>   "«NombreConexion»": "Server=«Servidor»;Database=«BD»;Integrated Security=True;TrustServerCertificate=True;"
> }
> ```
> | Parte | Separador |
> |---|---|
> | `"«NombreConexion»"` → su valor | `:` (es JSON) |
> | `Server` → `«Servidor»` (dentro de la cadena) | `=` |
> | Entre opciones (`Server=…;Database=…`) | `;` |

---

## 7. Paso 6, sección 8.2: el nombre de la conexión va entre comillas

**Qué pasa:** en la plantilla aparece `GetConnectionString("«NombreConexion»")`. Al reemplazar el marcador es fácil quitar también las comillas:
```csharp
options.UseSqlServer(builder.Configuration.GetConnectionString(conexion)); // ❌ subrayado rojo
```
Sin comillas, C# cree que `conexion` es una variable, y da el error **CS0103: El nombre 'conexion' no existe en el contexto actual**.

**Debe decir**, como recuadro **Error común** en 8.2:
> **Error común:** el nombre de la conexión es un **texto**, así que va entre comillas dobles y se escribe igual que en appsettings.json (respetando mayúsculas):
> ```csharp
> options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")); // ✅
> ```
> Al reemplazar un marcador «…» se quitan solo las comillas angulares « », **no** las comillas dobles `" "` que lo rodean.

También aclarar en 1.2 (Cómo leer este manual): *"Reemplázalos incluyendo las comillas angulares « ». Si el marcador está dentro de comillas dobles (`"«NombreConexion»"`), las comillas dobles se quedan."*

---

## 8. Paso 6, sección 8.3: falta definir qué es CORS antes de configurarlo

**Qué pasa:** la 8.3 empieza con "El navegador bloquea las peticiones…", pero no explica qué es un **origen**, quién bloquea ni por qué. El glosario (1.4) tiene una sola línea.

**Debe decir**, al inicio de 8.3, antes del código:
> **¿Qué es CORS?** *Cross-Origin Resource Sharing* (intercambio de recursos entre orígenes) es una regla de seguridad **de los navegadores**.
>
> - Un **origen** es la combinación **protocolo + dominio + puerto**. `http://localhost:5173` y `http://localhost:5290` son orígenes **distintos**, porque cambia el puerto, aunque los dos estén en tu computadora.
> - Cuando una página web (por ejemplo React en `http://localhost:5173`) pide datos con `fetch` a una API que está en **otro** origen (`http://localhost:5290`), el navegador primero pregunta a la API si permite a ese origen. Si la API no responde que sí, el navegador **bloquea la respuesta** y en la consola aparece `blocked by CORS policy`.
> - **CORS no lo aplica la API, lo aplica el navegador.** La API solo dice qué orígenes acepta. Por eso Swagger, Postman, una app móvil, WinForms o un servidor (por ejemplo, Web Forms usando `HttpClient` desde el code-behind) **no necesitan CORS**: no son páginas ejecutándose en un navegador.
> - Se configura en **dos lugares** de Program.cs, con el **mismo nombre de política**:
>
> | Dónde | Código | Para qué |
> |---|---|---|
> | Zona 1 (servicios), antes de `builder.Build()` | `builder.Services.AddCors(...)` + `AddPolicy("«Politica»", ...)` | **Define** la política: qué orígenes, encabezados y métodos se permiten. |
> | Zona 2 (pipeline), después de `builder.Build()` | `app.UseCors("«Politica»");` | **Aplica** la política a cada petición. Va antes de `UseAuthorization` y `MapControllers`. |
>
> | Método de la política | Qué permite |
> |---|---|
> | `WithOrigins("http://localhost:«PuertoFrontend»")` | Solo esos orígenes, escritos exactos y sin `/` final. |
> | `AllowAnyHeader()` | Cualquier encabezado (`Content-Type`, `Authorization`…). |
> | `AllowAnyMethod()` | Cualquier verbo HTTP (GET, POST, PUT, DELETE). Sin esto, GET funciona y PUT o DELETE se bloquean. |

---

## 9. Paso 7, sección 9.3: no dice dónde va el código

**Qué pasa:** la 9.3 muestra el campo `private readonly` y el constructor sueltos, sin decir en qué parte del archivo se escriben. Quien no lo sabe puede pegarlos fuera de la clase o arriba del `namespace`.

**Debe decir:** mostrar el controlador generado en 9.2 con el lugar marcado:
> El campo y el constructor van **dentro de la clase**, justo después de la llave `{` que abre `public class «Tabla»Controller : ControllerBase`, y **antes** de los métodos GET/POST/PUT/DELETE de la 9.4:
> ```csharp
> using Microsoft.AspNetCore.Mvc;
> using «Proyecto».Context;                      // ← agregar: para AppDbContext
>
> namespace «Proyecto».Controllers
> {
>     [Route("api/[controller]")]
>     [ApiController]
>     public class «Tabla»Controller : ControllerBase
>     {
>         // ▼▼▼ AQUÍ: campo + constructor (9.3) ▼▼▼
>         private readonly AppDbContext _context;
>
>         public «Tabla»Controller(AppDbContext context)
>         {
>             _context = context;
>         }
>         // ▲▲▲ fin de 9.3 ▲▲▲
>
>         // ▼▼▼ AQUÍ: los 5 métodos CRUD (9.4) ▼▼▼
>     }
> }
> ```
> - El nombre del constructor debe ser **idéntico** al de la clase (`«Tabla»Controller`), y no lleva tipo de retorno.
> - El `using «Proyecto».Context;` se agrega a mano (o con **Ctrl+.** sobre `AppDbContext` subrayado en rojo).

---

## 10. Paso 7, sección 9.4: errores comunes al copiar la plantilla CRUD

Agregar al final de la 9.4 un recuadro **Error común** con estos tres casos:

### a) Tipo de retorno de `GetPorId`
`GetTodos` devuelve una **lista** y `GetPorId` devuelve **un solo registro**. Si copias la firma de `GetTodos` en `GetPorId`, `return entidad;` marca el error **CS0029** ("No se puede convertir implícitamente el tipo «Tabla» en ActionResult<IEnumerable<«Tabla»>>"):
```csharp
public async Task<ActionResult<IEnumerable<«Tabla»>>> GetTodos()   // lista
public async Task<ActionResult<«Tabla»>> GetPorId(int id)          // uno solo (sin IEnumerable)
```

### b) El nombre después de `_context.` es el del DbSet
`_context.«Tabla»` usa **exactamente** el nombre de la propiedad `DbSet` de AppDbContext, con las mismas mayúsculas. Si en AppDbContext escribiste `DbSet<Rutas> rutas`, en el controlador es `_context.rutas`. Recomendación: nombrar el DbSet **igual que la tabla y la clase** (`DbSet<Rutas> Rutas`) para no confundirse.

### c) `using` que Visual Studio agrega solo por error
Al aceptar una sugerencia de IntelliSense con Tab o Enter, Visual Studio a veces agrega `using` que no tienen nada que ver con la API, por ejemplo:
```csharp
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using Microsoft.Identity.Client;
```
O repite uno que ya estaba (`using «Proyecto».Context;` dos veces). Bórralos: aparecen en gris (sin usar). **Clic derecho → Quitar y ordenar usings** (Ctrl+R, Ctrl+G) limpia todos los que sobran.

---

## 11. Paso 7, sección 9.5: resumir el patrón para recordarlo

Agregar después de la tabla de rutas:
> **Por cada tabla, el controlador tiene siempre 5 métodos:** 2 GET (todos y por id), 1 POST, 1 PUT y 1 DELETE. Cambian solo tres palabras: «Tabla», «ClavePrimaria» y el nombre del DbSet.
>
> Los que reciben un `id` siguen el mismo patrón de 4 pasos:
> 1. **Buscar:** `FindAsync(id)`
> 2. **Si no existe:** `return NotFound();` (404)
> 3. **Hacer el cambio:** `Add` / `SetValues` / `Remove`, y después `SaveChangesAsync()`
> 4. **Responder:** registro (200), `CreatedAtAction` (201) o `NoContent()` (204)
