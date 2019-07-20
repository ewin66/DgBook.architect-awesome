

```
dotnet new mvc -n EFGetStarted.AspNetCore.NewDb
cd EFGetStarted.AspNetCore.NewDb
```



```bash
dotnet new webapi --name IdentityServerCenter --no-https

dotnet restore 

for IdentityServerCenter
install-package IdentityServer4
install-package IdentityServer4.AccessTokenValidation

dotnet run 

mkdir WebApp
cd WebApp
dotnet new web #可以用 dotnet new --help 看帮助
dotnet run     #直接运行，此时可以在本机浏览器查看 http://localhost:5000
 
dotnet publish   #发布
cd bin/Debug/netcoreapp2.1/publish/
dotnet WebApp.dll
--------------------- 
作者：吉普赛的歌 	来源：CSDN <不写代码，生成一个.net core空网站>
原文：https://blog.csdn.net/yenange/article/details/81675142 
版权声明：本文为博主原创文章，转载请附上博文链接
```



```powershell
Add-Migration InitialCreate
Update-Database


dotnet ef migrations add InitialCreate
dotnet ef database update


```



##	用VSCode新建webapi项目[JwtAuthSample](https://www.cnblogs.com/wyt007/p/8183085.html)，

并打开所在文件夹项目

```bash
dotnet new webapi --name JwtAuthSample
dotnet new console --name PwdClient
dotnet new webapi --name JwtAuthSample --no-https
```

编辑JwtAuthSample.csproj，添加watch

```
<DotNetCliToolReference Include="Microsoft.DotNet.Watcher.Tools" Version="2.0.0" />
```

重新生成一下项目

```
dotnet restore
```

然后运行

```
dotnet watch run
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

##	 EF Core Scaffold-DbContext 注意点【$前加上`】

https://www.jianshu.com/p/65b34b1e8407

```
Scaffold-DbContext "Server=xxx.xxx.xxx.xxx;Database=Blogging;User ID=sa;Password=$xxxxx;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
```

以上是我的命令
 执行时，总是提示我

```bash
System.Data.SqlClient.SqlException (0x80131904): 用户 'sa' 登录失败。
```

后来发现，密码中包含$符号，所以需要在$符号前加上`，即：

```bash
Scaffold-DbContext "Server=xxx.xxx.xxx.xxx;Database=Blogging;User ID=sa;Password=`$xxxxx;" Micros
```





## 目录和命名空间

```bash
Scaffold-DbContext ... -ContextDir Data -OutputDir Models

dotnet ef dbcontext scaffold ... --context-dir Data --output-dir Models
```





```
dotnet user-secrets set ConnectionStrings.Chinook "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chinook"
dotnet ef dbcontext scaffold Name=Chinook Microsoft.EntityFrameworkCore.SqlServer
```

