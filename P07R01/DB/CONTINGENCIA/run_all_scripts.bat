@echo off
rem this batch file runs all the contingency scripts in the current folder
setlocal

rem Get the current directory
set "SERVER=Angelgar"
set "AUTH=-E"
set "SQLCMD=sqlcmd"
set "SCRIPTS_DIR=%~dp0"

echo.
echo ==========================================
echo Ejecutando Scripts 1: Contingencia Stored Procedures
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P07R01_BD_A_SP.sql" -b
IF errorlevel 1 goto error


echo.
echo ==========================================
echo Ejecutando Scripts 2: Contingencia Vistas
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P07R01_BD_B_Views.sql" -b
IF errorlevel 1 goto error


echo.
echo ==========================================
echo Ejecutando Scripts 3: Contingencia Carga Inicial
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P07R01_BD_C_CargaInicial.sql" -b
IF errorlevel 1 goto error


echo.
echo ==========================================
echo Ejecutando Scripts 4: Contingencia Tablas
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P07R01_BD_D_Tables.sql" -b
IF errorlevel 1 goto error


echo.
echo ==========================================
echo Ejecutando Scripts 5: Contingencia Base de Datos
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P07R01_BD_E_DataBase.sql" -b
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
