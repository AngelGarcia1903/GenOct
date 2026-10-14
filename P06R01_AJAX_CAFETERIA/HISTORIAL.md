# Historial — P06R01 AJAX Cafetería

## v1.0 · 12-oct-2026 — BD corregida, API creada y frontend conectado con jQuery AJAX

### Cómo ejecutarlo
1. **Base de datos:** ejecuta `SCRIPTS\run_all_scripts.bat`, que corre los scripts en orden: 01 → 02 → 03 → 04 → 05 → 07 → 08.
   - Si el servidor no es `Angelgar`, cambia `SERVER` en el `.bat`.
   - Para revertir todo usa `CONTINGENCIA\run_all_scripts.bat`, que corre de A a G y borra la BD `GenOct`.
2. **API:** abre `SISTEMA\BACKEND\P06R01_CAFETERIA\P06R01_CAFETERIA.sln` en Visual Studio, elige el perfil **http** y presiona F5.
   - Queda en `http://localhost:5220` y abre Swagger.
   - Si el servidor SQL es otro, cambia `ConnectionStrings:GenOct` en `appsettings.json`.
3. **Frontend:** abre `SISTEMA\FRONTEND\index.html`. Funciona con doble clic o con Live Server de VS Code en el puerto 5500.
   - Si la API usa otro puerto, cámbialo en `js/api.js` (`URL_API`).

### Arquitectura

```
Frontend (HTML + jQuery $.ajax)  ──HTTP/JSON──►  API ASP.NET Core 8 (EF Core)  ──►  SQL Server: GenOct
  index / menu / especiales / pedido              Controllers → Services → DbContext     tablas, vista, SP, triggers
```

| Página | Petición AJAX (`js/api.js`) | Endpoint | Objeto de la BD |
|---|---|---|---|
| `index.html`: "Los más pedidos" | `CafeApi.populares(3)` | `GET /api/productos/populares?top=3` | Productos + DetallesPedido |
| `menu.html`: filtros | `CafeApi.categorias()` | `GET /api/productos/categorias` | Productos |
| `menu.html`: carta | `CafeApi.productos(categoria)` | `GET /api/productos?categoria=` | Productos |
| `today-special.html` | `CafeApi.productos('Panaderia')` | `GET /api/productos?categoria=Panaderia` | Productos |
| `pedido.html`: carrito | `CafeApi.productos()` | `GET /api/productos` | Productos (precio y stock actuales) |
| `pedido.html`: confirmar | `CafeApi.crearPedido(datos)` | `POST /api/pedidos` | Clientes, `spInsertarPedido`, `spInsertarDetallePedido`, UPDATE de stock (triggers) |
| `pedido.html`: consultar | `CafeApi.pedido(id)` | `GET /api/pedidos/{id}` | Pedidos + DetallesPedido + Clientes |

Hay más endpoints, que se pueden probar en Swagger o en `P06R01_CAFETERIA.http`:
- `GET/POST/PUT/DELETE /api/productos`: CRUD que pasa por los 6 triggers.
- `GET /api/clientes` y `GET /api/clientes/{id}` (este último usa `spObtenerClienteId`).
- `GET /api/pedidos`.
- `GET /api/reportes/resumen-ventas`, que lee la vista `VWVistaResumenVentas`.
- `GET /api/auditoria`, que lee `AuditoriaProductos`.

### Base de datos: scripts (`SCRIPTS\`)
| Script | Cambio |
|---|---|
| 02 CreateTables | Se quitó el `ALTER TABLE` que creaba una segunda llave foránea Pedidos → Clientes, duplicada. |
| 03 InitialCharge | Ahora carga cada tabla solo si está vacía; antes, ejecutarlo varias veces duplicó todo. Usa `N''` para conservar acentos y la ñ ("Toño" se guardaba como "To�o"). Los teléfonos van como texto. Se agregaron 4 productos de Panadería. Los detalles ya apuntan a pedidos que existen: el `PedidoID 6` hacía fallar el script. Cada `Total` es la suma real de sus detalles. |
| 05 Stored_Procedure | En `spInsertarDetallePedido`, `@PrecioUnitario` pasó de INT a DECIMAL(10,2), porque 20.50 se guardaba como 21. `spConsultarContenidoTablas` consultaba Pedidos dos veces. Los EXEC de prueba quedaron comentados, porque insertaban un pedido en cada ejecución. |
| 07 Trigger | Se conservan los 6 triggers. Los **INSTEAD OF** validan y ejecutan la operación, y los **AFTER** auditan, así cada cambio se audita una sola vez. Instead_Insert registraba un ProductoID vacío; ahora guarda el ID nuevo en `SESSION_CONTEXT('UltimoProductoID')`. Instead_Update solo guardaba Nombre y Precio; ahora guarda todas las columnas. Instead_Delete rechaza con un mensaje claro el borrado de productos que tienen pedidos. Las consultas de práctica, incluido el generador de 5 millones de filas, quedaron comentadas. |
| 08 Jobs | Usa `@server_name = '(local)'` en lugar de `'Angelgar'`. Se corrigieron los comentarios: `freq_subday_type = 4` son minutos. |
| run_all_scripts.bat | Se corrigieron las etiquetas. |
| `README (1).md` | Borrado: era el perfil de GitHub de otra persona. |

Todos los `.sql` se guardaron en UTF-8 con BOM para que `sqlcmd` lea bien los acentos.

