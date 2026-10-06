@echo off
rem this batch file runs all the scripts in the current
setlocal

rem Get the current directory
set "SERVER=Angelgar"
set "AUTH=-E"
set "SQLCMD=sqlcmd"
set "SCRIPTS_DIR=%~dp0"

echo.
echo ==========================================
echo Ejecutando Scripts 1: Crear Base de Datos
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_A_ContingenciaJobs.sql" -b
IF errorlevel 1 goto error

rem Cambia por el nombre de la bd que crea tu script
set "DB=GenOct"

echo.
echo ==========================================
echo Ejecutando Scripts 2: Crear Tablas
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_B_ContingenciaTriggers.sql" -b

IF errorlevel 1 goto error


echo.
echo ==========================================
echo Ejecutando Scripts 3: Carga Inicial
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_C_StoredProcedures.sql" -b

IF errorlevel 1 goto error



echo.
echo ==========================================
echo Ejecutando Scripts 4: Carga Inicial
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_D_Views.sql"  -b

IF errorlevel 1 goto error



echo.
echo ==========================================
echo Ejecutando Scripts 5: Stored Procedures
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_E_InitialCharge.sql" -b

IF errorlevel 1 goto error



echo.
echo ==========================================
echo Ejecutando Scripts 7: Trigger
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_F_Contingencia_Create_Tables.sql" -b

IF errorlevel 1 goto error



echo.
echo ==========================================
echo Ejecutando Scripts 8: Jobs
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_G_Contingencia_Base_De_Datos.sql" -b

IF errorlevel 1 goto error


echo.
echo ==========================================
echo Los scripts se ejecutaron correctamente.
echo ==========================================
pause
goto end

:error
echo.
echo ==========================================
echo Error: Fallo en la ejecucion de los scripts.
echo Revisa el mensaje anterior para obtener mas detalles
echo ==========================================
pause

:end
endlocal