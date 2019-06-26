



PS C:\WINDOWS\system32> cd C:\microDg\consul_1.5.1_windows_amd64

PS C:\microDg\consul_1.5.1_windows_amd64> .\consul.exe agent -dev



PS C:\WINDOWS\system32> cd C:\dgMicro





这是开发环境测试,生产环境要建集群,要至少一台Server,多台Agent consul

监控页面http://127.0.0.1:8500/consult



```
dotnet WebConsul.dll --ip 127.0.0.1 --port 5001
dotnet WebConsul.dll --ip 127.0.0.1 --port 5002
```





```bash


System.AggregateException
  HResult=0x80131500
  Message=One or more errors occurred. (An error occurred while sending the request.)
  Source=System.Private.CoreLib
  StackTrace:
   at System.Threading.Tasks.Task`1.GetResultCore(Boolean waitCompletionNotification)
   at RestTest.Program.Main(String[] args) in E:\GitHub\github.PowerDG\DDDgP2018\4.6.0\aspnet-core\dgOcelot\MicroDg\RestTest\Program.cs:line 12

内部异常 1:
HttpRequestException: An error occurred while sending the request.

内部异常 2:
  HResult=0x80131500
----



System.AggregateException
  HResult=0x80131500
  Message=One or more errors occurred. (An error occurred while sending the request.)
  Source=System.Private.CoreLib
  StackTrace:
   at System.Threading.Tasks.Task`1.GetResultCore(Boolean waitCompletionNotification)
   at RestTest.Program.Main(String[] args) in E:\GitHub\github.PowerDG\DDDgP2018\4.6.0\aspnet-core\dgOcelot\MicroDg\RestTest\Program.cs:line 12

内部异常 1:
HttpRequestException: An error occurred while sending the request.

内部异常 2:
IOException: The server returned an invalid or unrecognized response.

```

