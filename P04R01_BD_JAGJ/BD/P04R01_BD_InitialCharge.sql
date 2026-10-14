USE Thona_PT
GO
/*
Proyecto P0R01_BD_JAGJ
Autor: Jose Angel Garcia Juarez
Fecha: 20261005
Descripción: Insercion de datos
*/


INSERT INTO TBLUsuarios(nombre, hobby, fechanacimiento, email)
VALUES 
('Jose', 'correr en los parques', GETDATE(), 'jose@gmail.com'),
('juan', 'ir a dar un paseo con el perro', GETDATE(), 'juan@gmail.com'),
('angel', 'salir a fiestas', GETDATE(), 'angel@gmail.com'),
('antonio', 'ver la tele ', GETDATE(), 'antonio@gmail.com'),
('daniel', 'nadar en lagos', GETDATE(), 'daniel@gmail.com');


select * from TBLUsuarios

exec sp_help 'TBLUsuarios'