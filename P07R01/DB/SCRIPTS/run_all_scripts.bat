@echo off
rem this batch file runs all the scripts in the current folder
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

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P07R01_BD_01_CreateDatabase.sql" -b
IF errorlevel 1 goto error


echo.
echo ==========================================
echo Ejecutando Scripts 2: Crear Tablas
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P07R01_BD_02_CreataTable.sql" -b
IF errorlevel 1 goto error


echo.
echo ==========================================
echo Ejecutando Scripts 3: Carga Inicial
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P07R01_BD_03_CargaInicial.sql" -b
IF errorlevel 1 goto error


echo.
echo ==========================================
echo Ejecutando Scripts 4: Vistas
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P07R01_BD_04_Views.sql" -b
IF errorlevel 1 goto error


echo.
echo ==========================================
echo Ejecutando Scripts 5: Stored Procedures
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P07R01_BD_05_StoredProcedure.sql" -b
IF errorlevel 1 goto error


echo.
echo ==========================================
echo Ejecutando Scripts 6: Consultas Generales
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P07R01_BD_06_Consultas_Generales.sql" -b
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
