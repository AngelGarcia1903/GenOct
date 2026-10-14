USE GenOctCCR
GO

/*
Proyecto P07R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20261007
Descripción: Consultas Generales
*/

--EXEC1
EXEC dbo.Insert_Camion
@Matricula = '4frfvce',
@TipoCamion = 'Tolva',
@Modelo = 142,
@Marca = 'JVN',
@Capacidad = 1200,
@Kilometraje = 90000,
@Disponibilidad = 1,
@UrlFoto = 'httpfpg//foto'
GO

--EXEC 2
EXEC dbo.Select_Camion
@Matricula='ICNRS';
GO


--EXEC 3
select * from Camiones

EXEC dbo.Update_Camion
@IdCamion = 3,
@Matricula = 'evrvr',
@TipoCamion = 'sedan',
@Modelo = 2018,
@Marca = 'VW',
@Capacidad = 800,
@Kilometraje = 120000,
@Disponibilidad = 1,
@UrlFoto = 'http/ecf';

--EXEC 4
EXEC dbo.Delete_Camion
@IdCamion = 4
GO

--EXEC 5
--select * from Choferes
EXEC dbo.Insert_Choferes
@Nombre = 'juan',
@ApPaterno = 'Gamaliel',
@ApMaterno = 'Zamarripa',
@Telefono = '23344334',
@Licencia = '3FCDC',
@UrlFoto = 'HTTP/DCDC',
@Disponibilidad = 1,
@FechaNacimiento = '19991012';

--EXEC 6
EXEC dbo.Select_Chofer;
--EXEC dbo.Select_Chofer @Disponibilidad = 1;
--EXEC dbo.Select_Chofer @Disponibilidad = 0;
GO


--EXEC 7
EXEC dbo.Update_Choferes
@IdChofer = 6,
@Nombre = 'Juan',
@ApPaterno = 'Gamaliel',
@ApMaterno = 'Zamarripa',
@Telefono = '4621234567',
@FechaNacimiento = '19991012',
@Licencia = '3FCDC',
@UrlFoto = 'HTTP/DCDC',
@Disponibilidad = 0;
GO

--EXEC 8
EXEC dbo.Delete_Chofer
@IdChofer = 5;
GO


--EXEC 9
EXEC dbo.Insert_Rutas
@IdChofer = 4,
@IdCamion = 1,
@Origen = 'Irapuato',
@Destino = 'Celaya',
@FechaSalida = '20261008 08:00',
@FechaLlegada = '20261008 10:30',
@ATiempo = 1,
@Distancia = 65.4;
GO



--EXEC 10
EXEC dbo.Select_Rutas;
--EXEC dbo.Select_Rutas @IdRuta = 1;
GO

--EXEC 11
EXEC dbo.Update_Rutas
@IdRuta = 1,
@IdChofer = 1,
@IdCamion = 3,
@Origen = 'Irapuato',
@Destino = 'Queretaro',
@FechaSalida = '20230423',
@FechaLlegada = '20230512',
@ATiempo = 0,
@Distancia = 160.2;
GO

--EXEC 12
EXEC dbo.Delete_Ruta
@IdRuta = 5;
GO

--EXEC 13
EXEC dbo.Select_Rutas_Detalle;
GO

--EXEC 14
EXEC dbo.Existe_Licencia
@Licencia = '3FCDC';
GO

--EXEC 15
EXEC dbo.Obtener_Camion_ID
@IdCamion = 1;
GO

--EXEC 16
EXEC dbo.Existe_Matricula
@Matricula = 'ICNRS';
GO

--EXEC 17
EXEC dbo.Listar_Camiones;
--EXEC dbo.Listar_Camiones @Disponibilidad = 1;
GO
--------------------------------------------------------------------------------
--VIEWS
SELECT 1 FROM VWChoferes;
SELECT 1 FROM VWCamiones;
SELECT 1 FROM VWRutas;


select  count(*) as Total_Registros, 'Choferes' as Tabla from Choferes union all
select  count(*) as Total_Registros, 'Camiones' as Tabla from Camiones union all
select  count(*) as Total_Registros, 'Rutas' as Tabla from Rutas

/*
sacar cada ruta con el nombre completo del chofer y la matricula del camion que lo realizo
*/

select 
r.IdRuta,
ch.Nombre,
ca.Matricula
from Rutas as r inner join Choferes as ch
on r.IdChofer = ch.IdChofer
inner join Camiones as ca on
r.IdCamion = r.ATiempo;

