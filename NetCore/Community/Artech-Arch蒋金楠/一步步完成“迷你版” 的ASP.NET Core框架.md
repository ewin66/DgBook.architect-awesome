# 一步步完成“迷你版” 的ASP.NET Core框架

发布时间：2019-05-10 10:52  浏览次数：36


一 前言

Artech 分享了 200行代码，7个对象——让你了解ASP.NET Core框架的本质 
<https://www.cnblogs.com/artech/p/inside-asp-net-core-framework.html> 。 
用一个极简的模拟框架阐述了ASP.NET Core框架最为核心的部分。

这里一步步来完成这个迷你框架。

二 先来一段简单的代码

这段代码非常简单，启动服务器并监听本地5000端口和处理请求。
 static async Task Main(string[] args) { HttpListener httpListener = new 
HttpListener(); httpListener.Prefixes.Add("http://localhost:5000/"); 
httpListener.Start(); while (true) { var context = await 
httpListener.GetContextAsync(); await 
context.Response.OutputStream.WriteAsync(Encoding.UTF8.GetBytes("hello 
world")); context.Response.Close(); } } 
现在要分离服务器(Server) 和 请求处理(handle),那么一个简单设计架构就出来了 ：

Pipeline =Server + HttpHandler

三 处理器的抽象

处理器要从请求(Request)中获取数据，和定制响应(Response)的数据。
 可以想到我们的处理器的处理方法应该是这样的：
 Task Handle(/*HttpRequest HttpResponse*/); 
它可以处理请求和响应，由于处理可以是同步或者异步的，所以返回Task。

很容易想到要封装http请求和响应，封装成一个上下文(Context) 
供处理器使用(这样的好处，处理器需要的其他数据也可以封装在这里，统一使用)，所以要开始封装HttpContext。

封装HttpContext
 public class HttpRequest { public Uri Url { get; } public NameValueCollection 
Headers { get; } public Stream Body { get; } } public class HttpResponse { 
public NameValueCollection Headers { get; } public Stream Body { get; } public 
int StatusCode { get; set; } } public class HttpContext { public HttpRequest 
Request { get; set; } public HttpResponse Response { get; set; } } 
要支持不同的服务器，则不同的服务器都要提供HttpContext，这样有了新的难题：服务器和HttpContext之间的适配 。
 现阶段的HttpContext包含HttpRequest和HttpResponse,请求和响应的数据都是要服务器(Server)提供的。
 可以定义接口，让不同的服务器提供实现接口的实例：
 public interface IHttpRequestFeature { Uri Url { get; } NameValueCollection 
Headers { get; } Stream Body { get; } } public interface IHttpResponseFeature { 
int StatusCode { get; set; } NameValueCollection Headers { get; } Stream Body { 
get; } } 
为了方便管理服务器和HttpContext之间的适配，定义一个功能的集合，通过类型可以找到服务器提供的实例
 public interface IFeatureCollection:IDictionary<Type,object> { } public 
static partial class Extensions { public static T Get<T>(this 
IFeatureCollection features) { return features.TryGetValue(typeof(T), out var 
value) ? (T)value : default; } public static IFeatureCollection Set<T>(this 
IFeatureCollection features,T feature) { features[typeof(T)] = feature; return 
features; } } public class FeatureCollection : Dictionary<Type, object>, 
IFeatureCollection { } 
接下来修改HttpContext，完成适配
 public class HttpContext { public HttpContext(IFeatureCollection features) { 
Request = new HttpRequest(features); Response = new HttpResponse(features); } 
public HttpRequest Request { get; set; } public HttpResponse Response { get; 
set; } } public class HttpRequest { private readonly IHttpRequestFeature 
_httpRequestFeature; public HttpRequest(IFeatureCollection features) { 
_httpRequestFeature = features.Get<IHttpRequestFeature>(); } public Uri Url => 
_httpRequestFeature.Url; public NameValueCollection Headers => 
_httpRequestFeature.Headers; public Stream Body => _httpRequestFeature.Body; } 
public class HttpResponse { private readonly IHttpResponseFeature 
_httpResponseFeature; public HttpResponse(IFeatureCollection features) { 
_httpResponseFeature = features.Get<IHttpResponseFeature>(); } public int 
StatusCode { get => _httpResponseFeature.StatusCode; set => 
_httpResponseFeature.StatusCode = value; } public NameValueCollection Headers 
=> _httpResponseFeature.Headers; public Stream Body => 
_httpResponseFeature.Body; } public static partial class Extensions { public 
static Task WriteAsync(this HttpResponse response,string content) { var buffer 
= Encoding.UTF8.GetBytes(content); return response.Body.WriteAsync(buffer, 0, 
buffer.Length); } } 
定义处理器

封装好了HttpContext,终于可以回过头来看看处理器。
 处理器的处理方法现在应该是这样：
 Task Handle(HttpContext context); 
接下来就是怎么定义这个处理器了。
 起码有两种方式：
 1、定义一个接口：
 public interface IHttpHandler { Task Handle(HttpContext context); } 
2、定义一个委托类型
public delegate Task RequestDelegate(HttpContext context); 
两种方式，本质上没啥区别，委托代码方式更灵活，不用实现一个接口，还符合鸭子模型。
 处理器就选用委托类型。
 定义了处理器，接下来看看服务器

四 服务器的抽象

服务器应该有一个开始方法，传入处理器，并执行。
 服务器抽象如下：
 public interface IServer { Task StartAsync(RequestDelegate handler); } 

定义一个HttpListener的服务器来实现IServer，由于HttpListener的服务器需要提供HttpContext所需的数据，所以先定义HttpListenerFeature
 public class HttpListenerFeature : IHttpRequestFeature, IHttpResponseFeature 
{ private readonly HttpListenerContext _context; public 
HttpListenerFeature(HttpListenerContext context) => _context = context; Uri 
IHttpRequestFeature.Url => _context.Request.Url; NameValueCollection 
IHttpRequestFeature.Headers => _context.Request.Headers; NameValueCollection 
IHttpResponseFeature.Headers => _context.Response.Headers; Stream 
IHttpRequestFeature.Body => _context.Request.InputStream; Stream 
IHttpResponseFeature.Body => _context.Response.OutputStream; int 
IHttpResponseFeature.StatusCode { get => _context.Response.StatusCode; set => 
_context.Response.StatusCode = value; } } 
定义HttpListener服务器
 public class HttpListenerServer : IServer { private readonly HttpListener 
_httpListener; private readonly string[] _urls; public 
HttpListenerServer(params string[] urls) { _httpListener = new HttpListener(); 
_urls = urls.Any() ? urls : new string[] { "http://localhost:5000/" }; } public 
async Task StartAsync(RequestDelegate handler) { Array.ForEach(_urls, url => 
_httpListener.Prefixes.Add(url)); _httpListener.Start(); 
Console.WriteLine($"服务器{typeof(HttpListenerServer).Name} 
开启，开始监听：{string.Join(";", _urls)}"); while (true) { var listtenerContext = 
await _httpListener.GetContextAsync(); var feature = new 
HttpListenerFeature(listtenerContext); var features = new FeatureCollection() 
.Set<IHttpRequestFeature>(feature) .Set<IHttpResponseFeature>(feature); var 
httpContext = new HttpContext(features); await handler(httpContext); 
listtenerContext.Response.Close(); } } } 
修改Main方法运行测试
 static async Task Main(string[] args) { IServer server = new 
HttpListenerServer(); async Task FooBar(HttpContext httpContext) { await 
httpContext.Response.WriteAsync("fooBar"); } await server.StartAsync(FooBar); } 
运行结果如下：


至此，完成了服务器和处理器的抽象。
 接下来单看处理器，所有的处理逻辑都集合在一个方法中，理想的方式是有多个处理器进行处理，比如处理器A处理完，则接着B处理器进行处理……
 那么就要管理多个处理器之间的连接方式。

五 中间件

中间件的定义

假设有三个处理器A,B,C
 框架要实现：A处理器开始处理，A处理完成之后，B处理器开始处理，B处理完成之后，C处理器开始处理。

引入中间件来完成处理器的连接。

中间件的要实现的功能很简单：

 \* 传入下一个要执行的处理器； 
 \* 在中间件中的处理器里，记住下一个要执行的处理器； 
 \* 返回中间件中的处理器，供其他中间件使用。
 所以中间件应该是这样的： //伪代码 处理器 Middleware(传入下一个要执行的处理器) { return 处理器 { //处理器的逻辑 
下一个要执行的处理器在这里执行 } } 
举个例子，现在有三个中间件FooMiddleware,BarMiddleware,BazMiddleware,分别对应的处理器为A,B,C
 要保证 处理器的处理顺序为 A->B->C
 则先要执行 最后一个BazMiddleware，传入“完成处理器” 返回 处理器C
 然后把处理器C 传入 BarMiddleware ，返回处理器B,依次类推。
//伪代码 var middlewares=new []{FooMiddleware,BarMiddleware,BazMiddleware}; 
middlewares.Reverse(); var next=完成的处理器; foreach(var middleware in middlewares) 
{ next= middleware(next); } //最后的next，就是最终要传入IServer 中的处理器 
模拟运行时的伪代码：
 //传入完成处理器，返回处理器C 处理器 BazMiddleware(完成处理器) { return 处理器C { //处理器C的处理代码 完成处理器 
}; } //传入处理器C，返回处理器B 处理器 BarMiddleware(处理器C) { return 处理器B { //处理器B的处理代码 执行处理器C 
}; } //传入处理器B，返回处理器A 处理器 FooMiddleware(处理器B) { return 处理器A { //处理器A的处理代码 执行处理器B 
}; } 
这样当处理器A执行的时候，会先执行自身的代码，然后执行处理器B,处理器B执行的时候，先执行自身的代码，然后执行处理器C,依次类推。

