Volo.Abp.EntityFrameworkCore.MySQL

Volo.Abp.EntityFrameworkCore.MySQL

Volo.Abp.EntityFrameworkCore.PostgreSql



```
Install-Package Volo.Abp.EntityFrameworkCore.PostgreSql
```

​                options.UseSqlServer();

​         .UseMySql(configuration.GetConnectionString("Default"));



​                options.UseMySQL();

​                options.UsePostgreSql();

Add-Migration -Name InitialIdentityServerConfigurationDbMigration -Context ConfigurationDbContext  -OutputDir Migrations/IdentityServer/ConfigurationDb -Project SimpleCmsWithAbp.EntityFrameworkCore

Add-Migration -Name InitialIdentityServerConfigurationDbMigration -Context ConfigurationDbContext  -OutputDir Migrations/IdentityServer/ConfigurationDb -Project Fooww.Research.EntityFrameworkCore

```
Add-Migration Init
Update-Database
```

```js
 "ConnectionStrings": {
    "Default": "server=192.168.1.102;Database=research_home_vnext;Uid=research_home;Pwd=research_home@20190423;SslMode=none;Allow User Variables=True",
    "Default2": "Server=localhost;Database=Research;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
"ConnectionStrings2": {
    
    "Default": "User ID=postgres;Password=wsx1001;Host=localhost;Port=5432;Database=RQDDDCoreDb;Pooling=true;",
    "Default9": "server=192.168.1.102;Database=research_home2019;Uid=research_home;Pwd=research_home@20190423;SslMode=none;Allow User Variables=True",
    "DefaultConnection": "server=192.168.1.102;Database=research_home;Uid=fooww;Pwd=Fooww_08@2018;SslMode=none;Allow User Variables=True",
    "Default3": "server=127.0.0.1;port=33066;Database=DgSquare2019;Uid=root;Pwd=wsx1001;SslMode=none;Allow User Variables=True",
    "Default2": "Server=localhost; Database=ResearchDb; Trusted_Connection=True;"
  }



```





