@echo off
if "%OS%" == "Windows_NT" setlocal
start "���������" ./jre7/bin/java -jar   -Dfile.encoding=UTF-8 -Xms256m -Xmx256m  agent.jar 
echo on