@echo off

powershell Invoke-WebRequest -Uri "https://raw.githubusercontent.com/MichaelNichxls/PythonEmbed4Win/main/PythonEmbed4Win.ps1" -OutFile "%TEMP%\PythonEmbed4Win.ps1"
powershell -ExecutionPolicy Bypass -File "%TEMP%\PythonEmbed4Win.ps1" -Version 3.12.2 2> NUL
del "%TEMP%\PythonEmbed4Win.ps1"

cd "python-*-embed-*"
attrib +h /d "Lib\site-packages"
".\python.exe" -m pip install --no-warn-script-location setuptools
".\python.exe" -m pip install --no-warn-script-location -r "%~dp0\requirements.txt"
cd ..