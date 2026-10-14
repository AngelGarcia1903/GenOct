USE GenOct
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20261001
Descripción: Creacion de Trigger
Modificación: 20261012 - Se conservan los 6 triggers; cada uno tiene una sola tarea para no auditar dos veces:
  INSTEAD OF  -> validan y ejecutan la operación real sobre Productos.
  AFTER       -> registran la auditoría con el ProductoID real.

  Operación  | INSTEAD OF (valida y ejecuta)          | AFTER (audita)
  INSERT     | TR_Auditoria_Instead_Insert            | TR_Auditoria_After_Insert
  UPDATE     | TR_Auditoria_Instead_Update            | TR_AuditarCambioPrecio
  DELETE     | TR_Auditoria_Instead_Delete            | TR_Auditoria_After_Delete

  Correcciones:
  - Instead_Insert auditaba con ProductoID vacío (el ID aún no existe en "inserted"); ahora valida e inserta
    todas las columnas, y guarda el ID creado en SESSION_CONTEXT('UltimoProductoID') para que la API lo lea.
  - Instead_Update solo actualizaba Nombre y Precio (se perdían Categoria y Stock); ahora actualiza todo.
    Audita las ediciones sin cambio de precio; las de precio las audita TR_AuditarCambioPrecio.
  - Instead_Delete ahora rechaza con un mensaje claro el borrado de productos que tienen pedidos.
  - Las consultas de práctica del final quedan comentadas (se ejecutaban cada vez que corría el script).
*/

--1 AFTER UPDATE: audita los cambios de precio
CREATE OR ALTER TRIGGER TR_AuditarCambioPrecio
ON Productos --nombre de tabla a auditar
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	IF UPDATE(Precio)
	BEGIN

	INSERT INTO AuditoriaProductos
	(ProductoID, Accion, PrecioAnterior, PrecioNuevo, Usuario)
	SELECT d.ProductoID, 'UPDATE', d.Precio, i.Precio, SYSTEM_USER

		FROM deleted AS d --te permite acceder a una tabla temporal para poder acceder al precio antes de ser modificado
		INNER JOIN inserted AS i
		ON
		d.ProductoID = i.ProductoID
		WHERE d.Precio <> i.Precio --solo cuando el precio realmente cambió
	END
END
GO


--2 AFTER INSERT: audita cada producto nuevo (aquí ya existe el ProductoID real)
CREATE OR ALTER TRIGGER TR_Auditoria_After_Insert
ON Productos
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
        INSERT INTO AuditoriaProductos
        (ProductoID, Accion, PrecioAnterior, PrecioNuevo, Usuario)
        SELECT
            i.ProductoID,
            'INSERT',
            NULL,
            i.Precio,
            SYSTEM_USER
        FROM inserted AS i;

END;
GO

--3 INSTEAD OF INSERT: valida e inserta (la auditoría la hace TR_Auditoria_After_Insert)
CREATE OR ALTER TRIGGER TR_Auditoria_Instead_Insert
ON Productos
INSTEAD OF INSERT
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM inserted
	           WHERE Precio <= 0
	              OR ISNULL(Stock, 0) < 0
	              OR LTRIM(RTRIM(Nombre)) = ''
	              OR LTRIM(RTRIM(Categoria)) = '')
		THROW 50001, N'Producto inválido: nombre y categoría son obligatorios, el precio debe ser mayor a 0 y el stock no puede ser negativo.', 1;

    INSERT INTO Productos (Nombre, Categoria, Precio, Stock, FechaCreacion)
    SELECT LTRIM(RTRIM(Nombre)), LTRIM(RTRIM(Categoria)), Precio, ISNULL(Stock, 0), ISNULL(FechaCreacion, GETDATE())
    FROM inserted;

	-- Fuera del trigger SCOPE_IDENTITY() regresa NULL, por eso se deja el ID en el contexto de la sesión
	DECLARE @NuevoID INT = SCOPE_IDENTITY();
	EXEC sp_set_session_context @key = N'UltimoProductoID', @value = @NuevoID;
END
GO

