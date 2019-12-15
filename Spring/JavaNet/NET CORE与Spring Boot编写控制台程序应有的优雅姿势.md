# [.NET CORE与Spring Boot编写控制台程序应有的优雅姿势](https://www.guoxiongfei.cn/cntech/20772.html)  

- 转载：[www.GuoXiongfei.cn](https://www.guoxiongfei.cn/cntech/20772.html#sourceUrl)
- /
- 时间：2019-07-03 21:00:38
- /
- 浏览：93,942次
- /
-   分类：[css](http://www.guoxiongfei.cn/developer/HTML5.html) ,  [Javascript](http://www.guoxiongfei.cn/developer/Javascript.html) ,  [Java](http://www.guoxiongfei.cn/developer/Java.html) ,  [DataBase](http://www.guoxiongfei.cn/developer/DB.html) ,  [app](http://www.guoxiongfei.cn/developer/app.html) 

> 本文分别说明.NETCORE与SpringBoot编写控制台程序应有的“正确”方法，以便.NET程序员、JAVA程序员可以相互学习与加深了解，注意本文只介绍用法，不会刻意强调哪种语言或哪种框架写的控制台程序要好。本文所说的编写控制台程序应有的“正确”方法，我把正确二字加上引号，因为没有绝对的正确，因人而异，因系统设计需求而异，我这里所谓的正确方法是指使用面向对象，依赖注入IOC，切面控制AOP等编...

  ![.NET CORE与Spring Boot编写控制台程序应有的优雅姿势](NET CORE与Spring Boot编写控制台程序应有的优雅姿势.assets/ViewTopicImg.jpg) 

本文分别说明.NET CORE与Spring Boot编写控制台程序应有的“正确”方法，以便.NET程序员、JAVA程序员可以相互学习与加深了解，注意本文只介绍用法，不会刻意强调哪种语言或哪种框架写的控制台程序要好。

本文所说的编写控制台程序应有的“正确”方法，我把正确二字加上引号，因为没有绝对的正确，因人而异，因系统设计需求而异，我这里所谓的**正确方法是指使用面向对象，依赖注入IOC，切面控制AOP等编码规范来提升程序的性能、整洁度、可读性、可维护性等，最终达到让人感觉有点高大上，有点优雅的样子**。

先来说说.NET CORE编写控制台程序，目前网络上大把的讲解ASP.NET CORE的编写规范，反而对于.NET  CORE控制台程序编写规范介绍比较少，大多停留在Hello Word程序中，而本文则来讲讲.NET CORE控制台的编写规范（应有的优雅姿势）^ v ^

如果说不讲什么IOC,DI，AOP等，不讲扩展性，规范性，全部面向过程（方法）编程，那估计没什么好讲的，因为无非就是定义一个class，然后在class中定义一堆的method（方法），如果在方法中需要使用到其它第三方组件，则直接单独引用，引用后进行简单封装util工具类的静态方法，甚至也不用封装，直接使用原生的方法，总之全部都是方法调方法。而这里所演示的编写控制台方法均是尽可能的使用.NET CORE所具有的特性，只有这样才能体现出.NET CORE框架的优势，否则普通控制台程序与.NET CORE控制台程序有什么区别。

**编写.NET CORE控制台程序优雅姿势一：（直接使用.NET CORE的 IOC、Logging、Config组件）**

代码如下：

```
//Program.csusing Microsoft.Extensions.DependencyInjection;using System;using Microsoft.Extensions.Logging;using Microsoft.Extensions.Configuration.Json;using Microsoft.Extensions.Configuration;using System.IO;namespace NetCoreConsoleApp{ class Program {  static void Main(string[] args)  {//设置config文件var config = new ConfigurationBuilder()  .SetBasePath(Directory.GetCurrentDirectory())  .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true).Build();//设置依赖注入var provider = new ServiceCollection().AddLogging(configLogging => //设置日志组件{ configLogging.SetMinimumLevel(LogLevel.Information); configLogging.AddConsole();})  .AddScoped<IConfiguration>(p => config)  .AddScoped<HostService>()  .BuildServiceProvider();var hostService = provider.GetService<HostService>();hostService.RunAsync();//统一入口服务Console.WriteLine("提示：程序已正常启动运行，按任意键停止运行并关闭程序...");Console.ReadLine();  } }}//HostService.csusing Microsoft.Extensions.Configuration;using Microsoft.Extensions.Logging;using System;using System.Diagnostics;using System.Threading;using System.Threading.Tasks;namespace NetCoreConsoleApp{ public class HostService {  private readonly IConfiguration config;  private readonly ILogger<HostService> logger;  public HostService(IConfiguration config, ILogger<HostService> logger)  {this.config = config;this.logger = logger;  }  public void RunAsync()  {Task.Run((Action)Execute);  }  /// <summary>  /// 控制台核心执行入口方法  /// </summary>  private void Execute()  {//TODO 业务逻辑代码，如下模拟Stopwatch stopwatch = Stopwatch.StartNew();for (int i = 1; i <= 100; i  ){ Console.WriteLine("test WriteLine:"i); Thread.Sleep(100);}stopwatch.Stop();logger.LogInformation("Logging - Execute Elapsed Times:{}ms", stopwatch.ElapsedMilliseconds);  } }}
```

因为要使用.NET CORE相关核心组件，故需要引用相关的NuGet包（引用包的方式有多种方式），而且默认的.NET  CORE控制台只会生成DLL并不会生成EXE启动程序，故如果仅在WIN系统下使用，还需要设置生成方式等，详细配置属性如下：（项目文件csproj）

![img](NET CORE与Spring Boot编写控制台程序应有的优雅姿势.assets/ContractedBlock.gif)![img](NET CORE与Spring Boot编写控制台程序应有的优雅姿势.assets/ExpandedBlockStart.gif)

```
<Project Sdk="Microsoft.NET.Sdk">  <PropertyGroup> <OutputType>Exe</OutputType> <TargetFramework>netcoreapp2.2</TargetFramework> <RuntimeIdentifiers>win10-x64</RuntimeIdentifiers> <SelfContained>false</SelfContained>  </PropertyGroup>  <ItemGroup> <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="2.2.0" /> <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="2.2.0" /> <PackageReference Include="Microsoft.Extensions.Logging.Console" Version="2.2.0" />  </ItemGroup></Project>
```

View Code

如上代码虽简单但代码编写顺序很关键，这里进行说明一下：

1.因为一般应用程序都会有config文件，故我们需要先通过new ConfigurationBuilder来设置config文件的方式及路径；

2.因为要使用.NET CORE默认的IOC框架，故newServiceCollection，然后将相关的依赖服务组件注册到IOC容器中；

3.config、logging均是一个程序最基本的依赖组件，故将其注册到IOC容器中，注册logging有专门的扩展方法（AddLogging），而config没有则直接使用通过的注册方法（当然也可以基于ServiceCollection写一个AddConfiguration扩展方法）

4.控制台需要一个核心的入口方法，用于处理核心业务，不要直接在Program中写方法，这样就不能使用IOC，同时也没有做到职责分明，Program仅是程序启动入口，业务处理应该有专门的入口，故上述代码中有HostService类（即：核心宿主服务类，意为存在于控制台中的服务处理类，在这个类的构造涵数中列出所需依赖的服务组件，以便实例化时IOC可以自动注入这个参数），并注册到IOC容器中，当然也可以先定义一个IHostService接口然后实现这个接口。（如果有多个HostService类实例，建议定义一个IHostService接口，接口中只需要入口方法定义即可，如：RunAsync）

5.当各组件初始化设置OK、IOC注册到位后，就应该通过IOC解析获得HostService类实例，并执行入口方法：RunAsync，该方法为异步后台执行，即调用该方法后，会在单独的后台线程处理核心业务，然后主线程继续往下面走，输出关闭提示信息，最后的Console.ReadLine();很关键，这个是等待输入流并挂起当前主线程，目的大家都知道，不要让控制台程序关闭。

通过上述的讲解及源代码展示，有没有感觉优雅呢？如果觉得这样还算优雅，那下面展示的第二种更优雅的姿势

**编写.NET CORE控制台程序优雅姿势二：（使用通用主机也称泛型主机HostBuilder）**

代码如下：Program.cs

```
using Microsoft.Extensions.DependencyInjection;using Microsoft.Extensions.Hosting;using Microsoft.Extensions.Logging;using NLog.Extensions.Logging;using Microsoft.Extensions.Configuration;using System.IO;using Polly;using System;namespace NetCoreConsoleApp{ class Program {  static void Main(string[] args)  {var host = new HostBuilder() .ConfigureHostConfiguration(configHost => {  configHost.SetBasePath(Directory.GetCurrentDirectory()); }) .ConfigureAppConfiguration(configApp => {  configApp.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); }) .ConfigureServices((context, services) => {  //添加数据访问组件示例：services.AddTransient<IDbAccesser>(provider =>  //{  // string connStr = context.Configuration.GetConnectionString("ConnDbStr");  // return new SqlDapperEasyUtil(connStr);  //});  //添加HttpClient封装类示例：s
```

源文地址：https://www.cnblogs.com/zuowj/p/11107243.html