所以，中间件的方法应该是下面这样的：
RequestDelegate DoMiddleware(RequestDelegate next); 
中间件的管理

要管理中间件，就要提供注册中间件的方法和最终构建出RequestDelegate的方法。
 定义注册中间件和构建处理器的接口： IApplicationBuilder
 public interface IApplicationBuilder { IApplicationBuilder 
Use(Func<RequestDelegate, RequestDelegate> middleware); RequestDelegate 
Build(); } 
实现：
 public class ApplicationBuilder : IApplicationBuilder { private readonly 
List<Func<RequestDelegate, RequestDelegate>> _middlewares = new 
List<Func<RequestDelegate, RequestDelegate>>(); public IApplicationBuilder 
Use(Func<RequestDelegate, RequestDelegate> middleware) { 
_middlewares.Add(middleware); return this; } public RequestDelegate Build() { 
_middlewares.Reverse(); RequestDelegate next = context => { 
context.Response.StatusCode = 404; return Task.CompletedTask; }; foreach (var 
middleware in _middlewares) { next = middleware(next); } return next; } } 
定义中间件测试

在Program 类里定义三个中间件：
 static RequestDelegate FooMiddleware(RequestDelegate next) { return async 
context => { await context.Response.WriteAsync("foo=>"); await next(context); 
}; } static RequestDelegate BarMiddleware(RequestDelegate next) { return async 
context => { await context.Response.WriteAsync("bar=>"); await next(context); 
}; } static RequestDelegate BazMiddleware(RequestDelegate next) { return async 
context => { await context.Response.WriteAsync("baz=>"); await next(context); 
}; } 
修改Main方法测试运行
 static async Task Main(string[] args) { IServer server = new 
HttpListenerServer(); var handler = new ApplicationBuilder() 
.Use(FooMiddleware) .Use(BarMiddleware) .Use(BazMiddleware) .Build(); await 
server.StartAsync(handler); } 
运行结果如下：


