#	EF Core 包管理

安装最新稳定`Microsoft.EntityFrameworkCore.Design`包。 

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Design -v 1.1.6
```



### ASP.NET Core 环境

若要指定用于 ASP.NET Core 项目的环境，请设置**env:ASPNETCORE_ENVIRONMENT**之前运行命令。

## 通用参数

下表显示了所共有的所有 EF Core 命令参数：

| 参数                     | 描述                                                         |
| :----------------------- | :----------------------------------------------------------- |
| -Context <String>        | 要使用的 `DbContext` 类。 唯一或完全限定的命名空间的类名称。  如果省略此参数，EF Core 将查找上下文类。 如果有多个上下文类，此参数是必需的。 |
| -Project <String>        | 目标项目中。 如果省略此参数，则**默认项目**有关**程序包管理器控制台**用作目标项目。 |
| -StartupProject <String> | 启动项目。 如果省略此参数，则**启动项目**中**解决方案属性**用作目标项目。 |
| -Verbose                 | 显示详细输出。                                               |

若要显示有关命令的帮助信息，请使用 PowerShell 的`Get-Help`命令。

 提示

上下文、 项目和 StartupProject 参数支持选项卡扩展。

## 添加迁移

添加新的迁移。

参数：

| 参数                | 描述                                                         |
| :------------------ | :----------------------------------------------------------- |
| -Name <String>      | 迁移的名称。 这是位置参数，是必需的。                        |
| -OutputDir <String> | 目录 （及其子命名空间） 来使用。 路径是相对于目标项目目录。 默认值为"迁移"。 |

## Drop-Database

删除数据库。

参数：

| 参数    | 描述                                   |
| :------ | :------------------------------------- |
| -WhatIf | 显示哪个数据库会被丢弃，但没有删除它。 |

## Get-DbContext

获取有关的信息`DbContext`类型。

## Remove-Migration

删除 （请回滚迁移已完成的代码更改） 的最后一个迁移。

参数：

| 参数   | 描述                                      |
| :----- | :---------------------------------------- |
| -Force | 还原迁移 （请回滚已应用到数据库的更改）。 |

## Scaffold-DbContext

为生成代码`DbContext`和数据库的实体类型。 为了使`Scaffold-DbContext`若要生成的实体类型，数据库表必须具有主键。

参数：

| 参数                 | 描述                                                         |
| :------------------- | :----------------------------------------------------------- |
| -连接<字符串 >       | 数据库的连接字符串。 对于 ASP.NET Core 2.x 项目，值可以是*名称 =<的连接字符串名称 >*。 在这种情况下名称来自于为项目设置的配置源。 这是位置参数，是必需的。 |
| -Provider <String>   | 要使用的提供程序。 通常这是 NuGet 包的名称为例： `Microsoft.EntityFrameworkCore.SqlServer`。 这是位置参数，是必需的。 |
| -OutputDir <String>  | 要将文件放入的目录。 路径是相对于项目目录。                  |
| -ContextDir <String> | 要放置的目录`DbContext`文件中。 路径是相对于项目目录。       |
| -Context <String>    | 名称`DbContext`类生成。                                      |
| -Schemas <String[]>  | 要生成的实体类型的表架构。 如果省略此参数，则包括所有架构。  |
| -表<String [] >      | 要生成的实体类型的表。 如果省略此参数，则包括所有表。        |
| -DataAnnotations     | 特性用于将模型配置 （如果可能）。 如果省略此参数，则使用仅 fluent API。 |
| -UseDatabaseNames    | 在数据库中显示的完全相同，请使用表和列的名称。 如果省略此参数，则会更改数据库名称，使其更紧密地符合 C# 名称样式约定。 |
| -Force               | 覆盖现有文件。                                               |

示例:

PowerShell 			

```powershell
Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
```

搭建基架以选定的表，并具有指定名称的单独文件夹中创建上下文的示例：

PowerShell 			

```powershell
Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables "Blog","Post" -ContextDir Context -Context BlogContext
```

## Script-Migration

生成应用的所有更改从一个所选迁移到另一个所选的迁移的 SQL 脚本。

参数：

| 参数             | 描述                                                         |
| :--------------- | :----------------------------------------------------------- |
| *-From* <String> | 开始迁移。 可能会被标识迁移，按名称或 id。 数字 0 是一种特殊情况，意味着*第一次迁移之前*。 默认值为 0。 |
| *-To* <String>   | 结束的迁移。 默认的最后一个迁移。                            |
| -Idempotent      | 生成脚本，可以在任何迁移的数据库上使用。                     |
| -输出<字符串 >   | 要将结果写入的文件。 如果省略此参数，创建应用程序的运行时文件时，例如使用相同的文件夹中生成的名称创建该文件： */obj/Debug/netcoreapp2.1/ghbkztfz.sql/*。 |

 提示

收件人、、 和输出参数支持选项卡扩展。

以下示例创建一个脚本，以便使用迁移名称 InitialCreate 迁移。

PowerShell 			

```powershell
Script-Migration -To InitialCreate
```

下面的示例创建一个脚本，以便所有迁移 InitialCreate 迁移之后，使用迁移 id。

PowerShell 			

```powershell
Script-Migration -From 20180904195021_InitialCreate
```

## Update-Database

更新数据库，到最后一个迁移或指定的迁移。

| 参数                  | 描述                                                         |
| :-------------------- | :----------------------------------------------------------- |
| *-Migration* <String> | 目标迁移。 可能会被标识迁移，按名称或 id。 数字 0 是一种特殊情况，意味着*第一次迁移之前*并导致所有迁移操作，从而还原。 如果指定无需迁移，则此命令的最后一个默认迁移。 |

 提示

迁移参数支持选项卡扩展。

下面的示例将恢复所有迁移。

PowerShell 			

```powershell
Update-Database -Migration 0
```

下面的示例将数据库更新为指定的迁移。 第一个示例使用迁移名称和另一个使用迁移 ID:

PowerShell 			

```powershell
Update-Database -Migration InitialCreate
Update-Database -Migration 20180904195021_InitialCreate
```



#	-.NET CLI

```bash
dotnet add package Microsoft.EntityFrameworkCore.Design
```



## 常用选项

|      | 选项                              | 描述                                                         |
| :--- | :-------------------------------- | :----------------------------------------------------------- |
|      | `--json`                          | 显示 JSON 输出。                                             |
| `-c` | `--context <DBCONTEXT>`           | `DbContext`类使用。 唯一或完全限定的命名空间的类名称。  如果省略此选项，则 EF Core 会发现上下文类。 如果有多个上下文类，则需要此选项。 |
| `-p` | `--project <PROJECT>`             | 目标项目的项目文件夹的相对路径。  默认值为当前文件夹。       |
| `-s` | `--startup-project <PROJECT>`     | 为启动项目的项目文件夹的相对路径。 默认值为当前文件夹。      |
|      | `--framework <FRAMEWORK>`         | [目标框架名字对象](https://docs.microsoft.com/zh-cn/dotnet/standard/frameworks#supported-target-framework-versions)有关[目标框架](https://docs.microsoft.com/zh-cn/dotnet/standard/frameworks)。  使用项目文件指定多个目标框架，并且想要选择其中之一时。 |
|      | `--configuration <CONFIGURATION>` | 生成配置，例如：`Debug`或`Release`。                         |
|      | `--runtime <IDENTIFIER>`          | 若要还原的包的目标运行时标识符。 有关运行时标识符 (RID) 的列表，请参阅 [RID 目录](https://docs.microsoft.com/zh-cn/dotnet/core/rid-catalog)。 |
| `-h` | `--help`                          | 显示帮助信息。                                               |
| `-v` | `--verbose`                       | 显示详细输出。                                               |
|      | `--no-color`                      | 不为着色输出。                                               |
|      | `--prefix-output`                 | 输出级别使用的前缀。                                         |

## dotnet ef 数据库拖放

删除数据库。

选项:

|      | 选项        | 描述                                   |
| :--- | :---------- | :------------------------------------- |
| `-f` | `--force`   | 不确认。                               |
|      | `--dry-run` | 显示哪个数据库会被丢弃，但没有删除它。 |

## dotnet ef 数据库更新

更新数据库，到最后一个迁移或指定的迁移。

参数：

| 参数          | 描述                                                         |
| :------------ | :----------------------------------------------------------- |
| `<MIGRATION>` | 目标迁移。 可能会被标识迁移，按名称或 id。 数字 0 是一种特殊情况，意味着*第一次迁移之前*并导致所有迁移操作，从而还原。 如果指定无需迁移，则此命令的最后一个默认迁移。 |

下面的示例将数据库更新为指定的迁移。 第一个示例使用迁移名称和另一个使用迁移 ID:

console 			

```console
dotnet ef database update InitialCreate
dotnet ef database update 20180904195021_InitialCreate
```

## dotnet ef dbcontext 信息

获取有关的信息`DbContext`类型。

## dotnet ef dbcontext 列表

列出可用`DbContext`类型。

## dotnet ef dbcontext 基架

为生成代码`DbContext`和数据库的实体类型。 为了使此命令来生成实体类型，数据库表必须具有主键。

参数：

| 参数           | 描述                                                         |
| :------------- | :----------------------------------------------------------- |
| `<CONNECTION>` | 数据库的连接字符串。 对于 ASP.NET Core 2.x 项目，值可以是*名称 =<的连接字符串名称 >*。 在这种情况下名称来自于为项目设置的配置源。 |
| `<PROVIDER>`   | 要使用的提供程序。 通常这是 NuGet 包的名称为例： `Microsoft.EntityFrameworkCore.SqlServer`。 |

选项:

|      | 选项                        | 描述                                                         |
| :--- | :-------------------------- | :----------------------------------------------------------- |
| -d   | `--data-annotations`        | 特性用于将模型配置 （如果可能）。 如果省略此选项，则使用仅 fluent API。 |
| `-c` | `--context <NAME>`          | 名称`DbContext`类生成。                                      |
|      | `--context-dir <PATH>`      | 要放置的目录`DbContext`中的类文件。 路径是相对于项目目录。 命名空间派生自文件夹名称。 |
| `-f` | `--force`                   | 覆盖现有文件。                                               |
| `-o` | `--output-dir <PATH>`       | 要将实体类文件放入的目录。 路径是相对于项目目录。            |
|      | `--schema <SCHEMA_NAME>...` | 要生成的实体类型的表架构。 若要指定多个架构，请重复`--schema`为每个。 如果省略此选项，则包括所有架构。 |
| `-t` | `--table <TABLE_NAME>`...   | 要生成的实体类型的表。 若要指定多个表，请重复`-t`或`--table`为每个。 如果省略此选项，则包括所有表。 |
|      | `--use-database-names`      | 在数据库中显示的完全相同，请使用表和列的名称。 如果省略此选项，则会更改数据库名称，使其更紧密地符合 C# 名称样式约定。 |

下面的示例搭建基架以所有架构和表，并将新文件放入*模型*文件夹。

console 			

```console
dotnet ef dbcontext scaffold "Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models
```

下面的示例搭建基架以选定的表，并具有指定名称的单独文件夹中创建的上下文：

console 			

```console
dotnet ef dbcontext scaffold "Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -t Blog -t Post --context-dir Context -c BlogContext
```

## 添加 dotnet ef 迁移

添加新的迁移。

参数：

| 参数     | 描述         |
| :------- | :----------- |
| `<NAME>` | 迁移的名称。 |

选项:

|      | 选项                  | 描述                                                         |
| :--- | :-------------------- | :----------------------------------------------------------- |
| `-o` | `--output-dir <PATH>` | 目录 （及其子命名空间） 来使用。 路径是相对于项目目录。 默认值为"迁移"。 |

## dotnet ef 迁移列表

列出了可用的迁移。

## dotnet ef 迁移删除

删除 （请回滚迁移已完成的代码更改） 的最后一个迁移。

选项:

|      | 选项      | 描述                                      |
| :--- | :-------- | :---------------------------------------- |
| `-f` | `--force` | 还原迁移 （请回滚已应用到数据库的更改）。 |

## dotnet ef 迁移脚本

从迁移中生成的 SQL 脚本。

参数：

| 参数     | 描述                                                         |
| :------- | :----------------------------------------------------------- |
| `<FROM>` | 开始迁移。 可能会被标识迁移，按名称或 id。 数字 0 是一种特殊情况，意味着*第一次迁移之前*。 默认值为 0。 |
| `<TO>`   | 结束的迁移。 默认的最后一个迁移。                            |

选项:

|      | 选项              | 描述                                     |
| :--- | :---------------- | :--------------------------------------- |
| `-o` | `--output <FILE>` | 要写入到脚本的文件。                     |
| `-i` | `--idempotent`    | 生成脚本，可以在任何迁移的数据库上使用。 |

以下示例创建一个脚本，以便 InitialCreate 迁移：

console 			

```console
dotnet ef migrations script 0 InitialCreate
```

下面的示例创建 InitialCreate 迁移后迁移的所有脚本。

console 			

```console
dotnet ef migrations script 20180904195021_InitialCreate
```



# 设计时 DbContext 创建

## 使用不带任何参数的构造函数

如果不能从应用程序服务提供商获取 DbContext，这些工具查找派生`DbContext`项目内的类型。 然后，他们尝试使用不带任何参数的构造函数创建实例。 这可以是默认构造函数，如果`DbContext`使用配置[ `OnConfiguring` ] [ 6](https://docs.microsoft.com/zh-cn/ef/core/miscellaneous/configuring-dbcontext#onconfiguring)方法。

## 从设计时工厂

此外可以告知工具如何通过实现来创建 DbContext`IDesignTimeDbContextFactory<TContext>`接口： 如果找到实现此接口的类中派生的同一项目`DbContext`或在应用程序的启动项目中，这些工具绕过改为创建 DbContext 和使用的设计时工厂的其他方法。

C# 			

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MyProject
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<BloggingContext>
    {
        public BloggingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BloggingContext>();
            optionsBuilder.UseSqlite("Data Source=blog.db");

            return new BloggingContext(optionsBuilder.Options);
        }
    }
}
```

