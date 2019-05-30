# 随笔分类 - Asp.Net Core



[使用Visual Studio Code开发Asp.Net Core WebApi学习笔记（十）-- 发布(Windows)](https://www.cnblogs.com/niklai/p/5735008.html)

摘要:  本篇将在这个系列演示的例子上继续记录Asp.Net Core在Windows上发布的过程。 Asp.Net  Core在Windows上可以采用两种运行方式。一种是自托管运行，另一种是发布到IIS托管运行。 第一部分、自托管 一、依赖.Net  Core环境 修改 project.json 文件内容，增[阅读全文](https://www.cnblogs.com/niklai/p/5735008.html)

posted @ [2016-08-04 00:10](https://www.cnblogs.com/niklai/p/5735008.html) 星辰.Lee 阅读(7354) | [评论 (3)](https://www.cnblogs.com/niklai/p/5735008.html#FeedBack)  [编辑](https://i.cnblogs.com/EditPosts.aspx?postid=5735008)



[使用Visual Studio Code开发Asp.Net Core WebApi学习笔记（九）-- 单元测试](https://www.cnblogs.com/niklai/p/5722175.html)

摘要:  本篇将结合这个系列的例子的基础上演示在Asp.Net Core里如何使用XUnit结合Moq进行单元测试，同时对整个项目进行集成测试。  第一部分、XUnit 修改 Project.json 文件内容，增加XUnit相关的nuget包引用，并修改部分配置。  增加一个Demo类和一个测试类 打开cmd窗[阅读全文](https://www.cnblogs.com/niklai/p/5722175.html)

posted @ [2016-07-31 23:44](https://www.cnblogs.com/niklai/p/5722175.html) 星辰.Lee 阅读(2435) | [评论 (2)](https://www.cnblogs.com/niklai/p/5722175.html#FeedBack)  [编辑](https://i.cnblogs.com/EditPosts.aspx?postid=5722175)



[使用Visual Studio Code开发Asp.Net Core WebApi学习笔记（八）-- 多环境开发](https://www.cnblogs.com/niklai/p/5712984.html)

摘要:  本篇将演示Asp.Net Core如何在多环境下进行开发适配。  在一个正规的开发流程里，软件开发部署将要经过三个阶段：开发、测试、上线，对应了三个环境：开发、测试、生产。在不同的环境里，需要编写不同的代码，比如，在开发环境里，为了方便开发和调试，前段js文件和css文件不会被压缩，异常信息将会暴露得[阅读全文](https://www.cnblogs.com/niklai/p/5712984.html)

posted @ [2016-07-28 00:27](https://www.cnblogs.com/niklai/p/5712984.html) 星辰.Lee 阅读(3163) | [评论 (2)](https://www.cnblogs.com/niklai/p/5712984.html#FeedBack)  [编辑](https://i.cnblogs.com/EditPosts.aspx?postid=5712984)



[使用Visual Studio Code开发Asp.Net Core WebApi学习笔记（七）-- 结构化配置](https://www.cnblogs.com/niklai/p/5701905.html)

摘要:  本篇将记录.Net Core里颇有特色的结构化配置的使用方法。  相比较之前通过Web.Config或者App.Config配置文件里使用xml节点定义配置内容的方式，.Net  Core在配置系统上发生了很大的变化，具有了配置源多样化、更加轻量、扩展性更好的特点。 第一部分、基于键值对的配置 如果要使[阅读全文](https://www.cnblogs.com/niklai/p/5701905.html)

posted @ [2016-07-25 23:58](https://www.cnblogs.com/niklai/p/5701905.html) 星辰.Lee 阅读(2219) | [评论 (1)](https://www.cnblogs.com/niklai/p/5701905.html#FeedBack)  [编辑](https://i.cnblogs.com/EditPosts.aspx?postid=5701905)



[使用Visual Studio Code开发Asp.Net Core WebApi学习笔记（六）-- 依赖注入](https://www.cnblogs.com/niklai/p/5683219.html)

摘要:  本篇将介绍Asp.Net Core中一个非常重要的特性：依赖注入，并展示其简单用法。 第一部分、概念介绍 Dependency  Injection：又称依赖注入，简称DI。在以前的开发方式中，层与层之间、类与类之间都是通过new一个对方的实例进行相互调用，这样在开发过程中有一个好处，可以清晰的知道在[阅读全文](https://www.cnblogs.com/niklai/p/5683219.html)

posted @ [2016-07-22 23:52](https://www.cnblogs.com/niklai/p/5683219.html) 星辰.Lee 阅读(5813) | [评论 (7)](https://www.cnblogs.com/niklai/p/5683219.html#FeedBack)  [编辑](https://i.cnblogs.com/EditPosts.aspx?postid=5683219)



[使用Visual Studio Code开发Asp.Net Core WebApi学习笔记（五）-- Filter](https://www.cnblogs.com/niklai/p/5676632.html)

摘要:  在上一篇里，介绍了中间件的相关内容和使用方法。本篇将介绍Asp.Net Core  MVC框架的过滤器的相关内容和使用方法，并简单说明一下与中间件的区别。 第一部分、MVC框架内置过滤器 下图展示了Asp.Net Core  MVC框架默认实现的过滤器的执行顺序： Authorization Filte[阅读全文](https://www.cnblogs.com/niklai/p/5676632.html)

posted @ [2016-07-18 00:05](https://www.cnblogs.com/niklai/p/5676632.html) 星辰.Lee 阅读(6036) | [评论 (3)](https://www.cnblogs.com/niklai/p/5676632.html#FeedBack)  [编辑](https://i.cnblogs.com/EditPosts.aspx?postid=5676632)



[使用Visual Studio Code开发Asp.Net Core WebApi学习笔记（四）-- Middleware](https://www.cnblogs.com/niklai/p/5665272.html)

摘要:  本文记录了Asp.Net管道模型和Asp.Net Core的Middleware模型的对比，并在上一篇的基础上增加Middleware功能支持。  在演示Middleware功能之前，先要了解一下Asp.Net管道模型发生了什么样的变化。 第一部分：管道模型 1. Asp.Net管道  在之前的Asp.[阅读全文](https://www.cnblogs.com/niklai/p/5665272.html)

posted @ [2016-07-14 23:08](https://www.cnblogs.com/niklai/p/5665272.html) 星辰.Lee 阅读(12258) | [评论 (9)](https://www.cnblogs.com/niklai/p/5665272.html#FeedBack)  [编辑](https://i.cnblogs.com/EditPosts.aspx?postid=5665272)



[使用Visual Studio Code开发Asp.Net Core WebApi学习笔记（三）-- Logger](https://www.cnblogs.com/niklai/p/5662094.html)

摘要:  本篇是在上一篇的基础上添加日志功能，并记录NLog在Asp.Net Core里的使用方法。 第一部分：默认Logger支持  一、project.json添加日志包引用，并在cmd窗口使用 dotnet restore 命令还原包文件。  二、修改Startup.cs文件，添加命令行窗口和调试窗口的日志[阅读全文](https://www.cnblogs.com/niklai/p/5662094.html)

posted @ [2016-07-12 00:07](https://www.cnblogs.com/niklai/p/5662094.html) 星辰.Lee 阅读(5555) | [评论 (6)](https://www.cnblogs.com/niklai/p/5662094.html#FeedBack)  [编辑](https://i.cnblogs.com/EditPosts.aspx?postid=5662094)



[使用Visual Studio Code开发Asp.Net Core WebApi学习笔记（二）-- Web Api Demo](https://www.cnblogs.com/niklai/p/5658876.html)

摘要:  在上一篇里，我已经建立了一个简单的Web-Demo应用程序。这一篇将记录将此Demo程序改造成一个Web Api应用程序。  一、添加ASP.NET Core MVC包 1. 在project.json文件添加Microsoft.AspNetCore.Mvc包 2.  在cmd窗口使用 dotnet r[阅读全文](https://www.cnblogs.com/niklai/p/5658876.html)

posted @ [2016-07-10 23:23](https://www.cnblogs.com/niklai/p/5658876.html) 星辰.Lee 阅读(5478) | [评论 (5)](https://www.cnblogs.com/niklai/p/5658876.html#FeedBack)  [编辑](https://i.cnblogs.com/EditPosts.aspx?postid=5658876)



[使用Visual Studio Code开发Asp.Net Core WebApi学习笔记（一）-- 起步](https://www.cnblogs.com/niklai/p/5655061.html)

摘要:  本文记录了在Windows环境下安装Visual Studio Code开发工具、.Net Core 1.0  SDK和开发一个简单的Web-Demo网站的全过程。 一、安装Visual Studio Code 安装文件下载地址：VS  Code，当前最新版本是1.3。 推荐安装最新版，因为附带Debu[阅读全文](https://www.cnblogs.com/niklai/p/5655061.html)

posted @ [2016-07-09 01:15](https://www.cnblogs.com/niklai/p/5655061.html) 星辰.Lee 阅读(12445) | [评论 (8)](https://www.cnblogs.com/niklai/p/5655061.html#FeedBack)  [编辑](https://i.cnblogs.com/EditPosts.aspx?postid=5655061)