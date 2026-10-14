USE GenOctCCR
GO

/*
Proyecto P07R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20261007
Descripción: Creacion de tablas en GenOctCCR
*/

BEGIN TRANSACTION;

BEGIN TRY

    IF NOT EXISTS (
        SELECT 1
        FROM INFORMATION_SCHEMA.TABLES
        WHERE TABLE_NAME = N'Camiones'
          AND TABLE_SCHEMA = N'dbo'
          AND TABLE_TYPE = N'BASE TABLE'
    )
    BEGIN

        CREATE TABLE dbo.Camiones
        (
            IdCamion INT IDENTITY(1,1) PRIMARY KEY,
            Matricula VARCHAR(50),
            TipoCamion VARCHAR(50),
            Modelo INT NOT NULL,
            Marca VARCHAR(50) NOT NULL,
            Capacidad INT NOT NULL,
            Kilometraje FLOAT NOT NULL,
            Disponibilidad BIT NOT NULL,
            UrlFoto VARCHAR(255) NOT NULL
        );

        PRINT 'Se creó la tabla Camiones correctamente';

    END
    ELSE
    BEGIN

        PRINT 'La tabla Camiones ya existe';

    END

    COMMIT TRANSACTION;

END TRY

BEGIN CATCH

    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    THROW;

END CATCH;
GO

	-------------------------------------------------------------------------------------------------
	
	BEGIN TRANSACTION
	BEGIN TRY

		IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Choferes'
		AND TABLE_SCHEMA = 'dbo')
		
		BEGIN

				CREATE TABLE dbo.Choferes
				( 
					IdChofer INT IDENTITY(1,1) PRIMARY KEY,
					Nombre VARCHAR(100) NOT NULL,
					ApPaterno VARCHAR(100) NOT NULL,
					ApMaterno VARCHAR(100) NOT NULL,
					Telefono VARCHAR(15) NOT NULL,
					FechaNacimiento DATE NOT NULL,
					Licencia VARCHAR(50) NOT NULL,
					UrlFoto VARCHAR(255) NOT NULL,
					Disponibilidad BIT NOT NULL,
					FechaRegistro DATETIME NOT NULL
						CONSTRAINT DF_Choferes_FechaRegistro DEFAULT GETDATE()
				);

				PRINT 'La tabla Choferes se creo correctamente'
		END
		ELSE
		BEGIN
			PRINT 'La tabla Choferes ya existe'
		END
			COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH

	GO
	-------------------------------------------------------------------------------------------

BEGIN TRANSACTION
	BEGIN TRY

	IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Rutas'
	AND TABLE_SCHEMA = N'dbo')
	BEGIN
		CREATE TABLE dbo.Rutas 
		(
		IdRuta INT IDENTITY(1,1) PRIMARY KEY,
		IdChofer INT NOT NULL,
		IdCamion INT NOT NULL,
		Origen VARCHAR(200) NOT NULL,
		Destino VARCHAR(200) NOT NULL,
		FechaSalida DATETIME NOT NULL,
		FechaLlegada DATETIME NOT NULL,
		ATiempo BIT NOT NULL,
		Distancia FLOAT NOT NULL,
		FechaRegistro DATETIME NOT NULL
			CONSTRAINT DF_Rutas_FechaRegistro DEFAULT GETDATE(),

		
		CONSTRAINT FK_Rutas_Choferes
			FOREIGN KEY(IdChofer)
			REFERENCES dbo.Choferes (IdChofer),

		CONSTRAINT FK_Rutas_Camiones
			FOREIGN KEY(IdCamion)
			REFERENCES dbo.Camiones (IdCamion)


		);

		PRINT 'Tablas Rutas creada correctamente'
	END
	ELSE
	BEGIN
		PRINT 'La tabla Rutas ya existe'
	END

	COMMIT TRANSACTION

	END TRY
	BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;
		THROW;
	END CATCH
GO

-------------------------------------------------------------------------------

EXEC SP_Help 'Camiones'
EXEC SP_Help 'Choferes'
EXEC SP_Help 'Rutas'