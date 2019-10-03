**MediatR****是什么？**





https://github.com/jbogard/MediatR



# 

# MediatR

[![Build Status](68747470733a2f2f63692e6170707665796f722e636f6d2f6170692f70726f6a656374732f7374617475732f6769746875622f6a626f676172642f6d6564696174723f6272616e63683d6d6173746572267376673d74727565.svg)](https://ci.appveyor.com/project/jbogard/mediatr) [![NuGet](68747470733a2f2f696d672e736869656c64732e696f2f6e756765742f64742f6d6564696174722e737667.svg)](https://www.nuget.org/packages/mediatr) [![NuGet](68747470733a2f2f696d672e736869656c64732e696f2f6e756765742f767072652f6d6564696174722e737667.svg)](https://www.nuget.org/packages/mediatr) [![MyGet (68747470733a2f2f696d672e736869656c64732e696f2f6d796765742f6d6564696174722d63692f762f4d6564696174522e737667.svg)](https://camo.githubusercontent.com/633249ebc40386461b7a4013bc4994fe0b9bdc97/68747470733a2f2f696d672e736869656c64732e696f2f6d796765742f6d6564696174722d63692f762f4d6564696174522e737667)](https://myget.org/gallery/mediatr-ci)

Simple mediator implementation in .NET

In-process messaging with no dependencies.

Supports request/response, commands, queries, notifications and  events, synchronous and async with intelligent dispatching via C#  generic variance.

Examples in the [wiki](https://github.com/jbogard/MediatR/wiki).

### 

### Installing MediatR

You should install [MediatR with NuGet](https://www.nuget.org/packages/MediatR):

```
Install-Package MediatR
```

Or via the .NET Core command line interface:

```
dotnet add package MediatR
```

Either commands, from Package Manager Console or .NET Core CLI, will download and install MediatR and all required dependencies.





---



#                   [MediatR](https://www.cnblogs.com/tanmingchao/p/9681975.html)



> **1.MediatR****是什么？**

[![复制代码](copycode.gif)](javascript:void(0);)

```
微软官方eshopOnContainer开源项目中使用到了该工具，

mediatR 是一种中介工具，解耦了消息处理器和消息之间耦合的类库，支持跨平台 .net Standard和.net framework

https://github.com/jbogard/MediatR/wiki 这里是原文地址。其作者就是Automapper的作者。

功能要是简述的话就俩方面：

request/response 请求响应

pub/sub 发布订阅
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

> **2.****使用**

```
nuget: install-package MediatR

MediatR没有其他的依赖项，您需要配置一个工厂委托,用来实例化所有处理程序、管道的行为,和前/后处理器。
```

 

> 3.Autofac完整的IOC注入示例：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
// uncomment to enable polymorphic dispatching of requests, but note that
// this will conflict with generic pipeline behaviors
// builder.RegisterSource(new ContravariantRegistrationSource());

// mediator itself
builder
  .RegisterType<Mediator>()
  .As<IMediator>()
  .InstancePerLifetimeScope();

// request handlers
builder
  .Register<SingleInstanceFactory>(ctx => {
    var c = ctx.Resolve<IComponentContext>();
    return t => c.TryResolve(t, out var o) ? o : null;
  })
  .InstancePerLifetimeScope();

// notification handlers
builder
  .Register<MultiInstanceFactory>(ctx => {
    var c = ctx.Resolve<IComponentContext>();
    return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
  })
  .InstancePerLifetimeScope();

// finally register our custom code (individually, or via assembly scanning)
// - requests & handlers as transient, i.e. InstancePerDependency()
// - pre/post-processors as scoped/per-request, i.e. InstancePerLifetimeScope()
// - behaviors as transient, i.e. InstancePerDependency()
builder.RegisterAssemblyTypes(typeof(MyType).GetTypeInfo().Assembly).AsImplementedInterfaces(); // via assembly scan
//builder.RegisterType<MyHandler>().AsImplementedInterfaces().InstancePerDependency(); // or individually
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

> 4.ASP.NET CORE 使用 IOC注入：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
引用 MediatR nuget:install-package MediatR

引用IOC扩展 nuget:installpackage MediatR.Extensions.Microsoft.DependencyInjection

使用方式：

services.AddMediatR(typeof(MyHandler));

或

services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

目的是为了扫描Handler的实现对象并添加到IOC的容器中
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

> 5.参考示例

> 5.1 请求响应(request/response)，三步：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
步骤一：创建一个消息对象，需要实现IRequest,或IRequest<>接口，表明该对象是处理器的一个对象

public class Ping : IRequest<string> { }

步骤二：创建一个处理器对象

public class PingHandler : IRequestHandler<Ping, string> { public Task<string> Handle(Ping request, CancellationToken cancellationToken) { return Task.FromResult("Pong"); } }

步骤三：最后，通过mediator发送一个消息

var response = await mediator.Send(new Ping()); Debug.WriteLine(response); // "Pong"
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

 

说明：如果某些情况下，如果你的消息发送不需要返回响应结果的话，可以使用AsyncRequestHandler<TRequest>

参考实现：

```
public class OneWay : IRequest { } public class OneWayHandlerWithBaseClass : AsyncRequestHandler<OneWay> { protected override Task Handle(OneWay request, CancellationToken cancellationToken) { // Twiddle thumbs } }
```

 

 

或者需要异步实现可以使用 RequestHandler 

参考实现：

```
public class SyncHandler : RequestHandler<Ping, string> { protected override string Handle(Ping request) { return "Pong"; } }
```

 

 

> 5.1.1 Request的类型说明，比较幼稚了，，

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
IRequest<T> 有返回值

IRequest 无返回值

 

IRequestHandler<T> 该对象的实现对象返回一个 Task 对象

AsyncRequestHandler<T> 该对象的子对象（继承）返回一个 Task 对象

RequestHandler<T> 该对象的子对象（继承） 无返回值

 

IRequestHandler<T,U> 该对象的实现对象返回一个 Task<U> 对象

RequestHandler<T,U> 该对象的子对象（继承）返回一个 U 对象
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

> 5.2 Publishing,依旧三步走

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
步骤一：创建一个用于通知的消息对象，实现INotification接口

public class Ping : INotification { }

步骤二：创建通知的处理器对象

public class Pong1 : INotificationHandler<Ping> { 　　 　　public Task Handle(Ping notification, CancellationToken cancellationToken) { Debug.WriteLine("Pong 1"); return Task.CompletedTask; } } 　　public class Pong2 : INotificationHandler<Ping> { public Task Handle(Ping notification, CancellationToken cancellationToken) { Debug.WriteLine("Pong 2"); return Task.CompletedTask; } }

三步骤：最终使用mediator发布你的消息

await mediator.Publish(new Ping());
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

 

> 5.3 其他：见github作者wiki参考示例

 

 

 

 

 

 

