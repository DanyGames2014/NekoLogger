@echo off
cloc --by-file-by-lang --exclude-dir=bin,obj,Properties --exclude-ext=csproj,user,bat --quiet *
pause>nul