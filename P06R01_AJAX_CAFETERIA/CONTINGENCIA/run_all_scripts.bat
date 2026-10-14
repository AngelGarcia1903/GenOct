@echo off
rem Revierte (en orden inverso) todo lo que crean los scripts de la carpeta SCRIPTS
setlocal

rem Cambia SERVER por el nombre de tu servidor SQL si es otro equipo
set "SERVER=Angelgar"
set "AUTH=-E"
set "SQLCMD=sqlcmd"
set "SCRIPTS_DIR=%~dp0"

echo.
echo ==========================================
echo Contingencia A: Jobs
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_A_ContingenciaJobs.sql" -b
IF errorlevel 1 goto error

rem Los scripts B a F trabajan dentro de GenOct: si la BD ya no existe no hay nada que revertir
%SQLCMD% -S %SERVER% %AUTH% -C -d master -b -Q "IF DB_ID('GenOct') IS NULL RAISERROR('La base de datos GenOct no existe: no hay nada mas que revertir.',16,1)"
IF errorlevel 1 goto sinbd

echo.
echo ==========================================
echo Contingencia B: Triggers
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_B_ContingenciaTriggers.sql" -b
IF errorlevel 1 goto error

echo.
echo ==========================================
echo Contingencia C: Stored Procedures
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_C_StoredProcedure.sql" -b
IF errorlevel 1 goto error

echo.
echo ==========================================
echo Contingencia D: Vistas
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_D_Views.sql" -b
IF errorlevel 1 goto error

echo.
echo ==========================================
echo Contingencia E: Carga Inicial
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_E_InitialCharge.sql" -b
IF errorlevel 1 goto error

echo.
echo ==========================================
echo Contingencia F: Tablas
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_F_Contingencia_Create_Tables.sql" -b
IF errorlevel 1 goto error

echo.
echo ==========================================
echo Contingencia G: Base de Datos
echo ==========================================

%SQLCMD% -S %SERVER% %AUTH% -C -d master -i "%SCRIPTS_DIR%P01R01_BD_G_Contingencia_Base_De_Datos.sql" -b
IF errorlevel 1 goto error

echo.
echo ==========================================
echo La contingencia se ejecuto correctamente.
echo ==========================================
pause
goto end

:sinbd
echo.
echo ==========================================
echo GenOct no existe: se revirtieron solo los Jobs.
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
