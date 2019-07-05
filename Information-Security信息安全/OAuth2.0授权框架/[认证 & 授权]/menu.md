# [认证 & 授权]随笔分类 

- [认证 & 授权]

认证 & 授权相关的概念：OAtuh2,JSON Web Token,OpenId Connect。

[[认证 & 授权\] 6. Permission Based Access Control](https://www.cnblogs.com/linianhui/p/permission-based-access-control.html)

摘要:  在前面5篇博客中介绍了OAuth2和OIDC（OpenId Connect），其作用是授权和认证。那么当我们得到OAuth2的Access  Token或者OIDC的Id  Token之后，我们的资源服务如何来验证这些token是否有权限来执行对资源的某一项操作呢？比如我有一个API，/books，它具[阅读全文](https://www.cnblogs.com/linianhui/p/permission-based-access-control.html)

posted @ [2018-01-13 15:23](https://www.cnblogs.com/linianhui/p/permission-based-access-control.html) blackheart 阅读(3196) | [评论 (14)](https://www.cnblogs.com/linianhui/p/permission-based-access-control.html#FeedBack)  [编辑](https://i.cnblogs.com/EditPosts.aspx?postid=8278914)



[[认证 & 授权\] 5. OIDC（OpenId Connect）身份认证（扩展部分）](https://www.cnblogs.com/linianhui/p/openid-connect-extension.html)

摘要:  在上一篇[认证授权] 4.OIDC（OpenId Connect）身份认证（核心部分）中解释了OIDC的核心部分的功能，即OIDC如何提供id  token来用于认证。由于OIDC是一个协议族，如果只是简单的只关注其核心部分其实是不足以搭建一个完整的OIDC服务的。本篇则解释下OIDC中比较常用的几个[阅读全文](https://www.cnblogs.com/linianhui/p/openid-connect-extension.html)

posted @ [2017-11-16 13:39](https://www.cnblogs.com/linianhui/p/openid-connect-extension.html) blackheart 阅读(6447) | [评论 (17)](https://www.cnblogs.com/linianhui/p/openid-connect-extension.html#FeedBack)  [编辑](https://i.cnblogs.com/EditPosts.aspx?postid=7843674)



[[认证 & 授权\] 4. OIDC（OpenId Connect）身份认证（核心部分）](https://www.cnblogs.com/linianhui/p/openid-connect-core.html)

摘要: 1  什么是OIDC？ 看一下官方的介绍（http://openid.net/connect/）： OpenID Connect 1.0 is a  simple identity layer on top of the OAuth 2.0 protocol. It allows  Clients to [阅读全文](https://www.cnblogs.com/linianhui/p/openid-connect-core.html)

posted @ [2017-05-30 09:18](https://www.cnblogs.com/linianhui/p/openid-connect-core.html) blackheart 阅读(28916) | [评论 (33)](https://www.cnblogs.com/linianhui/p/openid-connect-core.html#FeedBack)  [编辑](https://i.cnblogs.com/EditPosts.aspx?postid=6438673)



[[认证 & 授权\] 3. 基于OAuth2的认证（译）](https://www.cnblogs.com/linianhui/p/authentication-based-on-oauth2.html)

摘要: OAuth  2.0  规范定义了一个授权（delegation）协议，对于使用Web的应用程序和API在网络上传递授权决策非常有用。OAuth被用在各钟各样的应用程序中，包括提供用户认证的机制。这导致许多的开发者和API提供者得出一个OAuth本身是一个认证协议的错误结论，并将其错误的使用于此。让我们[阅读全文](https://www.cnblogs.com/linianhui/p/authentication-based-on-oauth2.html)

posted @ [2017-04-09 16:59](https://www.cnblogs.com/linianhui/p/authentication-based-on-oauth2.html) blackheart 阅读(10744) | [评论 (7)](https://www.cnblogs.com/linianhui/p/authentication-based-on-oauth2.html#FeedBack)  [编辑](https://i.cnblogs.com/EditPosts.aspx?postid=6229364)



[[认证 & 授权\] 2. OAuth2授权（续） & JWT(JSON Web Token)](https://www.cnblogs.com/linianhui/p/oauth2-extensions-protocol-and-json-web-token.html)

摘要: 1  RFC6749还有哪些可以完善的？ 1.1 撤销Token 在上篇[认证授权] 1.OAuth2授权  中介绍到了OAuth2可以帮我们解决第三方Client访问受保护资源的问题，但是只提供了如何获得access_token，并未说明怎么来撤销一个access_token。关于这部分OAuth2单[阅读全文](https://www.cnblogs.com/linianhui/p/oauth2-extensions-protocol-and-json-web-token.html)

posted @ [2017-04-03 03:56](https://www.cnblogs.com/linianhui/p/oauth2-extensions-protocol-and-json-web-token.html) blackheart 阅读(14589) | [评论 (9)](https://www.cnblogs.com/linianhui/p/oauth2-extensions-protocol-and-json-web-token.html#FeedBack)  [编辑](https://i.cnblogs.com/EditPosts.aspx?postid=6660464)



[[认证 & 授权\] 1. OAuth2授权](https://www.cnblogs.com/linianhui/p/oauth2-authorization.html)

摘要: 1  OAuth2解决什么问题的？  举个栗子先。小明在QQ空间积攒了多年的照片，想挑选一些照片来打印出来。然后小明在找到一家提供在线打印并且包邮的网站（我们叫它PP吧（Print  Photo缩写 😂））。 那么现在问题来了，小明有两个方案来得到打印的服务。 针对方案（1）：小明要去下载这些照片，然后[阅读全文](https://www.cnblogs.com/linianhui/p/oauth2-authorization.html)