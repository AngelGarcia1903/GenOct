# Correcciones a `P07R01_StoredProcedure.sql`

Fecha: 2026-10-07

## Cambios generales (todos los SP)

| Cambio | Motivo |
|---|---|
| `ROLLBACK TRANSACTION;` + `THROW;` con punto y coma | Sin `;`, SQL Server lee `ROLLBACK TRANSACTION THROW` como un rollback de una transacción llamada "THROW", así que **el error nunca se relanzaba**. Además, `THROW` exige que la instrucción anterior termine en `;`. |
| `;` al final de las instrucciones | Buena práctica y obligatorio antes de `THROW`. |
| Prefijo `dbo.` en todas las tablas | Algunos SP usaban `FROM Choferes` / `FROM Rutas` sin esquema. |
| Indentación uniforme | Legibilidad. |

## Encabezado del script

| Antes | Después | Motivo |
|---|---|---|
| `SP_Help 'Camiones'` sin `GO` | `EXEC SP_Help 'Camiones';` + `GO` | `CREATE PROCEDURE` debe ser la primera instrucción del lote; sin `GO` falla. |
| `C REATE OR ALTER PROCEDURE dbo.Insert_Camion` | `CREATE OR ALTER ...` | Había un espacio dentro de `CREATE` (solo en el archivo). |

## Camiones

### `dbo.Insert_Camion`
- Se quitó la **coma sobrante** después de `UrlFoto,` en la lista de columnas del `INSERT` (error de sintaxis).
- `@kilometraje` → `@Kilometraje` (consistencia con el parámetro).
- Se agregó `;` al `SELECT` del mensaje.

### `dbo.Select_Camion`
- Solo `;` y formato. Sin cambios de lógica.

### `dbo.Update_Camion`
- Solo `;` en `ROLLBACK`/`THROW` (ver cambios generales).

### `dbo.Delete_Camion`
- **Faltaba `BEGIN TRY` y `BEGIN TRANSACTION`**: tenía `COMMIT`, `END TRY` y `BEGIN CATCH` sin su apertura → no compilaba.
- `'Camion Eliminado Correctamente', AS Mensaje` → se quitó la **coma antes de `AS`**.

## Choferes

### `dbo.Insert_Choferes`
- `IF NOT EXISTS` → `IF EXISTS`: la lógica estaba invertida (lanzaba "ya existe" cuando **no** existía, impidiendo cualquier inserción).
- Columna `ApPaterno` **duplicada** en el `INSERT` → la segunda se cambió a `ApMaterno`.
- `'Chofer insertado correctamente', AS Mensaje` → se quitó la coma antes de `AS`.

### `dbo.Select_Chofer`
- `FROM Choferes` → `FROM dbo.Choferes`.

### `dbo.Update_Choferes`
- `ApPaterno = @ApMaterno` → `ApPaterno = @ApPaterno` (se guardaba el apellido materno en el paterno).
- `@Telefono VARCHAR(100)` → `VARCHAR(15)` para coincidir con `Insert_Choferes`.
- Coma antes de `AS Mensaje` eliminada.

### `dbo.Delete_Chofer`
- Coma antes de `AS Mensaje` eliminada.
- `;` faltantes (`SET NOCOUNT ON`, `THROW 50003 ...`).

## Rutas

### `dbo.Insert_Rutas`
- Coma antes de `AS Mensaje` eliminada; mensaje cambiado a singular: `'Ruta insertada correctamente'`.
- `;` faltantes (`SET NOCOUNT ON`, `THROW 50001 ...`, `COMMIT`).

### `dbo.Select_Rutas`
- **Faltaba la coma** entre `Distancia` y `FechaRegistro`: SQL lo interpretaba como `Distancia AS FechaRegistro` (no daba error, pero devolvía la columna equivocada y no traía `FechaRegistro`).
- `FROM Rutas` → `FROM dbo.Rutas`.

### `dbo.Update_Rutas` (el más roto)
- **Faltaba `BEGIN TRANSACTION`** (había `COMMIT` sin transacción abierta).
- Faltaba la coma después de `FechaLlegada = @FechaLlegada`.
- No actualizaba `Distancia` → se agregó `Distancia = @Distancia`.
- El `THROW 50002` estaba en un `BEGIN...END` **sin `IF`**, así que **siempre** lanzaba error → se agregó `IF @@ROWCOUNT = 0`.
- Mensajes copiados de camión → `'No se encontro ninguna ruta con ese ID'` y `'Ruta actualizada correctamente'`.

### `dbo.Delete_Ruta`
- `WHERE IRutas = @IdRuta` → `WHERE IdRuta = @IdRuta` (columna inexistente).

### `dbo.Select_Rutas_Detalle`
- `c,Telefono` → `c.Telefono` (había coma en lugar de punto).
- `cam.UrlFoto` → `cam.UrlFoto AS FotoCamion` (alias para distinguirla de la foto del chofer).

## Validaciones / consultas

### `dbo.Existe_Licencia`
- Se quitó la **coma sobrante** después de `@Licencia VARCHAR(50),` en los parámetros.

### `dbo.Obtener_Camion_ID`
- **Faltaba el `END`** del bloque `IF @@ROWCOUNT = 0 BEGIN ...` → no compilaba.
- Se agregó la columna `Marca` al `SELECT` (estaba omitida; `Listar_Camiones` sí la devuelve).

### `dbo.Existe_Matricula`
- Solo `;` y formato.

### `dbo.Listar_Cambios` → `dbo.Listar_Camiones`
- **Renombrado**: el nombre tenía un error de dedo ("Cambios"). Si ya existe `Listar_Cambios` en la BD o lo llama algún backend, elimínalo con `DROP PROCEDURE dbo.Listar_Cambios;` o actualiza la llamada.

## Pendiente de revisar (no se cambió)
- `Insert_Rutas` impide registrar **más de una ruta** con el mismo par chofer + camión, incluso en fechas distintas. Si la intención es evitar rutas simultáneas, convendría validar también por fechas.
- Los SP suponen que `Choferes` y `Rutas` tienen la columna `FechaRegistro`. Verifícalo con `SP_Help`.
