​		

https://www.cnblogs.com/guwei4037/p/5604724.html

### [.net WebApi 开发中某些注意事项](https://somefuture.iteye.com/blog/2172869)

1、简述javascript中的“=、==、===”的区别？

=赋值

==比较是否一般相等   "3"==3 //会做类型的隐式转换，true

===比较是否严格相等 "3"===3 //先比较类型,再进行值比较，false 

 

2、看下列代码输出为何？解释原因
var a = null;
alert(typeof a); 
var b;
alert(typeof b);
alert(c);

a为null，也是一个对象，所以typeof(a)为object.

b只有声明没有初始化，因此typeof(b)为undefined.

c没有定义，因此alert(c)会出现error.


3、编写javascript代码实现把两个数组合并，并删除第二个元素。

合并js数组用concat方法，array1.concat(array2)。

删除元素用splice方法，splice(1,1)，函数原型splice(index,count)，指从数组索引1处开始删除1个元素，即删除第二个元素。


4、简述javascript的作用域和闭包

js变量的作用域是指：函数内定义的局部变量只在此函数内有效，而全局变量可以全局有效。

闭包的作用就在于能够改变局部变量的作用域，将值保存下来，但是如果使用不当会造成无法回收变量，引起性能问题，甚至崩溃。


5、列举你用过的javascript框架，并简述它们的优缺点

js框架：jQuery EasyUI、ExtJS、Bootstrap、AngularJS等等。

jQuery EasyUI：轻量级web前端ui开发框架，尤其适合MIS系统的界面开发，能够重用jquery插件。

ExtJS：统一的前端UI开发框架，学习难度中等。尤其适合MIS系统的界面开发，开发文档和例子代码都比较完整。缺点是大量的js脚本，降低了运行速度。

Bootstrap：响应式网站开发框架，优点是降低了后端开发人员开发前端页面的难度，统一了界面风格，缺点是界面风格比较单一。

AngularJS：将java后端的优秀特性引入到了js前端，大而全的框架。缺点是学习曲线高，Angular2几乎重写。


6、简述a.Equals(b)和a==b的区别？

Equals方法比较内容（值是否相等），==比较引用地址（是否指向同一个对象）。

——————

更正一下，在Java中上述结论是正确的，但在C#中却正好反过来，即：**==比较内容是否相等，Equals先比较值，然后再比较引用**。


7、ASP.NET的Application、Session、Cookie、ViewState和Cache等变量的区别是什么？

Application 应用程序级别

Session 会话级别用户跟踪

Cookie 客户端存储少量信息

ViewState 保持ASP.NET控件状态的机制

Cache 缓存


8、列举ASP.NET MVC ActionResult的返回值有几种类型？

主要有View（视图）、PartialView（部分视图）、Content（内容）、Json（Json字符串）、Javascript（js脚本）、File（文件）等几种类型。


9、简述ASP.NET WebApi相对于ASP.NET MVC的优点？

WebApi消息处理管道独立于ASP.NET平台，支持多种寄宿方式。

 

10、简述ASP.NET请求的生命周期？

用户从 Web 服务器请求应用程序资源->ASP.NET 接收对应用程序的第一个请求->为每个请求创建 ASP.NET 核心对象->将[HttpApplication](https://msdn.microsoft.com/zh-cn/library/system.web.httpapplication(v=vs.100).aspx)对象分配给请求->由[HttpApplication](https://msdn.microsoft.com/zh-cn/library/system.web.httpapplication(v=vs.100).aspx) 管线处理请求

refer: https://msdn.microsoft.com/zh-cn/library/ms178473(v=vs.100).aspx


11、ORM中的延迟加载与直接加载有什么异同？

延迟加载（Lazy Loading）只在真正需要进行数据操作的时候再进行加载数据，可以减少不必要的开销。


12、简述Func<T>与Action<T>的区别？

Func<T>是有返回值的委托，Action<T>是没有返回值的委托。


13、开启一个异步线程的几种方式？多线程编程时的注意事项？

APM（Asynchrocous Programming  Model，比如：BeginXXX、IAsyncResult）、EAP（Event-Based Asynchronous  Pattern，比如：DownloadContentAsync）、TPL（Task Parallel  Library，比如：Task.Factory.StartNew）、async/await。

线程饿死、线程死锁、线程同步、线程安全。


14、简述Linq是什么，以及Linq的原理？并编写一个Linq to Object的示例代码

Linq（Language Integrated Query），其中Linq to Object是对Enumberable扩展方法的调用，在执行时会转化为Lambda然后执行。

示例代码：Linq分组统计

[![复制代码](assets/copycode-1558867625859.gif)](javascript:void(0);)

```
var result = from p in list.AsEnumerable() 
             group p by p.Province into g 
             select new 
             { 
                 g.Key, 
                 SumValue = g.Sum(p => p.Value) 
             }; 
result.ToList().ForEach((i) => 
{ 
    Console.WriteLine(i.Key + ":" + i.SumValue); 
}); 
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

15、简述面向对象的特性有哪些？你是如何理解的？

继承（inheritance）、封装（encapsulation）、多态（polymorphism）。


16、列举你所知道的设计模式？你在真实项目中使用过的有哪些？有什么心得？

单例模式、模板方法、工厂模式、外观模式、策略模式等。


17、编写SQL从A表中查出Name字段重复三条以上的记录，并编写SQL删除这些重复记录

查询Name字段记录重复三条以上的记录

```
select name from A group by name having count(name)>3
```

 删除重复记录

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
if exists(select * from sysobjects where name = 'tempA')
  drop table tempA
select ROW_NUMBER() over (order by name ) as rowid, name into tempA from A

select * from tempA

--删除重复记录，只保留rowid最小的那一行
delete from tempA where name in
(select name from tempA group by name having count(name)>3)
and rowid not in (select min(rowid) from tempA group by name having count(name)>3)

select * from tempA
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 