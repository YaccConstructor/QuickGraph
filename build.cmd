@echo off
cls

.paket\paket.bootstrapper.exe
if errorlevel 1 (
  exit /b %errorlevel%
)

.paket\paket.exe restore
if errorlevel 1 (
  exit /b %errorlevel%
)

IF NOT EXIST build.fsx (
  .paket\paket.exe update
  packages\FAKE\tools\FAKE.exe init.fsx
)

set PATH=C:\Program Files (x86)\MSBuild\12.0\Bin;%PATH%

packages\FAKE\tools\FAKE.exe build.fsx %*
