# [C#笔记（四）：Lambda 表达式](https://www.cnblogs.com/ForEvErNoME/archive/2012/08/21/2633767.html)



最近实习早出晚归很累，一直没有更新！额，基础还是要巩固的！

**匿名函数**

在学习委托时，有个概念叫做匿名函数：即不需要在外部定义方法，直接在初始化委托时申明方法。先来看一个例子。

[![复制代码](C#%E7%AC%94%E8%AE%B0%EF%BC%88%E5%9B%9B%EF%BC%89%EF%BC%9ALambda%20%E8%A1%A8%E8%BE%BE%E5%BC%8F.assets/copycode.gif)](javascript:void(0);)

```
    class Program
    {
        static void Main(string[] args)
        {
            //ad指向匿名函数
            //delegate(参数列表){方法体}
            AnonyDelegate ad = delegate(string str)
            {
                Console.WriteLine(str);
            };
            //调用委托
            ad("Hello World");
            Console.ReadKey();
        }
        //申明委托
        public delegate void AnonyDelegate(string s);
    }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

其实在调用时，匿名函数的参数的列表类型必须对应定义委托的参数列表类型，每次都需要自己去手动写。那么，能不能有一种方法能让编译器自动推断委托的参数列表类型呢？此时，Lambda 表达式就可以帮助我们处理这个问题了。

使用Lambda表达式：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
        static void Main(string[] args)
        {
            //能够根据委托推断出参数的列表类型，比匿名函数更简单
            AnonyDelegate ad = (str) =>{ Console.WriteLine(str); };
            //调用委托
            ad("Hello World");
            Console.ReadKey();
        }
        //申明委托
        public delegate void AnonyDelegate(string s);
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**Lambda表达式**

对于Lambda表达式的一些介绍，msdn也讲得挺清晰的。

"Lambda表达式"是一个匿名函数，它可以包含表达式和语句，并且可用于创建委托或表达式树类型。

所有 Lambda 表达式都使用 Lambda 运算符 =>，该运算符读为"goes to"。 该 Lambda 运算符的左边是输入参数（如果有），右边包含表达式或语句块。 Lambda 表达式 x => x * x 读作"x goes to x times x"。这句话可以简单的总结为Lambda 表达式的语法结构：

```
（输入参数）=> 表达式
```

注意点：

（1）当Lambda 只有一个输入参数时，可以省略括号，但其他情况是必须要加的。

```
            //当只有一个输入参数时，可以省略括号
            AnonyDelegate ad = str =>{ Console.WriteLine(str); };
```

（2）当只有一个空的括号时，表示没有参数。

```
            AnonyDelegate2 ad2 = () => { Console.WriteLine("Hello World"); };
           //申明委托
            public delegate void AnonyDelegate2();
```

（3）当lambda表达式中的匿名函数的方法体如果只有一句话，并且是返回值，那么可以省略{}以及return，就把=>后的表达式做为返回值

```
        //申明委托
        public delegate bool AnonyDelegate3(string str);
        AnonyDelegate3 ad3 = (str) => str == "Hello World";
        Console.WriteLine(ad3("Hello World"));
```

 

**Lambda表达式使用场景**

这一块内容相对比较重要，Lambda表达式现在应用很广，例如查询表达式，所以需要掌握这个基础知识。

Lambda 在基于方法的 LINQ 查询中用作标准查询运算符方法（如 Where等）的参数。

使用基于方法的语法在 Enumerable 类中调用 Where 方法时（像在 LINQ to Objects），参数是委托类型 System.Func<T, TResult>。 使用 Lambda 表达式创建委托很方便。 例如，当您在 System.Linq.Queryable 类中调用相同的方法时，则参数类型是 System.Linq.Expressions.Expression<Func>，其中 Func 是包含至多十六个输入参数的任何 Func 委托。 同样，Lambda 表达式只是一种用于构造表达式树的非常简练的方式。

使用Lambda表达式如何筛选数组中大于0的数呢？其实非常简单！

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
            int[] values = { -8, 2, 13, -5, 9 };
            IEnumerable<int> result = values.Where(s => s > 0);
            foreach (var val in result)
            {
                Console.WriteLine(val);
            }
            Console.ReadKey();
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 那么为什么直接可以使用该方法呢？我们转到where的方法定义，可以看到

```
public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);
```

Tsource:source 中的元素的类型。这里指int类型。

source:类型为System.Collections.Generic.IEnumerable<TSource>。要筛选的 IEnumerable<T>。这里指values数组。

predicate:类型为System.Func<TSource, Boolean>。用于测试每个元素是否满足条件的函数。这是指方法体的实现。

返回值:一个 IEnumerable<T>，包含输入序列中满足条件的元素。这里指返回IEnumerable<T>。

Func<TSource,bool> :实际上是泛型委托的 Func<T, TResult> 系列的其中之一。 Func<T, TResult> 委托使用类型参数定义输入参数的数目和类型，以及委托的返回类型。这是指int为输入参数，bool是返回类型。

同理其他的方法原理差不多。轻描淡写，下阶段再写深入些的！