USE master
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20260930
Descripción: Contingencia para creacion de Base de Datos Thona_PT
*/

BEGIN TRY

    IF EXISTS (
        SELECT 1
        FROM sys.databases
        WHERE name = 'Thona_PT'
    )
    BEGIN
        ALTER DATABASE Thona_PT SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
        DROP DATABASE Thona_PT;
        PRINT 'Se eliminó la base de datos Thona_PT correctamente.';
    END
    ELSE
    BEGIN
        PRINT 'La base de datos Thona_PT no existe.';
    END

END TRY

BEGIN CATCH
    THROW;
END CATCH;
GO

