# OAuth,Token和JWT



原文地址: <https://www.jianshu.com/p/9f80be6ba2e9>

## 前言

想一想,代码写多了,东西看多了之后,对很多名词和概念的名字很熟,但是不清楚名字背后的内容以及它们之间的联系?

比如OAuth,Token和JWT,下面是常见的一些疑问,本文将试着来解答这些问题:

1. 什么是OAuth?
2. 什么是Token?
3. 什么又是JWT？
4. 三者之间又是什么关系?

## 1. OAuth

### 1.1 定义

**OAuth**: An open protocol to allow secure authorization in a simple and standard method from web, mobile and desktop applications. 也就是说OAuth是一个开放标准,提供了一种简单和标准的安全授权方法,允许用户无需将某个网站的用户名密码提供给第三方应用就可以让该第三方应用访问该用户在某网站上的某些特定信息(如简单的个人信息)。

### 1.2 历史

-  [OAuth 1.0](https://tools.ietf.org/html/rfc5849): 2010年4月发布
-  [OAuth 2.0](https://tools.ietf.org/html/rfc6750): 2012年10月发布,不兼容OAuth 1.0

### 1.3 OAuth 2.0 协议处理流程

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

具体的详细介绍可以参考软大师的: [理解OAuth 2.0](http://www.ruanyifeng.com/blog/2014/05/oauth_2_0.html)

## 2. Token

Token就是获取信息的凭证,如上述的**Access Token**,关于Token的具体使用有相应的RFC文件指导: [The OAuth 2.0 Authorization Framework: Bearer Token Usage](https://tools.ietf.org/html/rfc6750)

### 2.1 Access Token 类型

Token的类型可分为两种:

1.  `bearer`. 包含一个简单的Token字符串.
2.  `mac`. 由消息授权码(Message Authentication Code)和Token组成.

示例:

```
// bearer
GET /resource/1 HTTP/1.1
Host: example.com
Authorization: Bearer mF_9.B5f-4.1JqM

// mac     
GET /resource/1 HTTP/1.1
Host: example.com
Authorization: MAC id="h480djs93hd8",
                   nonce="274312:dj83hs9s",
               mac="kDZvddkndxvhGRXZhvuDjEhGeE="
```

### 2.2 认证请求方式

使用Token的认证请求的方式有三种,客户端可以选择一种来实现,但是不能同时使用多种:

1. 放在请求头
2. 放在请求体
3. 放在URI

详细如下:

#### 2.2.1 放在请求头

放在Header的`Authorization`中,并使用`Bearer`开头:

```
GET /resource HTTP/1.1
Host: server.example.com
Authorization: Bearer mF_9.B5f-4.1JqM
```

#### 2.2.2 放在请求体

放在body中的`access_token`参数中,并且满足以下条件:

1. HTTP请求头的`Content-Type`设置成`application/x-www-form-urlencoded`.
2. Body参数是`single-part`.
3. HTTP请求方法应该是推荐可以携带Body参数的方法,比如`POST`,不推荐`GET`.

示例:

```
POST /resource HTTP/1.1
Host: server.example.com
Content-Type: application/x-www-form-urlencoded

access_token=mF_9.B5f-4.1JqM
```

#### 2.2.3 放在URI

放在uri中的`access_token`参数中

```
GET /resource?access_token=mF_9.B5f-4.1JqM
Host: server.example.com
```

## 3. JWT

**JWT**: JSON Web Tokens, 这是一个开放的标准,规定了一种Token实现方式,以JSON为格式,相应的RFC文件为: [JSON Web Token (JWT)](https://tools.ietf.org/html/rfc7519)

JWT的结构分为三个部分:

-  **Header**: 存放Token类型和加密的方法
-  **Payload**: 包含一些用户身份信息.
-  **Signature**: 签名是将前面的Header,Payload信息以及一个密钥组合起来并使用Header中的算法进行加密

最终生成的是一个有两个`.`号连接的字符串,前两个部分是Header和Payload的Base64编码,最后一个是签名,如下:

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.MejVLl-m7KMfaay0nXxDWGEVPWsQ2f6SZnTvq4fXaLI
```

详细内容参考: [Introduction to JSON Web Tokens](https://jwt.io/introduction/)

## 4. 问题解答

### 4.1 什么是OAuth?

也就是说OAuth是一个开放标准,提供了一种简单和标准的安全授权方法,允许用户无需将某个网站的用户名密码提供给第三方应用就可以让该第三方应用访问该用户在某网站上的某些特定信息(如简单的个人信息),现在一般用的是OAuth 2.0(不兼容1.0).

### 4.2 什么是Token?

Token就是获取信息的凭证,如上述的**Access Token**,让客户端无需用户密码即可获取用户授权的某些资源.

### 4.3 什么又是JWT？

JSON Web Tokens, 这是一个开放的标准,规定了一种Token实现方式,以JSON为格式.

### 4.4 三者之间又是什么关系?

这三个相互连接且是由大到小的一种关系,OAuth规定授权流程,Token为其中一环的一个信息载体,具体的一种实现方式由JWT规定.

## 5. Reference

1. [OAuth](https://en.wikipedia.org/wiki/OAuth)
2. [理解OAuth 2.0](http://www.ruanyifeng.com/blog/2014/05/oauth_2_0.html)
3. [JWT](https://jwt.io/)
4. [What is the difference between OAuth based and Token based authentication?](https://stackoverflow.com/a/34930402)

作者：keith666

链接：https://www.jianshu.com/p/9f80be6ba2e9

来源：简书

简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。