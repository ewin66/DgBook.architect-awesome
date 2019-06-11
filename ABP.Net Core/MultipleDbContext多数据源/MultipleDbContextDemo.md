# [ABP框架多数据库支持](http://www.nsoff.com/archives/657/)

  8-30  [杂七杂八](http://www.nsoff.com/archives/category/uncategorized/)  [honeyeater](http://www.nsoff.com/archives/author/honeyeater/)  1,058 views



使用 abp框架做基础框架开发应用程序。有需求要直接其它业务库，也就是说项目不只一个数据库
 查资料，最终在官方的示例项目找到了多数据库的DEMO

**MultipleDbContextDemo 地址：https://github.com/aspnetboilerplate/aspnetboilerplate-samples/tree/master/MultipleDbContextDemo**

**按照示例项目的Readme写的把项目运行起来。**

有两个上下文MyFirstDbContext和MySecondDbContext

```
 public MySecondDbContext()
 : base(“Second”) //指定连接字符串
 {

}
```

对应在web.config中配置节点。

```
<add  name=”Default” connectionString=”Server=localhost; Database=MultipleDB;  Trusted_Connection=True;” providerName=”System.Data.SqlClient” />
  <add name=”Second” connectionString=”Server=localhost;  Database=MultipleDB2; Trusted_Connection=True;”  providerName=”System.Data.SqlClient” />

 
```

它是以MyFirstDbContext.cs中 public virtual IDbSet<Person> Persons { get; set; }指定了实体来区分在哪个数据库上去操作

如果有两个相同的Person都在两个库中都有呢就需要在名字上做一下区别。

就是这么简单。

- [上一篇](http://www.nsoff.com/archives/649/)
- [下一篇](http://www.nsoff.com/archives/678/)

 版权属于: [烁飞日志](http://www.nsoff.com)

 原文地址: <http://www.nsoff.com/archives/657/>

转载时必须以链接形式注明原始出处及本声明。