# 设计时服务

工具使用某些服务仅在设计时使用。 这些服务 EF Core运行时服务，以阻止它们不会与你的应用部署分开管理。 若要重写其中一种服务 （例如服务来生成迁移文件），将添加的实现`IDesignTimeServices`到启动项目。

C# 			

```csharp
class MyDesignTimeServices : IDesignTimeServices
{
    public void ConfigureDesignTimeServices(IServiceCollection services)
        => services.AddSingleton<IMigrationsCodeGenerator, MyMigrationsCodeGenerator>()
}
```

#	迁移



```
Add-Migration InitialCreate
dotnet ef migrations add InitialCreate

Remove-Migration
dotnet ef migrations remove



Update-Database
dotnet ef database update

```

## 还原迁移

如果已对数据库应用一个迁移（或多个迁移），但需将其复原，则可使用同一命令来应用迁移，并指定回退时的目标迁移名称。

```
Update-Database LastGoodMigration
dotnet ef database update LastGoodMigration

```

## 生成 SQL 脚本

调试迁移或将其部署到生产数据库时，生成一个 SQL 脚本很有帮助。 之后可进一步检查该脚本的准确性，并对其作出调整以满足生产数据库的需求。 该脚本还可与部署技术结合使用。 基本命令如下。

```
Script-Migration
dotnet ef migrations script
```

