#	DDD领域驱动设计	&&	ABP框架



##	生物与编程

###	细胞生物



![点击查看源网页](../assets/timg-1559526624556.jpg)





OO-

![点击查看源网页](../assets/timg-1559526648380.jpg)





代码块

![点击查看源网页](../assets/timg-1559526800493.jpg)



![点击查看源网页](../assets/timg-1559526867082.jpg)





代码块-组价-模块-系统

![点击查看源网页](../assets/u=2477878436,360823185&fm=26&gp=0.jpg)



![点击查看源网页](../assets/timg-1559526701148.jpg)



消息-通信-神经反馈



![点击查看源网页](../assets/timg-1559526731693.jpg)

动植物

![点击查看源网页](../assets/timg-1559526751683.jpg)

![点击查看源网页](../assets/timg.jpg)



人类社会

![点击查看源网页](../assets/timg-1559526921334.jpg)

![点击查看源网页](../assets/timg-1559526932614.jpg)

![点击查看源网页](../assets/timg-1559526952122.jpg)





![ERP各个模块的架构图PPT](../assets/view.jpg)



![点击查看源网页](../assets/timg-1559527069173.jpg)

























## 领域原型/数据模型

##### [型与模型的视图](https://www.jdon.com/51189)

![http://www.methodsandtools.com/archive/ddd1.gif](../assets/ddd1.gif)



旧型C/S结构带来最大问题是：非常难于维护，修改起来，迁一动百。

数据库已经从过去的中心位置降为一种纯技术实现，数据库只是状态持久化的一种手段

OOL不再做SQL的运输工，不再是跑龙套的了，而是主角，那么如何让Java成为主角呢？那必须依赖[对象](http://www.jdon.com/query/searchThreadAction.shtml?query=面向对象&useGBK=on)这个概念，对象是生活在中间件服务器内存中，它又是数据库数据的业务封装，它和数据库有着
千丝万缕的关系，但是它又和[关系数据库存在天然矛盾](http://www.jdon.com/query/searchThreadAction.shtml?query=阻抗&useGBK=on)，两者水火不容。

尽早抛弃过去的两种影响：过程语言编程习惯和以数据库为中心的设计习惯，从全新的面向[对象](http://www.jdon.com/query/searchThreadAction.shtml?query=面向对象&useGBK=on)角度

> ### 数据库时代的[终结](https://www.jdon.com/artichect/dbover.htm)



###	四色原型分析

四色原型分析模式

领域建模，也就是在UML中画出类图，然后标记上类图四种关系（关联、依赖、继承和实现

这个类图是怎么出来的？为什么选用关联而不是依赖，这些实际都属于分析领域的知识，而[四色图](https://www.jdon.com/mda/archetypes.html)可以说为我们这种分析提炼提供了一种模板或分析框架，这样我们可以按图索骥去分析每个陌生的系统，我们拥有强大的分析方法工具。



![img](../assets/roleparty.gif)





#####	术语解释



|      | **moment-interval**                                          | **role archetype**                                           | **party, place, or thing archetype**                         | **description archetype**                                    |
| ---- | ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| 颜色 | 粉红                                                         | 黄色                                                         | 绿色                                                         | 蓝色                                                         |
|      | 行为                                                         | 角色                                                         | 主语                                                         | 成员描述                                                     |
| 解释 | **时刻-时间段原型**                                          | **角色原型**                                                 | **参与方-地点-物品原型**                                     | **描述原型**                                                 |
|      | 重要在于时间概念上：某个时刻(moment)或一段很短时间(interval)内.意味在**某个时刻发生的事情**因为业务要求或合法性原因需要跟踪；或者过一段时间以后，应该是很短的时间，可以帮助我们寻找到它。 | 任何一个系统都需要人或某个组织介入运行，某个角色被**授权**登录后，与此角色相关的业务特点就应用在他身上。 | Party表示有自己正常的状态并且能够自主控制自己的一些行为，通常情况下，人或组织是一种Party，但象护照，身份证等**注册性标志**等都可以作为Party。**【实体】** | 在设计模式这个实现级别，我们通常使用[组合模式](http://www.jdon.com/designpatterns/composite.htm)来实现种类原型。 |
|      |                                                              |                                                              |                                                              |                                                              |

![四色](../assets/four.gif)

四色原型好像是这样一个场景抽象：某人参与某活动对某事情操作，基本上我们人类的所有活动都可以用这段抽象来表达，这说明四色原型帮助我们更快地分析分辨事物。

数据库建模不能完全反应系统的全部特性和需求，使用同样一套数据库，完全由两套优差不同的设计方案和代码



　过去，我们是将业务逻辑写成SQL送往数据库执行，导致数据库成为业务逻辑主要运行瓶颈，那么，如果我们将
业务逻辑用对象概念表达，而不是SQL，那么我们的业务逻辑就围绕内存中的对象反复计算，这样，负载不是集中在
对象运行的中间件服务器上而对象/中间件都是用OOL语言表达的，无疑，这样的架构，OOL才成为主角。



类图其实是动态的，是一种隐形动态，使用[四色图表达的类图则是一种包含顺序图的完全动态图](https://www.jdon.com/mda/archetypes2.html)，可以说类图是立体多维的，而数据库模型图则是完全静态的。                  







可以将一个复杂的系统划分成一块一块，从而有助于设计实现，当我们一个系统有好几百个类图时，如果不采取四色原型进行归类，那么无疑很混乱，甚至类图提取不正确，概念重复，甚至只有在系统代码实现时才会发现如此严重问题，这对于分析设计来说无疑时重大打击。

### 独立于领域的组件

**Domain-Neutral Component(DNC)**



　　一个业务系统是由多个四色图反复拼装而成，我们称为这种现象是**Domain-Neutral Component**模式





[![Domain-Neutral Component](../assets/dnc.png)](https://www.jdon.com/mda/images/dnc.png)











#####	[一个普通电商系统的商品中心的领域模型图](https://www.cnblogs.com/netfocus/p/5548025.html)





![img](../assets/product-model.bmp)

#####	进化的究极体—-big ball of mud

当产品的复杂度不断增加，而我们有没有去控制控制这种复杂的话，我们的系统会成为ddd中称作big ball of mud（大泥球）的东西。

![img](../assets/2018080412281017.png)





##	DDD领域驱动设计

###	[领域模型概念](https://www.jdon.com/ddd.html)

> 统一了分析和设计编程，使得软件能够更灵活快速跟随需求变化。见下面DDD与传统CRUD或过程脚本或者面向数据表等在开发效率上比较：

![ddd](../assets/23142654)



### [架构对比](https://www.jdon.com/46117)

#####	mvc

 ![img](../assets/MVVM4-300x265-300x265.png)

#####	**[传统分层架构](https://www.cnblogs.com/wangiqngpei557/p/3163985.html)**

![img](../assets/30221104-f0721008a67f4805b82e04c00ed5b84a.jpg)



#####	[Clean架构](http://blog.8thlight.com/uncle-bob/2012/08/13/the-clean-architecture.html)

 [![img](../assets/23144399)](https://www.jdon.com/imageShowInHtml.jsp?id=34056&oid=23144399)

######	清晰架构Clean Architecture，
又称干净架构、清晰架构、整洁架构、清洁架构，是著名软件工程大师Robert C Martin提出的一种[架构整洁之道](https://www.jdon.com/artichect/the-clean-architecture.html)。

 ![img](../assets/cleanarchitecture.png)







##### 	Trinity Architecture

> 这里提出的Trinity Architecture是后端企业应用程序的架构模式。它源于采用依赖性倒置原理（DIP）的典型4层架构。它非常适合（但不限于）领域驱动设计（DDD）应用程序。

Trinity强调[平衡不受控制的灵活性和一致性](https://www.jdon.com/52191)。它提供了八个顶级模块的具体实施指南。 

 ![img](../assets/threeinone.png)



#####	【单体】DDD

[领域驱动设计的经典分层架构](https://www.cnblogs.com/netfocus/archive/2011/10/10/2204949.html)

![img](../assets/07230316-6cddb04bdbf840e18b06e466a613de50.png)

######	[领域驱动架构与N层架构设计](https://www.cnblogs.com/jevo/p/3408522.html)

   Eric  Evans的“领域驱动设计- 应对软件的复杂性“一书中描述和解释了建议的N层架构高层次的图：

![img](../assets/05143440-6c435a190e634ef6a474815d5a741ac0.gif)

######	**[DDD充血型架构](https://www.cnblogs.com/wangiqngpei557/p/3163985.html)**



![img](../assets/30221143-b3e67893815846d9a55e99b24492c283.jpg)



##### [六边形架构](http://alistair.cockburn.us/Hexagonal+architecture)



 [![img](../assets/23144399-1559530786810)](https://www.jdon.com/imageShowInHtml.jsp?id=34057&oid=23144399)

###### 综合六边形



>  我们可能还想区分“内部”和“外部”层之间的交互，其中内部我指的是两个层完全在我们的系统（或BC）内的交互，而外部交互跨越BC。

（例如，RESTful用于开放主机交互，或来自ESB适配器的调用用于已发布的语言交互）命中外部客户端端口。对于后端基础架构层，我们可以看到用于替代对象存储实现的持久性端口，此外，领域层中的对象可以通过[外部服务端口调用其他BC](https://www.jdon.com/51189)。 

![img](../assets/ddd5.gif)



#####	[适配DDD的六边形架构](https://www.cnblogs.com/Leo_wl/p/3866629.html)

![img](../assets/241327009943933.png)







### 	CQRS读写分离

#####	CQRS分离了读写职责
使用[CQRS分离了读写职责]()之后，可以对数据进行读写分离操作来改进性能，可扩展性和安全。如下图：

[![A CQRS architecture with separate read and write stores](../assets/261851423609113.png)





#####	[CQRS的简单实现](https://www.cnblogs.com/yangecnu/p/Introduction-CQRS.html)

](https://images0.cnblogs.com/blog/94031/201408/261851421105570.png)

[![CQRS](../assets/261851443918700.jpg)](https://images0.cnblogs.com/blog/94031/201408/261851438603372.jpg)



###	DDD框架



我们有了[Vaughn Vernon](http://vaughnvernon.co/)的[《实现领域驱动设计》](http://book.douban.com/subject/25844633/)

![img](../assets/241323134946102.jpg)



#####	 领域层：	[DataContext <- Entity <-RootEntity <- Repository]()

完全可以不参考UI设计，不参考原型设计，只按照需求说明书设计领域结构，设计完成之后的领域模型居然很神奇的符合UI和原型的需求。这样的开发流程似乎也很符合TDD的理念，接口先行、之后是测试、再后来是功能、最后才是UI。

[![img](../assets/23141382)](https://www.jdon.com/imageShowInHtml.jsp?id=21456&oid=23141382)

 DDD应对软件设计的复杂性不是作弊,。它将专注于问题域。然而,DDD却是很容易被做错，从而带来显著的成本。普遍认为,虽然可以使用DDD带来明显的好处,但是却难以解释为什么需要实现这些好处。事实是，在过去的十年中,许多项目使用DDD有许多成功与失败。那些最终成功仍然经历了相当多的困难。为什么?我的理解是,对DDD有一些误解，最重要的是,DDD往往减少到仅仅是建设项目中的一个领域模型。但也有一些其他的误解,比如将领域模型看成需要包含所有可能的行动，还有，将领域模型的构件(如通用语言)作为直接实现细节(抽象和具象不分)。



DDD的起点是它为设计服务的，然后才可能为落地实现细节的目的服务。所以,事实上,实现细节在哪里?

[DDD有助于发现高层架构，发现软件需要复制的领域中的机制和动态](https://www.jdon.com/46744)。具体地说，它意味着一个好DDD分析会最小化减轻领域专家和软件架构师之间的误解，并且减少了后续昂贵的需求变化的数量。

理解领域模型如何准确地映射到编程实现是至关重要的，这一步往往决定了项目的成败。



### UOW单元工作

[![img](../assets/23141037)](https://www.jdon.com/imageShowInHtml.jsp?id=21121&oid=23141037)





【[阳光铭睿](https://home.cnblogs.com/u/mienreal/) 】[Fami中，我选择了.NET技术平台](https://www.cnblogs.com/mienreal/p/4358806.html)

![img](../assets/231312467245574.png)





###	[领域驱动设计过程中使用的模式](https://www.cnblogs.com/netfocus/archive/2011/10/10/2204949.html)



##### 全图

![img](../assets/07231359-ecf3fa46d1a74dd6b34f0cb76427ea94.png)

##### [战略战术划分](http://mini.eastday.com/mobile/180317004319284.html)

![这里写图片描述](../assets/20180825152558118.jpg)





#####	简图--**战术模式**

#####	![img](../assets/p28.png)

#####	简图--**战略模式**

 ![img](http://www.ouarzy.com/wp-content/uploads/2018/05/strategic.png)



#####  AutoMapper

[通过一张图来表达就是这样的效果：](https://www.cnblogs.com/farb/p/4981387.html)

[![image](../assets/577014-20151120170407452-699851971.png)](http://images2015.cnblogs.com/blog/577014/201511/577014-20151120170407108-499238970.png)