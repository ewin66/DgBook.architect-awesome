# [LINQ学习（三）：Where子句](https://www.cnblogs.com/ForEvErNoME/archive/2012/07/23/2605630.html)



**说明：**与SQL命令中的Where作用相似，都是起到范围限定也就是过滤作用的 ，而判断条件就是它后面所接的子句。

**MSDN解释：**where 子句用于查询表达式中，用于指定将在查询表达式中返回数据源中的哪些元素。 它将一个布尔条件（"谓词"）应用于每个源元素（由范围变量引用），并返回满足指定条件的元素。 一个查询表达式可以包含多个where 子句，一个子句可以包含多个谓词子表达式。

 

Where操作包括3种形式，分别为简单形式、关系条件形式、First()形式。下面分别用实例举例下：

**Student.cs**

[![复制代码](LINQ%E5%AD%A6%E4%B9%A0%EF%BC%88%E4%B8%89%EF%BC%89%EF%BC%9AWhere%E5%AD%90%E5%8F%A5.assets/copycode.gif)](javascript:void(0);)

```
    /// <summary>
    /// 学生类
    /// </summary>
    public class Student
    {
        private int _numid;       
        private string _name;        
        private string _sexy;
        private int _scroe;

        /// <summary>
        /// 学号
        /// </summary>
        public int NumId
        {
            get { return _numid; }
            set { _numid = value; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sexy
        {
            get { return _sexy; }
            set { _sexy = value; }
        }
        /// <summary>
        /// 分数
        /// </summary>
        public int Scroe
        {
            get { return _scroe; }
            set { _scroe = value; }
        }
    }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

**测试数据**

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
List<Student> students = new List<Student>() { 
                new Student(){NumId=1,Name="Terry",Sexy="男",Scroe=92},
                new Student(){NumId=2,Name="AI",Sexy="男",Scroe=85},
                new Student(){NumId=3,Name="Wade",Sexy="男",Scroe=78},
                new Student(){NumId=4,Name="Tracy",Sexy="女",Scroe=60},
                new Student(){NumId=5,Name="Kobe",Sexy="女",Scroe=50},
            };
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**1.简单形式**

查询姓名为Terry的学生信息：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
            var query = from student in students
                        where student.Name=="Terry"
                        select student;
            foreach (var student in query)
            {
                Console.WriteLine("学号：{0}，姓名：{1}，性别：{2}，分数：{3}",student.NumId,student.Name,student.Sexy,student.Scroe);
                //学号：1，姓名：Terry，性别：男，分数：92
            }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

查询分数大于80的学生姓名：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
            var query = from student in students
                        where student.Scroe > 80
                        select student.Name;
            foreach (var student in query)
            {
                Console.WriteLine("姓名：{0}", student);
                //姓名：Terry
                //姓名：AI
            }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**2.关系条件形式**

说明："与"--"&"和"或"--"&"运算

查询性别为女性并且分数大于等于60的学生信息：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
            var query = from student in students
                        where student.Sexy == "女" && student.Scroe >= 60
                        select student;
            foreach (var student in query)
            {
                Console.WriteLine("学号：{0}，姓名：{1}，性别：{2}，分数：{3}", student.NumId, student.Name, student.Sexy, student.Scroe);
                //学号：4，姓名：Tracy，性别：女，分数：60
            }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 查询学号为01或者分数小于60分的学生信息：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
            var query = from student in students
                        where student.NumId == 1 || student.Scroe < 60
                        select student;
            foreach (var student in query)
            {
                Console.WriteLine("学号：{0}，姓名：{1}，性别：{2}，分数：{3}", student.NumId, student.Name, student.Sexy, student.Scroe);
                //学号：1，姓名：Terry，性别：男，分数：92
                //学号：5，姓名：Kobe，性别：女，分数：50
            }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**3.First()形式**

说明：返回集合中的一个元素，其实质就是在SQL语句中加TOP(1)，同样的Last()形式也差不多

查询集合中的第一个学生信息：

```
            var student = students.First();
            Console.WriteLine("学号：{0}，姓名：{1}，性别：{2}，分数：{3}", student.NumId, student.Name, student.Sexy, student.Scroe);
            //学号：1，姓名：Terry，性别：男，分数：92
```

查询小于80分的集合中的第一个学生信息：

```
            var student = students.First(s => s.Scroe < 80);
            Console.WriteLine("学号：{0}，姓名：{1}，性别：{2}，分数：{3}", student.NumId, student.Name, student.Sexy, student.Scroe);
            //学号：3，姓名：Wade，性别：男，分数：78
```

查询小于80分的集合中的最后一个学生信息：

```
            var student = students.Last(s => s.Scroe < 80);
            Console.WriteLine("学号：{0}，姓名：{1}，性别：{2}，分数：{3}", student.NumId, student.Name, student.Sexy, student.Scroe);
            //学号：5，姓名：Kobe，性别：女，分数：50
```

 

 

​    分类:             [LINQ](https://www.cnblogs.com/ForEvErNoME/category/382950.html)



