USE Thona_PT
GO
/*
Proyecto P04R01_BD_JAGJ
Autor: Jose Angel Garcia Juarez
Fecha: 20261006
Descripción: Agrega a TBLUsuarios las columnas del registro de la prueba Thona
             (ID generado, preposiciones, datos del código postal y fecha de registro).
             Todas aceptan NULL para no afectar los registros existentes.
             Se puede ejecutar varias veces: solo agrega las columnas que falten.
*/

BEGIN TRANSACTION
BEGIN TRY

	IF COL_LENGTH('dbo.TBLUsuarios', 'idUsuarioGenerado') IS NULL
		ALTER TABLE dbo.TBLUsuarios ADD idUsuarioGenerado NVARCHAR(10) NULL;

	IF COL_LENGTH('dbo.TBLUsuarios', 'preposiciones') IS NULL
		ALTER TABLE dbo.TBLUsuarios ADD preposiciones NVARCHAR(200) NULL;

	IF COL_LENGTH('dbo.TBLUsuarios', 'cp') IS NULL
		ALTER TABLE dbo.TBLUsuarios ADD cp CHAR(5) NULL;

	IF COL_LENGTH('dbo.TBLUsuarios', 'codEstado') IS NULL
		ALTER TABLE dbo.TBLUsuarios ADD codEstado NVARCHAR(5) NULL;

	IF COL_LENGTH('dbo.TBLUsuarios', 'estado') IS NULL
		ALTER TABLE dbo.TBLUsuarios ADD estado NVARCHAR(100) NULL;

	IF COL_LENGTH('dbo.TBLUsuarios', 'municipio') IS NULL
		ALTER TABLE dbo.TBLUsuarios ADD municipio NVARCHAR(100) NULL;

	IF COL_LENGTH('dbo.TBLUsuarios', 'codColonia') IS NULL
		ALTER TABLE dbo.TBLUsuarios ADD codColonia NVARCHAR(10) NULL;

	IF COL_LENGTH('dbo.TBLUsuarios', 'colonia') IS NULL
		ALTER TABLE dbo.TBLUsuarios ADD colonia NVARCHAR(150) NULL;

	IF COL_LENGTH('dbo.TBLUsuarios', 'fechaRegistro') IS NULL
		ALTER TABLE dbo.TBLUsuarios ADD fechaRegistro DATETIME NULL
			CONSTRAINT DF_TBLUsuarios_fechaRegistro DEFAULT GETDATE();

	COMMIT TRANSACTION;
	PRINT 'Columnas de registro verificadas/agregadas correctamente';
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION
	THROW
END CATCH
GO

SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'TBLUsuarios'
ORDER BY ORDINAL_POSITION
