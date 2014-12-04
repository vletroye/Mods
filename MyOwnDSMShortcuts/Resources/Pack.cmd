@echo off
if exist mods.spk (del mods.spk)
cd package
"../7z.exe" a -ttar ../package.tar *
if errorlevel 1 (
   cd ..
   echo Error while archiving package.
   exit /b 2
)
cd ..
7z.exe a -tgzip package.tgz package.tar
if errorlevel 1 (
   cd ..
   echo Error while compressing package
   exit /b 2
)
del package.tar
7z.exe a -ttar mods.spk package.tgz scripts Info PACKAGE_ICON*.PNG
if errorlevel 1 (
   cd ..
   echo Error while creating package.
   exit /b 2
)
del package.tgz