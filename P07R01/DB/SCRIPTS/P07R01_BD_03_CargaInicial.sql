USE GenOctCCR
GO

/*
Proyecto P07R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20261007
Descripción: Carga Inicial GenOctCCR
*/

--SP_HELP 'Camiones
--SELECT * FROM Camiones


BEGIN TRANSACTION
BEGIN TRY
   BEGIN
		INSERT INTO Camiones(Matricula, TipoCamion, Modelo, Marca, Capacidad,
		Kilometraje, Disponibilidad, UrlFoto) 
		VALUES
		('ICNRS', 'CAJA SECA', 2019, 'VW', 1200, 230000, 1, 'frgg5'),
		('I3R34F', 'TOLVA', 2016, 'VW', 1200, 230000, 1, 'http'),
		('4RRF', 'CAMION', 2011, 'VW', 1200, 230000, 1, 'htpp'),
		('SCDC', 'SEDAN', 2013, 'VW', 1200, 230000, 1, 'http'),
		('CWCE', 'PIPA', 2016, 'VW', 1200, 230000, 1, 'http'),
		('45G4F', 'REFRIGERADO', 2016, 'VW', 1200, 230000, 1, 'http'),
		('2S3CE', 'CAJA SECA', 2019, 'VW', 1200, 230000, 1, 'http');

	PRINT 'Se guardaron datos'	
	END
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;
	THROW;
END CATCH
GO

--SP_Help 'choferes'
INSERT INTO Choferes(Nombre, ApPaterno, ApMaterno, Telefono, FechaNacimiento, Licencia, UrlFoto, Disponibilidad)
VALUES
('Armando', 'Jimenez', 'Miranda', '665775434', '20030823', 'ASNUCECN', 'HTTP', 2),
('Aranza', 'Rio', 'Miranda', '4445434', '20030823', 'ASNUCECN', 'HTTP', 2),
('Araceli', 'Castro', 'Miranda', '64555434', '20030823', 'ASNUCECN', 'HTTP', 2),
('Ana', 'Hamer', 'Miranda', '43555434', '20030823', 'ASNUCECN', 'HTTP', 2),
('Antonia', 'Felineses', 'Miranda', '7565434', '20030823', 'ASNUCECN', 'HTTP', 2);


INSERT INTO Rutas(IdChofer, IdCamion, Origen, Destino, FechaSalida, FechaLlegada, ATiempo, Distancia)
VALUES
(1,3, 'Irapuato', 'Leon', '20230423', '20230512', 1, 156.6),
(2,3, 'Irapuato', 'Leon', '20230423', '20230512', 1, 156.6),
(2,3, 'Irapuato', 'Leon', '20230423', '20230512', 1, 156.6),
(2,3, 'Irapuato', 'Leon', '20230423', '20230512', 1, 156.6),
(3,3, 'Irapuato', 'Leon', '20230423', '20230512', 1, 156.6);

