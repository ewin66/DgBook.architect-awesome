[![Fork me on GitHub](https://camo.githubusercontent.com/652c5b9acfaddf3a9c326fa6bde407b87f7be0f4/68747470733a2f2f73332e616d617a6f6e6177732e636f6d2f6769746875622f726962626f6e732f666f726b6d655f72696768745f6f72616e67655f6666373630302e706e67) ](https://github.com/wanglong)

 	[Leo_wlCnBlogs](https://www.cnblogs.com/Leo_wl/) 	

 	  

# 导航

- ​        [ 博客园](https://www.cnblogs.com/)     
- ​         [ 首页](https://www.cnblogs.com/Leo_wl/)     
- ​              
- ​         [ 联系](https://msg.cnblogs.com/send/HackerVirus)    
- ​             
- ​         [ 管理](https://i.cnblogs.com/)     

| [<](javascript:void(0);)                                                        2019年11月                                                            [>](javascript:void(0);) |                                                              |                                                              |                                                             |                                                              |                                                             |      |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ | ----------------------------------------------------------- | ------------------------------------------------------------ | ----------------------------------------------------------- | ---- |
| 日                                                           | 一                                                           | 二                                                           | 三                                                          | 四                                                           | 五                                                          | 六   |
| 27                                                           | 28                                                           | 29                                                           | 30                                                          | 31                                                           | [1](https://www.cnblogs.com/Leo_wl/archive/2019/11/01.html) | 2    |
| 3                                                            | 4                                                            | [5](https://www.cnblogs.com/Leo_wl/archive/2019/11/05.html)  | [6](https://www.cnblogs.com/Leo_wl/archive/2019/11/06.html) | 7                                                            | 8                                                           | 9    |
| 10                                                           | [11](https://www.cnblogs.com/Leo_wl/archive/2019/11/11.html) | [12](https://www.cnblogs.com/Leo_wl/archive/2019/11/12.html) | 13                                                          | [14](https://www.cnblogs.com/Leo_wl/archive/2019/11/14.html) | 15                                                          | 16   |
| 17                                                           | 18                                                           | 19                                                           | 20                                                          | 21                                                           | 22                                                          | 23   |
| 24                                                           | 25                                                           | 26                                                           | 27                                                          | 28                                                           | 29                                                          | 30   |
| 1                                                            | 2                                                            | 3                                                            | 4                                                           | 5                                                            | 6                                                           | 7    |

公告

- **[我的标签](http://weibo.com/u/3209909971/home?wvr=5&lf=reg)** 

  ------

  ​    [2014](http://www.cnblogs.com/xing901022/p/3694709.html)    [2013下](http://www.cnblogs.com/xing901022/p/3248913.html)    [2013上](http://www.cnblogs.com/xing901022/archive/2013/01/18/2857982.html)    [2012下](http://www.cnblogs.com/xing901022/archive/2012/10/19/2699162.html)    [LVS中文](http://zh.linuxvirtualserver.org/)    [开源](http://code.taobao.org/)    [反向代理](http://www.cnblogs.com/xing901022/archive/2013/04/09/3248870.html)    [CUDA](http://www.cnblogs.com/xing901022/p/3248469.html) 

   [![点击这里给我发消息](Dependency injection.assets/button_old_131.gif)](http://wpa.qq.com/msgrd?v=3&uin=1340601454&site=qq&menu=yes)

  微信订阅号：HackerVirus

  ![img](Dependency injection.assets/141844064735565.png) 

   

  技术QQ群:114818988
  [欢迎点击访问个人网站](http://hackervirus.byethost18.com/)[http://hackervirus.sxl.cn/](http://hackervirus.sxl.cn)[![hit counter html code](http://xyz.freelogs.com/counter/index.php?u=hackervirus2020&s=angelus)](http://xyz.freelogs.com/stats/h/hackervirus2020/)
   

  ------

   

  ------

  

    

  ​        昵称：        [             HackerVirus         ](https://home.cnblogs.com/u/Leo_wl/)
  [             9年10个月         ](https://home.cnblogs.com/u/Leo_wl/)
  [             3457         ](https://home.cnblogs.com/u/Leo_wl/followers/)
  [             246         ](https://home.cnblogs.com/u/Leo_wl/followees/)

   [-取消关注](javascript:void(0);)

​         [Dependency injection](https://www.cnblogs.com/Leo_wl/p/11148137.html)          

https://www.cnblogs.com/Leo_wl/p/11148137.html





**阅读目录**

- [Dependency injection](https://www.cnblogs.com/Leo_wl/p/11148137.html#_label0)

[回到目录](https://www.cnblogs.com/Leo_wl/p/11148137.html#_labelTop)

# [Dependency injection](https://www.cnblogs.com/Vincent-yuan/p/11145717.html)

这篇文章主要讲解asp.net core 依赖注入的一些内容。

ASP.NET Core支持依赖注入。这是一种在类和其依赖之间实现控制反转的一种技术(IOC).

## 一．依赖注入概述

1.原始的代码

依赖就是一个对象的创建需要另一个对象。下面的MyDependency是应用中其他类需要的依赖：

[![复制代码](Dependency injection.assets/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public class MyDependency
{
    public MyDependency()
    {
    }

    public Task WriteMessage(string message)
    {
        Console.WriteLine(
            $"MyDependency.WriteMessage called. Message: {message}");

        return Task.FromResult(0);
    }
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

一个MyDependency类被创建使WriteMessage方法对另一个类可用。MyDependency类是IndexModel类的依赖(即IndexModel类的创建需要用到MyDependency类)：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public class IndexModel : PageModel
{
    MyDependency _dependency = new MyDependency();

    public async Task OnGetAsync()
    {
        await _dependency.WriteMessage(
            "IndexModel.OnGetAsync created this message.");
    }
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

2.原始代码分析

IndexModel类创建了MyDependency类，并且直接依赖MyDependency实例。上面的代码依赖是有问题的，并且应该被避免(避免直接创建依赖的实例对象)，

原因如下：

- 需要用一个不同的实现来替换MyDependency,这个类必须被修改

- 如果MyDependency有依赖，他们必须被这个类配置。在一个有很多类依赖MyDependency的大的项目中，配置代码在应用中会很分散。

- 这种实现对于单元测试是困难的。对于MyDependency，应用应该使用mock或者stub，用这种方式是不可能的。

依赖注入解决那些问题：

- 接口的使用抽象了依赖的实现

- 在service container注册依赖。ASP.NET Core提供了一个内置的service container, IServiceProvider.  Services是在应用的Startup.ConfigureServices中被注册。

- 一个类是在构造函数中注入service。框架执行着创建一个带依赖的实例的责任，并且当不需要时，释放。

3.下面是改良后的代码

这示例应用中，IMyDependency接口定义了一个方法：

```
public interface IMyDependency
{
    Task WriteMessage(string message);
}
```

接口被一个具体的类型，MyDependency实现：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public class MyDependency : IMyDependency
{
    private readonly ILogger<MyDependency> _logger;

    public MyDependency(ILogger<MyDependency> logger)
    {
        _logger = logger;
    }

    public Task WriteMessage(string message)
    {
        _logger.LogInformation(
            "MyDependency.WriteMessage called. Message: {MESSAGE}", 
            message);

        return Task.FromResult(0);
    }
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

在示例中，IMydependency实例被请求和用于调用服务的WriteMessage方法：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public class IndexModel : PageModel
{
    private readonly IMyDependency _myDependency;

    public IndexModel(
        IMyDependency myDependency, 
        OperationService operationService,
        IOperationTransient transientOperation,
        IOperationScoped scopedOperation,
        IOperationSingleton singletonOperation,
        IOperationSingletonInstance singletonInstanceOperation)
    {
        _myDependency = myDependency;
        OperationService = operationService;
        TransientOperation = transientOperation;
        ScopedOperation = scopedOperation;
        SingletonOperation = singletonOperation;
        SingletonInstanceOperation = singletonInstanceOperation;
    }

    public OperationService OperationService { get; }
    public IOperationTransient TransientOperation { get; }
    public IOperationScoped ScopedOperation { get; }
    public IOperationSingleton SingletonOperation { get; }
    public IOperationSingletonInstance SingletonInstanceOperation { get; }

    public async Task OnGetAsync()
    {
        await _myDependency.WriteMessage(
            "IndexModel.OnGetAsync created this message.");
    }
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

4.改良代码分析及扩展讲解(使用DI)

 MyDependency在构造函数中，要求有一个ILogger<TCategoryName>。用一种链式的方法使用依赖注入是很常见的。每个依赖依次再请求它自己需要的依赖。(即：MyDependency是一个依赖，同时，创建MyDependency又需要其他依赖：ILogger<TCategoryName>。)

IMyDependency和ILogger<TCategoryName>必须在service  container中注册。IMyDependency是在Startup.ConfigureServices中注册。ILogger<TCategoryName>是被logging abstractions  infrastructure注册，所以它是一种默认已经注册的框架提供的服务。(即框架自带的已经注册的服务，不需要再另外注册)

容器解析ILogger<TCategoryName>,通过利用泛型. 消除注册每一种具体的构造类型的需要。(因为在上面的例子中，ILogger中的泛型类型为MyDependency，但是如果在其他类中使用ILogger<>, 类型则是其他类型，这里使用泛型比较方便)

```
services.AddSingleton(typeof(ILogger<T>), typeof(Logger<T>)); 
```

这是它的注册的语句(框架实现的)，其中的用到泛型，而不是一种具体的类型。

在示例应用中，IMyDependency service是用具体的类型MyDependency来注册的。这个注册包括服务的生命周期(service lifetime)。Service lifetimes随后会讲。

如果服务的构造函数要求一个内置类型，像string,这个类型可以被使用configuration 或者options pattern来注入：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public class MyDependency : IMyDependency
{
    public MyDependency(IConfiguration config)
    {
        var myStringValue = config["MyStringKey"];

        // Use myStringValue
    }

    ...
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

或者 options pattern（注意：不止这些，这里简单举例）

![img](https://img2018.cnblogs.com/blog/1182288/201907/1182288-20190707102025471-2055270746.png)

## 二．框架提供的服务(Framework-provided services)

 Startup.ConfigureServices方法有责任定义应用使用的服务，包括平台功能，例如Entity Framework Core和ASP.NET Core  MVC。最初，IServiceColletion提供给ConfigureServices下面已经定义的服务(依赖于怎样配置host):

 ![img](https://img2018.cnblogs.com/blog/1182288/201907/1182288-20190707103718087-190417058.png)

当一个service  colletion 扩展方法可以用来注册一个服务，习惯是用一个单独的Add{SERVICE_NAME} 扩展方法来注册服务所需要的所有服务。下面的代码是一个怎么使用扩展方法AddDbContext, AddIdentity，和AddMvc, 添加额外的服务到container:

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

    services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    services.AddMvc();
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

更多的信息：[ServiceCollection Class](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.servicecollection) 

## 三. 服务生命周期(Service lifetimes)

为每个注册的服务选择一个合适的生命周期。ASP.NET Core服务可以用下面的声明周期配置：

Transient、Scoped、Singleton

**Transient(临时的)**

临时的生命周期服务是在每次从服务容器中被请求时被创建。这个生命周期对于lightweight(轻量的),stateless(无状态的)服务比较合适。

**Scoped(范围)**

范围生命周期被创建，一旦每个客户端请求时(connection)

警告：当在中间件中使用范围服务时，注入服务到Invoke或者InvokeAsync方法。不要通过构造函数注入，因为那回强制服务表现的像是singleton(单例)。

**Singleton(单独)**

单独生命周期在第一次请求时被创建(或者说当ConfigureService运行并且被service  registration指定时)。之后每一个请求都使用同一个实例。如果应用要求一个单独行为(singleton  behavior)，允许service  container来管理服务生命周期是被推荐的。不要实现一个单例设计模式并且在类中提供用户代码来管理这个对象的生命周期。

警告：从一个singleton来解析一个范围服务(scoped service)是危险的。它可能会造成服务有不正确的状态，当处理随后的请求时。

#### 构造函数注入行为

服务可以被通过两种机制解析：

- IServiceProvider
- ActivatorUtilities : 允许对象创建，可以不通过在依赖注入容器中注入的方式。ActivatorUtilities是使用user-facing abstractions,例如Tag Helpers , MVC controllers 和 model binders.

构造函数可以接受参数，不通过依赖注入提供，但是这些参数必须指定默认值。

当服务被通过IServiceProvider或者ActivatorUtilities解析时，构造函数注入要求一个公共的构造函数。

当服务被ActivatorUtilities解析时，构造函数注入要求一个合适的构造函数存在。构造函数的重载是被支持的，但是只有一个重载可以存在，它的参数可以被依赖注入执行(即：可以被依赖注入执行的，只有一个构造函数的重载)。

## 四. Entity Framework contexts

Entity Framework contexts 通常使用scoped  lifetime ,添加到服务容器中(service container).因为web 应用数据库操作的范围适用于client  request（客户端请求）。默认的生命周期是scoped,如果一个生命周期没有被AddDbContext<TContext>重载指定，当注册database context时。给出生命周期的服务不应该使用一个生命周期比服务的生命周期短的database context.

## 五．Lifetime and registration options

 为了说明lifetime和registration options之间的不同，考虑下面的接口：这些接口表示的任务都是带有唯一标识的操作。取决于这些接口的操作服务的生命周期怎么配置，container提供了要么是同一个要么是不同的服务当被一个类请求时：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public interface IOperation
{
    Guid OperationId { get; }
}

public interface IOperationTransient : IOperation
{
}

public interface IOperationScoped : IOperation
{
}

public interface IOperationSingleton : IOperation
{
}

public interface IOperationSingletonInstance : IOperation
{
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

这些接口在一个Operation类中被实现。Operation 构造函数生成了一个GUID，如果GUID没被提供：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public class Operation : IOperationTransient, 
    IOperationScoped, 
    IOperationSingleton, 
    IOperationSingletonInstance
{
    public Operation() : this(Guid.NewGuid())
    {
    }

    public Operation(Guid id)
    {
        OperationId = id;
    }

    public Guid OperationId { get; private set; }
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

OperationService依赖于其他的Operation 类型被注册。当OperationService被通过依赖注入请求，它要么接收每个服务的一个新实例要么接收一个已经存在的实例(在依赖服务的生命周期的基础上)。

- 当临时服务(transient  services)被创建时，当被从容器中请求时，IOperationTransient服务的OperationId是不同的。OperationService接收到一个IOperationTransient类的实例。这个新实例产生一个不同的OperationId.

- 每个client请求时，scoped services被创建，IOperationScoped  service的OperationId是一样的，在一个client request内。跨越client  requests,两个service享用一个不同的OperationId的值。

- 当singleton和singleton-instance服务一旦被创建，并且被使用跨越所有的client requests和所有的服务，则OperationId跨越所有的service requests是一致的。

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public class OperationService
{
    public OperationService(
        IOperationTransient transientOperation,
        IOperationScoped scopedOperation,
        IOperationSingleton singletonOperation,
        IOperationSingletonInstance instanceOperation)
    {
        TransientOperation = transientOperation;
        ScopedOperation = scopedOperation;
        SingletonOperation = singletonOperation;
        SingletonInstanceOperation = instanceOperation;
    }

    public IOperationTransient TransientOperation { get; }
    public IOperationScoped ScopedOperation { get; }
    public IOperationSingleton SingletonOperation { get; }
    public IOperationSingletonInstance SingletonInstanceOperation { get; }
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

在Startup.ConfigureServices中，每个类型根据命名的生命周期被添加到容器中：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

    services.AddScoped<IMyDependency, MyDependency>();
    services.AddTransient<IOperationTransient, Operation>();
    services.AddScoped<IOperationScoped, Operation>();
    services.AddSingleton<IOperationSingleton, Operation>();
    services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));

    // OperationService depends on each of the other Operation types.
    services.AddTransient<OperationService, OperationService>();
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

IOperationSingletonInstance服务是一个特殊的实例，它的ID是Guid.Empty. 它是清楚的，当这个类型被使用（它的GUID都是0组成的）

示例应用说明了requests内的对象生命周期和两个requests之间的对象生命周期。示例应用的IndexModel请求IOperation的每个类型和OperationService。这个页面展示了所有的这个page model类的和服务的OperationId值，通过属性指定。

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public class IndexModel : PageModel
{
    private readonly IMyDependency _myDependency;

    public IndexModel(
        IMyDependency myDependency, 
        OperationService operationService,
        IOperationTransient transientOperation,
        IOperationScoped scopedOperation,
        IOperationSingleton singletonOperation,
        IOperationSingletonInstance singletonInstanceOperation)
    {
        _myDependency = myDependency;
        OperationService = operationService;
        TransientOperation = transientOperation;
        ScopedOperation = scopedOperation;
        SingletonOperation = singletonOperation;
        SingletonInstanceOperation = singletonInstanceOperation;
    }

    public OperationService OperationService { get; }
    public IOperationTransient TransientOperation { get; }
    public IOperationScoped ScopedOperation { get; }
    public IOperationSingleton SingletonOperation { get; }
    public IOperationSingletonInstance SingletonInstanceOperation { get; }

    public async Task OnGetAsync()
    {
        await _myDependency.WriteMessage(
            "IndexModel.OnGetAsync created this message.");
    }
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

下面的输出展示了两个请求的结果：

![img](https://img2018.cnblogs.com/blog/1182288/201907/1182288-20190707110105669-622841445.png)

从结果看出：

- Transient对象总是不同的。Transient  OperationId的值对于第一个和第二个客户端请求是在OperationService中不同的，并且跨越client  requests. 一个新的实例被提供给每个service request和client request.
- Scoped对象对于一个client request内部是一样的，跨越client request是不同的。
- Singleton对象对于每个对象和每个请求都是一样的，不管Operation实例是否在ConfigureServices中被提供了。

 可以看出，Transient一直在变；Scoped 同一个client request请求内不变；Singleton一直不变；

##  六. Call Services from main（在main中调用services）

 用IServiceScopeFactory.CreateScope创建一个IServiceScope 来解析一个scoped service在应用的范围内。这个方式是有用的对于在Startup中得到一个scoped  service 来运行初始化任务。下面的例子展示了MyScopedServcie怎样包含一个context，在Program.Main中：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public static void Main(string[] args)
{
    var host = CreateWebHostBuilder(args).Build();

    using (var serviceScope = host.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;

        try
        {
            var serviceContext = services.GetRequiredService<MyScopedService>();
            // Use the context here
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred.");
        }
    }

    host.Run();
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

## 七．Scope validation(范围验证)

 当应用在开发环境运行时，默认的service provider 执行检查来验证：

- Scoped services不是直接或间接的被从root service provider中解析
- Scoped services 不是直接或间接的被注入为singletons

 root service provider 是当BuildServiceProvider被调用时被创建的。Root service provider的生命周期对应于应用/服务器 的生命周期，当provider随着应用启动并且当应用关闭时会被释放。

Scoped服务被创建它们的容器释放。如果scoped service在root  container中被创建，服务的生命周期实际上是被提升为singleton,因为它只有当应用或者服务器关闭时才会被root  container释放。验证servcie scopes 注意这些场景，当BuildServiceProvider被调用时。

## 八.Request Services

来自HttpContext的ASP.NET Core request中的可用的services通过HttpContext.RequestServices集合来暴露。

Request Services代表应用中被配置的services和被请求的部分。当对象指定依赖，会被RequestService中的类型满足，而不是ApplicationServices中的。

通常，应用不应该直接使用那些属性。相反的，请求满足那个类型的的这些类，可以通过构造函数并且允许框架注入这些依赖。这使类更容易测试。

注意：请求依赖，通过构造函数参数来得到RequestServices集合更受欢迎。

## 九. Design services for dependency injection

 最佳实践：

- 设计services使用依赖注入来包含它们的依赖
- 避免stateful，静态的方法调用
- 避免在services内直接初始化依赖类。直接初始化是代码关联一个特定的实现
- 使应用的类small, well-factored,和easily tested.

 如果一个类似乎有很多注入的依赖，这通常是它有太多职责的信号，并且违反了Single  Responsibility Principle(SRP)单一职责原则。尝试通过移动一些职责到一个新类来重构这个类。记住，Razor Pages page model classes和MVC controller classes应该专注于UI层面。Business rules和data  access implementation细节应该在那些合适的分开的关系的类中。

#### Disposal of services

 容器为它创建的类调用IDisposable的Dispose。如果一个实例被用户代码添加到容器中，它不会自动释放。

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
// Services that implement IDisposable:
public class Service1 : IDisposable {}
public class Service2 : IDisposable {}
public class Service3 : IDisposable {}

public interface ISomeService {}
public class SomeServiceImplementation : ISomeService, IDisposable {}

public void ConfigureServices(IServiceCollection services)
{
    // The container creates the following instances and disposes them automatically:
    services.AddScoped<Service1>();
    services.AddSingleton<Service2>();
    services.AddSingleton<ISomeService>(sp => new SomeServiceImplementation());

    // The container doesn't create the following instances, so it doesn't dispose of
    // the instances automatically:
    services.AddSingleton<Service3>(new Service3());
    services.AddSingleton(new Service3());
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

即，如果，类是被用户代码添加容器中的，不会自动释放。像下面这种直接new类的。

## 十.Default service container replacement

内置的service container意味着提供服务来满足框架和大多消费应用的需求。我们建议使用功能内置容器，除非你需要一个特殊的功能，内置容器不支持。有些功能在第三方容器支持，但是内置容器不支持：

- Property injection
- Injection based on name
- Child containers
- Custom lifetime management
- Fun<T> support for lazy initializtion

下面的示例，使用Autofac替代内置容器：

- 安装合适的容器包：

- - Autofac

- - Autofac.Extensions.DependencyInjection

- 在Startup.ConfigureServices中配置容器，并且返回IServiceProvider:

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public IServiceProvider ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
    // Add other framework services

    // Add Autofac
    var containerBuilder = new ContainerBuilder();
    containerBuilder.RegisterModule<DefaultModule>();
    containerBuilder.Populate(services);
    var container = containerBuilder.Build();
    return new AutofacServiceProvider(container);
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

要使用第三方容器，Startup.ConfigureServices必须返回IServiceProvider.

- 在DefaultModule中配置Autofac

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public class DefaultModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CharacterRepository>().As<ICharacterRepository>();
    }
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

在运行时，Autofac被用来解析类型和注入依赖。

更多： [Autofac documentation](https://docs.autofac.org/en/latest/integration/aspnetcore.html).

#### Thread safety

创建线程安全的单例服务。如果一个单例服务对一个临时的服务有依赖，这个临时的服务可能需要要求线程安全根据它怎样被单例服务使用。

单例服务的工厂方法，例如AddSingleton<TService>(IServiceColletion, Func<IServiceProvider,  TService>)的第二个参数，不需要线程安全。像一个类型的构造函数，它一次只能被一个线程调用。

## 十一.Recommendations

- Async/await 和 Task 依据service resolution(服务解决)是不支持的。C# 不支持异步的构造函数；因此，推荐的模式是在同步解析服务之后使用异步方法。

- 避免直接在service container中存储数据和配置。例如，用户的购物车不应该被添加到service  container. 配置应该使用option pattern. 相似的，避免data  holder对象可接近其他对象。最好是请求实际的item通过DI.

- 避免静态得到services（例如，静态类型IApplicationBuilder.ApplicationServices的在别处的使用）

- 避免使用service locator pattern. 例如，当你可以用DI时，不要用GetService来获取一个服务。

错误的：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public void MyMethod()
{
    var options = 
        _services.GetService<IOptionsMonitor<MyOptions>>();
    var option = options.CurrentValue.Option;

    ...
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

正确的：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
private readonly MyOptions _options;

public MyClass(IOptionsMonitor<MyOptions> options)
{
    _options = options.CurrentValue;
}

public void MyMethod()
{
    var option = _options.Option;

    ...
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

- 另一个service locator 变量要避免，是注入一个在运行时解析依赖的工厂。那些实践的两者都混合了Inversion of Control策略(即避免依赖注入和其他方式混合使用)。
- 避免静态得到HttpContext（例如，IHttpContextAccessor.HttpContext）

有时候的场景，可能需要忽略其中的建议。

DI是static/global object access patterns的可替代方式。如果你把它和static object access 方式混合使用，可能不能认识到DI的好处。

参考网址：https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2

​    分类:             [[27\]NET Core](https://www.cnblogs.com/Leo_wl/category/225703.html)