

# 10个小技巧助您写出高性能的ASP.NET Core代码



今天这篇文章我们来聊一聊如何提升并优化ASP.NET Core应用程序的性能，本文的大部分内容来自翻译，当然中间穿插着自己的理解，希望对大家有所帮助！话不多说开始今天的主题吧！
 我们都知道性能是公共网站取得成功的关键因素之一。如果一个网站的响应时间超过3秒，那么用户通常不会再此光顾（此网站）。谷歌，Bing，百度以及其他搜索引擎也更倾向于推荐优化后的，移动友好的以及响应速度更快的网站。

> 作者：依乐祝
>
> 原文地址：[https://www.cnblogs.com/yilezhu/p/10507984.html](https://links.jianshu.com/go?to=https%3A%2F%2Fwww.cnblogs.com%2Fyilezhu%2Fp%2F10507984.html)
>
> 大部分内容翻译自：[https://www.c-sharpcorner.com/article/10-tips-to-improve-performance-of-asp-net-core-application/](https://links.jianshu.com/go?to=https%3A%2F%2Fwww.c-sharpcorner.com%2Farticle%2F10-tips-to-improve-performance-of-asp-net-core-application%2F)

这里我们举一个例子：我们有多个搜索引擎，如Google、Bing、百度、搜狗等等；然而，我们更喜欢Google或Bing，因为这些搜索引擎速度非常快，可以在3-4秒内获得结果。如果这些搜索引擎的响应速度超过10秒，你还会使用它们吗？我认为大伙应该不会用了吧。如今的用户最不能容忍的想必就是等待了吧。

今天，我们将学习一些有助于提高ASP.NET Core网站性能的一些小技巧。希望大家能够有所收获。

我们都知道ASP.NET Core是微软提供的一个免费的、开源的、跨平台的Web开发框架。它不是ASP.NET的升级版本，但它是一个从头开始完全重写的框架，它附带了ASP.NET MVC和ASP.NET Web API的单一编程模型。

在这里，我不打算讨论ASP.NET Core及其特性。如果您是ASP.NET Core的新手，您可以阅读我的ASP.NET Core实战教程《[.NET Core实战项目之CMS 第一章 入门篇-开篇及总体规划](https://links.jianshu.com/go?to=https%3A%2F%2Fwww.cnblogs.com%2Fyilezhu%2Fp%2F9977862.html)》

下面我们就开始今天的主题，如何提升ASP.NET Core应用程序的性能的技巧开始吧。

## 尽量使用ASP.NET Core的最新版本

ASP.NET Core的第一个版本是在2016年与VisualStudio 2015一起发布的，现在我们有了ASP.NET Core3.0，每一个新版本都越来越好。最新的ASP.NET Core 3.0的主要更新如下：

-  **Razor组件的改进。**现在2个项目合并成单个项目模板，Razor组件支持端点路由和预渲染，Razor组件可以托管在Razor类库中。还改进了事件处理和表单和验证支持。
-  **运行时编译。**它在ASP.NET Core 3.0模板中被禁用，但现在可以通过向项目添加特殊的NuGet包来打开它。
-  **Worker Service 模板。**需要编写Windows服务还是Linux守护进程？现在我们有了**Worker Service** 模板。
-  **gRPC模板。**与谷歌一起构建的gRPC是一种流行的远程过程调用（RPC）框架。此版本的ASP.NET Core在ASP.NET Core上引入了第一等的gRPC支持。
-  **Angular模板使用Angular 7.** Angular SPA模板现在使用Angular 7，在第一次稳定释放之前，它将被Angular 8替换。
-  **SPA-s的身份验证。**Microsoft通过此预览为单页应用程序添加了现成的身份验证支持。
-  **SignalR与端点路由集成。**小变化 - 现在使用端点路由定义SingalR路由。
-  **SignalR Java客户端支持长轮询。**即使在不支持或不允许WebSocket的环境中，SignalR Java客户端现在也可以使用。

> 友情提示：在构建新的ASP.NET Core项目时，不要忘记选择最新版本。VisualStudio 2019预览版现在已经支持ASP.NET Core 3.0了。

## 尽量避免任何层的同步调用

在开发ASP.NET Core应用程序时，尽量避免创建阻塞的调用。阻塞调用是指当前请求未完成之前会一直阻止下一个执行的调用。阻塞调用或同步调用可以是任何东西，可以是从API中获取数据，也可以是执行一些内部操作。您应该始终以异步方式执行调用。

## 尽量使用异步编程(ASYNC-AWAIT)

异步编程模型是在C#5.0中引入的，并变得非常流行。ASP.NET Core使用相同的异步编程范例来使应用程序更可靠、更快和更稳定。

您应该在代码中使用端到端异步编程。

让我们举一个例子；我们有一个ASP.NET CoreMVC应用程序，中间有一些数据库的操作。正如我们所知道的，它可能有很多分层结构，这都取决于用户的项目架构，但是让我们举一个简单的例子，其中我们有Controller》Repository 层等等。让我们看看如何在控制器层编写示例代码。

```
[HttpGet]
[Route("GetPosts")]  
public async Task GetPosts()
{  
    try  
    {  
var posts = await postRepository.GetPosts();
        if (posts == null)  
        {  
            return NotFound();
        }  

        return Ok(posts);
    }  
    catch (Exception)
    {  
        return BadRequest();

    }  
}  
```

接下来的代码然是了我们如何在repository  层实现异步编程。

```
public async Task<List<PostViewModel>> GetPosts()
{  
    if (db != null)  
       {  
         return await (from p in db.Post
from c in db.Category
where p.CategoryId == c.Id
select new PostViewModel
                       {  
PostId = p.PostId,
Title = p.Title,
Description = p.Description,
CategoryId = p.CategoryId,
CategoryName = c.Name,
CreatedDate = p.CreatedDate
}).ToListAsync();
      }  

      return null;  
}  
```

## 使用异步编程避免TASK.WAIT或TAST.RESULT

在使用异步编程时，我建议您避免使用Task.Wait和Task.Result并尝试使用WAIT，原因如下：

1. 它们阻塞线程直到任务完成，并等待任务完成。等待同步阻塞线程，直到任务完成。
2. Wait 和 Task.Result 在AggregateException中包含所有类型的异常，并在在执行异常处理时增加复杂性。如果您使用的是等待await 而不是 Task.Wait和Task.Result的话，那么您就不必担心异常的处理了。
3. 有时，它们都会阻塞当前线程并创建死锁。
4. 只有在并行任务执行正在进行时才能使用Wait 和Task.Result 。我们建议您不要在异步编程中使用它。

下面让我们分别演示下正确使用以及不建议使用Task.Wait 的例子，来加深理解吧！

```
// 正确的例子 
Task task = DoWork();
await task;

// 不建议使用的例子 
Task task = DoWork();
task.Wait();
```

下面让我们分别演示下正确使用以及不规范使用Task.Result 的例子，来加深理解吧！

```
// Good Performance on UI  
Task<string> task = GetEmployeeName();
txtEmployeeName.Text = await task;

// Bad Performance on UI  
Task<string> task = GetEmployeeName();
txtEmployeeName.Text = task.Result;
```

了解更多关于[异步编程的最佳实践](https://links.jianshu.com/go?to=https%3A%2F%2Fmsdn.microsoft.com%2Fen-us%2Fmagazine%2Fjj991977.aspx).

## 尽量异步执行I/O操作

在执行I/O操作时，您应该异步执行它们，这样就不会影响其他进程。I/O操作意味着对文件执行一些操作，比如上传或检索文件。它可以是任何操作如：图像上传，文件上传或其他任何操作。如果您试图以同步的方式完成它，那么它会阻塞主线程并停止其他后台执行，直到I/O完成为止。因此，从提升性能上来说，您在对I/O进行操作时应该始终进行异步执行。

我们有很多异步方法可用于I/O操作，如ReadAsync、WriteAsync、FlushAysnc等。下面是一个简单的例子，说明我们如何异步创建一个文件的副本。

```
public async void CreateCopyOfFile()
{  
    string dir = @"c:\Mukesh\files\";  

    using (StreamReader objStreamReader= File.OpenText(dir + "test.txt"))  
    {  
        using (StreamWriter objStreamWriter= File.CreateText(dir+ "copy_test.txt"))  
        {  
await CopyFileToTarget(objStreamReader, objStreamWriter);
        }  
    }  
}  

public async Task CopyFileToTarget(StreamReader objStreamReader, StreamWriter objStreamWriter)
{   
    int num;
    char[] buffer = new char[0x1000];

    while ((num= await objStreamReader.ReadAsync(buffer, 0, buffer.Length)) != 0)
    {  
await objStreamWriter.WriteAsync(buffer, 0, num);
    }   
}  
```

## 尽量使用缓存

如果我们能在每次执行的时候减少减少对服务器的请求次数，那么我们就可以提高应用程序的性能。这并不意味着您执行的时候不会请求服务器，而是意味着您不会每次执行都请求服务器。第一次，您将请求服务器并获得响应，此响应将在某个地方存储一段时间(将有一些到期)，下一次当您对相同的响应进行调用时，您将首先检查您是否已经在第一个请求中获得了数据并存储在某个地方，如果是的话，您将检查是否已经获得了数据。使用存储的数据，而不是调用服务器。

将数据保存在某个位置并让下次请求从这个地方获取数据而不是从服务器获取是一种很好的做法。在这里，我们可以使用缓存。缓存内容有助于我们再次减少服务器调用，并帮助我们提高应用程序的性能。我们可以在客户端缓存、服务器端缓存或客户机/服务器端缓存等位置的任意点执行缓存。

我们可以在ASP.NET Core中使用不同类型的缓存，比如我们可以在内存中进行缓存，也可以使用响应缓存，也可以使用分布式缓存。更多关于[ASP.NET Core 中的缓存](https://links.jianshu.com/go?to=https%3A%2F%2Fdocs.microsoft.com%2Fen-us%2Faspnet%2Fcore%2Fperformance%2Fcaching%2Fresponse%3Fview%3Daspnetcore-2.2)

```
public async Task GetCacheData()
{  
var cacheEntry = await
_cache.GetOrCreateAsync(CacheKeys.Entry, entry =>
    {  
entry.SlidingExpiration = TimeSpan.FromSeconds(120);
        return Task.FromResult(DateTime.Now);
    });  

    return View("Cache", cacheEntry);
}  
```

## 优化数据访问

我们还可以通过优化数据访问逻辑、数据库表和查询来提高应用程序的性能。众所周知，大多数应用程序都使用某种数据库，每次从数据库获取数据时，都会影响应用程序的性能。如果数据库加载缓慢，则整个应用程序将缓慢运行。这里我们有一些建议：

1. 减少HTTP请求的次数，意味着您应该始终尝试减少网络往返次数。
2. 试着一次得到所有的数据。这意味着不对服务器进行多次调用，只需进行一两次调用就可以带来所有所需的数据。
3. 经常对不经常变化的数据设置缓存。
4. 不要试图提前获取不需要的数据，这会增加响应的负载，并导致应用程序的加载速度变慢。

## 优化自定义代码

除了业务逻辑和数据访问代码之外，应用程序中可能还有一些自定义代码。确保此代码也是优化的。这里有一些建议：

1. 应该优化对每个请求执行的自定义日志记录、身份验证或某些自定义处理程序的代码。
2. 不要在业务逻辑层或中间件中执行长时间运行的代码，它会阻塞到服务器的请求，从而导致应用程序需要很长时间才能获得数据。您应该在客户端或数据库端为此进行优化代码。
3. 始终检查长期运行的任务是否应该异步执行，而不影响其他进程。
4. 您可以使用实时客户端-服务器通信框架，如：SignalR，来进行异步工作。

## Entity Framework Core 的查询优化

众所周知，EF Core是一个面向.NET开发人员的ORM，它帮助我们处理数据库对象，而不像往常那样编写大量代码。它帮助我们使用模型的数据库。数据访问逻辑代码在性能上起着至关重要的作用。如果您的代码没有优化，那么应用程序的性能通常就不会很好。

但是，如果您在EFCore中以优化的方式编写数据访问逻辑，那么肯定会提高应用程序的性能。在这里，我们有一些技巧来提高性能。

1. 在获取只是用来只读显示的数据时不使用跟踪。它提高了性能。
2. 尝试在数据库端过滤数据，不要使用查询获取整个数据，然后在您的末尾进行筛选。您可以使用EF Core中的一些可用功能，可以帮助您在数据库端筛选数据的操作，如：WHERE，Select等。
3. 使用Take和Skip来获取我们所必须要显示的数量的记录。这里可以举一个分页的例子，在这个例子中，您可以在单击页码的同时使用Take和Skip来获取当前页面的数据。

让我们以一个例子为例，了解如何使用Select和AsNoTracking优化EF Core的查询。

```
public async Task<PaginatedList> GetPagedPendingPosts(int pageIndex, int pageSize, List allowedCategories)
{  
var allowedCatIds = allowedCategories.Select(x => x.Id);
var query = _context.Post
.Include(x => x.Topic.Category)
.Include(x => x.User)
.Where(x => x.Pending == true && allowedCatIds.Contains(x.Topic.Category.Id))
.OrderBy(x => x.DateCreated);

    return await PaginatedList.CreateAsync(query.AsNoTracking(), pageIndex, pageSize);
}  
```

## 其他一些提示

这里我们还有一些其他性能改进的东西可以在ASP.NET Core应用程序中进行实现。

1. 编写优化和测试代码。您还可以使用来自专业高级开发者的代码示例，包括产品文档。产品团队编写的代码(如C#团队)通常是优化的、现代化的，并且遵循最佳实践。
2. 使用经过优化和良好测试的API和库。例如，在某些情况下，ADO.NET可能是比 Entity Framework 或其他ORM库更好的选择。
3. 如果您需要下载一个很大的文件的话，您可能需要考虑使用压缩算法。这里有几个内置的压缩库，如Gzip和Brotli。

```
public void ConfigureServices(IServiceCollection services)
{  
services.AddResponseCompression();

services.Configure(options =>
    {  
options.Level = CompressionLevel.Fastest;
    });  
}  
```

## 附加的建议(面向Client)

我想分享一些面向客户端的提升性能的技巧。如果您正在使用ASP.NET Core MVC创建网站，下面是一些提示：

- *捆绑和小型化*

  使用捆绑和小型化可以减少服务器请求次数。尝试一次加载所有客户端资源，如样式、js/css。您可以首先使用小型化缩小文件，然后将这些文件打包到一个文件中，这将加快加载速度并减少HTTP请求的数量。

- *最后加载 JavaScript*

  您应该始终尝试在页面尾部加载JavaScript文件，除非在此之前需要使用它们。如果您这样做，您的网站将显示的更快，并且用户也不需要等待并看到这些内容。

- *压缩图像*

  确保使用压缩技术缩小图像的大小。

- *使用 CDN*

  如果您只有几个样式和JS文件，那么可以从您的服务器加载。对于较大的静态文件，请尝试使用CDN。CDN通常可以在多个位置上使用，并且文件是从本地服务器提供的。从本地服务器加载文件可以提高网站性能。

作者：菜鸟飞不动

链接：https://www.jianshu.com/p/75a417b09a4a

来源：简书

简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。