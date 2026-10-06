USE GenOct
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20260930
Descripción: Contingencia  de vistas
*/

BEGIN TRANSACTION
BEGIN TRY
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME = 'VWVistaResumenVentas'
AND TABLE_SCHEMA = 'dbo')
	BEGIN
		DROP VIEW VWVistaResumenVentas
		PRINT 'Se elimino correctamente la tabla de Vistas!'
	END
	ELSE
	BEGIN
	    PRINT 'La tabla de Vistas no existe!'
	END
	COMMIT TRANSACTION
END TRY

BEGIN CATCH
IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		THROW
END CATCH
GO
------------------------------------------------------------------------------

