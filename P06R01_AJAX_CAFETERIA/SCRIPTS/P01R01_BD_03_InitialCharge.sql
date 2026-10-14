USE GenOct
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20260930
Descripción: Carga Inicial
Modificación: 20261012
  - Cada tabla se carga solo si está vacía (antes, ejecutar el script dos veces duplicaba todo).
  - Textos con prefijo N'' para conservar acentos y la ñ.
  - Teléfonos como texto (la columna es NVARCHAR).
  - Se agregan productos de Panaderia (los muestra la página Especiales del frontend).
  - Los detalles apuntan a pedidos que sí existen (antes PedidoID 6 rompía la llave foránea)
    y el Total de cada pedido es la suma de sus detalles con el precio real del producto.
*/

BEGIN TRANSACTION
BEGIN TRY

	IF NOT EXISTS (SELECT 1 FROM Productos)
	BEGIN
		INSERT INTO Productos(Nombre, Categoria, Precio, Stock)
		VALUES
		(N'Café Americano', N'Bebidas', 20.50, 10),     -- 1
		(N'Café Chico', N'Bebidas', 15.50, 10),         -- 2
		(N'Café Grande', N'Bebidas', 30.50, 10),        -- 3
		(N'Café Cappuccino', N'Bebidas', 25.50, 10),    -- 4
		(N'Café Moca', N'Bebidas', 25.50, 10),          -- 5
		(N'Café Moca Chico', N'Bebidas', 20.50, 10),    -- 6
		(N'Café con canela', N'Bebidas', 20.50, 10),    -- 7
		(N'Café con leche', N'Bebidas', 20.50, 10),     -- 8
		(N'Café tostado', N'Bebidas', 20.50, 10),       -- 9
		(N'Café de olla', N'Bebidas', 20.50, 10),       -- 10
		(N'Concha', N'Panaderia', 18.00, 15),           -- 11
		(N'Croissant', N'Panaderia', 28.00, 12),        -- 12
		(N'Pan de elote', N'Panaderia', 25.00, 10),     -- 13
		(N'Galleta de avena', N'Panaderia', 15.00, 20); -- 14
		PRINT 'Productos cargados';
	END
	ELSE
		PRINT 'Productos ya tiene registros, no se cargan de nuevo';

	IF NOT EXISTS (SELECT 1 FROM Clientes)
	BEGIN
		INSERT INTO Clientes(Nombre, Email, Telefono)
		VALUES
		(N'Juan', N'juan@gmail.com', N'4644567788'),
		(N'Rocío', N'rocio@gmail.com', N'4633447588'),
		(N'Ángel', N'angel@gmail.com', N'4673447588'),
		(N'Toño', N'tono@gmail.com', N'4635347588'),
		(N'Carlos', N'carlos@gmail.com', N'4633457588');
		PRINT 'Clientes cargados';
	END
	ELSE
		PRINT 'Clientes ya tiene registros, no se cargan de nuevo';

	IF NOT EXISTS (SELECT 1 FROM Pedidos)
	BEGIN
		INSERT INTO Pedidos(ClienteID, Total, Estado)
		VALUES
		(1, 41.00, N'Completo'),   -- 2 x Café Americano
		(2, 46.50, N'Completo'),   -- 3 x Café Chico
		(3, 61.50, N'Completo'),   -- 1 x Café Cappuccino + 2 x Concha
		(4, 51.00, N'Completo'),   -- 2 x Café Moca
		(5, 58.50, N'Pendiente');  -- 1 x Café Grande + 1 x Croissant
		PRINT 'Pedidos cargados';
	END
	ELSE
		PRINT 'Pedidos ya tiene registros, no se cargan de nuevo';

	IF NOT EXISTS (SELECT 1 FROM DetallesPedido)
	BEGIN
		INSERT INTO DetallesPedido(PedidoID, ProductoID, Cantidad, PrecioUnitario)
		VALUES
		(1, 1, 2, 20.50),
		(2, 2, 3, 15.50),
		(3, 4, 1, 25.50),
		(3, 11, 2, 18.00),
		(4, 5, 2, 25.50),
		(5, 3, 1, 30.50),
		(5, 12, 1, 28.00);
		PRINT 'DetallesPedido cargados';
	END
	ELSE
		PRINT 'DetallesPedido ya tiene registros, no se cargan de nuevo';

	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;
	THROW;
END CATCH
GO

--SELECT * FROM Productos;
