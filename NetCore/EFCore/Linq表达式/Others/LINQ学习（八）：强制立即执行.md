# [LINQ学习（八）：强制立即执行](https://www.cnblogs.com/ForEvErNoME/archive/2012/07/28/2612821.html)





说明：我们可以知道所有 LINQ 查询操作都由以下三个不同的操作组成：获取数据源、创建查询、执行查询。执行查询可分为延迟执行和强制立即执行。

强制立即执行：

1.使用聚合函数（Count、Max、Average、First）等强制执行，计算并返回单一实例结果。

2.可通过对查询（立即执行）或查询变量（延迟执行）调用 ToList 或 ToArray 等方法来强制执行查询。

位于System.Linq中的Enumerable类提供一组用于查询实现 IEnumerable<T> 的对象的静态方法。具体方法介绍可查看[MSDN](http://msdn.microsoft.com/zh-cn/library/system.linq.enumerable_methods)。

 

**测试数据：**

[![复制代码](LINQ%E5%AD%A6%E4%B9%A0%EF%BC%88%E5%85%AB%EF%BC%89%EF%BC%9A%E5%BC%BA%E5%88%B6%E7%AB%8B%E5%8D%B3%E6%89%A7%E8%A1%8C.assets/copycode.gif)](javascript:void(0);)

```
        public class Student
        {
            public int NumId{ get; set; }           
            public string Name{ get; set; }
            public int Score { get; set; }
        }
            List<Student> students = new List<Student>{
               new Student {NumId=3,Name="Terry", Score=55},
               new Student {NumId=1,Name="AI", Score=80},
               new Student {NumId=3,Name="Kobe", Score=40},
               new Student {NumId=8,Name="James", Score=90},
               new Student {NumId=5,Name="Love", Score=60},
               new Student {NumId=6,Name="Wade", Score=85},
            };
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**1.使用聚合函数**

说明：函数有很多，这里只介绍几个：Count、Max、Average、First。

查询学生的人数：

```
            int count = (from student in students
                         select student.NumId).Count();
            Console.WriteLine("学生的人数：{0}", count);
            //学生的人数：6
```

查询最高分：

```
            int maxscore = (from student in students
                            select student.Score).Max();
            Console.WriteLine("最高分：{0}", maxscore);
            //最高分：90
```

查询平均分：

```
            double average = (from student in students
                              select student.Score).Average();
            Console.WriteLine("平均分：{0}", average);
            //平均分：68.3333333333333
```

返回列表第一个学生姓名：

```
            string Name = (from student in students
                           select student.Name).First();
            Console.WriteLine("返回列表第一个学生姓名：{0}", Name);
            //返回列表第一个学生姓名：Terry
```

 

**2.生成结果集**

说明：只有使用ToList 或 ToArray 等方法来强制转换，才能使变量立即获取结果集。

转换为List集合：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
            var querylist = (from student in students
                             select student).ToList();
            foreach (var student in querylist)
            {
                Console.WriteLine("{0} {1} {2}", student.NumId, student.Name, student.Score);
                //3 Terry 55
                //1 AI 80
                //3 Kobe 40
                //8 James 90
                //5 Love 60
                //6 Wade 85　　　　　　　　　//注意：变量querylist已经存储结果集
            }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

转换为Array数组：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
            var queryarray = (from student in students
                             select student).ToArray();
            foreach (var student in queryarray)
            {
                Console.WriteLine("{0} {1} {2}", student.NumId, student.Name, student.Score);
                //3 Terry 55
                //1 AI 80
                //3 Kobe 40
                //8 James 90
                //5 Love 60
                //6 Wade 85　　　　　　　　　//注意：变量queryarray已经存储结果集
            }
```

[![复制代码](LINQ%E5%AD%A6%E4%B9%A0%EF%BC%88%E5%85%AB%EF%BC%89%EF%BC%9A%E5%BC%BA%E5%88%B6%E7%AB%8B%E5%8D%B3%E6%89%A7%E8%A1%8C.assets/copycode.gif)](javascript:void(0);)

