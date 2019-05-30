


###                                                                   ASP.NET中的 Web API和MVC运行机制有什么差异？消息处理管道是否一致？                                 [问题点数：100分，结帖人wanglui1990]                    

 		https://bbs.csdn.net/topics/392366717 		 		 	

-  			 			 				[收藏帖子](javascript:;) 		
- [回复](javascript:;)

- ​                         [               ![img](assets/1_wanglui1990.jpg)                 ![img](assets/10.png)             ](https://my.csdn.net/wanglui1990)                                                         [tb_](https://my.csdn.net/wanglui1990)                                   ![Bbs2](assets/bbs2.png)                                                                                    结帖率 83.33%                

  ​                  我个人的理解是：Web API 相比MVC 只是去除了Controller与View的关联，不用再去解析ViewDescription（Web API也是没有ViewDescription的吧）。    具体些的，Web API和MVC运行机制有什么差异吗？消息处理管道是否一致？  如果知道可以说一下，有链接也可以。                                                                                                                                                                          *0*                      2018-04-25 22:26:07                             回复数 *5*             [只看楼主](https://bbs.csdn.net/topics/392366717?list=lz)           [引用](javascript:;)           [举报](javascript:;)           楼主                     

-  								  									[ 										![img](assets/1_sp1234.jpg) 											![img](assets/14.png) 									](https://my.csdn.net/sp1234) 									 								 							 								 									[以专业开发人员为伍](https://my.csdn.net/sp1234) 								 								 									![Bbs12](assets/bbs12.png) 									 									 								 							 								 									 											![Blank](assets/jinshi.png) 											 									 									 											![Blank](assets/tongpian.png) 											 									 									 											![Blank](assets/honghua.png) 											 									 									 											![Blank](assets/huanghua.png) 											 									 								 						

      					 							wepapi就是 ashx 进行一点必要的封装（发序列化、序列化），根本就跟 html、页面无关。而 Controller、View 都是为了写页面的功能。    如果你非要说“消息处理管道一致”所以哪一个就没有必要的话，那么其实你就再说 MVC 根本就是毫无意义的东西，用更简单的 Web API 就行了，甚至连序列化、反序列化一下输入输出参数都没有必要、直接用 ashx 就够了，是不是？难道说你还要追求繁琐的东西而扔掉简单的东西？    看不到高级的功能差异、只能看到”消息处理管道“，这个是没有搞明白 MVC 模式到底是什么。  							 								 							 					 					 						 							 								 									 								*0* 							 							2018-04-26 07:15:48 						 						 								[只看TA](https://bbs.csdn.net/topics/392366717?list=982381) 							[引用](javascript:;) 							[举报](javascript:;) 							#1    得分 30 						 					 						 							[细说Asp.Net Web API消息处理管道（二）](https://blog.csdn.net/tmchongye/article/details/63981991) 							[在细说Asp.Net Web API消息处理管道这篇文章中，通过翻看源码和实例验证的方式，我们知道了Asp.Net  Web API消息处理管道的组成类型以及Asp.Net Web  API是如何创建消息处理管道的。本文在上篇的基础上进行一个补充，谈谈在WebHost寄宿方式和SelfHost寄宿方式下，请求是如何进入到Asp.Net  Web API的消息处理管道的。  WebHost寄宿方式](https://blog.csdn.net/tmchongye/article/details/63981991) 						 				

-  								  									[ 										![img](https://profile.csdnimg.cn/1/8/D/1_sp1234) 											![img](https://g.csdnimg.cn/static/user-reg-year/2x/14.png) 									](https://my.csdn.net/sp1234) 									 								 							 								 									[以专业开发人员为伍](https://my.csdn.net/sp1234) 								 								 									![Bbs12](https://csdnimg.cn/jifen/images/xunzhang/jianzhang/bbs12.png) 									 									 								 							 								 									 											![Blank](https://csdnimg.cn/jifen/images/xunzhang/xunzhang/jinshi.png) 											 									 									 											![Blank](https://csdnimg.cn/jifen/images/xunzhang/xunzhang/tongpian.png) 											 									 									 											![Blank](https://csdnimg.cn/jifen/images/xunzhang/xunzhang/honghua.png) 											 									 									 											![Blank](https://csdnimg.cn/jifen/images/xunzhang/xunzhang/huanghua.png) 											 									 								 						

      					 							 WebAPI 就是封装一下 ashx 成为 API 编程模式，而 MVC 就是封装一下 ashx 成为 MVC 编程模式，都是为了程序员自认为高级点的现成编程模式。如果你知识中根本没有”API 长什么样子、MVC 模式是什么机制“的概念，那么你议论的（而不是技术讨论的）就全都是无关的什么“消息管道”之类的话了。  							 								 							 					 					 						 							 								 									 								*0* 							 							2018-04-26 07:28:11 						 						 								[只看TA](https://bbs.csdn.net/topics/392366717?list=982381) 							[引用](javascript:;) 							[举报](javascript:;) 							#2    得分 0 						 					 						 							[WebForm页面生命周期及asp.net运行机制](https://blog.csdn.net/mss359681091/article/details/51882688) 							[﻿﻿ 1.先上几张原理图着重理解：              现在针对第四副图原理进行解析： 流程: 1.浏览器发送请求 2.服务器软件（IIS）接收,它最终的目的就是为了向客户输出它请求的动态页面生成的html代码。 3.服务器不会处理类和动态页面，所以找扩展程序  4.交给FrameWork，它其中有个类HttpRuntime，其中有个ProcessRequest](https://blog.csdn.net/mss359681091/article/details/51882688) 						 				

-  								  									[ 										![img](assets/1_masanaka.jpg) 											![img](assets/16.png) 									](https://my.csdn.net/masanaka) 									 								 							 								 									[masanaka](https://my.csdn.net/masanaka) 								 								 									![Bbs5](assets/bbs5.png) 									 									 								 							 						

      					 							如果就框架的处理机制而言，官网上也没有相关的对比文章，网上能找到的对比，基本上都是基于应用层面的差别。  但是你从web api的发展来看的话，基本也能看出他们之间的一些关系吧。  最早web api是作为一个feature包含在mvc框架里的，应该是在mvc4的时候，web api作为被单独提出来，和mvc成为同级别的框架推广，是的，为了更方便的开发rest服务。  到了asp.net core, web api又被并回了mvc框架，本质上没有区别了。  www.dotnettricks.com/learn/webapi/difference-between-aspnet-mvc-and-aspnet-web-api  框架源码github上有，一搜就能找到，如果你真的想了解差别的话，看源码吧。  							 								 							 					 					 						 							 								 									 								*0* 							 							2018-04-26 10:12:26 						 						 								[只看TA](https://bbs.csdn.net/topics/392366717?list=543765) 							[引用](javascript:;) 							[举报](javascript:;) 							#3    得分 30 						 					 						 							[传统WebForm网站和MVC网站运行机制对比](https://blog.csdn.net/z15732621582/article/details/53870440) 							[先上图看对比：           一、运行机制    当我们访问一个网站的时候，浏览器和服务器都是做了哪些动作呢     （一）WebForm网站运行机制    假设为：www.baidu.com/index.aspx   1、Http请求（物理地址：index.aspx）   ①发送请求     浏览器向服务器发送请求报文，此时由IIS虚拟目录接受。（通过配置过IIS，把](https://blog.csdn.net/z15732621582/article/details/53870440) 						 				

-  								  									[ 										![img](assets/1_daixf_csdn.jpg) 											![img](assets/18.png) 									](https://my.csdn.net/daixf_csdn) 									 								 							 								 									[圣殿骑士18](https://my.csdn.net/daixf_csdn) 								 								 									![Bbs7](assets/bbs7.png) 									 									 								 							 								 									 											![Blank](assets/huanghua.png) 											 									 									 											![Blank](assets/lanhua.png) 											 									 								 						

      					 							据说webapi现在又合并进去了？  							 								 							 					 					 						 							 								 									 								*0* 							 							2018-04-26 11:28:12 						 						 								[只看TA](https://bbs.csdn.net/topics/392366717?list=120590) 							[引用](javascript:;) 							[举报](javascript:;) 							#4    得分 10 						 					 						 							[Asp.Net Mvc 运行机制原理分析](https://blog.csdn.net/vs2008ASPNET/article/details/81979284) 							[最近一段时间接手过的项目都是基于Asp.Net的，以前对aspnet运行机制有一个大概的了解，总觉得不够透彻，按自己的理解来分析一下。  Asp.Net 运行机制  理解mvc运行原理的前提是要了解aspnet运行原理，这方面网上资料多如牛毛，我这里就大致说一下aspnet生命周期    Http请求到IIS后，如果是静态资源则IIS读取后返回客户端，动态请求被isap.dll 转发自net托管平...](https://blog.csdn.net/vs2008ASPNET/article/details/81979284) 						 				

-  								  									[ 										![img](assets/1_hanjun0612.jpg) 											![img](assets/9.png) 									](https://my.csdn.net/hanjun0612) 									 								 							 								 									[正怒月神](https://my.csdn.net/hanjun0612) 								 								 									![Bbs9](assets/bbs9.png) 									 									版主 								 							 								 									 											![Blank](assets/huanghua.png) 											 									 									 											![Blank](assets/lanhua.png) 											 									 								 						

      					 							运行机制没什么差别。  但面对的方向不同。是两种不同的解决方案。  							 								                             厂家生产环保橡胶鼠标垫定制图案logo热转印超大号鼠标垫定制批发                                                                                                               广告                 1688热销                                                            							 					 					 						 							 								 									 								*0* 							 							2018-04-26 11:48:07 						 						 								[只看TA](https://bbs.csdn.net/topics/392366717?list=16718956) 							[引用](javascript:;) 							[举报](javascript:;) 							#5    得分 30 						 					 				

 		 		 		 	

-  			 			 				 		





- [编辑](javascript:void(0);)
- [预览](javascript:void(0);)



- [粗体](https://bbs.csdn.net/topics/392366717)
- [斜体](https://bbs.csdn.net/topics/392366717)
- [下划线](https://bbs.csdn.net/topics/392366717)
- \---------------
- [字体大小](https://bbs.csdn.net/topics/392366717)
- [字体颜色](https://bbs.csdn.net/topics/392366717)
- \---------------
- [图片](https://bbs.csdn.net/topics/392366717)
- [链接](https://bbs.csdn.net/topics/392366717)
- \---------------
- [左对齐](https://bbs.csdn.net/topics/392366717)
- [居中对齐](https://bbs.csdn.net/topics/392366717)
- [右对齐](https://bbs.csdn.net/topics/392366717)
- \---------------
- [引用](https://bbs.csdn.net/topics/392366717)
- [代码](https://bbs.csdn.net/topics/392366717)
- \---------------
- [QQ](https://bbs.csdn.net/topics/392366717)
- [monkey](https://bbs.csdn.net/topics/392366717)
- [onion](https://bbs.csdn.net/topics/392366717)
- \---------------
- [清除格式](https://bbs.csdn.net/topics/392366717)



​                                                        每天回帖即可获得10分可用分！[小技巧：教您如何更快获得可用分](https://bbs.csdn.net/help#common_problem)                             你还可以输入10000个字符                                                        

(Ctrl+Enter)                         

- 请遵守CSDN[用户行为准则](https://bbs.csdn.net/help#user_criterion)，不得违反国家法律法规 ; 转载请注明出自“CSDN（www.csdn.net）”，如是商业用途请联系原作者 ; 请不要讨论政治相关话题。

- [细说*Asp.Net* *Web* *API消息处理管道*(二) - tmchongye的专..._CSDN博客](https://blog.csdn.net/tmchongye/article/details/63981991)

  在细说*Asp.Net* *Web* *API消息处理管道*这篇文章中,通过翻看源码和实例验证的方式,我们知道了*Asp.Net* *Web* *API消息处理管道*的组成类型以及*Asp.Net* *Web* *API是*如何创建...

- [*Asp.Net* *Mvc 运行机制*原理分析 - vs2008*ASPNET的*专栏 - CSDN博客](https://blog.csdn.net/vs2008aspnet/article/details/81979284)

  最近一段时间接手过的项目都是基于Asp.Net的,以前对*aspnet*运行机制有一个大概...*ASP.NET中的Web* *API和MVC运行机制有什么差异?消息处理管道*是否一致?  04-25 ...

- [*Web* *APi*之*消息处理管道*(五) - weixin_34179762的博客 - CSDN博客](https://blog.csdn.net/weixin_34179762/article/details/90129589)

  *MVC有*一套请求处理*的机制*,当然*Web* *API*也有自己的一套*消息处理管道*,该*消息处理管道*...在*ASP.NET中*,我们知道,它有一个面向切面的请求管道,有19个主要的事件构成,能...

- [*ASP.NET* *Web* *API* *处理*架构 - liuxg123456789的博客 - CSDN博客](https://blog.csdn.net/liuxg123456789/article/details/78495004)

  *消息处理管道*(message handler pipeline)和控制器处理...目前在*ASP.NET* *Web* *API*里头已经内建的宿主选项有2...*MVC*4 *WebAPI*(二)——*Web* *API*工作方式 - sgear ...

- [*ASP.NET* *MVC与ASP.NET* *Web* *API*的区别 - xiaosachuchen..._CSDN博客](https://blog.csdn.net/xiaosachuchen/article/details/78530418)

  *Asp.Net中Web*Form与MVC,*Web* API模式对比  05-22 ...*WebApi和MVC有什么*区别?  08-11 阅读数 8106  https...细说Asp.Net *Web* API*消息处理管道*(二)  03-20 ...

- ​                   [ASP.NET平台下MVC与WebForm两种模式区别（图解）](https://blog.csdn.net/shenhuaikun/article/details/8769073)                 

  本文将为大家对比ASP.NET MVC与WebForm的区别，通过这种形式我们能更加了解ASP.NET MVC及其工作原理，也是为了令大家今后的开发工作更加方便，快捷。          1.传统WebForm开发中存在的一些问题，传统的ASP.NET开发中，微软的开发团队为开发者设计了一个在可视化设计器中拖放控件，编写代码响应事件的快速开发环境。然而，它所带来的负面效应是：由于控件封装了很多东

- ​                   [理解ASP.NET MVC底层*运行机制*](https://blog.csdn.net/u011148626/article/details/72530298)                 

  ASP.NET  MVC有三大组件(即模型、视图、控制器)。所谓模型，就是MVC需要提供的数据源，负责数据的访问和维护。所谓视图，就是用于显示模型中数据的用户界面。所谓控制器，就是用来处理用户的输入，负责改变模型的状态并选择适当的视图来显示模型的数据。以下是我绘制的MVC三大组件之间的交互图。   从交互图中可以看出,MVC从用户发送请求到页面呈现结果大致经过了五个步骤，分别为： (1).

- ​                   [ASP.NET Core 运行原理剖析](https://blog.csdn.net/sD7O95O/article/details/78126384)                 

  1.1. 概述在ASP.NET Core之前，ASP.NET  Framework应用程序由IIS加载。Web应用程序的入口点由InetMgr.exe创建并调用托管。以初始化过程中触发HttpApplication.Application_Start()事件。开发人员第一次执行代码的机会是处理Application_StartGlobal.asax中的事件。在ASP.NET  Core中，Global



- ​                   [Asp.net MVC进入请求*管道*的过程](https://blog.csdn.net/zy0421911/article/details/51206533)                 

  一：Asp.Net MVC请求处理原理（Asp.Net <em>mvc</em>  是怎样进入请求<em>管道</em>的。） 请求IIS-ISAPIRuntimeHttpWorkRequestHttpRuntimeHttpContext找到Global文件，并且编译该文件确保Global文件中Application_Start被调用创建HttpApplication(池  栈)如果池中

- ​                   [创建ASP_NET_Web_API_2.0应用实例](https://download.csdn.net/download/jessezj/7846489)                 

  由于ASP.NET Web API具有与ASP.NET  MVC类似的编程方式，再加上目前市面上专门介绍ASP.NET Web API 的书籍少之又少（我们看到的相关内容往往是某本介绍ASP.NET  MVC的书籍“额外奉送”的），以至于很多人会觉得ASP.NET Web API仅仅是ASP.NET  MVC的一个小小的扩展而已，自身并没有太多“大书特书”的地方。而真实的情况下是：ASP.NET Web  API不仅仅具有一个完全独立的<em>消息处理</em><em>管道</em>，而且这个<em>管道</em>比为ASP.NET  MVC设计的<em>管道</em>更为复杂，功能也更为强大。虽然被命名为“ASP.NET Web  API”，但是这个<em>消息处理</em><em>管道</em>却是独立于ASP.NET平台的，这也是为<em>什么</em>ASP.NET  Web API支持多种寄宿方式的根源所在。 为 了让读者朋友们先对ASP.NET Web API具有一个感性认识，接下来我们以实例演示的形式创建一个简单的ASP.NET Web  API应用。这是一个用于实现“联系人管理”的单页Web应用，我们以Ajax的形式调用Web API实现针对联系人的CRUD操作。

- [*ASP.NET* *Web* *API* *管道*模型 - 金源 - CSDN博客](https://blog.csdn.net/jinyuan0829/article/details/38379135)

  *ASP.NET* *Web* *API是*一个独立的框架,也有着自己的一套*消息处理管道*,不管是在*Web*Host宿主环境还是在SelfHost宿主环境请求和响应都是从*消息管道*经过的,这是必经之地,...

- [*WebApi和MVC有什么*区别? - Andrewniu的博客 - CSDN博客](https://blog.csdn.net/Andrewniu/article/details/79709082)

  .NET上实现了这中框架—http://*Asp.Net* *Web* *API*...一、*MVC和WebApi*路由*机制*比较1、*MVC*里面的路由在*MVC*...*管道*不同,*webapi*可以不在iis上,这种回答显然并不能...

- [*ASP.NET* *MVC*5请求*管道*和生命周期 - milijiangjun的博客 - CSDN博客](https://blog.csdn.net/milijiangjun/article/details/81874716)

  *ASP.NET中*,请求管道有两个核心组件:IHttpModule和...Asp.net mvc 深读*mvc运行机制* - 米奇老鼠  04-18...细说Asp.Net *Web* *API消息处理管道*(二) - tmchongye...

- [*WebApi和MVC*的区别 - CSDN博客](https://blog.csdn.net/xishining/article/details/78910286)

  *机制*,实现了诸如*Mvc*Handler和ControllerFactory这种消息处理和后台控制器方法选择*机制*,*Web* *Api*除了扩展了前者以外,另外写出了一套独立的,独立于*Asp .Net的消息处理管道*...

- [*Asp.Net中Web*Form*与MVC*,*Web* *API*模式对比 - bin的专栏 - CSDN博客](https://blog.csdn.net/chen_victor/article/details/72620135)

  *web*form,*web* *mvc和web* *api都是asp.net*官方的三套框架,想对比下三者的关系,查了下资料,*web* *api*跟*web* *mvc*基本同属一脉,只是*mvc*多了一个视图渲染,网上有些博客...

- ​                   [ASP.NET之旅--浅谈Asp.net*运行机制*（一）](https://blog.csdn.net/zhang_xinxiu/article/details/10832805)                 

  很多Asp.net开发人员都有过Asp的背景，以至于我们开发程序的时候总是停留在“页面”层次思考，也就是说我们常常会只考虑我们现在所做的系统是要完成<em>什么</em>功能，是要做问卷调查网站还是个人网站，而很少在“请求级”思考，思考能不能通过编码的方式来操作一个Http请求。在跨入Asp.net后Asp有了质的飞跃，很多底层的Http请求都得到了良好的应用，这时候Asp.net不仅仅是一门开发语言，而是一个开发平台。想要能在“请求级”来实现编码，我们就不得不来说说Asp.net的内部<em>运行机制</em>。

- ​                   [Asp.net*管道*模型（管线模型）](https://blog.csdn.net/u010690818/article/details/78327873)                 

  转 http://www.cnblogs.com/kuyusea/p/4638395.html    前言　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　    为<em>什么</em>我会起这样的一个标题，其实我原本只想了解<em>asp.net</em>的<em>管道</em>模型而已，但在查看资料的时候遇到不明白的地方又横向地查阅了其他相关的资料，而收获比当初预想的大了很多。   有本篇作基础，下面两篇就更好理解了： 

- ​                   [ASP.NET Core MVC/WebAPi 模型绑定探索（转载）](https://blog.csdn.net/csdn296/article/details/80768975)                 

  原文地址：https://www.cnblogs.com/CreateMyself/p/6246977.html话题在ASP.NET  Core之前MVC和Web APi被分开，也就说其请求<em>管道</em>是独立的，而在ASP.NET  Core中，WebAPi和MVC的请求<em>管道</em>被合并在一起，当我们建立控制器时此时只有一个Controller的基类而不再是Controller和APiController。所以才有...



- ​                   [学习篇：*asp.net* *mvc* *管道*模型](https://blog.csdn.net/u010690818/article/details/78328070)                 

  转  http://www.cnblogs.com/2-18/archive/2012/11/04/2753227.html   首先，客户端发送url请求→Http://localhost/Home/Index. 这时，服务端的内核模块中的  HTTP.SYS组件监听着80端口发来的请求。HTTP.SYS访问注册表，查看来的这种请求交给谁处理，然后返回信息给HTTP.SYS。HTTP.SYS一

- ​                   [ASP.NET MVC5请求*管道*和生命周期](https://blog.csdn.net/milijiangjun/article/details/81874716)                 

   请求处理<em>管道</em> 请求<em>管道</em>是一些用于处理HTTP请求的模块组合，在ASP.NET中，请求<em>管道</em>有两个核心组件：IHttpModule和IHttpHandler。所有的HTTP请求都会进入IHttpHandler，有IHttpHandler进行最终的处理，而IHttpModule通过订阅HttpApplication对象中的事件，可以在IHttpHandler对HTTP请求进行处理之前对请求进行预处理或...

- ​                   [Asp.Net异步编程-使用了异步,性能就提升了吗?](https://blog.csdn.net/qq_16495151/article/details/84653386)                 

  Asp.Net异步编程  写在前面的话,很久没有写Blog了,不对,其实<em>一致</em>就没有怎么写过.今天有空,我也来写一篇Blog  随着.Net4.5的推出,一种新的编程方式简化了异步编程,在网上时不时的也看到各种打着Asp.Net异步编程的口号,如何提高性能,如何提高吞吐率!  好多文章都说得不清楚,甚至是错误的.只看到了一些表象,混淆概念.希望这篇文章能够能够对一部分人理解Asp.net异步编程模型...

- ​                   [在ASP.NET MVC中使用WebApi注册路由注意事项](https://blog.csdn.net/u013316491/article/details/52097413)                 

  在ASP.NET  MVC中手动添加WebApi控制器，在App_Start中创建WebApiConfig.cs类文件配置路由，在Global.asax中注册路由时应把WebApiConfig.Register(GlobalConfiguration.Configuration);放在RouteConfig.RegisterRoutes(RouteTable.Routes);前面，否则出现404错误

- ​                   [WebApi和MVC有*什么*区别？](https://blog.csdn.net/zunguitiancheng/article/details/77100222)                 

  https://www.zhihu.com/question/46369458/answer/144963042   首先要重点说的是，Web API是一种无限接近于RESTful风格的轻型框架，且不是微软提出来的，微软在.NET上实现了这中框架—http://Asp.Net  Web API，所以“微软包装”是一个极大的偏见。 就应用市场时间而论，MVC普及市场的时间比Web API时



- ​                   [Asp.Net WebAPI中Filter过滤器的使用以及执行顺序](https://blog.csdn.net/lxrj2008/article/details/77869848)                 

  转发自：http://www.cnblogs.com/UliiAn/p/5402146.html  在WEB  Api中，引入了面向切面编程（AOP）的思想，在某些特定的位置可以插入特定的Filter进行过程拦截处理。引入了这一机制可以更好地践行DRY(Don’t  Repeat Yourself)思想，通过Filter能统一地对一些通用逻辑进行处理，如：权限校验、参数加解密、参数校验等方面我们都

- ​                   [【ASP.NET】Webform与MVC开发比较](https://blog.csdn.net/u013034223/article/details/50550286)                 

  去年暑假开始，跟着一个项目，开始接触到了MVC，那时候，自己对Webform的开发还没有在项目中真正实践过，没有<em>什么</em>过渡，就跳跃到MVC开发下了。而最近，在维护的一个项目中，并没有使用MVC开发，用的是Webform开发。这两次经历的结合，引发了我对本篇博客标题的思考，即Webform与MVC开发比较。     【Webform下的开发】     通过这次对ASP.NET Webform的重

- ​                   [用.Net Core控制台模拟一个ASP.Net Core的*管道*模型](https://blog.csdn.net/only_yu_yy/article/details/78867917)                 

  在我的上几篇文章中降到了<em>asp.net</em>  core的<em>管道</em>模型，为了更清楚地理解<em>asp.net</em>  core的<em>管道</em>，再网上学习了.Net  Core控制台应用程序对其的模拟，以加深映像，同时，供大家学习参考。 首先，新建一控制台应用程序。注意是.Net Core的控制台应用程序。  然后新建一个Context类，以模拟ASP.net core中的context类，然后再Context类中添加一个Wri

- ​                   [【深入ASP.NET原理系列】--ASP.NET请求*管道*、应用程序生命周期、整体*运行机制*](https://blog.csdn.net/jumtre/article/details/50423766)                 

  微软的程序设计和相应的IDE做的很棒，让人很快就能有生产力。.NET上手容易，生产力很高，但对于一个不是那么勤奋的人，他很可能就不再进步了，没有想深入下去的动力，他不用去理解整个框架和环境是怎么执行的，因为不用明白为<em>什么</em>好像也能做好工作。 .NET的人很多人不注重实现 ,知其然不知其所以然,这样真的OK么？永远怀着一颗学徒的心，你就能不断进步！   　　我们知道在ASP.NET中,若要对AS

- ​                   [*asp.net* *web* *api*帮助文档的说明](https://blog.csdn.net/zhaoyaoxing/article/details/27690379)                 

  <em>asp.net</em> <em>web</em> <em>api</em>帮助文档的说明



- ​                   [MVC面试问题与答案](https://blog.csdn.net/dc8899/article/details/21336467)                 

  这篇文章的目的是在面试之前让你快速复习ASP.NET MVC知识。

- ​                   [ASP.NET MVC架构与实战系列之一：理解MVC底层*运行机制*](https://blog.csdn.net/Sayesan/article/details/47779517)                 

  今天，我将开启一个崭新的话题：ASP.NET MVC框架的探讨。首先，我们回顾一下ASP.NET Web  Form技术与ASP.NET  MVC的异同点，并展示各自在Web领域的优劣点。在讨论之前，我对这两种技术都非常热衷，我个人觉得在实际的项目开发中，两者都能让我们受益匪浅，因此是目前Web领域两大平行和流行的技术。我们都知道，在传统的ASP.NET  Web Form应用程序中，Microso

- ​                   [ASP.NET MVC同时支持*web*与*web**api*模式](https://blog.csdn.net/laymat/article/details/65444701)                 

  我们在创建 <em>web</em>  <em>mvc</em>项目时是不支持<em>web</em>  <em>api</em>的接口方式访问的，所以我们需要添加额外的组件来支持实现双模式。 首先我们需要准备三个<em>web</em> <em>api</em>依赖的组件（目前在.net  4/4.5版本下面测试正常，2.0暂未进行测试，需要自行测试） 1、Microsoft.AspNet.WebApi.Client.5.2.2 2、Microsoft.AspNet.WebApi.Core.5.2.2

- ​                   [Asp.net MVC WebApi项目的自动接口文档及测试功能打开方法](https://blog.csdn.net/foren_whb/article/details/78866133)                 

  首先，创建一个WebApi项目，vs会自动根据模版创建一个完整的<em>web</em><em>api</em>程序，其中包括了自动文档的一切。但是，这个功能确实关闭的。。。蛋疼。。。。偏偏还没有地方显式的告诉打开的方法和步骤。。。。无语。。。 好了，现在先说如何打开<em>web</em><em>api</em>接口的自动文档： 一：项目右键属性，选择"生成"栏目，指定接口文档xml文件的路径和名字   二：打开帮助文档子项目的配置文件，解开红框标注的配置

- ​                   [ASP.NET MVC4 Web API+VS2013 编写、发布及部署流程](https://download.csdn.net/download/huhuateng/10346342)                 

  ASP.NET MVC4 Web API+VS2013 编写、发布及部署流程，其中部署的环境包括Windows7和阿里云服务器，自己边写边整理的文档，有很高的实际参考价值，适用于新手



- ​                   [Mvc4中的WebApi的使用方式](https://blog.csdn.net/WuLex/article/details/71545471)                 

  一:简单介绍<em>什么</em>是Web  <em>api</em>REST属于一种设计风格，REST 中的  POST（新增数据），GET（取得数据），PUT（更新数据），DELETE（删除数据）来进行数据库的增删改查，而如果开发人员的应用程式符合REST原则，则它的服务为“REST风格Web服务“也称的RESRful  Web API”。微软的<em>web</em>  <em>api</em>是在vs2012上的<em>mvc</em>4项目绑定发行的，它提出的<em>web</em>  <em>api</em>是完全基于R

- ​                   [ASP.NET Web API 中的路由以及Action的选择](https://blog.csdn.net/wf824284257/article/details/79491961)                 

  ASP.NET Web API 中的路由以及Action的选择  原文更新日期：2017.11.28    导航页面  http://blog.csdn.net/wf824284257/article/details/79475115    上一步  ASP.NET Web API 中的路由  http://blog.csdn.net/wf824284257/article/details/794...

- ​                   [在ASP.NET Web API和MVC中使用Ninject](https://download.csdn.net/download/jessehua/8472281)                 

  在ASP.NET Web API和ASP.NET Web MVC中使用Ninject,

- ​                   [【ASP.NET】*管道*模型](https://blog.csdn.net/qq_26545305/article/details/70214912)                 

  众所周知，我们在使用ASP.NET创建<em>web</em>项目时，会选择ASP.NET  WebForm，或ASP.NET MVC 。而他们都是基于ASP.Net  <em>管道</em>模型的，换句话说，<em>管道</em>模型是整个<em>asp.net</em>的核心。如下图所示：                                                              一、<em>管道</em>对象模型        在System.W

- ​                   [aspnet *web**api*源码](https://download.csdn.net/download/towangjindian/10112752)                 

  压缩文件包含aspnet <em>web</em><em>api</em>源码、<em>mvc</em>4源码、<em>web</em>stack源码。



- ​                   [ASP.NET页面请求过程及生命周期*管道*事件](https://blog.csdn.net/qq_29227939/article/details/51481147)                 

  Client（发送报文：请求行+请求头+空行+请求体）  Server，由  Http.sys 监听 Http 请求 -> WAS+Metabase（通过URL确定WebApp工作进程） ->  W3WP.exe(一个应用程序池，加载Aspnet_IsAPI.dll) ->AppDomainFactory(构造 ApplicationManager)->ISAPIApplicationHo

- ​                   [基于ASP.NET MVC 4、WebApi、jQuery和FormData的多文件上传方法](https://blog.csdn.net/hulihui/article/details/71055361)                 

  介绍了一个基于ASP.NET MVC 4、WebApi、jQuery、ajax和FormData数据对象的多文件上传方法。

- ​                   [*web**api*与*mvc*的不同之处](https://blog.csdn.net/u010178308/article/details/80519911)                 

  关于<em>web</em><em>api</em>  1.以http协议传数据，可跨平台，跨终端  2.路由方面与<em>mvc</em>相同，分默认路由和特性路由  3.需要进行路由配置  4.通过ajax获得数据  5.<em>mvc</em>路由和<em>web</em><em>api</em>路由是分开的，<em>web</em><em>api</em>配置处WebApiConfig.cs，<em>mvc</em>路由配置处RouteConfig.cs   If you are familiar with ASP.NET MVC, Web API ...

- ​                   [ASP.NET Web API Selfhost宿主环境中*管道*、路由](https://blog.csdn.net/JinYuan0829/article/details/38395451)                 

  ASP.NET Web API  Selfhost宿主环境中<em>管道</em>、路由 前言 前面的几个篇幅对Web  API中的路由和<em>管道</em>进行了简单的介绍并没有详细的去说明一些<em>什么</em>，然而ASP.NET  Web  API这个框架由于宿主环境的不同在不同的宿主环境中<em>管道</em>中的实现机制和路由的处理方式有着很大的不同，所以我会将对应不同的宿主环境来分别的做出简单的讲解。   ASP.NET Web API路由、<em>管道</em> 

- ​                   [Asp.net *mvc*4 WebApi 中使用多个Post请求,无法识别的问题](https://blog.csdn.net/m0_37526041/article/details/78471325)                 

  ﻿﻿ 解决方案： 方法1：修改WebApiConfig文件   //默认配置              config.Routes.MapHttpRoute(                  name: "DefaultApi",                  routeTemplate: "<em>api</em>/{controller}/{id}",                  



- ​                   [签名来保证ASP.NET MVC OR WEBAPI的接口安全](https://blog.csdn.net/tzweilai/article/details/52763383)                 

  当我们开发一款App的时候，App需要跟后台服务进行通信获取或者提交数据。如果我们没有完善的安全机制则很容易被别用心的人伪造请求而篡改数据。 所以我们需要使用某种安全机制来保证请求的合法。现在最常用的办法是给每个http请求添加一个签名，服务端来验证签名的合法性，如果签名合法则执行响应的操作，如果签名非法则直接拒绝请求。  签名算法  签名算法一般都使用Hash散列算法，常用的有MD5，

- ​                   [对ASP.NET Core MVC 2开发*web*应用程序的一些看法](https://blog.csdn.net/crf_moonlight/article/details/81132090)                 

  netCoreMvc的大概思路  MVC模式  模型(数据)  视图(HTML页面)  控制器(操作数据, 发送到页面)  整体架构  netCoreMvc虽然说源码\架构都重写了, 但感觉还是差不多的, 将整个的<em>web</em>应用看成一个整体的应用程序, 各功能之间集成得非常好, 而且对于HTTP\Cookie\Session等封装得比较严实  依赖注入\服务配置\中间件配置等等, 设计非常精妙, 非常...

- ​                   [*asp.net* *管道*事件注册、*管道*执行步骤模拟](https://blog.csdn.net/ydm19891101/article/details/50932167)                 

  我们知道Application的<em>管道</em>有23个步骤 19个事件，这些事件都是可以对请求报文进行处理的，也就是说可以进行过滤操作的。那么，我们如何进行过滤操作呢？方法就是进行19个事件的注册，具体操作方法有以下两种。 在进一步讲解之前，我们先来了解一下19个<em>管道</em>事件 下面是请求<em>管道</em>中的19个事件.  (1)BeginRequest: 开始处理请求  (2)AuthenticateReque

- ​                   [ASP.NET Web API 学习系列（一）创建与简单的增删改查](https://blog.csdn.net/qq_33329988/article/details/78059668)                 

  之前没接触过<em>web</em>  <em>api</em>，最近项目正好要用到这个所以就了解了一下，本人是一个菜鸟，第一次写博客，写的也都是一些最基础的东西，有哪些地方写的不够好还请大家多多指点和补充，谢谢！ 创建一个Web <em>api</em> 的程序，上图哈哈（VS2015）       程序创建之后，系统会默认创建如下文件：  具体<em>web</em> <em>api</em>路由配置今天先不多说，可以参考下  http://blog.csdn.ne

- ​                   [MVC5*管道*处理模型](https://blog.csdn.net/m0_37591671/article/details/82970442)                 

  MVC5<em>管道</em>处理模型  



- ​                   [在Asp.Net MVC项目中创建一个API](https://blog.csdn.net/daiqianjie/article/details/76268711)                 

  最近在忙一个MVC项目，Leader要求创建一个API，可通过某个link获得某个记录的信息。  本来想通过View来返回一个JSON记录，当我创建Controller时发现有一个API Controller，想必是MVC已经具备这个功能了，所以抱着试一试的心态，迅速补习了一下MVC API知识，马上现学现卖。

- ​                   [Asp.net *mvc* 深读*mvc**运行机制*](https://blog.csdn.net/u013108485/article/details/51180999)                 

  通过之前的学习，貌似感觉学到一些东西，回想起来却感觉连贯不起来。每次上网都会使用别人写的功能，可我写的怎么就动起来了，如何知道我的代码在哪又怎么使得我的代码能动起来实现其相应功能，不知道其内部机制，我又乱了，沉思了.....MVC<em>运行机制</em>核心可以说是就是控制器的作用。必须配合MVC架构的规则来查找相关网页(文档)。MVC是通过“网址路径”实现的查找，即“网址路径”和“文档路径”关系是由所谓的“网址

- ​                   [*asp.net* *mvc* *管道*模型 学习总结](https://blog.csdn.net/qq_25744257/article/details/87972344)                 

  1.Http请求处理流程  处理文件  1IIS根据文件的后缀名处理html或asp  2服获取后缀名以后，寻找处理的程序，找不到或没有受到服务器端的保护 直接返还文件。（受保护的例子是 App_Code中的），  3 处理后缀名程序，称为 ISAPI 应用程序，代理作用，映射请求页面和后缀名相对的处理程序。  一个完整的HTTP请求流程：  1.用户浏览器输入地址， 	2.DNS解析(域名...

- ​                   [ASP.NET WebApi 文件上传功能](https://blog.csdn.net/moonpure/article/details/46559507)                 

  今天同事在使用ASP.NET WebApi开发一个文件上传功能时发现WebApi无法实现大文件上传功能，于是向我求助。     　　当我知道这个问题的时候第一反应是WebApi不可能不支持，于是就开始查看他的代码：         1 var config = new HttpSelfHostConfiguration("http://localhost:8080");  2

- ​                   [*asp.net* *mvc* *web**api* 实用的接口加密方法](https://blog.csdn.net/a906423355/article/details/78285237)                 

  在很多项目中，因为<em>web</em><em>api</em>是对外开放的，这个时候，我们就要得考虑接口交换数据的安全性。 　　安全机制也比较多，如andriod与<em>web</em><em>api</em> 交换数据的时候，可以走双向证书方法，但是开发成本比较大， 　　今天我们不打算介绍这方面的知识，我们说说一个较简单也较常见的安全交换机制 　　在这里要提醒读者，目前所有的加密机制都不是绝对的安全! 　　我们的目标是，任何用户或者软件获取到我们的we



- ​                   [ASP.NET中WebSocket 的使用总结](https://blog.csdn.net/sinbadxia/article/details/54571767)                 

  需求背景

- ​                   [*asp.net* MVC4 *web* API 非常简单的登录](https://blog.csdn.net/u012319493/article/details/89034874)                 

  上一篇：<em>asp.net</em> MVC2 非常简单的登录 https://blog.csdn.net/u012319493/article/details/88992791 感觉 <em>web</em> API 与 MVC 一样，都有 M、V、C，但区别如下：  MVC 中，C 继承 Controller；<em>web</em> API 中，C 继承 ApiController。 MVC 中，C 中的 Action 名没有要求；<em>web</em> ...

- ​                   [ASP.NET 使用Swagger开发Web API接口项目](https://blog.csdn.net/boonya/article/details/80321229)                 

  ASP.NET 使用Swagger开发WebApi接口项目：项目使用Web  API创建自动提供了API文档，采用<em>mvc</em>方式创建项目稍麻烦点需要手动添加WebApiConfig配置，而采用Web  API项目这些都已经生成好了。创建Web  API项目添加Swagger依赖库Swagger生成的文件项目右键属性&amp;gt;生成&amp;gt;添加XML生成配置：Web  API提供的API列表打开项目启动主页：htt...

- ​                   [MVC模式简介，以及在*asp.net*中的原理及实现](https://blog.csdn.net/mamenqi_csdn/article/details/80787153)                 

  ​                                             MVC模式简介，以及在<em>asp.net</em>中的原理及实现1.简介：    　     MVC是一种软件开发架构，它包含了很多的设计模式，最为密切是以下三种：Observer (观察者模式),  Composite（组合模式）和Strategy（策略模式）。MVC最初是在Smalltalk-80中被用来构建用户界面的。　　...

- ​                   [ASP.NET Web API实现缓存的2种方式](https://blog.csdn.net/niuyongaiai/article/details/52529690)                 

  在ASP.NET Web API中实现缓存大致有2种思路。一种是通过ETag, 一种是通过类似ASP.NET MVC中的OutputCache。 通过ETag实现缓存 首先安装cachecow.server install-package cachecow.server 在WebApiConfig中。 public  static class WebApiConfig  {    



- ​                   [.net *mvc* *web* *api*上传图片/文件并重命名](https://blog.csdn.net/zch501157081/article/details/51540854)                 

  欢迎使用Markdown编辑器写博客本Markdown编辑器使用StackEdit修改而来，用它写博客，将会带来全新的体验哦： Markdown和扩展Markdown简洁的语法 代码块高亮 图片链接和图片上传 LaTex数学公式 UML序列图和流程图 离线写博客 导入导出Markdown文件 丰富的快捷键 快捷键 加粗    Ctrl + B  斜体    Ctrl + I  引用    Ctrl

- ​                   [浅谈Asp.net *运行机制*](https://blog.csdn.net/yuchen_0515/article/details/80668270)                 

  一、Asp.net <em>运行机制</em>概述1.使用Asp.net  进行动态Web开发，编写好Web应用程序，即动态页面，并部署到Web服务器，如IIS中；2.客户端在浏览器输入地址，请求相应的动态页面；3.Web   服务器根据客户端的请求，对Web应用程序进行编译或解释，并生成HTML流，返回给客户端4.客户端浏览器解释HTML流，并显示为Web页面 二、Asp.net  <em>运行机制</em>详解             ...

- ​                   [【C#】MVC调用WebApi的Demo](https://blog.csdn.net/unclebober/article/details/86649988)                 

  MVC调用WebApi的Demo 目的 通过MVC项目调用写好的Api，完成数据库增删改查操作 WebApi项目 链接：https://blog.csdn.net/unclebober/article/details/86649800 MVC调用WebApi 遇到的问题   如何将请求的数据以JSON格式返回 当客户端调用某个Action方法并希望以JSON的格式返回请求的数据时，ASP.NET ...

- ​                   [Asp.net Web Api开发（第四篇）Help Page配置和扩展](https://blog.csdn.net/yeqi3000/article/details/52708613)                 

  为了方面APP开发人员，服务端的接口都应当提供详尽的API说明。但每次有修改，既要维护代码，又要维护文档，一旦开发进度紧张，很容易导致代码与文档不<em>一致</em>。Web  API有一个Help Page插件，可以很方便的根据代码及注释自动生成相关API说明页面。Help  Page安装步骤及扩展(以VS2015为例)：右键点击WebAPI项目的引用，选择&quot;管理NuGet程序包&quot;在搜索框中输入  helppage进...

- ​                   [关于AJAX跨域调用ASP.NET MVC或者WebAPI服务的问题及解决方案](https://blog.csdn.net/ncqqbesny/article/details/44103095)                 

  关于AJAX跨域调用ASP.NET MVC或者WebAPI服务的问题及解决方案    作者：陈希章 时间：2014-7-3  问题描述 当跨域（cross domain）调用ASP.NET MVC或者ASP.NET Web API编写的服务时，会发生无法访问的情况。 重现方式   使用模板创建一个最简单的ASP.NET Web API项目，调试起来确认能正常工作   创

- ​                   [浅谈ASP.NET MVC运行过程](https://blog.csdn.net/zhanlanzhilian/article/details/79579980)                 

   描述本篇文章主要概述ASP.NET  MVC，具体包括如下内容：1.MVC模式概述2.WebForm概述3.WebForm与MVC区别4.ASP.NET  MVC发展历程5.运用程序结构6.ASP.NET MVC 默认约定 一 MVC模式概述1.  MVC模式运用领域分析：(1)当前，MVC作为一种主流框架，被广泛运用，如JAVA Web开发，.NET ASP,NET  MVC(2)MVC模式被广泛运用...

- ​                   [给现有MVC 项目添加 WebAPI](https://blog.csdn.net/qwlovedzm/article/details/56003770)                 

  1. 增加一个WebApi Controller, VS 会自动添加相关的引用，主要有System.Web.Http，System.Web.Http.WebHost，System.Net.Http  2. 在App_Start 下创建 WebApiConfig.cs 并注册路由   using System; using System.Collections.Generic; using

- ​                   [ASP.NET*运行机制*（二）--使用HttpModule，为每个请求附加额外信息](https://blog.csdn.net/qq_33857502/article/details/80597052)                 

  在之前的文章介绍过HttpModule,在这就不啰嗦了。今天完成了一个小案例，效果如图：为原有的文本，添加一些其它信息，实现思路如下：一、创建TestHttpModule类，并实现IHttpModule接口。二、实现IHttpModule接口中的方法，为HttpApplication对象的BeginRequest事件绑定方法，实现在用HttpHandler处理每个请求前，附加额外信息的功能。nam...

- ​                   [使用ASP.NET Web Api构建基于REST风格的服务实战系列教程](https://blog.csdn.net/binyao02123202/article/details/18361809)                 

  最近发现<em>web</em>  <em>api</em>很火，园内也有各种大神已经在研究，本人在<em>asp.net</em>官网上看到一个系列教程，原文地址：http://bitoftech.net/2013/11/25/detailed-tutorial-building-asp-net-<em>web</em>-<em>api</em>-restful-service/。于是打算跟着学一下，把学习过程记录在博客园的同时也分享给大家。 每一篇结束后我都会把代码共享 由于我也

- ​                   [关于ASP.NET WebApi (增删改查)](https://blog.csdn.net/Demain_lin/article/details/82586504)                 

  1、首先，我们先来介绍一下<em>什么</em>是WebApi     ASP.NET Web API 是一种框架，用于轻松构建可以由多种客户端（包括浏览器和移动设备）访问的 HTTP 服务。ASP.NET Web API 是一种用于在 .NET Framework 上构建 RESTful 应用程序的理想平台。  可以把WebApi看成Asp.Net项目类型中的一种，其他项目类型诸如我们熟知的WebForm项目，W...

- ​                   [使用Asp.Net MVC开发真正的Web程序](https://blog.csdn.net/dacong/article/details/4027872)                 

  ​    现在Asp.net  MVC1.0已经正式发了,完全改变我们在.Net平台下开发Web程序的方式.不在像以前的Web  form采用事件的方式来控制所有操作.我是从2003年,从Delphi转到<em>asp.net</em>平台上的,一开始就使用Web   form开发网站,真是爽,和使用delphi开发C/S的程序差不多,只要懂一点Web开发的特殊性就可以了,比如使用application,session,<em>什么</em>

- ​                   [ASP.NET API接口获取请求头中的数据和获取form-data中的图片](https://blog.csdn.net/qq_37240051/article/details/80741874)                 

  //获取请求头的UidIEnumerable&amp;lt;string&amp;gt;  Uid;Request.Headers.TryGetValues(&quot;uid&quot;, out  Uid);List&amp;lt;string&amp;gt; list = new  List&amp;lt;string&amp;gt;();list = Uid.ToList();User user =  ubll.Search(Convert.ToInt32(lis...

- ​                   [通过扩展让ASP.NET Web API支持JSONP](https://blog.csdn.net/rise51/article/details/51585754)                 

  同源策略（Same Origin  Policy）的存在导致了“源”自A的脚本只能操作“同源”页面的DOM，“跨源”操作来源于B的页面将会被拒绝。同源策略以及跨域资源共享在大部分情况下针对的是Ajax请求。同源策略主要限制了通过XMLHttpRequest实现的Ajax请求，如果请求的是一个“异源”地址，浏览器将不允许读取返回的内容。JSONP是一种常用的解决跨域资源共享的解决方案，现在我们利用AS

- ​                   [JS获取ASP.NET WebAPI返回的图片内容](https://blog.csdn.net/5653325/article/details/80655086)                 

  后台WebAPI返回图片内容的二进制流            byte[] imageBuffer;             //保存图片数据                 using (MemoryStream stream = new MemoryStream())             {                                 image.Save(stream...

- ​                   [ASP.NET WebAPI+*mvc*4.0+EasyUI快速开发框架+通用权限管理系统源码](https://download.csdn.net/download/libt51/8897033)                 

  框架特色： 1、基于ASP.NET MVC4.0 + WebAPI + EasyUI + Knockout的架构设计开发 2、采用MVC的框架模式，具有耦合性低、重用性高、生命周期成本低、可维护性高、有利软件工程化管理等优点 3、采用WebAPI，客户端完全摆脱了代理和<em>管道</em>来直接进行交互 4、采用EasyUI前台UI界面插件，可轻松的打造出功能丰富并且美观的UI界面 5、采用Knockout，，提供了一个数据模型与用户UI界面进行关联的高层次方式（采用行为驱动开发） 6、数据访问层采用强大的Fluentdata完美地支持多数据库操作 7、封装了一大部分比较实用的控件和组件，如自动完成控件、弹出控件、拼音模糊输入控件、日期控件、导出组件等

- ​                   [ASP.NET WebAPI 接口自测工具（v4.0最新版）](https://download.csdn.net/download/jiazhaokai1988/9475115)                 

  自己在研究ASP.NET WebAPI的过程中，为了方便自己调试，写了一个自测工具。 可以进行 application/json 协议的访问，也可以进行 application/x-www-form-urlencoded 协议的访问。 为了方便经常调试某个接口，可将 访问地址路径 和 参数写入到XML，后续可以选择。 输出参数如果是json字符串，可格式化，便于查看。（但是技术限制，不能传的字符串太长，否则得自己手动复制粘贴）  鉴于看到我这个旧资源还在经常被朋友下载，特此贴出最新版。 此版本为最新版本，添加了功能：可以在http协议的 header 里添加参数，以方便有 header 传参所需要的用户。 另外添加了个小小的 文件占用程序，修复了json convert网页指向错误的问题，在选择xml里的接口时候，可以对已保存的进行修改。  如有疑问或者建议，欢迎探讨。 PowerBy : http://www.iteming.wang，新浪微博：iteming

- ​                   [ASP.NET MVC、WebApi 设置返回Json为小驼峰命名](https://blog.csdn.net/q646926099/article/details/80169601)                 

  在ASP.NET MVC中，我们一般返回json数据，直接return  Json(data)就可以了，但是C#字段命名规范是首字母大写，返回Json的时候就是直接序列化了指定的实体对象，就是大写了。这里自己创建一个JsonResult，继承JsonResult，重写一下ExecuteResult方法，利用Newtonsoft.Json格式化一下数据，再自定义写回到请求中。     public cl...

- ​                   [Asp.Net MVC WebAPI身份验证Demo_v1.0.0](https://download.csdn.net/download/jx_521/10109980)                 

  项目中经常需要使用WebAPI编写接口提供给其他人调用， 那么接口就需要进行身份验证，否则只要知道了这个接口的地址都可以访问了 这里说下如何对<em>web</em><em>api</em>进行身份验证 主要通过重新AuthorizeAttribute内OnAuthorization方法进行身份验证

- ​                   [MVC和WebForm的特点和优点](https://blog.csdn.net/syaguang2006/article/details/37573337)                 

  MVC (Model、View、Controller)将一个Web应用分解为：Model、View和Controller。ASP.NET MVC框架提供了一个可以代替ASP.NETWebForm的基于MVC设计模式的应用。  ASP.NET MVC概述·MVC的优点：  1.通过把项目分成Model、View和Controller，使得复杂项目更加容易维护，减少项目之间的耦合。  2.

- ​                   [Asp.net WebApi 项目示例（增删改查）](https://blog.csdn.net/xuanwuziyou/article/details/34438231)                 

  1.页面运行效果

- ​                   [Web APi之*消息处理**管道*（五）](https://blog.csdn.net/weixin_34367845/article/details/85757047)                 

  前言 MVC有一套请求处理的机制，当然Web  API也有自己的一套<em>消息处理</em><em>管道</em>，该<em>消息处理</em><em>管道</em>贯穿始终都是通过HttpMessageHandler来完成。我们知道请求信息存在 RequestMessage 中，而响应信息则存在 ResponseMessage 中，当请求信息进入到<em>管道</em>中，此时HttpMessageHandler会对此进行相应的处理，当执行到控制器上的方法时此时就会进行响应，生成的...

- ​                   [asp .net *mvc*4 *web**api*（增、删、改、查）、多文件上传](https://download.csdn.net/download/resources_123/9789399)                 

  asp .net <em>mvc</em>4  <em>web</em><em>api</em>（增、删、改、查），uploadify.js多文件带进度条上传，支持大文件上传，支持在线打开预览文件

- ​                   [ASP.net Core MVC + WebAPI解决方案中同时启动多个项目](https://blog.csdn.net/snakerain/article/details/87978941)                 

  1.在解决方案-&amp;gt;属性-&amp;gt;启动项目-&amp;gt;多个项目启动 2.修改 MVC 和WebAPI每个启动项目的中Properties-&amp;gt;launchSettings.json文件中的端口号为不同内容，否则会出现Scket端口只允许使用一次的错误。  ...

- ​                   [IoC在Web API中的应用](https://blog.csdn.net/qq_27462223/article/details/77894683)                 

  控制反转（Inversion of Control，英文缩写为IoC）是框架的重要特征，并非面向对象编程的专用术语。它与依赖注入（Dependency Injection，简称DI）和依赖查找 （Dependency Lookup）并没有关系。简单地说，就是应用本身不负责依赖对象的创建和维护，而交给一个外部容器来负责。这样控制权就由应用转移到了外部IoC容器， 控制权就实现了所谓的反转。 一

- ​                   [HTTP协议/IIS 原理及ASP.NET*运行机制*浅析【图解】](https://blog.csdn.net/chelen_jak/article/details/50040537)                 

  转自：http://www.uml.org.cn/net/201306193.asp  前言  前一段在整理邮件的时候发现几年前和CDD老师交流时的一份邮件.下面是简单摘要:  “从技术角度来说，无论哪一个阵营，跟新技术都是不可避免的，也是很累的，当然作为一个程序员来说，也是必须的。要想让技术的更新对自己的影响减小，基础就必须打牢。所以,底层的东西和抽象层的东西需要下一番功夫。因为说到

- ​                   [Asp.net Web Api开发（第二篇）性能：使用Jil提升Json序列化性能](https://blog.csdn.net/yeqi3000/article/details/51692342)                 

  Asp.net Web Api开发（第二篇）性能：使用Jil提升Json序列化性能

- ​                   [从零开始学习 *asp.net* core 2.1 *web* *api* 后端*api*基础框架(七)-添加一个查询单笔数据的方法](https://blog.csdn.net/kingyumao/article/details/82897041)                 

  再写一个查询单笔数据的方法:   [Route(&quot;{id}&quot;)]           public JsonResult GetProduct(int id)           {               return new JsonResult(ProductService.Current.Products.SingleOrDefault(x =&amp;gt; x.Id == id));   ...

- ​                   [基于ASP.NET MVC 4 +Knockout.JS+Web API+FluentData+EasyUI 通用权限管理](https://download.csdn.net/download/liqiang0219/8182045)                 

  一套完整的框架及源代码，含数据初始数据，支持vs2010、2013，sql2008，学习真实项目及easyui、ko、fluent开发的资源

- ​                   [ASP.NET运行原理和*运行机制*](https://blog.csdn.net/yiyelanxin/article/details/78557082)                 

  一、ASP.NET运行原理  当一个http请求发送过来并被IIS机收到之后,IIS首先通过你请求的页面类型为其加载相应的dll文件，然后在处理过程中将这条请求发送给能够处理这条请求的模块,而在ASP.NET中这个模块就叫做HttpHandler,为<em>什么</em>aspx这样的文件可以被服务器处理,那是因为在服务器端有默认的HttpHandler专门处理aspx文件,IIS再将这条请求发送给能够处理这条请求的

- ​                   [C# ASP.NET *web**api* 解析所有的参数BASE64加密的串](https://blog.csdn.net/weixin_40918107/article/details/83986651)                 

  例如：加密前：http://xxxxxxxx/xxx/xxx?a=1&amp;amp;b=2&amp;amp;c=3  加密后http://xxxxxxxx/xxx/xxx?P=YT0xJmI9MiZjPTM=  在<em>api</em>创建模型类前，进行拦截，先将串解析，再根据解析的串对模型类的属性反射赋值。   public class ActionParamModelBinder : IModelBinder     { ...

- ​                   [ASP.NET Core HTTP *管道*中的那些事儿](https://blog.csdn.net/p77ll9l53x/article/details/72675691)                 

  IApplicationBuilder IApplicationBuilder 是应用大家最熟悉它的地方应该就是位于  Startup.cs 文件中的 Configure 方法了吧 public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory) {     app.UseDeveloperExcepti

- ​                   [MVC WebApi 文档生成注释说明的方法](https://blog.csdn.net/phker/article/details/78110685)                 

  在vs2015 里面生成的WebAPI项目  默认包含一个自动生成API说明文档的功能.  但是里面的方法注释和文档注释默认是不存在的.   百度了一下, 找到了解决方法.  生成的效果如下.首先要配置项目可以生成XML说明文档..  右击你的项目->生成->输出->勾上 XML文档文件  然后把这个文件名放在下面的代码里.然后 在Areas/HelpPage/App_Start/HelpPageC

- ​                   [ASP.NET Web API实现简单的文件下载与上传](https://blog.csdn.net/pan_junbiao/article/details/84065952)                 

  ASP.NET Web API实现简单的文件下载与上传。首先创建一个ASP.NET Web API项目，然后在项目下创建FileRoot目录并在该目录下创建ReportTemplate.xlsx文件，用于下面示例的使用。  1、文件下载  示例：实现报表模板文件下载功能。  1.1 后端代码   /// &amp;lt;summary&amp;gt; /// 下载文件 /// &amp;lt;/summary&amp;gt; [...

- ​                   [8种提升ASP.NET Web API性能的方法](https://blog.csdn.net/dz45693/article/details/51105383)                 

  英文原文：8 ways to improve ASP.NET  Web API performance  　　ASP.NET Web API 是非常棒的技术。编写 Web API 十分容易，以致于很多开发者没有在应用程序结构设计上花时间来获得很好的执行性能。  　　在本文中，我将介绍 8 项提高 ASP.NET Web API 性能的技术。  　　1、使用最快的 JSON 序列化工具

- ​                   [如何向MVC5项目中添加Wep API](https://blog.csdn.net/rowland001/article/details/51690491)                 

  近来学习MVC，已经能试着显示一个列表了（真实数据），想到一个网站的首页会有很多列表，如何操作呢？某人提醒我用API+jquery显示数据。一、查看MVC版本，决定你有没有必要看这篇文章  打开<em>web</em>.config，看到以下内容                publicKeyToke

- ​                   [ASP.NET WebApi 基于JWT实现Token签名认证(发布版)](https://blog.csdn.net/qq_41474416/article/details/82492026)                 

  一、前言   明人不说暗话,跟着阿笨一起玩WebApi！开发提供数据的WebApi服务，最重要的是数据的安全性。那么对于我们来说，如何确保数据的安全将会是需要思考的问题。在ASP.NET  WebService服务中可以通过SoapHead验证机制来实现，那么在ASP.NET  WebApi中我们应该如何保证我们的接口安全呢？在上此分享课程中阿笨给大家带来了传统的基于Session方式的Token签名...

- ​                   [解决ASP.NET Web *api* 使用AllowAnonymous特性不起作用的问题](https://blog.csdn.net/MDZZ666/article/details/88419189)                 

  问题：在控制器或方法添加[AllowAnonymous]特性的时候，无法跳过继承AuthorizeAttribute的子类的验证。 原因：   

- ​                   [ASP.NET常见面试题及答案（130题）](https://blog.csdn.net/qq1162195421/article/details/19484315)                 

  Asp.net核心技术思想  1、概述反射和序列化 反射:程序集包含模块，而模块包含类型，类型又包含成员。反射则提供了封装程序集、模块和类型的对象。您可以使用反射动态地创建类型的实例，将类型绑定到现有对象，或从现有对象中获取类型。然后，可以调用类型的方法或访问其字段和属性 序列化:序列化是将对象转换为容易传输的格式的过程。例如，可以序列化一个对象，然后使用 HTTP 通过 Internet 

- ​                   [使用HttpClient对ASP.NET Web API服务实现增删改查](https://blog.csdn.net/zhanglong_longlong/article/details/50372442)                 

  本篇体验使用HttpClient对ASP.NET Web API服务实现增删改查。   创建ASP.NET Web API项目     新建项目，选择"ASP.NET MVC 4 Web应用程序"。     选择"Web API"。     在Models文件夹下创建Product类。        public class Product     {     

- ​                   [使用Swagger 搭建高可读性ASP.Net WebApi文档](https://blog.csdn.net/qq_42606051/article/details/81868496)                 

  一、前言  在最近一个商城项目中，使用WebApi搭建API项目。但开发过程中，前后端工程师对于沟通接口的使用，是非常耗时的。之前也有用过Swagger构建WebApi文档，但是API文档的可读性并不高。尤其是没有传入参数和传出结果的说明，导致开发人员沟通困难。在园子里看到一篇关于对Swagger优化的文章，有很大的改进。解决了传入参数，API分区域筛选等问题，  非常感谢博主简玄冰。  不过实践之...

- ​                   [ASP.NET MVC Web Api 跨域访问](https://download.csdn.net/download/cj21828/8399545)                 

  ASP.NET MVC Web Api 跨域访问

- ​                   [.net *mvc* *web**api* 处理跨域请求](https://blog.csdn.net/jacky_zh/article/details/72874889)                 

  现在流行<em>web</em>app或者前端和后端分离，那么后端服务就会从重的程序处理，转变成数据驱动的数据抽取即可。那么<em>web</em><em>api</em>就变成了最佳选择。 然而，处理http请求还是仍旧的核心内容。 先看下跨域请求的定义：     跨域资源共享-Cross Origin Resource  Sharing(CORS)是一项W3C标准，允许服务端释放同源策略，使得服务端在接受一些跨域请求的同时拒绝其他的跨域请求（

- ​                   [ASP.Net MVC开发基础学习笔记（5）：区域、模板页与WebAPI初步](https://blog.csdn.net/mss359681091/article/details/51122899)                 

  一、区域—麻雀虽小，五脏俱全的迷你MVC项目 1.1 Area的兴起 为了方便大规模网站中的管理大量文件，在ASP.NET MVC 2.0版本中引入了一个新概念—区域(Area)。  在项目上右击创建新的区域，可以让我们的项目不至于太复杂而导致管理混乱。有了区域后，每个模块的页面都放入相应的区域内进行管理很方便。例如：上图中有两个模块，一个是Admin模块，另一个是Product模块，所有

- ​                   [使用Entity_Framework和Web_API商城实例](https://download.csdn.net/download/liaojg/8528355)                 

  在中ASP.NET MVC 4微软增加了对Web API的支持， Entity  Framework也是微软推出的一个轻量级的ORM框架，如何结合使用这两者构建网站项目？本教程教你一步一步的建立一个商城应用，在这个过程中对Web  API、ASP.NET MVC 4、Entity Framework有一个初步的了解，是入门的好教程。 包里有教程和所有VS 2010版本的源码。

- ​                   [如何更改MVC WebApi 中的请求路径](https://blog.csdn.net/itcast_jwz_310/article/details/78111010)                 

  问题描述： <em>web</em>Api 运行时 只有controller 名称 没有加载出方法的名字   解决方案： 在App_Start 文件下<em>web</em><em>api</em>Config  中更改Routes      config.Routes.MapHttpRoute(                 name: "DefaultApi",                 routeTemplate: "a

- ​                   [ASP.NET Web API 接收文件上传](https://download.csdn.net/download/starlightextinction/8732727)                 

  ASP.NET Web API File Upload and Multipart MIME

- ​                   [jquery/js实现一个网页同时调用多个倒计时(最新的)](https://blog.csdn.net/wuchengzeng/article/details/50037611)                 

  jquery/js实现一个网页同时调用多个倒计时(最新的)  最近需要网页添加多个倒计时. 查阅网络,基本上都是千遍一律的不好用. 自己按需写了个.希望对大家有用. 有用请赞一个哦!    //js   //js2 var plugJs={     stamp:0,     tid:1,     stampnow:Date.parse(new Date())/1000,//统一开始时间戳     ...

- ​                   [DTX - Database Toolbox For MFC Ver 1.8 (Freeware Version)下载](https://bbs.csdn.net/topics/392595087)                 

  DTX是什么   DTX是一系列的类，这些类提供对数据库的编辑、自动的读、写和显示。DTX可以自动的读写  Blob变量（比如读写DBimage）。它使用简单。版本1.8是一个只可以使用MS的Access数据库  ，但是这个类并不只是为Access数据库而做。它可以操作很多类型的数据库。   特色   1.使用非常简单。  2.自动读写变量  3.自动读写bolb  4.自动读写DBImages  5.使用标准的fields  6.自动更新屏幕数据  7.使用标准的windows（C Classes）控件、标准的颜色控件，标准的阴影（CDTX  classes）控件。 相关下载链接：[url=//download.csdn.net/download/xujunhaohao/2061154?utm_source=bbsseo]//download.csdn.net/download/xujunhaohao/2061154?utm_source=bbsseo[/url]

- ​                   [MFC.Widnows程序设计第2版1 PDF.rar下载](https://bbs.csdn.net/topics/392598956)                 

  MFC.Widnows程序设计第2版1 PDF 相关下载链接：[url=//download.csdn.net/download/System_Zero/2080769?utm_source=bbsseo]//download.csdn.net/download/System_Zero/2080769?utm_source=bbsseo[/url]

- ​                   [SOCKET应用在WEB页面的使用事例下载](https://bbs.csdn.net/topics/392617464)                 

  SOCKET在网页通信的具体事例，原理是B2B服务端通信。 相关下载链接：[url=//download.csdn.net/download/cghcxy2009/2166683?utm_source=bbsseo]//download.csdn.net/download/cghcxy2009/2166683?utm_source=bbsseo[/url]

- ​                   文章热词                                            [设计制作学习](https://edu.csdn.net/combos/o363_l0_t)                                                                 [机器学习教程](https://edu.csdn.net/courses/o5329_s5330_k)                                                                 [Objective-C培训](https://edu.csdn.net/courses/o280_s351_k)                                                                 [交互设计视频教程](https://edu.csdn.net/combos/o7115_s388_l0_t)                                                                 [颜色模型](https://edu.csdn.net/course/play/5599/104252)                                      
- ​                   相关热词                                            [mysql关联查询两次本表](https://www.csdn.net/gather_24/MtTaEg3sMDM5MS1ibG9n.html)                                                                 [native底部 react](https://www.csdn.net/gather_10/MtjaIg3sMTUzMy1kb3dubG9hZAO0O0OO0O0O.html)                                                                 [extjs glyph 图标](https://www.csdn.net/gather_1b/Ntzagg1sOTU3LWRvd25sb2Fk.html)                                                                 [长沙有什么web培训机构](https://www.csdn.net/gather_4a/MtTaYg1sODctZWR1.html)                                                                 [什么是web培训](https://www.csdn.net/gather_4a/MtTaUg0sMDYtZWR1.html)                                      

我们是很有底线的

### 猜你喜欢

- ​                                    [sublime text  channel_v3.json](https://download.csdn.net/download/ming_221/9944588)                                                                                           解决Sublime包管理package control 报错 There are no packages available for installation： 修改hosts没有用。ctrl + `                                

- ​                                    [iOS8开发技术（Swift版）：扩展（Extensions）](https://edu.csdn.net/course/detail/1179)                                                                                           iOS8,swift,extension,扩展,xcode,iOS,移动开发,                                

- ​                                    [案例上手 Python 数据可视化](https://gitchat.csdn.net/column/5c6cd09e7fa9074fde9c8909)                                                                                           数据可视化,数据分析                                

- ##### 细说Asp.Net Web API消息处理管道（二）

  2017-03-20

   3416

  在细说Asp.Net  Web API消息处理管道这篇文章中，通过翻看源码和实例验证的方式，我们知道了Asp.Net Web  API消息处理管道的组成类型以及Asp.Net Web  API是如何创建消息处理管道的。本文在上篇的基础上进行一个补充，谈谈在WebHost寄宿方式和SelfHost寄宿方式下，请求是如何进入到Asp.Net  Web API的消息处理管道的。  WebHost寄宿方式

- ##### ASP.NET中的 Web API和MVC运行机制有什么差异？消息处理管道是否一致？

  2018-04-25

   2331

  [size=15px]我个人的理解是：Web  API 相比MVC 只是去除了Controller与View的关联，不用再去解析ViewDescription（Web  API也是没有ViewDescription的吧）。rnrn具体些的，Web  API和MVC运行机制有什么差异吗？消息处理管道是否一致？rn如果知道可以说一下，有链接也可以。[/size]

- ##### WebForm页面生命周期及asp.net运行机制

  2016-07-11

   2881

  ﻿﻿ 1.先上几张原理图着重理解：              现在针对第四副图原理进行解析： 流程: 1.浏览器发送请求 2.服务器软件（IIS）接收,它最终的目的就是为了向客户输出它请求的动态页面生成的html代码。 3.服务器不会处理类和动态页面，所以找扩展程序  4.交给FrameWork，它其中有个类HttpRuntime，其中有个ProcessRequest

- ##### 传统WebForm网站和MVC网站运行机制对比

  2016-12-25

   925

  先上图看对比：           一、运行机制    当我们访问一个网站的时候，浏览器和服务器都是做了哪些动作呢     （一）WebForm网站运行机制    假设为：www.baidu.com/index.aspx   1、Http请求（物理地址：index.aspx）   ①发送请求     浏览器向服务器发送请求报文，此时由IIS虚拟目录接受。（通过配置过IIS，把

- ##### 解析ASP.NET WebForm和Mvc开发的区别

  2013-12-29

   59434

  因为以前主要是做WebFrom开发，对MVC开发并没有太深入的了解。自从来到创新工场的新团队后，用的技术都是自己以前没有接触过的，比如：MVC   和EF还有就是WCF，压力一直很大。在很多问题都是不清楚的情况下，问周围的人，别人也只是给自己讲一个大概。而且前两天因为问了一个比较细的问题，还被别人的一句话打击。“我只能告诉你方法，你还指望我手把手的交给你呀，不会你得自己学呀。。。”。没办法只能自己找时

- ##### ASP.NET平台下MVC与WebForm两种模式区别（图解）

  2013-04-07

   2881

  本文将为大家对比ASP.NET MVC与WebForm的区别，通过这种形式我们能更加了解ASP.NET MVC及其工作原理，也是为了令大家今后的开发工作更加方便，快捷。          1.传统WebForm开发中存在的一些问题，传统的ASP.NET开发中，微软的开发团队为开发者设计了一个在可视化设计器中拖放控件，编写代码响应事件的快速开发环境。然而，它所带来的负面效应是：由于控件封装了很多东

- ##### 理解ASP.NET MVC底层运行机制

  2017-05-19

   1030

  ASP.NET   MVC有三大组件(即模型、视图、控制器)。所谓模型，就是MVC需要提供的数据源，负责数据的访问和维护。所谓视图，就是用于显示模型中数据的用户界面。所谓控制器，就是用来处理用户的输入，负责改变模型的状态并选择适当的视图来显示模型的数据。以下是我绘制的MVC三大组件之间的交互图。   从交互图中可以看出,MVC从用户发送请求到页面呈现结果大致经过了五个步骤，分别为： (1).

- ##### ASP.NET Core 运行原理剖析

  2017-09-23

   3003

  1.1.  概述在ASP.NET Core之前，ASP.NET  Framework应用程序由IIS加载。Web应用程序的入口点由InetMgr.exe创建并调用托管。以初始化过程中触发HttpApplication.Application_Start()事件。开发人员第一次执行代码的机会是处理Application_StartGlobal.asax中的事件。在ASP.NET  Core中，Global

- ##### Asp.net MVC进入请求管道的过程

  2016-04-21

   3934

  一：Asp.Net  MVC请求处理原理（Asp.Net mvc 是怎样进入请求管道的。） 请求-->IIS--->ISAPIRuntime-->HttpWorkRequest-->HttpRuntime-->HttpContext-->找到Global文件，并且编译该文件-->确保Global文件中Application_Start被调用-->创建HttpApplication(池  栈)如果池中

- ##### 创建ASP_NET_Web_API_2.0应用实例

  2014-08-31

   9

  由于ASP.NET  Web API具有与ASP.NET MVC类似的编程方式，再加上目前市面上专门介绍ASP.NET Web API  的书籍少之又少（我们看到的相关内容往往是某本介绍ASP.NET MVC的书籍“额外奉送”的），以至于很多人会觉得ASP.NET Web  API仅仅是ASP.NET MVC的一个小小的扩展而已，自身并没有太多“大书特书”的地方。而真实的情况下是：ASP.NET Web  API不仅仅具有一个完全独立的消息处理管道，而且这个管道比为ASP.NET  MVC设计的管道更为复杂，功能也更为强大。虽然被命名为“ASP.NET Web  API”，但是这个消息处理管道却是独立于ASP.NET平台的，这也是为什么ASP.NET Web API支持多种寄宿方式的根源所在。 为 了让读者朋友们先对ASP.NET Web API具有一个感性认识，接下来我们以实例演示的形式创建一个简单的ASP.NET Web  API应用。这是一个用于实现“联系人管理”的单页Web应用，我们以Ajax的形式调用Web API实现针对联系人的CRUD操作。

- ##### ASP.NET之旅--浅谈Asp.net运行机制（一）

  2013-09-02

   12895

  很多Asp.net开发人员都有过Asp的背景，以至于我们开发程序的时候总是停留在“页面”层次思考，也就是说我们常常会只考虑我们现在所做的系统是要完成什么功能，是要做问卷调查网站还是个人网站，而很少在“请求级”思考，思考能不能通过编码的方式来操作一个Http请求。在跨入Asp.net后Asp有了质的飞跃，很多底层的Http请求都得到了良好的应用，这时候Asp.net不仅仅是一门开发语言，而是一个开发平台。想要能在“请求级”来实现编码，我们就不得不来说说Asp.net的内部运行机制。

- ##### Asp.net管道模型（管线模型）

  2017-10-24

   369

  转 http://www.cnblogs.com/kuyusea/p/4638395.html    前言　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　   为什么我会起这样的一个标题，其实我原本只想了解asp.net的管道模型而已，但在查看资料的时候遇到不明白的地方又横向地查阅了其他相关的资料，而收获比当初预想的大了很多。   有本篇作基础，下面两篇就更好理解了： 

- ##### ASP.NET Core MVC/WebAPi 模型绑定探索（转载）

  2018-06-22

   396

  原文地址：https://www.cnblogs.com/CreateMyself/p/6246977.html话题在ASP.NET  Core之前MVC和Web APi被分开，也就说其请求管道是独立的，而在ASP.NET  Core中，WebAPi和MVC的请求管道被合并在一起，当我们建立控制器时此时只有一个Controller的基类而不再是Controller和APiController。所以才有...

- ##### 学习篇：asp.net mvc 管道模型

  2017-10-24

   369

  转  http://www.cnblogs.com/2-18/archive/2012/11/04/2753227.html   首先，客户端发送url请求→Http://localhost/Home/Index. 这时，服务端的内核模块中的  HTTP.SYS组件监听着80端口发来的请求。HTTP.SYS访问注册表，查看来的这种请求交给谁处理，然后返回信息给HTTP.SYS。HTTP.SYS一

- ##### ASP.NET MVC5请求管道和生命周期

  2018-08-20

   146

   请求处理管道 请求管道是一些用于处理HTTP请求的模块组合，在ASP.NET中，请求管道有两个核心组件：IHttpModule和IHttpHandler。所有的HTTP请求都会进入IHttpHandler，有IHttpHandler进行最终的处理，而IHttpModule通过订阅HttpApplication对象中的事件，可以在IHttpHandler对HTTP请求进行处理之前对请求进行预处理或...

- ##### Asp.Net异步编程-使用了异步,性能就提升了吗?

  2018-11-30

   170

  Asp.Net异步编程  写在前面的话,很久没有写Blog了,不对,其实一致就没有怎么写过.今天有空,我也来写一篇Blog  随着.Net4.5的推出,一种新的编程方式简化了异步编程,在网上时不时的也看到各种打着Asp.Net异步编程的口号,如何提高性能,如何提高吞吐率!  好多文章都说得不清楚,甚至是错误的.只看到了一些表象,混淆概念.希望这篇文章能够能够对一部分人理解Asp.net异步编程模型...

- ##### 在ASP.NET MVC中使用WebApi注册路由注意事项

  2016-08-02

   2660

  在ASP.NET   MVC中手动添加WebApi控制器，在App_Start中创建WebApiConfig.cs类文件配置路由，在Global.asax中注册路由时应把WebApiConfig.Register(GlobalConfiguration.Configuration);放在RouteConfig.RegisterRoutes(RouteTable.Routes);前面，否则出现404错误

- ##### Asp.Net Mvc 运行机制原理分析

  2018-08-23

   1212

  最近一段时间接手过的项目都是基于Asp.Net的，以前对aspnet运行机制有一个大概的了解，总觉得不够透彻，按自己的理解来分析一下。  Asp.Net 运行机制  理解mvc运行原理的前提是要了解aspnet运行原理，这方面网上资料多如牛毛，我这里就大致说一下aspnet生命周期    Http请求到IIS后，如果是静态资源则IIS读取后返回客户端，动态请求被isap.dll 转发自net托管平...

- ##### Asp.Net WebAPI中Filter过滤器的使用以及执行顺序

  2017-09-06

   1615

  转发自：http://www.cnblogs.com/UliiAn/p/5402146.html  在WEB  Api中，引入了面向切面编程（AOP）的思想，在某些特定的位置可以插入特定的Filter进行过程拦截处理。引入了这一机制可以更好地践行DRY(Don’t  Repeat Yourself)思想，通过Filter能统一地对一些通用逻辑进行处理，如：权限校验、参数加解密、参数校验等方面我们都

- ##### WebApi和MVC有什么区别？

  2017-08-11

   8845

  https://www.zhihu.com/question/46369458/answer/144963042   首先要重点说的是，Web API是一种无限接近于RESTful风格的轻型框架，且不是微软提出来的，微软在.NET上实现了这中框架—http://Asp.Net  Web API，所以“微软包装”是一个极大的偏见。 就应用市场时间而论，MVC普及市场的时间比Web API时

- ##### 【深入ASP.NET原理系列】--ASP.NET请求管道、应用程序生命周期、整体运行机制

  2015-12-29

   1091

  微软的程序设计和相应的IDE做的很棒，让人很快就能有生产力。.NET上手容易，生产力很高，但对于一个不是那么勤奋的人，他很可能就不再进步了，没有想深入下去的动力，他不用去理解整个框架和环境是怎么执行的，因为不用明白为什么好像也能做好工作。 .NET的人很多人不注重实现 ,知其然不知其所以然,这样真的OK么？永远怀着一颗学徒的心，你就能不断进步！   　　我们知道在ASP.NET中,若要对AS

- ##### 【ASP.NET】Webform与MVC开发比较

  2016-01-20

   3576

  去年暑假开始，跟着一个项目，开始接触到了MVC，那时候，自己对Webform的开发还没有在项目中真正实践过，没有什么过渡，就跳跃到MVC开发下了。而最近，在维护的一个项目中，并没有使用MVC开发，用的是Webform开发。这两次经历的结合，引发了我对本篇博客标题的思考，即Webform与MVC开发比较。     【Webform下的开发】     通过这次对ASP.NET Webform的重

- ##### 用.Net Core控制台模拟一个ASP.Net Core的管道模型

  2017-12-21

   651

  在我的上几篇文章中降到了asp.net core的管道模型，为了更清楚地理解asp.net core的管道，再网上学习了.Net Core控制台应用程序对其的模拟，以加深映像，同时，供大家学习参考。 首先，新建一控制台应用程序。注意是.Net Core的控制台应用程序。  然后新建一个Context类，以模拟ASP.net core中的context类，然后再Context类中添加一个Wri

- ##### asp.net web api帮助文档的说明

  2014-05-30

   4241

  asp.net web api帮助文档的说明

- ##### MVC面试问题与答案

  2014-03-16

   29822

  这篇文章的目的是在面试之前让你快速复习ASP.NET MVC知识。

- ##### ASP.NET MVC架构与实战系列之一：理解MVC底层运行机制

  2015-08-19

   4748

  今天，我将开启一个崭新的话题：ASP.NET  MVC框架的探讨。首先，我们回顾一下ASP.NET Web Form技术与ASP.NET  MVC的异同点，并展示各自在Web领域的优劣点。在讨论之前，我对这两种技术都非常热衷，我个人觉得在实际的项目开发中，两者都能让我们受益匪浅，因此是目前Web领域两大平行和流行的技术。我们都知道，在传统的ASP.NET  Web Form应用程序中，Microso

- ##### ASP.NET MVC同时支持web与webapi模式

  2017-03-23

   6765

  我们在创建 web mvc项目时是不支持web api的接口方式访问的，所以我们需要添加额外的组件来支持实现双模式。 首先我们需要准备三个web api依赖的组件（目前在.net 4/4.5版本下面测试正常，2.0暂未进行测试，需要自行测试） 1、Microsoft.AspNet.WebApi.Client.5.2.2 2、Microsoft.AspNet.WebApi.Core.5.2.2

- ##### Asp.net MVC WebApi项目的自动接口文档及测试功能打开方法

  2017-12-21

   2016

  首先，创建一个WebApi项目，vs会自动根据模版创建一个完整的webapi程序，其中包括了自动文档的一切。但是，这个功能确实关闭的。。。蛋疼。。。。偏偏还没有地方显式的告诉打开的方法和步骤。。。。无语。。。 好了，现在先说如何打开webapi接口的自动文档： 一：项目右键属性，选择"生成"栏目，指定接口文档xml文件的路径和名字   二：打开帮助文档子项目的配置文件，解开红框标注的配置

- ##### ASP.NET MVC4 Web API+VS2013 编写、发布及部署流程

  2018-04-13

   42

  ASP.NET MVC4 Web API+VS2013 编写、发布及部署流程，其中部署的环境包括Windows7和阿里云服务器，自己边写边整理的文档，有很高的实际参考价值，适用于新手

- ##### Mvc4中的WebApi的使用方式

  2017-05-10

   6754

  一:简单介绍什么是Web  apiREST属于一种设计风格，REST 中的  POST（新增数据），GET（取得数据），PUT（更新数据），DELETE（删除数据）来进行数据库的增删改查，而如果开发人员的应用程式符合REST原则，则它的服务为“REST风格Web服务“也称的RESRful  Web API”。微软的web api是在vs2012上的mvc4项目绑定发行的，它提出的web api是完全基于R

- ##### 在ASP.NET Web API和MVC中使用Ninject

  2015-03-04

   154

  在ASP.NET Web API和ASP.NET Web MVC中使用Ninject,

- ##### ASP.NET Web API 中的路由以及Action的选择

  2018-03-09

   1462

  ASP.NET Web API 中的路由以及Action的选择  原文更新日期：2017.11.28    导航页面  http://blog.csdn.net/wf824284257/article/details/79475115    上一步  ASP.NET Web API 中的路由  http://blog.csdn.net/wf824284257/article/details/794...

- ##### 【ASP.NET】管道模型

  2017-04-17

   3434

  众所周知，我们在使用ASP.NET创建web项目时，会选择ASP.NET WebForm，或ASP.NET MVC 。而他们都是基于ASP.Net 管道模型的，换句话说，管道模型是整个asp.net的核心。如下图所示：                                                              一、管道对象模型        在System.W

- ##### aspnet webapi源码

  2017-11-10

   58

  压缩文件包含aspnet webapi源码、mvc4源码、webstack源码。

- ##### ASP.NET页面请求过程及生命周期管道事件

  2016-05-23

   1089

  Client（发送报文：请求行+请求头+空行+请求体）  Server，由  Http.sys 监听 Http 请求 -> WAS+Metabase（通过URL确定WebApp工作进程） ->  W3WP.exe(一个应用程序池，加载Aspnet_IsAPI.dll) ->AppDomainFactory(构造 ApplicationManager)->ISAPIApplicationHo

- ##### 基于ASP.NET MVC 4、WebApi、jQuery和FormData的多文件上传方法

  2017-05-01

   6812

  介绍了一个基于ASP.NET MVC 4、WebApi、jQuery、ajax和FormData数据对象的多文件上传方法。

- ##### webapi与mvc的不同之处

  2018-05-31

   1038

  关于webapi  1.以http协议传数据，可跨平台，跨终端  2.路由方面与mvc相同，分默认路由和特性路由  3.需要进行路由配置  4.通过ajax获得数据  5.mvc路由和webapi路由是分开的，webapi配置处WebApiConfig.cs，mvc路由配置处RouteConfig.cs  If you are familiar with ASP.NET MVC, Web API ...

- ##### ASP.NET Web API Selfhost宿主环境中管道、路由

  2014-08-06

   1953

  ASP.NET  Web API Selfhost宿主环境中管道、路由 前言 前面的几个篇幅对Web API中的路由和管道进行了简单的介绍并没有详细的去说明一些什么，然而ASP.NET Web  API这个框架由于宿主环境的不同在不同的宿主环境中管道中的实现机制和路由的处理方式有着很大的不同，所以我会将对应不同的宿主环境来分别的做出简单的讲解。   ASP.NET Web API路由、管道 

- ##### Asp.net mvc4 WebApi 中使用多个Post请求,无法识别的问题

  2017-11-07

   1443

  ﻿﻿ 解决方案： 方法1：修改WebApiConfig文件   //默认配置              config.Routes.MapHttpRoute(                  name: "DefaultApi",                  routeTemplate: "api/{controller}/{id}",                  

- ##### 签名来保证ASP.NET MVC OR WEBAPI的接口安全

  2016-10-09

   1584

  当我们开发一款App的时候，App需要跟后台服务进行通信获取或者提交数据。如果我们没有完善的安全机制则很容易被别用心的人伪造请求而篡改数据。 所以我们需要使用某种安全机制来保证请求的合法。现在最常用的办法是给每个http请求添加一个签名，服务端来验证签名的合法性，如果签名合法则执行响应的操作，如果签名非法则直接拒绝请求。  签名算法  签名算法一般都使用Hash散列算法，常用的有MD5，

- ##### 对ASP.NET Core MVC 2开发web应用程序的一些看法

  2018-07-20

   367

  netCoreMvc的大概思路  MVC模式  模型(数据)  视图(HTML页面)  控制器(操作数据, 发送到页面)  整体架构  netCoreMvc虽然说源码\架构都重写了, 但感觉还是差不多的, 将整个的web应用看成一个整体的应用程序, 各功能之间集成得非常好, 而且对于HTTP\Cookie\Session等封装得比较严实  依赖注入\服务配置\中间件配置等等, 设计非常精妙, 非常...

- ##### asp.net 管道事件注册、管道执行步骤模拟

  2016-03-27

   1760

  我们知道Application的管道有23个步骤 19个事件，这些事件都是可以对请求报文进行处理的，也就是说可以进行过滤操作的。那么，我们如何进行过滤操作呢？方法就是进行19个事件的注册，具体操作方法有以下两种。 在进一步讲解之前，我们先来了解一下19个管道事件 下面是请求管道中的19个事件.  (1)BeginRequest: 开始处理请求  (2)AuthenticateReque

- ##### ASP.NET Web API 学习系列（一）创建与简单的增删改查

  2017-09-22

   1510

  之前没接触过web api，最近项目正好要用到这个所以就了解了一下，本人是一个菜鸟，第一次写博客，写的也都是一些最基础的东西，有哪些地方写的不够好还请大家多多指点和补充，谢谢！ 创建一个Web api 的程序，上图哈哈（VS2015）       程序创建之后，系统会默认创建如下文件：  具体web api路由配置今天先不多说，可以参考下 http://blog.csdn.ne

- ##### MVC5管道处理模型

  2018-10-13

   220

  MVC5管道处理模型  

- ##### 在Asp.Net MVC项目中创建一个API

  2017-07-28

   488

  最近在忙一个MVC项目，Leader要求创建一个API，可通过某个link获得某个记录的信息。  本来想通过View来返回一个JSON记录，当我创建Controller时发现有一个API Controller，想必是MVC已经具备这个功能了，所以抱着试一试的心态，迅速补习了一下MVC API知识，马上现学现卖。

- ##### ASP.NET MVC、WebApi 设置返回Json为小驼峰命名

  2018-05-02

   2168

  在ASP.NET  MVC中，我们一般返回json数据，直接return  Json(data)就可以了，但是C#字段命名规范是首字母大写，返回Json的时候就是直接序列化了指定的实体对象，就是大写了。这里自己创建一个JsonResult，继承JsonResult，重写一下ExecuteResult方法，利用Newtonsoft.Json格式化一下数据，再自定义写回到请求中。     public cl...

- ##### asp.net mvc 管道模型 学习总结

  2019-03-03

   65

  1.Http请求处理流程  处理文件  1IIS根据文件的后缀名处理html或asp  2服获取后缀名以后，寻找处理的程序，找不到或没有受到服务器端的保护 直接返还文件。（受保护的例子是 App_Code中的），  3 处理后缀名程序，称为 ISAPI 应用程序，代理作用，映射请求页面和后缀名相对的处理程序。  一个完整的HTTP请求流程：  1.用户浏览器输入地址， 	2.DNS解析(域名...

- ##### asp.net mvc webapi 实用的接口加密方法

  2017-10-19

   879

  在很多项目中，因为webapi是对外开放的，这个时候，我们就要得考虑接口交换数据的安全性。 　　安全机制也比较多，如andriod与webapi 交换数据的时候，可以走双向证书方法，但是开发成本比较大， 　　今天我们不打算介绍这方面的知识，我们说说一个较简单也较常见的安全交换机制 　　在这里要提醒读者，目前所有的加密机制都不是绝对的安全! 　　我们的目标是，任何用户或者软件获取到我们的we

- ##### ASP.NET 使用Swagger开发Web API接口项目

  2018-05-15

   2097

  ASP.NET  使用Swagger开发WebApi接口项目：项目使用Web  API创建自动提供了API文档，采用mvc方式创建项目稍麻烦点需要手动添加WebApiConfig配置，而采用Web  API项目这些都已经生成好了。创建Web  API项目添加Swagger依赖库Swagger生成的文件项目右键属性&amp;gt;生成&amp;gt;添加XML生成配置：Web  API提供的API列表打开项目启动主页：htt...

- ##### ASP.NET中WebSocket 的使用总结

  2017-01-16

   3866

  需求背景



程序人生

![CSDN资讯](assets/csdn-zx.png)

CSDN资讯



kefu@csdn.net



*QQ客服*



[客服论坛](http://bbs.csdn.net/forums/Service)

<svg t="1538013874294" width="17" height="17" style="" viewBox="0 0 1194 1024" version="1.1" xmlns="http://www.w3.org/2000/svg" p-id="23784" xlink="http://www.w3.org/1999/xlink"><defs></defs></svg>

400-660-0108 

工作时间 8:30-22:00

[关于我们](https://www.csdn.net/company/index.html#about)[招聘](https://www.csdn.net/company/index.html#recruit)[广告服务](https://www.csdn.net/company/index.html#contact)            [            网站地图](https://www.csdn.net/gather/A)



[*百度提供站内搜索*](https://zn.baidu.com/cse/home/index) [京ICP备19004658号](http://www.miibeian.gov.cn/publish/query/indexFirst.action)

©1999-2019 北京创新乐知网络技术有限公司 

[经营性网站备案信息](https://csdnimg.cn/cdn/content-toolbar/csdn-ICP.png)        *网络110报警服务*

[北京互联网违法和不良信息举报中心](http://www.bjjubao.org/)

[中国互联网举报中心](http://www.12377.cn/)[家长监护](https://download.csdn.net/index.php/tutelage/)[版权申诉](https://blog.csdn.net/blogdevteam/article/details/90369522)

[                          ](https://mall.csdn.net/vip)      

​         

​         