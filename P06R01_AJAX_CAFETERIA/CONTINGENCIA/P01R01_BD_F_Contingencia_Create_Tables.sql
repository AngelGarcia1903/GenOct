USE GenOct
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20260930
Descripción: Contingencia para creacion de tabla
Modificación: 20261012 - Antes solo borraba si existían las 5 tablas a la vez; ahora borra cada una
              que exista, en orden de llaves foráneas (hijas primero).
*/

BEGIN TRANSACTION
BEGIN TRY
	IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DetallesPedido' AND TABLE_SCHEMA = 'dbo')
	BEGIN
		DROP TABLE DetallesPedido;
		PRINT 'Se eliminó la tabla DetallesPedido';
	END

	IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Pedidos' AND TABLE_SCHEMA = 'dbo')
	BEGIN
		DROP TABLE Pedidos;
		PRINT 'Se eliminó la tabla Pedidos';
	END

	IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Clientes' AND TABLE_SCHEMA = 'dbo')
	BEGIN
		DROP TABLE Clientes;
		PRINT 'Se eliminó la tabla Clientes';
	END

	IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Productos' AND TABLE_SCHEMA = 'dbo')
	BEGIN
		DROP TABLE Productos;
		PRINT 'Se eliminó la tabla Productos';
	END

	IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AuditoriaProductos' AND TABLE_SCHEMA = 'dbo')
	BEGIN
		DROP TABLE AuditoriaProductos;
		PRINT 'Se eliminó la tabla AuditoriaProductos';
	END

	COMMIT TRANSACTION

END TRY
BEGIN CATCH
IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		THROW
END CATCH
GO
