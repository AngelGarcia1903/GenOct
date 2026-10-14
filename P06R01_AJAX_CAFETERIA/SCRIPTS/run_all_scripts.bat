@echo off
rem Ejecuta en orden todos los scripts de creacion de la BD GenOct
setlocal

rem Cambia SERVER por el nombre de tu servidor SQL si es otro equipo
set "SERVER=Angelgar"
set "AUTH=-E"
set "SQLCMD=sqlcmd"
set "SCRIPTS_DIR=%~dp0"

echo.
echo ==========================================
echo Ejecutando Script 1: Crear Base de Datos
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_01_CreateDatabase.sql" -b
IF errorlevel 1 goto error

echo.
echo ==========================================
echo Ejecutando Script 2: Crear Tablas
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_02_CreateTables.sql" -b
IF errorlevel 1 goto error

echo.
echo ==========================================
echo Ejecutando Script 3: Carga Inicial
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_03_InitialCharge.sql" -b
IF errorlevel 1 goto error

echo.
echo ==========================================
echo Ejecutando Script 4: Vistas
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_04_Views.sql" -b
IF errorlevel 1 goto error

echo.
echo ==========================================
echo Ejecutando Script 5: Stored Procedures
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_05_Stored_Procedure.sql" -b
IF errorlevel 1 goto error

echo.
echo ==========================================
echo Ejecutando Script 7: Triggers
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_07_Trigger.sql" -b
IF errorlevel 1 goto error

echo.
echo ==========================================
echo Ejecutando Script 8: Jobs
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_08_Jobs.sql" -b
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
