





#  [C#枚举（Enum）小结](https://www.cnblogs.com/li-learning/p/CSharp_Enum.html) 

**枚举概念**

枚举类型（也称为枚举）提供了一种有效的方式来定义可能分配给变量的一组已命名整数常量。该类型使用enum关键字声明。

示例代码1

```
`enum` `Day { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday };`
```

默认情况下枚举中每个元素的基本类型都是int。可以使用冒号指定另一种整数类型。

示例代码2

```
`enum` `Day : ``byte` `{Sat=1, Sun, Mon, Tue, Wed, Thu, Fri};`
```

默认情况下，第一个枚举值具有值0，并且每个连续枚举数的值将增加1。

枚举数可以使用初始值设定项来替代默认值。

若设置某一枚举数的值，之后的枚举数仍然按1递增。

示例代码3

```
`enum` `Day {Sat=1, Sun, Mon, Tue=5, Wed, Thu, Fri};`
```

每个枚举都有一个基础类型，该基础类型可以是除char外的任何整数类型，枚举元素的默认基础类型是int。

已批准的枚举类型有byte、sbyte、short、ushort、int、uint、long或ulong。

可以为枚举类型的枚举器列表中的元素分配任何值，也可以使用计算值。

示例代码4

```csharp
`enum` `MachineState``{``    ``PowerOff = 0,``    ``Running = 5,``    ``Sleeping = 10,``    ``Hibernating = Sleeping + 5``}`
```

**枚举方法**

获取名称

public static string GetName(Type enumType, object value);

示例代码5

```
`//获取Day枚举中Day.Friday的名称，返回值为"Friday"``System.Enum.GetName(``typeof``(Day), Day.Friday)`
```

获取名称数组

public static string[] GetNames(Type enumType);

示例代码6

```
`string``[] names = System.Enum.GetNames(``typeof``(Day));`
```

获取实例值得数组

public static Array GetValues(Type enumType);

实例代码7

```
`Array arr = System.Enum.GetValues(``typeof``(Day));`
```

将枚举常数的名称或数值的字符串表示转换成等效的枚举对象

public static object Parse(Type enumType, string value);

示例代码8

```
`string` `day = Day.Friday.ToString();``var` `fri = (Day)System.Enum.Parse(``typeof``(Day),day);`
```

相关常用方法可以参考枚举基类 System.Enum

**作为位标志的枚举类型**

可以使用枚举类型来定义位标志，这使枚举类型的实例能够存储枚举器列表中定义的值的任何组合。 （当然，某些组合在你的程序代码中可能没有意义或不允许使用。）

创建位标志枚举的方法是，应用 [System.FlagsAttribute](https://docs.microsoft.com/zh-cn/dotnet/api/system.flagsattribute) 属性并适当定义一些值，以便可以对这些值执行 `AND`、`OR`、`NOT` 和 `XOR` 按位运算。 在位标志枚举中，包括一个值为零（表示“未设置任何标志”）的命名常量。 如果零值不表示“未设置任何标志”，请勿为标志指定零值。

示例代码9

```csharp
`[Flags]``enum` `Days``{``    ``None = 0x0,``    ``Sunday = 0x1,``    ``Monday = 0x2,``    ``Tuesday = 0x4,``    ``Wednesday = 0x8,``    ``Thursday = 0x10,``    ``Friday = 0x20,``    ``Saturday = 0x40``}`
```

枚举的位运算

|并集  &交集  ^差集  ~取反

**为枚举拓展新方法**

示例代码10

 

```csharp
`[Display(Name = ``"一周"``)]``public` `enum` `Day``{``　　[Display(Name = ``"星期天"``)]``　　Sunday,``　　[Display(Name = ``"星期一"``)]``　　Monday,``　　[Display(Name = ``"星期二"``)]``　　Tuesday,``　　[Display(Name = ``"星期三"``)]``　　Wednesday,``　　[Display(Name = ``"星期四"``)]``　　Thursday,``　　[Display(Name = ``"星期五"``)]``　　Friday,``　　[Display(Name = ``"星期六"``)]``　　Saturday``}``/// <summary>``/// 枚举拓展类``/// </summary>``public` `static` `class` `EnumExtend``{``　　``/// <summary>``　　``/// 根据System.ComponentModel.DataAnnotations下的DisplayAttribute特性获取显示文本``　　``/// </summary>``　　``/// <param name="t"></param>``　　``/// <returns></returns>``　　``public` `static` `string` `GetDisplayText(``this` `Enum t)``　　{``　　　　``var` `t_type = t.GetType();``　　　　``var` `fieldName = Enum.GetName(t_type, t);``　　　　``var` `objs = t_type.GetField(fieldName).GetCustomAttributes(``typeof``(DisplayAttribute), ``false``);``　　　　``return` `objs.Length > 0 ? ((DisplayAttribute)objs[0]).Name : ``null``;``　　}``}`
```

 

　　

参考文献：[枚举类型（C#编程指南）](https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/enumeration-types)



​     分类:              [C#语言](https://www.cnblogs.com/li-learning/category/1432407.html)

​     标签:              [C#](https://www.cnblogs.com/li-learning/tag/C%23/),             [Enum](https://www.cnblogs.com/li-learning/tag/Enum/),             [枚举](https://www.cnblogs.com/li-learning/tag/枚举/)