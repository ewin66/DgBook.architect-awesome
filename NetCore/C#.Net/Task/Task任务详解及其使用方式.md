# C#  Task任务详解及其使用方式

​                                                   2018年08月06日 15:43:28           [`FTF](https://me.csdn.net/younghaiqing)           阅读数：9046                   

​                   

### 1.Task类介绍：

Task 类的表示单个操作不返回一个值，通常以**异步方式**执行。 Task 对象是一个的中心思想 **基于任务的异步模式** 首次引入**.NET Framework 4** 中。 因为由执行工作 **Task 对象通常以异步方式执行在线程池线程**上而不是以同步方式在主应用程序线程，您可以使用 Status 属性，以及 IsCanceled, ，IsCompleted, ，和 IsFaulted 属性，以确定任务的状态。 大多数情况下，lambda 表达式用于指定的任务是执行的工作。

对于返回值的操作，您使用 Task 类。

任务Task和线程Thread的区别：

1、任务是架构在线程之上的，也就是说任务最终还是要**抛给线程**去执行。

2、**任务跟线程不是一对一的关系**，比如开10个任务并不是说会开10个线程，这一点任务有点类似线程池，但是任务相比线程池有很小的开销和精确的控制。

Task和Thread一样，位于System.Threading命名空间下!

### 一、创建Task

Task 类还提供了构造函数对任务进行初始化，但的未计划的执行。 出于性能原因， **Task.Run 或 TaskFactory.StartNew（工厂创建） 方法**是用于创建和计划计算的任务的首选的机制，但对于创建和计划必须分开的方案，您可以使用的**构造函数（new一个出来）**，然后调用 **Task.Start** 方法来计划任务，以在稍后某个时间执行。

```
 //第一种创建方式，直接实例化：必须手动去Start
   var task1 = new Task(() =>
    {
       //TODO you code
    });
   task1.Start（）；

//第二种创建方式，工厂创建，直接执行
   var task2 = Task.Factory.StartNew(() =>
    {
     //TODO you code
    });123456789101112
```

### 二、Task的简略生命周期：

| 方法名          | 说明                                               |
| --------------- | -------------------------------------------------- |
| Created         | 表示默认初始化任务，但是“工厂创建的”实例直接跳过。 |
| WaitingToRun    | 这种状态表示等待任务调度器分配线程给任务执行。     |
| RanToCompletion | 任务执行完毕。                                     |

```
//查看Task中的状态
   var task1 = new Task(() =>
         {
            Console.WriteLine("Begin");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Finish");
         });
         Console.WriteLine("Before start:" + task1.Status);
         task1.Start();
         Console.WriteLine("After start:" + task1.Status);
         task1.Wait();
         Console.WriteLine("After Finish:" + task1.Status);

         Console.Read();1234567891011121314
```

### 三、Task的任务控制：Task最吸引人的地方就是他的任务控制了，你可以很好的控制task的执行顺序，让多个task有序的工作

| 方法名                  | 说明                                                         |
| ----------------------- | ------------------------------------------------------------ |
| Task.Wait               | task1.Wait();就是等待任务执行（task1）完成，task1的状态变为Completed。 |
| Task.WaitAll            | 待所有的任务都执行完成：                                     |
| Task.WaitAny            | 发同Task.WaitAll，就是等待任何一个任务完成就继续向下执行     |
| Task.ContinueWith       | 第一个Task完成后自动启动下一个Task，实现Task的延续           |
| CancellationTokenSource | 通过cancellation的tokens来取消一个Task。                     |

下面详细介绍一下上面的几个方法：

#### 1、Task.Wait

task1.Wait();就是等待任务执行（task1）完成，task1的状态变为Completed。

#### 2、Task.WaitAll

看字面意思就知道，就是等待所有的任务都执行完成：

```
 {
Task.WaitAll(task,task2,task3...N)
Console.WriteLine("All task finished!");
}1234
```

即当task,task2,task3…N全部任务都执行完成之后才会往下执行代码（打印出：“All task finished!”）

#### 3、Task.WaitAny

这个用发同Task.WaitAll，就是等待任何一个任务完成就继续向下执行，将上面的代码WaitAll替换为WaitAny

```
 {
Task.WaitAny(task,task2,task3...N)
Console.WriteLine("Any task finished!");
}1234
```

即当task,task2,task3…N任意一个任务都执行完成之后就会往下执行代码（打印出：” Any task finished!”）

#### 4、Task.ContinueWith

就是在第一个Task完成后自动启动下一个Task，实现Task的延续，下面我们来看下他的用法，编写如下代码：

```
　　static void Main(string[] args)
        {
            var task1 = new Task(() =>
            {
                Console.WriteLine("Task 1 Begin");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Task 1 Finish");
            });
            var task2 = new Task(() =>
            {
                Console.WriteLine("Task 2 Begin");
                System.Threading.Thread.Sleep(3000);
                Console.WriteLine("Task 2 Finish");
            });
            task1.Start();
            task2.Start();
            var result = task1.ContinueWith<string>(task =>
            {
                Console.WriteLine("task1 finished!");
                return "This is task result!";
            });
            Console.WriteLine(result.Result.ToString());
            Console.Read();
        }
12345678910111213141516171819202122232425
```

可以看到，task1完成之后，开始执行后面的内容，并且这里我们取得task的返回值。

#### 5、Task的取消

前面说了那么多Task的用法，下面来说下Task的取消，比如我们启动了一个task,出现异常或者用户点击取消等等，我们可以取消这个任务。如何取消一个Task呢，我们通过cancellation的tokens来取消一个Task。在很多Task的Body里面包含循环，我们可以在轮询的时候判断IsCancellationRequested属性是否为True，如果是True的话就return或者抛出异常，抛出异常后面再说，因为还没有说异常处理的东西。

下面在代码中看下如何实现任务的取消，代码如下：

```
　　　　var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
            var task = Task.Factory.StartNew(() =>
            {
                for (var i = 0; i < 1000; i++)
                {
                    System.Threading.Thread.Sleep(1000);
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Abort mission success!");
                        return;
                    }
                }
            }, token);
            token.Register(() =>
            {
                Console.WriteLine("Canceled");
            });
            Console.WriteLine("Press enter to cancel task...");
            Console.ReadKey();
            tokenSource.Cancel();123456789101112131415161718192021
```

这里开启了一个Task,并给token注册了一个方法，输出一条信息，然后执行ReadKey开始等待用户输入，用户点击回车后，执行tokenSource.Cancel方法，取消任务。

## 注：

1. 因为任务通常运行以异步方式在线程池线程上，创建并启动任务的线程将继续执行，一旦该任务已实例化。 **在某些情况下，当调用线程的主应用程序线程，该应用程序可能会终止之前任何任务实际开始执行。**  其他情况下，应用程序的逻辑可能需要调用线程继续执行，仅当一个或多个任务执行完毕。 您可以同步调用线程的执行，以及异步任务它启动通过调用  Wait 方法来等待要完成的一个或多个任务。 若要等待完成一项任务，可以调用其 Task.Wait 方法。 调用 Wait  方法将一直阻塞调用线程直到单一类实例都已完成执行。

参考文献： 
 1.<https://www.cnblogs.com/yunfeifei/p/4106318.html> 
 2.<https://msdn.microsoft.com/zh-cn/library/system.threading.tasks.task.aspx?cs-save-lang=1&cs-lang=csharp#code-snippet-1>