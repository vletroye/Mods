@echo off
for %%a in (./*.spk) do set spk=%%~na
echo %spk%

7z.exe x -ttar  %spk%.spk
if errorlevel 1 (
   echo Error extracting spk.
   exit /b 2
)

7z.exe x -tgzip package.tgz
if errorlevel 1 (
   echo Error while extracting package.
   exit /b 2
)
del package.tgz

7z.exe x -ttar package.tar -opackage
if errorlevel 1 (
   echo Error while extracting filed.
   exit /b 2
)
del package.tar