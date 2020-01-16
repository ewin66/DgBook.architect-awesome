





Select操作包括7种形式





**说明：**在查询表达式中，select 子句可以指定将在执行查询时产生的值的类型。 该子句的结果将基于前面所有子句的计算结果以及 select 子句本身中的所有表达式。 查询表达式必须以 select 子句或 group 子句结束。

 

Select操作包括7种形式，分别为简单用法、匿名类型形式、条件形式、筛选形式、嵌套类型形式、本地方法调用形式、Distinct形式。下面分别用实例举例下：

[![复制代码](Select%E6%93%8D%E4%BD%9C%E5%8C%85%E6%8B%AC7%E7%A7%8D%E5%BD%A2%E5%BC%8F.assets/copycode.gif)](javascript:void(0);)

```csharp
            class Student
            {
               public string Name { get; set; }
               public int Score { get; set; }
            }
            List<Student> students = new List<Student>{
               new Student {Name="Terry", Score=50},
               new Student {Name="AI", Score=80},
               new Student {Name="AI", Score=70},
            };
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

### **1.简单用法**

说明:当以select结尾时表示的只是一个声明或者一个描述，并没有真正把数据取出来，只有当你需要该数据的时候，它才会执行这个语句，这就是延迟加载(deferred loading)。

查询学生的姓名：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```csharp
            var query = from student in students
                        select student.Name;
            foreach (var student in query)
            {
                Console.WriteLine("{0}", student);
                //Terry
                //AI
                //AI
            }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

### **2.匿名类型形式**

说明：其实质是编译器根据我们自定义产生一个匿名的类来帮助我们实现临时变量的储存。例如 var ob = new {Name = "Harry"}，编译器自动产生一个有property叫做Name的匿名类，然后按这个类型分配内存，并初始化对象。

查询学生的姓名：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```csharp
            var query = from student in students
                        select new
                        {
                            newName = "学生姓名：" + student.Name
                        };
            foreach (var student in query)
            {
                Console.WriteLine(student.newName);
                //学生姓名：Terry
                //学生姓名：AI
                //学生姓名：AI
            }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

### **3.条件形式**

说明：三元运算，类似于SQL语句case when condition then else的用法。

查询学生的分数等级：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```csharp
            var query = from student in students
                        select new
                        {
                            student.Name,
                            level = student.Score < 60 ? "不及格" : "合格"
                        };
            foreach (var student in query)
            {
                Console.WriteLine("{0}:{1}", student.Name, student.level);
                //Terry:不及格
                //AI:及格
                //AI:及格
            }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

### **4.筛选形式**

说明：结合where用起到过滤的作用。

查询Terry的分数：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
            var query = from student in students
                        where student.Name == "Terry"
                        select student;
            foreach (var student in query)
            {
                Console.WriteLine("{0}:{1}",student.Name,student.Score);
                //Terry:50
            }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

### **5.嵌套类型形式**

说明：如果一个数据源里面又包含了一个或多个集合列表，那么应该使用复合的select子句来进行查询。

查询大于80分的学生分数：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
           class Student
           {
              public string Name { get; set; }
              public List<int> Scores { get; set; }
           }
            List<Student> students = new List<Student>{
               new Student {Name="Terry", Scores=new List<int> {97, 72, 81, 60}},
               new Student {Name="AI", Scores=new List<int> {75, 84, 91, 39}},
               new Student {Name="Wade", Scores=new List<int> {88, 94, 65, 85}},
               new Student {Name="Tracy", Scores=new List<int>{97, 89, 85, 82}},
               new Student {Name="Kobe", Scores=new List<int> {35, 72, 91, 70}} 
            };
            var query = from student in students
                        select new
                        {
                            student.Name,
                            //生成新的集合对象
                            highScore=from sc in student.Scores
                            where sc>80
                            select sc
                        };
            foreach (var student in query)
            {
                Console.Write("{0}:",student.Name);
                foreach (var scores in student.highScore)
                {
                    Console.Write("{0},",scores);
                }
                Console.WriteLine();
                //Terry:97,81,
                //AI:84,91,
                //Wade:88,94,85,
                //Tracy:97,89,85,82,
                //Kobe:91,
            }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

### **6.本地方法调用形式**

说明：调用自定义方法。

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
            var query = from student in students
                        select new
                        {
                            student.Name,
                            //调用GetLevel方法
                            level = GetLevel(student.Score)
                        };
            foreach (var student in query)
            {
                Console.WriteLine("{0}:{1}", student.Name, student.level);
                //Terry:不及格
                //AI:及格
                //AI:及格
            }

            protected static string GetLevel(int score)
            {
                if (score > 60)
                {
                   return "及格";
                }
                else
                {
                   return "不及格";
                }
             }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**7.Distinct形式**

说明：用于查询不重复的结果集。类似于SQL语句SELECT DISTINCT 。

查询不重复的学生姓名：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
            var query = (from student in students
                         select student.Name).Distinct();
            foreach (var student in query)
            {
                Console.WriteLine("{0}", student);
                //Terry:
                //AI
            }
```