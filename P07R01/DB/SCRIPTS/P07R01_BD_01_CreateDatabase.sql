USE master
GO
/*
Proyecto P07R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20261007
Descripción: Creacion de la Base de Datos GenOctCCR
*/

IF NOT EXISTS(SELECT name FROM master.dbo.sysdatabases	WHERE name = N'GenOctCCR')

BEGIN
	CREATE DATABASE GenOctCCR;
	PRINT 'Se creo la base de datos GenOctCCR'
END
ELSE
BEGIN
	PRINT 'La base de datos GenOctCCr ya existe'
END
GO