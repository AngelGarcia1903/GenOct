USE msdb
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20261020
Descripción: Contingencia de Trigger
*/

--TR1
BEGIN TRANSACTION
	BEGIN TRY
		--VALIDACIONES
		
		IF EXISTS (
			SELECT 1
			FROM sys.objects
			WHERE name = 'TR_AuditarCambioPrecio'
				AND type = 'TR'
				AND SCHEMA_NAME(schema_id) = 'dbo'
		)

			BEGIN

				--ELIMINAR R: TRIGGER TR_AuditarCambiosPrecio;
				DROP TRIGGER dbo.TR_AuditarCambioPrecio;
				PRINT 'TRIGGER TR_AuditarCambioPrecio eliminado correctamente';
			END
		ELSE
			BEGIN
				PRINT 'El trigger [dbo].[TR_AuditarCambioPrecio] no existe.'
			END
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW
	END CATCH
GO

--TR2
BEGIN TRANSACTION
	BEGIN TRY
		--VALIDACIONES
		
		IF EXISTS (
			SELECT 1
			FROM sys.objects
			WHERE name = 'TR_Auditoria_After_Insert'
				AND type = 'TR'
				AND SCHEMA_NAME(schema_id) = 'dbo'
		)

			BEGIN

				--ELIMINAR R: TRIGGER TR_Auditoria_After_Insert;
				DROP TRIGGER dbo.TR_Auditoria_After_Insert;
				PRINT 'TRIGGER TR_Auditoria_After_Insert eliminado correctamente';
			END
		ELSE
			BEGIN
				PRINT 'El trigger [dbo].[TR_Auditoria_After_Insert] no existe.'
			END
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW
	END CATCH
GO

--TR3
BEGIN TRANSACTION
	BEGIN TRY
		--VALIDACIONES
		
		IF EXISTS (
			SELECT 1
			FROM sys.objects
			WHERE name = 'TR_Auditoria_Instead_Insert'
				AND type = 'TR'
				AND SCHEMA_NAME(schema_id) = 'dbo'
		)

			BEGIN

				--ELIMINAR R: TRIGGER TR_Auditoria_Instead_Insert;
				DROP TRIGGER dbo.TR_Auditoria_Instead_Insert;
				PRINT 'TRIGGER TR_Auditoria_Instead_Insert eliminado correctamente';
			END
		ELSE
			BEGIN
				PRINT 'El trigger [dbo].[TR_Auditoria_Instead_Insert] no existe.'
			END
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW
	END CATCH
GO

--TR4
BEGIN TRANSACTION
	BEGIN TRY
		--VALIDACIONES
		
		IF EXISTS (
			SELECT 1
			FROM sys.objects
			WHERE name = 'TR_Auditoria_Instead_Update'
				AND type = 'TR'
				AND SCHEMA_NAME(schema_id) = 'dbo'
		)

			BEGIN

				--ELIMINAR R: TRIGGER TR_Auditoria_Instead_Update;
				DROP TRIGGER dbo.TR_Auditoria_Instead_Update;
				PRINT 'TRIGGER TR_Auditoria_Instead_Update eliminado correctamente';
			END
		ELSE
			BEGIN
				PRINT 'El trigger [dbo].[TR_Auditoria_Instead_Update] no existe.'
			END
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW
	END CATCH
GO

--TR5
BEGIN TRANSACTION
	BEGIN TRY
		--VALIDACIONES
		
		IF EXISTS (
			SELECT 1
			FROM sys.objects
			WHERE name = 'TR_Auditoria_After_Delete'
				AND type = 'TR'
				AND SCHEMA_NAME(schema_id) = 'dbo'
		)

			BEGIN

				--ELIMINAR R: TRIGGER TR_Auditoria_After_Delete;
				DROP TRIGGER dbo.TR_Auditoria_After_Delete;
				PRINT 'TRIGGER TR_Auditoria_After_Delete eliminado correctamente';
			END
		ELSE
			BEGIN
				PRINT 'El trigger [dbo].[TR_Auditoria_After_Delete] no existe.'
			END
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW
	END CATCH
GO

--TR6
BEGIN TRANSACTION
	BEGIN TRY
		--VALIDACIONES
		
		IF EXISTS (
			SELECT 1
			FROM sys.objects
			WHERE name = 'TR_Auditoria_Instead_Delete'
				AND type = 'TR'
				AND SCHEMA_NAME(schema_id) = 'dbo'
		)

			BEGIN

				--ELIMINAR R: TRIGGER TR_Auditoria_Instead_Delete;
				DROP TRIGGER dbo.TR_Auditoria_Instead_Delete;
				PRINT 'TRIGGER TR_Auditoria_Instead_Delete correctamente';
			END
		ELSE
			BEGIN
				PRINT 'El trigger [dbo].[TR_Auditoria_Instead_Delete] no existe.'
			END
		COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW
	END CATCH
GO

