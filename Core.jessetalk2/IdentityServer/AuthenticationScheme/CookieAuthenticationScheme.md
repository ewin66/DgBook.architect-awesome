

# IdentityServer4 CookieAuthenticationScheme设置

IdentityServer4 [CookieAuthenticationScheme设置](https://blog.csdn.net/orderloh/article/details/91446290)

    在使用IdentityServer4时报错，错误如下：
    
    ERROR|IdentityServer4.Startup|Authentication scheme Bearer is configured for IdentityServer,
     but it is not a scheme that supports signin (like cookies). Either configure the default 
     authentication scheme with cookies or set the CookieAuthenticationScheme on the 
     IdentityServerOptions.

解决办法：
1、在Startup.cs中的services.AddIdentityServer添加如下代码。



    services.AddIdentityServer(options=>options.Authentication.CookieAuthenticationScheme="Cookie")



```csharp
services.AddIdentityServer(options=>options.Authentication.CookieAuthenticationScheme="Cookie")
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Resources.GetIdentityResourceResources())
                .AddInMemoryApiResources(Resources.GetApiResources())
                .AddInMemoryClients(Clients.GetClients())
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();
```

2、在services.AddAuthentication中添加AddCookie(“Cookie”);

```csharp
services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = $"http://{Configuration["Identity:IP"]}:{Configuration["Identity:Port"]}";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "WebApi";
                    options.ApiSecret = "DAWebApi";
      }).AddCookie("Cookie");
```





作者：orderloh 
来源：CSDN 
原文：https://blog.csdn.net/orderloh/article/details/91446290 
版权声明：本文为博主原创文章，转载请附上博文链接！



---



##	其他

AspNetCore.Authentication.[JwtBearer失败](http://www.it1352.com/308496.html)，没有可用于使用.NET的核心RC2令牌SecurityTokenValidator(AspNetCore.Authentication.JwtBearer
fails with No SecurityTokenValidator available for token with .net core
RC2)









