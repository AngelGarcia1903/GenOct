USE GenOctCCR
GO

/*
Proyecto P07R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20261007
Descripción: Vistas GenOctCCR
*/
--SP_Help 'Rutas'

		CREATE OR ALTER VIEW VWCamiones
		AS
		SELECT IdCamion, Matricula, TipoCamion,Modelo, Marca, Capacidad, Kilometraje, UrlFoto
		FROM Camiones WHERE Disponibilidad = 1;
		GO
		-------------------------------------------------------------------------------------------
		CREATE OR ALTER VIEW VWChoferes
		AS 
		SELECT IdChofer, Nombre, ApPaterno, ApMaterno, Telefono, FechaNacimiento, Licencia, UrlFoto, FechaRegistro
		FROM Choferes WHERE Disponibilidad = 1;
		GO
		-------------------------------------------------------------------------------------------------
		CREATE OR ALTER VIEW VWRutas
		AS 
		SELECT IdChofer, IdCamion, Origen, Destino, FechaSalida, FechaLlegada, ATiempo
		FROM Rutas WHERE ATiempo = 1;
		GO



