



##	GrantTypes

```
 AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
```



Client_Credentials

ResourceOwnerPassword

```
 AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
```

在SPA(Single Page Application)中, implicit flow基本上就是除了resource owner 
password flow 以外唯一合适的flow, 但是我们的网站可能会在client(SPA 
client/或者指用户)没使用网站的时候访问api, 为了这样做, 不但要保证token不过期, 我们还需要使用别的flow



Authentication 











## 	Menu

预备知识: <http://www.cnblogs.com/cgzl/p/7746496.html>

第一部分: <http://www.cnblogs.com/cgzl/p/7780559.html>

第二部分: <http://www.cnblogs.com/cgzl/p/7788636.html>

第三部分: <http://www.cnblogs.com/cgzl/p/7793241.html>

第四部分: <http://www.cnblogs.com/cgzl/p/7795121.html>

第五部分: <http://www.cnblogs.com/cgzl/p/7799567.html>

第六部分: <http://www.cnblogs.com/cgzl/p/7799567.html



# 建立authorization server

第一部分: <http://www.cnblogs.com/cgzl/p/7780559.html>

##	 配置Identity Server

#还是Startup.cs,编辑ConfigureServices方法:

这里不仅要把IdentityServer注册到容器中, 还要至少对其配置三点内容:

\1. 哪些API可以使用这个authorization server.

\2. 那些客户端Client(应用)可以使用这个authorization server.

\3. 指定可以使用authorization server授权的用户.

首先需要把上面这些做成一个配置文件:

建立Configuration/InMemoryConfiguration.cs:

## Configuration.cs



ApiResources: 这里指定了name和display name, 以后api使用authorization server的时候, 这个name一定要一致, 否则就不好用的.

Clients: Client的属性太多了, 这里就指定几个. 其中ClientSecrets是Client用来获取token用的.  AllowedGrantType:  这里使用的是通过用户名密码和ClientCredentials来换取token的方式. ClientCredentials允许Client只使用ClientSecrets来获取token.  这比较适合那种没有用户参与的api动作. AllowedScopes: 这里只用socialnetwork

Users: 这里的内存用户的类型是TestUser, 只适合学习和测试使用, 实际生产环境中还是需要使用数据库来存储用户信息的, 例如接下来会使用asp.net core identity. TestUser的SubjectId是唯一标识.



## ResourceOwnerPasswordAndClientCredentials



由于identity server我们设置的是 ResourceOwnerPasswordAndClientCredentials 
这个GrantType, 所以使用用户名密码以及使用ClientCredentials都可以. 那我们把用户名和密码去掉, 只发送Client 
Credentials:

仍然获取到了token. 控制台上的信息与上一个稍有不同, 没有user相关的信息了:

##  使用正经的证书:

证书可以通过几种渠道获得, 可以购买, 可以使用IIS生成, 也可以使用Openssl这样的工具生成证书. 我就使用openssl吧.

去openssl的windows官网: <https://slproweb.com/products/Win32OpenSSL.html>

下载 1.1.0版: <https://slproweb.com/download/Win64OpenSSL-1_1_0f.exe>

安装后, 打开命令行.

```
openssl req -newkey rsa:2048 -nodes -keyout socialnetwork.key -x509 -days 365 -out socialnetwork.cer
```

具体的信息就不管了. 这个证书的有效期是365天, 命令参数里面设定的.

## 添加像样的UI

Identity Server 4 提供了一套QuickStart UI : <https://github.com/IdentityServer/IdentityServer4.Quickstart.UI/tree/release>

在项目根目录打开Powershell(可以在项目根目录, 按住shift, 点击右键的Powershell)

#	Web Api项目

第二部分: <http://www.cnblogs.com/cgzl/p/7788636.html>



##	为Web Api添加Swagger帮助页面

完全依照官方文档安装swagger即可: <https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?tabs=visual-studio>

通过nuget安装或者通过package manager console:

