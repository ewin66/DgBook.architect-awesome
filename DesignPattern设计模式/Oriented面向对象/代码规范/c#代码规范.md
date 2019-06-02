## 变量名--需要表达出变量的用途

- 【强制】函数的**形参**和**局部变量**采用小驼峰命名。
  正例：taskId / taskNumber
  反例：taskid / TaskNumbe
- 【强制】类的**实例字段以”m_”**开头，**静态字段以”s_”**开头，后面的部分使用
  小驼峰的形式命名，如果是控件类型，需要在最后加上控件类型的全名。

正例：string m_userName / static string s_userAge / TextBox m_userAddress

反例：string UserName / static string UserAge / TextBox m_userAddressTextBox



- 【强制】类**常量**或者只读字段不加前缀，且使用**大驼峰**命名。
  正例：const string UserName / const TextBox UserAddressTextBox
  反例：const string m_userName / const TextBox s_userAddressTextBox
- 【强制】除了对象字段，最好不要出现非数字和字母的字符。
  反例：_name / __name / $name / name_ / name$ / name__
  表达式规范

## 函数

- 【强制】函数以”**动词**”为核心的词组命名。
  正例：GetUserName() / SetUserName() / Login()
  反例：UserBehavior()
- 【强制】函数和类型采用大驼峰命名。
  正例：GetTaskId / GetTaskNumber
  反例：gettasks / TaskNumber
- 强制】语句体即使只有一句也不省略{}。
  正例：if ( flag ) { flag=false; }
  反例：if ( flag ) flag=false;
- 【强制】if 语句中的条件表达式的逻辑运算不要超过 3 个。
  正例：if(isDeleted){return;}
  if ( flag || (value != “123” && s.Equals("123")) ) { return ;}
  反例：if (isDeleted || flag || (value != “123” && s.Equals("123") )) { return ; }
  函数规范
- 【推荐】**回调函数**可以采用”**On**+对象+事件”形式命名。
  正例：OnTextBoxChanged() / OnButtonClicked()
- 【强制】**代码每行最多 130 个字符，超过需要换行。**
- 【强制】**函数体不超过 30 行。**

## 类

- 【强制】类采用以”名词”为核心的词组命名。
  正例：BankAccount / BankAccountLoader
  反例：SupportInitialize / BankAccountLoad

- 【强制】类的属性采用以”名词”为核心的词组命名。
  正例：NameAppender
  反例：NameAppend

- 【强制】类的成员函数(包含普通函数、索引、属性)需参照函数的规范。

  

  ##### 常量类型

- 【强制】类常量或者只读字段不加前缀，且使用大驼峰命名。
  正例：const string UserName / const TextBox UserAddressTextBox
  反例：const string m_userName / const TextBox s_userAddressTextBox

- 【强制】类不允许有非常量的公开字段，如果确实需要，则需用属性来代替。
  正例：public string UserName{get;set;}
  反例：public string m_userName

- 【强制】**类公开方法不超过 15 个**，类**不超过 800 行**(包括空行在内) 。

  ##### 接口

- 【强制】以”I”开头，以”形容词”或者”名词”命名。
  正例：IBankAccount / ISupportInitialize
  反例：BankAccount / SupportInitialize / SetName

- 【强制】**接口的成员不超过 5 个**。 



