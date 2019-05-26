





https://blog.csdn.net/Dfame1588888/article/details/80850271



# 搜集的一些Asp.netMVC面试题（一）

​                                                   2018年06月29日 18:39:28           [d_霓虹深处](https://me.csdn.net/Dfame1588888)           阅读数：5493                   

​                   

# 1、列举ASP.NET页面之间传递值的几种方式？

1.使用QueryString,  如....?id=1; response. Redirect()....

2.使用Session变量

3.使用Server.Transfer

4.使用Application

5.使用Cache

6使用HttpContext的Item属性

7.使用文件

8.使用数据库

9.使用Cookie

# 2、简述 private、 protected、 public、internal 修饰符的访问权限？

private : 私有成员, 在类的内部才可以访问(只能从其声明上下文中进行访问)。  

protected : 保护成员，该类内部和从该类派生的类中可以访问。 

public : 公共成员，完全公开，没有访问限制。  

internal: 在同一命名空间内可以访问。 （很少用）

# 3、C#中的委托是什么？事件是不是一种委托？



委托是将一种方法作为参数代入到另一种方法。

事件是一种特殊的委托。  //比如：onclick事件中的参数就是一种方法。

# 4、实现多态的过程中 overload 重载 与override 重写的区别？



 override 重写与 overload 重载的区别——重载是方法的名称相同。参数或参数类型不同，进行多次重载以适应不同的需要

 Override 是进行基类中函数的重写。实现多态。

# 5、请编程实现一个冒泡排序算法？

经典的冒泡排序法？

int[] array=new int[*];

int temp=0;

for(int i=0;i<array.length-1;i++)

{

for(int j=i+1;j<array.length;i++)

​        {

if(array[j]<array[i])

​             {

temp=array[i];

array[i]=array[j];

array[j]=temp;

​            }



​        }

}

# 6、用.net做B/S结构的系统，您是用几层结构来开发，每一层之间的关系以及为什么要这样分层？



使用MVC模式分层

一般为3层

数据访问层，业务层，表示层。

数据访问层对数据库进行增删查改。

业务层一般分为二层，业务表观层实现与表示层的沟通，业务规则层实现用户密码的安全等。

表示层为了与用户交互例如用户添加表单。



优点：  分工明确，条理清晰，易于调试，而且具有可扩展性。

缺点：  增加成本。

# 7、什么是装箱和拆箱？



装箱就是隐式的将一个值型转换为引用型对象。

拆箱就是将一个引用型对象转换成任意值型。

比如：
int i=0;
Syste.Object obj=i;
这个过程就是装箱！就是将 i 装箱！


比如：
int i=0;
System.Object obj=i;
int j=(int)obj;
这个过程前2句是将 i 装箱，后一句是将 obj 拆箱！

# 8、什么是受管制托管代码？



托管代码是运行.NET 公共语言运行时CLR的代码

unsafe：非托管代码。不经过CLR运行。程序员自行分配和释放内存空间

# 9、ADO.net中常用的对象有哪些？分别描述一下



DataSet:数据集。

DataCommand:执行语句命令。

DataAdapter:数据的集合，用语填充。

DataReader:数据只读器

Connection 数据库连接对像

Command 数据库命令

# 10、在C＃中，string str = null 与 string str = “” 请尽量使用文字或图象说明其中的区别？。

string str = null 是不给他分配内存空间,而string str = "" 给它分配长度为空字符串的内存空间。

# 11、请详述在C#中类(class)与结构(struct)的异同？



class可以被实例化,属于引用类型,class可以实现接口和单继承其他类,还可以作为基类型,是分配在内存的堆上的

struct属于值类型,不能作为基类型,但是可以实现接口,是分配在内存的栈上的.

# 12、SQLSERVER服务器中，给定表 table1 中有两个字段 ID、LastUpdateDate，ID表示更新的事务号， LastUpdateDate表示更新时的服务器时间，请使用一句SQL语句获得最后更新的事务号？

select ID from tablel where LastUpdataData=(select Max(lastUpdataData) from table1)

# 13、abstract class和interface有什么区别?



声明方法的存在而不去实现它的类被叫做抽象类（abstract   class），它用于要创建一个体现某些基本行为的类，并为该类声明方法，但不能在该类中实现该类的情况。不能创建abstract 类的实例。然而可以创建一个变量，其类型是一个抽象类，并让它指向具体子类的一个实例。不能有抽象构造函数或抽象静态方法。Abstract 类的子类为它们父类中的所有抽象方法提供实现，否则它们也是抽象类为。取而代之，在子类中实现该方法。知道其行为的其它类可以在类中实现这些方法。

接口（interface）是抽象类的变体。在接口中，所有方法都是抽象的。多继承性可通过实现这样的接口而获得。接口中的所有方法都是抽象的，没有一个有程序体。接口只可以定义static   final成员变量。接口的实现与子类相似，除了该实现类不能从接口定义中继承行为。当类实现特殊接口时，它定义（即将程序体给予）所有这种接口的方法。然后，它可以在实现了该接口的类的任何对象上调用接口的方法。由于有抽象类，它允许使用接口名作为引用变量的类型。通常的动态联编将生效。引用可以转换到接口类型或从接口类型转换，instanceof 运算符可以用来决定某对象的类是否实现了接口。

 abstract 修饰词可用于类别、方法、属性、索引子 (Indexer) 和事件。在类别宣告里使用 abstract

饰词，表示该类别只是当做其它类别的基底类别而已。成员如果标记为抽象，或是包含在抽象类(Abstract Class) 内，则必须由衍生自此抽象类别的类别实作这个成员。

在静态属性上使用 abstract 修饰词是错误的。

在抽象方法宣告中使用 static 或 virtual 修饰词是错误的。

接口只包含方法、委派或事件的签章。方法的实作 (Implementation) 是在实作接口的类别中完成，

 

# 14、堆和栈的区别？



​    栈：由编译器自动分配、释放。在函数体中定义的变量通常在栈上。

​    堆：一般由程序员分配释放。用new、malloc等分配内存函数分配得到的就是在堆上。

# 15、GC是什么? 为什么要有GC?



GC是垃圾收集器。程序员不用担心内存管理，因为垃圾收集器会自动进行管理。要请求垃圾收集，可以调用下面的方法之一：

  System.gc()

  Runtime.getRuntime().gc()

# 16、.try {}里有一个return语句，那么紧跟在这个try后的finally {}里的code会不会被执行，什么时候被执行，在return前还是后?

会执行，在return前执行。

# 17、DataReader与Dataset有什么区别？

DataReader和DataSet最大的区别在于,DataReader使用时始终占用SqlConnection,在线操作数据库.任何对SqlConnection的操作都会引发DataReader的异常.因为DataReader每次只在内存中加载一条数据,所以占用的内存是很小的..因为DataReader的特殊性和高性能.所以DataReader是只进的.你读了第一条后就不能再去读取第一条了.   DataSet则是将数据一次性加载在内存中.抛弃数据库连接.读取完毕即放弃数据库连接.因为DataSet将数据全部加载在内存中.所以比较消耗内存.但是确比DataReader要灵活.可以动态的添加行,列,数据.对数据库进行回传更新操作

# 18、在c#中using和new这两个关键字有什么意义，请写出你所知道的意义？using 指令和语句 new 创建实例 new 隐藏基类中方法。



　using 引入名称空间或者使用非托管资源，使用完对象后自动执行实现了IDisposable接口的类的Dispose方法

　new 新建实例或者隐藏父类方法

# 19、什么是虚函数？什么是抽象函数？



虚函数：没有实现的，可由子类继承并重写的函数。Virtual CallSomeOne();

抽象函数：规定其非虚子类必须实现的函数，必须被重写。public abstract void CallSomeOne(); 

 

# 20、大概描述一下ASP.NET服务器控件的生命周期?

初始化  加载视图状态  处理回发数据  加载  发送回发更改通知  处理回发事件  预呈现  保存状态  呈现  处置  卸载

# 21、Static Nested Class 和 Inner Class的不同？

Static Nested Class是被声明为静态（static）的内部类，它可以不依赖于外部类实例被实例化。而通常的内部类需要在外部类实例化后才能实例化。

# 22、<%# %> 和 <%  %> 有什么区别？



<%# %>表示绑定的数据源

<% %>是服务器端代码块



# **23、asp.net中web应用程序获取数据的流程**

A.Web Page　B.Fill  C.Sql05  D.Data Sourse  E.DataGrid  F.DataSet  G.Select and Connect Commands  H.Sql Data Adapter

a,e,d,f,h,g,b,c

# 24、..NET和C#有什么区别？



.NET一般指 .NET FrameWork框架，它是一种平台，一种技术。

C#是一种编程语言，可以基于.NET平台的应用。

# 25、求以下表达式的值，写出您想到的一种或几种实现方法：1-2+3-4+……+m？

int Num=this.TextBox1.Text.ToString();

int Sum=0;

for（int i=0;i<Num+1;i++）

{

if(i%2)==1)

​     {

Sum+=1;

​       }

else

​    {

Sum=Sum-1;

​     }

}

