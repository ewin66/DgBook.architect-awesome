



# 	[IdentityServer4之Implicit(隐式许可)](https://www.cnblogs.com/ddrsql/p/7922451.html)

 

### **参考**

官方文档：[3_interactive_login](http://docs.identityserver.io/en/release/quickstarts/3_interactive_login.html) 、[7_javascript_client](http://docs.identityserver.io/en/release/quickstarts/7_javascript_client.html)

概念：[隐式许可](https://www.cnblogs.com/ddrsql/p/7789064.html#Implicit)

 

### **认证服务端配置**

**认证服务ApiResource配置**

```
new ApiResource("api1", "api项目 一")
{
    ApiSecrets = { new Secret("api1pwd".Sha256()) }
},
```

**认证服务Client配置**

[![复制代码](assets/copycode.gif)](javascript:void(0);)

```csharp
// OpenID Connect implicit flow client (MVC)
new Client
{
    ClientId = "mvc",
    ClientName = "MVC Client",
    AllowedGrantTypes = GrantTypes.Implicit,
    AllowAccessTokensViaBrowser = true,
    AccessTokenType = AccessTokenType.Reference,

    RedirectUris ={
        "http://localhost:5002/signin-oidc",
        "http://www.implicit.com/signin-oidc"
    },
    PostLogoutRedirectUris ={
        "http://localhost:5002/signout-callback-oidc",
        "http://www.implicit.com/signout-callback-oidc"
    },

    AllowedScopes =
    {
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Profile,
        "api1","api2"
    }
},
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**认证服务Startup配置**

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();

    // configure identity server with in-memory stores, keys, clients and scopes
    services.AddIdentityServer()
        .AddDeveloperSigningCredential()
        .AddInMemoryIdentityResources(Config.GetIdentityResources())
    	.AddInMemoryApiResources(Config.GetApiResources())//认证服务ApiResource配置
    	.AddInMemoryClients(Config.GetClients());//认证服务Client配置
        .AddTestUsers(Config.GetUsers());
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

### 资源服务Api配置

**资源服务器Startup配置**

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```csharp
services.AddMvcCore()
    .AddAuthorization()
    .AddJsonFormatters();

services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication(options =>
    {
        options.Authority = "http://localhost:5000";
        options.RequireHttpsMetadata = false;

        options.ApiName = "api1";  
        options.ApiSecret = "api1pwd";  //对应ApiResources中的密钥
    });
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

添加接口

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```csharp
[Route("[controller]")]
[Authorize]
public class IdentityController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var info = from c in User.Claims select new { c.Type, c.Value };
        var list = info.ToList();
        list.Add(new { Type = "api1返回", Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") });
        return new JsonResult(list);
    }
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

### Client客户端

（A）客户端通过向授权端点引导资源拥有者的用户代理开始流程。客户端包括它的客户端标识、请求范围、本地状态和重定向URI，一旦访问被许可（或拒绝）授权服务器将传送用户代理回到该URI。
 （B）授权服务器验证资源拥有者的身份（通过用户代理），并确定资源拥有者是否授予或拒绝客户端的访问请求。
 （C）假设资源拥有者许可访问，授权服务器使用之前（在请求时或客户端注册时）提供的重定向URI重定向用户代理回到客户端。重定向URI在URI片段中包含访问令牌。
 （D）用户代理顺着重定向指示向Web托管的客户端资源发起请求。用户代理在本地保留片段信息。
 （E）Web托管的客户端资源返回一个网页（通常是带有嵌入式脚本的HTML文档），该网页能够访问包含用户代理保留的片段的完整重定向URI并提取包含在片段中的访问令牌（和其他参数）。
 （F）用户代理在本地执行Web托管的客户端资源提供的提取访问令牌的脚本。
 （G）用户代理传送访问令牌给客户端。

**1、JS Client**

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```csharp
// JavaScript Client
new Client
{
    ClientId = "js",
    ClientName = "JavaScript Client",
    AllowedGrantTypes = GrantTypes.Implicit,
    AllowAccessTokensViaBrowser = true, //允许通过浏览器传输token

    RedirectUris =           { "http://localhost:5009/callback.html" },
    PostLogoutRedirectUris = { "http://localhost:5009/index.html" },
    AllowedCorsOrigins =     { "http://localhost:5009" },

    AllowedScopes =
    {
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Profile,
        "api1"
    }
},
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

js client的静态资源。

![img](assets/355798-20171129201241651-901964796.png)

 

 

**2、MVC Client**

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```csharp
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.SignInScheme = "Cookies";

        options.Authority = "http://localhost:5000";
        options.RequireHttpsMetadata = false;

        options.ClientId = "mvc";
        options.ResponseType = "id_token token";
        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.Scope.Add("api1");
    });
```

[![复制代码](assets/copycode.gif)](javascript:void(0);)

需要身份授权的页面

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```csharp
[Authorize]
public IActionResult Secure()
{
    ViewData["Message"] = "Secure page.";

    return View();
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

调用Api

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```csharp
public async Task<IActionResult> CallApiUsingUserAccessToken()
{
    var accessToken = await HttpContext.GetTokenAsync("access_token");

    var client = new HttpClient();
    client.SetBearerToken(accessToken);
    var content = await client.GetStringAsync("http://localhost:5001/identity");

    ViewBag.Json = JArray.Parse(content).ToString();
    return View("json");
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

### 获取token过程解析

**Reference形式获取access_token**

**1、前后端分离形式认证**

隐式许可没有获取Authorization Code的过程。

![img](assets/355798-20171129194805979-231754914.png)

4和5是是用户登录（资源拥有者授权）。

2和3为Get请求参数如下。

![img](assets/355798-20171129195311526-1439920403.png)

使用工具请求实际得到的是一个js Web-Hosted Client Resource返回的静态脚本资源 。

![img](assets/355798-20171129195136995-824978992.png)

304的回调 # 后的信息不会经过js client 的服务器，直接在浏览器解析。

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
http://localhost:5009/callback.html#
id_token=eyJhbGciOiJSUzI1NiIsImtpZCI6IjAzMTZkNWQwYTViNTgyYzc0NmJlMmVhZWU1MTg1NzhiIiwidHlwIjoiSldUIn0.eyJuYmYiOjE1MTE5NTY1NTMsImV4cCI6MTUxMTk1Njg1MywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoianMiLCJub25jZSI6ImEyOGY5MmRiMjVmNzRkYjQ5YTRhMzU3YTY0YzA2OTU2IiwiaWF0IjoxNTExOTU2NTUzLCJhdF9oYXNoIjoiUldXMU9pTHNzYmVHek1UaHBkbE1xdyIsInNpZCI6ImZiODI5MzQzYWRjN2U4MjUxMjk1OTE1NDBmZDc3ZjBhIiwic3ViIjoiMSIsImF1dGhfdGltZSI6MTUxMTk1NjU1MywiaWRwIjoibG9jYWwiLCJhbXIiOlsicHdkIl19.av1-FqeVdobrPl1QxrIndjpzwTHgoJTclx2xwaOexDapbi-yYe8uOIBQG3hR4B4juM3nBm5GFp0873Yn_ReUD4YAxKlBJuoVxzMVIxSVB2H7RZH5wiMCktY8rgWAV4_lSfDZG1B3n4Ged8gj80b5vlt6rKs48woJiaeDemtLs0EY6lqomENy0MY1lu2TkVco_AyB_ha6jJsLXgCK8U9ttN6862h0HlvDy7KqRpTqfstkcT1JIUkooX3JfUNwru4y38HIxs55_dA9Pq8hfBK--Y7_fRW2_vnDz99Bn0kRcQcfgHXHxcovtNU1MRdFataRtjP6QyC26DyXLcr5d8_QCw&
access_token=eyJhbGciOiJSUzI1NiIsImtpZCI6IjAzMTZkNWQwYTViNTgyYzc0NmJlMmVhZWU1MTg1NzhiIiwidHlwIjoiSldUIn0.eyJuYmYiOjE1MTE5NTY1NTMsImV4cCI6MTUxMTk2MDE1MywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC9yZXNvdXJjZXMiLCJhcGkxIl0sImNsaWVudF9pZCI6ImpzIiwic3ViIjoiMSIsImF1dGhfdGltZSI6MTUxMTk1NjU1MywiaWRwIjoibG9jYWwiLCJzY29wZSI6WyJvcGVuaWQiLCJwcm9maWxlIiwiYXBpMSJdLCJhbXIiOlsicHdkIl19.s1lYjwnuj6PKYwUpKj7CNFhKPEBFqbI51UT8okg1G8d73AU1Tx1KuZpkulxeAbhIqD807TiY_A9-h1shSNTAXOhlcSGR6hZiMHcAU50nFvkNTq16j0wjeGtF5i5xlNJDwvq77cDs0rWnge9dmPtQZNPQXWfNW49_A7BRQUDiAad5S3I0juYsvYS7HThf0dGXRiJwI8ZYNMDRT2jxthZRaImuQFNK3VAFNptb8rJ2R5D3oBbHWDriJ4N-6UUE36ICPenmhy-aykGm_bNjD_unm6sEKy7hGTboGetpgt4F_4fAKJQCCrEzfUohXvQwUSEjQoE1RlZssIFCQTdYnG86sw&
token_type=Bearer&
expires_in=3600&
scope=openid%20profile%20api1&
state=49773b518261412199fb3ece49a043e1&
session_state=yMpEMT7X9xYpM8aBRpJ8IWK60zUi71jXzIgRSaCHO14.6d673e3200de918c6c35a5d61b4ddd2e
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![img](assets/355798-20171129195705214-1977520440.png)

 

 

 

**2、OpenID Connect协议认证**

![img](assets/355798-20171129192524979-349157340.png)

7和22是用户登录（资源拥有者授权）。

5和23为Get请求参数如下。

![img](assets/355798-20171129193036339-869596323.png)

使用工具请求实际得到的是一个自动提交的表单。

![img](assets/355798-20171129193300276-1134190471.png)

 

 登录完成后的效果。

 ![img](assets/355798-20171129192551808-1427946651.png)