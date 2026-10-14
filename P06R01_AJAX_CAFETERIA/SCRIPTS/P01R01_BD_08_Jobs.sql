USE msdb
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20261010
Descripción: Creacion de Jobs
Modificación: 20261012
  - sp_add_jobserver usa el servidor local "(local)" en lugar del nombre fijo 'Angelgar',
    así el script funciona en cualquier equipo.
  - Comentarios corregidos: @freq_subday_type = 4 significa MINUTOS (no horas).
  Nota: el job solo corre si el servicio "Agente SQL Server" está iniciado.
*/

--CREACION DE JOBS
BEGIN TRANSACTION
	BEGIN TRY
IF NOT EXISTS (SELECT 1 FROM dbo.sysjobs WHERE name = 'jExecSPGenOct1')
BEGIN
--1 Crear el job
EXEC sp_add_job
	@job_name = 'jExecSPGenOct1', --nombre del job
	@enabled = 1, --habilitar el job
	@description = N'ejecuta el sp cada 5 minutos'

--2  Crear el step (paso) que ejecuta el stored procedure
EXEC sp_add_jobstep
	@job_name = N'jExecSPGenOct1', --Nombre del job
	@step_name = N'Ejecutar_Mi_SP',
	@subsystem = N'TSQL',
	@command = N'EXEC [dbo].[spConsultarContenidoTablas] ', --comando que ejecuta el procedimiento almacenado
	@database_name = N'GenOct',
	@retry_attempts = 3,
	@retry_interval = 1

--3 Crear el schedule (Programacion) para que se ejecute cada 5 minutos
EXEC sp_add_schedule
	@schedule_name = N'Schedule_Cada5MinGenOdc1',
	@freq_type = 4, --frecuencia diaria
	@freq_interval = 1, --ejecutar todos los dias
	@freq_subday_type = 4, --unidad: minutos
	@freq_subday_interval = 5, --cada 5 minutos
	@active_start_time = 000000, --hora de inicio (HHMMSS), empieza a medianoche
	@active_end_time = 235959

--4 Ligar el schedule al job
EXEC sp_attach_schedule
	@job_name = N'jExecSPGenOct1',
	@schedule_name = N'Schedule_Cada5MinGenOdc1'

--5 Agregar el job al servidor local para que el SQL Server Agent lo ejecute
EXEC dbo.sp_add_jobserver
	@job_name = N'jExecSPGenOct1',
	@server_name = N'(local)'

PRINT 'Job jExecSPGenOct1 creado';
END
ELSE
	PRINT 'El job jExecSPGenOct1 ya existe';
COMMIT TRANSACTION

END TRY
BEGIN CATCH
IF @@TRANCOUNT > 0
	BEGIN
		ROLLBACK TRANSACTION;
	END
	PRINT ERROR_MESSAGE();

END CATCH
GO