六 管理服务器和处理器

为了管理服务器和处理器之间的关系 抽象出web宿主
 如下：
 public interface IWebHost { Task StartAsync(); } public class WebHost : 
IWebHost { private readonly IServer _server; private readonly RequestDelegate 
_handler; public WebHost(IServer server,RequestDelegate handler) { _server = 
server; _handler = handler; } public Task StartAsync() { return 
_server.StartAsync(_handler); } } 
Main方法可以改一下测试
 static async Task Main(string[] args) { IServer server = new 
HttpListenerServer(); var handler = new ApplicationBuilder() 
.Use(FooMiddleware) .Use(BarMiddleware) .Use(BazMiddleware) .Build(); IWebHost 
webHost = new WebHost(server, handler); await webHost.StartAsync(); } 
要构建WebHost，需要知道用哪个服务器，和配置了哪些中间件，最后可以构建出WebHost
 代码如下：
 public interface IWebHostBuilder { IWebHostBuilder UseServer(IServer server); 
IWebHostBuilder Configure(Action<IApplicationBuilder> configure); IWebHost 
Build(); } public class WebHostBuilder : IWebHostBuilder { private readonly 
List<Action<IApplicationBuilder>> _configures = new 
List<Action<IApplicationBuilder>>(); private IServer _server; public IWebHost 
Build() { //所有的中间件都注册在builder上 var builder = new ApplicationBuilder(); foreach 
(var config in _configures) { config(builder); } return new WebHost(_server, 
builder.Build()); } public IWebHostBuilder 
Configure(Action<IApplicationBuilder> configure) { _configures.Add(configure); 
return this; } public IWebHostBuilder UseServer(IServer server) { _server = 
server; return this; } } 
给IWebHostBuilder加一个扩展方法,用来使用HttpListenerServer 服务器
 public static partial class Extensions { public static IWebHostBuilder 
UseHttpListener(this IWebHostBuilder builder, params string[] urls) { return 
builder.UseServer(new HttpListenerServer(urls)); } } 
修改Mian方法
 static async Task Main(string[] args) { await new WebHostBuilder() 
.UseHttpListener() .Configure(app=> app.Use(FooMiddleware) .Use(BarMiddleware) 
.Use(BazMiddleware)) .Build() .StartAsync(); } 
完成。

