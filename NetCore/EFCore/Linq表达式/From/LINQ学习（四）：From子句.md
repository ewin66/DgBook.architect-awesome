# [LINQ学习（四）：From子句](https://www.cnblogs.com/ForEvErNoME/archive/2012/07/24/2605712.html)



**说明：**查询表达式必须以 from 子句开头。 另外，查询表达式还可以包含子查询，子查询也是以 from 子句开头。SQL命令中from指的是数据表，LINQ中from 子句中引用的数据源的类型必须为 IEnumerable、IEnumerable<T> 或一种派生类型（如 IQueryable<T>）。

 

**1.复合from子句**

说明：如果一个数据源里面又包含了一个或多个集合列表，那么应该使用复合的from子句来进行查询。

查询分数小于60分学生的姓名和分数：

[![复制代码](LINQ%E5%AD%A6%E4%B9%A0%EF%BC%88%E5%9B%9B%EF%BC%89%EF%BC%9AFrom%E5%AD%90%E5%8F%A5.assets/copycode.gif)](javascript:void(0);)

```
        class Student
        {
            public string Name { get; set; }
            public List<int> Scores { get; set; }
        }
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>{
               new Student {Name="Terry", Scores=new List<int> {97, 72, 81, 60}},
               new Student {Name="AI", Scores=new List<int> {75, 84, 91, 39}},
               new Student {Name="Wade", Scores=new List<int> {88, 94, 65, 85}},
               new Student {Name="Tracy", Scores=new List<int>{97, 89, 85, 82}},
               new Student {Name="Kobe", Scores=new List<int> {35, 72, 91, 70}} 
            };
            var query = from student in students
                        from score in student.Scores
                        where score < 60
                        select new { name = student.Name, score };
            foreach (var student in query)
            {
                Console.WriteLine("{0},{1}", student.name, student.score);
                //AI,39
                //Kobe,35
            }
            Console.ReadKey();
        }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**2.使用多个 from 子句执行联接**

说明：复合 from 子句用于访问单个数据源中的内部集合。 不过，查询还可以包含多个可从独立数据源生成补充查询的 from 子句。

交叉联接：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```csharp
            char[] char1 = { 'A', 'B', 'C' };
            char[] char2 = { 'a', 'b', 'c' };
            var query =
                from c1 in char1
                from c2 in char2
                select new { c1, c2 };
            Console.WriteLine("交叉联接:");
            foreach (var result in query)
            {
                Console.WriteLine("{0}", result);
                //交叉联接:
                //{ c1 = A, c2 = a }
                //{ c1 = A, c2 = b }
                //{ c1 = A, c2 = c }
                //{ c1 = B, c2 = a }
                //{ c1 = B, c2 = b }
                //{ c1 = B, c2 = c }
                //{ c1 = C, c2 = a }
                //{ c1 = C, c2 = b }
                //{ c1 = C, c2 = c }
            }
```