```
Install-Package Swashbuckle.AspNetCore
```



## 添加[Authorize]属性:

打开ValuesController, 在Controller上面添加这个属性:

```
    [Authorize]
    [Route("api/[controller]")]
    public class ValuesController : Controller
```



##	AddAuthentication

AddAuthentication()是把验证服务注册到DI, 并配置了Bearer作为默认模式.

AddIdentityServerAuthentication()是在DI注册了token验证的处理者.

由于是本地运行, 所以就不使用https了, RequireHttpsMetadata = false. 如果是生产环境, 一定要使用https.

Authority指定Authorization Server的地址.

ApiName要和Authorization Server里面配置ApiResource的name一样.

然后, 在Startup的Configure方法里配置Authentication中间件.

```
app.UseAuthentication();

app.UseMvc();
```

这句话就是在把验证中间件添加到管道里, 这样每次请求就会调用验证服务了. 一定要在UserMvc()之前调用.

当在controller或者Action使用[Authorize]属性的时候,  这个中间件就会基于传递给api的Token来验证Authorization, 如果没有token或者token不正确,  这个中间件就会告诉我们这个请求是UnAuthorized(未授权的).



 事实上这就是api从identity server请求获取public key, 然后在webapi里用它来验证token.

##	client_credentials

上面这种验证 我们使用的是client_credentials. 下面我们使用resourceownerpassword这个flow来试试:



## resourceownerpassword

在postman里面这样请求token, grant_type改成password, 然后添加username和password:

# OpenId Connect Authentication

第三部分: <http://www.cnblogs.com/cgzl/p/7793241.html>



现在让我们从MvcClient使用从Authorization Server获取的token来访问web api. 并且确保这个token不过期.

现在我们的mvcClient使用的是implicit flow, 也就是说, token 被发送到client. 这种情况下 token的生命可能很短, 但是我们可以重定向到authorization server 重新获取新的token.

d_token是openid connect指定的, 你需要从authorization server获得它, 用来验证你的身份, 知道你已经登陆了. id_token不是你用来访问api的.

access_token是用来访问api的.

##  添加OpenId Connect Authentication

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

                options.ClientId = "mvc_implicit";
                options.SaveTokens = true;
            });
