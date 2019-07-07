



#  			[OAuth2.0介绍](https://www.cnblogs.com/ddrsql/p/7789064.html)

 

1. [OAuth2.0简介](https://www.cnblogs.com/ddrsql/p/7789064.html#Introduction)
2. [四种许可类型](https://www.cnblogs.com/ddrsql/p/7789064.html#Type)
    2.1. [授权码许可(Authorization Code)](https://www.cnblogs.com/ddrsql/p/7789064.html#AuthorizationCode)
    2.2. [隐式许可(Implicit)](https://www.cnblogs.com/ddrsql/p/7789064.html#Implicit)
    2.3. [资源拥有者密码凭据许可(Resource Owner Password Credentials)](https://www.cnblogs.com/ddrsql/p/7789064.html#PasswordCredentials)
    2.4. [客户端凭据许可(Client Credentials)](https://www.cnblogs.com/ddrsql/p/7789064.html#ClientCredentials)



## 1、OAuth2.0简介



#### OAuth2官网是这么描述的：

The OAuth 2.0 authorization framework enables a third-party  application to obtain limited access to an HTTP service, either on  behalf of a resource owner by orchestrating an approval interaction  between the resource owner and the HTTP service, or by allowing the  third-party application to obtain access on its own behalf.
 官方文档：https://tools.ietf.org/html/rfc6749
 中文文档：https://github.com/jeansfish/RFC6749.zh-cn/blob/master/index.md

**白话**
 OAuth2 是一个开放**授权协议**标准，它允许用户(资源拥有者)让第三方应用访问该用户在某服务的特定私有资源(资源服务器)但是不提供账号密码信息给第三方应用。

#### 角色

------

**1、Resource Owner：资源拥有者**(能够许可对受保护资源的访问权限的实体。当资源拥有者是个人时，它被称为最终用户。)
 **2、Resource Server：资源服务器**(托管受保护资源的服务器，能够接收和响应使用访问令牌对受保护资源的请求。)
 **3、Client：第三方应用客户端**(使用资源拥有者的授权代表资源拥有者发起对受保护资源的请求的应用程序)
 **4、Authorization Server ：授权服务器**(在成功验证资源拥有者且获得授权后颁发访问令牌给客户端的服务器。)

#### 协议流程

------

```
 +--------+                               +---------------+
 |        |--(A)- Authorization Request ->|   Resource    |
 |        |                               |     Owner     |
 |        |<-(B)-- Authorization Grant ---|               |
 |        |                               +---------------+
 |        |
 |        |                               +---------------+
 |        |--(C)-- Authorization Grant -->| Authorization |
 | Client |                               |     Server    |
 |        |<-(D)----- Access Token -------|               |
 |        |                               +---------------+
 |        |
 |        |                               +---------------+
 |        |--(E)----- Access Token ------>|    Resource   |
 |        |                               |     Server    |
 |        |<-(F)--- Protected Resource ---|               |
 +--------+                               +---------------+
```

#### 抽象的OAuth2.0流程描述

（A）客户端从资源拥有者处请求授权。授权请求可以直接向资源拥有者发起（如图所示），或者更可取的是通过授权服务器作为中介间接发起。
 （B）客户端收到授权许可，这是一个代表资源拥有者的授权的凭据，使用本规范中定义的四种许可类型之一或者使用扩展许可类型表示。授权许可类型取决于客户端请求授权所使用的方法以及授权服务器支持的类型。
 （C）客户端与授权服务器进行身份认证并出示授权许可以请求访问令牌。
 （D）授权服务器验证客户端身份并验证授权许可，若有效则颁发访问令牌。
 （E）客户端从资源服务器请求受保护资源并出示访问令牌进行身份验证。
 （F）资源服务器验证访问令牌，若有效则处理该请求。



## 2、四种许可类型


 **1、Authorization Code：授权码许可； 2、Implicit：隐式许可； 3、Resource Owner Password Credentials：资源拥有者密码凭据许可； 4、Client Credentials ：客户端凭据许可。**



### 2.1、授权码许可(Authorization Code)



------

授权码模式是最常见的一种授权模式，最为安全和完善。
 **适用范围**
 需要得到长期授权，OAuth客户端是Web应用服务器，OAuth访问令牌不宜泄露给用户的环境。

Authorization Code具体的流程如下：

```
 +----------+
 | Resource |
 |   Owner  |
 |          |
 +----------+
      ^
      |
     (B)
 +----|-----+          Client Identifier      +---------------+
 |         -+----(A)-- & Redirection URI ---->|               |
 |  User-   |                                 | Authorization |
 |  Agent  -+----(B)-- User authenticates --->|     Server    |
 |          |                                 |               |
 |         -+----(C)-- Authorization Code ---<|               |
 +-|----|---+                                 +---------------+
   |    |                                         ^      v
  (A)  (C)                                        |      |
   |    |                                         |      |
   ^    v                                         |      |
 +---------+                                      |      |
 |         |>---(D)-- Authorization Code ---------'      |
 |  Client |          & Redirection URI                  |
 |         |                                             |
 |         |<---(E)----- Access Token -------------------'
 +---------+       (w/ Optional Refresh Token)
```

#### 授权码许可流程描述

（A）客户端通过向授权端点引导资源拥有者的用户**代理开始流程**。客户端包括它的客户端标识、请求范围、本地状态和重定向URI，一旦访问被许可（或拒绝）授权服务器将传送用户代理回到该URI。
 （B）授权服务器**验证**资源拥有者的身份（通过用户代理），并确定资源拥有者是否授予或拒绝客户端的访问请求。
 （C）假设资源拥有者许可访问，授权服务器使用之前（在请求时或客户端注册时）**提供的重定向URI重定向用户代理回到客户端**。重定向URI包括授权码和之前客户端提供的任何本地状态。
 （D）客户端通过包含上一步中收到的授权码从授权服务器的令牌端点请求访问令牌。当发起请求时，客户端与授权服务器进行身份验证。客户端包含用于获得授权码的**重定向URI来*用于*验证**。
 （E）授权服务器对客户端进行身份验证，验证授权代码，并确保接收的重定向URI与在步骤（C）中用于重定向（资源拥有者的用户代理）到客户端的URI相匹配。如果通过，授权服务器响应返回访问令牌与可选的刷新令牌。

#### 过程详解

------

##### 授权请求（A）

| 参数          | 是否必须 | 含义                                                         |
| ------------- | -------- | ------------------------------------------------------------ |
| response_type | 必需     | 授权类型，值固定为“code”。                                   |
| client_id     | 必需     | 客户端标识。                                                 |
| redirect_uri  | 可选     | 成功授权后的回调地址。                                       |
| scope         | 可选     | 表示授权范围。                                               |
| state         | 推荐     | client端的状态值。用于第三方应用防止CSRF攻击，成功授权后回调时会原样带回。 |

示例：

```
GET /authorize?response_type=code&client_id=s6BhdRkqt3&state=xyz&redirect_uri=https%3A%2F%2Fclient%2Eexample%2Ecom%2Fcb HTTP/1.1
Host: server.example.com
```

##### 授权响应（C）

| 参数  | 是否必须 | 含义                                                         |
| ----- | -------- | ------------------------------------------------------------ |
| code  | 必需     | 授权服务器生成的授权码。授权码必须在颁发后很快过期以减小泄露风险。 推荐的最长的授权码生命周期是10分钟。客户端不能使用授权码超过一次。 如果一个授权码被使用一次以上，授权服务器必须拒绝该请求并应该撤销（如可能） 先前发出的基于该授权码的所有令牌。 授权码与客户端标识和重定向URI绑定。授权服务器生成的授权码。 授权码必须在颁发后很快过期以减小泄露风险。 |
| state | 必需     | 客户端提供的state参数原样返回。                              |

示例：

```
HTTP/1.1 302 Found
Location: https://client.example.com/cb?code=SplxlOBeZQQYbYS6WxSbIA&state=xyz
```

##### 访问令牌请求（D）

| 参数         | 是否必须 | 含义                                         |
| ------------ | -------- | -------------------------------------------- |
| grant_type   | 必需     | 授权类型，值固定为“authorization_code”。     |
| code         | 必需     | 从授权服务器收到的授权码。                   |
| redirect_uri | 必需     | 必须和**授权请求**中提供的redirect_uri相同。 |
| client_id    | 必需     | 必须和**授权请求**中提供的client_id相同。    |

示例：

```
POST /token HTTP/1.1
Host: server.example.com
Authorization: Basic czZCaGRSa3F0MzpnWDFmQmF0M2JW
Content-Type: application/x-www-form-urlencoded
grant_type=authorization_code&code=SplxlOBeZQQYbYS6WxSbIA&redirect_uri=https%3A%2F%2Fclient%2Eexample%2Ecom%2Fcb
```



##### 访问令牌响应（E）



| 参数说明      | 是否必须 | 描述                                                         |
| ------------- | -------- | ------------------------------------------------------------ |
| access_token  | 必需     | 授权令牌，Access_Token。                                     |
| token_type    | 必需     | 表示令牌类型，该值大小写不敏感,可以是bearer类型或mac类型。   |
| expires_in    | 推荐     | 该access token的有效期，单位为秒。如果省略，则授权服务器应该通过其他方式提供过期时间，或者记录默认值。 |
| refresh_token | 可选     | 在授权续期时，获取新的Access_Token时需要提供的参数。         |
| scope         | 可选     | 表示授权范围。                                               |

示例：

```
HTTP/1.1 200 OK
Content-Type: application/json;charset=UTF-8
Cache-Control: no-store
Pragma: no-cache
{
  "access_token":"2YotnFZFEjr1zCsicMWpAA",
  "token_type":"example",
  "expires_in":3600,
  "refresh_token":"tGzv3JOkF0XG5Qx2TlKWIA",
  "example_parameter":"example_value"
}
```

[参考 QQ OAuth2 API（Authorization Code）](http://wiki.connect.qq.com/使用authorization_code获取access_token)



### 2.2、隐式许可(Implicit)



------

相比授权码许可，隐式许可少了第一步获取Authorization Code的过程，因此变得更为简单。但正因为如此也降低了安全性。授权服务器不能颁发刷新令牌。
 **适用范围**
 其适用于**没有**Server服务器来接受处理Authorization **Code的第三方应用。**
 **仅需临时访问的场景**，用户会定期在API提供者那里进行登录，OAuth客户端运行在浏览器中（Javascript、Flash等）浏览器绝对可信，因为该类型可能会将访问令牌泄露给恶意用户或应用程序。

Implicit具体的流程如下：

```bash
 +----------+
 | Resource |
 |  Owner   |
 |          |
 +----------+
      ^
      |
     (B)
 +----|-----+          Client Identifier     +---------------+
 |         -+----(A)-- & Redirection URI --->|               |
 |  User-   |                                | Authorization |
 |  Agent  -|----(B)-- User authenticates -->|     Server    |
 |          |                                |               |
 |          |<---(C)--- Redirection URI ----<|               |
 |          |          with Access Token     +---------------+
 |          |            in Fragment
 |          |                                +---------------+
 |          |----(D)--- Redirection URI ---->|   Web-Hosted  |
 |          |          without Fragment      |     Client    |
 |          |                                |    Resource   |
 |     (F)  |<---(E)------- Script ---------<|               |
 |          |                                +---------------+
 +-|--------+
   |    |
  (A)  (G) Access Token
   |    |
   ^    v
 +---------+
 |         |
 |  Client |
 |         |
 +---------+
```

#### 隐式许可流程描述

（A）客户端通过向授权端点引导资源拥有者的用户代理开始流程。客户端包括它的客户端标识、请求范围、本地状态和**重定向URI**，**一旦访问被许可（或拒绝）**授权服务器将传送用户代理**回到该URI**。
 （B）授权服务器验证资源拥有者的身份（通过用户代理），并确定资源拥有者是否授予或拒绝客户端的访问请求。
 （C）假设资源拥有者许可访问，授权服务器使用之前（在请求时或客户端注册时）提供的重定向URI重定向用户代理回到客户端。重定向URI在**URI片段中包含访问令牌**。
 （D）**用户代理**顺着重定向指示向Web托管的客户端资源发起请求。用户代理在本地保留片段信息。
 （E）Web托管的客户端资源**返回一个网页（通常是带有嵌入式脚本的HTML文档）**，该网页能够访问包含用户代理保留的片段的**完整重定向URI并提取包含在片段中的访问令牌（和其他参数）**。
 （F）用户代理在本地执行Web托管的客户端资源提供的提取访问令牌的脚本。
 （G）用户代理传送访问令牌给客户端。

#### 过程详解

------

##### 授权请求

| 参数          | 是否必须 | 含义                                                         |
| ------------- | -------- | ------------------------------------------------------------ |
| response_type | 必需     | 授权类型，值固定为“token”。                                  |
| client_id     | 必需     | 客户端标识。                                                 |
| redirect_uri  | 可选     | 成功授权后的回调地址。                                       |
| scope         | 可选     | 表示授权范围。                                               |
| state         | 推荐     | client端的状态值。用于第三方应用防止CSRF攻击，成功授权后回调时会原样带回。 |

示例：

```
GET /authorize?response_type=token&client_id=s6BhdRkqt3&state=xyz&redirect_uri=https%3A%2F%2Fclient%2Eexample%2Ecom%2Fcb HTTP/1.1
Host: server.example.com
```

##### 访问令牌响应

| 参数说明     | 是否必须 | 描述                                                         |
| ------------ | -------- | ------------------------------------------------------------ |
| access_token | 必需     | 授权令牌，Access_Token。                                     |
| token_type   | 必需     | 表示令牌类型，该值大小写不敏感,可以是bearer类型或mac类型。   |
| expires_in   | 推荐     | 该access token的有效期，单位为秒。如果省略，则授权服务器应该通过其他方式提供过期时间，或者记录默认值。 |
| scope        | 可选     | 表示授权范围。                                               |
| state        | 可选     | client端的状态值。用于第三方应用防止CSRF攻击，成功授权后回调时会原样带回。 |

示例：
 \#后的信息不会回传到服务端example.com/cb中。

```
HTTP/1.1 302 Found
Location: http://example.com/cb#access_token=2YotnFZFEjr1zCsicMWpAA&state=xyz&token_type=example&expires_in=3600
```

[参考 QQ OAuth2 API（Implicit）](http://wiki.connect.qq.com/使用implicit_grant方式获取access_token)



### 2.3、资源拥有者密码凭据许可(Resource Owner Password Credentials)



------

资源拥有者向客户端提供自己的用户名和密码。客户端使用这些信息，向授权服务器请求授权。
 **适用范围**
 这种模式会直接将用户密码暴露给客户端，一般适用于Resource server高度信任第三方Client的情况下。

Resource Owner Password Credentials具体的流程如下：

```
 +----------+
 | Resource |
 |  Owner   |
 |          |
 +----------+
      v
      |    Resource Owner
     (A) Password Credentials
      |
      v
 +---------+                                  +---------------+
 |         |>--(B)---- Resource Owner ------->|               |
 |         |         Password Credentials     | Authorization |
 | Client  |                                  |     Server    |
 |         |<--(C)---- Access Token ---------<|               |
 |         |    (w/ Optional Refresh Token)   |               |
 +---------+                                  +---------------+
```

#### 资源拥有者密码凭据许可流程描述

（A）资源拥有者提供给客户端它的用户名和密码。
 （B）通过包含从资源拥有者处接收到的凭据，客户端从授权服务器的令牌端点请求访问令牌。当发起请求时，客户端与授权服务器进行身份验证。
 （C）授权服务器对客户端进行身份验证，验证资源拥有者的凭证，如果有效，颁发访问令牌。

#### 过程详解

------

##### 访问令牌请求

| 参数       | 是否必须 | 含义                           |
| ---------- | -------- | ------------------------------ |
| grant_type | 必需     | 授权类型，值固定为“password”。 |
| username   | 必需     | 资源拥有者的用户名。           |
| password   | 必需     | 资源拥有者的密码。             |
| scope      | 可选     | 表示授权范围。                 |

示例：

```
POST /token HTTP/1.1
Host: server.example.com
Authorization: Basic czZCaGRSa3F0MzpnWDFmQmF0M2JW
Content-Type: application/x-www-form-urlencoded

grant_type=password&username=johndoe&password=A3ddj3w
```

##### 访问令牌响应

[与Authorization Code中的访问令牌响应一样](https://www.cnblogs.com/ddrsql/p/7789064.html#AccessTokenResponse)

```
HTTP/1.1 200 OK
Content-Type: application/json;charset=UTF-8
Cache-Control: no-store
Pragma: no-cache

{
"access_token":"2YotnFZFEjr1zCsicMWpAA",
"token_type":"example",
"expires_in":3600,
"refresh_token":"tGzv3JOkF0XG5Qx2TlKWIA",
"example_parameter":"example_value"
}
```



### 2.4、客户端凭据许可(Client Credentials)



------

客户端（Client）请求授权服务器验证，通过验证就发access token，Client直接以已自己的名义去访问Resource server的一些受保护资源。
 **适用范围**
 以客户端本身而不是单个用户的身份来读取、修改资源服务器所开放的API。

Client Credentials具体的流程如下：

```
 +---------+                                  +---------------+
 |         |                                  |               |
 |         |>--(A)- Client Authentication --->| Authorization |
 | Client  |                                  |     Server    |
 |         |<--(B)---- Access Token ---------<|               |
 |         |                                  |               |
 +---------+                                  +---------------+
```

#### 客户端凭据许可流程描述

（A）客户端与授权服务器进行身份验证并向令牌端点请求访问令牌。
 （B）授权服务器对客户端进行身份验证，如果有效，颁发访问令牌。

#### 过程详解

------

##### 访问令牌请求

| 参数       | 是否必须 | 含义                                     |
| ---------- | -------- | ---------------------------------------- |
| grant_type | 必需     | 授权类型，值固定为“client_credentials”。 |
| scope      | 可选     | 表示授权范围。                           |

示例：
 客户端身份验证两种方式
 1、Authorization: Basic czZCaGRSa3F0Mzo3RmpmcDBaQnIxS3REUmJuZlZkbUl3。
 2、client_id（客户端标识），client_secret（客户端秘钥）。

```
POST /token HTTP/1.1
Host: server.example.com
Authorization: Basic czZCaGRSa3F0MzpnWDFmQmF0M2JW
Content-Type: application/x-www-form-urlencoded

grant_type=client_credentials
```

##### 访问令牌响应

[参数介绍参见 Authorization Code中的访问令牌响应](https://www.cnblogs.com/ddrsql/p/7789064.html#AccessTokenResponse)
 刷新令牌不应该包含在内。

```
HTTP/1.1 200 OK
Content-Type: application/json;charset=UTF-8
Cache-Control: no-store
Pragma: no-cache

{
"access_token":"2YotnFZFEjr1zCsicMWpAA",
"token_type":"example",
"expires_in":3600,
"example_parameter":"example_value"
}
```

[参考 微信公众平台获取access_token（Client Credentials Grant）](https://mp.weixin.qq.com/wiki?t=resource/res_main&id=mp1421140183)