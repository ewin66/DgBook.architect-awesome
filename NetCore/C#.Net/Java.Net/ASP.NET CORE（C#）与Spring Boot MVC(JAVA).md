



​          [ASP.NET CORE（C#）与Spring Boot MVC(JAVA)](https://www.cnblogs.com/Leo_wl/p/11306099.html)           	



> **阅读目录**
>
> - [一、一、匿名类](https://www.cnblogs.com/Leo_wl/p/11306099.html#_label0)
> - [二、二、类型初始化](https://www.cnblogs.com/Leo_wl/p/11306099.html#_label1)
> - [三、三、委托（方法引用）](https://www.cnblogs.com/Leo_wl/p/11306099.html#_label2)
> - [四、四、Lambda表达式](https://www.cnblogs.com/Leo_wl/p/11306099.html#_label3)
> - [五、五、泛型](https://www.cnblogs.com/Leo_wl/p/11306099.html#_label4)
> - [六、六、自动释放](https://www.cnblogs.com/Leo_wl/p/11306099.html#_label5)
> - [七、七、重写（override）](https://www.cnblogs.com/Leo_wl/p/11306099.html#_label6)
> - [八、一、引用依赖（包）](https://www.cnblogs.com/Leo_wl/p/11306099.html#_label7)
> - [九、二、依赖注入 DI （IOC容器）](https://www.cnblogs.com/Leo_wl/p/11306099.html#_label8)
> - [十、三、过滤器、拦截器 AOP](https://www.cnblogs.com/Leo_wl/p/11306099.html#_label9)
> - [undefined、四、配置读取](https://www.cnblogs.com/Leo_wl/p/11306099.html#_label10)
> - [undefined、五、发布、部署、运行](https://www.cnblogs.com/Leo_wl/p/11306099.html#_label11)



**阅读目录**

- [干货分享：ASP.NET CORE（C#）与Spring Boot MVC(JAVA)异曲同工的编程方式总结](https://www.cnblogs.com/Leo_wl/p/11306099.html#_label0)

[回到目录](https://www.cnblogs.com/Leo_wl/p/11306099.html#_labelTop)

# [干货分享：ASP.NET CORE（C#）与Spring Boot MVC(JAVA)异曲同工的编程方式总结](https://www.cnblogs.com/zuowj/p/11181726.html)

 

目录

- C# VS JAVA 基础语法类比篇：
  - [一、匿名类](https://www.cnblogs.com/zuowj/p/11181726.html#一匿名类)
  - [二、类型初始化](https://www.cnblogs.com/zuowj/p/11181726.html#二类型初始化)
  - [三、委托（方法引用）](https://www.cnblogs.com/zuowj/p/11181726.html#三委托方法引用)
  - [四、Lambda表达式](https://www.cnblogs.com/zuowj/p/11181726.html#四lambda表达式)
  - [五、泛型](https://www.cnblogs.com/zuowj/p/11181726.html#五泛型)
  - [六、自动释放](https://www.cnblogs.com/zuowj/p/11181726.html#六自动释放)
  - [七、重写（override）](https://www.cnblogs.com/zuowj/p/11181726.html#七重写override)
- ASP.NET CORE VS Spring Boot 框架部署类比篇：
  - [一、引用依赖（包）](https://www.cnblogs.com/zuowj/p/11181726.html#一引用依赖包)
  - [二、依赖注入 DI （IOC容器）](https://www.cnblogs.com/zuowj/p/11181726.html#二依赖注入-di-ioc容器)
  - [三、过滤器、拦截器 AOP](https://www.cnblogs.com/zuowj/p/11181726.html#三过滤器拦截器-aop)
  - [四、配置读取](https://www.cnblogs.com/zuowj/p/11181726.html#四配置读取)
  - [五、发布、部署、运行](https://www.cnblogs.com/zuowj/p/11181726.html#五发布部署运行)

> 我(梦在旅途，http://zuowj.cnblogs.com; http://www.zuowenjun.cn)最近发表的一篇文章《[.NET CORE与Spring Boot编写控制台程序应有的优雅姿势](https://www.cnblogs.com/zuowj/p/11107243.html)》看到都上48小时阅读排行榜（当然之前发表的文章也有哦！），说明关注.NET  CORE及Spring Boot的人很多，也是目前的主流方向，于是我便决定系统性的总结一下C# 与JAVA 、ASP.NET CORE 与  Spring Boot MVC，让更多的人了解它们，消除之前可能存在的对.NET或JAVA的误解。

本文目的是通过全面简述C# 与JAVA 在基础语法以及ASP.NET CORE 与 Spring Boot  MVC的在框架规范、部署、运行的异曲同工的实现方式，让大家更多的了解C#与JAVA，本文不会刻意说哪门语言好，我认为这是没有意义的，更多的是了解每种语言的特点、优点以及不同语言的共性，掌握编程内功（如：面向对象、DI、AOP、设计模式、算法），这样才能让自己更能适应社会及未来的变化。

本文主要以示例代码为主，辅以简单文字说明，不会细讲每个语法点，只会体现不同的实现方式而矣，全文无废话，全是干货，慢慢欣赏吧。

*（注：本文内容是使用Markdown编辑器进行编辑完成！）*

## C# VS JAVA 基础语法类比篇：

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11306099.html#_labelTop)

### 一、匿名类

###### C#（直接new{}，在{}中直接定义只读公开属性或委托方法，无需预先定义任何接口或类）

```csharp
            #region 1.匿名类
            var helloWord = new
            {
                CodeBy = "C#匿名类",
                Output = new Action<string, string>((name, codeBy) =>
                {
                    System.Console.WriteLine($"Welcome:{name},Hello Word!  by {codeBy}");
                }) 
            };

            helloWord.Output("梦在旅途", helloWord.CodeBy);
            #endregion
```

###### JAVA（需要先定义接口或类，然后 new 接口或类的构造函数{}，{}内实现接口方法或重写父类接口）

```csharp
        //1.匿名类
        IHelloWord helloWord=new IHelloWord() {
            @Override
            public void output(String name) {
                System.out.printf("Welcome:%s,Hello Word!  by %s\n",name,getCodeBy());
            }

            @Override
            public String getCodeBy() {
                return "JAVA匿名类";
            }
        };

        helloWord.output("梦在旅途");


public interface IHelloWord {
    void output(String name);
    String getCodeBy();
}
```

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11306099.html#_labelTop)

### 二、类型初始化

###### C#（IList类型（Dictionary、List）直接在new 类型{}，在{}内直接使用{key,value}或{value}方式添加集合元素，其实是隐式调用了add方法）

```csharp
            #region 2.类型初始化

            Dictionary<string, string> map = new Dictionary<string, string>
            {
              { "key1","value1" },//(隐式自动调用add方法)
              { "key2", "value2" },
              { "key3", "value3" }
            };

            foreach (var item in map)
            {
                System.Console.WriteLine($"key:{item.Key},value:{item.Value}");
            }

            List<string> list = new List<string>
            {
                "list-item1",//(隐式自动调用add方法)
                "list-item2",
                "list-item3"
            };

            foreach (string item in list)
            {
                System.Console.WriteLine(item);
            }

            String[] strArr = { "arr1", "arr2", "arr3" };
            foreach (string item in strArr)
            {
                System.Console.WriteLine(item);
            }


            Person person = new Person
            {
                Name = "梦在旅途",
                Age = 23,
                Sex = "男"
            };


            string json = JsonConvert.SerializeObject(person);
            System.Console.WriteLine("Person json：" + json);

            #endregion
```

###### JAVA（new集合类型{}，并在{}内再次使用{}，即{{赋值 }}，在双大括号内进行赋值操作，省略类名，这个特点有点类似VB及VB.NET的with语句，大家有兴趣可以了解一下，数组的初始化与C#相同，都可以直接在定义数组的时候在{}中给定元素）

```java
        //2.类型初始化
        Map<String,String> map=new HashMap(){
            {
                put("key1","value1");
                put("key2","value2");
                put("key3","value3");
            }
        };

        for (Map.Entry<String, String> item:map.entrySet()) {
            System.out.printf("key:%1$s,value:%2$s\n",item.getKey(),item.getValue());
        }

        List<String> list=new ArrayList(){
            {
                add("list-item1");
                add("list-item2");
                add("list-item3");
            }
        };

        for (String item :list) {
            System.out.printf("%s\n",item);
        }



        String[] strArr={"arr1","arr2","arr3"};

        for (String item :strArr) {
            System.out.printf("%s\n",item);
        }


        Person person=new Person(){
            {
                setName("zwj");
                setAge(32);
                setSex("男");
            }
        };

        ObjectMapper jsonMapper=new ObjectMapper();
        String json= jsonMapper.writeValueAsString(person);
        System.out.println("Person json：" + json);
```

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11306099.html#_labelTop)

### 三、委托（方法引用）

###### C#（委托定义使用delegate关键字，后面就跟方法答名定义【不含方法体】，可委托普通方法，静态方法，有很多的现成的预定义委托类型，如：Action<T0...T16>,Func<T0...T16,TOut>各有16个重载）

```csharp
            #region 3.委托
            delegate void HelloDelegate(string name);//定义委托类型(重点是方法签名)

            //常规普通自定义委托类型及委托相应的方法
            HelloWord helloWordObj = new HelloWord();

            HelloDelegate helloDelegate = helloWordObj.Output; //委托实例方法
            helloDelegate.Invoke("梦在旅途");// OR helloDelegate("梦在旅途");

            HelloDelegate helloDelegate2 = HelloWord.OutputForStatic; //委托类的静态方法
            helloDelegate2.Invoke("zuowenjun"); // OR helloDelegate2("zuowenjun");

            //使用通用的已封装好的委托类型（如：Func、Action）并实例化
            Func<int, int, int> multiplyFunc = new Func<int, int, int>(delegate (int a, int b)
            {
                return a * b;
            });

            int x = 12, y = 25;
            int multiplyResult = multiplyFunc.Invoke(x, y); //OR multiplyFunc(x,y);
            System.Console.WriteLine($"{x}乘以{y}等于:{multiplyResult}");

            Action<string> helloAction = new Action<string>(delegate (string name)
            {
                System.Console.WriteLine($"hello,{name},how are you!");
                System.Console.WriteLine("learning keep moving!");
            });
            helloAction.Invoke("www.zuowenjun.cn");

            #endregion
```

###### JAVA（定义委托需要先定义委托类型【即：函数式接口，规则：接口+@FunctionalInterface+一个方法定义】，然后就可以普通方法，静态方法，有很多的现成的预定义委托类型【即：函数式接口】，如：BiFunction，Consumer等）

```java
        //3.委托

        HelloWord helloWordObj = new HelloWord();

        HelloWordDelegate helloWordDelegate = helloWordObj::output;
        helloWordDelegate.invoke("梦在旅途");

        HelloWordDelegate helloWordDelegate2 = HelloWord::outputForStatic;
        helloWordDelegate2.invoke("zuowenjun");


        //使用已封装好的委托方法（JAVA这边称：函数式接口，有很多详见：https://www.runoob.com/java/java8-functional-interfaces.html）
        BiFunction<Integer, Integer, Integer> multiplyFunc = new BiFunction<Integer, Integer, Integer>() {
            @Override
            public Integer apply(Integer i, Integer i2) {
                return i * i2;
            }
        };

        int x = 12, y = 25;
        int multiplyResult = multiplyFunc.apply(x, y);
        System.out.printf("%d乘以%d等于:%d%n", x, y, multiplyResult);


        Consumer<String> helloAction=new Consumer<String>() {
            @Override
            public void accept(String s) {
                System.out.printf("hello,%s,how are you!%n",s);
                System.out.printf("learning keep moving!%n");
            }
        };
        helloAction.accept("www.zuowenjun.cn");


@FunctionalInterface
public interface HelloWordDelegate {
    void  invoke(String name);
}

public class HelloWord implements IHelloWord {

    @Override
    public void output(String name) {
        System.out.printf("Welcome:%s,Hello Word!  by %s\n",name,getCodeBy());
    }

    public  static void outputForStatic(String name){
        System.out.printf("Welcome:%s,Hello Word!  by JAVA static\n",name);
    }

    @Override
    public String getCodeBy() {
        return "JAVA";
    }
}
```

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11306099.html#_labelTop)

### 四、Lambda表达式

###### C#（使用(入参)=>{方法处理体}，与要传入或要实例化的委托方法签名相同即可）

```csharp
            #region 4.Lambda

            Func<int, int, int> multiplyFunc2 = new Func<int, int, int>((a, b) => a * b);

            int x2 = 12, y2 = 25;
            int multiplyResult2 = multiplyFunc2.Invoke(x2, y2); //OR multiplyFunc(x,y);
            System.Console.WriteLine($"{x2}乘以{y2}等于:{multiplyResult2}");

            Action<string> helloAction2 = new Action<string>(name =>
            {
                System.Console.WriteLine($"hello,{name},how are you!");
                System.Console.WriteLine("learning keep moving!");
            });

            helloAction2.Invoke("www.zuowenjun.cn");

            int[] intArr = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            intArr = intArr.Where(i => i >= 5).ToArray();
            foreach (int i in intArr)
            {
                System.Console.WriteLine($"int-{i}");
            }

            string msg = "测试外部变量被Lambda引用";
            Action testMsgAction = () =>
            {
                msg += "--改变内容";
                System.Console.WriteLine("Lambda方法体中的值：" + msg);
            };
            testMsgAction();
            System.Console.WriteLine("原始值：" + msg);

            #endregion
```

###### JAVA（使用(入参)->{方法处理体}，与要传入或要实例化的方法签名相同，且传入或实例化的类型必需是函数式接口【可以理解为自定义的委托类型】,**注意与C#不同，Lambda方法体内不能引用外部非final的变量，与C# Lambda有本质不同**）

```java
        //4.Lambda

        BiFunction<Integer, Integer, Integer> multiplyFunc = (i1, i2) -> i1 * i2;

        int x = 12, y = 25;
        int multiplyResult = multiplyFunc.apply(x, y);
        System.out.printf("%d乘以%d等于:%d%n", x, y, multiplyResult);


        Consumer<String> helloAction= s -> {
            System.out.printf("hello,%s,how are you!%n",s);
            System.out.printf("learning keep moving!%n");
        };
        helloAction.accept("www.zuowenjun.cn");

        int[] intArr = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        intArr= Arrays.stream(intArr).filter(value -> value>=5).toArray();
        for (int n : intArr) {
            System.out.printf("int-%d%n",n);
        }
```

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11306099.html#_labelTop)

### 五、泛型

###### C#（真泛型，不同的泛型类型参数视为不同的类型，有泛型接口，泛型类，泛型方法，泛型委托，泛型约束：in表示逆变【泛型参数父类型转子类型，属于消费者，一般用于入参】，out 表示协变【泛型参数子类型转父类型】，只有委托、接口才支持可变性）

``` cs
            #region 5.泛型

            //常用泛型集合类型
            List<int> intList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            List<long> longList = new List<long> { 1L, 2L, 3L, 4L, 5L, 6L, 7L, 8L, 9L };

            Dictionary<string, string> dic = new Dictionary<string, string> {
                { "k1","v1"},{ "k2","v2"},{ "k3","v3"}
            };

            //泛型方法
            var demo = new DemoGenericClass();
            //demo.DisplayResult("学习永无止境"); 错误，因为约束是值类型
            demo.DisplayResult(ConsoleColor.DarkGreen);

            List<YellowPerson> yellowPersonList = new List<YellowPerson> {
                new YellowPerson(){ Name="zzz",Age=11,Sex="G"},
                new YellowPerson(){ Name="xxx",Age=22,Sex="B"}
            };

            //协变(泛型参数子类转父类)
            //public interface IEnumerable<out T>
            IEnumerable<YellowPerson> yellowPersons = yellowPersonList;
            IEnumerable<Person> persons = yellowPersons;//协变（子类到父类的转变） ,泛型参数 out标记，一般用于出参,这个正确的

            // List<Person> personList = yellowPersonList; 因为List是类，而且泛型参数并没有标记out，不适用协变，故这样转换是错误的

            foreach (var p in persons)
            {
                System.Console.WriteLine($"item :【Name={p.Name},Age={p.Age},Sex={p.Sex},Color={p.Color}】");
            }

            //逆变（泛型参数父类转子类）
            Action<object, object> showPlusResultAction = (d1, d2) => Console.WriteLine($"{d1}+{d2}={d1.ToString() + d2.ToString()}");

            Action<string, string> showStrPlusResultAction = showPlusResultAction;//逆变（父类到子类的转变），泛型参数 in标记，一般用于入参

            showPlusResultAction(55, 66);
            showStrPlusResultAction("你好", "中国");

            ShowMsg<Person> showMsg = new ShowMsg<Person>((p) =>
            {
                System.Console.WriteLine($"ShowMsg :【Name={p.Name},Age={p.Age},Sex={p.Sex},Color={p.Color}】");
            });
            //ShowMsg<HelloWord> showMsg2 = new ShowMsg<HelloWord>(...); 这样是不行的，因为泛型约束为需继承自Person

            showMsg.Invoke(new Person() { Name = "zuowenjun", Age = 33, Sex = "B" });
            showMsg.Invoke(new YellowPerson() { Name = "zuowenjun2", Age = 33, Sex = "B" });

            //综合演示：入参逆变，出参协变
            Func<Person, Person, string> getDataFunc = (x, y) => x.Name + y.Name;
            Func<YellowPerson, YellowPerson, object> getDataFunc2 = getDataFunc;
            object dataResult = getDataFunc2(new YellowPerson() { Name = "张三", Age = 33, Sex = "G" }, new YellowPerson() { Name = "赵六", Age = 33, Sex = "B" });
            System.Console.WriteLine($"getDataFunc2:{dataResult}");

            List<int> a = new List<int>();
            List<String> b = new List<string>();
            bool isEqual = (a.GetType() == b.GetType());
            System.Console.WriteLine($"List<int> 与 List<String> {(isEqual ? "is" : "not")} Equal ");//结果是不相等


            #endregion
   
   //以上示例需要用到的类
   
   public class BaseClass
    {
        /// <summary>
        /// 必需是用virtual标记的方法（即：虚方法）或abstract标记的方法（即：抽象方法）子类才能使用override进行重写
        /// </summary>
        /// <param name="name"></param>
        public virtual void SayHello(string name)
        {
            System.Console.WriteLine($"{nameof(BaseClass)} Say:{name},hello!");
        }
        
    }
   
            
    public class DemoGenericClass : BaseClass, IDisposable
    {
        public void DisplayResult<T>(T arg) where T : struct
        {
            System.Console.WriteLine($"DemoGenericClass.DisplayResult:{arg}");
        }

        public void Dispose()
        {
            System.Console.WriteLine("DemoGenericClass Disposed");
        }

        public override void SayHello(string name)
        {
            base.SayHello(name);
            System.Console.WriteLine($"{nameof(DemoGenericClass)} Say:{name},hello again!");
        }
    }
    

    public class Person
    {
        public virtual Color Color { get; }
        public string Name { get; set; }

        public int Age { get; set; }

        public string Sex { get; set; }

    }

    public class BlackPerson : Person
    {
        public override Color Color => Color.Black;
    }

    public class YellowPerson : Person
    {
        public override Color Color => Color.Yellow;
    }

    public class WhitePerson : Person
    {
        public override Color Color => Color.White;
    }
```

###### JAVA（伪泛型，编译后类型参数擦除，同一个泛型类型不同的泛型参数类型相同，有泛型接口，泛型类，泛型方法，泛型约束：super限定下边界，逆变，用于入参,属于消费者,extends限定上边界，协变，用于出参,属于生产者，还有？通匹符）

```java
      //常用泛型集合
        List<Integer> intList = new ArrayList(){
            {
                add(1);
                add(2);
                add(3);
                add(4);
                add(5);
            }
        };

        Map<String,String> map=new HashMap(){
            {
                put("k1","v1");
                put("k2","v2");
                put("k3","v3");
            }
        };

        //泛型方法
        DemoGenericClass demo=new DemoGenericClass();
        demo.displayResult(new YellowPerson(){{
            setName("zwj");setSex("B");setAge(33);
        }});

        List<Integer> a=new ArrayList<>();
        List<String> b=new ArrayList<>();
        boolean isEqual =(a.getClass()==b.getClass());
        System.out.printf("List<Integer>与List<String> %s Equal %n",isEqual?"is":"not"); //结果是相等，都是同一个List类型，不能使用instanceof判断泛型类型实例

        //协变、逆变（详见说明：https://www.jianshu.com/p/2bf15c5265c5 ，意义与C#相同）
        List<? super Person> persons=new ArrayList<>(); //super：限定下边界，逆变，用于入参
        persons.add(new Person(){
            {
                setName("张三");
                setAge(25);
                setSex("B");
            }
        });

        persons.add(new YellowPerson(){
            {
                setName("赵六");
                setAge(18);
                setSex("G");
            }
        });

       List<? extends Person> result= (List<? extends Person>) persons;//extends:限定上边界，协变，用于出参
       for (Person p:result){
           System.out.printf("Person list item:%s %n",p.toString());
       }

//以上示例需要用到的类
    public class DemoGenericClass implements AutoCloseable
    {
        @Override
        public void close() throws Exception {
            System.out.println("DemoGenericClass closed");
        }

        public <T extends Person> void displayResult(T arg) //泛型约束（泛型参数上边界,协变）
        {
           System.out.printf("DemoGenericClass.DisplayResult:%s %n",arg.toString());
        }
    }
    

public class Person {
    private String name;
    private int age;
    private String sex;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getAge() {
        return age;
    }

    public void setAge(int age) {
        this.age = age;
    }

    public String getSex() {
        return sex;
    }

    public void setSex(String sex) {
        this.sex = sex;
    }

    @Override
    public String toString() {
        return String.format("Person=[Name:%s,Age:%d,Sex:%s] %n", name, age, sex);
    }
}

class YellowPerson extends Person {

    @Override
    public String toString() {
        return "YellowPerson#toString-"+ super.toString();
    }
}
```

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11306099.html#_labelTop)

### 六、自动释放

###### C#（采用using包裹，要实现自动释放必需实现IDisposable接口）

```
            using (var demo2 = new DemoGenericClass()) //DemoGenericClass实现IDisposable接口
            {
                demo2.DisplayResult(123456);
            }
```

###### JAVA（采用try包裹，要实现自动释放必需实现AutoCloseable接口）

```
        try(DemoGenericClass demo=new DemoGenericClass()) {
            demo.displayResult(new YellowPerson(){
                {
                    setName("zuowenjun");
                    setAge(33);
                    setSex("B");
                }
            });
        }
```

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11306099.html#_labelTop)

### 七、重写（override）

###### C#（必需是用virtual标记的方法（即：虚方法）或abstract标记的方法（即：抽象方法）子类才能使用override进行重写，重写后父类的方法将被子类取代，若需在子类中执行父类被重写的方法，应使用base关键字，若父类方法非虚方法或抽象方法但又想“重写”怎么办？则只能使用new覆盖方法，覆盖方法与重写方法的不同之处在于，在父类中仍可以正常执行父类的方法而不会执行子类的覆盖方法，覆盖方法的方法签名、访问修饰符均没有严格限制，即使不相同仍不会报错，但IDE会有提示，如需真正覆盖父类方法，则应按照重写的规范来，只是使用new来修饰覆盖方法，但覆盖方法与重写方法有本质不同，一般情况下更建议使用重写方法）

> C#所有类的普通方法默认是密封方法（类似JAVA的final方法），是不允许被重写，可以理解为默认是不开放的，需要开放重写的方法必需使用virtual标记为虚方法（虚方法至少是protected及以上的访问权限），若重写后不想被后续的子类再次重写，则可以标记为sealed，即：密封方法

```
    public class BaseClass
    {
        /// <summary>
        /// 必需是用virtual标记的方法（即：虚方法）或abstract标记的方法（即：抽象方法）子类才能使用override进行重写
        /// </summary>
        /// <param name="name"></param>
        public virtual void SayHello(string name)
        {
            System.Console.WriteLine($"{nameof(BaseClass)} Say:{name},hello!");
        }
        
    }

    public class DemoGenericClass : BaseClass
    {
        public override void SayHello(string name)
        {
            base.SayHello(name);
            System.Console.WriteLine($"{nameof(DemoGenericClass)} Say:{name},hello again!");
        }
    }
```

###### JAVA（非private  且非 final  修饰的普通方法默认均可在子类中进行重写，重写要求基本与C#相同，只是无需强制Override关键字，但建议仍使用@Override注解，以便IDE进行重写规范检查，重写后父类的方法将被子类取代，若需在子类中执行父类被重写的方法，应使用super关键字）

> JAVA所有类的普通方法默认是虚方法，都是可以被重写，可以理解为默认是开放重写的，若不想被重写则应标记为final ,即：最终方法（C#中称密封方法）

```
    public  class BaseClass{
        public  void  testOutput(String msg){
            System.out.println("output Msg:" + msg);
        }
    }

    public class DemoGenericClass extends BaseClass
    {
        @Override
        public  void  testOutput(String msg){
            super.testOutput(msg);
            System.out.println("output again Msg:" + msg);
        }
    }
```

## ASP.NET CORE VS Spring Boot 框架部署类比篇：

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11306099.html#_labelTop)

### 一、引用依赖（包）

###### C#（编辑csproj文件，可以通过PackageReference引用包、ProjectReference引用同一个解决方案下的其它项目，Reference引用本地DLL组件，csproj除了引用包以外，还可以通过在PropertyGroup元素下配置相关的属性，比如TargetFramework指定SDK框架版本等）

> .NET项目的包是NuGet包，可以从nuget.org上查找浏览所需的包，项目中引用依赖包，除了在csproj文件中使用PackageReference添加编辑外（具体用法参见：[项目文件中的包引用 (PackageReference)](https://docs.microsoft.com/zh-cn/nuget/consume-packages/package-references-in-project-files)）还可以使用package manager控制台使用包管理命令，如：`Install-Package ExcelEasyUtil -Version 1.0.0`，或者直接使用.NET CLI命令行工具，如：`dotnet add package ExcelEasyUtil --version 1.0.0`
>
> .NET有包、元包、框架 之分，详细了解：[包、元包和框架](https://docs.microsoft.com/zh-cn/dotnet/core/packages)

```
  <!--包引用-->
  <ItemGroup>
    <PackageReference Include="Autofac.Extras.DynamicProxy" Version="4.5.0" />
    <PackageReference Include="Autofac" Version="4.9.2" />
    <PackageReference Include="Microsoft.AspNetCore.App" />
    <PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="2.1.1" />
    <PackageReference Include="Autofac.Extensions.DependencyInjection" Version="4.4.0" />
  </ItemGroup>

  <!--同一解方案下的项目引用-->
  <ItemGroup>
    <ProjectReference Include="..\StandardClassLib2019\StandardClassLib2019.csproj"  />
  </ItemGroup>

  <!--本地组件直接引用-->
  <ItemGroup>
    <Reference Include="KYExpress.Common">
      <HintPath>xxxx\xxxx.dll</HintPath>
      <Private>true</Private>
    </Reference>
  </ItemGroup>
```

###### JAVA（编辑POM 文件，通过dependencies.dependency来声明引入多个依赖，根据scope可以指定依赖的有效作用范围）

```
    <dependencies>
        <!--maven包依赖-->
        <dependency>
            <groupId>org.springframework.boot</groupId>
            <artifactId>spring-boot-starter</artifactId>
        </dependency>

        <!--本地JAR包依赖（scope=system,systemPath=jar包存放目录）-->
        <dependency>
            <groupId>cn.zuowenjun.boot.mybatis.plugin</groupId>
            <artifactId>cn.zuowenjun.boot.mybatis.plugin</artifactId>
            <version>1.0</version>
            <scope>system</scope>
            <systemPath>${basedir}/src/main/libs/xxxxx.jar</systemPath>
        </dependency>

        <!--同一父项目Module之间依赖,注意这个必需先创建基于POM的父项目，然后各子Moudle 的POM 的parent指向父项目-->
        <dependency>
            <groupId>cn.zuowenjun.springboot</groupId>
            <artifactId>springboot-demo1</artifactId>
            <version>1.0-SNAPSHOT</version>
        </dependency>
    </dependencies>
```

**JAVA POM 依赖继承两种方式**

通过parent继承，如下所示：(如下是非常典型的spring boot的parent继承)，项目将继承spring-boot-starter-parent POM中的所有设置及依赖（如：properties、dependencies等）

```
    <parent>
        <groupId>org.springframework.boot</groupId>
        <artifactId>spring-boot-starter-parent</artifactId>
        <version>2.1.6.RELEASE</version>
    </parent>
```

通过dependencyManagement继承，如下所示：（这是依赖管理，dependencyManagement里只是声明依赖，并不实现引入，因此子项目可按需显式的声明所需的依赖项。**如果不在子项目中声明依赖，则不会从父项目中继承依赖，只有在子项目中声明了依赖项，且没有指定具体版本，才会从父项目中继承依赖项，（写了版本号相当于覆盖），**version和scope都读取自父pom）

```
    <dependencyManagement>
        <dependencies>
            <dependency>
                <groupId>org.springframework.cloud</groupId>
                <artifactId>spring-cloud-dependencies</artifactId>
                <version>Greenwich.SR2</version>
                <type>pom</type>
                <scope>import</scope>
            </dependency>
        </dependencies>
    </dependencyManagement>
```

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11306099.html#_labelTop)

### 二、依赖注入 DI （IOC容器）

###### C#（一般在Startup文件中ConfigureServices方法中按需注册依赖，注册依赖可以指定生命周期如：AddTransient【瞬时，即：每次都创建新实例】、AddScoped【作用域范围内】、AddSingleton【单例，仅实例化一次】，具体效果可以参见：[在 ASP.NET Core 依赖注入](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2)）

```
//1.使用ASP.NET CORE默认的DI框架，在Startup文件中ConfigureServices方法中按需注册依赖
        public void ConfigureServices(IServiceCollection services)
        {
            //采用ASP.NET CORE默认的IOC容器注册
            services.AddTransient<IOperationTransient, Operation>();
            services.AddScoped<IOperationScoped, Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();
            services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));
        }

//2.在Controller中就可以直接采用构造函数注入或指明从IOC容器中获得实例[FromServices]
    [ApiController]
    [Route("api/[controller]")]
    public class DemoController : Controller
    {
        private readonly OperationService operationService;

        public DemoController(OperationService operationService)
        {
            this.operationService = operationService;
        }

        [Route("optid")]
        public object Operation([FromServices]OperationService optSrv){
            //TODO:方法体中直接使用operationService 或 入参optSrv均可
        }
      }

//如上所需接口及类定义

    public interface IOperation
    {
        Guid OperationId { get; }
    }


    public interface IOperationTransient : IOperation
    {
    }

    public interface IOperationScoped : IOperation
    {
    }

    public interface IOperationSingleton : IOperation
    {
    }

    public interface IOperationSingletonInstance : IOperation
    {
    }
    

    public class Operation : IOperationTransient,
        IOperationScoped,
        IOperationSingleton,
        IOperationSingletonInstance
    {
        public Operation() : this(Guid.NewGuid())
        {
        }

        public Operation(Guid id)
        {
            OperationId = id;
        }

        public Guid OperationId { get; private set; }
    }
    
    public class OperationService
    {
        public OperationService(
            IOperationTransient transientOperation,
            IOperationScoped scopedOperation,
            IOperationSingleton singletonOperation,
            IOperationSingletonInstance instanceOperation)
        {
            TransientOperation = transientOperation;
            ScopedOperation = scopedOperation;
            SingletonOperation = singletonOperation;
            SingletonInstanceOperation = instanceOperation;
        }

        public IOperationTransient TransientOperation { get; }
        public IOperationScoped ScopedOperation { get; }
        public IOperationSingleton SingletonOperation { get; }
        public IOperationSingletonInstance SingletonInstanceOperation { get; }

    }
```

C#使用第三方IOC容器，如：autofac，由第三方IOC容器接管并实现DI，示例如下：（autofac支持更多、更灵活的依赖注入场景）

```
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //采用ASP.NET CORE默认的IOC容器注册
            services.AddTransient<IOperationTransient, Operation>();
            services.AddScoped<IOperationScoped, Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();
            services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));

            services.AddTransient<OperationService, OperationService>();

            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services); //交由autofac IOC容器管理

            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);//使用utofac IOC容器
        }
```

###### JAVA（可以使用xml来进行Bean的依赖注册，也可使用注解方式来进行依赖注册，目前在DI方面更多的是流行注解注册及注入，故这里也以注解依赖注册及注入为简要说明，更多有关注解依赖注册及注入以及XML的依赖注册及注入详细说明，可查阅我之前的文章：[JAVA WEB快速入门之通过一个简单的Spring项目了解Spring的核心（AOP、IOC）](https://www.cnblogs.com/zuowj/p/10020536.html)）

> 注解依赖注册一般可以通过自定义一个spring统一注册配置类，如代码中所示BeansConfig，这种一般对于集中注册Bean或Bean之间有先后依赖，先后顺序时比较有效果；另一种是直接在Bean上使用@Component注解（或其它专用含义的注解，如：@Repository、@Service，这些注解本身也标记了@Component注解）

```
//1. 在自定义的spring统一注册配置类中注册相关Bean
@Configuration
public class BeansConfig {

    @Bean
    @Scope("prototype") //singleton,request,session
    @Order(1) //注册顺序
    public DemoBean demoBean(){
        return new DemoBean();
    }

    @Bean("demo") //定义名称
    @Order(2)
    public  DemoInterface demoInterface(){
        return  new DemoImplBean(demoBean()); //构造函数注入
    }
}

//2.在Controller中就可以直接通过属性注入或构造函数注入获得实例，并在ACTION中使用这些实例对象
@RestController
public class DemoController {

    @Autowired
    private  DemoBean demoBean;

    @Autowired
    @Qualifier("demo")//指定从IOC中解析的bean注册名
    private  DemoInterface demoInterface;

    @Autowired
    private  DemoBean2 demoBean2;

    @RequestMapping(path = "/demo/msg",method = RequestMethod.GET,produces = "application/json;charset=utf-8")
    public Object testMsg(@RequestParam(value = "m",required = false) String m){
        //TODO:可直接使用：demoBean、demoInterface、demoBean2这些私有字段，它们通过属性注入
        return "test msg:" + m;
    }

}

//以下是如上所需的类及接口定义

public class DemoBean {

}

public interface DemoInterface {
    void showMsg(String msg);
}

public class DemoImplBean implements  DemoInterface {

    private  DemoBean demoBean;

    public  DemoImplBean(DemoBean demoBean){
        this.demoBean=demoBean;
    }

    @Override
    public void showMsg(String msg) {
        System.out.println("show msg:" + msg);
    }
}

//通过标记Component，交由spring IOC自动扫描注册
@Component
public class DemoBean2 {

}
```

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11306099.html#_labelTop)

### 三、过滤器、拦截器 AOP

###### C#（在ASP.NET   CORE中实现AOP常见有三种方式：第一种：添加ACTION过滤器（仅适用于MVC）；第二种：使用第三方的AOP切面拦截器（如下文的AopInterceptor，可拦截指定的任意位置的虚方法），第三种：在请求管道中添加中间件（仅适用MVC））

```
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opt => opt.Filters.Add<AopFilter>() //第一种：添加过滤器，实现ACTION执行前后记录耗时
                ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            containerBuilder.RegisterType<AopInterceptor>();
            containerBuilder.RegisterType<OperationService>().InterceptedBy(typeof(AopInterceptor)).EnableClassInterceptors(); //第二种:启用autofac的AOP拦截
            
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
            
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //第三种：使用一个自定义的中间件，实现AOP的效果
            app.Use(async (ctx, next) =>
            {
                //如果为示例逻辑
                if (!ctx.Request.Query.TryGetValue("token", out var tokenVal) || tokenVal != "zuowenjun")
                {
                    await ctx.Response.WriteAsync("验证token失败，禁止访问！");
                    return;
                }

                ctx.Request.EnableBuffering();//启动用buffer，以便可以重置Position
                var requestReader = new StreamReader(ctx.Request.Body);

                var requestContent = requestReader.ReadToEnd();
                ctx.Request.Body.Position = 0; //需要重置为流开头，否则将导致后续的Model Binding失效等各种问题

                var originalResponseStream = ctx.Response.Body;//记录原始请求
                using (var ms = new MemoryStream())
                {
                    ctx.Response.Body = ms;//因原始请求为只写流，故此处用自定义的内存流来接收响应流数据

                    var watch = Stopwatch.StartNew();
                    await next.Invoke();
                    watch.Stop();

                    ms.Position = 0;
                    var responseReader = new StreamReader(ms);
                    var responseContent = responseReader.ReadToEnd();

                    string logMsg = $"execedTime:{ watch.ElapsedMilliseconds.ToString() }ms,Request,{requestContent},Response: { responseContent}";
                    Logger.LogInformation(logMsg);

                    ms.Position = 0;//恢复流位置为开头

                    await ms.CopyToAsync(originalResponseStream); //将当前的流合并到原始流中
                    ctx.Response.Body = originalResponseStream; //恢复原始响应流
                };
            });

            app.UseMvc();

        }



    /// <summary>
    /// Filter仅针对接入层（MVC）有效，底层服务若需使用AOP，则必需使用特定的AOP框架
    /// </summary>
    public class AopFilter : IActionFilter
    {
        private readonly Stopwatch stopWatch = new Stopwatch();

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //执行前逻辑
            stopWatch.Start();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //执行后逻辑
            stopWatch.Stop();
            var returnResult = context.Result;
            if (returnResult is ObjectResult)
            {
                var objResult = (returnResult as ObjectResult);
                objResult.Value = new { Original = objResult.Value, ElapsedTime = stopWatch.ElapsedMilliseconds.ToString() + "ms" };
            }
            else if (returnResult is JsonResult)
            {
                var jsonResult = (returnResult as JsonResult);
                jsonResult.Value = new { Original = jsonResult.Value, ElapsedTime = stopWatch.ElapsedMilliseconds.ToString() + "ms" };
            }
        }


    }
```

###### JAVA（可以通过自定义Filter、HandlerInterceptor、MethodInterceptor 、around AOP增强等方式实现AOP拦截处理）

```
//最先执行，由servlet拦截请求（适用WEB）
@WebFilter(filterName = "demoFilter",urlPatterns = "/*")
class  DemoFilter implements Filter {

    @Override
    public void init(FilterConfig filterConfig) throws ServletException {
        //初始化
    }

    @Override
    public void doFilter(ServletRequest servletRequest, ServletResponse servletResponse, FilterChain filterChain) throws IOException, ServletException {
        //过滤处理
    }

    @Override
    public void destroy() {
        //销毁之前执行
    }
}

//其次执行，由spring MVC拦截请求（适用Spring MVC）
@Component
public class DemoHandlerInterceptor implements HandlerInterceptor  {
//也可继承自HandlerInterceptorAdapter

    @Override
    public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler) throws Exception {
        //执行前

        return false;
    }

    @Override
    public void postHandle(HttpServletRequest request, HttpServletResponse response, Object handler, ModelAndView modelAndView) throws Exception {
        //执行后，生成视图之前执行
    }

    @Override
    public void afterCompletion(HttpServletRequest request, HttpServletResponse response, Object handler, Exception ex) throws Exception {
        //在DispatcherServlet完全处理完请求之后被调用，可用于清理资源
    }
}

//最后执行，拦截方法
@Component
class DemoMethodInterceptor implements MethodInterceptor{

    @Override
    public Object invoke(MethodInvocation methodInvocation) throws Throwable {
        return null;
    }
}

//方法拦截的另一种形式
@Component
@Aspect
class AutoAspectJInterceptor {

    @Around("execution (*..controller.*.*(..))")
    public Object around(ProceedingJoinPoint point) throws Throwable{
        //执行前
        Object object = point.proceed();
        //执行后
        return object;
    }

}
```

> 特别说明：ASP.NET CORE中的**Fitler** 与Spring MVC中的**MethodInterceptor**类似，都是控制方法，而ASP.NET CORE中的**请求管道中间件**与Spring MVC中的**Filter、HandlerInterceptor**类似，都是控制请求过程。这点要搞清楚。

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11306099.html#_labelTop)

### 四、配置读取

###### C#（支持多种配置数据提供程序，支持多种获取配置信息的方式，详见：[ASP.NET Core 中的配置](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.2)）

```
            //Configuration为IConfiguration实例对象
            Configuration.GetValue("key");//适用单个key-value
            Configuration.Get<TConfig>();//适用整个config文件映射为一个TConfig类型的对象
            Configuration.GetSection("key").GetChildren();//获取子项集合
```

###### JAVA（支持多种配置数据源格式（yml,Properties），可通过@value、@ConfigurationProperties、Environment等常见方法来获取配置信息）

```
    //1.通过@value方式获取配置信息
    @Value("${zuowenjun.site}")
    public String zwjSite;

    //2.通过创建一个映射配置信息的Bean（ConfigProperties） 方式获取配置信息
    @Component
    @ConfigurationProperties()//如果有前缀，则可以设置prefix=XXX
        public static class Zuowenjun {

        private String site;
        private String skills;
        private String motto;


        public String getSite() {
            return site;
        }

        public void setSite(String site) {
            this.site = site;
        }

        public String getSkills() {
            return skills;
        }

        public void setSkills(String skills) {
            this.skills = skills;
        }

        public String getMotto() {
            return motto;
        }

        public void setMotto(String motto) {
            this.motto = motto;
        }

    }

//3.通过Environment来直接获取配置信息
environment.getProperty("zuowenjun.site");
```

[返回顶部](https://www.cnblogs.com/Leo_wl/p/11306099.html#_labelTop)

### 五、发布、部署、运行

C#（ASP.NET CORE：除了如下使用.NET CLI命今进行发布打包，也可以使用VS或VS CODE可视化操作进行发布操作）

```
dotnet publish --configuration Release
```

JAVA（Spring MVC：除了如下使用MAVEN命令进行清理打包，还可以使用IDEA来进行打包，具体方法可参见：[Springboot项目打包成jar运行2种方式](https://www.cnblogs.com/stm32stm32/p/9973325.html)）

```
mvn clean package；
```

C#（ASP.NET CORE）、JAVA(Spring MVC)都可以：

1. 都支持WINDOWS服务器、Linux服务器等多种平台服务器 部署运行

2. 都支持使用命令行启动运行ASP.NET CORE 或Spring MVC应用，例如：

   > dotnet aspnetcoreApp.dll --urls="http://*:5001"
   >
   > java -jar springmvcApp.jar --server.port=5001

3. 都支持Jenkins CI&CD ，Docker、k8s虚拟化部署

4. 都支持在Linux服务器中以守护进程方式运行，例如：

   > nohup dotnet aspnetcoreApp.dll > aspnetcoreApp.out 2>&1 &
   >
   > nohup java -jar springmvcApp.jar > springmvcApp.out 2>&1 &
   >
   > //或者都使用Supervisor来构建守护进程，还提供管理UI，具体请参见网上相关资源

好了，总结到此结束，愿能帮助到那些处于.NET 转JAVA 或JAVA 转.NET或者想多了解一门编程语言的朋友们，祝大家事业有成。今后将分享更多关于分布式、算法等方面的知识，不局限.NET或JAVA语言，敬请期待，谢谢！

**码字不易，若需转载及转载我之前的文章请注明出处，谢谢。**





​     分类:              [[27\]NET Core](https://www.cnblogs.com/Leo_wl/category/225703.html)

​         [好文要顶](javascript:void(0);)         [已关注](javascript:void(0);)     [收藏该文](javascript:void(0);)     [![img](icon_weibo_24.png)](javascript:void(0);)     [![img](wechat.png)](javascript:void(0);) 

[![img](u104109.gif)](https://home.cnblogs.com/u/Leo_wl/)

​             [HackerVirus](https://home.cnblogs.com/u/Leo_wl/)
​             [关注 - 246](https://home.cnblogs.com/u/Leo_wl/followees/)
​             [粉丝 - 3417](https://home.cnblogs.com/u/Leo_wl/followers/)         





​                 我在关注他 [取消关注](javascript:void(0);)     

​         0     

​         0     



​     



​      [« ](https://www.cnblogs.com/Leo_wl/p/11223236.html) 上一篇：    [Net UI Spy工具:ManagedSpy](https://www.cnblogs.com/Leo_wl/p/11223236.html)     
​     [» ](https://www.cnblogs.com/Leo_wl/p/11312156.html) 下一篇：    [深层信念网络](https://www.cnblogs.com/Leo_wl/p/11312156.html)  