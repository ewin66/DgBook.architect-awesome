由于Framework 4.0和Framework 4.5对Task类稍微有些不同，此处声明以下代码都是基于Framework 4.5

------

Task类和Task<TResult>类，后者是前者的###   泛型版本###   。TResult类型为Task所调用方法的返回值。

主要区别在于Task构造函数接受的参数是###   Action委托###   ，而Task<TResult>接受的是###   Func<TResult>委托###   。

```
Task(Action) Task<TResult>(Func<TResult>)  
```

 

###   启动一个任务###    

```csharp
static void Main(string[] args)         {             
    Task Task1 = new Task(() => Console.WriteLine("Task1"));             Task1.Start();             Console.ReadLine();         } 
```

通过实例化一个Task对象，然后Start，这种方式中规中矩，但是实践中，通常采用更方便快捷的方式

```csharp
Task.Run(() => Console.WriteLine("Foo"));
```

这种方式直接运行了Task，不像上面的方法还需要调用Start();

Task.Run方法是Task类中的静态方法，接受的参数是委托。返回值是为该Task对象。

Task.Run(Action)

Task.Run<TResult>(Func<Task<TResult>>)

Task构造方法还有一个重载函数如下：

Task 构造函数 (Action, TaskCreationOptions)，对应的Task泛型版本也有类似构造函数。TaskCreationOptions参数指示Task创建和执行的可选行为。常用的参数LongRunning。

默认情况下，Task任务是由###   *线程池线程异步执行###   *的。如果是运行时间很长的操作，使用LongRunning 参数暗示任务调度器，*将这个任务放在非线程池上运行。通常不需要用这个参数，除非通过性能测试觉得使用该参数能有更好的性能，才使用*。

###   任务等待###   

默认情况下，Task任务是由线程池线程异步执行。要知道Task任务的是否完成，可以通过task.IsCompleted属性获得，也可以使用task.Wait来等待Task完成。Wait会阻塞当前线程。 

```csharp
    static void Main(string[] args) 
            { 
                Task Task1=Task.Run(() => { Thread.Sleep(5000); 
                Console.WriteLine("Foo"); 
                    Thread.Sleep(5000); 
                }); 
                Console.WriteLine(Task1.IsCompleted); 
                Task1.Wait();//阻塞当前线程 
                Console.WriteLine(Task1.IsCompleted); 
            } 
```

Wait方法有个重构方法，签名如下：public bool Wait(int millisecondsTimeout)，接受一个时间。如果在设定时间内完成就返回true，否则返回false。如下的代码：

```csharp
    static void Main(string[] args) 
            { 
                Task Task1=Task.Run(() => { Thread.Sleep(5000); 
                Console.WriteLine("Foo"); 
                    Thread.Sleep(5000); 
                }); 
     
                Console.WriteLine("Task1.IsCompleted:{0}",Task1.IsCompleted); 
                bool b=Task1.Wait(2000); //Wait方法有个重构方法
                Console.WriteLine("Task1.IsCompleted:{0}", Task1.IsCompleted); 
                Console.WriteLine(b); 
                Thread.Sleep(9000); 
                Console.WriteLine("Task1.IsCompleted:{0}", Task1.IsCompleted); 
           } 
```

运行结果为：

