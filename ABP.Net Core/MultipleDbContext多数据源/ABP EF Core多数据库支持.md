

# ABP EF Core多数据库支持

​                                                   2018年12月26日 16:29:54           [娃都会打酱油了](https://me.csdn.net/starfd)           阅读数：1243                                                                  

​                   

   版权声明：本文为starfd原创文章，转载请标明出处。          https://blog.csdn.net/starfd/article/details/85264434        



[[GitHub]](https://github.com/fdstar/TaobaoAuthorization/tree/master/src/TaobaoAuthorization.EntityFrameworkCore)

ABP官方[Demo](https://github.com/aspnetboilerplate/aspnetboilerplate-samples/tree/master/MultipleDbContextEfCoreDemo)中提供了在EF Core中如何实现多数据库支持的例子，但Demo说明文档中对于要做哪些修改没做说明，所以本文在此做下说明。

首先要说明的是（我这边通过官方模板生成时输入的项目名称为`TaobaoAuthorization`），除了`ConnectionStringName`声明是在`TaobaoAuthorizationConsts`中外，所有修改均在`TaobaoAuthorization.EntityFrameworkCore`项目中。

为了支持多数据库，你需要做以下调整

- 将`DbContextOptionsConfigurer`的`Configure`方法修改为泛型方法，这样你就不需要像官方Demo那样实现两个`DbContextOptionsConfigurer`

```csharp
    public static class DbContextOptionsConfigurer
    {
        public static void Configure<T>(
            DbContextOptionsBuilder<T> dbContextOptions,
            string connectionString
            )
            where T : AbpDbContext
        {
            /* This is the single point to configure DbContextOptions for TaobaoAuthorizationDbContext */
            //dbContextOptions.UseSqlServer(connectionString);
            dbContextOptions.UseMySql(connectionString);
        }

        public static void Configure<T>(
            DbContextOptionsBuilder<T> dbContextOptions,
            DbConnection connection
            )
            where T : AbpDbContext
        {
            /* This is the single point to configure DbContextOptions for TaobaoAuthorizationDbContext */
            //dbContextOptions.UseSqlServer(connectionString);
            dbContextOptions.UseMySql(connection);
        }
    }
123456789101112131415161718192021222324
```

- 增加其它数据库对应的`DbContext`，这里除了默认的`TaobaoAuthorizationDbContext`外，我们再新增了`TaobaoAuthorizedDbContext`
- 调整并新增其他数据库对应的`DbContextFactory`，这里还是提取了一个抽象类出来

```csharp
    /// <summary>
    /// EF Core PMC commands 基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DbContextFactory<T> : IDesignTimeDbContextFactory<T>
        where T : AbpDbContext
    {
        /// <summary>
        /// 要采用的数据库连接节点名称
        /// </summary>
        public abstract string ConnectionStringName { get; }
        /// <summary>
        /// 创建DbContext实例
        /// 如果觉得每个数据库都要自己new太low，那么可以采用反射来动态创建，毕竟这里也只是PMC command使用的
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public abstract T CreateDbContext(DbContextOptions<T> options);
        public T CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<T>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(ConnectionStringName)
            );
            return this.CreateDbContext(builder.Options);
        }
    }
1234567891011121314151617181920212223242526272829
```

然后将原先官网生成的相应`DbContextFactory`进行调整，如果还有其它数据库也只要继承`DbContextFactory<T>`即可

```csharp
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class TaobaoAuthorizationDbContextFactory : DbContextFactory<TaobaoAuthorizationDbContext>
    {//第一个数据库配置
        public override string ConnectionStringName => TaobaoAuthorizationConsts.DefaultConnectionStringName;

        public override TaobaoAuthorizationDbContext CreateDbContext(DbContextOptions<TaobaoAuthorizationDbContext> options)
        {
            return new TaobaoAuthorizationDbContext(options);
        }
    }

    public class TaobaoAuthorizedDbContextFactory : DbContextFactory<TaobaoAuthorizedDbContext>
    {//第二个数据库配置
        public override string ConnectionStringName => TaobaoAuthorizationConsts.AuthorizedConnectionStringName;

        public override TaobaoAuthorizedDbContext CreateDbContext(DbContextOptions<TaobaoAuthorizedDbContext> options)
        {
            return new TaobaoAuthorizedDbContext(options);
        }
    }
1234567891011121314151617181920
```

- 增加`MyConnectionStringResolver`，ABP如何分辨应该用哪个数据库连接就是在这里定义的，当然这里还是在官网Demo的基础上略作调整，实际可以考虑增加`Dictionary<Type,string>`来实现`DbContext`与配置中`ConnectionStringName`的映射关系

```csharp
    public class MyConnectionStringResolver : DefaultConnectionStringResolver
    {
        public MyConnectionStringResolver(IAbpStartupConfiguration configuration)
            : base(configuration)
        {
        }

        public override string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {
            var connectStringName = this.GetConnectionStringName(args);
            if (connectStringName != null)
            {
                var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());
                return configuration.GetConnectionString(connectStringName);
            }
            return base.GetNameOrConnectionString(args);
        }
        private string GetConnectionStringName(ConnectionStringResolveArgs args)
        {
            var type = args["DbContextConcreteType"] as Type;
            if (type == typeof(TaobaoAuthorizedDbContext))
            {
                return TaobaoAuthorizationConsts.AuthorizedConnectionStringName;//返回数据库二的节点名称
            }
            return null;//采用默认数据库
        }
    }
123456789101112131415161718192021222324252627
```

- 调整模板生成的`EntityFrameworkCoreModule`，增加`PreInitialize`实现，注意需要添加引用`using Abp.Configuration.Startup`，如果你还有其它数据库连接，那么也只要再依样新增`AddDbContext`即可

```csharp
        public override void PreInitialize()
        {
            base.PreInitialize();
            Configuration.ReplaceService<IConnectionStringResolver, MyConnectionStringResolver>();
            this.AddDbContext<TaobaoAuthorizationDbContext>();//数据库一
            this.AddDbContext<TaobaoAuthorizedDbContext>();//数据库二
        }
        private void AddDbContext<TDbContext>()
            where TDbContext: AbpDbContext
        {
            Configuration.Modules.AbpEfCore().AddDbContext<TDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    DbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    DbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                }
            });
        }
12345678910111213141516171819202122
```

至此你就已经完成了ABP多数据库支持的代码调整工作，上述代码均可在[这里](https://github.com/fdstar/TaobaoAuthorization/tree/master/src/TaobaoAuthorization.EntityFrameworkCore)找到。