USE GenOct
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20260930
Descripción: Creacion consultas generales
*/


USE GenOct
GO

SELECT * FROM [dbo].[Clientes];
SP_HELP 'Clientes'

SELECT * FROM [dbo].[Pedidos];
SP_HELP 'Pedidos'

SELECT * FROM [dbo].[DetallesPedido];
SP_HELP 'DetalleSPedido'

SELECT * FROM [dbo].[Productos];
SP_HELP 'Productos'

SELECT * FROM [dbo].[AuditoriaProductos];
SP_HELP 'AuditoriaProductos'
SELECT TOP 10 * FROM  AuditoriaProductos
SELECT COUNT(*) FROM AuditoriaProductos

--USO DE JOINS
--OBTENER TODOS LOS PEDIDOS CON EL NOMBRE DEL CLIENTE

SELECT 
	c.Nombre AS Clientes,
	p.PedidoID,
	p.Fecha,
	p.Total,
	p.Estado
FROM Clientes AS c
INNER JOIN Pedidos AS p
ON c.ClienteID = p.ClienteID
ORDER BY p.Fecha DESC;
GO
-------------------------------------
SELECT
	 c.Nombre as Cliente,
	 p.PedidoID,
	 p.Fecha,
	 prod.Nombre AS Producto,
	 dp.Cantidad,
	 dp.PrecioUnitario,
	 (dp.Cantidad * dp.PrecioUnitario) AS Subtotal
FROM Clientes AS c
INNER  JOIN Pedidos AS p
ON
c.ClienteID = p.ClienteID
INNER JOIN DetallesPedido AS dp
ON
p.PedidoID = dp.PedidoID
INNER JOIN Productos AS prod
ON
dp.ProductoID = prod.ProductoID
WHERE p.Fecha >= '2024-01-01'
ORDER BY p.Fecha DESC, p.PedidoID;

--------------------------------------------
SELECT * FROM [dbo].[VWVistaResumenVentas]
--------------------------------------------
EXEC [dbo].[spObtenerClienteId]
@Cliente=1

------------------------------------------------------
---1
SELECT
p.PedidoID,
c.Nombre AS cliente,
p.Fecha,
p.Total,
p.Estado
FROM Pedidos AS p
INNER JOIN Clientes AS c
ON c.ClienteID=p.ClienteID
ORDER BY P.PedidoID ASC;
GO

---2
SELECT 
c.ClienteID,
c.Nombre,
c.Email
FROM Clientes AS c
LEFT JOIN Pedidos AS p
ON c.ClienteID = p.ClienteID
WHERE p.PedidoID IS NULL
ORDER BY C.ClienteID ASC
GO
-------------------------------------------------------
--INSERT INTO
--SELECTED * FROM
Productos(Nombre, Categoria, Precio, Stock, FechaCreación) VALUES ('MANTEConcha', 'Panaderia', 20, 50, GETDATE())
GO

SELECT * FROM AuditoriaProductos;
SELECT * FROM Productos;

--UPDATE
--SELECTED * FROM
Productos
SET Nombre = 'MANTEConcha', Categoria = 'Panaderia', Precio=50, Stock=50
WHERE ProductoID = 4
GO


--DELETE FROM
--SELECT * FROM
DetallesPedido
WHERE ProductoID = 4
GO

--DELETE FROM
--SELECT * FROM
Productos 
WHERE ProductoID = 4
GO

SELECT TOP 5 * FROM DetallesPedido;
SELECT TOP 5 * FROM Pedidos;