use GenOct;
GO
/*
Proyecto P0R01_BD_JAGJ
Autor: Jose Angel Garcia Juarez
Fecha: 20260930
Descripción: creación de tablas
*/

-- ACID 
-- COMMIT Y ROLLBACK

BEGIN TRANSACTION
	BEGIN TRY
		IF NOT EXISTS ( SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Productos' AND TABLE_SCHEMA = 'dbo')

		BEGIN
			CREATE TABLE Productos(
			ProductoID INT PRIMARY KEY IDENTITY(1,1),
			Nombre NVARCHAR(100) NOT NULL,
			Categoria NVARCHAR(50) NOT NULL,
			Precio DECIMAL(10,2) NOT NULL,
			Stock INT DEFAULT 0,
			FechaCreacion DATETIME DEFAULT GETDATE()
			)
			PRINT ('LA TABLA PRODUCTOS FUE CREADA CORRECTAMENTE')
		END

			ELSE
		BEGIN
				PRINT ('LA TABLA [dbo].[Productos] YA EXISTE');
		END
		COMMIT TRANSACTION; -- CONFIRMAR CAMBIOS
		END TRY
		BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
GO

-------------------------------------------------

-- commit y rollback
BEGIN TRANSACTION;
	BEGIN TRY
		--VALIDACIONES
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Clientes' AND TABLE_SCHEMA = 'dbo')

			BEGIN

		  -- Tabla Clientes
			CREATE TABLE Clientes (
			ClienteID INT PRIMARY KEY IDENTITY(1,1),
			Nombre NVARCHAR(100) NOT NULL,
			Email NVARCHAR(100),
			Telefono NVARCHAR(15)
			);

PRINT 'Tabla : Clientes fue creada correctamente.';
		END
	ELSE
		BEGIN
			PRINT 'La tabla [dbo].[Clientes] ya existes.';
		END
	COMMIT TRANSACTION; -- Confirmar cambios
  END TRY
  BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;
	THROW;
  END CATCH
GO

-- COMMIT Y ROLLBACK
BEGIN TRANSACTION;
	BEGIN TRY
		--VALIDACIONES
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Pedidos' AND TABLE_SCHEMA = 'dbo')
			BEGIN

			--Tabla Pedidos
				CREATE TABLE Pedidos (
				PedidoID INT PRIMARY KEY IDENTITY(1,1),
				ClienteID INT FOREIGN KEY REFERENCES Clientes(ClienteID),
				Fecha DATETIME DEFAULT GETDATE(),
				Total DECIMAL(10,2) NOT NULL,
				Estado NVARCHAR(20) DEFAULT 'Pendiente'
				);
	PRINT 'Tabla : Pedidos fue creada correctamente.';
			END
		ELSE
			BEGIN
				PRINT 'La tabla [dbo].[Pedidos] ya existe.'
			END
		COMMIT TRANSACTION; -- CONFIRMAR CAMBIOS
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
GO

-- COMMIT Y ROLLBACK
BEGIN TRANSACTION;
	BEGIN TRY
		--VALIDACIONES
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'DetallesPedido' AND TABLE_SCHEMA = 'dbo')

			BEGIN 

			-- CREAR TABLA CON FOREIGN KEY
					CREATE TABLE DetallesPedido (
						DetalleID INT PRIMARY KEY IDENTITY(1,1),
						PedidoID INT FOREIGN KEY REFERENCES Pedidos(PedidoID),
						ProductoID INT FOREIGN KEY REFERENCES Productos(ProductoID),
						Cantidad INT NOT NULL,
						PrecioUnitario DECIMAL(10,2) NOT NULL
					);
					-- Agregar Foreign key a tabla existente
					ALTER TABLE Pedidos
					ADD CONSTRAINT FK_Pedidos_Clientes
					FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID);

	PRINT 'Tabla : DetallesPedido fue creada correctamente.'
				END
			ELSE
				BEGIN
					PRINT 'La tabla [dbo].[DetallesPedido] ya existe'
				END
			COMMIT TRANSACTION;
		END TRY
		BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
GO
---------------------


-- COMMIT Y ROLLBACK
BEGIN TRANSACTION;
	BEGIN TRY
		--VALIDACIONES
		IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AuditoriaProductos' AND TABLE_SCHEMA = 'dbo')

			BEGIN 

			-- CREAR TABLA CON FOREIGN KEY
					CREATE TABLE AuditoriaProductos (
						AuditoriaID INT PRIMARY KEY IDENTITY(1,1),
						ProductoID INT,
						Accion NVARCHAR(20),
						PrecioAnterior DECIMAL (10,2),
						PrecioNuevo DECIMAL(10,2),
						Usuario NVARCHAR(100),
						Fecha DATETIME DEFAULT GETDATE()
					);

	PRINT 'Tabla : AuditoriaProductos fue creada correctamente.'
				END
			ELSE
				BEGIN
					PRINT 'La tabla [dbo].[AuditoriaProductos] ya existe'
				END
			COMMIT TRANSACTION;
		END TRY
		BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		THROW;
	END CATCH
GO

