USE GenOctCCR
GO
/*
Proyecto P07R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20261008
Descripción: Contingencia de Carga Inicial
*/

BEGIN TRANSACTION;
BEGIN TRY
	IF EXISTS(SELECT 1 FROM dbo.Rutas)
	OR EXISTS(SELECT 1 FROM dbo.Choferes)
	OR EXISTS(SELECT 1 FROM dbo.Camiones)
	BEGIN
		DELETE FROM dbo.Rutas;
		DELETE FROM dbo.Choferes;
		DELETE FROM dbo.Camiones;

		--Reinicia los IDENTITY para que el siguiente registro vuelva a ser el 1
		DBCC CHECKIDENT ('dbo.Rutas', RESEED, 0);
		DBCC CHECKIDENT ('dbo.Choferes', RESEED, 0);
		DBCC CHECKIDENT ('dbo.Camiones', RESEED, 0);

		PRINT 'SE ELIMINARON REGISTROS DE LAS TABLAS';
	END
	ELSE
	BEGIN
		PRINT 'NO HAY REGISTROS EN LAS TABLAS';
	END
	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;
	THROW;
END CATCH
GO
