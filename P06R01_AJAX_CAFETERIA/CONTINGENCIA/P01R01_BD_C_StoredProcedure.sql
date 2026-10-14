USE GenOct
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20261020
Descripción: Contingencia de Stored Procedures
*/
--===========================================================

--COMMIT Y ROLLBACK

--SP1
BEGIN TRANSACTION
	BEGIN TRY
		--VALIDACIONES
		IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'spObtenerClienteId'
		AND ROUTINE_SCHEMA = 'dbo')
		BEGIN
			--Eliminar: procedimiento [dbo].[spObtenerClienteId]
			DROP PROCEDURE spObtenerClienteId;
			PRINT 'Procedimiento spObtenerClienteId eliminado correctamente';
		END
		ELSE
		BEGIN
			PRINT 'El procedimiento spObtenerClienteId no existe'
		END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		THROW
	END CATCH
GO

--SP2
BEGIN TRANSACTION
	BEGIN TRY
		--VALIDACIONES
		IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'spInsertarDetallePedido'
		AND ROUTINE_SCHEMA = 'dbo')
		BEGIN
			--Eliminar: procedimiento [dbo].[spInsertarDetallePedido]
			DROP PROCEDURE spInsertarDetallePedido;
			PRINT 'Procedimiento spInsertarDetallePedido eliminado correctamente';
		END
		ELSE
		BEGIN
			PRINT 'El procedimiento spInsertarDetallePedido no existe'
		END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		THROW
	END CATCH
GO

--SP3
BEGIN TRANSACTION
	BEGIN TRY
		--VALIDACIONES
		IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'spInsertarPedido'
		AND ROUTINE_SCHEMA = 'dbo')
		BEGIN
			--Eliminar: procedimiento [dbo].[spInsertarPedido]
			DROP PROCEDURE spInsertarPedido;
			PRINT 'Procedimiento spInsertarPedido eliminado correctamente';
		END
		ELSE
		BEGIN
			PRINT 'El procedimiento spInsertarPedido no existe'
		END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		THROW
	END CATCH
GO

--SP4
BEGIN TRANSACTION
	BEGIN TRY
		--VALIDACIONES
		IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'spConsultarContenidoTablas'
		AND ROUTINE_SCHEMA = 'dbo')
		BEGIN
			--Eliminar: procedimiento [dbo].[spConsultarContenidoTablas]
			DROP PROCEDURE spConsultarContenidoTablas;
			PRINT 'Procedimiento spConsultarContenidoTablas eliminado correctamente';
		END
		ELSE
		BEGIN
			PRINT 'El procedimiento spConsultarContenidoTablas no existe'
		END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
		THROW
	END CATCH
GO