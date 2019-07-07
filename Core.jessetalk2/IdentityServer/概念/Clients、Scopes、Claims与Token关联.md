#  			[IdentityServer4之Clients、Scopes、Claims与Token关联](https://www.cnblogs.com/ddrsql/p/8309516.html) 		



## IdentityServer4之Clients、Scopes、Claims与Token关联 

### **参考**

**官方文档**：[client](http://docs.identityserver.io/en/release/reference/client.html)、[identity_resource](http://docs.identityserver.io/en/release/reference/identity_resource.html)、[api_resource](http://docs.identityserver.io/en/release/reference/api_resource.html)：三类配置项介绍描述。

**打一个不恰当的比喻来描述一下**
User：表示自己 。
Client：表示客户经理，能指引或者代办一些业务。
Resource：表示银行，包括identity_resource（银行基本业务）、api_resource（银行特色业务）。多个resource比作多个分行。

**user中的**
Claims：自身在银行已经有的业务（包括自己YY的业务）。

**client中的**
Claims、Scopes是客户经理会推荐给你（User）的业务需不需要看自己。
Claims：好比优惠client可以选择给你或者不给。
Scopes：但是推荐给你的某个Scope业务可能与银行已经下线了但是client不知道。

**Resource中的**
Claims、Scopes、Scopes->Claims：表示银行的业务。

Token：银行认可自己拥有的业务信息。

### **User、Client、Resource配置**

**User配置**

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
new TestUser
{
    SubjectId = "1",
    Username = "ddr",
    Password = "123",
    Claims = new []
    {
        new Claim("name", "ddr"),
        new Claim("get", "get_order"),  //User Claim Type 与 Api Resource中的Claims、Scopes->Claims的Type匹配就会输出到Token
        new Claim("add", "add_order"),
        new Claim("add", "add_account"),
        new Claim("del", "del_all"),
        new Claim("website", "https://ddr.com")
    }
},
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**Client配置**

![img](https://images2017.cnblogs.com/blog/355798/201801/355798-20180118120157178-575922352.png)

![img](https://images2017.cnblogs.com/blog/355798/201801/355798-20180118120807537-1517049741.png)

 

 

**Identity Resources配置**

一般不需要改变就是默认的OpenId、Profile、Email、Phone、Address。

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
{ IdentityServerConstants.StandardScopes.Profile, new[]
                { 
                    JwtClaimTypes.Name,
                    JwtClaimTypes.FamilyName,
                    JwtClaimTypes.GivenName,
                    JwtClaimTypes.MiddleName,
                    JwtClaimTypes.NickName,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Profile,
                    JwtClaimTypes.Picture,
                    JwtClaimTypes.WebSite,
                    JwtClaimTypes.Gender,
                    JwtClaimTypes.BirthDate,
                    JwtClaimTypes.ZoneInfo,
                    JwtClaimTypes.Locale,
                    JwtClaimTypes.UpdatedAt 
                }},
{ IdentityServerConstants.StandardScopes.Email, new[]
                { 
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified 
                }},
{ IdentityServerConstants.StandardScopes.Address, new[]
                {
                    JwtClaimTypes.Address
                }},
{ IdentityServerConstants.StandardScopes.Phone, new[]
                {
                    JwtClaimTypes.PhoneNumber,
                    JwtClaimTypes.PhoneNumberVerified
                }},
{ IdentityServerConstants.StandardScopes.OpenId, new[]
                {
                    JwtClaimTypes.Subject
                }}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**Api Resource配置**

![img](https://images2017.cnblogs.com/blog/355798/201801/355798-20180118120752053-2122144593.png)

![img](https://images2017.cnblogs.com/blog/355798/201801/355798-20180118120833037-76748225.png)

 

### 过程详解

**使用正常方式获取的Token**

**![img](https://images2017.cnblogs.com/blog/355798/201801/355798-20180118121416146-565309955.png)**

 

**获取的Token详细信息**

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
[
  {
    "type": "nbf",
    "value": "1516248790"
  },
  {
    "type": "exp",
    "value": "1516252390"
  },
  {
    "type": "iss",
    "value": "http://www.ids4.com"
  },
  {
    "type": "aud",
    "value": "http://www.ids4.com/resources"
  },
  {
    "type": "aud",
    "value": "shop"
  },
  {
    "type": "client_id",
    "value": "ro.client"
  },
  {
    "type": "sub",
    "value": "1"
  },
  {
    "type": "auth_time",
    "value": "1516248785"
  },
  {
    "type": "idp",
    "value": "local"
  },
  {
    "type": "get",
    "value": "get_order"
  },
  {
    "type": "add",
    "value": "add_order"
  },
  {
    "type": "add",
    "value": "add_account"
  },
  {
    "type": "scope",
    "value": "account"
  },
  {
    "type": "scope",
    "value": "order"
  },
  {
    "type": "amr",
    "value": "pwd"
  },
  {
    "type": "api1返回",
    "value": "2018-01-18 12:13:15"
  }
]
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

client没有把优惠给你，client客户经理的Claims中是有ro - get_account但是这项优惠没有取出来，Properties默认设置![img](https://images2017.cnblogs.com/blog/355798/201801/355798-20180118122004396-918035348.png)不会返回到Token。

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
// check for client claims
if (request.ClientClaims != null && request.ClientClaims.Any())
{
    if (subject == null || request.Client.AlwaysSendClientClaims)
    {
        foreach (var claim in request.ClientClaims)
        {
            var claimType = claim.Type;

            if (request.Client.ClientClaimsPrefix.IsPresent())
            {
                claimType = request.Client.ClientClaimsPrefix + claimType;
            }

            outputClaims.Add(new Claim(claimType, claim.Value, claim.ValueType));
        }
    }
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

`new Claim("del", "del_all")` 是自己YY出来的Token里也不会有。

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
/// <summary>
/// Filters the claims based on requested claim types.
/// </summary>
/// <param name="context">The context.</param>
/// <param name="claims">The claims.</param>
/// <returns></returns>
public static List<Claim> FilterClaims(this ProfileDataRequestContext context, IEnumerable<Claim> claims)
{
    return claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

claims自己的claims信息。

![img](https://images2017.cnblogs.com/blog/355798/201801/355798-20180118123121178-221491221.png)

context.RequestedClaimTypes是Api Resource中Claims、Scopes->Claims的信息。

![img](https://images2017.cnblogs.com/blog/355798/201801/355798-20180118123158178-73576026.png)

 

client客户经理推荐的123实际在银行已经下线了。

如果获取Token请求包含了 "123" 的scope，但是实际上Resource又不存在就会提示invalid_scope。

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
foreach (var scope in requestedScopes)
{
    var identity = resources.IdentityResources.FirstOrDefault(x => x.Name == scope);
    if (identity != null)
    {
        if (!client.AllowedScopes.Contains(scope))
        {
            _logger.LogError("Requested scope not allowed: {scope}", scope);
            return false;
        }
    }
    else
    {
        var api = resources.FindApiScope(scope);
        if (api == null || !client.AllowedScopes.Contains(scope))
        {
            _logger.LogError("Requested scope not allowed: {scope}", scope);
            return false;
        }
    }
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

### Scope对模块鉴权

参考：https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims

Token中带有 scope:order或者scope:account的请求都能访问IdentityController。

Api项目配置

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
services.AddMvcCore()
    .AddAuthorization(options =>
    {
        options.AddPolicy("Order", policy => policy.RequireClaim("scope","order","account"));
    })
    .AddJsonFormatters();
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
[Authorize(Policy = "Order")]
public class IdentityController : ControllerBase
```

IdentityServer4配置后台管理，在github找到一个随机生成数据的后台改成使用读数据库，数据库使用ids4示例生成。时间紧做出来并不好凑合用。

https://github.com/ddrsql/IdentityServer4.Admin