USE GenOct
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20260930
Descripción: Creacion de STORED PROCEDURES
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
EXEC [dbo].[spObtenerClienteId]
@Cliente=1
GO
------------------------------------------------------

DECLARE @Nuevop INT;
EXEC spInsertarPedido
@Cliente=1,
@Total=358,
@Estado='completo',
@NuevoPedidoID =@Nuevop OUTPUT; 
GO
--PRINT @Nuevop;
--SELECT @Nuevop AS UltimoPedido;
-----------------------------------------------------------

--sp propio
CREATE OR ALTER PROCEDURE spInsertarDetallePedido
@PedidoID INT,
@ProductoID INT,
@Cantidad INT,
@PrecioUnitario INT,
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

DECLARE @NuevoDP INT;
EXEC [dbo].[spInsertarDetallePedido]
@PedidoID = 2,
@ProductoID = 6,
@Cantidad=50,
@PrecioUnitario = 20,
@UltimoDetallePedido = @NuevoDP OUTPUT;
GO

---------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.spConsultarContenidoTablas
AS 
BEGIN

SELECT * FROM dbo.Productos;
SELECT * FROM dbo.Clientes;
SELECT * FROM dbo.Pedidos;
SELECT * FROM dbo.DetallesPedido;
SELECT * FROM dbo.Pedidos;

END
GO