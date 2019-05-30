



#  				[金源](https://blog.csdn.net/jinyuan0829)

从零开始

https://blog.csdn.net/jinyuan0829/article/list/1?orderby=UpdateTime





---

看原创

- 排序：

  [默认](https://blog.csdn.net/jinyuan0829/article/list)

  [按更新时间](javascript:void(0);)

  [按访问量](https://blog.csdn.net/jinyuan0829/article/list?orderby=ViewCount)

  [ RSS订阅](https://blog.csdn.net/jinyuan0829/rss/list)     

####          [         原        ASP.NET Web API Model-ActionBinding      ](https://blog.csdn.net/JinYuan0829/article/details/39556593)     

​       [          前面的几个篇幅把Model部分的知识点划分成一个个的模块来讲解，而在控制器执行过程中分为好多个过程，对于控制器执行过程(一)主要讲解了过滤器以及在后面的过滤器篇幅中也有讲到，而在过滤器之中还有一些执行过程，也就是在授权过滤器执行完毕后，行为过滤器执行之前，我们要做的就是Model绑定，面前也都说了...       ](https://blog.csdn.net/JinYuan0829/article/details/39556593)     

   

####          [         原        ASP.NET Web API Model-ParameterBinding      ](https://blog.csdn.net/JinYuan0829/article/details/39481611)

​       [         通过上个篇幅的学习了解Model绑定的基础知识，然而在ASP.NET Web  API中Model绑定功能模块并不是被直接调用的，而是要通过本篇要介绍的内容ParameterBinding的一系列对象对其进行封装调用，通过本篇的学习之后也会大概的清楚在Web  API中绑定会有哪几种方式。      ](https://blog.csdn.net/JinYuan0829/article/details/39481611)     

​         2014-09-22 20:42:46       

 

####          [         原        ASP.NET Web API Model-ModelBinder      ](https://blog.csdn.net/JinYuan0829/article/details/39377095)     

​       [         本篇中会为大家介绍在ASP.NET Web  API中ModelBinder的绑定原理以及涉及到的一些对象模型，还有简单的Model绑定示例，在前面的篇幅中讲解了Model元数据、ValueProvider的模块，然后还有本篇的Model绑定的模块这些会结合到后面篇幅中的ParameterBinde...       ](https://blog.csdn.net/JinYuan0829/article/details/39377095)     

​         2014-09-18 19:31:03       

 

####          [         原        ASP.NET Web API Model-ValueProvider      ](https://blog.csdn.net/JinYuan0829/article/details/39347345)     

​       [          前面一篇讲解了Model元数据，Model元数据是在Model绑定中很重要的一部分，只是Model绑定中涉及的知识点比较多，对于ASP.NET  MVC框架来说ASP.NET Web  API框架中在Model绑定部分又新增了参数绑定这么一个机制，这些内容都会在后面的篇幅中说明，前面的这些篇幅都是讲解...      ](https://blog.csdn.net/JinYuan0829/article/details/39347345)     

​         2014-09-17 20:10:58       

 

####          [         原        ASP.NET Web API Model-ModelMetadata      ](https://blog.csdn.net/JinYuan0829/article/details/39298513)     

​       [          前面的几个篇幅主要围绕控制器的执行过程，奈何执行过程中包含的知识点太庞大了，只能一部分一部分的去讲解，在上两篇中我们看到在控制器方法选择器根据请求选定了控制器方法后会生成对应的描述对象之后进入过滤器执行过程中，之后也是我们所讲的在授权过滤器执行之后会执行对Model的系列操作，中间包括Model元...       ](https://blog.csdn.net/JinYuan0829/article/details/39298513)     

​         2014-09-15 21:13:08       



 

####          [         原        ASP.NET Web API 过滤器创建、执行过程(二)      ](https://blog.csdn.net/JinYuan0829/article/details/39272101)     

​       [         作者：金源  出处：http://blog.csdn.net/jinyuan0829 本文版权归作者和CSDN共有，欢迎转载，但未经作者同意必须保留此段声明，且在文章页面      ](https://blog.csdn.net/JinYuan0829/article/details/39272101)     

​         2014-09-14 19:25:13       

 

####          [         原        ASP.NET Web API 过滤器创建、执行过程(一)      ](https://blog.csdn.net/JinYuan0829/article/details/39036065)     

​       [         在上一篇中我们讲到控制器的执行过程系列，这个系列要搁置一段时间了，因为在控制器执行的过程中包含的信息都是要单独的用一个系列来描述的，就如今天的这个篇幅就是在上面内容之后所看到的一个知识要点之一。      ](https://blog.csdn.net/JinYuan0829/article/details/39036065)     

​         2014-09-03 23:24:18       



 

####          [         原        ASP.NET Web API 控制器执行过程(一)      ](https://blog.csdn.net/JinYuan0829/article/details/39014217)     

​       [         前面两篇讲解了控制器的创建过程，只是从框架源码的角度去简单的了解，在控制器创建过后所执行的过程也是尤为重要的，本篇就来简单的说明一下控制器在创建过后将会做哪些工作。      ](https://blog.csdn.net/JinYuan0829/article/details/39014217)     

​         2014-09-02 23:43:11       

 

####          [         原        ASP.NET Web API 控制器创建过程(二)      ](https://blog.csdn.net/JinYuan0829/article/details/38655505)     

​       [         好了，还是回归主题，对于上一篇的内容讲解的只是ASP.NET Web API控制器创建过程中的一个局部知识，在接着上篇内容讲解的之前，我会先回顾一下上篇的内容，并且在本篇里进行整合，让我们要看到的是一个整个的创建过程。      ](https://blog.csdn.net/JinYuan0829/article/details/38655505)     

​         2014-08-18 09:05:22       



​          

####          [         原        ASP.NET Web API 控制器创建过程(一)      ](https://blog.csdn.net/JinYuan0829/article/details/38434367)     

​       [         在前面对管道、路由有了基础的了解过后，本篇将带大家一起学习一下在ASP.NET Web  API中控制器的创建过程，这过程分为几个部分下面的内容会为大家讲解第一个部分，也是ASP.NET Web API框架跟ASP.NET  MVC框架实现上存在不同的一部分。      ](https://blog.csdn.net/JinYuan0829/article/details/38434367)     

 

####          [         原        ASP.NET Web API WebHost宿主环境中管道、路由      ](https://blog.csdn.net/JinYuan0829/article/details/38412517)     

​       [         上篇中说到ASP.NET Web API框架在SelfHost环境中管道、路由的一个形态，本篇就来说明一下在WebHost环境中ASP.NET Web API框架中的管道、路由又是哪一种形态。      ](https://blog.csdn.net/JinYuan0829/article/details/38412517)     

​    

####          [         原        ASP.NET Web API Selfhost宿主环境中管道、路由      ](https://blog.csdn.net/JinYuan0829/article/details/38395451)     

​       [         ASP.NET Web API Selfhost宿主环境中管道、路由 前言 前面的几个篇幅对Web API中的路由和管道进行了简单的介绍并没有详细的去说明一些什么，然而ASP.NET Web API这个框架由于宿主环境的不同在不同的宿主环境中管道中的实现机制和路由的处理方式有着很大的不同，所以...      ](https://blog.csdn.net/JinYuan0829/article/details/38395451)     

​         2014-08-06 08:33:05       

  

####          [         原        ASP.NET Web API 管道模型      ](https://blog.csdn.net/JinYuan0829/article/details/38379135)

​       [         ASP.NET Web  API是一个独立的框架，也有着自己的一套消息处理管道，不管是在WebHost宿主环境还是在SelfHost宿主环境请求和响应都是从消息管道经过的，这是必经之地，本篇就为大家简单的介绍一下ASP.NET  Web API框架中的管道对象模型。      ](https://blog.csdn.net/JinYuan0829/article/details/38379135)     

​         2014-08-05 08:37:38       



​         阅读数 1569        



​         评论数 0        

####          [         原        ASP.NET Web API 开篇示例介绍      ](https://blog.csdn.net/JinYuan0829/article/details/38364755)     

​       [         对于我这个初学者来说ASP.NET Web API这个框架很陌生又熟悉着。 陌生的是ASP.NET Web API是一个全新的框架，对于这个框架在一个项目中起到的作用我暂且还不是很清楚这里也就不妄下结论了，说实话不是我不想而是我无能为力，只能自己去摸索试着去了解它。 熟悉的是ASP.NET Web...      ](https://blog.csdn.net/JinYuan0829/article/details/38364755)     

​         2014-08-04 08:41:43       



​         阅读数 3174        



​         评论数 0        

####          [         原        ASP.NET Web API 路由对象介绍      ](https://blog.csdn.net/JinYuan0829/article/details/38364761)     

​       [         在ASP.NET、ASP.NET MVC和ASP.NET Web  API中这些框架中都会发现有路由的身影，它们的原理都差不多，只不过在不同的环境下作了一些微小的修改，这也是根据每个框架的特性来制定的，今天我们就来看一看路由的结构，虽然我在MVC系列里写过路由的篇幅不过在这里是Web  API 路由对...      ](https://blog.csdn.net/JinYuan0829/article/details/38364761)     

​         2014-08-04 08:43:36       



​         阅读数 1628        



​         评论数 0        