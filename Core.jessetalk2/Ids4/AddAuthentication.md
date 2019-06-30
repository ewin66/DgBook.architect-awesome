







##	 [IdentityServer4（7）- 使用客户端认证控制API访问（客户端授权模式）](https://www.cnblogs.com/stulzq/p/7495129.html)

## 七.添加[API](https://www.cnblogs.com/stulzq/p/7495129.html)

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvcCore()
            .AddAuthorization()
            .AddJsonFormatters(); 
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "http://localhost:5000";
                options.RequireHttpsMetadata = false;

                options.Audience = "api1";
            });
    } 
    
    public void Configure(IApplicationBuilder app)
    {
        app.UseAuthentication(); 
        app.UseMvc();
    }
}
```

