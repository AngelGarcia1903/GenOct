USE GenOct
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20260930
Descripción: Carga Inicial
*/

INSERT INTO Productos(Nombre, Categoria, Precio, Stock) 
VALUES
('Cafe Americano', 'Bebidas', 20.5, 10),
('Cafe Chico', 'Bebidas', 15.5, 10),
('Cafe Grande', 'Bebidas', 30.5, 10),
('Cafe Capuccino', 'Bebidas', 25.5, 10),
('Cafe Moca', 'Bebidas', 25.5, 10),
('Cafe Moca Chico', 'Bebidas', 20.5, 10),
('Cafe con canela', 'Bebidas', 20.5, 10),
('Cafe con leche', 'Bebidas', 20.5, 10),
('Cafe tostado', 'Bebidas', 20.5, 10),
('Cafe de olla', 'Bebidas', 20.5, 10);
GO


INSERT INTO Clientes(Nombre, Email, Telefono)
VALUES
('juan', 'juan@gmail.com', 4644567788),
('rocio', 'rocio@gmail.com', 4633447588),
('angel', 'angel@gmail.com', 4673447588),
('toño', 'toño@gmail.com', 4635347588),
('carlos', 'carlos@gmail.com', 4633457588);
GO

INSERT INTO Pedidos(ClienteID,Total, Estado)
VALUES
(1,125,'Completo'),
(2,225,'Completo'),
(3,325,'Completo'),
(4,425,'Completo'),
(5,525,'Incompleto');
GO

INSERT INTO DetallesPedido(PedidoID, ProductoID, Cantidad, PrecioUnitario)
VALUES 
(1, 1, 2, 20),
(2, 2, 3, 50),
(3, 3, 4, 56),
(5, 5, 2, 34),
(6, 6, 5, 34);
GO

--SELECT * FROM Productos;