System.Consele.WriteLine(Sum.Tostring());

System.Consele.ReadLine();



# 26、CTS、CLS、CLR分别作何解释？

CTS：通用语言系统。CLS：通用语言规范。CLR：公共语言运行库。



# 27、面向对象的语言具有________性、_________性、________性？



封装、继承、多态。

封装：用抽象的数据类型将数据和基于数据的操作封装在一起，数据被保护在抽象数据类型内部。

 
继承：子类拥有父类的所有数据和操作。

 

多态：一个程序中同名的不同方法共存的情况。有两种形式的多态– 重载与重写。

# 28、启动一个线程是用run()还是start()?



启动一个线程是调用start()方法，使线程所代表的虚拟处理机处于可运行状态，这意味着它可以由JVM调度并执行。这并不意味着线程就会立即运行。

run()方法可以产生必须退出的标志来停止一个线程。

# 29、 .net Remoting 和 Web Service 的区别？



1、Remoting可以灵活的定义其所基于的协议，如果定义为HTTP，则与Web   Service就没有什么区别了，一般都喜欢定义为TCP，这样比Web   Service稍为高效一些   
  2、Remoting不是标准，而Web   Service是标准；   
  3、Remoting一般需要通过一个WinForm或是Windows服务进行启动，而Web   Service则需要IIS进行启动。   
  4、在VS.net开发环境中，专门对Web   Service的调用进行了封装，用起来比Remoting方便   
 建议还是采用Web   Service好些，对于开发来说更容易控制。

 

