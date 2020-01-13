##   左耳朵耗子：[程序复杂混乱的根本原因是什么？](https://mp.weixin.qq.com/s?__biz=MzI4MTY5NTk4Ng==&mid=2247492261&idx=4&sn=a01a5d1368db49d92ca9d72557bf99d5&chksm=eba7e138dcd0682e520267c24e72bc08b4a8f5e87a946dfac19d9860bf4265f4f6ebdfcbb1ee&scene=0&xtrack=1&key=05fabe4cf5ab7e7220f19a8196b91aa9d1b5987a62c7264632725910a3d68080938b274d8e1fcb25b4aa0d4d6c09156e99630f9dcfcea7e57f4b30ee9a9157ef932f2b114a8b5fc52cb96c1a4e64161b&ascene=1&uin=MjQxODY2MzI0Mw%3D%3D&devicetype=Windows+10&version=62070158&lang=zh_CN&exportkey=AV2sbMrMN1LsRT%2FOmBcIBwM%3D&pass_ticket=4xryhSh4OhNyOSeS1aFZCO3OLO5H3GnIsom55cKlHkRHex9cYs%2FzoOAQno72ebEh)

  [                         极客时间                      ](javascript:void(0);)                                *2019-08-21*                

相信大家都知道陈皓，网名“左耳朵耗子”，酷壳（coolshell.cn）博客博主，资深技术专家。耗子叔在《编程范式游记》专栏里说：**绝大多数程序复杂混乱的根本原因就是业务逻辑与控制逻辑纠缠不清。**

相信大家都知道陈皓，网名“左耳朵耗子”，酷壳（coolshell.cn）博客博主，资深技术专家。耗子叔在《编程范式游记》专栏里说：绝大多数程序复杂混乱的根本原因就是业务逻辑与控制逻辑纠缠不清。

如果你看过那些混乱不堪的代码，会发现其中最大的问题就是把 Logic 和 Control 纠缠在一起了，导致代码很混乱，难以维护，Bug 很多。如果你再仔细结合各式各样的编程范式来思考，**会发现所有的语言或编程范式都在解决下面这 3 个事：**

\1. Control 是可以标准化的。比如：遍历数据、查找数据、多线程、并发、异步等，都是可以标准化的。

\2. 因为 Control 需要处理数据，所以标准化 Control，需要标准化 Data Structure，我们可以通过泛型编程来解决这个事。

\3. 而 Control 还要处理用户的业务逻辑，即 Logic。所以，我们可以通过标准化接口 / 协议来实现，我们的 Control 模式可以适配于任何的 Logic。

上述 3 点，就是编程范式的本质：**有效地分离 Logic、Control 和 Data 是写出好程序的关键所在！**

编程范式，是程序语言的“设计本质”，也是“编程方法论”。学习这些范式，可以让你了解诸多不同类型的编程语言各自要解决什么问题，此外，还可以帮助你**从编程的表面直接看到本质，这对于指导我们提高编程技能，写出更优秀的代码，极具现实意义 。**

为了让更多人感受到陈皓对技术的独家见解，汲取他 20 年编程心得和经验，极客时间跟陈皓一起，**推出《编程范式游记》小专栏。**