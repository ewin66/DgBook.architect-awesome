

```
dotnet new mvc -n EFGetStarted.AspNetCore.NewDb

cd EFGetStarted.AspNetCore.NewDb
```





```powershell
Add-Migration InitialCreate
Update-Database


dotnet ef migrations add InitialCreate
dotnet ef database update


```



## 创建控制器

```bash
dotnet tool install -g dotnet-aspnet-codegenerator
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet restore
dotnet aspnet-codegenerator controller -name BlogsController -m Blog -dc BloggingContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries

dotnet run
```

---





ConfigurationDbContext

##	使用依赖注入注册上下文

服务（例如 BloggingContext）在应用程序启动期间通过依赖关系注入进行注册。 需要这些服务的组件（如 MVC 控制器）可以通过向构造函数或属性添加相关参数来获得对应服务。

若要使 BloggingContext 对 MVC 控制器可用，请将其注册为服务。

    在 Startup.cs 中，添加以下 using 语句：
    
        using EFGetStarted.AspNetCore.NewDb.Models;
         
        using Microsoft.EntityFrameworkCore;


​     

    将以下突出显示的代码添加到 ConfigureServices 方法：
    
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
         
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            //连接数据库
            var connection = "Data Source=blogging.db";
            services.AddDbContext<BloggingContext>
                (options => options.UseSqlite(connection));
            // BloggingContext requires
            // using EFGetStarted.AspNetCore.NewDb.Models;
            // UseSqlite requires
            // using Microsoft.EntityFrameworkCore;
        }
    
    生产应用通常会将连接字符串放在配置文件或环境变量中。 
---------------------
作者：后跳闪到腰 
来源：CSDN 
原文：https://blog.csdn.net/a1034386099/article/details/88634536 
版权声明：本文为博主原创文章，转载请附上博文链接！



##	 反向工程



https://docs.microsoft.com/zh-cn/ef/core/managing-schemas/scaffolding

 

````powershell
Scaffold-DbContext 'Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chinook' Microsoft.EntityFrameworkCore.SqlServer

dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chinook" Microsoft.EntityFrameworkCore.SqlServer
可以通过将 -Tables 参数添加到上述命令来指定要为哪些表生成实体。 例如 -Tables Blog,Post。

Scaffold-DbContext ... -Tables Artist, Album
dotnet ef dbcontext scaffold ... --table Artist --table Album


Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

````



## 目录和命名空间

```bash
Scaffold-DbContext ... -ContextDir Data -OutputDir Models

dotnet ef dbcontext scaffold ... --context-dir Data --output-dir Models
```





```
dotnet user-secrets set ConnectionStrings.Chinook "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chinook"
dotnet ef dbcontext scaffold Name=Chinook Microsoft.EntityFrameworkCore.SqlServer
```