```

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); 这句话是指, 我们关闭了JWT的Claim 类型映射, 以便允许well-known claims.

这样做, 就保证它不会修改任何从Authorization Server返回的Claims.

AddAuthentication()方法是像DI注册了该服务.

这里我们使用Cookie作为验证用户的首选方式: DefaultScheme = "Cookies".

而把DefaultChanllangeScheme设为"oidc"是因为, 当用户需要登陆的时候, 将使用的是OpenId Connect Scheme.

然后的AddCookie, 是表示添加了可以处理Cookie的处理器(handler).

最后AddOpenIdConnect是让上面的handler来执行OpenId Connect 协议.

其中的Authority是指信任的Identity Server ( Authorization Server).

ClientId是Client的识别标志. 目前Authorization Server还没有配置这个Client, 一会我们再弄.

Client名字也暗示了我们要使用的是implicit flow,  这个flow主要应用于客户端应用程序, 这里的客户端应用程序主要是指javascript应用程序. implicit  flow是很简单的重定向flow, 它允许我们重定向到authorization server, 然后带着id token重定向回来, 这个  id token就是openid connect 用来识别用户是否已经登陆了. 同时也可以获得access token. 很明显,  我们不希望access token出现在那个重定向中. 这个一会再说.

一旦OpenId Connect协议完成, SignInScheme使用Cookie Handler来发布Cookie (中间件告诉我们已经重定向回到MvcClient了, 这时候有token了, 使用Cookie handler来处理).

SaveTokens为true表示要把从Authorization Server的Reponse中返回的token们持久化在cookie中.









------

##	添加Client----Implicit flow



ClientId要和MvcClient里面指定的名称一致.

**OAuth是使用Scopes**来划分Api的, 而**OpenId Connect则使用Scopes来限制**信息, 例如使用offline access时的Profile信息, 还有用户的其他细节信息.

这里**GrantType要改为Implicit**. 使用Implicit flow时, 首先会重定向到Authorization  Server, 然后登陆, 然后Identity Server需要知道是否可以重定向回到网站, 如果不指定重定向返回的地址的话,  我们的Session有可能就会被劫持. 

RedirectUris就是登陆成功之后重定向的网址,  这个网址(http://localhost:5002/signin-oidc)在MvcClient里, openid  connect中间件使用这个地址就会知道如何处理从authorization server返回的response. 这个地址将会在openid  connect 中间件设置合适的cookies, 以确保配置的正确性.

而PostLogoutRedirectUris是登出之后重定向的网址. 有可能发生的情况是, 你登出网站的时候, 会重定向到Authorization Server, 并允许从Authorization Server也进行登出动作.

最后还需要**指定OpenId Connect使用的Scopes**, 之前我们指定的socialnetwork是一个ApiResource.  而这里我们需要添加的是让我们能使用OpenId Connect的SCopes, 这里就要使用Identity Resources.  Identity Server带了几个常量可以用来指定OpenId Connect预包装的Scopes.  上面的AllowedScopes设定的就是我们要用的scopes, 他们包括 openid Connect和用户的profile,  同时也包括我们之前写的api resource: "socialnetwork". 要注意区分, 这里有Api resources,  还有openId connect scopes(用来限定client可以访问哪些信息), 而为了使用这些openid connect  scopes, 我们需要设置这些identity resoruces, 这和设置ApiResources差不多:





##	SSO(Single Sign-On) 单点登录
所以这就允许我们做SSO(Single Sign-On) 单点登录了. 这时候其他使用这个Authorization 
Server的Client应用, 由于用户已经登陆到Authorization Server了, 只需要请求用户的许可来访问用户的数据就行了.

###	想要从MvcClient调用WebApi

我们现在想从MvcClient调用WebApi的api/Values节点, 这就需要使用从Authorization  Server返回的token. 但是由于我们使用的是implicit flow, 而使用implicit flow,  一切数据都是被发送到Client的. 这就是说, 为了让MvcClient知道用户已经成功登陆了, Authorization  Server将会告诉Client(Chrome浏览器)重定向回到MvcClient网站, 并附带着数据.  这意味着token和其他安全信息将会在浏览器里面被传递. 也就是说从Authorization Server发送access token的时候,  如果有人监听的话就会看见这些数据, 使用ssl能有效阻止监听到数据. 当然肯定有办法解决这个问题, 例如使用其他flow. 但是有时候还是必须要使用implicit flow 获取到access token. 我们需要做的就是告诉Authorization Server可以使用implicit flow来获取token.





d_token是openid connect指定的, 你需要从authorization server获得它, 用来验证你的身份, 知道你已经登陆了. id_token不是你用来访问api的.

access_token是用来访问api的.

```csharp
 new Client
                {
                    ClientId = "socialnetwork",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "socialnetwork" }
                },
```



```csharp
new Client
                {
                    ClientId = "mvc_implicit",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "socialnetwork"
                    },
                    AllowAccessTokensViaBrowser = true
                    //告诉Authorization Server在使用implicit flow时可以允许返回Access token.
                    //在某种情况下还是不建议这么做.
                }
```

有个地方写到返回类型是id_token. 这表示我们要进行的是Authentication.

而我们想要的是既做Authentication又做Authorization. 也就是说我们既要id_token还要token本身.

这么做, 在MvcClient的CongifureServices:





```csharp
public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

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

                options.ClientId = "mvc_implicit";
                options.ResponseType = "id_token token";
                //id_token既做Authentication又做Authorization. 
                //也就是说我们既要id_token还要token本身.
                options.SaveTokens = true;
            });
        }
