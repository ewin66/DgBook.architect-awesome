# [abp集成abp.Dapper](https://www.cnblogs.com/feigao/p/11059754.html)





### 首先看下官网的介绍：

https://aspnetboilerplate.com/Pages/Documents/Dapper-Integration

中文翻译：

### 介绍

[Dapper](https://github.com/StackExchange/Dapper)是.NET的对象关系映射器（ORM）。该[Abp.Dapper](https://www.nuget.org/packages/Abp.Dapper)包装简单集成到精致小巧ASP.NET样板。它与EF 6.x，EF Core或NHibernate一起作为辅助ORM提供程序。

### 安装

在开始之前，您需要将 [Abp.Dapper](https://www.nuget.org/packages/Abp.Dapper)和EF Core，EF 6.x或NHibernate ORM NuGet包安装到您要使用的项目中。

#### 模块注册

首先，您需要在模块上为AbpDapperModule添加DependsOn属性：

[![复制代码](copycode.gif)](javascript:void(0);)

```
[DependsOn(
     typeof(AbpEntityFrameworkCoreModule),
     typeof(AbpDapperModule)
)]
public class MyModule : AbpModule
{
    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(SampleApplicationModule).GetAssembly());
    }
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

请注意，应该在EF Core依赖项之后添加AbpDapperModule依赖项。

#### 表映射的实体

您可以配置映射。例如，Person类映射到以下示例中的 Persons表：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
public class PersonMapper : ClassMapper<Person>
{
    public PersonMapper()
    {
        Table("Persons");
        Map(x => x.Roles).Ignore();
        AutoMap();
    }
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

您应该设置包含映射器类的程序集。例：

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
[DependsOn(
     typeof(AbpEntityFrameworkModule),
     typeof(AbpDapperModule)
)]
public class MyModule : AbpModule
{
    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(SampleApplicationModule).GetAssembly());
        DapperExtensions.SetMappingAssemblies(new List<Assembly> { typeof(MyModule).GetAssembly() });
    }
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

 

### 用法

注册AbpDapperModule后，您可以使用Generic IDapperRepository接口（而不是标准IRepository）来注入dapper存储库。

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

```
public class SomeApplicationService : ITransientDependency
{
    private readonly IDapperRepository<Person> _personDapperRepository;
    private readonly IRepository<Person> _personRepository;

    public SomeApplicationService(
        IRepository<Person> personRepository,
        IDapperRepository<Person> personDapperRepository)
    {
        _personRepository = personRepository;
        _personDapperRepository = personDapperRepository;
    }

    public void DoSomeStuff()
    {
        var people = _personDapperRepository.Query("select * from Persons");
    }
}
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

您可以在同一个事务中同时使用EF和Dapper存储库！

------

官网上说的有点模糊，这里整理下

1、在application和efcore层添加nuget包Abp.Dapper

![img](https://img2018.cnblogs.com/blog/763945/201906/763945-20190620164234931-1225338792.png)

 

![img](https://img2018.cnblogs.com/blog/763945/201906/763945-20190620164153905-1775844048.png)

2、在EntityFrameworkCore层的EntityFrameworkCore->**EntityFrameworkModule类中添加以下代码，对应官网文档的【模块注册】

 ![img](https://img2018.cnblogs.com/blog/763945/201906/763945-20190620164415189-1563119010.png)

3、设置包含映射器类的程序集

![img](https://img2018.cnblogs.com/blog/763945/201906/763945-20190620164651275-937290853.png)

```
DapperExtensions.DapperExtensions.SetMappingAssemblies(new List<Assembly> { typeof(WisdomCloudEntityFrameworkModule).GetAssembly() });
```

 如果是mysql，必须添加下面这句

```
//使用mysql必须修改，默认是sqlserver
DapperExtensions.DapperExtensions.SqlDialect = new MySqlDialect();
```

 

4、EntityFrameworkCore添加下面文件夹来添加映射关系，对应官网文档【表映射的实体】

![img](https://img2018.cnblogs.com/blog/763945/201906/763945-20190621142254060-1856424051.png)

 

 

5、在application层就可以通过IDapperRepository使用了

![img](https://img2018.cnblogs.com/blog/763945/201906/763945-20190620165005761-1007495584.png)

 