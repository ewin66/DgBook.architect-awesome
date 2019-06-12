##                                                                             .Net Core中使用ref和Span<T>提高程序性能                       

​                                                                                                                                  张磊                                                                                                                                                      [                         dotNET跨平台                      ](javascript:void(0);)                                                                   *2017-05-21*                      

# 一、前言

其实说到ref，很多同学对它已经有所了解，ref是C# 7.0的一个语言特性，它为开发人员提供了返回本地变量引用和值引用的机制。
Span也是建立在ref语法基础上的一个复杂的数据类型，在文章的后半部分，我会有一个例子说明如何使用它。



# 二、ref关键字

不论是ref还是out关键，都是一种比较难以理解和操作的语言特性，如C语言中操作指针一样，这样的高级语法总是什么带来一些副作用，但是我不认为这有什么，而且不是每一个C#开发者都要对这些内部运行的机制有着深刻的理解，我觉得不论什么复杂的东西只是为人们提供了一个自由的选择，风险和灵活性永远是不能兼容的。

来看几个例子来说明引用与指针的相同性，当然下面的使用方式早在C# 7.0之前就可以使用了：

```csharp
public static void IncrementByRef(ref int x){
    x++;
}public unsafe static void IncrementByPointer(int* x){
   (*x)++;
}
```

上面两个函数分别是使用ref和非安全指针来完成参数+1。

```
int i = 30;
IncrementByRef(ref i);// i = 31unsafe{
   IncrementByPointer(&i);
}// i = 32
```

下面是C# 7.0提供的特性：

## 1.ref locals (引用本地变量)

```csharp
int i = 42;		ref var x = ref i;
x = x + 1;// i = 43
```

这个例子中为本地 i 变量的引用 x, 当改变x的值时i变量的值也改变了。

## 2.ref returns (返回值引用)

ref returns是C# 7中一个强大的特性，下面代码是最能体现其特性的，该函数提供了，返回int数组中某一项的引用：

```csharp
public static ref int GetArrayRef(int[] items, int index)
    	=> ref items[index];
```

通过下标取得数组中的项目的引用，改变引用值时，数组也会随之改变。



# 三、Span

System.Span是.Net Core核心的一部分，在**System.Memory.dll** 程序集下。目前该特性是独立的，将来可能会集成到CoreFx中；

如何使用呢？在.Net Core 2.0 SDK创建的项目下引用如下NuGet包：

```
  <ItemGroup>
    <PackageReference Include="System.Memory" Version="4.4.0-preview1-25305-02" />
    <PackageReference Include="System.Runtime.CompilerServices.Unsafe" Version="4.4.0-preview1-25305-02" />
  </ItemGroup>
```

在上面我们看到了使用ref关键字可以提供的**类似指针(T*)的操作单一值对象方式**。基本上在.NET体系下操作指针都不认为是一件好的事件，当然.NET为我们提供了安全操作单值引用的ref。但是单值只是用户使用“指针”的一小部分需求；对于指针来说，更常见的情况是操作一系列连续的内存空间中的“元素”时。

Span表示为一个已知长度和类型的连续内存块。许多方面讲它非常类似T[]或ArraySegment，它提供安全的访问内存区域指针的能力。其实我理解它更将是.NET中操作(void*)指针的抽象,熟悉C/C++开发者应该更明白这意味着什么。

Span的特点如下：

- 抽象了所有**连续内存空间的类型系统**，包括：数组、非托管指针、堆栈指针、fixed或pinned过的托管数据，以及值内部区域的引用
- 支持CLR标准对象类型和值类型
- 支持泛型
- 支持GC,而不像指针需要自己来管理释放

下面来看下Span的定义，它与ref有着语法和语义上的联系：

```
public struct Span<T> {  
  ref T _reference; 
     int _length;  
       public ref T this[int index] { get {...} }
    ...
}
       public struct ReadOnlySpan<T> {  
         ref T _reference;    int _length;    public T this[int index] { get {...} }
    ...
}
```

接下来我会用一个直观的例子来说明**Span**的使用场景；我们以字符截取和字符转换（转换为整型）为例：

如有一个字符串**string content = "content-length:123"**,要转换将123转换为整型，通常的做法是先Substring将与数字字符无关的字符串进行截断，转换代码如下：

```csharp
string content = "content-length:123";
Stopwatch watch1 = new Stopwatch();
watch1.Start();for (int j = 0; j < 100000; j++)
{    int.Parse(content.Substring(15));
}
watch1.Stop();
Console.WriteLine("\tTime Elapsed:\t" + watch1.ElapsedMilliseconds.ToString("N0") + "ms");
```

为什么使用这个例子呢，这是一个典型的substring的使用场景，每次操作**string**都会生成新的**string对象**,当然不光是Substring,在进行**int.Parse**时重复操作string对象，如果大量操作就会给GC造成压力。

使用Span实现这个算法：

```csharp
string content = "content-length:123";
ReadOnlySpan<char> span = content.ToCharArray();    
span.Slice(15).ParseToInt();
watch.Start();for (int j = 0; j < 100000; j++)
{    int icb = span.Slice(15).ParseToInt();
}
watch.Stop();
Console.WriteLine("\tTime Elapsed:\t" + watch.ElapsedMilliseconds.ToString("N0") + "ms");
```

这里将string转换为int的算法利用ReadonlySpan实现，这也是Span的典型使用场景，官方给的场景也是如些，Span适用于多次复用操作连续内存的场景。

转换代码如下：

```csharp
public static class ReadonlySpanxtension{    
public static int ParseToInt(
this ReadOnlySpan<char> rspan)   
 {
    Int16 sign = 1;      
  	int num = 0;
    UInt16 index = 0;      
    if (rspan[0].Equals('-')){
            sign = -1; index = 1;
        }      
      for (int idx = index; idx < rspan.Length; idx++){        
        char c = rspan[idx];
            num = (c - '0') + num * 10;
        }        return num * sign;
    }

}
```



# 四、最后

上述两段代码100000次调用的时间如下：

```csharp
String 	Substring Convert:        Time Elapsed:   18ms
		ReadOnlySpan Convert:        Time Elapsed:   4ms
```

目前Span的相关支持还够，它只是最基础架构，之后CoreFx会对很多API使用Span进行重构和实现。可见.Net Core的性能日后会越来越强大。

原文地址：http://www.cnblogs.com/maxzhang1985/p/6875622.html