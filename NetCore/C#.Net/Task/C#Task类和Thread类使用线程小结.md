# C#中如果正确使用线程Task类和Thread类

2014年09月18日 10:39:05

海风鸥  所属专栏：                                   [C# 实时监控线程封装](https://blog.csdn.net/column/details/28057.html)                                                

​                   

 版权声明：未经允许，不可转载。          https://blog.csdn.net/zhengxu25689/article/details/39368485        

**C#中使用线程Task类和Thread类小结**

​             刚接触C#3个月左右，原先一直使用C++开发，因为公司的需要，所地采用C#开发，主要是控制设备的实时性操作，此为背景。

​              对于C#中的Task和Thread我在这不作介绍，要了解更多的，如果查看相当信息。此次项目中使用到TASK和THRED，让我调试足足用了将近两周的时间才找出问题所在，所以在此写出来防止跟我一样刚接触C#，又同时需要对线程的实时性要求的开发人员一些个人总结注意事项。

>  [C#中如果正确使用线程Task类和Thread类](https://blog.csdn.net/zhengxu25689/article/details/39368485)
>
>  ​        1.Task[适合用于**多处理器**，且i系列多处理器](https://blog.csdn.net/zunguitiancheng/article/details/78995143)。
>
>  
>
>  有非常方便的对并行运行的任务集合获取返回值的方式，比如***\*WhenAll\****和***\*WhenAny\****。
>
>  > 程序中添加很多异步的操作作为Task实例加载的时候，为了充分利用运行时所有可用的逻辑内核，任务调度器会尝试的并行的运行这些任务，也会尝试在所有的可用内核上对工作进行负载均衡。
>  >
>  > 但在实际的编码过程当中，并不是所有的代码片段都能够方便的用任务来运行，因为任务会带来额外的开销，尽管这种开销比添加线程所带来的开销要小，但是仍然需要将这个开销考虑在内。
>
>  ​          2.Thread则适用于**所有的处理器，实时性更高**。





> Task是依赖于CPU的，如果你的CPU是**多核**的，Task效率相对要高些
> 如果是单核的，Task没有什么优势
>
> task类似于轻量级的线程池，但是比线程池多了一些有点，比如是状态之类的
>
> 这个我刚刚测试了，确实**需要更多的内存**。
>
> TASK注重点在并行~ 所以如果你是工作在**多核情况**下，那么task或许是你最好的选择了，但是thread却无法实现自动化的并行操作~ 
> task是基于threadPool的，所以相比thread来说，就算再单核，我也依然觉得task这种方式会比thread强~ 



> 只有当async和await关键词同时出现， 
> 该方法才被转换为异步方法处理。 
> [await关键处的代码片段是在线程池线程上执行](https://www.cnblogs.com/zhili/archive/2013/05/15/Csharp5asyncandawait.html)
>
> async[不会影响方法是同步还是异步运行并完成](http://developer.51cto.com/art/201305/393992_all.htm),而是，它使方法可被分割成多个片段，其中一些片段可能异步运行，这样这个方法可能异步完成。
>



​            下面是我的个人测试代码：

​             其中使用的对错可能是我个人对C#线程理解不够引起的，如果有使用不正确错，望大鸟指导。

```csharp
        private static void ThreadAndTaskTest()



        {



            Stopwatch watch = new Stopwatch();



            watch.Start();



 



<p>            //Thread threadTest1 = new Thread(() =>



            //{



            //    Thread.Sleep(2000);



            //    Debug.WriteLine("线程1结束消耗时间：{0}", watch.ElapsedMilliseconds);



            //});



            //threadTest1.Start();</p><p>            //Thread threadTest2 = new Thread(() =>



            //{



            //    Thread.Sleep(2000);



            //    Debug.WriteLine("线程2结束消耗时间：{0}", watch.ElapsedMilliseconds);



            //});



            //threadTest2.Start();</p><p>            //Thread threadTest3 = new Thread(() =>



            //{



            //    Thread.Sleep(2900);



            //    Debug.WriteLine("线程2结束消耗时间：{0}", watch.ElapsedMilliseconds);



            //});



            //threadTest3.Start();</p><p> </p>            var Task1 = Task.Factory.StartNew(() =>



            {



                Thread.Sleep(2500);



                Debug.WriteLine("线程1结束消耗时间：{0}", watch.ElapsedMilliseconds);



            });



 



            var Task2 = Task.Factory.StartNew(() =>



            {



                Thread.Sleep(2700);



                Debug.WriteLine("线程2结束消耗时间：{0}", watch.ElapsedMilliseconds);



            });



 



            var Task3 = Task.Factory.StartNew(() =>



            {



                Thread.Sleep(2900);



                Debug.WriteLine("线程3结束消耗时间：{0}", watch.ElapsedMilliseconds);



            });



 



            while (watch.ElapsedMilliseconds <= 3000)



            {



                //if (!threadTest.IsAlive && !threadTest1.IsAlive)



                if (Task1.IsCompleted && Task2.IsCompleted && Task3.IsCompleted)



                {



                    Debug.WriteLine("监控结束消耗时间：{0}", watch.ElapsedMilliseconds);



                    break;



                }



                else



                    Thread.Sleep(1);



            }



        }
```

​             上面采用Task测试结果如下：

​             线程1结束消耗时间：2503
​              线程2结束消耗时间：2703
​              线程3结束消耗时间：3944(理论应该2905)

 

​             同样的代码采用Thread的测试结如下：

​             线程2结束消耗时间：2003
​              线程1结束消耗时间：2002
​              线程2结束消耗时间：2905


​            上面测试环境在:

​            处理器：Pentium（R）Dual-Core CPU E6700 @3.20GHXZ

​            安装内存（RAM）：4.0GB

​            系统类型：32位操作系统

 

​           如果采用 i5系列的CPU，其它硬件环境都一样则不会有这种超时情况.

​             

​             在此也遇到点问题：线程20个以上同时运行，线程的实时性差异也很大，同一个线程函数差距有700ms.           