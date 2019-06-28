





##                                   Create                                 a New Solution                             

```markup
abp new Acme.BookStore
```

Markup

Copy

*You can use different level of namespaces; e.g. BookStore, Acme.BookStore or Acme.Retail.BookStore.*

​                                 **new** command creates a **layered MVC application**                                 with **Entity Framework Core** as the database provider.                                 However, it has additional options. Examples:                             

```markup
# Use MongoDB as the database provider
abp new Acme.BookStore -d mongodb
```

Markup

Copy

```markup
# Create a solution based on the tiered architecture
abp new Acme.BookStore --tiered


abp new Fooww.Research --tiered
Fooww.Research
```

Markup

Copy

```markup
# Create a module template
abp new Acme.BookStore.Payment -t mvc-module
```

Markup

Copy

See the [CLI documentation](https://docs.abp.io/en/abp/latest/CLI) for all available templates and options.





###	New User

{
  "password": "123456",
  "userName": "wsx2019",
  "name": "string",
  "surname": "string",
  "email": "wsxs@qq.com",
  "phoneNumber": "18896566053",
  "twoFactorEnabled": true,
  "lockoutEnabled": true,
  "roleNames": [ 
  ]
}s



----



### Error

System.Net.Http.HttpRequestException
  HResult=0x80004005
  Message=由于目标计算机积极拒绝，无法连接。
  Source=System.Net.Http
  StackTrace:
   at System.Net.Http.ConnectHelper.<ConnectAsync>d__2.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Threading.Tasks.ValueTask`1.get_Result()
   at System.Net.Http.HttpConnectionPool.<CreateConnectionAsync>d__44.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Threading.Tasks.ValueTask`1.get_Result()
   at System.Net.Http.HttpConnectionPool.<WaitForCreatedConnectionAsync>d__49.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Threading.Tasks.ValueTask`1.get_Result()
   at System.Net.Http.HttpConnectionPool.<SendWithRetryAsync>d__39.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1.ConfiguredTaskAwaiter.GetResult()
   at System.Net.Http.RedirectHandler.<SendAsync>d__4.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1.ConfiguredTaskAwaiter.GetResult()
   at System.Net.Http.DiagnosticsHandler.<SendAsync>d__2.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   at Microsoft.Extensions.Http.Logging.LoggingHttpMessageHandler.<SendAsync>d__2.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   at Microsoft.Extensions.Http.Logging.LoggingScopeHttpMessageHandler.<SendAsync>d__2.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1.ConfiguredTaskAwaiter.GetResult()
   at System.Net.Http.HttpClient.<FinishSendAsyncBuffered>d__62.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Volo.Abp.Http.Client.DynamicProxying.ApiDescriptionCache.<GetFromServerAsync>d__5.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Volo.Abp.Http.Client.DynamicProxying.ApiDescriptionCache.<GetAsync>d__4.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Volo.Abp.Http.Client.DynamicProxying.ApiDescriptionFinder.<FindActionAsync>d__2.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Volo.Abp.Http.Client.DynamicProxying.DynamicHttpProxyInterceptor`1.<MakeRequestAsync>d__42.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Volo.Abp.Http.Client.DynamicProxying.DynamicHttpProxyInterceptor`1.<MakeRequestAndGetResultAsync>d__41`1.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpInterceptorAdapter`1.<ExecuteWithReturnValueAsync>d__8`1.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Volo.Abp.AspNetCore.Mvc.Client.CachedApplicationConfigurationClient.<<GetAsync>b__14_0>d.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Volo.Abp.Caching.DistributedCache`1.<GetOrAddAsync>d__34.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Volo.Abp.AspNetCore.Mvc.Client.CachedApplicationConfigurationClient.<GetAsync>d__14.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Volo.Abp.AspNetCore.Mvc.Client.RemoteLanguageProvider.<GetLanguagesAsync>d__4.MoveNext()
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Nito.AsyncEx.Synchronous.TaskExtensions.WaitAndUnwrapException[TResult](Task`1 task)
   at System.Threading.Tasks.ContinuationResultTaskFromResultTask`2.InnerInvoke()
   at System.Threading.ExecutionContext.RunInternal(ExecutionContext executionContext, ContextCallback callback, Object state)
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Threading.Tasks.Task.ExecuteWithThreadLocal(Task& currentTaskSlot)
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Nito.AsyncEx.Synchronous.TaskExtensions.WaitAndUnwrapException[TResult](Task`1 task)
   at Nito.AsyncEx.AsyncContext.Run[TResult](Func`1 action)
   at Microsoft.AspNetCore.Builder.AbpApplicationBuilderExtensions.UseAbpRequestLocalization(IApplicationBuilder app, Action`1 optionsAction)
   at Fooww.Research.Web.ResearchWebModule.OnApplicationInitialization(ApplicationInitializationContext context) in C:\GitHub\github.DGMicro\Hundred-Micro\dgHundred\Research(5)\src\Fooww.Research.Web\ResearchWebModule.cs:line 216
   at Volo.Abp.Modularity.ModuleManager.InitializeModules(ApplicationInitializationContext context)
   at Volo.Abp.AbpApplicationBase.InitializeModules()
   at Fooww.Research.Web.Startup.Configure(IApplicationBuilder app, ILoggerFactory loggerFactory) in C:\GitHub\github.DGMicro\Hundred-Micro\dgHundred\Research(5)\src\Fooww.Research.Web\Startup.cs:line 23

内部异常 1:
SocketException: 由于目标计算机积极拒绝，无法连接。