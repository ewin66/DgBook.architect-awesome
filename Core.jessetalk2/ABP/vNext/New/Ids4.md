



```bash
info: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[0]
      User profile is available. Using 'C:\Users\Administrator\AppData\Local\ASP.NET\DataProtection-Keys' as key repository and Windows DPAPI to encrypt keys at rest.
info: IdentityServer4.Startup[0]
      Starting IdentityServer4 version 2.3.2.0
info: IdentityServer4.Startup[0]
      Using explicitly configured authentication scheme Cookie for IdentityServer
Hosting environment: Development
Content root path: D:\Fooww.Research\aspnet-core\src\Fooww.Research.Web.Host
Now listening on: http://localhost:21021
Application started. Press Ctrl+C to shut down.
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[1]
      Request starting HTTP/1.1 GET http://localhost:21021/
fail: Microsoft.AspNetCore.Server.Kestrel[13]
    
    
System.InvalidOperationException: No authentication handler is registered for the scheme 'Bearer'. The registered schemes are: Identity.Application, Identity.External, Identity.TwoFactorRememberMe, Identity.TwoFactorUserId, idsrv, idsrv.external, IdentityBearerIdentityServerAuthenticationJwt, IdentityBearerIdentityServerAuthenticationIntrospection, IdentityBearer, Cookie, JwtBearer. Did you forget to call AddAuthentication().Add[SomeAuthHandler]("Bearer",...)?
   at Microsoft.AspNetCore.Authentication.AuthenticationService.AuthenticateAsync(HttpContext context, String scheme)
   at Fooww.Research.Authentication.JwtBearer.JwtTokenMiddleware.<>c__DisplayClass0_0.<<UseJwtTokenMiddleware>b__0>d.MoveNext() in D:\Fooww.Research\aspnet-core\src\Fooww.Research.Web.Core\Authentication\JwtBearer\JwtTokenMiddleware.cs:line 15
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware.Invoke(HttpContext context)
   at Abp.AspNetCore.Security.AbpSecurityHeadersMiddleware.Invoke(HttpContext httpContext) in D:\Github\aspnetboilerplate\src\Abp.AspNetCore\AspNetCore\Security\AbpSecurityHeadersMiddleware.cs:line 26
   at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpProtocol.ProcessRequests[TContext](IHttpApplication`1 application)
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[2]
      Request finished in 714.1176ms 500
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[1]
      Request starting HTTP/1.1 GET http://localhost:21021/favicon.ico
fail: Microsoft.AspNetCore.Server.Kestrel[13]
      Connection id "0HLNRGOA5LMR2", Request id "0HLNRGOA5LMR2:00000002": An unhandled exception was thrown by the application.
System.InvalidOperationException: No authentication handler is registered for the scheme 'Bearer'. The registered schemes are: Identity.Application, Identity.External, Identity.TwoFactorRememberMe, Identity.TwoFactorUserId, idsrv, idsrv.external, IdentityBearerIdentityServerAuthenticationJwt, IdentityBearerIdentityServerAuthenticationIntrospection, IdentityBearer, Cookie, JwtBearer. Did you forget to call AddAuthentication().Add[SomeAuthHandler]("Bearer",...)?
   at Microsoft.AspNetCore.Authentication.AuthenticationService.AuthenticateAsync(HttpContext context, String scheme)
   at Fooww.Research.Authentication.JwtBearer.JwtTokenMiddleware.<>c__DisplayClass0_0.<<UseJwtTokenMiddleware>b__0>d.MoveNext() in D:\Fooww.Research\aspnet-core\src\Fooww.Research.Web.Core\Authentication\JwtBearer\JwtTokenMiddleware.cs:line 15
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware.Invoke(HttpContext context)
   at Abp.AspNetCore.Security.AbpSecurityHeadersMiddleware.Invoke(HttpContext httpContext) in D:\Github\aspnetboilerplate\src\Abp.AspNetCore\AspNetCore\Security\AbpSecurityHeadersMiddleware.cs:line 26
   at Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpProtocol.ProcessRequests[TContext](IHttpApplication`1 application)
info: Microsoft.AspNetCore.Hosting.Internal.WebHost[2]
      Request finished in 125.9464ms 0 
```

