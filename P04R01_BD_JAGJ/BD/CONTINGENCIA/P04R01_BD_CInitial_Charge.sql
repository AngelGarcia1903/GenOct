USE Thona_PT
GO
/*
Proyecto P0R01_BD_JAGJ
Autor: Jose Angel Garcia Juarez
Fecha: 20261012
Descripción: Contingencia para carga inicial de datos en TBLUsuarios
*/

BEGIN TRY
    -- Se eliminan específicamente los registros insertados en la carga inicial
    DELETE FROM TBLUsuarios 
    WHERE email IN (
        'jose@gmail.com', 
        'juan@gmail.com', 
        'angel@gmail.com', 
        'antonio@gmail.com', 
        'daniel@gmail.com'
    );

    PRINT 'Contingencia de carga inicial ejecutada: Registros eliminados correctamente de TBLUsuarios.';

    -- Opcional: Si necesitas que la tabla quede completamente vacía y 
    -- reiniciar el contador de identidad (ID autoincrementable), 
    -- comenta el bloque DELETE de arriba y usa esta línea:
    -- TRUNCATE TABLE TBLUsuarios;

END TRY
BEGIN CATCH
    PRINT 'Ocurrió un error al intentar revertir la carga inicial.';
    THROW;
END CATCH;
GO