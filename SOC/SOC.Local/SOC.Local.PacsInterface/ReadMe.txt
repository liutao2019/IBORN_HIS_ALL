5.0项目Pacs接口
查询数据：pacs数据库patientinfo表
文件结构树：带*文件从pacs客户端下复制
│  HIS.exe
│  Connection.dll*
│  Connection.ini*
│  CryptoWrapper.dll*
│  data.xml*
│  joint.dll*
│  joint.ini
│  LNCA_MS_Crypto.dll*
│  md5codec.dll*
│  PACSID.dll*
│  PacsLog.dll*
│  PacsQueryDBAccess.dll*
│  PacsStringRes.dll*
│  PacsUI.dll*
│  RISLogin.dll*
│  SOC.Local.PacsInterface.dll
│  XMLServer.dll*
│  XMLServer.ini*
├─PacsClient
│  │  
│  │  Display.exe
│  │  

joint.ini内容
[app]
run=.\PacsClient\Display.exe

[Login]
username=***
password=***

