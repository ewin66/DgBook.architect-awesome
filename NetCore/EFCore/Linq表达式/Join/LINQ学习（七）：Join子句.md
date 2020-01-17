# [LINQ学习（七）：Join子句](https://www.cnblogs.com/ForEvErNoME/archive/2012/07/27/2611281.html)



说明：在关系型数据库中，对于多个表的操作可以使用Join进行内联接、外联接和交叉联接等。同样的在LINQ查询语法中，Join子句在多表操作中也是十分重要的，使用 join 子句可以将来自不同源序列并且在对象模型中没有直接关系的元素相关联。join 子句执行同等联接，使用 equals 关键字而不是 == 运算符。

 

Join子句操作包括3种形式，分别为内部联接，分组联接，左外部联接。下面分别用实例举例下：

**测试数据：**

[![复制代码](LINQ%E5%AD%A6%E4%B9%A0%EF%BC%88%E4%B8%83%EF%BC%89%EF%BC%9AJoin%E5%AD%90%E5%8F%A5.assets/copycode.gif)](javascript:void(0);)

```
        /// <summary>
        /// 学生表
        /// </summary>
        class Student
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        /// <summary>
        /// 课程表
        /// </summary>
        class Course
        {
            public string CourseId { get; set; }
            public string CourseName { get; set; }
        }

        List<Student> students = new List<Student> { 
                new Student(){Id="C03",Name="Terry"},
                new Student(){Id="C03",Name="James"},
                new Student(){Id="C01",Name="Kobe"},
                new Student(){Id="C02",Name="AI"},
                new Student(){Id="C01",Name="Wade"},
                new Student(){Id="C05",Name="Kelly"},
            };
        List<Course> cours = new List<Course> { 
                new Course(){CourseId="C01",CourseName="C#课程设计"},
                new Course(){CourseId="C02",CourseName="Java深入"},
                new Course(){CourseId="C03",CourseName="PHP应用开发"},
                new Course(){CourseId="C04",CourseName="IOS案例大全"}
            };
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**1.内部联接**

说明："内部联接"产生一个结果集，对于该结果集内第一个集合中的每个元素，只要在第二个集合中存在一个匹配元素，该元素就会出现一次。 如果第一个集合中的某个元素没有匹配元素，则它不会出现在结果集内。

查询学生的选课信息：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
            var query = from student in students
                        join cour in cours on student.Id equals cour.CourseId
                        select new
                        {
                            Id = student.Id,
                            Name = student.Name,
                            CourName = cour.CourseName
                        };
            foreach (var student in query)
            {
                Console.WriteLine("{0} {1} {2}", student.Id, student.Name, student.CourName);
                //C03 Terry PHP应用开发
                //C03 James PHP应用开发
                //C01 Kobe C#课程设计
                //C02 AI Java深入
                //C01 Wade C#课程设计
                //注意：C05没有匹配，所以不会出现在结果集中
            }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**2.分组联接**

说明：含有 into 表达式的 join 子句称为分组联接。分组联接本质上是一个对象数组序列，结果序列会组织为多个组形式数据进行返回就是会产生一个分层的结果序列。通俗点讲此序列第一个集合中的每个元素与第二个集合中的一组相关元素进行配对，如果找不到就返回空数组。我的理解是分组联接和内部联接基本差不多。

查询学生的选课信息：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
            var query = from student in students
                        join cour in cours on student.Id equals cour.CourseId into courseGroup
                        select new
                        {
                            Id = student.Id,
                            Name = student.Name,
                            CourInfo = courseGroup
                        };
            foreach (var student in query)
            {
                Console.Write("{0} {1} ",student.Id,student.Name);
                foreach (var cour in student.CourInfo)
                {
                    Console.Write("{0}", cour.CourseName);
                }
                Console.WriteLine();
                //C03 Terry PHP应用开发
                //C03 James PHP应用开发
                //C01 Kobe C#课程设计
                //C02 AI Java深入
                //C01 Wade C#课程设计
                //C05 Kelly
                //注意：C05没有匹配，但是返回空的数组
            }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**3.左外部联接**

说明：在左外部联接中，将返回左侧源序列中的所有元素，即使它们在右侧序列中没有匹配的元素也是如此。 可以使用 LINQ，通过对分组联接的结果调用 DefaultIfEmpty 来执行左外部联接。

查询学生的选课信息：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
            var query = from student in students
                        join cour in cours on student.Id equals cour.CourseId into courseGroup
                        from stucour in courseGroup.DefaultIfEmpty()
                        select new
                        {
                            Id=student.Id,
                            Name = student.Name,
                            Cour = courseGroup
                        };
            foreach (var student in query)
            {
                Console.Write("{0} {1} ", student.Id, student.Name);
                foreach (var cour in student.Cour)
                {
                    Console.Write("{0}",cour.CourseName);
                }
                Console.WriteLine();
                //C03 TerryC03 PHP应用开发
                //C03 JamesC03 PHP应用开发
                //C01 KobeC01 C#课程设计
                //C02 AIC02 Java深入
                //C01 WadeC01 C#课程设计
                //C05 Kelly                //注意：返回左侧源序列中的所有元素
            }
```

[![复制代码](LINQ%E5%AD%A6%E4%B9%A0%EF%BC%88%E4%B8%83%EF%BC%89%EF%BC%9AJoin%E5%AD%90%E5%8F%A5.assets/copycode.gif)](javascript:void(0);)



