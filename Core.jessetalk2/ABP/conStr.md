



```csharp
Add-Migration Init
Update-Database

Add-Migration -Name InitialIdentityServerConfigurationDbMigration -Context ConfigurationDbContext 
```





```json
    "Default": "server=192.168.1.102;Database=research_home_vnext;Uid=research_home;Pwd=research_home@20190423;SslMode=none;Allow User Variables=True",

    "Default3": "server=127.0.0.1;port=3366;Database=AbpIds4;Uid=root;Pwd=wsx1001;SslMode=none;Allow User Variables=True", 

{
  "ConnectionStrings": {
    "Default": "server=192.168.1.102;Database=research_home2019;Uid=research_home;Pwd=research_home@20190423;SslMode=none;Allow User Variables=True",
    "DefaultConnection": "server=192.168.1.102;Database=research_home;Uid=fooww;Pwd=Fooww_08@2018;SslMode=none;Allow User Variables=True",
    "Default3": "server=127.0.0.1;port=33066;Database=DgSquare2019;Uid=root;Pwd=wsx1001;SslMode=none;Allow User Variables=True",
    "Default2": "Server=localhost; Database=ResearchDb; Trusted_Connection=True;"
          "Default": "User ID=postgres;Password=wsx1001;Host=localhost;Port=5432;Database=PostgreSqlResDemoDb;Pooling=true;",

      
      
  },
  "App": {
    "ServerRootAddress": "http://localhost:21021/",
    "ClientRootAddress": "http://localhost:4200/",
    "CorsOrigins": "http://localhost:4200,http://localhost:8080,http://localhost:8081,http://localhost:3000"
  },
  "Authentication": {
    "JwtBearer": {
      "IsEnabled": "true",
      "SecurityKey": "Research_C421AAEE0D114E9C",
      "Issuer": "Research",
      "Audience": "Research"
    }
  }
}
```

