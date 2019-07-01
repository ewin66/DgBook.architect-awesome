```bash
info: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[0]
      User profile is available. Using 'C:\Users\Administrator\AppData\Local\ASP.NET\DataProtection-Keys' as key repository and Windows DPAPI to encrypt keys at rest.
info: Microsoft.EntityFrameworkCore.Infrastructure[10403]
      Entity Framework Core 2.2.1-servicing-10028 initialized 'ConfigurationDbContext' using provider 'Pomelo.EntityFrameworkCore.MySql' with options: MigrationsAssembly=Fooww.Research.EntityFrameworkCore
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (10ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='research_home2019' AND TABLE_NAME='__EFMigrationsHistory';
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='research_home2019' AND TABLE_NAME='__EFMigrationsHistory';
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT `MigrationId`, `ProductVersion`
      FROM `__EFMigrationsHistory`
      ORDER BY `MigrationId`;
info: Microsoft.EntityFrameworkCore.Migrations[20405]
      No migrations were applied. The database is already up to date.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
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
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
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
Content root path: D:\Fooww.Research\aspnet-core\src\Fooww.Research.Web.Host
Now listening on: http://localhost:21021
Application started. Press Ctrl+C to shut down.
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[1]
      Request starting HTTP/1.1 GET http://localhost:21021/
【【【【】】】】fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HLNRMSEI9UNK", Request id "0HLNRMSEI9UNK:00000001": An unhandled exception was thrown by the application.
System.InvalidOperationException: No authentication handler is registered for the scheme 'Bearer'. The registered schemes are: Identity.Application, Identity.External, Identity.TwoFactorRememberMe, Identity.TwoFactorUserId, idsrv, idsrv.external, JwtBearer. Did you forget to call AddAuthentication().Add[SomeAuthHandler]("Bearer",...)?


InvalidOperationException: No authentication handler is registered for the scheme 'Bearer'.
InvalidOperationException:没有为方案“承载者”注册任何身份验证处理程序。
The registered schemes are: Identity.
注册计划包括:身份证明。
Application, Identity.
应用程序的身份。
External, Identity.
外部,身份。
TwoFactorRememberMe, Identity.
TwoFactorRememberMe,身份。
TwoFactorUserId, idsrv, idsrv.
TwoFactorUserId、idsrv idsrv。
external, JwtBearer.
外部,JwtBearer。
Did you forget to call AddAuthentication().Add[SomeAuthHandler]("Bearer",...)?
您是否忘记调用AddAuthentication().Add[SomeAuthHandler](“肩负者”，…)?

   at Microsoft.AspNetCore.Authentication.AuthenticationService.AuthenticateAsync(HttpContext context, String scheme)
   at Fooww.Research.Authentication.JwtBearer.JwtTokenMiddleware.<>c__DisplayClass0_0.<<UseJwtTokenMiddleware>b__0>d.MoveNext() in D:\Fooww.Research\aspnet-core\src\Fooww.Research.Web.Core\Authentication\JwtBearer\JwtTokenMiddleware.cs:line 15
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware.Invoke(HttpContext context)
   at Abp.AspNetCore.Security.AbpSecurityHeadersMiddleware.Invoke(HttpContext httpContext) in D:\Github\aspnetboilerplate\src\Abp.AspNetCore\AspNetCore\Security\AbpSecurityHeadersMiddleware.cs:line 26
   at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpProtocol.ProcessRequests[TContext](IHttpApplication`1 application)
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[2]
      Request finished in 364.8662ms 500
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[1]
      Request starting HTTP/1.1 GET http://localhost:21021/favicon.ico
【】fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HLNRMSEI9UNK", Request id "0HLNRMSEI9UNK:00000002": An unhandled exception was thrown by the application.
System.InvalidOperationException: No authentication handler is registered for the scheme 'Bearer'. The registered schemes are: Identity.Application, Identity.External, Identity.TwoFactorRememberMe, Identity.TwoFactorUserId, idsrv, idsrv.external, JwtBearer. Did you forget to call AddAuthentication().Add[SomeAuthHandler]("Bearer",...)?
   at Microsoft.AspNetCore.Authentication.AuthenticationService.AuthenticateAsync(HttpContext context, String scheme)
   at Fooww.Research.Authentication.JwtBearer.JwtTokenMiddleware.<>c__DisplayClass0_0.<<UseJwtTokenMiddleware>b__0>d.MoveNext() in D:\Fooww.Research\aspnet-core\src\Fooww.Research.Web.Core\Authentication\JwtBearer\JwtTokenMiddleware.cs:line 15
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware.Invoke(HttpContext context)
   at Abp.AspNetCore.Security.AbpSecurityHeadersMiddleware.Invoke(HttpContext httpContext) in D:\Github\aspnetboilerplate\src\Abp.AspNetCore\AspNetCore\Security\AbpSecurityHeadersMiddleware.cs:line 26
   at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpProtocol.ProcessRequests[TContext](IHttpApplication`1 application)
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[2]
      Request finished in 70.7972ms 0

```

