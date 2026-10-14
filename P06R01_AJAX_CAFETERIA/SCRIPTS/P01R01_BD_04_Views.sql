USE GenOct
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20260930
Descripción: Creacion de vistas
*/

CREATE OR ALTER VIEW VWVistaResumenVentas 
AS
	SELECT TOP (10)
	c.ClienteID,
	c.Nombre AS Cliente,
	c.Email,
	COUNT(p.PedidoID) AS TotalPedidos,
	ISNULL(SUM(p.Total),0) AS MontoTotal,
	MAX(p.Fecha) AS UltimaCompra
FROM Clientes AS c
LEFT JOIN Pedidos AS p
ON c.ClienteID = p.ClienteID
GROUP BY c.ClienteID, c.Nombre, c.Email
ORDER BY MontoTotal DESC;
GO
------------------------------------------------------------------------------------------
--SELECT*FROM VWVistaResumenVentas;
--------------------------------------------------
