@echo off
call set-env-vars.bat

@echo off
set LISTEN_PORT=9999
@echo on

dotnet run --generate-razor-view true --controller %1 --view-names all