--4 INSTEAD OF UPDATE: valida y actualiza todas las columnas
CREATE OR ALTER TRIGGER TR_Auditoria_Instead_Update
ON Productos
INSTEAD OF UPDATE
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM inserted
	           WHERE Precio <= 0
	              OR ISNULL(Stock, 0) < 0
	              OR LTRIM(RTRIM(Nombre)) = ''
	              OR LTRIM(RTRIM(Categoria)) = '')
		THROW 50001, N'Producto inválido: nombre y categoría son obligatorios, el precio debe ser mayor a 0 y el stock no puede ser negativo.', 1;

	-- Ediciones sin cambio de precio (los cambios de precio los audita TR_AuditarCambioPrecio)
    INSERT INTO AuditoriaProductos
    (ProductoID, Accion, PrecioAnterior, PrecioNuevo, Usuario)
    SELECT
    d.ProductoID,
    'UPDATE',
    d.Precio,
    i.Precio,
    SYSTEM_USER
    FROM deleted AS d
    INNER JOIN inserted AS i
    ON
    i.ProductoID = d.ProductoID
    WHERE d.Precio = i.Precio

    UPDATE p
    SET
      p.Nombre = LTRIM(RTRIM(i.Nombre)),
      p.Categoria = LTRIM(RTRIM(i.Categoria)),
      p.Precio = i.Precio,
      p.Stock = i.Stock,
      p.FechaCreacion = i.FechaCreacion

    FROM Productos AS p
    INNER JOIN inserted i
    ON
    p.ProductoID = i.ProductoID
END;
GO

--5 AFTER DELETE: audita cada producto borrado
CREATE OR ALTER TRIGGER TR_Auditoria_After_Delete
ON Productos
AFTER DELETE
AS
BEGIN
	SET NOCOUNT ON;
    INSERT INTO AuditoriaProductos
    (ProductoID, Accion, PrecioAnterior, PrecioNuevo, Usuario)
    SELECT
    d.ProductoID,
    'DELETE',
    d.Precio,
    NULL,
    SYSTEM_USER
    FROM deleted AS d
END
GO

--6 INSTEAD OF DELETE: valida y borra (la auditoría la hace TR_Auditoria_After_Delete)
CREATE OR ALTER TRIGGER TR_Auditoria_Instead_Delete
ON Productos
INSTEAD OF DELETE
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM DetallesPedido AS dp INNER JOIN deleted AS d ON dp.ProductoID = d.ProductoID)
		THROW 50002, N'No se puede eliminar el producto porque tiene pedidos registrados.', 1;

    DELETE p
    FROM Productos AS p
    INNER JOIN deleted AS d
    ON p.ProductoID = d.ProductoID
END
GO

/* ---------------------------------------------------------------------------------
   CONSULTAS DE PRÁCTICA (comentadas para que no se ejecuten junto con los triggers)
------------------------------------------------------------------------------------
--CTE
WITH Productos AS (
SELECT
p.ClienteID,
p.Total,
p.Estado
FROM Pedidos AS p
INNER JOIN DetallesPedido AS dp
ON
p.PedidoID=dp.PedidoID
)
SELECT * FROM Productos AS pp
WHERE pp.ClienteID=1

--SP_HELP 'Productos'

-- Generar 5,000,000 de registros (prueba de rendimiento: NO ejecutar en la BD que usa la API)
--;WITH Numeros AS
--(
--    SELECT TOP (5000000)
--        ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS N
--    FROM sys.all_columns AS a
--    CROSS JOIN sys.all_columns AS b
--)
--INSERT INTO Productos (Nombre, Categoria, Precio, Stock, FechaCreacion)
--SELECT
--    CONCAT('Producto ', N),
--    CONCAT('Categoria ', (N % 20) + 1),
--    CAST(((N % 10000) + 100) / 10.0 AS DECIMAL(10,2)),
--    N % 1000,
--    DATEADD(DAY, -(N % 3650), GETDATE())
--FROM Numeros;

--SELECT COUNT(*) AS TotalRegistros FROM Productos;

--SELECT COMPUESTO
SET STATISTICS TIME ON;
SET STATISTICS IO ON;

SELECT top 10
    ProductoID,
    Nombre,
    Categoria,
    Precio,
    Stock
FROM Productos
WHERE Categoria = 'Categoria 10'
AND Stock > 500;

--CTE
WITH ProductosCTE AS
(
    SELECT top 10
        ProductoID,
        Nombre,
        Categoria,
        Precio,
        Stock
    FROM Productos
)
SELECT *
FROM ProductosCTE
WHERE Categoria = 'Categoria 10'
AND Stock > 500;

--tabla temporal
DECLARE
@tbltemp TABLE(
ID INT,
NOMBRE VARCHAR,
TELEFONO NUMERIC(10)
);
INSERT INTO @tbltemp VALUES(1, 'J', 124);
SELECT*FROM @tbltemp;
--------------------------------------------------------------------------------- */
