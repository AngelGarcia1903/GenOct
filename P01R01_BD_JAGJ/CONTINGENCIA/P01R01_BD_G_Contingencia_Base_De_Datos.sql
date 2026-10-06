USE master
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20260930
Descripción: Contingencia para creacion de Base de Datos
*/

BEGIN TRY

    IF EXISTS (
        SELECT 1
        FROM sys.databases
        WHERE name = 'GenOct'
    )
    BEGIN
        ALTER DATABASE GenOct SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
        DROP DATABASE GenOct;
        PRINT 'Se eliminó la base de datos GenOct correctamente.';
    END
    ELSE
    BEGIN
        PRINT 'La base de datos GenOct no existe.';
    END

END TRY

BEGIN CATCH
    THROW;
END CATCH;
GO

