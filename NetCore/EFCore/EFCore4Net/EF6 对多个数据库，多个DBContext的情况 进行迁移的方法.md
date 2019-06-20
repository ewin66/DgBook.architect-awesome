# [EF6 对多个数据库，多个DBContext的情况 进行迁移的方法。](https://www.cnblogs.com/kmsfan/p/5744250.html)



参见：

<http://stackoverflow.com/questions/21537558/multiple-db-contexts-in-the-same-db-and-application-in-ef-6-and-code-first-migra>

和：

<http://stackoverflow.com/questions/24970876/the-type-context-does-not-inherit-from-system-data-entity-migrations-dbmigrat>

 如果`ConfigurationTypeName不能用的话，那么就用Configuration 我就是这么成功的。`









---



##	[Multiple DB Contexts in the Same DB and Application in EF 6 and Code First Migrations](https://stackoverflow.com/questions/21537558/multiple-db-contexts-in-the-same-db-and-application-in-ef-6-and-code-first-migra)

http://stackoverflow.com/questions/21537558/multiple-db-contexts-in-the-same-db-and-application-in-ef-6-and-code-first-migra



[问问题](https://stackoverflow.com/questions/ask)

91

我是实体框架的新手。我正在尝试建立一个使用EF 6的MVC应用程序，我使用的是代码第一迁移(CodeFirstMigrations)。我正在使用区域在应用程序，并希望有不同的DbContext在每个地区，以打破它。我知道EF 6有ContextKey，但我找不到关于如何使用它的完整信息。目前，我只能一次使用一个上下文。

有人能给出一个有足够细节的例子让一个新的人像我这样的EF理解和使用。

[ASP.NETMVC](https://stackoverflow.com/questions/tagged/asp.net-mvc) [实体-框架](https://stackoverflow.com/questions/tagged/entity-framework) [EF-代码-第一](https://stackoverflow.com/questions/tagged/ef-code-first) [EF-迁移](https://stackoverflow.com/questions/tagged/ef-migrations)

[分享](https://stackoverflow.com/q/21537558)[改进这个问题](https://stackoverflow.com/posts/21537558/edit)

问2月3日下午20：55

![img](assets/7e425a11c5548821a22d2ace327a7842.png)

拉伊

**551**2815

添加注释



 

实体框架6增加了对多个`DbContext`s通过添加`-ContextTypeName`和`-MigrationsDirectory`旗子。我只是在我的PackageManager控制台中运行了命令并粘贴了下面的输出.

如果你有2个`DbContext`在你的项目中，你运行`enable-migrations`，您将得到一个错误(您可能已经知道)：

```
PM> enable-migrations
More than one context type was found in the assembly 'WebApplication3'.
To enable migrations for 'WebApplication3.Models.ApplicationDbContext', use Enable-Migrations -ContextTypeName WebApplication3.Models.ApplicationDbContext.
To enable migrations for 'WebApplication3.Models.AnotherDbContext', use Enable-Migrations -ContextTypeName WebApplication3.Models.AnotherDbContext.
```

所以你得跑`enable-migrations`每一个`DbContext`分开来。你必须为每个人指定一个文件夹`Configuration.cs`要生成的文件.。

```
PM> Enable-Migrations -ContextTypeName ApplicationDbContext -MigrationsDirectory Migrations\ApplicationDbContext
Checking if the context targets an existing database...
Code First Migrations enabled for project WebApplication3.

PM> Enable-Migrations -ContextTypeName AnotherDbContext -MigrationsDirectory Migrations\AnotherDbContext
Checking if the context targets an existing database...
Code First Migrations enabled for project WebApplication3.
```

为每个`DbContext`，您可以这样做，方法是指定`Configuration`班级：

```
PM> Add-Migration -ConfigurationTypeName WebApplication3.Migrations.ApplicationDbContext.Configuration "InitialDatabaseCreation"
Scaffolding migration 'InitialDatabaseCreation'.
The Designer Code for this migration file includes a snapshot of your current Code First model. This snapshot is used to calculate the changes to your model when you scaffold the next migration. If you make additional changes to your model that you want to include in this migration, then you can re-scaffold it by running 'Add-Migration InitialDatabaseCreation' again.

PM> Add-Migration -ConfigurationTypeName WebApplication3.Migrations.AnotherDbContext.Configuration "InitialDatabaseCreation"
Scaffolding migration 'InitialDatabaseCreation'.
The Designer Code for this migration file includes a snapshot of your current Code First model. This snapshot is used to calculate the changes to your model when you scaffold the next migration. If you make additional changes to your model that you want to include in this migration, then you can re-scaffold it by running 'Add-Migration InitialDatabaseCreation' again.
```

你就跑了`update-database`同样的方式：

```
PM> Update-Database -ConfigurationTypeName WebApplication3.Migrations.ApplicationDbContext.Configuration
Specify the '-Verbose' flag to view the SQL statements being applied to the target database.
Applying explicit migrations: [201402032113124_InitialDatabaseCreation].
Applying explicit migration: 201402032113124_InitialDatabaseCreation.
Running Seed method.

PM> Update-Database -ConfigurationTypeName WebApplication3.Migrations.AnotherDbContext.Configuration
Specify the '-Verbose' flag to view the SQL statements being applied to the target database.
Applying explicit migrations: [201402032113383_InitialDatabaseCreation].
Applying explicit migration: 201402032113383_InitialDatabaseCreation.
Running Seed method.
```

希望这能帮上忙。



[分享](https://stackoverflow.com/a/21538091)[改进这个答案](https://stackoverflow.com/posts/21538091/edit)

回答2月3日21时25分

![img](https://www.gravatar.com/avatar/73bb7d7ffe700cdbe22828c038f72f2b?s=32&d=identicon&r=PG)

朱炳良

**33.2k**87063

- 

  我是否需要为每个上下文都有一个单独的连接字符串，还是有一个解决方法？ – [拉伊](https://stackoverflow.com/users/3170063/lrayh) 2月4日下午15：20

- 3

  它们可以共享相同的连接字符串。但你要确保它们不是映射到同一张桌子上。 – [朱炳良](https://stackoverflow.com/users/3199781/anthony-chu) 2月4日下午15：23

- 

  如果它们确实映射到同一个表，您仍然可以定义哪个迁移将首先运行，并让它的迁移文件创建表，哪个将运行，然后修改它，这样它就不会创建已经退出的表。然后你可以用`MigrateDatabaseToLatestVersion`锻造`ctx.Database.initialize()`要以正确的顺序运行的每个上下文，或运行`Update-Database`按正确的顺序手工指挥。(如果您执行到以前版本的db迁移，则进行反向迁移)。它是*“危险”*但这是可以做到的。 – [约塔比](https://stackoverflow.com/users/1216612/jotabe) 11月24日14点56分

- 

  因此，我将迁移添加到我的项目中，并创建了与ApplicationDbContext不同的上下文。我继续使用了大约6个月与站点相关的数据的上下文，然后是开始与我的ApplicationUser打交道的时候了。我的基本登录和注册工作正常，但我想扩展用户类以添加一些附加字段。这个答案对于为这个上下文设置一个新的迁移配置非常有帮助。谢谢！快起来 – [埃里克·比沙德](https://stackoverflow.com/users/2623804/eric-bishard) 2月17日下午4：35

- 

  如果我能给你一个+10这个简短但足够多的答案，我会的，谢谢@anthonyju。 – [卡里姆公司](https://stackoverflow.com/users/2427481/karim-ag) 11月1日‘15日9：03

[秀**1**更多评论](https://stackoverflow.com/questions/21537558/multiple-db-contexts-in-the-same-db-and-application-in-ef-6-and-code-first-migra#)

## **受保护**通过[邵鲁丁](https://stackoverflow.com/users/1108891/shaun-luttin) 4月22日下午14：31

谢谢你对这个问题的兴趣。因为它吸引了必须删除的低质量或垃圾邮件答案，所以现在发布一个答案需要10。[声誉](https://stackoverflow.com/help/whats-reputation)在这个网站上([协会奖金不算在内](https://stackoverflow.com/help/privileges/new-user)). 

你愿意回答其中一个吗？[未回答的问题](https://stackoverflow.com/unanswered?fromProtectedNotice=true)而不是？





----







# [The type 'Context' does not inherit from 'System.Data.Entity.Migrations.DbMigrationsConfiguration'. with EF migration](https://stackoverflow.com/questions/24970876/the-type-context-does-not-inherit-from-system-data-entity-migrations-dbmigrat)

​         [             









4

我有两个上下文和一个数据库。

当我试图添加一个数据库迁移(添加迁移)时，我会得到这个错误。

我把EF更新为6.2。

我检查了配置，cs文件正在使用

```
internal sealed class Configuration : DbMigrationsConfiguration<MSiH.CigaretteContext>
```

PM>添加-迁移-配置MSiH.DataAccess.CigabteContext-详细的cmdlet添加-迁移在命令管道位置1提供以下参数的值：名称：使用启动项目‘GridAndMap’的initals。使用NuGet项目“GridAndMap”。

```
System.Data.Entity.Migrations.Infrastructure.MigrationsException: The type
 'MSiH.CigaretteContext' does not inherit from 
'System.Data.Entity.Migrations.DbMigrationsConfiguration'. 
Migrations configuration types must extend from
'System.Data.Entity.Migrations.DbMigrationsConfiguration'.
at System.Data.Entity.Utilities.TypeExtensions.CreateInstance[T]
(Type type, Func`3 typeMessageFactory, Func`2 exceptionFactory)
at System.Data.Entity.Migrations.Utilities.MigrationsConfigurationFinder.FindMigrationsConfiguration
(Type contextType, String configurationTypeName, 
Func`2 noType, Func`3 multipleTypes, Func`3 noTypeWithName, Func`3 multipleTypesWithName)
 at System.Data.Entity.Migrations.Design.ToolingFacade.BaseRunner.FindConfiguration()
 at System.Data.Entity.Migrations.Design.ToolingFacade.ScaffoldRunner.Run()
 at System.AppDomain.DoCallBack(CrossAppDomainDelegate callBackDelegate)
 at System.AppDomain.DoCallBack(CrossAppDomainDelegate callBackDelegate)
 at System.Data.Entity.Migrations.Design.ToolingFacade.Run(BaseRunner runner)
 at System.Data.Entity.Migrations.Design.ToolingFacade.Scaffold(String migrationName,    String language, String rootNamespace, Boolean ignoreChanges)
 at System.Data.Entity.Migrations.AddMigrationCommand.Execute(String name, Boolean force, Boolean ignoreChanges)
 at System.Data.Entity.Migrations.AddMigrationCommand.<>c__DisplayClass2.<.ctor>b__0()
 at System.Data.Entity.Migrations.MigrationsDomainCommand.Execute(Action command)
 The type 'MSiH.EyePaid.CigaretteWebApp.DataAccess.CigaretteContext' does not inherit  
 from 'System.Data.Entity.Migrations.DbMigrationsConfiguration'. Migrations 
 configuration types must extend from
'System.Data.Entity.Migrations.DbMigrationsConfiguration'.
```

**更新**

我在跟踪一个[例](http://www.dotnet-tricks.com/Tutorial/entityframework/2VOa140214-Entity-Framework-6-Code-First-Migrations-with-Multiple-Data-Contexts.html)关于如何在多个上下文中使用EF迁移。

```
Enable-Migrations -ContextTypeName Foo.CigaretteContext
```

这个命令给出了错误：

```
Add-Migration -configuration Foo.CigaretteContext Initial
```

此命令工作如下：

```
Add-Migration Initial
```

[实体-框架](https://stackoverflow.com/questions/tagged/entity-framework) [EF-迁移](https://stackoverflow.com/questions/tagged/ef-migrations)

[分享](https://stackoverflow.com/q/24970876)[改进这个问题](https://stackoverflow.com/posts/24970876/edit)

[编辑7月26日下午15：09](https://stackoverflow.com/posts/24970876/revisions)

 

- 1

  你必须出示你的“香烟”课程，否则我们帮不上忙。 – [戴维格](https://stackoverflow.com/users/1663001/davidg) 7月26日下午15：31

添加注释



## 1个答复

[主动](https://stackoverflow.com/questions/24970876/the-type-context-does-not-inherit-from-system-data-entity-migrations-dbmigrat?answertab=active#tab-top)[最老](https://stackoverflow.com/questions/24970876/the-type-context-does-not-inherit-from-system-data-entity-migrations-dbmigrat?answertab=oldest#tab-top)[得票](https://stackoverflow.com/questions/24970876/the-type-context-does-not-inherit-from-system-data-entity-migrations-dbmigrat?answertab=votes#tab-top)



5



这个`Configuration`或`ConfigurationTypeName`参数派生的类。[DbMigrationsConfiguration](http://msdn.microsoft.com/en-us/library/system.data.entity.migrations.dbmigrationsconfiguration(v=vs.113).aspx).

您所指的是一个派生自`DbContext`.

你应该这么做。

```
Add-Migration -Configuration MSiH.MigrationDatabaseIfAny.Configuration Initial
```

PS：`MigrationDatabaseIfAny`是目录名，如果`Configuration`类位于目录下，如果不只是删除它的话。



[分享](https://stackoverflow.com/a/24972812)[改进这个答案](https://stackoverflow.com/posts/24972812/edit)

回答7月26日下午15：54

![img](https://i.stack.imgur.com/yYu0X.jpg?s=32&g=1)

尤利亚姆·钱德拉

**12.7k**114259

添加注释



