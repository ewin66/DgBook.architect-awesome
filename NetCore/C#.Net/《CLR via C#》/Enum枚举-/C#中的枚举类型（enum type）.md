



#  [C#中的枚举类型（enum type）](https://www.cnblogs.com/ylbtech/p/3149964.html) 



 C#中的枚举类型（enum type）



 

 　　枚举类型（enum type）是具有一组命名常量的独特的值类型。在以下示例中：

```
enum Color
{ 
    Red,
    Green,
    Blue
}
```

　　声明一个名为 Color 的枚举类型，该类型具有三个成员：Red、Green 和 Blue。

　　枚举具体是怎么声明呢？枚举声明用于声明新的枚举类型。枚举声明以关键字 enum 开始，然后定义该枚举类型的名称、可访问性、基础类型和成员。具体格式：

　　修饰词（new、public、protected、internal、private）enum 枚举类型名:整数类型

```
{ 
    enum-member-declarations,
    enum-member-declaration
}
```



 　

　　枚举类型一般用于列出唯一的元素，如一周的各天、国家/地区名称，等等。下面的示例代码声明并使用一个名为 Color 的枚举类型，该枚举类型有三个常数值 Red、Green 和 Blue。

[![复制代码](copycode-1570026265438.gif)](javascript:void(0);)

```csharp
using System;

namespace ConsoleApplication1
{
    enum Color
    { 
        Red,
        Green,
        Blue
    }
    class EnumTypeExample
    {
        static void PrintColor(Color color)
        {
            switch (color)
            { 
                case Color.Red:
                    Console.WriteLine("Red");
                    break;
                case Color.Green:
                    Console.WriteLine("Green");
                    break;
                case Color.Blue:
                    Console.WriteLine("Blue");
                    break;
                default:
                    Console.WriteLine("Unknown color");
                    break;
            }
        }
        static void Main(string[] args)
        {
            Color c = Color.Red;
            PrintColor(c);
            PrintColor(Color.Blue);
        }
    }
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

//Execation Result

```
Red
Blue
请按任意键继续. . .
```

 1.B.2, 一个枚举的关联值或隐式地、或显示地被赋值。如果枚举成员的声明中具有 constant-expression  初始值设定项，则该常量表达式的值（它隐式转换为枚举的基础类型）就是该枚举成员的关联值。如果枚举成员的声明不具有初始值设定项，则它的关联值按下面规则隐式地设置：

　　如果枚举成员是在枚举类型中声明的第一个枚举成员，则它的关联值为零。否则，枚举成员的关联值是通过将前一个枚举成员（按照文本顺序）的关联值加  1 得到的。这样增加后的值必须在该基础类型可表示的值范围内；否则，会出现编译时错误。有关枚举类型成员关联值赋值案例，参考下例：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```csharp
using System;

namespace Test
{
    public enum Day : uint  //如果不设置数据类型，默认为 int
    { Mon=1,Tue=2,Wed=3,Thu=4,Fri=5,Sat,Sun}
    class EnumType
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(ChooseDay(Day.Sun));
            Console.WriteLine(ChooseDay(Day.Mon));
        }
        static string ChooseDay(Day d)
        {
            string tmp = string.Empty;
            switch (d)
            { 
                case Day.Mon:
                    tmp = string.Format("你选择一周中的第{0}天（即周一），工作",(uint)d);
                    break;
                case Day.Tue:
                    tmp = string.Format("你选择一周中的第{0}天（即周二），工作", (uint)d);
                    break;
                case Day.Wed:
                    tmp = string.Format("你选择一周中的第{0}天（即周三），工作", (uint)d);
                    break;
                case Day.Thu:
                    tmp = string.Format("你选择一周中的第{0}天（即周四），工作", (uint)d);
                    break;
                case Day.Fri:
                    tmp = string.Format("你选择一周中的第{0}天（即周五），工作", (uint)d);
                    break;
                case Day.Sat:
                    tmp = string.Format("你选择一周中的第{0}天（即周六），休息", (uint)d);
                    break;
                case Day.Sun:
                    tmp = string.Format("你选择一周中的第{0}天（即周日），休息", (uint)d);
                    break;
                default:
                    tmp = "不合法";
                    break;
            }
            return tmp;
        }
    }
}
```

[![复制代码](copycode-1570026265438.gif)](javascript:void(0);)

//Execation Result

```
你选择一周中的第7天（即周日），休息
你选择一周中的第1天（即周一），工作
请按任意键继续. . .
```