### Base de datos: contingencias (`CONTINGENCIA\`)
| Script | Cambio |
|---|---|
| A Jobs | Se agregó el `GO` final. |
| B Triggers | Usaba `USE msdb`, así que nunca encontraba los triggers; ahora usa `USE GenOct`. |
| E InitialCharge | Antes solo borraba si las 4 tablas tenían datos; ahora limpia cada tabla, incluida AuditoriaProductos, y reinicia los IDENTITY para que la carga inicial vuelva a empezar en 1. |
| F Tablas | Antes solo borraba si existían las 5 tablas; ahora borra las que existan, empezando por las hijas. |
| run_all_scripts.bat | Llamaba a `..._StoredProcedures.sql`, un archivo que no existe. Ahora valida antes de B–F que la BD exista, y si no existe solo revierte el job. |

**Datos:** con autorización se borró y se volvió a crear `GenOct`. Tenía 10,000,081 productos (10 millones generados en la prueba de rendimiento) y los datos reales repetidos 8 veces. Quedó con 14 productos, 5 clientes, 5 pedidos y 7 detalles.

### Backend (`SISTEMA\BACKEND\P06R01_CAFETERIA`)
- Se borró la plantilla WeatherForecast.
- Se agregó `Microsoft.EntityFrameworkCore.SqlServer 8.0.31`.
- **Archivos nuevos:**
  - `Models/`: Producto, Cliente, Pedido, DetallePedido, AuditoriaProducto y ResumenVenta (la vista).
  - `Data/CafeteriaContext.cs`: declara los 6 triggers con `HasTrigger`, para que EF no use `OUTPUT`.
  - `Dtos/`, con validaciones en español.
  - `Services/ProductoService.cs`: el INSERT lee el ID desde `SESSION_CONTEXT`.
  - `Services/PedidoService.cs`: crea el pedido en una sola transacción y bloquea el stock con `UPDLOCK`.
  - `ErroresSql.cs`: convierte el THROW 50001 en 400 y el 50002 en 409.
  - `ReglaNegocioFilter.cs`: devuelve los errores en formato ProblemDetails.
  - `Controllers/`: Productos, Pedidos, Clientes y Reportes.
- `Program.cs`:
  - DbContext con la cadena `GenOct`.
  - CORS "Frontend" para `localhost:5500`, `127.0.0.1:5500` y `null`, que es el origen de los HTML abiertos con doble clic.
  - Se quitó `UseHttpsRedirection`, porque redirigir a https rompe el AJAX.
- `P06R01_CAFETERIA.csproj.user`: el perfil activo pasa a **http**.

### Frontend (`SISTEMA\FRONTEND`)
- **Nuevo `js/api.js`:** concentra todas las peticiones `$.ajax`.
- **Nuevo `js/cafeteria.js`:** pinta tarjetas, filtros, carrito, confirmación y consulta. El carrito guarda en `localStorage` solo IDs y cantidades; los precios siempre se piden a la API.
- **Nuevo `pedido.html`:** reemplaza a `contact.html`, que generaba un `.txt` sin enviar nada y no tenía tabla en la BD.
- **Páginas actualizadas:**
  - `index.html`: favoritos desde la API.
  - `menu.html`: carta y filtros desde la API.
  - `today-special.html`: Panadería desde la API.
  - En el menú, "Hablemos" pasa a ser "Tu pedido", con un contador.
- **Información de más que se quitó:**
  - Los 3 cafés escritos a mano, con descripciones que no existen en la BD.
  - El formulario de descarga.
  - Las tarjetas de "combinaciones" sin datos.
  - Las fotos de paisajes que se usaban como fotos de café. Ahora hay una foto por categoría: `special-1.jpg` para Bebidas y `special-2.jpg` para Panadería.
- **Archivos borrados:**
  - `contact.html`, `build_pages.py`, `checks.cjs`, `__pycache__` y `JS.code-workspace` (este apuntaba a otras carpetas).
  - `fonts/`, Bootstrap, Font Awesome, `templatemo-style.css` y `templatemo-script.js`.
  - 20 imágenes que nadie usaba.
- `css/cafe-house.css`: estilos nuevos para precio, stock, carrito, alertas y consulta de pedido.

### Verificación realizada
- Los dos `.bat` (contingencia y creación) se ejecutaron completos y sin errores.
- **Triggers:** se probaron en una transacción revertida.
  - Cada operación se audita una vez.
  - Se recupera el ID del producto nuevo.
  - Los cambios de Stock y Categoría se guardan.
  - El borrado protegido da el error 50002.
- **API:**
  - Compila con 0 advertencias.
  - Todos los GET responden.
  - El CRUD de productos devuelve 201, 200, 200, 204 y 409.
  - Pedido: devuelve 201; el total se calcula en el servidor, las líneas repetidas se suman, el cliente se reutiliza por correo y el stock baja.
  - Errores: sin stock 409, producto inexistente 404, datos inválidos 400 con un mensaje por campo.
  - CORS: `null` y `127.0.0.1:5500` permitidos, otros orígenes rechazados.
  - Swagger responde 200.
- **Navegador:**
  - Favoritos, carta (14 productos) y filtro de Panadería (4).
  - Agregar al pedido y contador.
  - Carrito con precios de la API.
  - Confirmar pedido: se registró el #8.
  - Consultar el #8 y el #999 (no existe).
  - Mensaje con "Reintentar" cuando la API está apagada.
  - Sin errores en la consola.
- Al terminar se restauraron los datos con la contingencia E y la carga 03. AuditoriaProductos quedó con 14 registros INSERT, porque la recarga pasa por los triggers.

### Pendientes y avisos
- **jQuery 1.11.2:** es la versión que traía la plantilla. Funciona para `$.ajax`, pero es antigua; se recomienda actualizarla a jQuery 3.7.
- **Job:** no corre mientras el servicio "Agente SQL Server" esté detenido.
- **Consulta de pedido:** cualquiera que conozca el número puede consultar un pedido. Por eso la página no muestra el correo ni el teléfono del cliente.
