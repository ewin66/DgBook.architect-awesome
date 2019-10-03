







#                   [C#枚举的简单使用](https://www.cnblogs.com/xiongze520/p/10271350.html)              



枚举这个名词大家都听过，很多小伙伴也使用过，

那么枚举在开发中能做什么，使用它后能给程序代码带来什么改变，为什么用枚举。

各位看官且坐下，听我一一道来。

------

 

**为什么使用枚举？**

1、枚举能够使代码更加清晰，它允许使用描述性的名称表示整数值。

2、枚举使代码更易于维护，有助于确保给变量指定合法的、期望的值。

3、枚举使代码更易输入和读取。

------

 

**枚举有哪些用法？**

1、简单枚举

2、标志枚举

------

 

**1、简单枚举**

- 枚举使用enum关键字来声明，与类同级。枚举本身可以有修饰符，但枚举的成员始终是公共的，不能有访问修饰符。枚举本身的修饰符仅能使用public和internal。
- 枚举是值类型，隐式继承自System.Enum，不能手动修改。System.Enum本身是引用类型，继承自System.ValueType。
- 枚举都是隐式密封的，不允许作为基类派生子类。
- 枚举类型的枚举成员均为静态，且默认为Int32类型。
- 每个枚举成员均具有相关联的常数值。此值的类型就是枚举的底层数据类型。每个枚举成员的常数值必须在该枚举的底层数据类型的范围之内。如果没有明确指定底层数据类型则默认的数据类型是int类型。
- 枚举成员不能相同，但枚举的值可以相同。
- 枚举最后一个成员的逗号和大括号后面的分号可以省略

　　C#提供类一个类来方便操作枚举，下面给出这个类的常用方法：

| 方法              | 名称                                                         |
| ----------------- | ------------------------------------------------------------ |
| CompareTo         | 将此实例与指定对象进行比较并返回一个对二者的相对值的指示     |
| Equals            | 指示此实例是否等于指定的对象                                 |
| Format            | 根据指定格式将指定枚举类型的指定值转换为其等效的字符串表示形式 |
| GetName           | 在指定枚举中检索具有指定值的常数的名称                       |
| GetNames          | 检索指定枚举中常数名称的数组                                 |
| GetNames          | 检索指定枚举中常数名称的数组                                 |
| GetTypeCode       | 返回此实例的基础 TypeCode                                    |
| GetUnderlyingType | 返回指定枚举的基础类型                                       |
| GetValues         | 索指定枚举中常数值的数组                                     |
| HasFlag           | 确定当前实例中是否设置了一个或多个位域                       |
| IsDefined         | 返回指定枚举中是否存在具有指定值的常数的指示                 |
| Parse             | 将一个或多个枚举常数的名称或数字值的字符串表示转换成等效的枚举对象。 一个参数指定该操作是否不区分大小写 |
| TryParse          | 将一个或多个枚举常数的名称或数字值的字符串表示转换成等效的枚举对象。 用于指示转换是否成功的返回值 |

 

 

 

 

 

 

 

 

 

 

 

 

 

 

 

 

 

 

 

 

 

 

 

 

 

 

 

如果要显示指定枚举的底层数据类型很简单，只需在声明枚举的时候加个冒号，后面紧跟要指定的数据类型（可指定类型有：byte、sbyte、short、ushort、int、uint、long、ulong）。

显式设置枚举的成员常量值，默认是从0开始，逐个递增的。但是以下例子却设置成了1,2,3,0。而且成员值可以一样的。

如下示例：由枚举值获取枚举名称与由枚举名称获取枚举值

[![复制代码](copycode-1570026197228.gif)](javascript:void(0);)

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enumApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //输出方式一：
            Console.WriteLine("我是四大名著之一的：" + Enum.GetName(typeof(Man), 1));  //西游记（是哪个名著由值获取）

            //输出方式二：
            string[] array1 = Enum.GetNames(typeof(Man));
            Console.WriteLine("我是四大名著之一的：" + array1[2]);   //红楼梦（是哪个名著由值获取）

            //输出方式三：
            Array array2 = Enum.GetValues(typeof(Man));
            Console.WriteLine("我是四大名著之一的：" + array2.GetValue(3));  //三国演义（是哪个名著由值获取）

            //输出方式四：
            Type t = Enum.GetUnderlyingType(typeof(Man));
            Console.WriteLine("我输出的是值类型："+t);       //输出 Int32

            //输出方式五：由值获取内容
            int i = 0;
            string Name = Enum.Parse(typeof(Man), i.ToString()).ToString();     //此时 Name="水浒传"
            Console.WriteLine("我是四大名著之一的：" + Name);

            //输出方式六：由值获取内容
            string Name2 = "红楼梦";
            int j = Convert.ToInt32(Enum.Parse(typeof(Man), Name2));     //此时 j=2
            Console.WriteLine("我是《红楼梦》对应的值序号："+j);

            Console.ReadKey();
        }
        enum Man:int //四大名著枚举
        {
            西游记 = 1,
            红楼梦 = 2,
            三国演义 = 3,
            水浒传 = 0
        }
    }
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**2、标志枚举**

标志枚举要在顶部加[System.Flags]特性进行声明。而且枚举支持组合运算。

这种位运算是非常有用的，在sql语句里也支持位运算。也就是说，把一个枚举运算后的结果存入数据库之后，还能够按照你的要求读取出来。比如：

将一个"高帅"存如数据库的值存入数据库，那么存入去的就是整型5。

```
select * from Table where Column & 1 = 1　　//Column 是列名

select * from Table1 where  Column  | 1 = Column 
```

如下demo：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enumApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var man = People.高 | People.帅;  //赋值为101    计算方法001或上100,结果是101
            Console.WriteLine("高和帅的枚举值和为："+(int)man);
            if ((man & People.高) == People.高)       //101 man 
            {                                    //001 高 逐位相与，
                Console.WriteLine("此人：高");      //001 如果结果是高，就可以反推出man包含 高
            }
            else
            {
                Console.WriteLine("此人：矮");
            }
            Console.ReadKey();
        }

       [System.Flags]
       public enum People:int 
        {
            高 = 1,  //001
            富 = 2,  //010
            帅 = 4,  //100
        }
    }
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

**3、枚举使用的一些建议**

- 作为参数、返回值、变量等类型可以使枚举，尽量使用枚举(要注意考虑分类的稳定性)
- 大多数情况下都可以使用int类型枚举，下列情况除外。
- 枚举可能被大量频繁的使用，这时为了节约空间可以使用小于int类型的枚举。
- 标志枚举，且标志多于32个。

 

枚举用好了还是蛮方便的，大家不妨去试试。