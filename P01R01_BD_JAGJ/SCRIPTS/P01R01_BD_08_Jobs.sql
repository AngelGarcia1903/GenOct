USE msdb
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20261010
Descripción: Creacion de Jobs
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
	--@start_step_id = 1,
	--@owner_login_name = N'sa'; --Se puede cambiar el usuario sa si es necesario

--2  Crear el step (paso) que ejecuta el stored procedure
EXEC sp_add_jobstep
	@job_name = N'jExecSPGenOct1', --Nombre del job
	@step_name = N'Ejecutar_Mi_SP',
	@subsystem = N'TSQL',
	@command = N'EXEC [dbo].[spConsultarContenidoTablas] ', --comando que ejecuta el procedimiento almacenado
	@database_name = N'GenOct', --Reemplaza con tu base de datos
	--@on_success_action = 1 --continuar con el siguiente paso o terminar si es el unico
	@retry_attempts = 3,
	--@on_fail_action = 2; --parar el job si falla
		@retry_interval = 1
--3 Crear el schedule (Programacion) para que se ejecuta cada hora
EXEC sp_add_schedule
	--@job_name = N'jExecSPGenOct', --Nombre del job
	--@name = N'RespaldoCadaHora', --Nombre de la programacion
	@schedule_name = N'Schedule_Cada5MinGenOdc1',
	--@enabled = 1 --Habilitar la programcion
	@freq_type = 4, --frecuencia diaria
	@freq_interval = 1, --ejecutar todos los dias
	@freq_subday_type = 4, --frecuencia de horas
	@freq_subday_interval = 5, --ejecutar cada 5 minutos
	@active_start_time = 000000, --hora de inicio(formato HHMMSS), empieza a medianoche
	@active_end_time = 235959

--4 Agregar el job al servidor
EXEC sp_attach_schedule
	@job_name = N'jExecSPGenOct1',
	@schedule_name = N'Schedule_Cada5MinGenOdc1' --Cambua '(local) por el nombre de tu servidor si es necesario'

--5 Agregar el job al server para que el SQL server Agent lo ejecute
EXEC dbo.sp_add_jobserver
	@job_name = N'jExecSPGenOct1',
	@server_name = N'Angelgar'  --cambiar el nombre del servidor

END
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