七 添加一个UseMiddleware 扩展 玩玩
 public static IApplicationBuilder UseMiddleware(this IApplicationBuilder 
application, Type type) { //省略实现 } public static IApplicationBuilder 
UseMiddleware<T>(this IApplicationBuilder application) where T : class { return 
application.UseMiddleware(typeof(T)); } 
添加一个中间件
 public class QuxMiddleware { private readonly RequestDelegate _next; public 
QuxMiddleware(RequestDelegate next) { _next = next; } public async Task 
InvokeAsync(HttpContext context) { await context.Response.WriteAsync("qux=>"); 
await _next(context); } } public static partial class Extensions { public 
static IApplicationBuilder UseQux(this IApplicationBuilder builder) { return 
builder.UseMiddleware<QuxMiddleware>(); } } 
使用中间件
 class Program { static async Task Main(string[] args) { await new 
WebHostBuilder() .UseHttpListener() .Configure(app=> app.Use(FooMiddleware) 
.Use(BarMiddleware) .Use(BazMiddleware) .UseQux()) .Build() .StartAsync(); } 
运行结果


最后，期待Artech 新书。


-  标签： 

- [ASP](http://www.matools.com/blog/tag/ASP)
-  , 

- [NET](http://www.matools.com/blog/tag/NET)
-  , 

- [Core](http://www.matools.com/blog/tag/Core)

- [« 上一篇：解读大内老A的《.NET Core框架本质》](http://www.matools.com/blog/190531946)
- [» 下一篇：37 岁学编程，发现第一个 Bug，创造商业编程语言 | 人物志](http://www.matools.com/blog/190516714)