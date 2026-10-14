USE master
GO
/*
Proyecto P07R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20261008
Descripción: Contingencia de Base de Datos
*/

--DROP DATABASE no se permite dentro de una transaccion, por eso no lleva BEGIN TRANSACTION
IF EXISTS (SELECT 1 FROM sys.databases WHERE name = N'GenOctCCR')
BEGIN
	--Cierra las conexiones abiertas a la base para poder eliminarla
	ALTER DATABASE GenOctCCR SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE GenOctCCR;
	PRINT 'SE ELIMINO LA BASE DE DATOS';
END
ELSE
BEGIN
	PRINT 'LA BASE DE DATOS NO EXISTE';
END
GO
