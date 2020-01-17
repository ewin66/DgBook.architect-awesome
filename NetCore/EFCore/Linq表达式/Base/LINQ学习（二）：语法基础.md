# [LINQ学习（二）：语法基础](https://www.cnblogs.com/ForEvErNoME/archive/2012/07/23/2602255.html)



在学习LINQ查询语法前，需要理解C#3.0的一些新特性。额，现在C#4.5都出了，不详细讲了，[C#3.0参考资料](http://www.cnblogs.com/KevinYang/archive/2008/11/04/1326101.html)。

 下面看下这个查询表达式：

[![复制代码](LINQ%E5%AD%A6%E4%B9%A0%EF%BC%88%E4%BA%8C%EF%BC%89%EF%BC%9A%E8%AF%AD%E6%B3%95%E5%9F%BA%E7%A1%80.assets/copycode.gif)](javascript:void(0);)

```
            int[] numbers = new int[] { 1, 5, 3, 6, 2};
            var query = from num in numbers
                        where num > 2
                        orderby num ascending
                        select num;
            foreach (int num in query)
            {
                Console.Write("{0} ", num);
            }    
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

**结构特点：**

\1. LINQ  查询变量类型化为 IEnumerable<T> 或派生类型，如 IQueryable<T>。

用 var关键字来避免使用泛型语法，query变量在这里指的是IEnumerable<int>（query变量也称作范围变量，它类似于 foreach 语句中的迭代变量，只是两者之间有一个非常重要的区别：范围变量从不实际存储来自数据源的数据）。 在 LINQ to XML 中，源数据显示为一个 IEnumerable<XElement>。 在 LINQ to DataSet 中，它是一个 IEnumerable<DataRow>。 在 LINQ to SQL 中，它是您定义用来表示 SQL 表中数据的任何自定义对象的 IEnumerable 或 IQueryable。

\2. 查询表达式必须以 from 子句开头，并且必须以 select 或 group 子句结尾。

第一个 from 子句和最后一个 select 或 group 子句之间，查询表达式可以包含一个或多个下列可选子句：where、orderby、join、left 甚至附加的 from 子句。 还可以使用 into 关键字使 join 或 group 子句的结果能够充当同一查询表达式中附加查询子句的源。（之后的文章将会详细介绍这一部分）

\3. IEnumerable<T> 是一个接口，通过该接口，可以使用 foreach 语句来枚举泛型集合类。

 