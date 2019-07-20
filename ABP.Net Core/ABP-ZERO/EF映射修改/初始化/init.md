



```csharp
//AppRoleConfig--C:\GitHub\github.DGMicro\The-Thousand-and-One-Home\IdentityServerDemo\IdentityServerDemo\src\Research.Core\Authorization\Roles\AppRoleConfig

using Abp.MultiTenancy;
using Abp.Zero.Configuration;

namespace Research.Authorization.Roles
{
    public static class AppRoleConfig
    {
        public static void Configure(IRoleManagementConfig roleManagementConfig)
        {
            ////Static host roles

            //roleManagementConfig.StaticRoles.Add(
            //    new StaticRoleDefinition(
            //        StaticRoleNames.Host.Admin,
            //        MultiTenancySides.Host)
            //    );

            ////Static tenant roles

            //roleManagementConfig.StaticRoles.Add(
            //    new StaticRoleDefinition(
            //        StaticRoleNames.Tenants.Admin,
            //        MultiTenancySides.Tenant)
            //    );
        }
    }
}

```





System.NullReferenceException
  HResult=0x80004003
  Message=Object reference not set to an instance of an object.
  Source=Research.EntityFrameworkCore
  StackTrace:
   at Research.EntityFrameworkCore.Seed.Tenants.TenantRoleAndUserBuilder.CreateRolesAndUsers()
   at Research.EntityFrameworkCore.Seed.SeedHelper.WithDbContext[TDbContext](IIocResolver iocResolver, Action`1 contextAction)
   at System.Collections.Generic.List`1.ForEach(Action`1 action)
   at Abp.AbpBootstrapper.Initialize()
   at Abp.AspNetCore.AbpApplicationBuilderExtensions.InitializeAbp(IApplicationBuilder app)
   at Abp.AspNetCore.AbpApplicationBuilderExtensions.UseAbp(IApplicationBuilder app, Action`1 optionsAction)
   at Research.Web.Host.Startup.Startup.Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at Microsoft.AspNetCore.Hosting.ConventionBasedStartup.Configure(IApplicationBuilder app)
   at Microsoft.AspNetCore.Hosting.Internal.WebHost.BuildApplication()
   at Microsoft.AspNetCore.Hosting.Internal.WebHost.<StartAsync>d__26.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.WebHostExtensions.<RunAsync>d__5.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.WebHostExtensions.<RunAsync>d__4.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.WebHostExtensions.Run(IWebHost host)
   at Research.Web.Host.Startup.Program.Main(String[] args)