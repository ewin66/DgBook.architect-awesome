

# [IdentityServer4之Implicit(隐式许可) —— oidc-client-js前后端分离](https://www.cnblogs.com/ddrsql/p/8004888.html)

### **参考**

**官方文档**：[oidc-client-js](https://github.com/IdentityModel/oidc-client-js/wiki)：oidc-client是一个JavaScript库，用于在浏览器（也可能是Cordova风格的应用程序）中运行。它为OIDC和OAuth2提供协议支持，并为用户会话和访问令牌管理提供管理功能。文档最后也给出了Angular2、Aurelia、ReactJS  & Redux、Blog post on Angular这几种前端框架的示例。

[JsOidc](https://github.com/IdentityServer/IdentityServer4.Samples/tree/release/Clients/src/JsOidc)：使用oidc-client-js的客户端示例。

**概念**：[隐式许可](https://www.cnblogs.com/ddrsql/p/7789064.html#Implicit)：简单描述过程以及适用范围。

[Authorize Endpoint](http://docs.identityserver.io/en/release/endpoints/authorize.html)：主要介绍了请求过程参数含义。

 **三个Web站点**：

1、localhost:5000：认证服务器。

2、www.jsclient.com：js纯静态客户端。

3、localhost:5001：Api资源服务器。

### **认证服务端配置**

**认证服务ApiResource配置**

```
new ApiResource("api1", "api项目 一")
{
    ApiSecrets = { new Secret("api1pwd".Sha256()) }
},
```

**认证服务Client配置**

js client的静态资源服务端配置了host。

[![复制代码](assets/copycode-1562483159247.gif)](javascript:void(0);)

```csharp
// JavaScript Client
new Client
{
    ClientId = "js",
    ClientName = "JavaScript Client",
    AllowedGrantTypes = GrantTypes.Implicit,
    AllowAccessTokensViaBrowser = true,

    RedirectUris = {
        "http://localhost:5009/callback.html",
        "http://www.jsclient.com/callback.html"
    },
    PostLogoutRedirectUris = {
        "http://localhost:5009/index.html",
        "http://www.jsclient.com/index.html"
    },
    AllowedCorsOrigins = {
        "http://localhost:5009",
        "http://www.jsclient.com"
    },

    AllowedScopes =
    {
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Profile,
        "api1"
    }
},
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

### 资源服务Api配置

**资源服务器Startup配置**

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```csharp
// This method gets called by the runtime. Use this method to add services to the container.
public void ConfigureServices(IServiceCollection services)
{
    //services.AddMvc();
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
    //支持跨域
    services.AddCors(options =>
    {
        // this defines a CORS policy called "default"
        options.AddPolicy("default", policy =>
        {
            policy.WithOrigins("http://www.jsclient.com")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

Configure中添加 app.UseCors("default"); 

 

添加接口

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
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

**1、JS Client**

js Web-Hosted Client Resource的静态资源。

![img](assets/355798-20171208111454015-1095070029.png)

运行后效果如下：

![img](assets/355798-20171208111930671-137008354.png)

```
页面中操作按钮如下：
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
<li><a class="btn btn-primary" href="index.html">Home</a></li>
<li><button class="btn btn-default request" data-scope='openid' data-type='id_token'>Login Only</button></li>
<li><button class="btn btn-default request" data-scope='openid profile' data-type='id_token'>Profile</button></li>
<li><button class="btn btn-default request" data-scope='openid profile api1' data-type='id_token token'>Profile and API Access</button></li>
<li><button class="btn btn-default request" data-scope='api1' data-type='token'>API Access Only</button></li>
<li><button class="btn btn-primary call">Call Service</button></li>
<li><button class="btn btn-info revoke">Revoke Access Token</button></li>
<li><button class="btn btn-info logout">Logout</button></li>
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
id_token：请求一个身份令牌（只允许身份范围）。token：请求访问令牌（只允许资源作用域）。id_token token：请求身份令牌和访问令牌。清楚这个几个概念后页面上的操作按钮就都比较好理解了。
```

 app.js中的配置如下：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
var config = {
    authority: "http://localhost:5000/",
    client_id: "js",
    redirect_uri: window.location.origin + "/callback.html",
    post_logout_redirect_uri: window.location.origin + "/index.html",

    // if we choose to use popup window instead for logins
    popup_redirect_uri: window.location.origin + "/popup.html",
    popupWindowFeatures: "menubar=yes,location=yes,toolbar=yes,width=1200,height=800,left=100,top=100;resizable=yes",

    // these two will be done dynamically from the buttons clicked, but are
    // needed if you want to use the silent_renew
    response_type: "id_token token",
    //scope: "openid profile email api1 api2.read_only",
    scope: "openid profile api1",

    // this will toggle if profile endpoint is used
    loadUserInfo: false,

    // silent renew will get a new access_token via an iframe 
    // just prior to the old access_token expiring (60 seconds prior)
    silent_redirect_uri: window.location.origin + "/silent.html",
    automaticSilentRenew: true,

    // will revoke (reference) access tokens at logout time
    revokeAccessTokenOnSignout: true,

    // this will allow all the OIDC protocol claims to be visible in the window. normally a client app 
    // wouldn't care about them or want them taking up space
    filterProtocolClaims: false
};
Oidc.Log.logger = window.console;
Oidc.Log.level = Oidc.Log.DEBUG;

var mgr = new Oidc.UserManager(config);

mgr.events.addUserLoaded(function (user) {
    log("User loaded");
    showTokens();
});
mgr.events.addUserUnloaded(function () {
    log("User logged out locally");
    showTokens();
});
mgr.events.addAccessTokenExpiring(function () {
    log("Access token expiring..." + new Date());
    console.log("Access token expiring..." + new Date());
});
mgr.events.addSilentRenewError(function (err) {
    log("Silent renew error: " + err.message);
});
mgr.events.addUserSignedOut(function () {
    log("User signed out of OP");
    mgr.removeUser();
});
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

登录完成后会请求GET /connect/checksession在页面内追加iframe，iframe只包含js维护一系列的events，认证信息会存储在Session Storage中。

![img](assets/355798-20171208122345687-718107718.png)

**automaticSilentRenew: true** 

时会在设置的AccessTokenLifetime过期前AccessTokenExpiringNotificationTime（默认60秒）通过当前留存的id_token去请求新的身份认证信息。

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
GET /connect/authorize?client_id=js&redirect_uri=http%3A%2F%2Fwww.jsclient.com%2Fsilent.html&
response_type=id_token%20token&
scope=openid%20profile%20api1&
state=d814b897efc249a0b2028355f6ce80e8&
nonce=fb555249154b43eea816a122a34b0119&
prompt=none&
id_token_hint=eyJhbGciOiJSUzI1NiIsImtpZCI6IjAzMTZkNWQwYTViNTgyYzc0NmJlMmVhZWU1MTg1NzhiIiwidHlwIjoiSldUIn0.eyJuYmYiOjE1MTI3MDY2MTMsImV4cCI6MTUxMjcwNjkxMywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjoianMiLCJub25jZSI6IjUwM2YyY2VhNzE5ZjRlMjc5N2EzNThlZDlhYTI0ZDkyIiwiaWF0IjoxNTEyNzA2NjEzLCJhdF9oYXNoIjoiSUJVbTU1dFhEd3VKUTVJNlRJUGI2ZyIsInNpZCI6IjAwOGU2N2E2MWY2MzY1MjA1OTU5MTJmODQ2ZDdhNTVkIiwic3ViIjoiMiIsImF1dGhfdGltZSI6MTUxMjcwNjYxMiwiaWRwIjoibG9jYWwiLCJhbXIiOlsicHdkIl19.bJA7ejtzhjfobADoTpoNghhT19a7cnCBDGHSXvTOUpHwb_1To84l0Lu2pcGtDFeWqpWUTUKcXseNaao6bR41N45A_xCCfFsCHvSJ9pG_l-3NWIqceU-Zy-6JVH3MupbQLw3fm0Swt2NdfrjQ5G7q-e2WSXezHxXUvHbD2-PLqTG8_Qcotl0pK7lizGgdHiYF4SQM-qMLvTSOF0FHwkqiusxwrbDjR33UFYX7xlvjYRMoRvnw10qQlOQFO_0cox8BqxC-rW3EOu7mXyfz8TLyLqBIp9hlss0sayBEzxr-G_WFczmN5sL2xFpXK9dizhONAKUP7gpUjfrwy0CLiWefRg
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

AccessTokenLifetime设置的是90秒，所以Access token  expiring...的时间就是在获取新的id_token  token信息后的31秒也就是过期前AccessTokenExpiringNotificationTime（默认60秒）。

 ![img](assets/355798-20171208124507812-424460153.png)

 

### 获取token过程解析

登录获取token的过程就参考上一篇。

 





标签: [IdentityServer4](https://www.cnblogs.com/ddrsql/tag/IdentityServer4/)