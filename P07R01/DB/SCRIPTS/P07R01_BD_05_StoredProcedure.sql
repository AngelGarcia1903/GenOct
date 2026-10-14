USE GenOctCCR
GO

/*
Proyecto P07R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20261007
Descripción: SP
*/

GO
----------1
-----------------CAMIONES-----------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.Insert_Camion
(
	@Matricula VARCHAR(50),
	@TipoCamion VARCHAR(50),
	@Modelo INT,
	@Marca VARCHAR(50),
	@Capacidad INT,
	@Kilometraje FLOAT,
	@Disponibilidad BIT,
	@UrlFoto VARCHAR(255)
)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION;

		IF EXISTS (SELECT 1 FROM dbo.Camiones WHERE Matricula = @Matricula)
		BEGIN
			THROW 50001, 'Ya existe un camion con esa matricula', 1;
		END

		INSERT INTO dbo.Camiones
		(
			Matricula,
			TipoCamion,
			Modelo,
			Marca,
			Capacidad,
			Kilometraje,
			Disponibilidad,
			UrlFoto
		)
		VALUES
		(
			@Matricula,
			@TipoCamion,
			@Modelo,
			@Marca,
			@Capacidad,
			@Kilometraje,
			@Disponibilidad,
			@UrlFoto
		);

		COMMIT TRANSACTION;

		SELECT 'Camion insertado correctamente' AS Mensaje;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
------------------------------------------------------------------------------------


--SP_Help 'Camiones'

------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.Select_Camion
(
	@Matricula VARCHAR(50)
)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		SELECT *
		FROM dbo.Camiones
		WHERE Matricula = @Matricula;

		IF @@ROWCOUNT = 0
			PRINT 'No se encontro ningun camion con esa matricula';
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO
-----------------------------------------------------------------------------------------

-----------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.Update_Camion
(
	@IdCamion INT,
	@Matricula VARCHAR(50),
	@TipoCamion VARCHAR(50),
	@Modelo INT,
	@Marca VARCHAR(50),
	@Capacidad INT,
	@Kilometraje FLOAT,
	@Disponibilidad BIT,
	@UrlFoto VARCHAR(255)
)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION;

		UPDATE dbo.Camiones
		SET
			Matricula = @Matricula,
			TipoCamion = @TipoCamion,
			Modelo = @Modelo,
			Marca = @Marca,
			Capacidad = @Capacidad,
			Kilometraje = @Kilometraje,
			Disponibilidad = @Disponibilidad,
			UrlFoto = @UrlFoto
		WHERE IdCamion = @IdCamion;

		IF @@ROWCOUNT = 0
		BEGIN
			THROW 50002, 'No se encontro ningun camion con ese ID', 1;
		END

		COMMIT TRANSACTION;

		SELECT 'Camion actualizado correctamente' AS Mensaje;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
-----------------------------------------------------------------------------------------------------------


--------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.Delete_Camion
(
	@IdCamion INT
)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION;

		DELETE
		FROM dbo.Camiones
		WHERE IdCamion = @IdCamion;

		IF @@ROWCOUNT = 0
		BEGIN
			THROW 50003, 'No se encontro ningun camion con ese ID', 1;
		END

		COMMIT TRANSACTION;

		SELECT 'Camion eliminado correctamente' AS Mensaje;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
-----------------CHOFERES----------------------------------------------------------


-----------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.Insert_Choferes
(
	@Nombre VARCHAR(100),
	@ApPaterno VARCHAR(100),
	@ApMaterno VARCHAR(100),
	@Telefono VARCHAR(15),
	@FechaNacimiento DATE,
	@Licencia VARCHAR(50),
	@UrlFoto VARCHAR(255),
	@Disponibilidad BIT
)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION;

		IF EXISTS (SELECT 1 FROM dbo.Choferes WHERE Licencia = @Licencia)
		BEGIN
			THROW 50001, 'Ya existe un chofer con esa licencia', 1;
		END

		INSERT INTO dbo.Choferes
		(
			Nombre,
			ApPaterno,
			ApMaterno,
			Telefono,
			FechaNacimiento,
			Licencia,
			UrlFoto,
			Disponibilidad
		)
		VALUES
		(
			@Nombre,
			@ApPaterno,
			@ApMaterno,
			@Telefono,
			@FechaNacimiento,
			@Licencia,
			@UrlFoto,
			@Disponibilidad
		);

		COMMIT TRANSACTION;

		SELECT 'Chofer insertado correctamente' AS Mensaje;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO
