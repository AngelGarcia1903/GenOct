USE GenOct
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20260930
Descripción: Creacion de STORED PROCEDURES
Modificación: 20261012
  - spInsertarDetallePedido: @PrecioUnitario pasa de INT a DECIMAL(10,2) (antes 20.50 se guardaba como 21).
  - spConsultarContenidoTablas: consultaba Pedidos dos veces; ahora la segunda es AuditoriaProductos.
  - Los EXEC de prueba quedan comentados: insertaban un pedido nuevo cada vez que se ejecutaba el script.
  - La API usa spObtenerClienteId, spInsertarPedido y spInsertarDetallePedido.
*/

CREATE OR ALTER PROCEDURE spObtenerClienteId
	@Cliente INT
	AS
	BEGIN --LOGICA DE SP
	SELECT
	ClienteID,
	Nombre,
	Email,
	Telefono
	FROM Clientes
	WHERE ClienteID=@Cliente
	END
GO

------------------------------------------------------------

CREATE OR ALTER PROCEDURE spInsertarPedido
	@Cliente INT = 1,
	@Total DECIMAL(10,2) = 500,
	@Estado NVARCHAR(20) = 'Pendiente',
	@NuevoPedidoID INT OUTPUT
AS --es parte del procedure
BEGIN
	INSERT INTO Pedidos(ClienteID, Total, Estado, Fecha)
	VALUES (@Cliente, @Total, @Estado, GETDATE());

	--Obtener el ID del pedido recien creado
	SET @NuevoPedidoID = SCOPE_IDENTITY();

	SELECT @NuevoPedidoID AS PedidoCreado;
END;
GO

----------------------------------------------------------------
-- Pruebas (descomentar para ejecutarlas a mano)
--EXEC [dbo].[spObtenerClienteId]
--@Cliente=1
--GO
------------------------------------------------------

--DECLARE @Nuevop INT;
--EXEC spInsertarPedido
--@Cliente=1,
--@Total=358,
--@Estado='completo',
--@NuevoPedidoID =@Nuevop OUTPUT;
--GO
--PRINT @Nuevop;
--SELECT @Nuevop AS UltimoPedido;
-----------------------------------------------------------

--sp propio
CREATE OR ALTER PROCEDURE spInsertarDetallePedido
@PedidoID INT,
@ProductoID INT,
@Cantidad INT,
@PrecioUnitario DECIMAL(10,2),
@UltimoDetallePedido INT OUTPUT
AS
BEGIN

	INSERT INTO DetallesPedido(PedidoID, ProductoID, Cantidad, PrecioUnitario)
	VALUES (@PedidoID, @ProductoID, @Cantidad, @PrecioUnitario)

	SET @UltimoDetallePedido = SCOPE_IDENTITY();

	SELECT @UltimoDetallePedido as DetallePedido;

END;
GO
----------------------------------------------------------------------
-- Prueba (descomentar para ejecutarla a mano)
--DECLARE @NuevoDP INT;
--EXEC [dbo].[spInsertarDetallePedido]
--@PedidoID = 2,
--@ProductoID = 6,
--@Cantidad=50,
--@PrecioUnitario = 20,
--@UltimoDetallePedido = @NuevoDP OUTPUT;
--GO

---------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.spConsultarContenidoTablas
AS
BEGIN

SELECT * FROM dbo.Productos;
SELECT * FROM dbo.Clientes;
SELECT * FROM dbo.Pedidos;
SELECT * FROM dbo.DetallesPedido;
SELECT * FROM dbo.AuditoriaProductos;

END
GO
