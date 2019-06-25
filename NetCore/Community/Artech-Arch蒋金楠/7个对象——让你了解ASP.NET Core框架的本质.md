







2019年1月19日，微软技术（苏州）俱乐部成立，我受邀在成立大会上作了一个名为《ASP.NET  Core框架揭秘》的分享。在此次分享中，我按照ASP.NET Core自身的运行原理和设计思想创建了一个 “迷你版” 的ASP.NET  Core框架，并且利用这个 “极简” 的模拟框架阐述了ASP.NET  Core框架最核心、最本质的东西。整个框架涉及到的核心代码不会超过200行，涉及到7个核心的对象。

[PPT下载](https://files.cnblogs.com/files/artech/Inside-ASP-NET-Core-Framework.pdf)
[源代码下载](https://files.cnblogs.com/files/artech/asp-net-core-mini.7z)

> 目录
> 1. 从Hello World谈起
> 2. ASP.NET Core Mini
> 3. Hello World 2
> 4. 第一个对象：HttpContext
> 5. 第二个对象：RequetDelegate
> 6. 第三个对象：Middleware
> 7. 第四个对象：ApplicationBuilder
> 8. 第五个对象：Server
> 9. HttpContext和Server之间的适配
> 10. HttpListenerServer
> 11. 第六个对象：WebHost
> 12. 第七个对象：WebHostBuilder
> 13. 回顾一下Hello World 2
> 14. 打个广告：《ASP.NET Core框架揭秘》

# 1、从Hello World谈起

当我们最开始学习一门技术的时候都喜欢从Hello World来时，貌似和我们本篇的主题不太搭。但事实却非如此，在我们看来如下这个Hello World是对ASP.NET Core框架本质最好的体现。

```
public class Program
{
    public static void Main()
    => new WebHostBuilder()
        .UseKestrel()
        .Configure(app => app.Run(context => context.Response.WriteAsync("Hello World!")))
        .Build()
        .Run();
}
```

如上这个Hello World程序虽然人为地划分为若干行，但是整个应用程序其实只有一个语句。这个语句涉及到了ASP.NET Core程序两个核心对象WebHost和WebHostBuilder。我们可以将WebHost理解为寄宿或者承载Web应用的宿主，应用的启动可以通过启动作为宿主的WebHost来实现。至于WebHostBuilder，顾名思义，就是WebHost的构建者。

在调用WebHostBuilder的Build方法创建出WebHost之前，我们调用了它的两个方法，其中UseKestrel旨在注册一个名为Kestrel的服务器，而Configure方法的调用则是为了注册一个用来处理请求的中间件，后者在响应的主体内容中写入一个“Hello World”文本。

当我们调用Run方法启动作为应用宿主的WebHost的时候，后者会利用WebHostBuilder提供的服务器和中间件构建一个请求处理管道。这个由一个服务器和若干中间件构成的管道就是ASP.NET Core框架的核心，我们接下来的核心任务就是让大家搞清楚这个管道是如何被构建起来的，以及该管道采用怎样的请求处理流程。

[![clip_image002[6\]](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080818607-291482266.jpg)](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080815630-1902225530.jpg)

# 2、ASP.NET Core Mini

在过去这些年中，我不断地被问到同一个问题：如何深入地去一个开发框架。我知道每个人都具有适合自己的学习方式，而且我觉得我个人的学习方法也算不上高效，所以我很少会正面回应这个问题。不过有一个方法我倒很乐意与大家分享，那就是当你在学习一个开发框架的时候不要只关注编程层面的东西，而应该将更多的精力集中到对架构设计层面的学习。

针对某个框架来说，它提供的编程模式纷繁复杂，而底层的设计原理倒显得简单明了。那么如何检验我们对框架的设计原理是否透彻呢，我觉得最好的方式就是根据你的理解对框架进行“再造”。当你按照你的方式对框架进行“重建”的过程中，你会发现很多遗漏的东西。如果被你重建的框架能够支撑一个可以运行的Hello World应用，那么可以基本上证明你已经基本理解了这个框架最本质的东西。

虽然ASP.NET Core目前是一个开源的项目，我们可以完全通过源码来学习它，但是我相信这对于绝大部分人来说是有难度的。为此我们将ASP.NET Core最本质、最核心的部分提取出来，重新构建了一个迷你版的ASP.NET Core框架。

[![clip_image004[6\]](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080821641-164935959.jpg)](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080820607-650756066.jpg)

ASP.NET Core Mini具有如上所示的三大特点。第一、它是对真实ASP.NET Core框架的真实模拟，所以在部分API的定义上我们做了最大限度的简化，但是两者的本质是完全一致的。如果你能理解ASP.NET Core Mini，意味着你也就是理解了真实ASP.NET Core框架。第二、这个框架是可执行的，我们提供的并不是伪代码。第三、为了让大家能够在最短的时间内理解ASP.NET Core框架的精髓，ASP.NET Core Mini必需足够简单，所以我们整个实现的核心代码不会超过200行。

# 3、Hello World 2

既然我们的ASP.NET Core Mini是可执行的，意味着我们可以在上面构建我们自己的应用，如下所示的就是在ASP.NET Core Mini上面开发的Hello World，可以看出它采用了与真实ASP.NET Core框架一致的编程模式。

```
public class Program
{
    public static async Task Main()
    {
        await new WebHostBuilder()
            .UseHttpListener()
            .Configure(app => app
                .Use(FooMiddleware)
                .Use(BarMiddleware)
                .Use(BazMiddleware))
            .Build()
            .StartAsync();
    }

    public static RequestDelegate FooMiddleware(RequestDelegate next)
    => async context => {
        await context.Response.WriteAsync("Foo=>");
        await next(context);
    };

    public static RequestDelegate BarMiddleware(RequestDelegate next)
    => async context => {
            await context.Response.WriteAsync("Bar=>");

            await next(context);
        };

    public static RequestDelegate BazMiddleware(RequestDelegate next)
    => context => context.Response.WriteAsync("Baz");
}
```

我们有必要对上面这个Hello World程序作一个简答的介绍：在创建出WebHostBuilder之后，我们调用了它的扩展方法UseHttpListener注册了一个自定义的基于HttpListener的服务器，我们会在后续内容中介绍该服务器的实现。在随后针对Configure方法的调用中，我们注册了三个中间件。由于中间件最终是通过Delegate对象来体现的，所以我们可以将中间件定义成与Delegate类型具有相同签名的方法。

我们目前可以先不用考虑表示中间件的三个方法为什么需要成如上的形式，只需要知道三个中间件在针对请求的处理流程中都作了些什么。上面的代码很清楚，三个中间件分别会在响应的内容中写入一段文字，所以程序运行后，如果我们利用浏览器访问该应用，会得到如下所示的输出结果。

 [![clip_image006[6\]](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080825603-1802425781.jpg)](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080823666-280208091.jpg)

# 4、第一个对象：HttpContext

正如本篇文章表示所说，我们的ASP.NET  Core Mini由7个核心对象构建而成。第一个就是大家非常熟悉的HttpContext对象，它可以说是ASP.NET  Core应用开发中使用频率最高的对象。要说明HttpContext的本质，还得从请求处理管道的层面来讲。对于由一个服务器和多个中间件构建的管道来说，面向传输层的服务器负责请求的监听、接收和最终的响应，当它接收到客户端发送的请求后，需要将它分发给后续中间件进行处理。对于某个中间件来说，当我们完成了自身的请求处理任务之后，在大部分情况下也需要将请求分发给后续的中间件。请求在服务器与中间件之间，以及在中间件之间的分发是通过共享上下文的方式实现的。

[![clip_image008[6\]](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080828604-2024821875.jpg)](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080826624-1399512848.jpg)

如上图所示，当服务器接收到请求之后，会创建一个通过HttpContext表示的上下文对象，所有中间件都是在这个上下文中处理请求的，那么一个HttpContext对象究竟携带怎样的上下文信息呢？我们知道一个HTTP事务（Transaction）具有非常清晰的界定，即接收请求、发送响应，所以请求和响应是两个基本的要素，也是HttpContext承载的最核心的上下文信息。

我们可以将请求理解为输入、响应理解为输出，所以应用程序可以利用HttpContext得到当前请求所有的输入信息，也可以利用它完成我们所需的所有输出工作。为此我们为ASP.NET Core Mini定义了如下这个极简版本的HttpContext。

```
public class HttpContext
{           
    public  HttpRequest Request { get; }
    public  HttpResponse Response { get; }
}
public class HttpRequest
{
    public  Uri Url { get; }
    public  NameValueCollection Headers { get; }
    public  Stream Body { get; }
}
public class HttpResponse
{
    public  NameValueCollection Headers { get; }
    public  Stream Body { get; }
    public int StatusCode { get; set;}
}
```

如上面的代码片段所示，HttpContext通过它的两个属性Request和Response来表示请求和响应，它们对应的类型分别为HttpRequest和HttpResponse。通过前者，我们可以得到请求的地址、手部集合和主体内容，利用后者，我们可以设置响应状态码，也可以设置首部和主体内容。

# 5、第二个对象：RequestDelegate

RequestDelegate是我们介绍的第二个核心对象。我们从命名可以看出这是一个委托（Delegate）对象，和上面介绍的HttpContext一样，我们也只有从管道的角度才能充分理解这个委托对象的本质。

在从事软件行业10多年来，我对软件的架构设计越来越具有这样的认识：好的设计一定是“简单”的设计。所以每当我在设计某个开发框架的时候，一直会不断告诉我自己：“还能再简单点吗？”。我们上面介绍的ASP.NET Core管道的设计就具有“简单”的特质：Pipeline = Server + Middlewares。但是“还能再简单点吗？”，其实是可以的：我们可以将多个Middleware构建成一个单一的“HttpHandler”，那么整个ASP.NET Core框架将具有更加简单的表达：Pipeline =Server + HttpHandler。

[![clip_image010[6\]](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080832652-765345547.jpg)](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080830664-202350109.jpg)

那么我们如来表达HttpHandler呢？我们可以这样想：既然针对当前请求的所有输入和输出都通过HttpContext来表示，那么HttpHandler就可以表示成一个Action<HttpContext>对象。那么HttpHandler在ASP.NET Core中是通过Action<HttpContext>来表示的吗？其实不是的，原因很简单：Action<HttpContext>只能表示针对请求的 “同步” 处理操作，但是针对HTTP请求既可以是同步的，也可以是异步的，更多地其实是异步的。

那么在.NET Core的世界中如何来表示一个同步或者异步操作呢？你应该想得到，那就是Task对象，那么HttpHandler自然就可以表示为一个Func<HttpContext，Task>对象。由于这个委托对象实在太重要了，所以我们将它定义成一个独立的类型。

[![clip_image012[6\]](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080837612-1362824907.jpg)](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080835615-68548887.jpg)

# 6、第三个对象：Middleware

在对RequestDelegate这个委托对象具有充分认识之后，我们来聊聊中间件又如何表达，这也是我们介绍的第三个核心对象。中间件在ASP.NET Core被表示成一个Func<RequestDelegate, RequestDelegate>对象，也就是说它的输入和输出都是一个RequestDelegate。

[![clip_image014[6\]](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080841607-556751951.jpg)](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080839607-517985300.jpg)

对于为什么会采用一个Func<RequestDelegate,   RequestDelegate>对象来表示中间件，很多初学者会很难理解。我们可以这样的考虑：对于管道的中的某一个中间件来说，由后续中间件组成的管道体现为一个RequestDelegate对象，由于当前中间件在完成了自身的请求处理任务之后，往往需要将请求分发给后续中间件进行处理，所有它它需要将由后续中间件构成的RequestDelegate作为输入。

当代表中间件的委托对象执行之后，我们希望的是将当前中间件“纳入”这个管道，那么新的管道体现的RequestDelegate自然成为了输出结果。所以中间件自然就表示成输入和输出均为RequestDelegate的Func<RequestDelegate, RequestDelegate>对象。

# 7、第四个对象：ApplicationBuilder

ApplicationBuilder是我们认识的第四个核心对象。从命名来看，这是我们接触到的第二个Builder，既然它被命名为ApplicationBuilder，意味着由它构建的就是一个Application。那么在ASP.NET Core框架的语义下应用（Application）又具有怎样的表达呢？

对于这个问题，我们可以这样来理解：既然Pipeline = Server + HttpHandler，那么用来处理请求的HttpHandler不就承载了当前应用的所有职责吗？那么HttpHandler就等于Application，由于HttpHandler通过RequestDelegate表示，那么由ApplicationBuilder构建的Application就是一个RequestDelegate对象。

[![clip_image016[6\]](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080846626-1256355996.jpg)](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080843607-654707895.jpg)

由于表示HttpHandler的RequestDelegate是由注册的中间件来构建的，所以ApplicationBuilder还具有注册中间件的功能。基于ApplicationBuilder具有的这两个基本职责，我们可以将对应的接口定义成如下的形式。Use方法用来注册提供的中间件，Build方法则将注册的中间件构建成一个RequestDelegate对象。

```
public interface  IApplicationBuilder
{
    IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware);
    RequestDelegate Build();
}
```

如下所示的是针对该接口的具体实现。我们利用一个列表来保存注册的中间件，所以Use方法只需要将提供的中间件添加到这个列表中即可。当Build方法被调用之后，我们只需按照与注册相反的顺序依次执行表示中间件的Func<RequestDelegate,  RequestDelegate>对象就能最终构建出代表HttpHandler的RequestDelegate对象。

```
public class ApplicationBuilder : IApplicationBuilder
{
    private readonly List<Func<RequestDelegate, RequestDelegate>> _middlewares = new List<Func<RequestDelegate, RequestDelegate>>();
    public RequestDelegate Build()
    {
        _middlewares.Reverse();
        return httpContext =>
        {
            RequestDelegate next = _ => { _.Response.StatusCode = 404; return Task.CompletedTask; };
            foreach (var middleware in _middlewares)
            {
                next = middleware(next);
            }
            return next(httpContext);
        };
    }

    public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
    {
        _middlewares.Add(middleware);
        return this;
    }
}
```

在调用第一个中间件（最后注册）的时候，我们创建了一个RequestDelegate作为输入，后者会将响应状态码设置为404。所以如果ASP.NET Core应用在没有注册任何中间的情况下总是会返回一个404的响应。如果所有的中间件在完成了自身的请求处理任务之后都选择将请求向后分发，同样会返回一个404响应。

# 8、第五个对象：Server

服务器在管道中的职责非常明确，当我们自动作应用宿主的WebHost的时候，服务它被自动启动。启动后的服务器会绑定到指定的端口进行请求监听，一旦有请求抵达，服务器会根据该请求创建出代表上下文的HttpContext对象，并将该上下文作为输入调用由所有注册中间件构建而成的RequestDelegate对象。

[![clip_image018[6\]](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080849624-1753750215.jpg)](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080847630-2054455715.jpg)

简单起见，我们使用如下这个简写的IServer接口来表示服务器。我们通过定义在IServer接口的唯一方法StartAsync启动服务器，作为参数的handler正是由所有注册中间件共同构建而成的RequestDelegate对象

```
public interface IServer
{ 
    Task StartAsync(RequestDelegate handler);
}
```

# 9、HttpContext和Server之间的适配

面向应用层的HttpContext对象是对请求和响应的封装，但是请求最初来源于服务器，针对HttpContext的任何响应操作也必需作用于当前的服务器才能真正起作用。现在问题来了，所有的ASP.NET  Core应用使用的都是同一个HttpContext类型，但是却可以注册不同类型的服务器，我们必需解决两者之间的适配问题。

[![clip_image020[6\]](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080852622-1143523244.jpg)](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080850625-107550302.jpg)

计算机领域有一句非常经典的话：“任何问题都可以通过添加一个抽象层的方式来解决，如果解决不了，那就再加一层”。同一个HttpContext类型与不同服务器类型之间的适配问题也可可以通过添加一个抽象层来解决，我们定义在该层的对象称为Feature。如上图所示，我们可以定义一系列的Feature接口来为HttpContext提供上下文信息，其中最重要的就是提供请求的IRequestFeature和完成响应的IResponseFeature接口。那么具体的服务器只需要实现这些Feature接口就可以了。

[![clip_image022[6\]](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080856626-710206291.jpg)](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080854627-793637914.jpg)

我们接着从代码层面来看看具体的实现。如下面的代码片段所示，我们定义了一个IFeatureCollection接口来表示存放Feature对象的集合。从定义可以看出这是一个以Type和Object作为Key和Value的字典，Key代表注册Feature所采用的类型，而Value自然就代表Feature对象本身，话句话说我们提供的Feature对象最终是以对应Feature类型（一般为接口类型）进行注册的。为了编程上便利，我们定义了两个扩展方法Set<T>和Get<T>来设置和获取Feature对象。

```
public interface IFeatureCollection : IDictionary<Type, object> { }
public class FeatureCollection : Dictionary<Type, object>, IFeatureCollection { }   
public static partial class Extensions
{
    public static T Get<T>(this IFeatureCollection features) => features.TryGetValue(typeof(T), out var value) ? (T)value : default(T);
    public static IFeatureCollection Set<T>(this IFeatureCollection features, T feature)
    { 
        features[typeof(T)] = feature;
        return features;
    }
}
```

如下所示的用来提供请求和响应IHttpRequestFeature和IHttpResponseFeature接口的定义，可以看出它们具有与HttpRequest和HttpResponse完全一致的成员定义。

```
public interface IHttpRequestFeature
{
    Uri                     Url { get; }
    NameValueCollection     Headers { get; }
    Stream                  Body { get; }
}    
public interface IHttpResponseFeature
{
    int                       StatusCode { get; set; }
    NameValueCollection     Headers { get; }
    Stream                  Body { get; }
}
```

接下来我们来看看HttpContext的具体实现。ASP.NET Core  Mini的HttpContext只包含Request和Response两个属性成员，对应的类型分别为HttpRequest和HttpResponse，如下所示的就是这两个类型的具体实现。我们可以看出HttpRequest和HttpResponse都是通过一个IFeatureCollection对象构建而成的，它们对应的属性成员均有分别由包含在这个Feature集合中的IHttpRequestFeature和IHttpResponseFeature对象来提供的。

```
public class HttpRequest
{
    private readonly IHttpRequestFeature _feature;    
      
    public  Uri Url => _feature.Url;
    public  NameValueCollection Headers => _feature.Headers;
    public  Stream Body => _feature.Body;

    public HttpRequest(IFeatureCollection features) => _feature = features.Get<IHttpRequestFeature>();
}

public class HttpResponse
{
    private readonly IHttpResponseFeature _feature;

    public  NameValueCollection Headers => _feature.Headers;
    public  Stream Body => _feature.Body;
    public int StatusCode { get => _feature.StatusCode; set => _feature.StatusCode = value; }

    public HttpResponse(IFeatureCollection features) => _feature = features.Get<IHttpResponseFeature>();

}
```

HttpContext的实现就更加简单了。如下面的代码片段所示，我们在创建一个HttpContext对象是同样会提供一个IFeatureCollection对象，我们利用该对象创建对应的HttpRequest和HttpResponse对象，并作为对应的属性值。

```
public class HttpContext
{           
    public  HttpRequest Request { get; }
    public  HttpResponse Response { get; }

    public HttpContext(IFeatureCollection features)
    {
        Request = new HttpRequest(features);
        Response = new HttpResponse(features);
    }
}
```

# 10、HttpListenerServer

在对服务器和它与HttpContext的适配原理具有清晰的认识之后，我们来尝试着自己定义一个服务器。在前面的Hello World实例中，我们利用WebHostBuilder的扩展方法UseHttpListener注册了一个HttpListenerServer，我们现在就来看看这个采用HttpListener作为监听器的服务器类型是如何实现的。

由于所有的服务器都需要自动自己的Feature实现来为HttpContext提供对应的上下文信息，所以我们得先来为HttpListenerServer定义相应的接口。对HttpListener稍微了解的朋友应该知道它在接收到请求之后同行会创建一个自己的上下文对象，对应的类型为HttpListenerContext。如果采用HttpListenerServer作为应用的服务器，意味着HttpContext承载的上下文信息最初来源于这个HttpListenerContext，所以Feature的目的旨在解决这两个上下文之间的适配问题。

[![clip_image024[6\]](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080900611-217441256.jpg)](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080857640-459371492.jpg)

如下所示的HttpListenerFeature就是我们为HttpListenerServer定义的Feature。HttpListenerFeature同时实现了IHttpRequestFeature和IHttpResponseFeature，实现的6个属性成员最初都来源于创建该Feature对象提供的HttpListenerContext对象。

```
public class HttpListenerFeature : IHttpRequestFeature, IHttpResponseFeature
{
    private readonly HttpListenerContext _context;
    public HttpListenerFeature(HttpListenerContext context) => _context = context;

    Uri IHttpRequestFeature.Url => _context.Request.Url;
    NameValueCollection IHttpRequestFeature.Headers => _context.Request.Headers;
    NameValueCollection IHttpResponseFeature.Headers => _context.Response.Headers;
    Stream IHttpRequestFeature.Body => _context.Request.InputStream;
    Stream IHttpResponseFeature.Body => _context.Response.OutputStream;
    int IHttpResponseFeature.StatusCode { get => _context.Response.StatusCode; set => _context.Response.StatusCode = value; }
}
```

如下所示的是HttpListenerServer的最终定义。我们在构造一个HttpListenerServer对象的时候可以提供一组监听地址，如果没有提供，会采用“localhost:5000”作为默认的监听地址。在实现的StartAsync方法中，我们启动了在构造函数中创建的HttpListenerServer对象，并在一个循环中通过调用其GetContextAsync方法实现了针对请求的监听和接收。

```
public class HttpListenerServer : IServer
{
    private readonly HttpListener     _httpListener;
    private readonly string[]             _urls;

    public HttpListenerServer(params string[] urls)
    {
        _httpListener = new HttpListener();
        _urls = urls.Any()?urls: new string[] { "http://localhost:5000/"};
    }

    public async Task StartAsync(RequestDelegate handler)
    {
        Array.ForEach(_urls, url => _httpListener.Prefixes.Add(url));    
        _httpListener.Start();
        while (true)
        {
            var listenerContext = await _httpListener.GetContextAsync(); 
            var feature = new HttpListenerFeature(listenerContext);
            var features = new FeatureCollection()
                .Set<IHttpRequestFeature>(feature)
                .Set<IHttpResponseFeature>(feature);
            var httpContext = new HttpContext(features);
            await handler(httpContext);
            listenerContext.Response.Close();
        }
    }
}
```

当HttpListener监听到抵达的请求后，我们会得到一个HttpListenerContext对象，此时我们只需要据此创建一个HttpListenerFeature对象并它分别以IHttpRequestFeature和IHttpResponseFeature接口类型注册到创建FeatureCollection集合上。我们最终利用这个FeatureCollection对象创建出代表上下文的HttpContext，然后将它作为参数调用由所有中间件共同构建的RequestDelegate对象即可。

# 11、第六个对象：WebHost

到目前为止我们已经知道了由一个服务器和多个中间件构成的管道是如何完整针对请求的监听、接收、处理和最终响应的，接下来来讨论这样的管道是如何被构建出来的。管道是在作为应用宿主的WebHost对象启动的时候被构建出来的，在ASP.NET  Core Mini中，我们将表示应用宿主的IWebHost接口简写成如下的形式：只包含一个StartAsync方法用来启动应用程序。

```
public interface IWebHost
{
    Task StartAsync();
}
```

由于由WebHost构建的管道由Server和HttpHandler构成，我们在默认实现的WebHost类型中，我们直接提供者两个对象。在实现的StartAsync方法中，我么只需要将后者作为参数调用前者的StartAsync方法将服务器启动就可以了。

```
public class WebHost : IWebHost
{
    private readonly IServer _server;
    private readonly RequestDelegate _handler; 
    public WebHost(IServer server, RequestDelegate handler)
    {
        _server = server;
        _handler = handler;
    } 
    public Task StartAsync() => _server.StartAsync(_handler);
}
```

# 12、第七个对象：WebHostBuilder

作为最后一个着重介绍的核心对象，WebHostBuilder的使命非常明确：就是创建作为应用宿主的WebHost。由于在创建WebHost的时候需要提供注册的服务器和由所有注册中间件构建而成的RequestDelegate，所以在对应接口IWebHostBuilder中，我们为它定义了三个核心方法。

```
public interface IWebHostBuilder
{
    IWebHostBuilder UseServer(IServer server);
    IWebHostBuilder Configure(Action<IApplicationBuilder> configure);
    IWebHost Build();
}
```

除了用来创建WebHost的Build方法之外，我们提供了用来注册服务器的UseServer方法和用来注册中间件的Configure方法。Configure方法提供了一个类型为Action<IApplicationBuilder>的参数，意味着我们针对中间件的注册是利用上面介绍的IApplicationBuilder对象来完成的。

如下所示的WebHostBuilder是针对IWebHostBuilder接口的默认实现，它具有两个字段分别用来保存注册的中间件和调用Configure方法提供的Action<IApplicationBuilder>对象。当Build方法被调用之后，我们创建一个ApplicationBuilder对象，并将它作为参数调用这些Action<IApplicationBuilder>委托，进而将所有中间件全部注册到这个ApplicationBuilder对象上。我们最终调用它的Build方法得到由所有中间件共同构建的RequestDelegate对象，并利用它和注册的服务器构建作为应用宿主的WebHost对象。

```
public class WebHostBuilder : IWebHostBuilder
{
    private IServer _server;
    private readonly List<Action<IApplicationBuilder>> _configures = new List<Action<IApplicationBuilder>>();   

    public IWebHostBuilder Configure(Action<IApplicationBuilder> configure)
    {
        _configures.Add(configure);
        return this;
    }
    public IWebHostBuilder UseServer(IServer server)
    {
        _server = server;
        return this;
    }   

    public IWebHost Build()
    {
        var builder = new ApplicationBuilder();
        foreach (var configure in _configures)
        {
            configure(builder);
        }
        return new WebHost(_server, builder.Build());
    }
}
```

# 13、回顾一下Hello World 2

到目前为止，我们已经将ASP.NET Core Mini涉及的七个核心对象介绍完了，然后我们再来回顾一下建立在这个模拟框架上的Hello World程序。

```
public class Program
{
    public static async Task Main()
    {
        await new WebHostBuilder()
            .UseHttpListener()
            .Configure(app => app
                .Use(FooMiddleware)
                .Use(BarMiddleware)
                .Use(BazMiddleware))
            .Build()
            .StartAsync();
    }

    public static RequestDelegate FooMiddleware(RequestDelegate next)
    => async context => {
        await context.Response.WriteAsync("Foo=>");
        await next(context);
    };

    public static RequestDelegate BarMiddleware(RequestDelegate next)
    => async context => {
            await context.Response.WriteAsync("Bar=>");

            await next(context);
        };

    public static RequestDelegate BazMiddleware(RequestDelegate next)
    => context => context.Response.WriteAsync("Baz");
}
```

首选我们调用WebHostBuilder的扩展方法UseHttpListener采用如下的方式完成了针对HttpListenerServer的注册。由于中间件体现为一个Func<RequestDelegate,   RequestDelegate>对象，我们自然可以采用与之具有相同声明的方法（FooMiddleware、BarMiddleware和BazMiddleware）来定义对应的中间件。中间件调用HttpResponse的WriteAsync以如下的方式将指定的字符串写入响应主体的输出流。

```
public static partial class Extensions
{
   public static IWebHostBuilder UseHttpListener(this IWebHostBuilder builder, params string[] urls)
    => builder.UseServer(new HttpListenerServer(urls));

    public static Task WriteAsync(this HttpResponse response, string contents)
    {
        var buffer = Encoding.UTF8.GetBytes(contents);
        return response.Body.WriteAsync(buffer, 0, buffer.Length);
     }
}
```

# 14、打个广告：《ASP.NET Core框架揭秘》

ASP.NET Core Mini模拟了真实ASP.NET Core框架最核心的部分，即由服务器和中间件构成的请求处理管道。真正的ASP.NET Core框架自然要复杂得多得多，那么我们究竟遗漏了什么呢？

[![clip_image026[6\]](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080903608-382600093.jpg)](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128080901606-1803323920.jpg)

如上所示的5个部分是ASP.NET  Core  Mini没有涉及的，其中包括依赖注入、以Startup和StartupFilter的中间件注册方式、针对多种数据源的配置系统、诊断日志系统和一系列预定义的中间件，上述的每个方面都涉及到一个庞大的主题，我们将ASP.NET  Core涉及到的方方面都写在我将要出版的《ASP.NET Core框架揭秘》中，如果你想全方面了解一个真实的ASP.NET  Core框架，敬请期待新书出版。

[![image](assets/19327-20190128121646628-598925177-1560304784172.png)](https://img2018.cnblogs.com/blog/19327/201901/19327-20190128121634632-245404936.png)

作者：蒋金楠 
微信公众账号：大内老A
微博：[www.weibo.com/artech](http://www.weibo.com/artech)
如果你想及时得到个人撰写文章以及著作的消息推送，或者想看看个人推荐的技术资料，可以扫描左边二维码（或者长按识别二维码）关注个人公众号）。
本文版权归作者和博客园共有，欢迎转载，但未经作者同意必须保留此段声明，且在文章页面明显位置给出原文连接，否则保留追究法律责任的权利。