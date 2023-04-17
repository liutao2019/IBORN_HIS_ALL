@echo off
if "%OS%" == "Windows_NT" setlocal
start "代理服务器" ./jre7/bin/java -jar   -Dfile.encoding=UTF-8 -Xms256m -Xmx256m  agent.jar 
echo on