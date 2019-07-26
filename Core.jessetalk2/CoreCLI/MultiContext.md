  

##	指定Context

```bash
Update-Database -Context FirstDbContext
SimpleCmsWithAbpDbContext
Update-Database -Context SimpleCmsWithAbpDbContext
ConfigurationDbContext


Update-Database -Context ConfigurationDbContext
Update-Database -Context ResearchDbContext
dotnet ef database update -c ApplicationDbContext
Update-Database -Verbose


```



```
Add-Migration -Name InitialIdentityServerConfigurationDbMigration -Context ConfigurationDbContext  -OutputDir Migrations/IdentityServer/ConfigurationDb -Project Fooww.Research.EntityFrameworkCore
```

Add-Migration -Name InitBook	-OutputDir EntityFrameworkCore/Migrations

##	Update-Database

###		进行数据库版本回溯。

```
Update-Database –TargetMigration:"201309201643300_AddCity.cs"
```

###		版本之间的Sql脚本

执行程序包管理器控制台语句，生成数据库版本之间的Sql脚本。该操作仅为生成Sql语句，并未在数据库中进行执行。

```
Update-Database -Script -SourceMigration:"201309201643300_AddCity.cs" -TargetMigration:"201309201708043_ModifyCity.cs" 
```

　　其中-TargetMigration在未指定的情况，默认为迁移到最新的版本。





##	**EF Code First Migrations**

1>、为指定的DbContext启用数据库迁移

```
PM> Enable-Migrations -ContextTypeName Portal.PortalContext
```

2>、设置是否允许自动迁移

```
Enable-Migrations
```

生成的Configuration.cs类文件的构造函数

```
public Configuration()
{
      AutomaticMigrationsEnabled = false;
}
```

3>、Enable-Migrations指定项目名称

```
PM> Enable-Migrations -StartUpProjectName Portal
```

如果在“Package Manager Console”中选择了默认项目可以不设置“-StartUpProjectName”参数；如果多次执行此命令可以添加-Force参数。

4>、查看所执行的Sql语句 -Verbose指令

```
Update-Database -Verbose 
```

**4、代码下载**

[Portal.zip](http://files.cnblogs.com/libingql/Portal.zip)

**5、参考资料**

http://msdn.microsoft.com/en-US/data/jj591621

# 添加到 ConfigureServices 方法：

```bash

Add-Migration InitialCreate -Context FirstDbContext -OutputDir Migrations\FirstDbContextMigrations
Update-Database -Context FirstDbContext

Add-Migration InitialCreate -Context SecondDbContext -OutputDir Migrations\SecondDbContextMigrations
Update-Database -Context SecondDbContext
```

```csharp
public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<cookiepolicyoptions>(options =>
        {
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });
 
 
        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
 
        var connection = @"Server=你的数据库地址;Database=FirstDbContext;User Id=你的数据库账号;Password=你的数据库密码;"; // 手动高亮
        services.AddDbContext<firstdbcontext> // 手动高亮
            (options => options.UseSqlServer(connection)); // 手动高亮
 
        var secondDbconnection = @"Server=你的数据库地址;Database=SecondDbContext;User Id=你的数据库账号;Password=你的数据库密码;"; // 手动高亮
        services.AddDbContext<seconddbcontext> // 手动高亮
            (options => options.UseSqlServer(secondDbconnection)); // 手动高亮
    }</seconddbcontext></firstdbcontext></cookiepolicyoptions>
```









```bash




Update-Database [-SourceMigration <String>] [-TargetMigration <String>] [-Script] [-Force] 
  [-ProjectName <String>] [-StartUpProjectName <String>] [-ConfigurationTypeName <String>] 
  [-ConnectionStringName <String>] [-AppDomainBaseDirectory <String>] [<CommonParameters>]

Update-Database [-SourceMigration <String>] [-TargetMigration <String>] [-Script] [-Force] 
  [-ProjectName <String>] [-StartUpProjectName <String>] [-ConfigurationTypeName <String>] 
  -ConnectionString <String> -ConnectionProviderName <String> 
  [-AppDomainBaseDirectory <String>] [<CommonParameters>]
```





```
 Enable-Migrations -ContextTypeName Portal.PortalContext
 
 ConfigurationDbContext
  Enable-Migrations -ContextTypeName ConfigurationDbContext
  
  
  Update-Database --context ConfigurationDbContext
```