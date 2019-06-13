 		[Task的异步模式](https://www.cnblogs.com/Leo_wl/p/4881190.html) 	



> **阅读目录**
>
> # [基于Task的异步模式--全面介绍](https://www.cnblogs.com/farb/p/4851349.html)
>
> - 一、返回该系列目录《基于Task的异步模式--全面介绍》
>   - [1.编译器生成](https://www.cnblogs.com/Leo_wl/p/4881190.html#_label0_0)
>   - [2.手动生成](https://www.cnblogs.com/Leo_wl/p/4881190.html#_label0_1)
>   - [3.混合生成](https://www.cnblogs.com/Leo_wl/p/4881190.html#_label0_2)
>   - [4.计算限制](https://www.cnblogs.com/Leo_wl/p/4881190.html#_label0_3)
>   - [5.I/O限制](https://www.cnblogs.com/Leo_wl/p/4881190.html#_label0_4)
>   - [6.混合计算限制和I/O限制的任务](https://www.cnblogs.com/Leo_wl/p/4881190.html#_label0_5)
> - [二、返回该系列目录《基于Task的异步模式--全面介绍》](https://www.cnblogs.com/Leo_wl/p/4881190.html#_label1)



**阅读目录**

- [Task的异步模式](https://www.cnblogs.com/Leo_wl/p/4881190.html#_label0)

[回到目录](https://www.cnblogs.com/Leo_wl/p/4881190.html#_labelTop)

# [Task的异步模式](http://www.cnblogs.com/farb/p/4870421.html)

[返回顶部](https://www.cnblogs.com/Leo_wl/p/4881190.html#_labelTop)

### [返回该系列目录《基于Task的异步模式--全面介绍》](http://www.cnblogs.com/farb/p/4851349.html)

## 生成方法



#### 编译器生成

在.NET Framework  4.5中，C#编译器实现了TAP。任何标有async关键字的方法都是异步方法，编译器会使用TAP执行必要的转换从而异步地实现方法。这样的方法应该返回Task或者Task<TResult>类型。在后者的案例中，方法体应该返回一个TResult,且编译器将确保通过返回的Task<TResult>是可利用的。相似地，方法体内未经处理的异常会被封送到输出的task,造成返回的Task以Faulted的状态结束。一个例外是如果OperationCanceledException(或派生类型)未经处理，那么返回的Task会以Canceled状态结束。



#### 手动生成

开发者可以手动地实现TAP，就像编译器那样或者更好地控制方法的实现。编译器依赖来自System.Threading.Tasks命名空间暴露的公开表面区域（和建立在System.Threading.Tasks之上的System.Runtime.CompilerServices中支持的类型），还有对开发者直接可用的功能。当手动实现TAP方法时，开发者必须保证当异步操作完成时，完成返回的Task。



#### 混合生成

在编译器生成的实现中混合核心逻辑的实现，对于手动实现TAP通常是很有用的。比如这种情况，为了避免方法直接调用者产生而不是通过Task暴露的异常，如：

[![复制代码](../assets/copycode-1560388543293.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public Task<int> MethodAsync(string input)
{
    if (input == null) throw new ArgumentNullException("input");
    return MethodAsyncInternal(input);
}

private async Task<int> MethodAsyncInternal(string input)
{
    … // code that uses await
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

参数应该在编译器生成的异步方法之外改变，这种委托有用的另一种场合是，当一个“快速通道”优化可以通过返回一个缓存的task来实现的时候。

## 工作负荷

计算受限和I/O受限的异步操作可以通过TAP方法实现。然而，当TAP的实现从一个库公开暴露时，应该只提供给包含I/O操作的工作负荷（它们也可以包含计算，但不应该只包含计算）。如果一个方法纯粹受计算限制，它应该只通过一个异步实现暴露，消费者然后就可以为了把该任务卸载给其他的线程的目的来选择是否把那个同步方法的调用包装成一个Task，并且/或者来实现并行。



#### 计算限制

Task类最适合表示计算密集型操作。默认地，为了提供有效的执行操作，它利用了.Net线程池中特殊的支持，同时也对异步计算何时，何地，如何执行提供了大量的控制。

生成计算受限的tasks有几种方法。

1. 在.Net  4中，启动一个新的计算受限的task的主要方法是TaskFactory.StartNew(),该方法接受一个异步执行的委托（一般来说是一个Action或者一个Func<TResult>）。如果提供了一个Action，返回的Task就代表那个委托的异步执行操作。如果提供了一个Func<TResult>，就会返回一个Task<TResult>。存在StartNew（）的重载，该重载接受CancellationToken，TaskCreationOptions,和TaskScheduler,这些都对task的调度和执行提供了细粒度的控制。作用在当前调度者的工厂实例可以作为Task类的静态属性，例如Task.Factory.StartNew()。
2. 在.Net  4.5中，Task类型暴露了一个静态的Run方法作为一个StartNew方法的捷径，可以很轻松地使用它来启动一个作用在线程池上的计算受限的task。从.Net  4.5开始，对于启动一个计算受限的task，这是一个更受人喜欢的机制。当行为要求更多的细粒度控制时，才直接使用StartNew。
3. Task类型公开了构造函数和Start方法。如果必须要有分离自调度的构造函数，这些就是可以使用的（正如先前提到的，公开的APIs必须只返回已经启动的tasks）。
4. Task类型公开了多个ContinueWith的重载。当另外一个task完成的时候，该方法会创建新的将被调度的task。该重载接受CancellationToken，TaskCreationOptions,和TaskScheduler,这些都对task的调度和执行提供了细粒度的控制。
5. TaskFactory类提供了ContinueWhenAll  和ContinueWhenAny方法。当提供的一系列的tasks中的所有或任何一个完成时，这些方法会创建一个即将被调度的新的task。有了ContinueWith,就有了对于调度的控制和任务的执行的支持。

思考下面的渲染图片的异步方法。task体可以获得cancellation token为的是，当渲染发生的时候，如果一个撤销请求到达后，代码可能过早退出。而且，如果一个撤销请求在渲染开始之前发生，我们也可以阻止任何的渲染。

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public Task<Bitmap> RenderAsync(
    ImageData data, CancellationToken cancellationToken)
{
    return Task.Run(() =>
    {
        var bmp = new Bitmap(data.Width, data.Height);
        for(int y=0; y<data.Height; y++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            for(int x=0; x<data.Width; x++)
            {
                … // render pixel [x,y] into bmp
            }
        }
        return bmp;
    }, cancellationToken);
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

如果下面的条件至少一个是正确的，计算受限的tasks会以一个Canceled状态的结束：

1. 在Task过度到TaskStatus.Running状态之前，CancellationToken为一个发出撤销请求的创建方法的参数提供（如StartNew，Run）。
2. 有这样的一个Task，它内部有未处理的OperationCanceledException。该OperationCanceledException   包含和CancellationToken属性同名的CancellationToken传递到该Task，且该CancellationToken已经发出了撤销请求。

如果该Task体中有另外一个未经处理的异常，那么该Task就会以Faulted的状态结束，同时在该task上等待的任何尝试或者访问它的结果都将导致抛出异常。



#### I/O限制

使用TaskCompletionSource<TResult>类型创建的Tasks不应该直接被全部执行的线程返回。TaskCompletionSource<TResult>暴露了一个返回相关的Task<TResult>实例的Task属性。该task的生命周期通过TaskCompletionSource<TResult>实例暴露的方法控制，换句话说，这些实例包括SetResult,  SetException, SetCanceled, 和它们的TrySet* 变量。

思考这样的需求，创建一个在特定的时间之后会完成的task。比如，当开发者在UI场景中想要延迟一个活动一段时间时，这可能使有用的。.NET中的System.Threading.Timer类已经提供了这种能力，在一段特定时间后异步地调用一个委托，并且我们可以使用TaskCompletionSource<TResult>把一个Task放在timer上，例如：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public static Task<DateTimeOffset> Delay(int millisecondsTimeout)
{
    var tcs = new TaskCompletionSource<DateTimeOffset>();
    new Timer(self =>
    {
        ((IDisposable)self).Dispose();
        tcs.TrySetResult(DateTimeOffset.UtcNow);
    }).Change(millisecondsTimeout, -1);
    return tcs.Task;
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

在.Net 4.5中，Task.Delay()就是为了这个目的而生的。比如，这样的一个方法可以使用到另一个异步方法的内部，以实现一个异步的轮训循环：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public static async Task Poll(
    Uri url, 
    CancellationToken cancellationToken, 
    IProgress<bool> progress)
{
    while(true)
    {
        await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
        bool success = false;
        try
        {
            await DownloadStringAsync(url);
            success = true;
        }
        catch { /* ignore errors */ }
        progress.Report(success);
    }
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

没有TaskCompletionSource<TResult>的非泛型副本。然而，Task<TResult>派生自Task，因而，泛型的TaskCompletionSource<TResult>可以用于那些   I/O受限的方法，它们都利用一个假的TResult源（Boolean是默认选择，如果开发者关心Task向下转型的Task<TResult>的消费者，那么可以使用一个私有的TResult类型）仅仅返回一个Task。比如，开发的之前的Delay方法是为了顺着产生的Task<DateTimeOffset>返回当前的时间。如果这样的  一个结果值是不必要的，那么该方法可以通过下面的代码取而代之（注意返回类型的改变和TrySetresult参数的改变）：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

![复制代码](https://common.cnblogs.com/images/copycode.gif)

```
public static Task Delay(int millisecondsTimeout)
{
    var tcs = new TaskCompletionSource<bool>();
    new Timer(self =>
    {
        ((IDisposable)self).Dispose();
        tcs.TrySetResult(true);
    }).Change(millisecondsTimeout, -1);
    return tcs.Task;
}
```

![复制代码](https://common.cnblogs.com/images/copycode.gif)

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)



#### 混合计算限制和I/O限制的任务

异步方法不是仅仅受限于计算受限或者I/O受限的操作，而是可以代表这两者的混合。实际上，通常情况是不同性质的多个异步操作被组合在一起生成更大的混合操作。比如，思考之前的RenderAsync方法，该方法基于一些输入的ImageData执行一个计算密集的操作来渲染一张图片。该ImageData可能来自于一个我们异步访问的Web服务：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
public async Task<Bitmap> DownloadDataAndRenderImageAsync(
    CancellationToken cancellationToken)
{
    var imageData = await DownloadImageDataAsync(cancellationToken);
    return await RenderAsync(imageData, cancellationToken);
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

这个例子也展示了一个单独的CancellationToken是如何通过多个异步操作被线程化的。

[返回顶部](https://www.cnblogs.com/Leo_wl/p/4881190.html#_labelTop)

### [返回该系列目录《基于Task的异步模式--全面介绍》](http://www.cnblogs.com/farb/p/4851349.html)

------

作者：tkb至简 出处：<http://www.cnblogs.com/farb/>

QQ:782762625

欢迎各位多多交流！

本文版权归作者和博客园共有，欢迎转载。未经作者同意下，必须在文章页面明显标出原文链接及作者，否则保留追究法律责任的权利。
如果您认为这篇文章还不错或者有所收获，可以点击右下角的**【推荐】**按钮，因为你的支持是我继续写作，分享的最大动力！