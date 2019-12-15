         [NET CORE与Spring Boot](https://www.cnblogs.com/Leo_wl/p/11144341.html)          



**阅读目录**

- [NET CORE与Spring Boot](https://www.cnblogs.com/Leo_wl/p/11144341.html#_label0)

[回到目录](https://www.cnblogs.com/Leo_wl/p/11144341.html#_labelTop)

# [NET CORE与Spring Boot](https://www.cnblogs.com/zuowj/p/11107243.html)

本文分别说明.NET CORE与Spring Boot 编写控制台程序应有的“正确”方法，以便.NET程序员、JAVA程序员可以相互学习与加深了解，注意本文只介绍用法，不会刻意强调哪种语言或哪种框架写的控制台程序要好。

本文所说的编写控制台程序应有的“正确”方法，我把正确二字加上引号，因为没有绝对的正确，因人而异，因系统设计需求而异，我这里所谓的**正确方法是指使用面向对象，依赖注入IOC，切面控制AOP等编码规范来提升程序的性能、整洁度、可读性、可维护性等，最终达到让人感觉有点高大上，有点优雅的样子**。

先来说说.NET CORE编写控制台程序，目前网络上大把的讲解ASP.NET CORE的编写规范，反而对于.NET  CORE控制台程序编写规范介绍比较少，大多停留在Hello Word 程序中，而本文则来讲讲.NET  CORE控制台的编写规范（应有的优雅姿势）^ v ^

 如果说不讲什么IOC,DI，AOP等，不讲扩展性，规范性，全部面向过程（方法）编程，那估计没什么好讲的，因为无非就是定义一个class，然后在class中定义一堆的method（方法），如果在方法中需要使用到其它第三方组件，则直接单独引用，引用后进行简单封装util工具类的静态方法，甚至也不用封装，直接使用原生的方法，总之全部都是方法调方法。而这里所演示的编写控制台方法均是尽可能的使用.NET CORE所具有的特性，只有这样才能体现出.NET CORE框架的优势，否则普通控制台程序与.NET CORE控制台程序有什么区别。

**编写.NET CORE控制台程序优雅姿势一：（直接使用.NET CORE的 IOC、Logging、Config组件）**

代码如下：

```csharp
//Program.cs
 
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
 
namespace NetCoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //设置config文件
            var config = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true).Build();
 
            //设置依赖注入
            var provider = new ServiceCollection()
                                    .AddLogging(configLogging => //设置日志组件
                                    {
                                        configLogging.SetMinimumLevel(LogLevel.Information);
                                        configLogging.AddConsole();
                                    })
                                   .AddScoped<IConfiguration>(p => config)
                                   .AddScoped<HostService>()
                                   .BuildServiceProvider();
 
            var hostService = provider.GetService<HostService>();
 
            hostService.RunAsync();//统一入口服务
 
            Console.WriteLine("提示：程序已正常启动运行，按任意键停止运行并关闭程序...");
            Console.ReadLine();
 
        }
    }
}
 
 
//HostService.cs<br>
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
 
namespace NetCoreConsoleApp
{
    public class HostService
    {
        private readonly IConfiguration config;
        private readonly ILogger<HostService> logger;
        public HostService(IConfiguration config, ILogger<HostService> logger)
        {
            this.config = config;
            this.logger = logger;
        }
 
        public void RunAsync()
        {
            Task.Run((Action)Execute);
        }
 
        /// <summary>
        /// 控制台核心执行入口方法
        /// </summary>
        private void Execute()
        {
            //TODO 业务逻辑代码，如下模拟
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 1; i <= 100; i++)
            {
                Console.WriteLine("test WriteLine:" + i);
                Thread.Sleep(100);
            }
 
            stopwatch.Stop();
 
            logger.LogInformation("Logging - Execute Elapsed Times:{}ms", stopwatch.ElapsedMilliseconds);
        }
 
    }
}
```

因为要使用.NET CORE相关核心组件，故需要引用相关的NuGet包（引用包的方式有多种方式），而且默认的.NET  CORE控制台只会生成DLL并不会生成EXE启动程序，故如果仅在WIN系统下使用，还需要设置生成方式等，详细配置属性如下：（项目文件csproj）

![img](NET CORE与Spring Boot.assets/ContractedBlock.gif) View Code

 如上代码虽简单但代码编写顺序很关键，这里进行说明一下：

1.因为一般应用程序都会有config文件，故我们需要先通过new ConfigurationBuilder来设置config文件的方式及路径；

2.因为要使用.NET CORE默认的IOC框架，故new ServiceCollection，然后将相关的依赖服务组件注册到IOC容器中；

3.config、logging 均是一个程序最基本的依赖组件，故将其注册到IOC容器中，注册logging有专门的扩展方法（AddLogging），而config没有则直接使用通过的注册方法（当然也可以基于ServiceCollection写一个AddConfiguration扩展方法）

4.控制台需要一个核心的入口方法，用于处理核心业务，不要直接在Program中写方法，这样就不能使用IOC，同时也没有做到职责分明，Program仅是程序启动入口，业务处理应该有专门的入口，故上述代码中有HostService类（即：核心宿主服务类， 意为存在于控制台中的服务处理类，在这个类的构造涵数中列出所需依赖的服务组件，以便实例化时IOC可以自动注入这个参数），并注册到IOC容器中，当然也可以先定义一个IHostService接口然后实现这个接口。（如果有多个HostService类实例，建议定义一个IHostService接口，接口中只需要入口方法定义即可，如：RunAsync）

5.当各组件初始化设置OK、IOC注册到位后，就应该通过IOC解析获得HostService类实例，并执行入口方法：RunAsync，该方法为异步后台执行，即调用该方法后，会在单独的后台线程处理核心业务，然后主线程继续往下面走，输出关闭提示信息，最后的Console.ReadLine();很关键，这个是等待输入流并挂起当前主线程，目的大家都知道，不要让控制台程序关闭。

 通过上述的讲解及源代码展示，有没有感觉优雅呢？如果觉得这样还算优雅，那下面展示的第二种更优雅的姿势

**编写.NET CORE控制台程序优雅姿势二：（使用通用主机也称泛型主机HostBuilder）**

代码如下：Program.cs

```
using` `Microsoft.Extensions.DependencyInjection;``using` `Microsoft.Extensions.Hosting;``using` `Microsoft.Extensions.Logging;``using` `NLog.Extensions.Logging;``using` `Microsoft.Extensions.Configuration;``using` `System.IO;``using` `Polly;``using` `System;` `namespace` `NetCoreConsoleApp``{``  ``class` `Program``  ``{``    ``static` `void` `Main(``string``[] args)``    ``{``      ``var` `host = ``new` `HostBuilder()``        ``.ConfigureHostConfiguration(configHost =>``        ``{``          ``configHost.SetBasePath(Directory.GetCurrentDirectory());``        ``})``        ``.ConfigureAppConfiguration(configApp =>``        ``{``          ``configApp.AddJsonFile(``"appsettings.json"``, optional: ``false``, reloadOnChange: ``true``);``        ``})``        ``.ConfigureServices((context, services) =>``        ``{``          ``//添加数据访问组件示例：services.AddTransient(provider =>``          ``//{``          ``//  string connStr = context.Configuration.GetConnectionString("ConnDbStr");``          ``//  return new SqlDapperEasyUtil(connStr);``          ``//});` `          ``//添加HttpClient封装类示例：services.AddHttpClient()``          ``//.AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(3, t => TimeSpan.FromMilliseconds(800)));` `          ``services.AddHostedService();``        ``})``        ``.ConfigureLogging((context, configLogging) =>``        ``{``          ``configLogging.ClearProviders();``          ``configLogging.SetMinimumLevel(LogLevel.Trace);``          ``configLogging.AddNLog(context.Configuration);``        ``})``        ``.UseConsoleLifetime()``        ``.Build();` `      ``host.Run();``    ``}``  ``}``}
```

DemoHostedService类代码：

```
using` `Microsoft.Extensions.Configuration;``using` `Microsoft.Extensions.Hosting;``using` `Microsoft.Extensions.Logging;``using` `System;``using` `System.Diagnostics;``using` `System.Threading;``using` `System.Threading.Tasks;` `namespace` `NetCoreConsoleApp``{``  ``public` `class` `DemoHostedService : IHostedService``  ``{``    ``private` `readonly` `IConfiguration config;``    ``private` `readonly` `ILogger logger;` `    ``public` `DemoHostedService(IConfiguration config, ILogger logger)``    ``{``      ``this``.config = config;``      ``this``.logger = logger;``    ``}` `    ``public` `Task StartAsync(CancellationToken cancellationToken)``    ``{``      ``Console.WriteLine(nameof(DemoHostedService) + ``"已开始执行..."``);` `      ``//TODO 业务逻辑代码，如下模拟``      ``Stopwatch stopwatch = Stopwatch.StartNew();``      ``for` `(``int` `i = 1; i <= 100; i++)``      ``{``        ``Console.WriteLine(``"test WriteLine:"` `+ i);``        ``Thread.Sleep(100);``      ``}` `      ``stopwatch.Stop();` `      ``logger.LogInformation(``"Logging - Execute Elapsed Times:{}ms"``, stopwatch.ElapsedMilliseconds);` `      ``return` `Task.FromResult(0);``    ``}` `    ``public` `Task StopAsync(CancellationToken cancellationToken)``    ``{``      ``Console.WriteLine(nameof(DemoHostedService) + ``"已被停止"``);``      ``return` `Task.FromResult(0);``    ``}` `  ``}``}
```

　

因为要使用HostBuilder类及相关的.NET CORE组件（如上代码主要使用到了：Host、Dapper、Nlog、Polly等），故仍需引用相关的NuGet包，详细配置属性如下：（项目文件csproj）

![img](https://images.cnblogs.com/OutliningIndicators/ContractedBlock.gif) View Code

 如上代码所示，写过ASP.NET CORE程序的人可能比较眼熟，这与ASP.NET CORE的写法很类似，是的，你没有看错，[**HostBuilder**](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-2.2)是通用主机，是可以广泛应用于非HTTP的环境下，而ASP.NET CORE中的**[WebHostBuilder](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/host/web-host?view=aspnetcore-2.2)** 主要用于HTTP  WEB环境，使用方式基本类似，都是先定义HostBuilder，然后利用扩展方法注册、配置各种组件（中间件），最后调用Host的Run方法，开启后台服务执行，不同的是WebHostBuilder多了属于HTTP专有的一些属性及方法及其适用的中间件。

由于这种写法比较通用，适用于已熟悉.NET CORE或ASP.NET CORE的人群，上手也较简单，故建议采取这种方式来写.NET CORE控制台程序。需要注意的是HostBuilder中最重要的是：注册**HostedService** 服务，如上代码中的DemoHostedService即是实现了IHostedService接口的宿主后台服务类，可以定义多个，然后都注册到IOC中，最后Host会按注册先后顺序执行多个HostedService服务的StartAsync方法，当停止时同样会执行多个HostedService服务的StopAsync方法

下面再来看看使用Spring&Spring Boot框架来优雅的编写控制台程序 

**编写Spring控制台程序优雅姿势一：（只引用所必需的spring jar包、logger jar包，追求极简风）**

使用IDEA +MAVEN 创建一个quickstart 控制台项目，在maven POM XML中先引用所必需的spring jar包、logger jar包等，配置如下：

![img](https://images.cnblogs.com/OutliningIndicators/ContractedBlock.gif) View Code

然后采取自定义注解类(SpringBeansConfig)的方式注册相关Bean（包含配置映射类Bean：AppProperties），代码如下：

```
//app.java` `package` `cn.zuowenjun.spring;` `import` `cn.zuowenjun.spring.cn.zuowenjun.spring.services.HostService;``import` `org.springframework.context.annotation.AnnotationConfigApplicationContext;` `import` `java.io.IOException;` `/**`` ``* Hello world!`` ``*/``public` `class` `App {``  ``public` `static` `void` `main(String[] args) {``    ``AnnotationConfigApplicationContext applicationContext = ``new` `AnnotationConfigApplicationContext(SpringBeansConfig.``class``);``    ``HostService hostService = applicationContext.getBean(HostService.``class``);` `    ``hostService.run();` `    ``applicationContext.registerShutdownHook();` `    ``try` `{``      ``System.in.read();``    ``} ``catch` `(IOException e) {``      ``System.out.println(``"等待读取输入数据报错："` `+ e.getMessage() + ``"，将直接退出程序！"``);``    ``}``  ``}``}` `//AppProperties.java` `package` `cn.zuowenjun.spring;` `import` `org.springframework.beans.factory.annotation.Value;` `public` `class` `AppProperties {` `  ``@Value``(``"${app.name}"``)``  ``private` `String appName;` `  ``@Value``(``"${app.author}"``)``  ``private` `String appAuthor;` `  ``@Value``(``"${app.test.msg}"``)``  ``private` `String testMsg;` `  ``public` `String getAppName() {``    ``return` `appName;``  ``}` `  ``public` `void` `setAppName(String appName) {``    ``this``.appName = appName;``  ``}` `  ``public` `String getAppAuthor() {``    ``return` `appAuthor;``  ``}` `  ``public` `void` `setAppAuthor(String appAuthor) {``    ``this``.appAuthor = appAuthor;``  ``}` `  ``public` `String getTestMsg() {``    ``return` `testMsg;``  ``}` `  ``public` `void` `setTestMsg(String testMsg) {``    ``this``.testMsg = testMsg;``  ``}``}` `//SpringBeansConfig.java` `package` `cn.zuowenjun.spring;` `import` `cn.zuowenjun.spring.cn.zuowenjun.spring.services.HostService;``import` `org.springframework.context.annotation.Bean;``import` `org.springframework.context.annotation.Configuration;``import` `org.springframework.context.annotation.PropertySource;``import` `org.springframework.context.annotation.Scope;``import` `org.springframework.core.annotation.Order;` `@Configuration``@PropertySource``(value = ``"classpath:app.properties"``, ignoreResourceNotFound = ``false``)``public` `class` `SpringBeansConfig {` `  ``@Bean``  ``@Order``(``1``)``  ``public` `HostService hostService() {``    ``return` `new` `HostService();``  ``}` `  ``@Bean``  ``@Order``(``0``)``  ``@Scope``(``"singleton"``)``  ``public` `AppProperties appProperties() {``    ``return` `new` `AppProperties();``  ``}` `  ``//注册其它所需Bean...``}` `//HostService.java` `package` `cn.zuowenjun.spring.cn.zuowenjun.spring.services;` `import` `cn.zuowenjun.spring.AppProperties;``import` `org.slf4j.Logger;``import` `org.slf4j.LoggerFactory;``import` `org.springframework.beans.factory.annotation.Autowired;``import` `org.springframework.util.StopWatch;` `import` `java.util.Collections;``import` `java.util.concurrent.ExecutorService;``import` `java.util.concurrent.Executors;` `public` `class` `HostService {` `  ``private` `static` `final` `Logger LOGGER = LoggerFactory.getLogger(HostService.``class``);` `  ``@Autowired``  ``private` `AppProperties appProperties;` `  ``//可以添加其它属性注入` `  ``public` `void` `run() {``//    ExecutorService pool = Executors.newSingleThreadExecutor();``//    pool.execute(() -> execute());` `    ``new` `Thread(``this``::execute).start();``  ``}` `  ``/// ``  ``/// 控制台核心执行入口方法``  ``/// ``  ``private` `void` `execute() {``    ``//TODO 业务逻辑代码，如下模拟``    ``StopWatch stopwatch = ``new` `StopWatch();``    ``stopwatch.start();``    ``for` `(``int` `i = ``1``; i <= ``100``; i++) {``      ``System.out.println(``"test WriteLine:"` `+ i);``      ``try` `{``        ``Thread.sleep(``100``);``      ``} ``catch` `(Exception e) {``      ``}``    ``}``    ``stopwatch.stop();` `    ``System.out.println(String.join(``""``, Collections.nCopies(``30``, ``"="``)));` `    ``System.out.printf(``"app name is:%s %n"``, appProperties.getAppName());``    ``System.out.printf(``"app author is:%s %n"``, appProperties.getAppAuthor());``    ``System.out.printf(``"app test msg:%s %n"``, appProperties.getTestMsg());` `    ``LOGGER.info(``"Logging - Execute Elapsed Times:{}ms"``, stopwatch.getTotalTimeMillis());``  ``}``}
```

　app.properties配置文件内容如下，注意应放在classpth目录下（即：resources目录下，没有需自行创建并设为resources目录）：

```
app.name=demo spring console``app.author=zuowenjun``app.test.msg=hello java spring console app!
```

　如上即上实现一个spring的控制台程序，当然由于是示例，故只引用了logger包，正常还需引用jdbc或ORM框架的相关jar包，　上述代码关键逻辑说明（同样要注意顺序）：

1.new **AnnotationConfigApplicationContext**类（spring IOC容器），创建一个IOC容器，类似.NET CORE中的ServiceProvider类；

2.定义 **SpringBeansConfig** bean注册配置类（注册相关依赖），这个类中依次注入相关的bean，如果bean之间有依赖顺序关系，建议添加@Order并指明序号；该类作为AnnotationConfigApplicationContext的构造函数参数传入，以便IOC自动解析并完成实际注册；

3.同样是定义一个HostService 宿主服务类，并实现run方法逻辑，一般采取后台线程异步执行，为了演示效果与.NET  CORE的HostService 类相同，示例逻辑基本相同。另外还定义了AppProperties配置映射类，便于直接读取配置，.NET  CORE同样也有类似注册bind到配置类中，然后在服务类中使用：IOptions<配置类>作为构造函数参数实现构造函数注入。只是由于篇幅有限故.NET CORE部份直接采取了注入IConfiguration，大家有兴趣可以查看网上相关资料。

4.IOC容器初始化并注册成功后，即可解析HostService 类获得实例，执行run方法，run方法会开启线程在后台处理，并返回到主线程，直至in.read()阻塞挂起主线程，防止程序自动关闭。

**编写Spring boot控制台程序优雅姿势二：（引用spring boot jar包）**

 使用IDEA+Spring Initializr来创建一个spring boot项目，创建过程中按需选择依赖的框架，我这里是示例，故除了默认spring-boot-starter依赖外，其余什么依赖都不添加，创建后Maven POM XML如下：

![img](https://images.cnblogs.com/OutliningIndicators/ContractedBlock.gif) View Code

然后创建相关的Bean类：HostService（宿主服务类，这个与前文定义类均相同）、AppProperties（配置映射类，这个是映射默认的application.properties配置文件，注意这里的映射方式与前文所描述稍有不周，采用：@ConfigurationProperties+属性映射，无需加@Value注解，映射属性时如果有-则应写成驼峰式，如果有.则应定义内部静态类，呈现层级属性完成映射，具体的用法可以参见我之前的文章）：

```
//HostService.java` `package` `cn.zuowenjun.spring.services;` `import` `cn.zuowenjun.spring.AppProperties;``import` `org.slf4j.Logger;``import` `org.slf4j.LoggerFactory;``import` `org.springframework.beans.factory.annotation.Autowired;``import` `org.springframework.stereotype.Component;``import` `org.springframework.util.StopWatch;` `import` `java.util.Collections;` `@Component``public` `class` `HostService {` `  ``private` `static` `final` `Logger LOGGER = LoggerFactory.getLogger(HostService.``class``);` `  ``@Autowired``  ``private` `AppProperties appProperties;` `  ``//可以添加其它属性注入` `  ``public` `void` `run() {``//    ExecutorService pool = Executors.newSingleThreadExecutor();``//    pool.execute(() -> execute());` `    ``new` `Thread(``this``::execute).start();``  ``}` `  ``/// ``  ``/// 控制台核心执行入口方法``  ``/// ``  ``private` `void` `execute() {``    ``//TODO 业务逻辑代码，如下模拟``    ``StopWatch stopwatch = ``new` `StopWatch();``    ``stopwatch.start();``    ``for` `(``int` `i = ``1``; i <= ``100``; i++) {``      ``System.out.println(``"test WriteLine:"` `+ i);``      ``try` `{``        ``Thread.sleep(``100``);``      ``} ``catch` `(Exception e) {``      ``}``    ``}``    ``stopwatch.stop();` `    ``System.out.println(String.join(``""``, Collections.nCopies(``30``, ``"="``)));` `    ``System.out.printf(``"app name is:%s %n"``, appProperties.getName());``    ``System.out.printf(``"app author is:%s %n"``, appProperties.getAuthor());``    ``System.out.printf(``"app test msg:%s %n"``, appProperties.getTestMsg());` `    ``LOGGER.info(``"Logging - Execute Elapsed Times:{}ms"``, stopwatch.getTotalTimeMillis());``  ``}``}` `//AppProperties.java` `package` `cn.zuowenjun.spring;` `import` `org.springframework.boot.context.properties.ConfigurationProperties;``import` `org.springframework.stereotype.Component;` `@Component``@ConfigurationProperties``(prefix = ``"app"``)``public` `class` `AppProperties {` `  ``private` `String name;` `  ``private` `String author;` `  ``private` `String testMsg;` `  ``public` `String getName() {``    ``return` `name;``  ``}` `  ``public` `void` `setName(String name) {``    ``this``.name = name;``  ``}` `  ``public` `String getAuthor() {``    ``return` `author;``  ``}` `  ``public` `void` `setAuthor(String author) {``    ``this``.author = author;``  ``}` `  ``public` `String getTestMsg() {``    ``return` `testMsg;``  ``}` `  ``public` `void` `setTestMsg(String testMsg) {``    ``this``.testMsg = testMsg;``  ``}``}
```

application.properties配置文件：(注意app.test.msg此处改为了app.test-msg，因为这样就可以直接映射到类的属性中，否则得定义内部类有点麻烦)

```
app.name=demo spring console``app.author=zuowenjun``app.test-msg=hello java spring console app!
```

　最后改造spring boot  application类，让SpringbootConsoleApplication类实现ApplicationRunner接口，并在run方法中编写通过属性依赖注入获得HostService类的实例，最后执行HostService的run方法即可，代码如下：

```
package` `cn.zuowenjun.spring;` `import` `cn.zuowenjun.spring.services.HostService;``import` `org.springframework.beans.factory.annotation.Autowired;``import` `org.springframework.boot.ApplicationArguments;``import` `org.springframework.boot.ApplicationRunner;``import` `org.springframework.boot.SpringApplication;``import` `org.springframework.boot.autoconfigure.SpringBootApplication;` `@SpringBootApplication``public` `class` `SpringbootConsoleApplication ``implements` `ApplicationRunner {` `  ``@Autowired``  ``private` `HostService hostService;` `  ``public` `static` `void` `main(String[] args) {``    ``SpringApplication.run(SpringbootConsoleApplication.``class``, args);``  ``}` `  ``@Override``  ``public` `void` `run(ApplicationArguments args) ``throws` `Exception {``    ``hostService.run();``  ``}``}
```

　如上步骤即完成了优雅编写spring boot控制台程序，关键点是ApplicationRunner，这个是给spring  boot执行的入口，另一种思路，我们其实还可以把HostService类改造一下，让其实现ApplicationRunner接口，那么run方法即为spring boot的启动入口。

总结一下：.

NET CORE控制台程序优雅姿势一与Spring控制台优雅姿势一核心思想是一样的，都是手动创建各个依赖组件及IOC容器的实例，都是通过IOC容器显式的解析获得HostService类的实例，最后运行HostService#run方法。

NET CORE控制台程序优雅姿势二与Spring控制台优雅姿势二核心思想也是一样的，都是利用IOC容器来直接管理注册的各个依赖组件，并由.NET CORE、Spring boot框架自行调度HostService#run方法。

我个人更倾向优雅姿势二的方法来编写.NET CORE或Spring Boot的控制台程序，因为写得更少，做得更多。

​    分类:             [[27\]NET Core](https://www.cnblogs.com/Leo_wl/category/225703.html)





 [« ](https://www.cnblogs.com/Leo_wl/p/11138872.html) 上一篇：    [多线程调用有参数的方法---c# Thread 与 Task](https://www.cnblogs.com/Leo_wl/p/11138872.html)    
    [» ](https://www.cnblogs.com/Leo_wl/p/11144372.html) 下一篇：    [使用Rabbit MQ消息队列](https://www.cnblogs.com/Leo_wl/p/11144372.html)