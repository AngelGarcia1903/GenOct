USE Thona_PT
GO
/*
Proyecto P04R01_BD_JAGJ
Autor: Jose Angel Garcia Juarez
Fecha: 20261012
Descripción: Contingencia para revertir las columnas agregadas a TBLUsuarios.
             Elimina primero el constraint de fechaRegistro y luego las columnas.
*/

BEGIN TRANSACTION
BEGIN TRY

    -- 1. ELIMINAR EL CONSTRAINT (RESTRICCIÓN)
    -- Es obligatorio borrar primero el Default Constraint antes de borrar la columna fechaRegistro
    IF EXISTS (SELECT 1 FROM sys.default_constraints WHERE name = 'DF_TBLUsuarios_fechaRegistro')
    BEGIN
        ALTER TABLE dbo.TBLUsuarios DROP CONSTRAINT DF_TBLUsuarios_fechaRegistro;
    END

    -- 2. ELIMINAR LAS COLUMNAS (Validando que existan)
    IF COL_LENGTH('dbo.TBLUsuarios', 'idUsuarioGenerado') IS NOT NULL
        ALTER TABLE dbo.TBLUsuarios DROP COLUMN idUsuarioGenerado;

    IF COL_LENGTH('dbo.TBLUsuarios', 'preposiciones') IS NOT NULL
        ALTER TABLE dbo.TBLUsuarios DROP COLUMN preposiciones;

    IF COL_LENGTH('dbo.TBLUsuarios', 'cp') IS NOT NULL
        ALTER TABLE dbo.TBLUsuarios DROP COLUMN cp;

    IF COL_LENGTH('dbo.TBLUsuarios', 'codEstado') IS NOT NULL
        ALTER TABLE dbo.TBLUsuarios DROP COLUMN codEstado;

    IF COL_LENGTH('dbo.TBLUsuarios', 'estado') IS NOT NULL
        ALTER TABLE dbo.TBLUsuarios DROP COLUMN estado;

    IF COL_LENGTH('dbo.TBLUsuarios', 'municipio') IS NOT NULL
        ALTER TABLE dbo.TBLUsuarios DROP COLUMN municipio;

    IF COL_LENGTH('dbo.TBLUsuarios', 'codColonia') IS NOT NULL
        ALTER TABLE dbo.TBLUsuarios DROP COLUMN codColonia;

    IF COL_LENGTH('dbo.TBLUsuarios', 'colonia') IS NOT NULL
        ALTER TABLE dbo.TBLUsuarios DROP COLUMN colonia;

    IF COL_LENGTH('dbo.TBLUsuarios', 'fechaRegistro') IS NOT NULL
        ALTER TABLE dbo.TBLUsuarios DROP COLUMN fechaRegistro;

    COMMIT TRANSACTION;
    PRINT 'Contingencia ejecutada: Las columnas añadidas y el constraint fueron eliminados correctamente.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
    THROW;
END CATCH
GO