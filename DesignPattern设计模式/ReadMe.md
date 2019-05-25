



# 设计模式



## **什么是设计模式**



设计模式（ Design Pattern ）代表了最佳的实践，通常被有经验的面向对象的软件开发人员所采用。设计模式是软件开发人员在软件开发过程中面临的一般问题的解决方案。这些解决方案是众多软件开发人员经过相当长一段时间的试验和错误总结出来的。



上面的解释来自于网络，是比较标准的定义，可以从中筛选出几个关键字来帮助我们理解什么是设计模式：

- 最佳实践
- 解决方案
- 试验和错误总结

从上面的三个关键词中可以总结出，设计模式就是在针对编码过程中遇到的问题总结出来的最佳解决方案。



那么这些问题指的是什么问题呢？面向对象的程序应该具有可维护性、代码可复用性、扩展性及灵活性，要解决的问题就是代码可维护性问题、复用性问题、扩展性问题及灵活性问题。



**简单来说，设计模式就是指导你*如何写出可维护、可复用、可扩展及灵活的代码*。**

## 设计模式的六大原则

- [《设计模式的六大原则》](https://blog.csdn.net/q291611265/article/details/48465113)
  - 开闭原则：对扩展开放,对修改关闭，多使用抽象类和接口。
  - 里氏替换原则：基类可以被子类替换，使用抽象类继承,不使用具体类继承。
  - 依赖倒转原则：要依赖于抽象,不要依赖于具体，针对接口编程,不针对实现编程。
  - 接口隔离原则：使用多个隔离的接口,比使用单个接口好，建立最小的接口。
  - 迪米特法则：一个软件实体应当尽可能少地与其他实体发生相互作用，通过中间类建立联系。
  - 合成复用原则：尽量使用合成/聚合,而不是使用继承。

## 23种常见设计模式

- [《设计模式》](http://www.runoob.com/design-pattern/design-pattern-tutorial.html)
- [《23种设计模式全解析》](https://www.cnblogs.com/susanws/p/5510229.html)
- [《设计模式类图与示例》](https://github.com/ToryZhou/design-pattern)





##	三大类【图说设计模式】：



> 【图说设计模式】
>
> https://design-patterns.readthedocs.io/zh_CN/latest/

### 	看懂UML类图和时序图

##### 类之间的关系

##### 时序图



###	结构型模式（ Structural Patterns ）

结构型模式(Structural Pattern)描述如何将类或者对 象结合在一起形成更大的结构，就像搭积木，可以通过 简单积木的组合形成复杂的、功能更为强大的结构。

结构型模式可以分为类结构型模式和对象结构型模式：

- 类结构型模式关心类的组合，由多个类可以组合成一个更大的

系统，在类结构型模式中一般只存在继承关系和实现关系。 - 对象结构型模式关心类与对象的组合，通过关联关系使得在一 个类中定义另一个类的实例对象，然后通过该对象调用其方法。 根据“合成复用原则”，在系统中尽量使用关联关系来替代继 承关系，因此大部分结构型模式都是对象结构型模式。

### 创建型模式（ Creational Patterns ）

创建型模式(Creational Pattern)对类的实例化过程进行了抽象，能够将软件模块中对象的创建和对象的使用分离。为了使软件的结构更加清晰，外界对于这些对象只需要知道它们共同的接口，而不清楚其具体的实现细节，使整个系统的设计更加符合单一职责原则。

创建型模式在创建什么(What)，由谁创建(Who)，何时创建(When)等方面都为软件设计者提供了尽可能大的灵活性。创建型模式隐藏了类的实例的创建细节，通过隐藏对象如何被创建和组合在一起达到使整个系统独立的目的。

###	行为型模式（ Behavioral Patterns ）

行为型模式(Behavioral Pattern)是对在不同的对象之间划分责任和算法的抽象化。

行为型模式不仅仅关注类和对象的结构，而且重点关注它们之间的相互作用。

通过行为型模式，可以更加清晰地划分类与对象的职责，并研究系统在运行时实例对象 之间的交互。在系统运行时，对象并不是孤立的，它们可以通过相互通信与协作完成某些复杂功能，一个对象在运行时也将影响到其他对象的运行。

行为型模式分为类行为型模式和对象行为型模式两种：

- 类行为型模式：类的行为型模式使用继承关系在几个类之间分配行为，类行为型模式主要通过多态等方式来分配父类与子类的职责。
- 对象行为型模式：对象的行为型模式则使用对象的聚合关联关系来分配行为，对象行为型模式主要是通过对象关联等方式来分配两个或多个类的职责。根据“合成复用原则”，系统中要尽量使用关联关系来取代继承关系，因此大部分行为型设计模式都属于对象行为型设计模式。

## 应用场景

- [《细数JDK里的设计模式》](http://blog.jobbole.com/62314/)
  - 结构型模式：（ Structural Patterns ）
    - 适配器：用来把一个接口转化成另一个接口，如 java.util.Arrays#asList()。
    - 桥接模式：这个模式将抽象和抽象操作的实现进行了解耦，这样使得抽象和实现可以独立地变化，如JDBC；
    - 组合模式：使得客户端看来单个对象和对象的组合是同等的。换句话说，某个类型的方法同时也接受自身类型作为参数，如 Map.putAll，List.addAll、Set.addAll。
    - 装饰者模式：动态的给一个对象附加额外的功能，这也是子类的一种替代方式，如 java.util.Collections#checkedList|Map|Set|SortedSet|SortedMap。
    - 享元模式：使用缓存来加速大量小对象的访问时间，如 valueOf(int)。
    - 代理模式：代理模式是用一个简单的对象来代替一个复杂的或者创建耗时的对象，如 java.lang.reflect.Proxy
  - 创建模式:（ Creational Patterns ）
    - 抽象工厂模式：抽象工厂模式提供了一个协议来生成一系列的相关或者独立的对象，而不用指定具体对象的类型，如 java.util.Calendar#getInstance()。
    - 建造模式(Builder)：定义了一个新的类来构建另一个类的实例，以简化复杂对象的创建，如：java.lang.StringBuilder#append()。
    - 工厂方法：就是 **一个返*** 回具体对象的方法，而不是多个，如 java.lang.Object#toString()、java.lang.Class#newInstance()。
    - 原型模式：使得类的实例能够生成自身的拷贝、如：java.lang.Object#clone()。
    - 单例模式：全局只有一个实例，如 java.lang.Runtime#getRuntime()。
  - 行为模式：（ Behavioral Patterns ）
    - 责任链模式：通过把请求从一个对象传递到链条中下一个对象的方式，直到请求被处理完毕，以实现对象间的解耦。如 javax.servlet.Filter#doFilter()。
    - 命令模式：将操作封装到对象内，以便存储，传递和返回，如：java.lang.Runnable。
    - 解释器模式：定义了一个语言的语法，然后解析相应语法的语句，如，java.text.Format，java.text.Normalizer。
    - 迭代器模式：提供一个一致的方法来顺序访问集合中的对象，如 java.util.Iterator。
    - 中介者模式：通过使用一个中间对象来进行消息分发以及减少类之间的直接依赖，java.lang.reflect.Method#invoke()。
    - 空对象模式：如 java.util.Collections#emptyList()。
    - 观察者模式：它使得一个对象可以灵活的将消息发送给感兴趣的对象，如 java.util.EventListener。
    - 模板方法模式：让子类可以重写方法的一部分，而不是整个重写，如 java.util.Collections#sort()。
- [《Spring-涉及到的设计模式汇总》](https://www.cnblogs.com/hwaggLee/p/4510687.html)
- [《Mybatis使用的设计模式》](https://blog.csdn.net/u012387062/article/details/54719114)

## 单例模式

- [《单例模式的三种实现 以及各自的优缺点》](https://blog.csdn.net/YECrazy/article/details/79481964)
- [《单例模式－－反射－－防止序列化破坏单例模式》](https://www.cnblogs.com/ttylinux/p/6498822.html)
  - 使用枚举类型。

## 责任链模式

TODO

## MVC

- [《MVC 模式》](http://www.runoob.com/design-pattern/mvc-pattern.html)
  - 模型(model)－视图(view)－控制器(controller) 

## IOC

- [《理解 IOC》](https://www.zhihu.com/question/23277575)
- [《IOC 的理解与解释》](https://www.cnblogs.com/NancyStartOnce/p/6813162.html)
  - 正向控制：传统通过new的方式。反向控制，通过容器注入对象。
  - 作用：用于模块解耦。
  - DI：Dependency Injection，即依赖注入，只关心资源使用，不关心资源来源。

## AOP

- [《轻松理解AOP(面向切面编程)》](https://blog.csdn.net/yanquan345/article/details/19760027)
- [《Spring AOP详解》](https://www.cnblogs.com/hongwz/p/5764917.html)
- [《Spring AOP的实现原理》](http://www.importnew.com/24305.html)
  - Spring AOP使用的动态代理，主要有两种方式：JDK动态代理和CGLIB动态代理。
- [《Spring AOP 实现原理与 CGLIB 应用》](https://www.ibm.com/developerworks/cn/java/j-lo-springaopcglib/)
  - Spring AOP 框架对 AOP 代理类的处理原则是：如果目标对象的实现类实现了接口，Spring AOP 将会采用 JDK 动态代理来生成 AOP 代理类；如果目标对象的实现类没有实现接口，Spring AOP 将会采用 CGLIB 来生成 AOP 代理类 

## UML

- [《UML教程》](https://www.w3cschool.cn/uml_tutorial/)

## 重构

- [《架构之重构的12条军规》](http://www.infoq.com/cn/articles/architect-12-rules-complete/)
- [如何用重构的方式让它整洁起来](https://www.jianshu.com/p/fe9efc548179)
- [浅谈代码重构：常用的重构方法有哪些](https://www.2cto.com/kf/201804/737688.html)

## 微服务思想

- [《微服务架构设计》](https://www.cnblogs.com/wintersun/p/6219259.html)
- [《微服务架构技术栈选型手册》](http://www.infoq.com/cn/articles/micro-service-technology-stack)

### 康威定律

- [《微服务架构的理论基础 - 康威定律》](https://yq.aliyun.com/articles/8611)
  - 定律一：组织沟通方式会通过系统设计表达出来，就是说架构的布局和组织结构会有相似。
  - 定律二：时间再多一件事情也不可能做的完美，但总有时间做完一件事情。一口气吃不成胖子，先搞定能搞定的。
  - 定律三：线型系统和线型组织架构间有潜在的异质同态特性。种瓜得瓜，做独立自治的子系统减少沟通成本。
  - 定律四：大的系统组织总是比小系统更倾向于分解。合久必分，分而治之。
- [《微服务架构核⼼20讲》