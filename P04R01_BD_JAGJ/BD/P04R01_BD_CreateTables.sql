USE Thona_PT
GO
/*
Proyecto P0R01_BD_JAGJ
Autor: Jose Angel Garcia Juarez
Fecha: 20261005
Descripción: creacion de la tabla de Thona
*/


BEGIN TRANSACTION
BEGIN TRY
	IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=N'TBLUsuarios' AND TABLE_SCHEMA=N'dbo')

		BEGIN

			CREATE TABLE TBLUsuarios(
			idUsuarios INT PRIMARY KEY IDENTITY (1,1),
			nombre NVARCHAR(100) NOT NULL,
			hobby NVARCHAR(150),
			fechanacimiento DATETIME NOT NULL,
			email NVARCHAR(100) NOT NULL
			)
			PRINT 'Tabla creada exitosamente';
		END
			ELSE

		BEGIN
			PRINT 'La tabla ya existe'
		END

	COMMIT TRANSACTION;
END TRY
	BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
		THROW
	END CATCH
GO


select * from TBLUsuarios