```




上一篇讲了使用OpenId Connect进行Authentication.


# Hybrid Flow和Offline Access

第四部分: 	https://www.cnblogs.com/cgzl/p/7795121.html

**authorization** code flow. 它和implicit flow 很像, 不同的是, 
在重定向回到网站的时候获取的不是access token, 而是从authorization server获取了一个code, 
使用它网站可以交换一个secret, 使用这个secret可以获取access token和refresh tokens.

**Hybrid** Flow, 是两种的混合, 首先identity token通过浏览器传过来了, 然后客户端可以在进行任何工作之前对其验证, 如果验证成功, 客户端就会再打开一个通道向Authorization Server请求获取access token.

```csharp
    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
```





##	InMemoryConfiguration添加一个Client:

GrantType要改成Hybrid或者HybrdAndClientCredentials, 如果只使用Code Flow的话不行,  因为我们的网站使用Authorization Server来进行Authentication, 我们想获取Access  token以便被授权来访问api. 所以这里用HybridFlow.

还需要添加一个新的Email scope, 因为我想改变api来允许我基于email来创建用户的数据, 因为authorization  server 和 web api是分开的, 所以用户的数据库也是分开的. Api使用用户名(email)来查询数据库中的数据.

AllowOfflineAccess. 我们还需要获取Refresh Token, 这就要求我们的网站必须可以"离线"工作, 这里离线是指用户和网站之间断开了, 并不是指网站离线了.

这就是说网站可以使用token来和api进行交互, 而不需要用户登陆到网站上. 

```csharp
public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

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
                options.ClientId = "mvc_code";
                options.ClientSecret = "secret";
                options.ResponseType = "id_token code";
                options.Scope.Add("socialnetwork");
                options.Scope.Add("offline_access");
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
            });
        }
