



##	MariaDB/Mysql

https://mariadb.org/

https://downloads.mariadb.org/mariadb/



```
Add-Migration Init
Update-Database
```



Mysql8 [优化](https://dev.mysql.com/doc/refman/8.0/en/optimization.html)

https://dev.mysql.com/doc/

在EntityFrameworkCore项目中添加 
Install-Package Pomelo.EntityFrameworkCore.MySql	和 
Install-Package Pomelo.EntityFrameworkCore.MySql.Design		两个包，以便支持MySql

Install-Package Microsoft.EntityFrameworkCore.Tools

```sql
show variables like 'character_set_database';
--检查您的DB字符集
```



数据库所选的字符集为utf8_general_ci。虽然Pomelo建议选择utf8mb4作为字符集（详细信息请参阅Pomelo《[Getting Started](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql#getting-started)》），但建议不要选择

MySql的驱动准备好以后，需要修改数据库链接，打开Migrations项目和Web.Host项目中的appsettings.json文件，将ConnectionStrings的Default值修改为“Server=localhost;database=simplecmswithabp;uid=root;pwd=abcd-1234;charset=UTF8;”。在生产环境中，不要使用root作为数据库的连接用户，这个大家应该都懂的。

---------------------
作者：上将军 
来源：CSDN 
原文：https://blog.csdn.net/tianxiaode/article/details/78816876 
版权声明：本文为博主原创文章，转载请附上博文链接！

```csharp
// builder.UseMySql(connectionString);      

    public static class SimpleCmsWithAbpDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<SimpleCmsWithAbpDbContext> builder, string connectionString)
        {
            //builder.UseSqlServer(connectionString);
            builder.UseMySql(connectionString);           
        }

        public static void Configure(DbContextOptionsBuilder<SimpleCmsWithAbpDbContext> builder, DbConnection connection)
        {
            //builder.UseSqlServer(connection);
            builder.UseMySql(connection);
        }
    }



```



```
Add-Migration Init
Update-Database
```





##	PostgreSQL

Install-Package Npgsql.EntityFrameworkCore.PostgreSQL
Install-Package Npgsql.EntityFrameworkCore.PostgreSQL.Design

Install-Package Microsoft.EntityFrameworkCore.Tools

管理包控制台对应EF所在项目

启动项对应配置文件启动项目

工具 - > NuGet软件包管理器 - >软件包管理器控制台 
//创建模型的初始表 
**Add-Migration InitialCreate** 
//将新迁移应用于数据库 
**Update-Database**

```
Add-Migration Init
Update-Database
```

```
    "Default": "User ID=postgres;Password=wsx1001;Host=localhost;Port=5432;Database=PostgreSqlResDemoDb;Pooling=true;",

```





## ConDefault

````json

{
    //quadri	clover
  "ConnectionStrings": { 
    //"Default": "server=127.0.0.1;port=3366;Database=IdentityServerDemoDb;Uid=root;Pwd=wsx1001;SslMode=none;Allow User Variables=True", 
    "DefaultPostgres": "User ID=postgres;Password=wsx1001;Host=localhost;Port=5432;Database=IdentityServerDemoDb;Pooling=true;",
    "Default010": "User ID=postgres;Password=wsx1001;Host=localhost;Port=5432;Database=IdentityServerDemoDb;Pooling=true;",

    //"Default": "server=192.168.1.102;Database=IdentityServerDemoDb;Uid=research_home;Pwd=research_home@20190423;SslMode=none;Allow User Variables=True",
    "DefaultConnection": "server=192.168.1.102;Database=research_home;Uid=fooww;Pwd=Fooww_08@2018;SslMode=none;Allow User Variables=True",
    "Default0": "server=192.168.1.102;Database=IdentityServerDemoDb;Uid=research_home;Pwd=research_home@20190423;SslMode=none;Allow User Variables=True",
    "Default": "server=127.0.0.1;port=33068;Database=researc2314entityServer;Uid=root;Pwd=wsx1001;SslMode=none;Allow User Variables=True",

    //"Default8": "server=127.0.0.1;port=3366;Database=AbpAuthIds4;Uid=root;Pwd=wsx1001;SslMode=none;Allow User Variables=True",
    //"DefaultConnection": "server=192.168.1.102;Database=research_home;Uid=fooww;Pwd=Fooww_08@2018;SslMode=none;Allow User Variables=True", 
    //"Default02": "Server=localhost; Database=ResearchDb; Trusted_Connection=True;",  
    "DefaultRQ": "Server=10.0.75.1; Database=AbpZeroTemplateDb; User=sa; Password=123qwe;",
    "Default3366": "server=127.0.0.1;port=3366;Database=IdentityServerDemoDb;Uid=root;Pwd=wsx1001;SslMode=none;Allow User Variables=True",
    "Default33066": "server=127.0.0.1;port=33066;Database=DgSquare2019;Uid=root;Pwd=wsx1001;SslMode=none;Allow User Variables=True",
    "DefaultConStr": "Server=localhost; Database=ResearchDb; Trusted_Connection=True;",
    "Default4Demo": "Server=localhost; Database=IdentityServerDemoDb; Trusted_Connection=True;"

  },
  "Authentication": {
    "Facebook": {
      "IsEnabled": "false",
      "AppId": "",
      "AppSecret": ""
    },
    "Google": {
      "IsEnabled": "false",
      "ClientId": "",
      "ClientSecret": ""
    },
    "JwtBearer": {
      "IsEnabled": "true",
      "SecurityKey": "IdentityServerDemo_C421AAEE0D114E9C",
      "Issuer": "IdentityServerDemo",
      "Audience": "IdentityServerDemo"
    }
  },
  "IdentityServer": {
    "IsEnabled": "true" ,
    "Authority": "http://localhost:62114",
    "ApiName": "default-api",
    "ApiSecret": "secret",
    "Clients": [
      {
        "ClientId": "client",
        "AllowedGrantTypes": [
          "password"
        ],
        "ClientSecrets": [
          {
            "Value": "def2edf7-5d42-4edc-a84a-30136c340e13"
          }
        ],
        "AllowedScopes": [
          "default-api"
        ]
      }
    ]
  },
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  }
}




````

