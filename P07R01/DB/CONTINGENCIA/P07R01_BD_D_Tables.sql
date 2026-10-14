USE GenOctCCR
GO
/*
Proyecto P07R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20261008
Descripción: Contingencia de tablas
*/

--Se borra primero Rutas por las llaves foraneas hacia Choferes y Camiones
BEGIN TRANSACTION;
BEGIN TRY
	IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Rutas'
	AND TABLE_SCHEMA = 'dbo')
	BEGIN
		DROP TABLE dbo.Rutas;
		PRINT 'SE ELIMINO LA TABLA Rutas';
	END
	ELSE
	BEGIN
		PRINT 'NO EXISTE LA TABLA Rutas';
	END
	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;
	THROW;
END CATCH
GO
------------------------------------------------------------------------------------------------
BEGIN TRANSACTION;
BEGIN TRY
	IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Choferes'
	AND TABLE_SCHEMA = 'dbo')
	BEGIN
		DROP TABLE dbo.Choferes;
		PRINT 'SE ELIMINO LA TABLA Choferes';
	END
	ELSE
	BEGIN
		PRINT 'NO EXISTE LA TABLA Choferes';
	END
	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;
	THROW;
END CATCH
GO
------------------------------------------------------------------------------------------------
BEGIN TRANSACTION;
BEGIN TRY
	IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Camiones'
	AND TABLE_SCHEMA = 'dbo')
	BEGIN
		DROP TABLE dbo.Camiones;
		PRINT 'SE ELIMINO LA TABLA Camiones';
	END
	ELSE
	BEGIN
		PRINT 'NO EXISTE LA TABLA Camiones';
	END
	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;
	THROW;
END CATCH
GO
------------------------------------------------------------------------------------------------
