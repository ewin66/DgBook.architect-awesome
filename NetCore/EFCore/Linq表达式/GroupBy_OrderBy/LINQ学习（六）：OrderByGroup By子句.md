# [LINQ学习（六）：OrderBy/Group By子句](https://www.cnblogs.com/ForEvErNoME/archive/2012/07/26/2608424.html)





**测试数据：**

[![复制代码](LINQ%E5%AD%A6%E4%B9%A0%EF%BC%88%E5%85%AD%EF%BC%89%EF%BC%9AOrderByGroup%20By%E5%AD%90%E5%8F%A5.assets/copycode-1579247783950.gif)](javascript:void(0);)

```
           class Student
           {
               public string Name { get; set; }
               public int Score { get; set; }
            }
            List<Student> students = new List<Student>{
               new Student {Name="Terry", Score=50},          
               new Student {Name="Tom", Score=85},
               new Student {Name="Wade", Score=90},
               new Student {Name="James", Score=70},
               new Student {Name="Kobe", Score=90},
               new Student {Name="AK", Score=90},             
            };
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**1.OrderBy**

说明：在查询表达式中，orderby子句可对集合按升序（ascending）或降序（descending）排序（默认的是升序）。可以指定多个排序的值，以便执行一个或多个次要排序操作。

对分数进行降序排序，然后再将分数相同的学生姓名进行升序排列（分数为主要排序，姓名为次要排序）：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
            var query = from student in students
                        orderby student.Score descending, student.Name
                        select student;
            foreach (var student in query)
            {
                Console.WriteLine("{0}:{1}", student.Name, student.Score);
                //AK:90
                //Kobe:90
                //Wade:90
                //Tom:85
                //James:70
                //Terry:50
            }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

 **2.Group By**

 （1）说明：group子句返回一个 IGrouping<TKey, TElement> 对象序列，这些对象包含零个或更多个与该组的键值匹配的项。 例如，可以按照每个字符串中的第一个字母对字符串序列进行分组。 在这种情况下，第一个字母是键且具有 char 类型，并且存储在每个 IGrouping<TKey, TElement> 对象的Key属性中。

按照学生分数分组：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
            var query = from student in students
                        group student by student.Score;
            foreach (var studentGroup in query)
            {
                //studentGroup推断为IGrouping<int,Student>类型
                Console.WriteLine("{0}", studentGroup.Key);
                //50
                //85
                //90
                //70
            }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

（2）说明：由于 group 查询产生的 IGrouping<TKey, TElement> 对象实质上是列表的列表，因此必须使用嵌套的 foreach 循环来访问每一组中的各个项。 外部循环用于循环访问组键，内部循环用于循环访问组本身中的每个项。组可能具有键，但没有元素。如果您想要对每个组执行附加查询操作，则可以使用 into 上下文关键字指定一个临时标识符。 使用 into 时，必须继续编写该查询，并最终用一个 select 语句或另一个 group 子句结束该查询。

查询每个分数组中的每个学生的信息：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
            var query = from student in students
                        group student by student.Score into g
                        select g;
            foreach (var studentGroup in query)
            {
                Console.WriteLine("分数组：{0}", studentGroup.Key);
                foreach (var student in studentGroup)
                {
                    Console.Write("{0}：{1}，", student.Name,student.Score);
                }
                Console.WriteLine();
                //分组：50
                //Terry：50，
                //分组：85
                //Tom：85，
                //分组：90
                //Wade：90，Kobe：90，AK：90，
                //分组：70
                //James：70，
            }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

（3）同样的，group 子句可按照任何类型进行分组，如字符串、内置数值类型、用户定义的命名类型或匿名类型。形式差不多，有需要可查阅其他资料

 