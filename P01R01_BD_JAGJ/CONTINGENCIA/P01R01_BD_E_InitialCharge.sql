USE GenOct
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20260930
Descripción: Contingencia de Carga Inicial
*/


BEGIN TRANSACTION
BEGIN TRY

IF EXISTS
(SELECT 1 FROM Clientes)
AND EXISTS (SELECT 1 FROM Pedidos)
AND EXISTS (SELECT 1 FROM DetallesPedido)
AND EXISTS (SELECT 1 FROM Productos)							
BEGIN
	DELETE FROM DetallesPedido
	DELETE FROM Pedidos;
	DELETE FROM Clientes;
	DELETE FROM Productos;
	PRINT 'Se eliminaron los registros de las tablas'
END
ELSE
BEGIN
PRINT 'Ya no hay registros que eliminar'
END
COMMIT TRANSACTION
END TRY

BEGIN CATCH
IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		THROW
END CATCH
GO

