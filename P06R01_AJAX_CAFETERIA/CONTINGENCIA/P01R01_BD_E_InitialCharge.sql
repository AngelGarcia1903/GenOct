USE GenOct
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20260930
Descripción: Contingencia de Carga Inicial
Modificación: 20261012
  - Antes solo borraba si las 4 tablas tenían datos a la vez; ahora limpia cada tabla por separado.
  - También limpia AuditoriaProductos.
  - Reinicia los IDENTITY para que, al volver a correr la carga inicial, los IDs empiecen en 1
    (la carga inicial relaciona pedidos y detalles por esos IDs).
*/


BEGIN TRANSACTION
BEGIN TRY

	-- Orden: primero las tablas hijas (llaves foráneas)
	IF EXISTS (SELECT 1 FROM DetallesPedido)
	BEGIN
		DELETE FROM DetallesPedido;
		DBCC CHECKIDENT ('DetallesPedido', RESEED, 0);
		PRINT 'Se eliminaron los registros de DetallesPedido';
	END

	IF EXISTS (SELECT 1 FROM Pedidos)
	BEGIN
		DELETE FROM Pedidos;
		DBCC CHECKIDENT ('Pedidos', RESEED, 0);
		PRINT 'Se eliminaron los registros de Pedidos';
	END

	IF EXISTS (SELECT 1 FROM Clientes)
	BEGIN
		DELETE FROM Clientes;
		DBCC CHECKIDENT ('Clientes', RESEED, 0);
		PRINT 'Se eliminaron los registros de Clientes';
	END

	IF EXISTS (SELECT 1 FROM Productos)
	BEGIN
		DELETE FROM Productos;
		DBCC CHECKIDENT ('Productos', RESEED, 0);
		PRINT 'Se eliminaron los registros de Productos';
	END

	IF EXISTS (SELECT 1 FROM AuditoriaProductos)
	BEGIN
		DELETE FROM AuditoriaProductos;
		DBCC CHECKIDENT ('AuditoriaProductos', RESEED, 0);
		PRINT 'Se eliminaron los registros de AuditoriaProductos';
	END

	PRINT 'Contingencia de carga inicial terminada';
COMMIT TRANSACTION
END TRY

BEGIN CATCH
IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		THROW
END CATCH
GO
