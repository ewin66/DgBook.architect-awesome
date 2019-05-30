<svg aria-hidden="true" style="position: absolute; width: 0px; height: 0px; overflow: hidden;"></svg>

<svg aria-hidden="true" style="position: absolute; width: 0px; height: 0px; overflow: hidden;"></svg>

- ​                          
- [首页](https://www.csdn.net/)
- [博客](https://blog.csdn.net/)
- [学院](https://edu.csdn.net)                          
- [下载](https://download.csdn.net)
- [论坛](https://bbs.csdn.net)
- [APP](https://www.csdn.net/csdn-app/)                          
- [问答](https://ask.csdn.net)
- [商城](https://h5.youzan.com/v2/showcase/homepage?alias=BUj3rrGa2J&ps=760)
- [活动](https://huiyi.csdn.net/)
- [VIP会员](https://mall.csdn.net/vip)![img](https://csdnimg.cn/public/common/toolbar/images/vipimg.png)
- [招聘](http://job.csdn.net)
- [ITeye](http://www.iteye.com)
- [GitChat](https://gitbook.cn/?ref=csdn)

- 
- ​                                                    
- [写博客](https://mp.csdn.net/postedit)              
- ​              [![img](https://csdnimg.cn/public/common/toolbar/images/message-icon.png)消息](https://i.csdn.net/#/msg/index)                              
- [![img](https://profile.csdnimg.cn/5/2/1/2_u011535356)](https://i.csdn.net)

 

原

# 传统WebForm网站和MVC网站运行机制对比

​                                                   2016年12月25日 16:25:54           [赵尽朝](https://me.csdn.net/z15732621582)           阅读数：927                                                                  

​                   

​                      版权声明：本文为博主原创文章，未经博主允许不得转载。          https://blog.csdn.net/z15732621582/article/details/53870440        

   **先上图看对比：**

​    **![img](https://img-blog.csdn.net/20161225162150140?watermark/2/text/aHR0cDovL2Jsb2cuY3Nkbi5uZXQvejE1NzMyNjIxNTgy/font/5a6L5L2T/fontsize/400/fill/I0JBQkFCMA==/dissolve/70/gravity/Center)**



 

#   一、运行机制

   当我们访问一个网站的时候，浏览器和服务器都是做了哪些动作呢

 

##   （一）WebForm网站运行机制

   假设为：www.baidu.com/index.aspx

###   1、Http请求（物理地址：index.aspx）

  **①发送请求**

​    浏览器向服务器发送请求报文，此时由IIS虚拟目录接受。（通过配置过IIS，把网站挂载在服务器上，通过访问**虚拟目录**的方式访问网站的。）

  **②转交请求**

​    服务器端的IIS软件接收到请求后，把请求交给.NET FrameWork进行处理

  **③创建页面类对象**

​    .NET FrameWork根据请求的地址index.aspx,会创建对应的index_aspx类的对象（**页面对象**）。

###   2、返回给浏览器

   **①转交回复**

   通过IIS传输出给浏览器，要输出的html元素或其他内容（html+js+css等）

   **②解析为图形界面**

   器解析html代码，并翻译为图形化界面

 

##   （二）MVC网站的运行机制

  假设为：www.baidu.com/news/index

###   1、Http请求（逻辑地址：news/index）

​    ①发送请求（news/index）

​    ②转交请求（同上）

​    ③创建**类对象+方法**

​    .NET FrameWork根据**路由配置**，解析URL，并**创建news类**的对象，并调**用对象的index方法**。通过View方法加载视图，然后访问视图文件夹下的index.cshtml 

###   2、返回给浏览器（同上）

 

#   二、区别

##   1、请求方式

  **①WebForm：index.aspx页面**

   www.baidu.com/index.aspx

​    在用户看来请求的是index.aspx页面，实际上服务器端运行的是index.aspx对应的类（前台页面类的对象），先调用ProcessRequest方法，然后调Page_Load方法

  **②MVC:类名+方法名**

   www.baidu.com/news/index

##   2、服务器端创建对象

  **①webForm：页面类对象**

   服务器端创建index.asp的前台页面类的对象‘index.aspx’

  **②MVC：类对象**

   MVC:创建news类对象，并调用Index方法。

​    .NET FrameWork根据路由配置，解析URL，并创建news类的对象，并调用对象的index方法。通过View方法加载视图，然后访问视图文件夹下的index.cshtml

##   3、传值方式

  **①WebForm：CodeBehand**

   前台-->后台

​    前台页面**继承**于后台页面，可以通过**<%...%>**方式，访问后台页面公开的属性（强耦合）

   后台-->前台

​    包含**runat=Server**控件的前台页面，都会以**变量**的方式存在于后台类中，供后台调用

 **MVC: ViewData**

​    把ViewData当作一个中间类在Controller和View之间传输数据。好处：强类型视图。

 

#    三、总结

相同点：WebForm和MVC都是基于ASP.NET**管道模型**上的两种不同的开发方式。

<iframe scrolling="no" src="https://pos.baidu.com/s?hei=104&amp;wid=900&amp;di=u3501897&amp;ltu=https%3A%2F%2Fblog.csdn.net%2Fz15732621582%2Farticle%2Fdetails%2F53870440&amp;psi=bcd1aea7d2a4bc78865dd9f5d5329aa9&amp;dis=0&amp;prot=2&amp;dc=3&amp;cdo=-1&amp;tpr=1558948038462&amp;col=zh-CN&amp;par=2560x999&amp;pss=1391x6702&amp;chi=1&amp;dtm=HTML_POST&amp;cmi=2&amp;ccd=24&amp;drs=1&amp;tcn=1558948038&amp;ltr=https%3A%2F%2Fbbs.csdn.net%2Ftopics%2F392366717&amp;psr=2560x1080&amp;pis=-1x-1&amp;dai=2&amp;ps=3319x399&amp;cfv=0&amp;tlm=1558948038&amp;ti=%E4%BC%A0%E7%BB%9FWebForm%E7%BD%91%E7%AB%99%E5%92%8CMVC%E7%BD%91%E7%AB%99%E8%BF%90%E8%A1%8C%E6%9C%BA%E5%88%B6%E5%AF%B9%E6%AF%94%20-%20%E8%B5%B5%E5%B0%BD%E6%9C%9D%20-%20CSDN%E5%8D%9A%E5%AE%A2&amp;cec=UTF-8&amp;pcs=1391x902&amp;cce=true&amp;dri=0&amp;cja=false&amp;ant=0&amp;exps=111000,119008,110011&amp;ari=2&amp;cpl=1" width="900" height="104" frameborder="0"></iframe>





 			[ 				![img](https://avatar.csdn.net/5/2/1/3_u011535356.jpg) 			](https://me.csdn.net/u011535356) 		

 			 			 			 		



- ![chenhaiming123](https://avatar.csdn.net/9/5/B/3_chenhaiming123.jpg)

  ​              [14期-陈海明：](https://me.csdn.net/chenhaiming123)              知识在于积累，厚积薄发，很棒(8个月前#24楼)

  

- ![sms15732621690](https://avatar.csdn.net/8/B/E/3_sms15732621690.jpg)

  ​              [申明霜：](https://me.csdn.net/sms15732621690)              抢了个沙发(2年前#23楼)查看回复(4)

  

- ![yiwangxiblog](https://avatar.csdn.net/6/6/6/3_yiwangxiblog.jpg)

  ​              [李-晓洁：](https://me.csdn.net/yiwangxiblog)              厉害了组长(2年前#22楼)

  

- ![JYL15732624861](https://avatar.csdn.net/E/A/C/3_jyl15732624861.jpg)

  ​              [焦玉丽：](https://me.csdn.net/JYL15732624861)              学习啦(2年前#21楼)

  

- ![nangeali](https://avatar.csdn.net/D/5/0/3_nangeali.jpg)

  ​              [Heart-Forever：](https://me.csdn.net/nangeali)              分析的很好，学习了。(2年前#20楼)

  

- ![qq_26545305](https://avatar.csdn.net/C/B/9/3_qq_26545305.jpg)

  ​              [LemmonTreelss：](https://me.csdn.net/qq_26545305)              没有比较，就没有收货(2年前#19楼)

  

- ![u013034640](https://avatar.csdn.net/4/7/4/3_u013034640.jpg)

  ​              [傻子点点：](https://me.csdn.net/u013034640)              很细致的内容，一张图胜过千言万语。(2年前#18楼)

  

- ![DJuan15732626157](https://avatar.csdn.net/8/E/C/3_djuan15732626157.jpg)

  ​              [我是太阳啦啦啦：](https://me.csdn.net/DJuan15732626157)              总结的很详细啊！(2年前#17楼)

  

- ![JYL15732624861](https://avatar.csdn.net/E/A/C/3_jyl15732624861.jpg)

  ​              [焦玉丽：](https://me.csdn.net/JYL15732624861)              学的很扎实啊(2年前#16楼)

  

- ![sz15732624895](https://avatar.csdn.net/A/7/5/3_sz15732624895.jpg)

  ​              [宋喆-Sally：](https://me.csdn.net/sz15732624895)              总结的很好 了解了(2年前#15楼)

  

- ![zhang18330699274](https://avatar.csdn.net/2/9/2/3_zhang18330699274.jpg)

  ​              [Emily呀：](https://me.csdn.net/zhang18330699274)              ：WebForm和MVC都是基于ASP.NET管道模型上的两种不同的开发方式。(2年前#14楼)

  

- ![happyniceyq](https://avatar.csdn.net/F/F/5/3_happyniceyq.jpg)

  ​              [杨倩-Yvonne：](https://me.csdn.net/happyniceyq)              学习需要我们不断总结。(2年前#13楼)

  

- ![u013201439](https://avatar.csdn.net/7/9/E/3_u013201439.jpg)

  ​              [Bboy-AJ-任杰：](https://me.csdn.net/u013201439)              ASP.NET的三种开发模式：Web Forms,MVC,Web Pages(2年前#12楼)

  

- ![boniesunshine](https://avatar.csdn.net/C/F/0/3_boniesunshine.jpg)

  ​              [韩丽萍：](https://me.csdn.net/boniesunshine)              总结的很好(2年前#11楼)

  

- ![yxf15732625262](https://avatar.csdn.net/7/4/B/3_yxf15732625262.jpg)

  ​              [杨晓风-linda：](https://me.csdn.net/yxf15732625262)              WebForm和MVC都是基于ASP.NET管道模型上的两种不同的开发方式。(2年前#10楼)

  

查看 28 条热评

####  				*MVC*与*WebForm*的区别		

 				 				 					阅读数  					748 				

 			[ 				原文：http://www.cnblogs.com/birdshover/archive/2009/08/24/1552614.htmlhttp://blog.csdn.net/hdh123123/a... 			](https://blog.csdn.net/xuefeiliuyuxiu/article/details/54428913) 			 									博文 											[来自：	 xuefeiliuyuxiu的专栏](https://blog.csdn.net/xuefeiliuyuxiu) 												 		

####  				*Webform*和*MVC*，为什么*MVC*更好一些？		

 				 				 					阅读数  					2119 				

 			[ 				前言如果你看了最近微软的议程，你会发现他们现在的焦点除了MVC，还是MVC。问题在于为什么微软如此热衷于丢弃传统的APS.NETWebform而转向ASP.NETMVC？本文就主要来讨论这个问题。AS... 			](https://blog.csdn.net/qq_30469045/article/details/53503130) 			 									博文 											[来自：	 俞金洋的博客](https://blog.csdn.net/qq_30469045) 												 		

####  				为什么要从*Webform*过渡到*MVC*中		

 				 				 					阅读数  					896 				

 			[ 				可以说，在未来几年中，Webform的使用会逐渐减少，而取而代之的就是MVC。可能你不会同意我的观点，那么我就试着阐述一下我的观点，如果你还是不能接受，那么请你反驳我。学习一个新语言或者是新架构是需要... 			](https://blog.csdn.net/WuLex/article/details/79007961) 			 									博文 											[来自：	 极客神殿](https://blog.csdn.net/WuLex) 												 		

####  				源码深度解析Spring*Mvc*请求*运行机制*		

 				 				 					阅读数  					1万+ 				

 			[ 				本文依赖的是springmvc4.0.5.RELEASE，通过源码深度解析了解springMvc的请求运行机制。通过源码我们可以知道从客户端发送一个URL请求给springMvc开始，到返回数据给客户... 			](https://blog.csdn.net/liyantianmin/article/details/46948963) 			 									博文 											[来自：	 石头视角](https://blog.csdn.net/liyantianmin) 												 		

<iframe id="iframeu3491668_0" name="iframeu3491668_0" src="https://pos.baidu.com/bcdm?conwid=852&amp;conhei=60&amp;rdid=3491668&amp;dc=3&amp;exps=110011&amp;psi=bcd1aea7d2a4bc78865dd9f5d5329aa9&amp;di=u3491668&amp;dri=0&amp;dis=0&amp;dai=1&amp;ps=3753x423&amp;enu=encoding&amp;dcb=___adblockplus&amp;dtm=HTML_POST&amp;dvi=0.0&amp;dci=-1&amp;dpt=none&amp;tsr=0&amp;tpr=1558948037984&amp;ti=%E4%BC%A0%E7%BB%9FWebForm%E7%BD%91%E7%AB%99%E5%92%8CMVC%E7%BD%91%E7%AB%99%E8%BF%90%E8%A1%8C%E6%9C%BA%E5%88%B6%E5%AF%B9%E6%AF%94%20-%20%E8%B5%B5%E5%B0%BD%E6%9C%9D%20-%20CSDN%E5%8D%9A%E5%AE%A2&amp;ari=2&amp;dbv=0&amp;drs=1&amp;pcs=1391x902&amp;pss=1391x4680&amp;cfv=0&amp;cpl=1&amp;chi=1&amp;cce=true&amp;cec=UTF-8&amp;tlm=1558948038&amp;prot=2&amp;rw=902&amp;ltu=https%3A%2F%2Fblog.csdn.net%2Fz15732621582%2Farticle%2Fdetails%2F53870440&amp;ltr=https%3A%2F%2Fbbs.csdn.net%2Ftopics%2F392366717&amp;ecd=1&amp;uc=2560x999&amp;pis=-1x-1&amp;sr=2560x1080&amp;tcn=1558948038&amp;qn=d357cc2a24706487&amp;dpv=d357cc2a24706487&amp;tt=1558948037532.491.492.500" vspace="0" hspace="0" marginwidth="0" marginheight="0" scrolling="no" style="border:0;vertical-align:bottom;margin:0;width:852px;height:60px" allowtransparency="true" width="852" height="60" frameborder="0" align="center,center"></iframe>

####  				web服务器*运行机制*		

 				 				 					阅读数  					733 				

 			[ 				浏览器作用：1。向远程服务器发送请求2。读取远程服务器返回的字符串数据3。根据字符串数据渲染出一个丰富多彩的页面（建立HTML页面的DOM模型） web服务器运行机制：1。浏览器发送请求数据到服务器2... 			](https://blog.csdn.net/weixin_41351690/article/details/80241355) 			 									博文 											[来自：	 looselyNLL的博客](https://blog.csdn.net/weixin_41351690) 												 		

####  				Asp.net *mvc* 深读*mvc**运行机制*		

 				 				 					阅读数  					1630 				

 			[ 				通过之前的学习，貌似感觉学到一些东西，回想起来却感觉连贯不起来。每次上网都会使用别人写的功能，可我写的怎么就动起来了，如何知道我的代码在哪又怎么使得我的代码能动起来实现其相应功能，不知道其内部机制，我... 			](https://blog.csdn.net/u013108485/article/details/51180999) 			 									博文 											[来自：	 米奇老鼠](https://blog.csdn.net/u013108485) 												 		

####  				ASP.NET *MVC* 5.0——ASP.NET管道原理		

 				 				 					阅读数  					2448 				

 			[ 				1.IIS与ASP.NETIIS与ASP.NET是两个相互独立的管道，在各自管辖的范围内，具有自己的一套机制对HTTP请求进行处理。两个管道通过ISAPI实现联通，IIS是第一道屏障，当对HTTP请求... 			](https://blog.csdn.net/Shiyaru1314/article/details/45010207) 			 									博文 											[来自：	 世界中心的专栏](https://blog.csdn.net/Shiyaru1314) 												 		

####  				论REST架构与*传统**MVC*		

 				 				 					阅读数  					4273 				

 			[ 				一前言：       由于REST可以降低开发的复杂度，提高系统的可伸缩性，增强系统的可扩展性，简化应用系统之间的集成，因而得到了广大开发人员的喜爱，同时得到了业界广泛的支持。比如IBM，Google... 			](https://blog.csdn.net/u013628152/article/details/42709033) 			 									博文 											[来自：	 十五楼亮哥](https://blog.csdn.net/u013628152) 												 		

####  				ASP.NET平台下*MVC*与*WebForm*两种模式区别（图解）		

 				 				 					阅读数  					2884 				

 			[ 				本文将为大家对比ASP.NETMVC与WebForm的区别，通过这种形式我们能更加了解ASP.NETMVC及其工作原理，也是为了令大家今后的开发工作更加方便，快捷。        1.传统WebFor... 			](https://blog.csdn.net/shenhuaikun/article/details/8769073) 			 									博文 											[来自：	 天下只此一家的专栏](https://blog.csdn.net/shenhuaikun) 												 		

#### *Webform*和*MVC*,为什么*MVC*更好一些? - autumn20080101的..._CSDN博客

​                      11-20                    



#### *MVC*与*WebForm*的区别 - xuefeiliuyuxiu的专栏 - CSDN博客

​                      4-18                    

传统WebForm网站和MVC网站运行机制对比  12-25 阅读数 896  先上图看对比: ...博文 来自: 赵尽朝  WebForm与MVC混用 09-12 阅读数 1万+  在现有的WebF...

​           ASP.NET就业实例视频教程（5）*WebForm*控件——更便捷地创建页面         

​           [             在使用ASP.NET的WebForm控件开发网站时，开发人员不用编写HTML、CSS代码也能开发web应用程序，直接对控件进行设置就可完成页面的创建。本课程学习的WebForm控件就是为了、高效的开发web应用程序。n【课程特色】n1、课程设计循序渐进、讲解细致、通俗易懂、非常适合自主学习n2、教学过程贯穿实战案例，边学边用n3、突出技术关键点、并且分析透彻           ](https://edu.csdn.net/course/detail/6712?utm_source=baidutj)                        学院             讲师：  徐照兴                    

####  				我的第一个asp.net *webform**网站*增加对*MVC*支持的历程		

 				 				 					阅读数  					1118 				

 			[ 				中间百度，google了无数次，由于是业余时间弄，费了一周多的时间才搞定，不多说闲话，直接开始。为WebForm项目添加引用System.Web.Abstractions;System.Web.Dyn... 			](https://blog.csdn.net/sxf359/article/details/70550939) 			 									博文 											[来自：	 sxf359的博客](https://blog.csdn.net/sxf359) 												 		

#### *WebForm*页面生命周期及asp.net*运行机制* - 李赛赛的专栏 - CSDN博客

​                      3-29                    

传统WebForm网站和MVC网站运行机制对比 12-25 阅读数 871  先上图看对比: ...博文 来自: 赵尽朝  web服务器运行机制  05-08 阅读数 588  浏览器作用:1...

#### *WebForm*与*MVC*混用 - 左直拳的马桶_日用桶 - CSDN博客

​                      4-10                    

传统WebForm网站和MVC网站运行机制对比  12-25 阅读数 888  先上图看对比: ...博文 来自: 赵尽朝  ASP.NET平台下MVC与WebForm两种模式区别(图解)  04-07 ...

####  				在*webForm*项目加添加asp.net *mvc*项目同时开发		

 				 				 					阅读数  					1777 				

 			[ 				本章将讨论如果在传统的webform项目中怎么添加asp.netmvc项目，实现混合项目开发！ 下面我们将一步一步操作：１．新建一个mvc项目，等会可以从这里copy一些东西到webform项目里面的... 			](https://blog.csdn.net/ddxkjddx/article/details/6687908) 			 									博文 											[来自：	 ddxkjddx的专栏](https://blog.csdn.net/ddxkjddx) 												 		

[![LisenYang](https://avatar.csdn.net/9/3/9/3_yangyisen0713.jpg)](https://blog.csdn.net/yangyisen0713)关注

[LisenYang](https://blog.csdn.net/yangyisen0713)



 422篇文章

 排名:1000+



[![十五楼亮哥](https://avatar.csdn.net/9/E/A/3_u013628152.jpg)](https://blog.csdn.net/u013628152)关注

[十五楼亮哥](https://blog.csdn.net/u013628152)



 525篇文章

 排名:1000+



[![天下只此一家](https://avatar.csdn.net/6/5/5/3_shenhuaikun.jpg)](https://blog.csdn.net/shenhuaikun)关注

[天下只此一家](https://blog.csdn.net/shenhuaikun)



 63篇文章

 排名:千里之外



[![sxf359](https://avatar.csdn.net/4/B/B/3_sxf359.jpg)](https://blog.csdn.net/sxf359)关注

[sxf359](https://blog.csdn.net/sxf359)



 186篇文章

 排名:5000+



#### *WebForm*中使用*MVC* - zhao1949的专栏 - CSDN博客

​                      11-23                    



#### 2-3 了解一个*网站*的部署与*运行机制* - u014643382的博客 - CSDN博客

​                      11-12                    

这个提示,指的是php现在不能创建文件,因为php-fpm现在是以www-data用户运行的...传统WebForm网站和MVC网站运行机制对比- 赵尽朝  12-25 781  先上图看对比:...

####  				*MVC**运行机制*		

 				 				 					阅读数  					1329 				

 			[ 				MVC运行机制ASP.NET是一种建立动态Web应用程序的技术。它是.NET框架的一部分，可以使用任何.NET兼容的语言编写ASP.NET应用程序。相对于Java、PHP等，ASP.NET具有方便性、... 			](https://blog.csdn.net/yongyinmg/article/details/16826893) 			 									博文 											[来自：	 yongyinmg的专栏](https://blog.csdn.net/yongyinmg) 												 		

####  				*WebForm*运行的部分原理		

 				 				 					阅读数  					3626 				

 			[ 				首先WebForm即web窗体包含两个页面文件：aspx前台页面和cs后台页面文件。通过反编译器Reflector我们可以看到在Dll程序集中前台页面和后台页面分别生成了两个不同的类，而且前台页面as... 			](https://blog.csdn.net/yhc0322/article/details/6853807) 			 									博文 											[来自：	 阴慧超的博客](https://blog.csdn.net/yhc0322) 												 		

#### Redux 与*传统**MVC*的比较 - 长歌倚楼的学习记录 - CSDN博客

​                      11-13                    

在开始全新的React项目前,先好好研究一下React两个典型的“轮子”,Reflux和...传统WebForm网站和MVC网站运行机制对比 - 赵尽朝  12-25 782  先上图看对比...

#### *Webform*和*MVC*,为什么*MVC*更好一些? - 俞金洋的博客 - CSDN博客

​                      4-16                    

传统WebForm网站和MVC网站运行机制对比  12-25 阅读数 896  先上图看对比: ...博文 来自: 赵尽朝  VS2013无法创建WebForm和MVC项目的解决方案  03-25 阅读...

####  				*WebForm**网站*和*MVC**网站**运行机制*的区别		

 				 				 					阅读数  					2046 				

 			[ 				WebForm网站和MVC网站运行机制的区别①WebForm网站的运行机制比如说我们现在要访问一个WebForm站点：www.google.com.hk/Default.aspx(仅仅是示例)。我们的... 			](https://blog.csdn.net/hdh123123/article/details/49475245) 			 									博文 											[来自：	 hdh123123的专栏](https://blog.csdn.net/hdh123123) 												 		

<iframe scrolling="no" src="https://pos.baidu.com/s?hei=60&amp;wid=852&amp;di=u3491668&amp;ltu=https%3A%2F%2Fblog.csdn.net%2Fz15732621582%2Farticle%2Fdetails%2F53870440&amp;psi=bcd1aea7d2a4bc78865dd9f5d5329aa9&amp;cmi=2&amp;tpr=1558948038462&amp;pcs=1391x902&amp;exps=111000,119008,110011&amp;col=zh-CN&amp;tcn=1558948039&amp;cja=false&amp;ti=%E4%BC%A0%E7%BB%9FWebForm%E7%BD%91%E7%AB%99%E5%92%8CMVC%E7%BD%91%E7%AB%99%E8%BF%90%E8%A1%8C%E6%9C%BA%E5%88%B6%E5%AF%B9%E6%AF%94%20-%20%E8%B5%B5%E5%B0%BD%E6%9C%9D%20-%20CSDN%E5%8D%9A%E5%AE%A2&amp;par=2560x999&amp;dri=1&amp;cdo=-1&amp;ltr=https%3A%2F%2Fbbs.csdn.net%2Ftopics%2F392366717&amp;cfv=0&amp;psr=2560x1080&amp;pss=1391x6811&amp;ari=2&amp;dtm=HTML_POST&amp;cce=true&amp;dis=0&amp;drs=1&amp;ant=0&amp;chi=1&amp;ps=4796x423&amp;dai=3&amp;cpl=1&amp;ccd=24&amp;dc=3&amp;pis=-1x-1&amp;prot=2&amp;cec=UTF-8&amp;tlm=1558948038" width="852" height="60" frameborder="0"></iframe>

#### IIS对Asp.Net *WebForm*和Asp.Net *Mvc*的处理通用部分 - ..._CSDN博客

​                      5-8                    

传统WebForm网站和MVC网站运行机制对比  12-25 阅读数 911  先上图看对比: ...博文 来自: 赵尽朝  在IIS上部署ASP.NET MVC项目 08-10 阅读数 4808  需求...

####  				*WebForm*页面生命周期及asp.net*运行机制*		

 				 				 					阅读数  					2882 				

 			[ 				﻿﻿1.先上几张原理图着重理解：     现在针对第四副图原理进行解析：流程:1.浏览器发送请求2.服务器软件（IIS）接收,它最终的目的就是为了向客户输出它请求的动态页面生成的html代码。3.服务... 			](https://blog.csdn.net/mss359681091/article/details/51882688) 			 									博文 											[来自：	 李赛赛的专栏](https://blog.csdn.net/mss359681091) 												 		

####  				*WebForm*页面生命周期及asp.net*运行机制*复习		

 				 				 					阅读数  					2032 				

 			[ 				http://blog.csdn.net/sxycxwb/article/details/8242861 			](https://blog.csdn.net/longvslove/article/details/8782795) 			 									博文 											[来自：	 longvslove的专栏](https://blog.csdn.net/longvslove) 												 		

####  				2-3 了解一个*网站*的部署与*运行机制*		

 				 				 					阅读数  					167 				

 			[ 				要想挖掘网站的漏洞，对于网站的架构和运行机制必须有一个了解。使用LNMP架构，搭建开源的博客程序wordpress通过这个过程来了解网站的部署和运行。LNMP架构-**L**inux系统-Web服务*... 			](https://blog.csdn.net/u014643382/article/details/82722013) 			 									博文 											[来自：	 u014643382的博客](https://blog.csdn.net/u014643382) 												 		

####  				*MVC*和*WebForm*的特点和优点		

 				 				 					阅读数  					2349 				

 			[ 				MVC(Model、View、Controller)将一个Web应用分解为：Model、View和Controller。ASP.NETMVC框架提供了一个可以代替ASP.NETWebForm的基于MV... 			](https://blog.csdn.net/syaguang2006/article/details/37573337) 			 									博文 											[来自：	 syaguang2006的专栏](https://blog.csdn.net/syaguang2006) 												 		

####  				ASP.NET*网站*页面加载及运行效率等多方面实战优化		

 				 				 					阅读数  					4401 				

 			[ 				ASP.NET网站优化之-论网站访问优化的重要性！ 			](https://blog.csdn.net/yangyisen0713/article/details/48490355) 			 									博文 											[来自：	 LisenYang的专栏](https://blog.csdn.net/yangyisen0713) 												 		

<iframe src="https://kunpeng-sc.csdnimg.cn/#/preview/237?positionId=62&amp;queryWord=" scrolling="no" width="100%" height="75px" frameborder="0"></iframe>

####  				对ASP.NET*网站*高性能和多并发的设计的讨论		

 				 				 					阅读数  					1102 				

 			[ 				对以下文章内容我要说明下，在财大气粗的互联网公司或为财大气粗的客户服务的不缺钱的主，请立即绕行，以下内容不适合您。以下内容为客户计算资源紧缺，预算紧缺，无法通过增大带宽，增多服务器，购买各种高级服务的... 			](https://blog.csdn.net/linshichen/article/details/71562191) 			 									博文 											[来自：	 linshichen的专栏](https://blog.csdn.net/linshichen) 												 		

####  				asp.net全球化——让你的*网站*在中英文中自由切换		

 				 				 					阅读数  					1390 				

 			[ 				由于最近的一笔单子，为客户做的是代理签证、办理移民的网站，需要用到中英文切换，把我这个从未涉及过这一领域的人儿给小小的吓住了。不过吓归吓，还是要着手做的嘛。中文的、英文的、百度上、谷歌上，到处搜这方面... 			](https://blog.csdn.net/rowland001/article/details/17282055) 			 									博文 											[来自：	 懒兔Jodie](https://blog.csdn.net/rowland001) 												 		

####  				*传统*的Web Form(三层架构) 与 *MVC* 的区别		

 				 				 					阅读数  					2921 				

 			[ 				三层架构的正确理解为：数据层(不是“数据访问层“”)、业务逻辑层、表示层。数据层：用户存储数据，多由数据库构成，有时候也用数据文件能辅助存储数据。比如医院的药品列表、人员列表、病例列表等都存储在这一层... 			](https://blog.csdn.net/guigenyi/article/details/46804627) 			 									博文 											[来自：	 guigenyi的专栏](https://blog.csdn.net/guigenyi) 												 		

####  				ASP.NET *MVC*架构与实战系列之一：理解*MVC*底层*运行机制*		

 				 				 					阅读数  					4748 				

 			[ 				今天，我将开启一个崭新的话题：ASP.NETMVC框架的探讨。首先，我们回顾一下ASP.NETWebForm技术与ASP.NETMVC的异同点，并展示各自在Web领域的优劣点。在讨论之前，我对这两种技... 			](https://blog.csdn.net/Sayesan/article/details/47779517) 			 									博文 											[来自：	 Sayesan的专栏](https://blog.csdn.net/Sayesan) 												 		

####  				ASP.NET *MVC*的*运行机制*--url的全局分析		

 				 				 					阅读数  					4708 				

 			[ 				全局     首先我们来看一副图片     首先，用户通过Web浏览器向服务器发送一条url请求，这里请求的url不再是xxx.aspx格式，而是http://HostName/ControllerN... 			](https://blog.csdn.net/Kunar/article/details/6013075) 			 									博文 											[来自：	 Kunar](https://blog.csdn.net/Kunar) 												 		

<iframe src="https://kunpeng-sc.csdnimg.cn/#/preview/234?positionId=63&amp;queryWord=" scrolling="no" width="100%" height="75px" frameborder="0"></iframe>

####  				*传统*的Web处理模式与*MVC*处理模式的区别		

 				 				 					阅读数  					996 				

 			[ 				传统的Web处理模式如图(左边客户端，右边服务器)：用户在浏览器地址栏中输入要访问的地址（例如：www.itcast/index.aspx）,通过浏览器向服务器发送请求报文。服务器通过IIS软件接收后... 			](https://blog.csdn.net/u010011371/article/details/43341159) 			 									博文 											[来自：	 雨忆古风](https://blog.csdn.net/u010011371) 												 		

####  				*传统*的web和*MVC*处理方式		

 				 				 					阅读数  					1683 				

 			[ 				今天主要来学习一下传统的web和mvc处理方式的异同点。先看web处理方式。  左边是客户端。右边是服务器软件。服务器会有一个IIS服务器软件。从客户端发送的请求（例如www.TGB.cn/index... 			](https://blog.csdn.net/u010176014/article/details/42192421) 			 									博文 											[来自：	 博学之,审问之,慎思之,明辨之,笃行之](https://blog.csdn.net/u010176014) 												 		

####  				使用Asp.Net *MVC*开发真正的Web程序		

 				 				 					阅读数  					2098 				

 			[ 				   现在Asp.netMVC1.0已经正式发了,完全改变我们在.Net平台下开发Web程序的方式.不在像以前的Webform采用事件的方式来控制所有操作.我是从2003年,从Delphi转到asp.... 			](https://blog.csdn.net/dacong/article/details/4027872) 			 									博文 											[来自：	 dacong的专栏](https://blog.csdn.net/dacong) 												 		

####  				*传统*架构与分布式架构SOA的比较		

 				 				 					阅读数  					1万+ 				

 			[ 				传统架构与SOA架构的区别和特点：传统架构：   存在问题：1.模块之间耦合度太高，其中一个升级其他都得升级          2.开发困难，各个团队开发最后都要整合一起          3.系统的... 			](https://blog.csdn.net/u012976158/article/details/53229082) 			 									博文 											[来自：	 追风逐日的博客](https://blog.csdn.net/u012976158) 												 		

####  				CDN与*传统**网站*访问*对比*		

 				 				 					阅读数  					1232 				

 			[ 				CDN的全称是ContentDeliveryNetwork，即内容分发网络。其基本思路是尽可能避开互联网上有可能影响数据传输速度和稳定性的瓶颈和环节，使内容传输的更快、更稳定。通过在网络各处放置节点服... 			](https://blog.csdn.net/zhangdaisylove/article/details/45822935) 			 									博文 											[来自：	 james zhang的博客](https://blog.csdn.net/zhangdaisylove) 												 		

<iframe src="https://kunpeng-sc.csdnimg.cn/#/preview/235?positionId=64&amp;queryWord=" scrolling="no" width="100%" height="75px" frameborder="0"></iframe>

####  				使用*MVC*框架开发*网站*(一)		

 				 				 					阅读数  					7518 				

 			[ 				概述本章您将学会：1.MVC的概念及使用2.MVC与ASP.NET的区别3.路由和URL导向4.控制器和视图5.模型与模型状态6.过滤器7…….第1章_MVC与ASP.NET的区别1.1概述MVC是一... 			](https://blog.csdn.net/bj_xuzhiqiang/article/details/80619012) 			 									博文 											[来自：	 上善若水](https://blog.csdn.net/bj_xuzhiqiang) 												 		

####  						c#.asp.net 开发的*webform*的*网站*					

05-19

 						c#.asp.net 开发的webform的网站				

下载

####  						asp.net*运行机制*图,必备参考					

10-19

 						asp.net运行机制图,必备参考。asp.net运行机制图,必备参考				

下载

####  				*MVC*总结--*MVC*简介以及和*WebForm*区别		

 				 				 					阅读数  					3618 				

 			[ 				什么是MVC   MVC（Model-View-Controller，模型—视图—控制器模式）用于表示一种软件架构模式。它把软件系统分为三个基本部分：模型（Model），视图（View）和控制器（Co... 			](https://blog.csdn.net/u010924834/article/details/41307891) 			 									博文 											[来自：	 To Begin,Begin　　](https://blog.csdn.net/u010924834) 												 		

####  				*WebForm* 运行原理		

 				 				 					阅读数  					698 				

 			[ 				创建一个WebForm，在前台放入一个文本框，后台运行的时候给文本框赋值。前台代码：后台代码：usingSystem;usingSystem.Collections.Generic;usingSyst... 			](https://blog.csdn.net/NetBeginner_/article/details/7884050) 			 									博文 											[来自：	 NetBeginner_的专栏](https://blog.csdn.net/NetBeginner_) 												 		

<iframe src="https://kunpeng-sc.csdnimg.cn/#/preview/236?positionId=65&amp;queryWord=" scrolling="no" width="100%" height="75px" frameborder="0"></iframe>

####  				深入理解*MVC*原理		

 				 				 					阅读数  					1万+ 				

 			[ 				WebMVC简介1.1、Web开发中的请求-响应模型： 在Web世界里，具体步骤如下：1、 Web浏览器（如IE）发起请求，如访问http://sishuok.com2、 Web服务器（如Tomcat... 			](https://blog.csdn.net/DlMmU/article/details/55511308) 			 									博文 											[来自：	 黎先生的博客](https://blog.csdn.net/DlMmU) 												 		

####  				【JavaWeb探究】解析Web运行原理		

 				 				 					阅读数  					6273 				

 			[ 				接下来的一段时间，会陆陆续续的总结一下JavaWeb的相关知识。今天这篇博客，作为... 			](https://blog.csdn.net/huanjileaimeidan/article/details/47984445) 			 									博文 											[来自：	 不忘初心](https://blog.csdn.net/huanjileaimeidan) 												 		

####  				web应用程序和Web*网站*区别 		

 				 				 					阅读数  					2万+ 				

 			[ 				web应用程序和Web网站区别 Vs2005和VS2008中都有建立web应用程序和Web网站，总搞的大家不知所戳。web应用程序可能是微软为了让程序员很好的从winform过渡到web开发而保留了。... 			](https://blog.csdn.net/swort_177/article/details/4204224) 			 									博文 											[来自：	 swort_177的专栏](https://blog.csdn.net/swort_177) 												 		

####  				ASP.NET Form验证和角色权限		

 				 				 					阅读数  					686 				

 			[ 				第一部分——怎样实现From认证；第二部分——Form认证的实战运用；第三部分——实现单点登录（SingleSignOn）第一部分如何运用Form表单认证一、新建一个测试项目为了更好说明，有必要新建一... 			](https://blog.csdn.net/u011883102/article/details/46010877) 			 									博文 											[来自：	 那是一阵风](https://blog.csdn.net/u011883102) 												 		

####  				.NET中实现*网站*的国际化		

 				 				 					阅读数  					1406 				

 			[ 				原文地址：http://dotnet.9sssd.com/aspnet/art/949网站在开发的过程中需要实现多语言版本，我们暂且认为有英语和汉语两个版本。网站结构包括，UI过程，rest服务，以及... 			](https://blog.csdn.net/XuWei_XuWei/article/details/36423701) 			 									博文 											[来自：	 Hello World](https://blog.csdn.net/XuWei_XuWei) 												 		



####  						*传统**MVC*架构和前后端分离架构模式*对比*					

02-25

 						通过多维度对传统的MVC和前后端分离架构进行对比，深入对比了两种架构方式的优缺点，还有MVC和MVVM的区别与联系。				

下载

####  				Redux 与*传统**MVC*的比较		

 				 				 					阅读数  					160 				

 			[ 				Redux状态管理统一的入口与统一的状态管理store数据库实例state数据库中存贮的数据dispatch用户发起请求action:{type,payload}请求的url以及请求的数据reduce... 			](https://blog.csdn.net/qq_25247589/article/details/81302453) 			 									博文 											[来自：	 长歌倚楼的学习记录](https://blog.csdn.net/qq_25247589) 												 		

####  				IIS对Asp.Net *WebForm*和Asp.Net *Mvc*的处理通用部分		

 				 				 					阅读数  					626 				

 			[ 				.NETFrameWork4的系统全局配置文件(本人安装在C:\Windows\Microsoft.NET\Framework64\v4.0.30319\Config)中添加了一个HttpModule... 			](https://blog.csdn.net/zhangyihui2016/article/details/48297439) 			 									博文 											[来自：	 难道我还不够帅的专栏](https://blog.csdn.net/zhangyihui2016) 												 		

####  				避开*WebForm*天坑，拥抱ASP.Net *MVC*吧		

 				 				 					阅读数  					9527 				

 			[ 				有鹏友在如鹏网的QQ群中提了一个问题：请问，在ASP.Net中如何隐藏一个MenuItem，我想根据不同的权限，对功能菜单进行隐藏，用style不行。 如果要仅仅解答这个问题，很好解答，答案很简单：给... 			](https://blog.csdn.net/cownew/article/details/50394939) 			 									博文 											[来自：	 杨中科](https://blog.csdn.net/cownew) 												 		

####  				不用asp.net *MVC*，用*WebForm*照样可以实现*MVC*		

 				 				 					阅读数  					6600 				

 			[ 				在《避开WebForm天坑，拥抱ASP.Net MVC吧》这篇博客中我讲到了ASP.net WebForm由于一些先天的“诱导犯罪”的缺陷，现在用ASP.net MVC的公司越来越多。但是根据那篇文章... 			](https://blog.csdn.net/cownew/article/details/50400933) 			 									博文 											[来自：	 杨中科](https://blog.csdn.net/cownew) 												 		



####  				解析ASP.NET *WebForm*和*Mvc*开发的区别		

 				 				 					阅读数  					5万+ 				

 			[ 				因为以前主要是做WebFrom开发，对MVC开发并没有太深入的了解。自从来到创新工场的新团队后，用的技术都是自己以前没有接触过的，比如：MVC和EF还有就是WCF，压力一直很大。在很多问题都是不清楚的... 			](https://blog.csdn.net/yisuowushinian/article/details/17646121) 			 									博文 											[来自：	 强子的专栏](https://blog.csdn.net/yisuowushinian) 												 		

####  				ASP.NET之旅--浅谈Asp.net*运行机制*（一）		

 				 				 					阅读数  					1万+ 				

 			[ 				很多Asp.net开发人员都有过Asp的背景，以至于我们开发程序的时候总是停留在“页面”层次思考，也就是说我们常常会只考虑我们现在所做的系统是要完成什么功能，是要做问卷调查网站还是个人网站，而很少在“... 			](https://blog.csdn.net/zhang_xinxiu/article/details/10832805) 			 									博文 											[来自：	 航帆远洋](https://blog.csdn.net/zhang_xinxiu) 												 		

####  				winform 和 *webform* 之间数据交互		

 				 				 					阅读数  					319 				

 			[ 				1，winform发送代码如下： stringstrSystem="YINPL";      stringstrUserIP=GetLocalIP();     //发送登入数据 ... 			](https://blog.csdn.net/zyzBulus/article/details/81502832) 			 									博文 											[来自：	 Michael_Zyz的博客](https://blog.csdn.net/zyzBulus) 												 		

####  				从java web的*mvc*理解asp.net的*mvc*（一）		

 				 				 					阅读数  					1726 				

 			[ 				小编最近修理一个.net做的网站，然而没接触过c#，一时间不知如何是好，只好开始学。发现其结构特点与java有相通之处。1.下面先来看看两者的项目架构：.netmvcjavawebMVC（ssm）：从... 			](https://blog.csdn.net/change_on/article/details/50114827) 			 									博文 											[来自：	 小浩子的博客](https://blog.csdn.net/change_on) 												 		

####  				基于dubbo从*传统**MVC*架构转向SOA架构分布式设计2--（*mvc*->soa）		

 				 				 					阅读数  					659 				

 			[ 				下面运用到一个MVC架构的小项目例子，这个例子我也是随便找的，也就之前学习mybaits的时候用的，springmvc+mybaits的开始改造！1.创建一个maven的parent项目：把不要的文件... 			](https://blog.csdn.net/u011320740/article/details/78561103) 			 									博文 											[来自：	 Menco的博客](https://blog.csdn.net/u011320740) 												 		



####  				c#  asp.net  web程序获取当前文件路径		

 				 				 					阅读数  					5458 				

 			[ 				网上搜到的很多方法拿到的只是iis程序的路径，下面两个获取当前文件的路径System.Web.HttpContext.Current.Server.MapPath(@"../test.txt... 			](https://blog.csdn.net/a616724623/article/details/79582411) 			 									博文 											[来自：	 a616724623的博客](https://blog.csdn.net/a616724623) 												 		

####  				JVM学习05——内存分配与回收		

 				 				 					阅读数  					5680 				

 			[ 				对象的内存分配从大方向上将，就是在堆上分配（但也可能经过JIT编译后被拆散为标量类型并间接在栈上分配），对象主要分配在新生代的Eden区上，如果启动了本地线程分配缓冲，将按线程优先在TLAB（转换后援... 			](https://blog.csdn.net/xu__cg/article/details/53052993) 			 									博文 											[来自：	 小小本科生成长之路](https://blog.csdn.net/xu__cg) 												 		

####  				linux上安装Docker(非常简单的安装方法)		

 				 				 					阅读数  					27万+ 				

 			[ 				最近比较有空，大四出来实习几个月了，作为实习狗的我，被叫去研究Docker了，汗汗！  Docker的三大核心概念：镜像、容器、仓库 镜像：类似虚拟机的镜像、用俗话说就是安装文件。 容器：类似一个轻量... 			](https://blog.csdn.net/qq_36892341/article/details/73918672) 			 									博文 											[来自：	 我走小路的博客](https://blog.csdn.net/qq_36892341) 												 		

####  				centos 查看命令源码		

 				 				 					阅读数  					12万+ 				

 			[ 				# yum install yum-utils   设置源: [base-src\] name=CentOS-5.4 - Base src - baseurl=http://vault.ce... 			](https://blog.csdn.net/silentpebble/article/details/41279285) 			 									博文 											[来自：	 linux/unix](https://blog.csdn.net/silentpebble) 												 		

####  				DirectX修复工具增强版		

 				 				 					阅读数  					203万+ 				

 			[ 				最后更新：2018-12-20  DirectX修复工具最新版：DirectX Repair V3.8 增强版NEW!  版本号：V3.8.0.11638  大小: 107MB/7z格式压缩，189M... 			](https://blog.csdn.net/VBcom/article/details/7245186) 			 									博文 											[来自：	 VBcom的专栏](https://blog.csdn.net/VBcom) 												 		

####  				人脸检测工具face_recognition的安装与应用		

 				 				 					阅读数  					8万+ 				

 			[ 				人脸检测工具face_recognition的安装与应用 			](https://blog.csdn.net/roguesir/article/details/77104246) 			 									博文 											[来自：	 roguesir的博客](https://blog.csdn.net/roguesir) 												 		

####  				frp配置本地服务端口到服务器80端口		

 				 				 					阅读数  					2万+ 				

 			[ 				搭建环境： ubuntu 16.04 LTS （本地服务计算机） ubuntu 14.04 LTS(阿里云服务器) apache tomcat 7 java 7  frp 0.8.1 linux 搭建... 			](https://blog.csdn.net/Yan_Chou/article/details/53406095) 			 									博文 											[来自：	 Anteoy的博客](https://blog.csdn.net/Yan_Chou) 												 		

####  				利用栈实现中缀表达式转前缀表达式		

 				 				 					阅读数  					6953 				

 			[ 				前面既然写了中缀转后缀的，那么现在说下中缀转前缀的，至于后缀（前缀）转中缀，可以根据相关的转换规则自行转换。目的将中缀表达式（即标准的表达式）转换为前缀表达式例如：1+2*3+(4*5+6)7 转换成... 			](https://blog.csdn.net/tutuxs/article/details/54891548) 			 									博文 											[来自：	 Xefvan的博客](https://blog.csdn.net/tutuxs) 												 		

####  				C语言函数操作大全----(超详细)		

 				 				 					阅读数  					3万+ 				

 			[ 				fopen（打开文件） 相关函数 open，fclose 表头文件 #include 定义函数 FILE * fopen(const char * path,const char * mode)... 			](https://blog.csdn.net/u010258235/article/details/45666851) 			 									博文 											[来自：	 独旅天涯](https://blog.csdn.net/u010258235) 												 		

####  				关于SpringBoot bean无法注入的问题（与文件包位置有关）		

 				 				 					阅读数  					24万+ 				

 			[ 				问题场景描述整个项目通过Maven构建，大致结构如下： 核心Spring框架一个module spring-boot-base service和dao一个module server-core 提供系统... 			](https://blog.csdn.net/gefangshuai/article/details/50328451) 			 									博文 											[来自：	 开发随笔](https://blog.csdn.net/gefangshuai) 												 		

####  				即时通讯-Android推送方案（MQTT）		

 				 				 					阅读数  					7583 				

 			[ 				1.什么是MQTT协议MQTT（Message Queuing Telemetry Transport，消息队列遥测传输）是IBM开发的一个即时通讯协议。有可能成为物联网的重要组成部分。该协议支持所有... 			](https://blog.csdn.net/u012987546/article/details/53020916) 			 									博文 											[来自：	 liujun2son](https://blog.csdn.net/u012987546) 												 		

####  				jquery/js实现一个网页同时调用多个倒计时(最新的)		

 				 				 					阅读数  					52万+ 				

 			[ 				jquery/js实现一个网页同时调用多个倒计时(最新的)  最近需要网页添加多个倒计时. 查阅网络,基本上都是千遍一律的不好用. 自己按需写了个.希望对大家有用. 有用请赞一个哦!    //js ... 			](https://blog.csdn.net/wuchengzeng/article/details/50037611) 			 									博文 											[来自：	 Websites](https://blog.csdn.net/wuchengzeng) 												 		

####  				ubuntu16.04 通过命令，修改屏幕分辨率		

 				 				 					阅读数  					1万+ 				

 			[ 				ubuntu16.04 通过命令，修改屏幕分辨率 			](https://blog.csdn.net/l185979505/article/details/52856101) 			 									博文 											[来自：	 l185979505的博客](https://blog.csdn.net/l185979505) 												 		

####  				OpenCV生成标定图(棋盘格)		

 				 				 					阅读数  					7067 				

 			[ 				   网上查了一下工业视觉标定板，少则几百大洋，多则几千大洋，就想在A4纸上山寨打印一个标定图，就是黑白方格相间的那种。A4纸的标准大小为210*297mm。搞了个把小时，其实想明白了之后很简单。从每... 			](https://blog.csdn.net/eric_e/article/details/79570454) 			 									博文 											[来自：	 eric_e的博客](https://blog.csdn.net/eric_e) 												 		

####  				魔兽争霸3冰封王座1.24e 多开联机补丁 信息发布与收集点		

 				 				 					阅读数  					5万+ 				

 			[ 				畅所欲言！ 			](https://blog.csdn.net/Smile_qiqi/article/details/32724931) 			 									博文 											[来自：	 Smile_qiqi的专栏](https://blog.csdn.net/Smile_qiqi) 												 		

####  				Xmanager 5 远程连接linux图形界面		

 				 				 					阅读数  					4万+ 				

 			[ 				准备环境：Windows客户端安装 Xmanager 软件我用的Xmanager Enterprise 5  Linux系统环境[root@localhost ~\]# cat /etc/issue C... 			](https://blog.csdn.net/fgf00/article/details/50965686) 			 									博文 											[来自：	 人生就是一场修行](https://blog.csdn.net/fgf00) 												 		

####  				MATLAB中注释一段程序		

 				 				 					阅读数  					4万+ 				

 			[ 				在MATLAB中，可以注释一段程序。 使用“%{”和“%}”。 例如 %{ 。。。 %} 即可。 经典方法是用 if 0，但缺点是不够直观，注释掉的内容仍然保持代码的颜色。现在可以用 ... 			](https://blog.csdn.net/zd0303/article/details/7058457) 			 									博文 											[来自：	 知识小屋](https://blog.csdn.net/zd0303) 												 		

####  				Java设计模式学习06——静态代理与动态代理		

 				 				 					阅读数  					2万+ 				

 			[ 				一、代理模式为某个对象提供一个代理，从而控制这个代理的访问。代理类和委托类具有共同的父类或父接口，这样在任何使用委托类对象的地方都可以使用代理类对象替代。代理类负责请求的预处理、过滤、将请求分配给委托... 			](https://blog.csdn.net/xu__cg/article/details/52970885) 			 									博文 											[来自：	 小小本科生成长之路](https://blog.csdn.net/xu__cg) 												 		

####  				利用CSS设置背景图片不显示的问题		

 				 				 					阅读数  					1618 				

 			[ 				用CSS写背景图片，background-image:url("1.jpg"); 但是一直都不显示图片，只有原本写好的div的边框。 一般不显示都是路径写错的问题，（图片的相对路径是指相对于写这条c... 			](https://blog.csdn.net/yovven/article/details/75252871) 			 									博文 											[来自：	 yovven的博客](https://blog.csdn.net/yovven) 												 		

​                                                                                      [                         设计制作学习                    ](https://edu.csdn.net/combos/o363_l0_t)                                                                                                          [                         机器学习教程                    ](https://edu.csdn.net/courses/o5329_s5330_k)                                                                                                          [                         Objective-C培训                    ](https://edu.csdn.net/courses/o280_s351_k)                                                                                                          [                         交互设计视频教程                    ](https://edu.csdn.net/combos/o7115_s388_l0_t)                                                                                                          [                         颜色模型                    ](https://edu.csdn.net/course/play/5599/104252)                                                             

​             [               ![img](https://avatar.csdn.net/E/0/5/3_z15732621582.jpg)                               ![img](https://g.csdnimg.cn/static/user-reg-year/1x/4.png)                           ](https://blog.csdn.net/z15732621582)                      

​                 [赵尽朝](https://blog.csdn.net/z15732621582)             

​                              关注                      

- [原创](https://blog.csdn.net/z15732621582?t=1)

  [184](https://blog.csdn.net/z15732621582?t=1)

- 粉丝

  88

- 喜欢

  48

- 评论

  3542

- 等级：

  ​                 [                                                                                    ](https://blog.csdn.net/home/help.html#level)             

- 访问：

  ​                 25万+            

- 积分：

  ​                 1万+            

- 排名：

  2081

勋章：

​                           ![img](https://g.csdnimg.cn/static/user-medal/zhuanlan.svg)                                                   

<iframe id="iframe6096221_0" name="iframe6096221_0" onload="BAIDU_SSP_cacheRequest('ef54b7eca0c50ab4', this);" src="https://pos.baidu.com/bcdm?conwid=300&amp;conhei=250&amp;rtbid=3084354&amp;rdid=13314292&amp;dc=2&amp;exps=110011&amp;psi=bcd1aea7d2a4bc78865dd9f5d5329aa9&amp;di=6096221&amp;dri=0&amp;dis=0&amp;dai=0&amp;ps=332x91&amp;enu=encoding&amp;dcb=___adblockplus&amp;dtm=HTML_POST&amp;dvi=0.0&amp;dci=-1&amp;dpt=none&amp;tsr=0&amp;tpr=1558948037984&amp;ti=%E4%BC%A0%E7%BB%9FWebForm%E7%BD%91%E7%AB%99%E5%92%8CMVC%E7%BD%91%E7%AB%99%E8%BF%90%E8%A1%8C%E6%9C%BA%E5%88%B6%E5%AF%B9%E6%AF%94%20-%20%E8%B5%B5%E5%B0%BD%E6%9C%9D%20-%20CSDN%E5%8D%9A%E5%AE%A2&amp;ari=2&amp;dbv=0&amp;drs=1&amp;pcs=1391x902&amp;pss=1391x9713&amp;cfv=0&amp;cpl=1&amp;chi=1&amp;cce=true&amp;cec=UTF-8&amp;tlm=1558948042&amp;prot=2&amp;rw=902&amp;ltu=https%3A%2F%2Fblog.csdn.net%2Fz15732621582%2Farticle%2Fdetails%2F53870440&amp;ltr=https%3A%2F%2Fbbs.csdn.net%2Ftopics%2F392366717&amp;ecd=1&amp;uc=2560x999&amp;pis=-1x-1&amp;sr=2560x1080&amp;tcn=1558948042&amp;qn=ef54b7eca0c50ab4&amp;dpv=ef54b7eca0c50ab4&amp;tt=1558948037532.4855.5064.5064" vspace="0" hspace="0" marginwidth="0" marginheight="0" scrolling="no" style="border:0;vertical-align:bottom;margin:0;width:300px;height:250px" allowtransparency="true" width="300" height="250" frameborder="0" align="center,center"></iframe>

### 最新文章

- ​                 [Redis使用不当可能造成的问题](https://blog.csdn.net/z15732621582/article/details/82083668)             
- ​                 [synchronized 和 lock 有什么区别？](https://blog.csdn.net/z15732621582/article/details/81841415)             
- ​                 [怎么实现所有线程在等待某个事件的发生才会去执行](https://blog.csdn.net/z15732621582/article/details/81610317)             
- ​                 [dubbo源码分析 -- 网络编解码](https://blog.csdn.net/z15732621582/article/details/81085389)             
- ​                 [dubbo源码分析 - 序列化与反序列化](https://blog.csdn.net/z15732621582/article/details/81084889)             

### 博主专栏

- ​                         [                             ![img](https://img-blog.csdn.net/20180717105217673?imageView2/5/w/120/h/120)                         ](https://blog.csdn.net/z15732621582/column/info/24944)                     

  [dubbo源码分析](https://blog.csdn.net/z15732621582/column/info/24944)

  文章数：18 篇 访问量：2248

- ​                         [                             ![img](https://img-blog.csdn.net/column?imageView2/5/w/120/h/120)                         ](https://blog.csdn.net/z15732621582/column/info/25070)                     

  [微框架](https://blog.csdn.net/z15732621582/column/info/25070)

  文章数：0 篇 访问量：20

- ​                         [                             ![img](https://img-blog.csdn.net/20180719203147656?imageView2/5/w/120/h/120)                         ](https://blog.csdn.net/z15732621582/column/info/25072)                     

  [Java学习](https://blog.csdn.net/z15732621582/column/info/25072)

  文章数：0 篇 访问量：11

### 个人分类

- ​                 [                     软工开发文档                     2篇                 ](https://blog.csdn.net/z15732621582/article/category/5851525)             
- ​                 [                     机房收费系统                     3篇                 ](https://blog.csdn.net/z15732621582/article/category/5868921)             
- ​                 [                     UML                     6篇                 ](https://blog.csdn.net/z15732621582/article/category/5909143)             
- ​                 [                     C#                     21篇                 ](https://blog.csdn.net/z15732621582/article/category/5972433)             
- ​                 [                     VB.NET                     9篇                 ](https://blog.csdn.net/z15732621582/article/category/6090508)             
- ​                 [                     三层                     4篇                 ](https://blog.csdn.net/z15732621582/article/category/6100068)             
- ​                 [                     感悟                     17篇                 ](https://blog.csdn.net/z15732621582/article/category/6109435)             
- ​                 [                     B/S                     21篇                 ](https://blog.csdn.net/z15732621582/article/category/6230199)             
- ​                 [                     软考                     3篇                 ](https://blog.csdn.net/z15732621582/article/category/6433785)             
- ​                 [                     自考                     3篇                 ](https://blog.csdn.net/z15732621582/article/category/6433790)             
- ​                 [                     步步扎进Java                     41篇                 ](https://blog.csdn.net/z15732621582/article/category/6653397)             
- ​                 [                     Linux                     3篇                 ](https://blog.csdn.net/z15732621582/article/category/7257123)             
- ​                 [                     开发工具                     6篇                 ](https://blog.csdn.net/z15732621582/article/category/7481597)             

​         展开     

### 热门文章

- idea热部署且开启自动编译

  阅读数 79473

- Ueditor在线编辑及存入数据库

  阅读数 12712

- VMware上linux与windows互相复制与粘贴

  阅读数 7444

- “sqlHelper.sqlHelper”的类型初始值设定项引发异常。

  阅读数 6902

- “对非共享成员的引用要求对象引用”

  阅读数 4860

### 最新评论

- 【SSH网上商城】框架

  ​                     [Xumuyang_：](https://my.csdn.net/Xumuyang_)学习了                

- dubbo服务引用-zookeep...

  ​                     [shouchanyue4634：](https://my.csdn.net/shouchanyue4634)图片看不清楚                

- 【学习】身份证号获取个人信息

  ​                     [shang_0122：](https://my.csdn.net/shang_0122)感谢博主分享                

- idea热部署且开启自动编译

  ​                     [qq_36871327：](https://my.csdn.net/qq_36871327)[reply]zxl13735005529[/reply] 参考这篇文章https://blog.csdn.net/kingboyworld/article/details/73440717#commentBox                

- idea热部署且开启自动编译

  ​                     [zxl13735005529：](https://my.csdn.net/zxl13735005529)[reply]qq_36871327[/reply] 872844634@qq.com，谢谢啦                

### 1



<iframe id="iframeu3163270_0" name="iframeu3163270_0" src="https://pos.baidu.com/bcdm?conwid=300&amp;conhei=250&amp;rdid=3163270&amp;dc=3&amp;exps=110011&amp;psi=bcd1aea7d2a4bc78865dd9f5d5329aa9&amp;di=u3163270&amp;dri=0&amp;dis=0&amp;dai=5&amp;ps=2167x91&amp;enu=encoding&amp;dcb=___adblockplus&amp;dtm=HTML_POST&amp;dvi=0.0&amp;dci=-1&amp;dpt=none&amp;tsr=0&amp;tpr=1558948037984&amp;ti=%E4%BC%A0%E7%BB%9FWebForm%E7%BD%91%E7%AB%99%E5%92%8CMVC%E7%BD%91%E7%AB%99%E8%BF%90%E8%A1%8C%E6%9C%BA%E5%88%B6%E5%AF%B9%E6%AF%94%20-%20%E8%B5%B5%E5%B0%BD%E6%9C%9D%20-%20CSDN%E5%8D%9A%E5%AE%A2&amp;ari=2&amp;dbv=0&amp;drs=1&amp;pcs=1391x902&amp;pss=1391x9713&amp;cfv=0&amp;cpl=1&amp;chi=1&amp;cce=true&amp;cec=UTF-8&amp;tlm=1558948043&amp;prot=2&amp;rw=902&amp;ltu=https%3A%2F%2Fblog.csdn.net%2Fz15732621582%2Farticle%2Fdetails%2F53870440&amp;ltr=https%3A%2F%2Fbbs.csdn.net%2Ftopics%2F392366717&amp;ecd=1&amp;uc=2560x999&amp;pis=-1x-1&amp;sr=2560x1080&amp;tcn=1558948043&amp;qn=843216317625019d&amp;tt=1558948037532.5730.5731.5731" vspace="0" hspace="0" marginwidth="0" marginheight="0" scrolling="no" style="border:0;vertical-align:bottom;margin:0;width:300px;height:250px" allowtransparency="true" width="300" height="250" frameborder="0" align="center,center"></iframe>

![CSDN学院](https://csdnimg.cn/pubfooter/images/edu-QR.png)

CSDN学院



CSDN企业招聘



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

-  			

<svg class="icon hover-hide" aria-hidden="true">
					<use xlink:href="https://blog.csdn.net/z15732621582/article/details/53870440#csdnc-comments"></use>
				</svg>

 						28				

 			

 			 		

 			

- ​          				 					 				 				 			
-  				[ 					 						 					 					 				](https://blog.csdn.net/z15732621582/article/details/53729238) 			
-  			[ 				 					 				 				 			](https://blog.csdn.net/z15732621582/article/details/53966326) 		

[                          ](https://mall.csdn.net/vip)    [              ](https://blog.csdn.net/z15732621582/article/details/53870440#)

​          

​                                                      