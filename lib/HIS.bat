@echo ��ʱ1�� ��ɾ��
ping 127.0.0.1
xcopy "%cd%/DownData" "%cd%" /s /h /d /y
HIS.exe
--@pause
@exit