为了能清楚地描述Web   Service   和Remoting之间得区别,我打算从他们的体系结构上来说起:     
  Web   Service大体上分为5个层次:     
  1.   Http传输信道     
  2.   XML的数据格式     
  3.   SOAP封装格式     
  4.   WSDL的描述方式     
  5.   UDDI     

 总体上来讲，.NET   下的   Web   Service结构比较简单，也比较容易理解和应用：   
一般来讲在.NET结构下的WebService应用都是基于.net   
Framework以及IIS的架构之下，所以部署(Dispose)起来相对比较容易点。

  从实现的角度来讲，     
  首先WebService必须把暴露给客户端的方法所在的类继承于：System.Web.Services.WebService这个基类，其次所暴露的方法前面必须有[WebMethod]或者[WebMethodAttribute]     
    
WebService的运行机理（运行过程）
  首先客户端从服务器的到WebService的WSDL，同时在客户端声称一个代理类(Proxy   Class)    这个代理类负责与WebService服务器进行Request   和Response     
    当一个数据（XML格式的）被封装成SOAP格式的数据流发送到服务器端的时候，就会生成一个进程对象并且把接收到这个Request的SOAP包进行解析，然后对事物进行处理，处理结束以后再对这个计算结果进行SOAP包装，然后把这个包作为一个Response发送给客户端的代理类(Proxy    Class)，同样地，这个代理类也对这个SOAP包进行解析处理，继而进行后续操作。  

 

下面对.net   Remoting进行概括的阐述：     
.net    Remoting    是在DCOM等基础上发展起来的一种技术，它的主要目的是实现跨平台、跨语言、穿透企业防火墙，这也是他的基本特点，与WebService有所不同的是，它支持HTTP以及TCP信道，而且它不仅能传输XML格式的SOAP包，也可以传输传统意义上的二进制流，这使得它变得效率更高也更加灵活。而且它不依赖于IIS，用户可以自己开发(Development)并部署(Dispose)自己喜欢的宿主服务器，所以从这些方面上来讲WebService其实上是.net    Remoting的一种特例。

 

.net  Remoting的特点是     
他的优点是用户既可以使用TCP信道方式进行二进制流方式通信，也可以使用HTTP信道进行SOAP格式的性通信  ，效率相对WebService要高不少；但是它的缺点也很明显，.net   remoting只能应用于MS 的.net Framework之下。
从性能上来讲Remoting的效率和传统的DCOM、COM+的性能很相近！

# 30、数组有没有length()这个方法? String有没有length()这个方法？ 

数组没有length()这个方法，有length的属性。String有length()这个方法。 

# 31、子类对父类中虚方法的处理有重写（override）和覆盖（new），请说明它们的区别？



有父类ParentClass和子类ChildClass、以及父类的虚方法VirtualMethod。有如下程序段：
ParentClass pc = new ChildClass();pc.VirtualMethod(...);
如果子类是重写（override）父类的VirtualMethod，则上面的第二行语句将调用子类的该方法
如果子类是覆盖（new）父类的VirtualMethod，则上面的第二行语句将调用父类的该方法

#  32、值类型和引用类型的区别？写出C#的样例代码？

基于值类型的变量直接包含值。将一个值类型变量赋给另一个值类型变量时，将复制包含的值。这与引用类型变量的赋值不同，引用类型变量的赋值只复制对对象的引用，而不复制对象本身。所有的值类型均隐式派生自 System.ValueType。与引用类型不同，从值类型不可能派生出新的类型。但与引用类型相同的是，结构也可以实现接口。与引用类型不同，值类型不可能包含 null值。然而，可空类型功能允许将 null 赋给值类型。每种值类型均有一个隐式的默认构造函数来初始化该类型的默认值。值类型主要由两类组成：结构、枚举,结构分为以下几类：Numeric（数值）类型、整型、浮点型、decimal、bool、用户定义的结构。引用类型的变量又称为对象，可存储对实际数据的引用。声明引用类型的关键字：class、interface、delegate、内置引用类型：object、string

