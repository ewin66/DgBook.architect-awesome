info: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[0]
      User profile is available. Using 'C:\Users\PowerDg\AppData\Local\ASP.NET\DataProtection-Keys' as key repository and Windows DPAPI to encrypt keys at rest.
info: Microsoft.EntityFrameworkCore.Infrastructure[10403]
      Entity Framework Core 2.2.1-servicing-10028 initialized 'ConfigurationDbContext' using provider 'Pomelo.EntityFrameworkCore.MySql' with options: MigrationsAssembly=Fooww.Research.EntityFrameworkCore
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (17ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='AbpRRIds4' AND TABLE_NAME='__EFMigrationsHistory';
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='AbpRRIds4' AND TABLE_NAME='__EFMigrationsHistory';
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT `MigrationId`, `ProductVersion`
      FROM `__EFMigrationsHistory`
      ORDER BY `MigrationId`;
info: Microsoft.EntityFrameworkCore.Migrations[20405]
      No migrations were applied. The database is already up to date.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM `ApiResources` AS `a`)
          THEN TRUE ELSE FALSE
      END
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM `IdentityResources` AS `i`)
          THEN TRUE ELSE FALSE
      END
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (5ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT CASE
          WHEN EXISTS (
              SELECT 1
              FROM `Clients` AS `c`)
          THEN TRUE ELSE FALSE
      END
info: IdentityServer4.Startup[0]
      Starting IdentityServer4 version 2.4.0.0
info: IdentityServer4.Startup[0]
      Using the default authentication scheme JwtBearer for IdentityServer
fail: IdentityServer4.Startup[0]
      Authentication scheme JwtBearer is configured for IdentityServer, but it is not a scheme that supports signin (like cookies). Either configure the default authentication scheme with cookies or set the CookieAuthenticationScheme on the IdentityServerOptions.
Hosting environment: Development
Content root path: C:\github.PowerDG\The-Thousand-and-One-Home\Fooww.Research01\aspnet-core\src\Fooww.Research.Web.Host
Now listening on: http://localhost:21021
Application started. Press Ctrl+C to shut down.

C:\Program Files\dotnet\dotnet.exe (进程 30648)已退出，返回代码为: -1。
若要在调试停止时自动关闭控制台，请启用“工具”->“选项”->“调试”->“调试停止时自动关闭控制台”。
按任意键关闭此窗口...