-------------------------------------------------------------------------------------------------


-------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.Select_Chofer
(
	@Disponibilidad BIT = NULL
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		IdChofer,
		Nombre,
		ApPaterno,
		ApMaterno,
		Telefono,
		FechaNacimiento,
		Licencia,
		UrlFoto,
		Disponibilidad,
		FechaRegistro
	FROM dbo.Choferes
	WHERE @Disponibilidad IS NULL
		OR Disponibilidad = @Disponibilidad;
END
GO
---------------------------------------------------------------------------------------------


----------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.Update_Choferes
(
	@IdChofer INT,
	@Nombre VARCHAR(100),
	@ApPaterno VARCHAR(100),
	@ApMaterno VARCHAR(100),
	@Telefono VARCHAR(15),
	@FechaNacimiento DATE,
	@Licencia VARCHAR(50),
	@UrlFoto VARCHAR(255),
	@Disponibilidad BIT
)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION;

		UPDATE dbo.Choferes
		SET
			Nombre = @Nombre,
			ApPaterno = @ApPaterno,
			ApMaterno = @ApMaterno,
			Telefono = @Telefono,
			FechaNacimiento = @FechaNacimiento,
			Licencia = @Licencia,
			UrlFoto = @UrlFoto,
			Disponibilidad = @Disponibilidad
		WHERE IdChofer = @IdChofer;

		IF @@ROWCOUNT = 0
		BEGIN
			THROW 50002, 'No se encontro ningun chofer con ese ID', 1;
		END

		COMMIT TRANSACTION;

		SELECT 'Chofer actualizado correctamente' AS Mensaje;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO


----------------------------------------------------------------------------------------------------------

-----------------------------------------------------------------------------------------------------------

CREATE OR ALTER PROCEDURE dbo.Delete_Chofer
(
	@IdChofer INT
)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION;

		DELETE
		FROM dbo.Choferes
		WHERE IdChofer = @IdChofer;

		IF @@ROWCOUNT = 0
		BEGIN
			THROW 50003, 'No se encontro ningun chofer con ese ID', 1;
		END

		COMMIT TRANSACTION;

		SELECT 'Chofer eliminado correctamente' AS Mensaje;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO


-----------------RUTAS---------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.Insert_Rutas
(
	@IdChofer INT,
	@IdCamion INT,
	@Origen VARCHAR(200),
	@Destino VARCHAR(200),
	@FechaSalida DATETIME,
	@FechaLlegada DATETIME,
	@ATiempo BIT,
	@Distancia FLOAT
)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION;

		IF EXISTS
		(
			SELECT 1
			FROM dbo.Rutas
			WHERE IdChofer = @IdChofer
				AND IdCamion = @IdCamion
		)
		BEGIN
			THROW 50001, 'Ya existe una ruta asignada a ese chofer y camion', 1;
		END

		INSERT INTO dbo.Rutas
		(
			IdChofer,
			IdCamion,
			Origen,
			Destino,
			FechaSalida,
			FechaLlegada,
			ATiempo,
			Distancia
		)
		VALUES
		(
			@IdChofer,
			@IdCamion,
			@Origen,
			@Destino,
			@FechaSalida,
			@FechaLlegada,
			@ATiempo,
			@Distancia
		);

		COMMIT TRANSACTION;

		SELECT 'Ruta insertada correctamente' AS Mensaje;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO


------------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.Select_Rutas
(
	@IdRuta INT = NULL
)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		SELECT
			IdRuta,
			IdChofer,
			IdCamion,
			Origen,
			Destino,
			FechaSalida,
			FechaLlegada,
			ATiempo,
			Distancia,
			FechaRegistro
		FROM dbo.Rutas
		WHERE @IdRuta IS NULL
			OR IdRuta = @IdRuta;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO


-----------------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.Update_Rutas
(
	@IdRuta INT,
	@IdChofer INT,
	@IdCamion INT,
	@Origen VARCHAR(200),
	@Destino VARCHAR(200),
	@FechaSalida DATETIME,
	@FechaLlegada DATETIME,
	@ATiempo BIT,
	@Distancia FLOAT
)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION;

		UPDATE dbo.Rutas
		SET
			IdChofer = @IdChofer,
			IdCamion = @IdCamion,
			Origen = @Origen,
			Destino = @Destino,
			FechaSalida = @FechaSalida,
			FechaLlegada = @FechaLlegada,
			ATiempo = @ATiempo,
			Distancia = @Distancia
		WHERE IdRuta = @IdRuta;

		IF @@ROWCOUNT = 0
		BEGIN
			THROW 50002, 'No se encontro ninguna ruta con ese ID', 1;
		END

		COMMIT TRANSACTION;

		SELECT 'Ruta actualizada correctamente' AS Mensaje;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO


------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.Delete_Ruta
(
	@IdRuta INT
)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION;

		DELETE
		FROM dbo.Rutas
		WHERE IdRuta = @IdRuta;

		IF @@ROWCOUNT = 0
		BEGIN
			THROW 50003, 'No se encontro ninguna ruta con ese ID', 1;
		END

		COMMIT TRANSACTION;

		SELECT 'Ruta eliminada correctamente' AS Mensaje;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
END
GO


-----------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.Select_Rutas_Detalle
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		r.IdRuta,
		r.Origen,
		r.Destino,
		r.FechaSalida,
		r.FechaLlegada,
		r.ATiempo,
		r.Distancia,
		c.Nombre AS NombreChofer,
		c.Licencia,
		c.Telefono AS TelefonoChofer,
		c.UrlFoto AS FotoChofer,
		cam.Matricula,
		cam.UrlFoto AS FotoCamion
	FROM dbo.Rutas AS r
	INNER JOIN dbo.Choferes AS c
		ON r.IdChofer = c.IdChofer
	INNER JOIN dbo.Camiones AS cam
		ON r.IdCamion = cam.IdCamion;
END
GO


-----------------VALIDACIONES / CONSULTAS------------------------------------------
CREATE OR ALTER PROCEDURE dbo.Existe_Licencia
(
	@Licencia VARCHAR(50)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		CASE
			WHEN EXISTS
			(
				SELECT 1
				FROM dbo.Choferes
				WHERE Licencia = @Licencia
			)
			THEN 1
			ELSE 0
		END AS ExistenciaLicencia;
END
GO


-------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.Obtener_Camion_ID
(
	@IdCamion INT
)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		SELECT
			IdCamion,
			Matricula,
			TipoCamion,
			Modelo,
			Marca,
			Capacidad,
			Kilometraje,
			Disponibilidad,
			UrlFoto
		FROM dbo.Camiones
		WHERE IdCamion = @IdCamion;

		IF @@ROWCOUNT = 0
		BEGIN
			THROW 50002, 'No se encontro ningun camion con ese ID', 1;
		END
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO

----------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.Existe_Matricula
(
	@Matricula VARCHAR(50)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
		CASE
			WHEN EXISTS
			(
				SELECT 1
				FROM dbo.Camiones
				WHERE Matricula = @Matricula
			)
			THEN 1
			ELSE 0
		END AS ExisteMatricula;
END
GO


-------------------------------------------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.Listar_Camiones
(
	@Disponibilidad BIT = NULL
)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		SELECT
			IdCamion,
			Matricula,
			TipoCamion,
			Modelo,
			Marca,
			Capacidad,
			Kilometraje,
			Disponibilidad,
			UrlFoto
		FROM dbo.Camiones
		WHERE @Disponibilidad IS NULL
			OR Disponibilidad = @Disponibilidad
		ORDER BY IdCamion DESC;
	END TRY
	BEGIN CATCH
		THROW;
	END CATCH
END
GO


