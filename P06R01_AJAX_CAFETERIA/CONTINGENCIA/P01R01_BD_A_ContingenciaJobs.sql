USE msdb
GO
/*
Proyecto P01R01_BD_JAGJ
Auto: Jose Angel Garcia Juarez
Fecha: 20261020
Descripción: Contingencia de Jobs
*/

IF EXISTS (SELECT 1 FROM dbo.sysjobs WHERE name = 'jExecSPGenOct1') 
BEGIN
	EXEC dbo.sp_delete_job
	@job_name = N'jExecSPGenOct1',
	@delete_unused_schedule = 1 --;
	--@force_delete = 1;
PRINT 'Job y sus recursos asociados eliminados';

--borrar duplicados
DECLARE @SCHEDULE_ID INT;
DECLARE cur CURSOR FOR
SELECT SCHEDULE_ID FROM msdb.dbo.sysschedules WHERE name = N'Schedule_Cada5MinGenOdc1';
OPEN cur;
FETCH NEXT FROM cur INTO @SCHEDULE_ID;
WHILE @@FETCH_STATUS = 0
BEGIN
	EXEC dbo.sp_delete_schedule @SCHEDULE_ID = @SCHEDULE_ID,
	@FORCE_DELETE = 1
	FETCH NEXT FROM cur INTO @SCHEDULE_ID;

END
CLOSE cur
	DEALLOCATE cur;
	PRINT 'Se borraron duplicates'
END
GO