[![83987B3EBCAD4B198297D125DF6B849A](assets/2359144_1356612675DcnI.jpg)](https://s1.51cto.com/attachment/201212/27/2359144_1356612675tEqI.jpg)

 

###   获得返回值###    

要获得返回值，就要用到Task的泛型版本了。 

```csharp
    static void Main(string[] args) 
            { 
                Task<int> Task1 = Task.Run<int>(() => { Thread.Sleep(5000); return Enumerable.Range(1, 100).Sum(); }); 
                Console.WriteLine("Task1.IsCompleted:{0}",Task1.IsCompleted); 
                Console.WriteLine("Task1.IsCompleted:{0}", Task1.Result);//如果方法未完成，则会等待直到计算完成，得到返回值才运行下去。 
                Console.WriteLine("Task1.IsCompleted:{0}", Task1.IsCompleted); 
            } 
```

结果如下：

[![EEA94E6174324CFAB04312D10DB3AABD](assets/2359144_1356612675hC5b.jpg)](https://s1.51cto.com/attachment/201212/27/2359144_13566126757OfK.jpg)

###   异常抛出###   

和线程不同，Task中抛出的异常可以捕获，但是也**不是直接捕获**，而是由**调用Wait()方法或者访问Result属性**的时候，由他们获得异常，将这个异常包装成AggregateException类型，再重新抛出捕获。 

```csharp
    static void Main(string[] args) 
            { 
                try 
                { 
                    Task<int> Task1 = Task.Run<int>(() => { throw new Exception("xxxxxx"); return 1; }); 
                    Task1.Wait(); //waiT!!!
                } 
                catch (Exception ex)//error的类型为System.AggregateException 
                { 
                    Console.WriteLine(ex.StackTrace); 
                    Console.WriteLine("-----------------"); 
                    Console.WriteLine(ex.InnerException.StackTrace); 
                } 
            } 
```

如上代码，运行结果如下：

[![D4DA781C92F149E897BE8C325F8A90C0](assets/2359144_1356612678WoSv.jpg)](https://s1.51cto.com/attachment/201212/27/2359144_1356612676q7Ia.jpg)

可以看到异常真正发生的地方。

对于某些匿名的Task（通过 Task.Run方法生成的，不调用wait，也不关心是否运行完成），某些情况下，记录它们的异常错误也是有必要的。这些异常称作未观察到的异常（**unobserved** exceptions）。可以**通过订阅一个全局的静态事件TaskScheduler.UnobservedTaskException来处理这些异常**。只要当一个Task有异常，并且在被垃圾回收的时候，才会触发这一个事件。**如果Task还处于被引用状态，或者只要GC不回收这个Task，这个UnobservedTaskException事件就不会被触发**

例子： 

```csharp
    static void Main(string[] args) 
            { 
                TaskScheduler.UnobservedTaskException += UnobservedTaskException; 
                Task.Run<int>(() => { throw new Exception("xxxxxx"); return 1; }); 
                Thread.Sleep(1000); 
           } 
     
            static void UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e) 
            { 
                Console.WriteLine(e.Exception.Message); 
                Console.WriteLine(e.Exception.InnerException.Message); 
            } 
```

这样的代码直到程序运行完成也为未能触发UnobservedTaskException，因为GC没有开始做垃圾回收。

在代码中加入 GC.Collect(); 

```csharp
    static void Main(string[] args) 
            { 
                TaskScheduler.UnobservedTaskException += UnobservedTaskException; 
                Task.Run<int>(() => { throw new Exception("xxxxxx"); return 1; }); 
                Thread.Sleep(1000); 
                GC.Collect(); 
                GC.WaitForPendingFinalizers(); 
            } 
```

运行后得到如下：

[![6E202F46BB6B4FE384CDD49A564D2A7E](assets/2359144_1356612678XrF9.jpg)](https://s1.51cto.com/attachment/201212/27/2359144_1356612678WHWj.jpg)

 

###   延续任务###   

延续任务就是说当一个Task完成后，继续运行下一个任务。通常有2种方法实现。

一种是使用GetAwaiter方法。GetAwaiter方法返回一个TaskAwaiter结构，该结构有一个OnCompleted事件，只需对OnCompleted事件赋值，即可在完成后调用该事件。 

```csharp
    static void Main(string[] args) 
            { 
                Task<int> Task1 = Task.Run<int>(() => { return Enumerable.Range(1, 100).Sum(); }); 
                var awaiter = Task1.GetAwaiter(); 
                awaiter.OnCompleted(() => 
                { 
                    Console.WriteLine("Task1 finished"); 
                    int result = awaiter.GetResult(); //[result] 
                    Console.WriteLine(result); // Writes result 
                }); 
                Thread.Sleep(1000); 
            } 
```

运行结果如下：

[![0A31FA65B04B477A92175A8184AE2857](assets/2359144_1356612678uRzj.jpg)](https://s1.51cto.com/attachment/201212/27/2359144_1356612678j7EJ.jpg)

此处调用**GetResult()**的好处在于，一旦先前的Task有异常，就会抛出该异常。而且该异常和之前演示的异常不同，它不需要经过AggregateException再包装了。

另一种延续任务的方法是调用**ContinueWith**方法。ContinueWith返回的任然是一个Task类型。ContinueWith方法有很多重载，算上泛型版本，差不多40个左右的。其中最常用的，就是接受一个**Action或者Func委托****，而且，这些委托的第一个传入参数都是Task类型，即可以访问先前的Task对象。示例： 

```csharp
    static void Main(string[] args) 
            { 
                Task<int> Task1 = Task.Run<int>(() => {return Enumerable.Range(1, 100).Sum(); }); 
                Task1.ContinueWith(antecedent => { 
    Console.WriteLine(antecedent.Result); 
    Console.WriteLine("Runing Continue Task"); 
    }); 
                Thread.Sleep(1000); 
            } 
```

使用这种ContinueWith方法和GetAwaiter都能实现相同的效果，有点小区别

就是**ContinueWith**若获取Result的时候有异常，抛出的异常类型是经过AggregateException包裹的，而**GetAwaiter**()后的OnCompleted所调用的方法中，如果出错，直接抛出异常。 

 

###   生成Task的另一种方法，TaskCompletionSource

TaskCompletionSource

使用**TaskCompletionSource**很简单，只需要实例化它即可。TaskCompletionSource有一个Task属性，你可以对该属性暴露的task做操作，比如让它wait或者ContinueWith等操作。当然，这个task由TaskCompletionSource完全控制。TaskCompletionSource<TResult>类中有一些成员方法如下：

```csharp
    public class TaskCompletionSource<TResult> 
    { 
    public void SetResult (TResult result); 
    public void SetException (Exception exception); 
    public void SetCanceled(); 
    public bool TrySetResult (TResult result); 
    public bool TrySetException (Exception exception); 
    public bool TrySetCanceled(); 
    ... 
    } c
```

调用以上方法意味着对Task做状态的改变，将状态设成completed，faulted或者 canceled。这些方法只能调用一次，不然会有异常。Try的方法可以调多次，只不过返回false而已。

通过一些技巧性的编码，将线程和Task协调起来，通过Task获得线程运行的结果。

示例代码： 

```csharp
static void Main(string[] args)       
 { 
            var tcs = new TaskCompletionSource<int>(); 
            new Thread(() => { 
                Thread.Sleep(5000); 
                int i = Enumerable.Range(1, 100).Sum(); 
                tcs.SetResult(i); }).Start();//线程把运行计算结果，设为tcs的Result。 
            Task<int> task = tcs.Task; 
            Console.WriteLine(task.Result); //此处会阻塞，直到匿名线程调用tcs.SetResult(i)完毕 
        } 
```

说明一下以上代码：

tcs是TaskCompletionSource<int>的一个实例，即**这个Task返回的肯定是一个int类型**。

获得tcs的Task属性，读取并打印该属性的值。那么 Console.WriteLine(task.Result);其实是会阻塞的，直到task的result被赋值之后，才会取消阻塞。而对task.result的赋值正在一个匿名线程中做的。也就是说，一直等到匿名线程运行结束，把运行结果赋值给tcs后，task.Result的值才会被获得。这正是**变相的实现了线程同步的功能**，并且可以获得线程的运行值。而此时的线程**并不是运行在线程池上的**！**！！！！**！。

我们可以定义一个泛型方法，来实现一个Task对象，并且运行Task的线程不是线程池线程：

```csharp
    Task<TResult> Run<TResult>(Func<TResult> function) 
            { 
                var tcs = new TaskCompletionSource<TResult>(); 
                Thread t = new Thread(() => 
                 { 
                     try { tcs.SetResult(function()); //赋给某个task
                         } 
                     catch (Exception ex) { tcs.SetException(ex); } 
                 }); 
                t.IsBackground = true; 
                t.Start();//启动线程 
                return tcs.Task; 
            } 
```

比如什么一个泛型方法，接受的参数是Func委托，返回的是Task类型。

该方法中启动一个线程t，把t设为后台线程，该线程运行的内容就是传入的Func委托，并将Func委托的运行后的返回值通过**tcs.SetResult赋给某个task**。同时，如果有异常的话，就把异常赋给，某个task，然后将这个task返回。这样，直到线程运行完毕，才能得到task.Result的值。调用的时候： 

```csharp
    Task<int> task = Run(() => { 
        Thread.Sleep(5000); 
        return Enumerable.Range(1, 100).Sum(); }); 
    Console.Write(task.Result);//这句会阻塞当前线程，直到task的result值被赋值才行。 
```

##### 创建Task，而不绑定任何线程

TaskCompletionSource的另一个强大用处，是可以创建Task，而不绑定任何线程，比如，我们可以通过TaskCompletionSource实现对某一个方法的延迟调用。

代码示例： 

```csharp
    static Task<int> delayFunc() 
            { 
                var tcs = new TaskCompletionSource<int>(); 
                var timer = new System.Timers.Timer(5000) { AutoReset = false }; 
                timer.Elapsed += (sender, e) => { 
                    timer.Dispose(); 
                    int i = Enumerable.Range(1, 100).Sum(); 
                    tcs.SetResult(i); 
                }; 
     
                timer.Start(); 
                return tcs.Task; 
            } 
```

说明：

delayFunc()方法使用了一个定时器，5秒后，定时器事件触发，将i的值赋给某个task的result。返回的是tcs.Task属性，调用方式: 

```
var task = delayFunc(); 
Console.Write(task.Result); 
```

task变量得到赋值后，要读取Result值，必须等到tcs.SetResult(i);运行完成才行。这就相当于实现了延迟某个方法。

当然Task自身提供了Delay方法，使用方法如下： 

```csharp
    Task.Delay (5000).GetAwaiter().OnCompleted (() =>
                          Console.WriteLine (42)); 
    或者: 
    Task.Delay (5000).ContinueWith (ant => Console.WriteLine (42)); 
```

Delay方法是相当于异步的Thread.Sleep();

\---------------------------------

参考资料：《C# 5.0 IN A NUTSHELL》

MSDN官方资料