# 33、C#中的接口和类有什么异同？



异：不能直接实例化接口。接口不包含方法的实现。接口、类和结构可从多个接口继承。但是C# 只支持单继承：类只能从一个基类继承实现。类定义可在不同的源文件之间进行拆分。

同：接口、类和结构可从多个接口继承。接口类似于抽象基类：继承接口的任何非抽象类型都必须实现接口的所有成员。接口可以包含事件、索引器、方法和属性。一个类可以实现多个接口。

# 34、UDP连接和TCP连接的异同。？

TCP---传输控制协议,提供的是面向连接、可靠的字节流服务。当客户和服务器彼此交换数据前，必须先在双方之间建立一个TCP连接，之后才能传输数据。TCP提供超时重发，丢弃重复数据，检验数据，流量控制等功能，保证数据能从一端传到另一端。 
UDP---用户数据报协议，是一个简单的面向数据报的运输层协议。UDP不提供可靠性，它只是把应用程序传给IP层的数据报发送出去，但是并不能保证它们能到达目的地。由于UDP在传输数据报前不用在客户和服务器之间建立一个连接，且没有超时重发等机制，故而传输速度很快。

# 35、请解释web.config文件中的重要节点？

Web.config文件是一个XML文本文件，它用来储存 ASP.NET Web 应用程序的配置信息（如最常用的设置ASP.NET Web 应用程序的身份验证方式），它可以出现在应用程序的每一个目录中。当你通过VB.NET新建一个Web应用程序后，默认情况下会在根目录自动创建一个默认的Web.config文件，包括默认的配置设置，所有的子目录都继承它的配置设置。如果你想修改子目录的配置设置，你可以在该子目录下新建一个Web.config文件。它可以提供除从父目录继承的配置信息以外的配置信息，也可以重写或修改父目录中定义的设置。　1、<authentication> 节作用：配置 ASP.NET 身份验证支持（为Windows、Forms、PassPort、None四种）。该元素只能在计算机、站点或应用程序级别声明。<authentication> 元素必需与<authorization> 节配合使用。示例：以下示例为基于窗体（Forms）的身份验证配置站点，当没有登陆的用户访问需要身份验证的网页，网页自动跳转到登陆网页。<authentication mode="Forms" > <forms loginUrl="logon.aspx" name=".FormsAuthCookie"/></authentication> 其中元素loginUrl表示登陆网页的名称，name表示Cookie名称2、<authorization> 节作用：控制对 URL 资源的客户端访问（如允许匿名用户访问）。此元素可以在任何级别（计算机、站点、应用程序、子目录或页）上声明。必需与<authentication> 节配合使用。3、<compilation>节作用：配置 ASP.NET 使用的所有编译设置。默认的debug属性为“True”.在程序编译完成交付使用之后应将其设为True（Web.config文件中有详细说明，此处省略示例）4、<customErrors>作用：为 ASP.NET 应用程序提供有关自定义错误信息的信息。它不适用于 XML Web services 中发生的错误。5、<httpRuntime>节作用：配置 ASP.NET HTTP 运行库设置。该节可以在计算机、站点、应用程序和子目录级别声明。　6、 <pages>作用：标识特定于页的配置设置（如是否启用会话状态、视图状态，是否检测用户的输入等）。<pages>可以在计算机、站点、应用程序和子目录级别声明。7、<sessionState>　　作用：为当前应用程序配置会话状态设置（如设置是否启用会话状态，会话状态保存位置）。　8、<trace>　　作用：配置 ASP.NET 跟踪服务，主要用来程序测试判断哪里出错。

# 36、

设有关系EMP（ENO，ENAME，SALARY，DNO ），其中各属性的含义依次为职工号、姓名、工资和所在部门号，以及关系DEPT（DNO，DNAME，MANAGER），其中各属性含义依次为部门号、部门名称、部门经理的职工号。试用SQL语句完成以下查询：？



a) 列出各部门中工资不低于600元的职工的平均工资。
select dno , avg(salary) as average from emp where salary>=600 group by dno

b) 查询001号职工所在部门名称。
select DNAME from dept where DNO = (select DNO from emp where eno=’001’) 
或者
select d.dname from dept as d left jon emp as e on e.dno = d.dno where e.eno=’001’

c)请用SQL语句将“销售部”的那些工资数额低于600的职工的工资上调10%。
update EMP set SALARY =SALARY*(1+0.1) where SALARY<600 and DNO = ( select DNO from dept where dname= ‘销售部’ )



end..........