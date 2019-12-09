# [通过配置的方式Autofac 《第三篇》](https://www.cnblogs.com/qixuejia/p/5009845.html)



https://www.cnblogs.com/qixuejia/p/5009845.html

# 一、基本配置

### 　　**1、通过配置的方式使用Autofac**

[![复制代码](Autofac03 通过配置的方式Autofac 《第三篇》.assets/copycode.gif)](javascript:void(0);)

```xml
　　<?xml version="1.0"?>
　　<configuration>
  　　<configSections>
    　　<section name="autofac" type="Autofac.Configuration.SectionHandler, Autofac.Configuration"/>
  　　</configSections>
  　　<autofac defaultAssembly="ConsoleApplication3">
    　　<components>
      　　<component type="ConsoleApplication3.Worker, ConsoleApplication3" service="ConsoleApplication3.IPerson" />
    　　</components>
  　　</autofac>
　　</configuration>
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

### 　　**2、通过RegisterModule方式使用配置文件中的信息**

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```csharp
    static void Main(string[] args)
    {
        ContainerBuilder builder = new ContainerBuilder();
        builder.RegisterType<AutoFacManager>();
        builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
        using (IContainer container = builder.Build())
        {
            AutoFacManager manager = container.Resolve<AutoFacManager>();
            manager.Say();
        } 
            
        Console.ReadKey();
    }
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

### 　　**3、通过Register的方式**

```csharp
    builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
    builder.Register(c => new AutoFacManager(c.Resolve<IPerson>()));
```

 

作者：[Cat Qi](http://qixuejia.cnblogs.com)
 出处：[http://qixuejia.cnblogs.com/](http://qixuejia.cnblogs.com)
 本文版权归作者和博客园共有，欢迎转载，但未经作者同意必须保留此段声明，且在文章页面明显位置给出原文连接，否则保留追究法律责任的权利。

​    分类:             [框架设计：IoC/DI](https://www.cnblogs.com/qixuejia/category/658230.html)