```





首先改ClientId和Authorization server一致. 这样用户访问的时候和implicit差不多, 只不过重定向回来的时候, 获取了一个code, 使用这个code可以换取secret然后获取access token.

所以需要在网站(MvcClient)上指定Client Secret. 这个不要泄露出去.

还需要改变reponse type, 不需要再获取access token了, 而是code, 这意味着使用的是Authorization Code flow.

还需要指定请求访问的scopes: 包括 socialnetwork api和离线访问

最后还可以告诉它从UserInfo节点获取用户的Claims.

---



这里可以看到请求访问的scope, response_type. 还告诉我们respose mode是from_post, 这就是说, 在这登陆后重定向回到网站是使用的form post方式.



---

这里可以看到请求访问的范围, 包括个人信息和Application Access.



---

Authorization **Server的控制台**:  

就会看到一个request. 中间件发起了一个请求使用Authorization Code和ClientId和secret来换取了Access token.

当Authorization验证上述信息后, 它就会创建一个token.



##	打印Refresh Token

这些token包含了什么时候过期的信息.

如果access token过期了, 就无法访问api了. 所以需要确保access token不过期. 这就需要使用refresh token了.

:使用这个refresh token可以获取到新的access token和refresh_token, 当这个access_token过期的时候, 可以使用refresh_token再获取一个access_token和refresh_token......



##	获取自定义Claims

web api 要求request请求提供access token, 以证明请求的用户是已经授权的. 现在我们准备从Access token里面提取一些自定义的Claims, 例如Email.



##	使用Access Token调用Web Api

首先在web api项目建立一个IdentityController:



##	刷新Access Token

根据配置不同, token的有效期可能差别很大, 如果token过期了, 那么发送请求之后就会返回401 UnAuthorized.

当然如果token过期了, 你可以让用户重定向到Authorization Server重新登陆,再回来操作, 不过这样太不友好, 太繁琐了.

既然我们有refresh token了, 那不如向authorization server请求一个新的access token和refresh token. 然后再把这些更新到cookie里面. 所以下次再调用api的时候使用的是新的token.





# EF持久化



第五部分: <http://www.cnblogs.com/cgzl/p/7799567.html>

# [js(angular5) 客户端](https://www.cnblogs.com/cgzl/p/7894446.html)



第六部分: <http://www.cnblogs.com/cgzl/p/7799567.html>



这是后端的代码: <https://github.com/solenovex/asp.net-core-2.0-web-api-boilerplate>

这里面有几个dbcontext, 需要分别对Identity Server和Sales.DataContext进行update-database, 如果使用的是Package Manager Console的话.

进行update-database的时候, 如果是针对IdentityServer这个项目的要把IdentityServer设为启动项目,  如果是针对Sales.DataContext的, 那么要把SalesApi.Web设为启动项目, 然后再进行update-database.





# 分类





[ASP.NET Core 2.1 Web API + Identity Server 4 + Angular 6 + Angular Material 实战小项目视频](https://www.cnblogs.com/cgzl/p/9498482.html) solenovex 2018-08-18 20:09 阅读:7402 评论:53  



[学习Identity Server 4的预备知识 (误删, 重补)](https://www.cnblogs.com/cgzl/p/9405796.html) solenovex 2018-08-02 14:34 阅读:1895 评论:5  



[Identity Server 4 - Hybrid Flow - 使用ABAC保护MVC客户端和API资源](https://www.cnblogs.com/cgzl/p/9282059.html) solenovex 2018-07-09 10:30 阅读:1253 评论:3  



[Identity Server 4 - Hybrid Flow - 保护API资源](https://www.cnblogs.com/cgzl/p/9276278.html) solenovex 2018-07-07 13:08 阅读:1215 评论:2  



[Identity Server 4 - Hybrid Flow - Claims](https://www.cnblogs.com/cgzl/p/9268371.html) solenovex 2018-07-05 15:04 阅读:1316 评论:2  



[Identity Server 4 - Hybrid Flow - MVC客户端身份验证](https://www.cnblogs.com/cgzl/p/9253667.html) solenovex 2018-07-04 21:22 阅读:4093 评论:22  



[Identity Server 4 预备知识 -- OpenID Connect 简介](https://www.cnblogs.com/cgzl/p/9231219.html) solenovex 2018-06-27 14:12 阅读:1877 评论:6  



[Identity Server 4 预备知识 -- OAuth 2.0 简介](https://www.cnblogs.com/cgzl/p/9221488.html) solenovex 2018-06-25 13:10 阅读:3009 评论:16  



[用 Identity Server 4 (JWKS 端点和 RS256 算法) 来保护 Python web api](https://www.cnblogs.com/cgzl/p/8270677.html) solenovex 2018-01-11 21:44 阅读:2007 评论:1  



[使用Identity Server 4建立Authorization Server (6) - js(angular5) 客户端](https://www.cnblogs.com/cgzl/p/7894446.html) solenovex 2017-11-25 10:41 阅读:3783 评论:10  



[使用Identity Server 4建立Authorization Server (5)](https://www.cnblogs.com/cgzl/p/7799567.html) solenovex 2017-11-08 10:58 阅读:3931 评论:11  



[使用Identity Server 4建立Authorization Server (4)](https://www.cnblogs.com/cgzl/p/7795121.html) solenovex 2017-11-07 15:09 阅读:5358 评论:28  



[使用Identity Server 4建立Authorization Server (3)](https://www.cnblogs.com/cgzl/p/7793241.html) solenovex 2017-11-06 16:12 阅读:4343 评论:6  



[使用Identity Server 4建立Authorization Server (2)](https://www.cnblogs.com/cgzl/p/7788636.html) solenovex 2017-11-05 21:24 阅读:5235 评论:9  



[使用angular4和asp.net core 2 web api做个练习项目(三)](https://www.cnblogs.com/cgzl/p/7768147.html) solenovex 2017-11-02 19:45 阅读:1676 评论:4  



# end