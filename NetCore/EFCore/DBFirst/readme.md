

# [一步步学习EF Core(1.DBFirst)](https://www.cnblogs.com/GuZhenYin/p/6857413.html)

在VS的**工具**选项中,选择**NuGet包管理器,**选择**程序包管理控制台**

```csharp
Scaffold-DbContext "这里输入你的数据库连接字符串" Microsoft.EntityFrameworkCore.SqlServer
```

```json
  "ConnectionStrings": {
    "SchoolConnection": "Data Source=.;Initial Catalog=School_Test;User ID=**;Password=***;MultipleActiveResultSets=true"
  }
```



https://www.cnblogs.com/GuZhenYin/p/6857413.html

---



Pomelo.EntityFrameworkCore.MySql	和 
Pomelo.EntityFrameworkCore.MySql.Design		两个包，以便支持MySql

Install-Package Microsoft.EntityFrameworkCore.Tools



```csharp
dotnet ef dbcontext scaffold "Server=localhost;User Id=test;Password=123456;Database=TestDb" "Pomelo.EntityFrameworkCore.MySql"
```

dotnet ef dbcontext scaffold "server=192.168.1.102;Database=research_home;Uid=fooww;Pwd=Fooww_08@2018;SslMode=none;Allow User Variables=True" "Pomelo.EntityFrameworkCore.MySql"



Package控制台

```
Scaffold-DbContext "server=192.168.1.102;Database=research_home;Uid=fooww;Pwd=Fooww_08@2018;SslMode=none;Allow User Variables=True" "Pomelo.EntityFrameworkCore.MySql"
```



```csharp
  "ConnectionStrings": {
    "DefaultConnection": "server=192.168.1.102;Database=research_home;Uid=fooww;Pwd=Fooww_08@2018;SslMode=none;Allow User Variables=True"
  },
```



