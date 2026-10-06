USE GenOct
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20261001
Descripción: Creacion de Trigger
*/

CREATE OR ALTER TRIGGER TR_AuditarCambioPrecio
ON Productos --nombre de tabla a auditar
--1 AFTER UPDATE
AFTER UPDATE
AS
BEGIN
	IF UPDATE(Precio)
	BEGIN

	INSERT INTO AuditoriaProductos
	(ProductoID, Accion, PrecioAnterior, PrecioNuevo, Usuario)
	SELECT d.ProductoID, 'UPDATE', d.Precio, i.Precio, SYSTEM_USER
		
		FROM deleted AS d --te permite acceder a una tabla temporal para poder acceder al precio antes de ser modificado
		INNER JOIN inserted AS i
		ON
		d.ProductoID = i.ProductoID
	END
END
GO


--SACAR TRIGGER DE LOS OTROS 5 TRIGGER 
--2 AFTER INSERT
CREATE OR ALTER TRIGGER TR_Auditoria_After_Insert
ON Productos
AFTER INSERT
AS
BEGIN
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

--3 INSTEAD OF INSERT
CREATE OR ALTER TRIGGER TR_Auditoria_Instead_Insert
ON Productos
INSTEAD OF INSERT
AS
BEGIN
    INSERT INTO AuditoriaProductos
        (ProductoID, Accion, PrecioAnterior, PrecioNuevo, Usuario)
    SELECT
        ProductoID,
        'INSERT',
        NULL,
        Precio,
        SYSTEM_USER
    FROM inserted AS i;

    INSERT INTO Productos (Nombre, Categoria, Precio, Stock)
    SELECT Nombre, Categoria, Precio, Stock
    FROM inserted;
END
GO
--4 INSTEAD OF UPDATE
CREATE OR ALTER TRIGGER TR_Auditoria_Instead_Update
ON Productos
INSTEAD OF UPDATE
AS
BEGIN
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
    UPDATE p
    SET 
      p.Nombre = i.Nombre,
      p.Precio = i.Precio

    FROM Productos AS p
    INNER JOIN inserted i
    ON
    p.ProductoID = i.ProductoID
END;
GO

--5 AFTER DELETE
CREATE OR ALTER TRIGGER TR_Auditoria_After_Delete
ON Productos
AFTER DELETE
AS
BEGIN
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

--6 INSTEAD OF DELETE
CREATE OR ALTER TRIGGER TR_Auditoria_Instead_Delete
ON Productos
INSTEAD OF DELETE
AS
BEGIN
    
    INSERT INTO AuditoriaProductos
    (ProductoID, Accion, PrecioAnterior, PrecioNuevo, Usuario)
    SELECT
    d.ProductoID,
    'DELETE',
    d.Precio,
    NULL,
    SYSTEM_USER
    FROM deleted AS d
    
    DELETE p
    FROM Productos AS p
    INNER JOIN deleted AS d 
    ON p.ProductoID = d.ProductoID
END
GO

-----------------------------------------
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


------------------------------------------------------------------------------
-- Generar 5,000,000 de registros


--;WITH Numeros AS
--(
--    SELECT TOP (5000000)
--        ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS N
--    FROM sys.all_columns AS a
--    CROSS JOIN sys.all_columns AS b
--)
--INSERT INTO Productos
--(
--    Nombre,
--    Categoria,
--    Precio,
--    Stock,
--    FechaCreación
--)
--SELECT
--    CONCAT('Producto ', N),
--    CONCAT('Categoria ', (N % 20) + 1),
--    CAST(((N % 10000) + 100) / 10.0 AS DECIMAL(10,2)),
--    N % 1000,
--    DATEADD(DAY, -(N % 3650), GETDATE())
--FROM Numeros;

--SELECT COUNT(*) AS TotalRegistros
--FROM Productos;
---------------------------------------------------
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
----------------------------------------------
--CTE
SET STATISTICS TIME ON;
SET STATISTICS IO ON;

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

---------------------------------------------
--SELECT * FROM Productos;
-------------------------------------
DECLARE --tabla temporal
@tbltemp TABLE(
ID INT,
NOMBRE VARCHAR,
TELEFONO NUMERIC(10)
);
INSERT INTO @tbltemp VALUES(1, 'J', 124);
SELECT*FROM @tbltemp;

-------------------------------------------
