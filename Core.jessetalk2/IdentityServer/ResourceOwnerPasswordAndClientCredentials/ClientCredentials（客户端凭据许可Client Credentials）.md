



# 	[IdentityServer4之Client Credentials（客户端凭据许可）](https://www.cnblogs.com/ddrsql/p/7887083.html)





### **参考**

项目创建：[0_overview](http://docs.identityserver.io/en/release/quickstarts/0_overview.html)，[1_client_credentials](http://docs.identityserver.io/en/release/quickstarts/1_client_credentials.html)

概念：[客户端凭据许可](https://www.cnblogs.com/ddrsql/p/7789064.html#ClientCredentials)

 

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

```
//client credentials client
new Client
{
    ClientId = "client",
    // no interactive user, use the clientid/secret for authentication
    AllowedGrantTypes = GrantTypes.ClientCredentials,    //Jwt = 0；Reference = 1支持撤销；
    AccessTokenType = AccessTokenType.Reference,
    // secret for authentication
    ClientSecrets =
    {
        new Secret("secret".Sha256()),
        new Secret("abc".Sha256())
    },
    // scopes that client has access to
    AllowedScopes = { "api1" }
},
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

**认证服务Startup配置**

```csharp
// configure identity server with in-memory stores, keys, clients and scopes
services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryApiResources(Config.GetApiResources())//认证服务ApiResource配置
    .AddInMemoryClients(Config.GetClients());//认证服务Client配置
```

配置完成启动访问http://localhost:5000/.well-known/openid-configuration

![img](assets/355798-20171123175533711-889360263.png)

 

### 资源服务Api配置

**资源服务器Startup配置**

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
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

客户端与授权服务器进行身份验证并向令牌端点请求访问令牌。

授权服务器对客户端进行身份验证，如果有效，颁发访问令牌。

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```csharp
//credentials client
private void btnAuth_Click(object sender, EventArgs e)
{
    // discover endpoints from metadata
    Task<DiscoveryResponse> discoTask = DiscoveryClient.GetAsync(txtCCAuthSer.Text);
    discoTask.Wait();
    var disco = discoTask.Result;
    if (disco.IsError)
    {
        MessageBox.Show(disco.Error, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        txtCCResult.Text = string.Empty;
        txtAccessToken.Text = string.Empty;
        return;
    }

    // request token
    var tokenClient = new TokenClient(disco.TokenEndpoint, txtCCClient.Text, txtCCSecret.Text);
    Task<TokenResponse> tokenTask = tokenClient.RequestClientCredentialsAsync(txtCCScopes.Text);
    tokenTask.Wait();
    var tokenResponse = tokenTask.Result;

    if (tokenResponse.IsError)
    {
        MessageBox.Show(tokenResponse.Error, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        txtCCResult.Text = string.Empty;
        txtAccessToken.Text = string.Empty;
        return;
    }
    
    txtCCResult.Text = tokenResponse.Json.ToString();
    txtAccessToken.Text = tokenResponse.AccessToken;
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

###	Client调用Api

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```csharp
private void btnCallApi_Click(object sender, EventArgs e)
{
    // call api
    var client = new HttpClient();
    client.SetBearerToken(txtAccessToken.Text);

    var responseTask = client.GetAsync(txtCCApiUrl.Text);
    responseTask.Wait();
    var response = responseTask.Result;
    if (!response.IsSuccessStatusCode)
    {
        MessageBox.Show(response.StatusCode.ToString(), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        txtApiResult.Text = string.Empty;
    }
    else
    {
        var contentTask = response.Content.ReadAsStringAsync();
        contentTask.Wait();
        var content = contentTask.Result;
        txtApiResult.Text = JArray.Parse(content).ToString();
    }
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

### 获取token过程解析

**Jwt形式获取access_token**

通过IdentityModel发送请求

![img](assets/355798-20171123224742343-890356627.png)

监听请求数据

![img](assets/355798-20171123223641140-799693387.png)

客户端身份验证两种方式
 1、Authorization: Basic czZCaGRSa3F0Mzo3RmpmcDBaQnIxS3REUmJuZlZkbUl3。

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
POST /connect/token HTTP/1.1
Connection: Keep-Alive
Content-Type: application/x-www-form-urlencoded
Accept: application/json
Authorization: Basic Y2xpZW50OnNlY3JldA==
Expect: 100-continue
Host: localhost:5000
MS-ASPNETCORE-TOKEN: 08de58f6-58ee-4f05-8d95-3829dde6ae09
X-Forwarded-For: [::1]:12112
X-Forwarded-Proto: http
Content-Length: 40
grant_type=client_credentials&scope=api1
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

2、client_id（客户端标识），client_secret（客户端秘钥）。

##### 	client_id（客户端标识），client_secret（客户端秘钥）。



 ![img](assets/355798-20171123224319921-127245441.png)

 

**Reference形式获取access_token**

将client的AccessTokenType设置为1

![img](assets/355798-20171123224917281-1499529683.png)

再次获取的access_token不包含Claim信息。

![img](assets/355798-20171123225155265-1290026123.png)

此时获取的access_token(加密后)对应PersistedGrants表中的key

![img](assets/355798-20171123230312328-440360097.png)

 

### 调用Api资源服务过程解析

##### 	**Jwt形式获取access_token调用Api**

监听请求数据

**![img](assets/355798-20171123231149984-45734604.png)**

api资源服务验证Jwt形式token会去认证服务器获取一次配置信息。

![img](assets/355798-20171123231817546-831023125.png)

![img](assets/355798-20171123231015125-1113286523.png)

 ![img](assets/355798-20171123232428250-587400551.png)

 

#####  **Reference形式获取access_token调用Api**

 监听请求数据

 ![img](assets/355798-20171123232847875-341578057.png)

api资源服务验证Reference形式token每次（可配置缓存）会去认证服务器获取信息。[参考](http://docs.identityserver.io/en/release/endpoints/introspection.html)

 ![img](assets/355798-20171123233128984-1726797375.png)

![img](assets/355798-20171123233808484-1702031889.png)











![img](assets/355798-20171123233851593-1030519786.png)





#	end



##  Client请求与调用

```csharp
//api 请求token  
Task<TokenResponse> tokenTask = tokenClient.RequestClientCredentialsAsync(txtCCScopes.Text);
    
    //Client 加头调用
    
    client.SetBearerToken(txtAccessToken.Text);

    var responseTask = client.GetAsync(txtCCApiUrl.Text);
    
```





##	客户端身份验证两种方式

 1、Authorization: Basic czZCaGRSa3F0Mzo3RmpmcDBaQnIxS3REUmJuZlZkbUl3

```
POST /connect/token HTTP/1.1
Connection: Keep-Alive
Content-Type: application/x-www-form-urlencoded
Accept: application/json
Authorization: Basic Y2xpZW50OnNlY3JldA==
Expect: 100-continue
Host: localhost:5000
MS-ASPNETCORE-TOKEN: 08de58f6-58ee-4f05-8d95-3829dde6ae09
X-Forwarded-For: [::1]:12112
X-Forwarded-Proto: http
Content-Length: 40
grant_type=client_credentials&scope=api1
```



2、client_id（客户端标识），client_secret（客户端秘钥）。