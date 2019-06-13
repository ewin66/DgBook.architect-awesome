# [ASP.NET Core WebAPI中使用JWT Bearer认证和授权](https://www.cnblogs.com/royzshare/p/10114198.html)





# [ASP .NET Core 基本知识点示例 目录](https://www.cnblogs.com/royzshare/p/9547683.html)

> 1. ASP.NET Core 的 **运行机制** [文章](https://www.cnblogs.com/royzshare/p/9442666.html)
> 2. ASP.NET Core 中的 **配置** [文章](https://www.cnblogs.com/royzshare/p/9436018.html) [源代码](https://github.com/zdz72113/NETCore_BasicKnowledge.Examples/tree/master/ConfigrationDemo)
> 3. ASP.NET Core 中的 **依赖注入** [文章](https://www.cnblogs.com/royzshare/p/9440914.html) [源代码](https://github.com/zdz72113/NETCore_BasicKnowledge.Examples/tree/master/DIDemo)
> 4. ASP.NET Core 中的 **日志** [文章](https://www.cnblogs.com/royzshare/p/9454625.html) [源代码](https://github.com/zdz72113/NETCore_BasicKnowledge.Examples/tree/master/LogDemo)
> 5. ASP.NET Core 中的 **缓存** [文章](https://www.cnblogs.com/royzshare/p/9474740.html) [源代码](https://github.com/zdz72113/NETCore_BasicKnowledge.Examples/tree/master/CacheDemo)
> 6. ASP.NET Core 中的 **ORM 之 Dapper** [文章](https://www.cnblogs.com/royzshare/p/9522127.html) [源代码](https://github.com/zdz72113/NETCore_BasicKnowledge.Examples/tree/master/ORMDemo)
> 7. ASP.NET Core 中的 **ORM 之 Entity Framework** [文章](https://www.cnblogs.com/royzshare/p/9686706.html) [源代码](https://github.com/zdz72113/NETCore_BasicKnowledge.Examples/tree/master/ORMDemo)
> 8. EF Core 实现多租户 [文章](https://www.cnblogs.com/royzshare/p/9958888.html#4115720) [源代码](https://github.com/zdz72113/NETCore_BasicKnowledge.Examples/tree/master/ORMDemo/ORMDemo.MultiTenancy)
> 9. ASP.NET Core 中的 **ORM 之 AutoMapper** [文章](https://www.cnblogs.com/royzshare/p/9951683.html) [源代码](https://github.com/zdz72113/NETCore_BasicKnowledge.Examples/tree/master/ORMDemo/ORMDemo.AutoMapperTest)
> 10. .NET Core 中的**通用主机和后台服务** [文章](https://www.cnblogs.com/royzshare/p/10083453.html) [源代码](https://github.com/zdz72113/NETCore_BasicKnowledge.Examples/tree/master/GenericHostDemo)
> 11. ASP.NET Core WebAPI中使用**JWT Bearer认证和授权** [文章](https://www.cnblogs.com/royzshare/p/10114198.html) [源代码](https://github.com/zdz72113/NETCore_BasicKnowledge.Examples/tree/master/SecurityDemo/SecurityDemo.Authentication.JWT)
> 12. ASP.NET Core 中的实时框架 **SingalR** [文章]<https://www.cnblogs.com/royzshare/p/10194326.html>) [源代码](https://github.com/zdz72113/NETCore_BasicKnowledge.Examples/tree/master/SignalRDemo)
>
> 
>
> 分类: [NET Core Basic](https://www.cnblogs.com/royzshare/category/1244163.html)

目录

- [为什么是 JWT Bearer](https://www.cnblogs.com/royzshare/p/10114198.html#为什么是-jwt-bearer)
- [什么是 JWT](https://www.cnblogs.com/royzshare/p/10114198.html#什么是-jwt)
- [JWT 的优缺点](https://www.cnblogs.com/royzshare/p/10114198.html#jwt-的优缺点)
- [在 WebAPI 中使用 JWT 认证](https://www.cnblogs.com/royzshare/p/10114198.html#在-webapi-中使用-jwt-认证)
- [刷新 Token](https://www.cnblogs.com/royzshare/p/10114198.html#刷新-token)
- 使用授权
  - [简单授权](https://www.cnblogs.com/royzshare/p/10114198.html#简单授权)
  - [基于固定角色的授权](https://www.cnblogs.com/royzshare/p/10114198.html#基于固定角色的授权)
  - [基于策略的授权](https://www.cnblogs.com/royzshare/p/10114198.html#基于策略的授权)
  - [自定义策略授权](https://www.cnblogs.com/royzshare/p/10114198.html#自定义策略授权)
  - [基于资源的授权](https://www.cnblogs.com/royzshare/p/10114198.html#基于资源的授权)
- [源代码](https://www.cnblogs.com/royzshare/p/10114198.html#源代码)
- [参考](https://www.cnblogs.com/royzshare/p/10114198.html#参考)

# 为什么是 JWT Bearer

ASP.NET Core 在 Microsoft.AspNetCore.Authentication 下实现了一系列认证, 包含 `Cookie`, `JwtBearer`, `OAuth`, `OpenIdConnect` 等,

- Cookie 认证是一种比较常用本地认证方式, 它由浏览器自动保存并在发送请求时自动附加到请求头中, 更适用于 MVC 等纯网页系统的本地认证.
- OAuth & OpenID Connect 通常用于运程认证, 创建一个统一的认证中心, 来统一配置和处理对于其他资源和服务的用户认证及授权.
- JwtBearer 认证中, 客户端通常将 JWT(一种Token) 通过 HTTP 的 Authorization header 发送给服务端, 服务端进行验证. 可以方便的用于 WebAPI 框架下的本地认证.
   当然, 也可以完全自己实现一个WebAPI下基于Token的本地认证, 比如自定义Token的格式, 自己写颁发和验证Token的代码等. 这样的话通用性并不好, 而且也需要花费更多精力来封装代码以及处理细节.

# 什么是 JWT

JWT (JSON Web Token) 是一种基于JSON的、用于在网络上声明某种主张的令牌（token）。
 作为一个开放的标准（RFC 7519），定义了一种简洁的、自包含的方法，从而使通信双方实现以JSON对象的形式安全的传递信息。

JWT通常由三部分组成: 头信息（header）, 消息体（payload）和签名（signature）。
 头信息指定了该JWT使用的签名算法：

```
header = {"alg": "HS256", "typ": "JWT"}
```

消息体包含了JWT的意图：

```
payload = {"sub": "1234567890", "name": "John Doe", "iat": 1516239022}
```

未签名的令牌由base64url编码的头信息和消息体拼接而成（使用"."分隔），签名则通过私有的key计算而成：

```
key = "secretkey" 
unsignedToken = encodeBase64(header) + '.' + encodeBase64(payload)  
signature = HMAC-SHA256(key, unsignedToken) 
```

最后在尾部拼接上base64url编码的签名（同样使用"."分隔）就是JWT了：

```
token = encodeBase64(header) + '.' + encodeBase64(payload) + '.' + encodeBase64(signature) 
```

JWT常常被用作保护服务端的资源，客户端通常将JWT通过HTTP的Authorization header发送给服务端，服务端使用自己保存的key计算、验证签名以判断该JWT是否可信。

```
Authorization: Bearer <token>
```

# JWT 的优缺点

相比于传统的 cookie-session 认证机制，优点有：

1. **更适用分布式和水平扩展**
    在cookie-session方案中，cookie内仅包含一个session标识符，而诸如用户信息、授权列表等都保存在服务端的session中。如果把session中的认证信息都保存在JWT中，在服务端就没有session存在的必要了。当服务端水平扩展的时候，就不用处理session复制（session  replication）/ session黏连（sticky session）或是引入外部session存储了。
2. **适用于多客户端（特别是移动端）的前后端解决方案**
    移动端使用的往往不是网页技术，使用Cookie验证并不是一个好主意，因为你得和Cookie容器打交道，而使用Bearer验证则简单的多。
3. **无状态化**
    JWT 是无状态化的，更适用于 RESTful 风格的接口验证。

它的缺点也很明显：

1. **更多的空间占用**
    JWT 由于Payload里面包含了附件信息，占用空间往往比SESSION ID大，在HTTP传输中会造成性能影响。所以在设计时候需要注意不要在JWT中存储太多的claim,以避免发生巨大的,过度膨胀的请求。
2. **无法作废已颁布的令牌**
    所有的认证信息都在JWT中，由于在服务端没有状态，即使你知道了某个JWT被盗取了，你也没有办法将其作废。在JWT过期之前（你绝对应该设置过期时间），你无能为力。

# 在 WebAPI 中使用 JWT 认证

1. 定义配置类 JwtIssuerOptions.cs

   ```
   public class JwtIssuerOptions
   {
       /// <summary>
       /// 4.1.1.  "iss" (Issuer) Claim - The "iss" (issuer) claim identifies the principal that issued the JWT.
       /// </summary>
       public string Issuer { get; set; }
   
       /// <summary>
       /// 4.1.2.  "sub" (Subject) Claim - The "sub" (subject) claim identifies the principal that is the subject of the JWT.
       /// </summary>
       public string Subject { get; set; }
   
       /// <summary>
       /// 4.1.3.  "aud" (Audience) Claim - The "aud" (audience) claim identifies the recipients that the JWT is intended for.
       /// </summary>
       public string Audience { get; set; }
   
       /// <summary>
       /// 4.1.4.  "exp" (Expiration Time) Claim - The "exp" (expiration time) claim identifies the expiration time on or after which the JWT MUST NOT be accepted for processing.
       /// </summary>
       public DateTime Expiration => IssuedAt.Add(ValidFor);
   
       /// <summary>
       /// 4.1.5.  "nbf" (Not Before) Claim - The "nbf" (not before) claim identifies the time before which the JWT MUST NOT be accepted for processing.
       /// </summary>
       public DateTime NotBefore => DateTime.UtcNow;
   
       /// <summary>
       /// 4.1.6.  "iat" (Issued At) Claim - The "iat" (issued at) claim identifies the time at which the JWT was issued.
       /// </summary>
       public DateTime IssuedAt => DateTime.UtcNow;
   
       /// <summary>
       /// Set the timespan the token will be valid for (default is 120 min)
       /// </summary>
       public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(120);
   
   
       /// <summary>
       /// "jti" (JWT ID) Claim (default ID is a GUID)
       /// </summary>
       public Func<Task<string>> JtiGenerator =>
         () => Task.FromResult(Guid.NewGuid().ToString());
   
       /// <summary>
       /// The signing key to use when generating tokens.
       /// </summary>
       public SigningCredentials SigningCredentials { get; set; }
   }
   ```

2. 定义的帮助类 JwtFactory.cs, 主要是用于生成Token

   ```
   public interface IJwtFactory
   {
       Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
       ClaimsIdentity GenerateClaimsIdentity(User user);
   }
   
   public class JwtFactory : IJwtFactory
   {
       private readonly JwtIssuerOptions _jwtOptions;
   
       public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
       {
           _jwtOptions = jwtOptions.Value;
           ThrowIfInvalidOptions(_jwtOptions);
       }
   
       public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
       {
           var claims = new List<Claim>
           {
               new Claim(JwtRegisteredClaimNames.Sub, userName),
               new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
               new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
               identity.FindFirst(ClaimTypes.Name),
               identity.FindFirst("id")
           };
           claims.AddRange(identity.FindAll(ClaimTypes.Role));
   
           // Create the JWT security token and encode it.
           var jwt = new JwtSecurityToken(
               issuer: _jwtOptions.Issuer,
               audience: _jwtOptions.Audience,
               claims: claims,
               notBefore: _jwtOptions.NotBefore,
               expires: _jwtOptions.Expiration,
               signingCredentials: _jwtOptions.SigningCredentials);
   
           var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
   
           var response = new
           {
               auth_token = encodedJwt,
               expires_in = (int)_jwtOptions.ValidFor.TotalSeconds,
               token_type = "Bearer"
           };
   
           return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
       }
   
       public ClaimsIdentity GenerateClaimsIdentity(User user)
       {
           var claimsIdentity  = new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"));
           claimsIdentity.AddClaim(new Claim("id", user.Id.ToString()));
           claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
           foreach (var role in user.Roles)
           {
               claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
           }
           return claimsIdentity;
       }
   
       /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
       private static long ToUnixEpochDate(DateTime date)
         => (long)Math.Round((date.ToUniversalTime() -
                              new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                             .TotalSeconds);
   
       private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
       {
           if (options == null) throw new ArgumentNullException(nameof(options));
   
           if (options.ValidFor <= TimeSpan.Zero)
           {
               throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
           }
   
           if (options.SigningCredentials == null)
           {
               throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
           }
   
           if (options.JtiGenerator == null)
           {
               throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
           }
       }
   }
   ```

3. 在 Startup.cs 里面添加相关代码:

   读取配置:

   ```
   var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
   services.Configure<JwtIssuerOptions>(options =>
   {
       options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
       options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
       options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
   }); 
   ```

   JwtBearer验证:

   ```
   public class Startup
   {
       private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
       private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SecretKey));
   
       public void ConfigureServices(IServiceCollection services)
       {
           services.AddSingleton<IJwtFactory, JwtFactory>();
   
           services.AddAuthentication(options =>
           {
               options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
   
           }).AddJwtBearer(configureOptions =>
           {
               configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
               configureOptions.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],
                   ValidateAudience = true,
                   ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = _signingKey,
                   RequireExpirationTime = false,
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.Zero
               };
               configureOptions.SaveToken = true;
           });
       }
   
       public void Configure(IApplicationBuilder app, IHostingEnvironment env)
       {
           app.UseAuthentication();
           app.UseMvc();
       }
   }
   ```

   Swagger相关:

   ```
   services.AddSwaggerGen(options =>
   {
       var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
       options.AddSecurityRequirement(security);
       options.AddSecurityDefinition("Bearer", new Swashbuckle.AspNetCore.Swagger.ApiKeyScheme
       {
           Description = "Format: Bearer {auth_token}",
           Name = "Authorization",
           In = "header"
       });
   });
   ```

4. 创建一个控制器 AuthController.cs，用来提供签发 Token 的 API

   ```
   [Route("api/[controller]")]
   [ApiController]
   public class AuthController : ControllerBase
   {
       private readonly IJwtFactory _jwtFactory;
       private readonly JwtIssuerOptions _jwtOptions;
   
       public AuthController(IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
       {
           _jwtFactory = jwtFactory;
           _jwtOptions = jwtOptions.Value;
       }
   
       /// <summary>
       /// Log in
       /// </summary>
       /// <param name="request"></param>
       /// <returns></returns>
       [HttpPost("[action]")]
       public async Task<IActionResult> Login([FromBody]LoginRequest request)
       {
           var users = TestUsers.Users.Where(r => r.UserName.Equals(request.UserName));
           if (users.Count() <= 0)
           {
               ModelState.AddModelError("login_failure", "Invalid username.");
               return BadRequest(ModelState);
           }
           var user = users.First();
           if (!request.Password.Equals(user.Password))
           {
               ModelState.AddModelError("login_failure", "Invalid password.");
               return BadRequest(ModelState);
           }
   
           var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(user.UserName, user.Id.ToString());
           var token = await _jwtFactory.GenerateEncodedToken(user.UserName, claimsIdentity);
           return new OkObjectResult(token);
       }
   
       /// <summary>
       /// Get User Info
       /// </summary>
       /// <returns></returns>
       [HttpGet("[action]")]
       [Authorize]
       public IActionResult GetUserInfo()
       {
           var claimsIdentity = User.Identity as ClaimsIdentity;
           return Ok(claimsIdentity.Claims.ToList().Select(r=> new { r.Type, r.Value}));
       }
   }
   ```

5. 为需要保护的API添加 `[Authorize]` 特性

   ```
   [Route("api/[controller]")]
   [ApiController]
   [Authorize]
   public class ValuesController : ControllerBase
   {
       // GET api/values
       [HttpGet]
       public ActionResult<IEnumerable<string>> Get()
       {
           return new string[] { "value1", "value2" };
       }
   }
   ```

6. 使用 Swagger UI 或者 PostMan 等工具测试

   获取Token:

   ```
   curl -X POST "http://localhost:5000/api/Auth/Login" -H "accept: application/json" -H "Content-Type: application/json-patch+json" -d "{ \"userName\": \"Paul\", \"password\": \"Paul123\"}"
   ```

   返回值:

   ```
   "{\r\n  \"auth_token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJQYXVsIiwianRpIjoiM2I1YzEyMzMtZTI1YS00ZWU5LWJkNjYtY2Y0NjU2YWMzM2QzIiwiaWF0IjoxNTQ0NTg5ODY5LCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiUGF1bCIsImlkIjoiZDM3ZjI3Y2UtODc4MC00NDI1LTkxMzUtYjY4OGE3NmM0YzBmIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbImFkbWluaXN0cmF0b3IiLCJhcGlfYWNjZXNzIl0sIm5iZiI6MTU0NDU4OTg2OCwiZXhwIjoxNTQ0NTk3MDY4LCJpc3MiOiJTZWN1cml0eURlbW8uQXV0aGVudGljYXRpb24uSldUIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwLyJ9.UAWLYQ5lA6xWofWIjGsPGWtAMHEtqZSfrfVaBui2mKI\",\r\n  \"expires_in\": 7200,\r\n  \"token_type\": \"Bearer\"\r\n}"
   ```

   在 <https://jwt.io/> 上解析 Token 如下:

   ```
   {
     "sub": "Paul",
     "jti": "3b5c1233-e25a-4ee9-bd66-cf4656ac33d3",
     "iat": 1544589869,
     "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name": "Paul",
     "id": "d37f27ce-8780-4425-9135-b688a76c4c0f",
     "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": ["administrator","api_access"],
     "nbf": 1544589868,
     "exp": 1544597068,
     "iss": "SecurityDemo.Authentication.JWT",
     "aud": "http://localhost:5000/"
   }
   ```

   使用 Token 访问受保护的 API

   ```
   curl -X GET "http://localhost:5000/api/Values" -H "accept: text/plain" -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJQYXVsIiwianRpIjoiM2I1YzEyMzMtZTI1YS00ZWU5LWJkNjYtY2Y0NjU2YWMzM2QzIiwiaWF0IjoxNTQ0NTg5ODY5LCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiUGF1bCIsImlkIjoiZDM3ZjI3Y2UtODc4MC00NDI1LTkxMzUtYjY4OGE3NmM0YzBmIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbImFkbWluaXN0cmF0b3IiLCJhcGlfYWNjZXNzIl0sIm5iZiI6MTU0NDU4OTg2OCwiZXhwIjoxNTQ0NTk3MDY4LCJpc3MiOiJTZWN1cml0eURlbW8uQXV0aGVudGljYXRpb24uSldUIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwLyJ9.UAWLYQ5lA6xWofWIjGsPGWtAMHEtqZSfrfVaBui2mKI"
   ```

# 刷新 Token

因为JWT在服务端是没有状态的, 无论用户注销, 修改密码还是Token被盗取, 你都无法将其作废.  所以给JWT设置有效期并且尽量短是很有必要的. 但我们不可能让用户每次Token过期后都重新输入一次用户名和密码为了生成新的Token.  最好是有种方式在用户无感知的情况下完成Token刷新. 所以这里引入了Refresh Token.

1. 修改 JwtFactory 中的 GenerateEncodedToken 方法, 新加一个参数 refreshToken, 并在包含在 response 里和 auth_token 一起返回.

   ```
   public async Task<string> GenerateEncodedToken(string userName, string refreshToken, ClaimsIdentity identity)
   {
       var response = new
       {
           auth_token = encodedJwt,
           refresh_token = refreshToken,
           expires_in = (int)_jwtOptions.ValidFor.TotalSeconds,
           token_type = "Bearer"
       };
   
       return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
   }
   ```

2. 修改 AuthController 中的 Login Action, 在每次客户端请求 JWT Token 的时候, 同时生成一个  GUID 的 refreshToken. 这个 refreshToken 需要保存在数据库或者缓存里. 这里方便演示放入了  MemoryCache 里面. 缓存的过期时间要比JWT Token的过期时间稍微长一点.

   ```
   string refreshToken = Guid.NewGuid().ToString();
   var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(user);
   
   _cache.Set(refreshToken, user.UserName, TimeSpan.FromMinutes(11));
   
   var token = await _jwtFactory.GenerateEncodedToken(user.UserName, refreshToken, claimsIdentity);
   return new OkObjectResult(token);
   ```

3. 添加一个RefreshToken的接口, 接收参数 refresh_token, 然后检查 refresh_token 的有效性,  如果有效生成一个新的 auth_token 和 refresh_token 并返回. 同时需要删除掉原来 refresh_token 的缓存.
    这里只是简单的利用缓存的过期时间和auth_token的过期时间相近从而默认 refresh_token 是有效的, 精确期间需要把对应的 auth_token过期时间一起放入缓存, 在刷新Token的时候验证这个时间.

   ```
   /// <summary>
   /// RefreshToken
   /// </summary>
   /// <param name="request"></param>
   /// <returns></returns>
   [HttpPost("[action]")]
   public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenRequest request)
   {
       string userName;
       if (!_cache.TryGetValue(request.RefreshToken, out userName))
       {
           ModelState.AddModelError("refreshtoken_failure", "Invalid refreshtoken.");
           return BadRequest(ModelState);
       }
       if (!request.UserName.Equals(userName))
       {
           ModelState.AddModelError("refreshtoken_failure", "Invalid userName.");
           return BadRequest(ModelState);
       }
   
       var user = _userService.GetUserByName(request.UserName);
       string newRefreshToken = Guid.NewGuid().ToString();
       var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(user);
   
       _cache.Remove(request.RefreshToken);
       _cache.Set(newRefreshToken, user.UserName, TimeSpan.FromMinutes(11));
   
       var token = await _jwtFactory.GenerateEncodedToken(user.UserName, newRefreshToken, claimsIdentity);
       return new OkObjectResult(token);
   }
   ```

4. 测试

   获取Token:

   ```
   curl -X POST "http://localhost:5000/api/Auth/Login" -H "accept: application/json" -H "Content-Type: application/json-patch+json" -d "{ \"userName\": \"Paul\", \"password\": \"Paul123\"}"
   ```

   返回值:

   ```
   "{\r\n  \"auth_token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJQYXVsIiwianRpIjoiNzA5Y2VkNjEtNWQ2ZS00N2RlLTg4NjctNzVjZGM0N2U0MWZiIiwiaWF0IjoxNTQ0NjgxOTA0LCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiUGF1bCIsImlkIjoiZmE3NjMxYzEtMzk0NS00MzUwLThjM2YtOWYxZDRhODU0MDFhIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbImFkbWluaXN0cmF0b3IiLCJhcGlfYWNjZXNzIl0sIm5iZiI6MTU0NDY4MTkwMywiZXhwIjoxNTQ0NjgyNTAzLCJpc3MiOiJTZWN1cml0eURlbW8uQXV0aGVudGljYXRpb24uSldUIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwLyJ9.tEJ-EuaI-BalW4lJEL8aeJzdryKfE440EC4cAVOW1PY\",\r\n  \"refresh_token\": \"3093f839-fd3c-47a3-97a9-c0324e4e6b7e\",\r\n  \"expires_in\": 600,\r\n  \"token_type\": \"Bearer\"\r\n}"
   ```

   请求RefreshToken:

   ```
   curl -X POST "http://localhost:5000/api/Auth/RefreshToken" -H "accept: application/json" -H "Content-Type: application/json-patch+json" -d "{ \"userName\": \"Paul\", \"refreshToken\": \"3093f839-fd3c-47a3-97a9-c0324e4e6b7e\"}"
   ```

   返回新的 auth_token 和 refresh_token

   ```
   "{\r\n  \"auth_token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJQYXVsIiwianRpIjoiMjI2M2Y4NGEtZjlmMC00ZTM1LWI1YTUtMDdhYmI0M2UzMWQ5IiwiaWF0IjoxNTQ0NjgxOTIxLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiUGF1bCIsImlkIjoiZmE3NjMxYzEtMzk0NS00MzUwLThjM2YtOWYxZDRhODU0MDFhIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjpbImFkbWluaXN0cmF0b3IiLCJhcGlfYWNjZXNzIl0sIm5iZiI6MTU0NDY4MTkyMSwiZXhwIjoxNTQ0NjgyNTIxLCJpc3MiOiJTZWN1cml0eURlbW8uQXV0aGVudGljYXRpb24uSldUIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwLyJ9.A1hXNVmkqD80GqfF69LwvarpNf5QedPvKFDcB5xA4Z0\",\r\n  \"refresh_token\": \"b33de8ff-5213-4d37-be0b-7b561553e0f7\",\r\n  \"expires_in\": 600,\r\n  \"token_type\": \"Bearer\"\r\n}"
   ```

# 使用授权

在认证阶段我们通过用户令牌获取到了用户的Claims，而授权便是对这些Claims进行验证, 比如是否拥有某种角色，年龄是否大于18岁(如果Claims里有年龄信息)等。

## 简单授权

ASP.NET Core中使用`Authorize`特性授权, 使用`AllowAnonymous`特性跳过授权.

```
//所有用户都可以Login, 但只有授权的用户才可以Logout.
public class AccountController : Controller
{
    [AllowAnonymous]
    public ActionResult Login()
    {
    }
    
    [Authorize]
    public ActionResult Logout()
    {
    }
}
```

## 基于固定角色的授权

适用于系统中的角色是固定的，每种角色可以访问的Controller和Action也是固定的情景.

```
//可以指定多个角色, 以逗号分隔
[Authorize(Roles = "Administrator")]
public class AdministrationController : Controller
{
}
```

## 基于策略的授权

在ASP.NET Core中，重新设计了一种更加灵活的授权方式：基于策略的授权, 它是授权的核心.
 在使用基于策略的授权时，首先要定义授权策略，而授权策略本质上就是对Claims的一系列断言。
 基于角色的授权和基于Scheme的授权，只是一种语法上的便捷，最终都会生成授权策略。

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();

    services.AddAuthorization(options =>
    {
        //options.AddPolicy("Administrator", policy => policy.RequireRole("administrator"));
        options.AddPolicy("Administrator", policy => policy.RequireClaim(ClaimTypes.Role, "administrator"));
        
        //options.AddPolicy("Founders", policy => policy.RequireClaim("EmployeeNumber", "1", "2", "3", "4", "5"));
    });
}
[Authorize(Policy = "Administrator")]
public ActionResult<IEnumerable<string>> GetValueByAdminPolicy()
{
    return new string[] { "GetValueByAdminPolicy" };
}
```

## 自定义策略授权

基于策略的授权中有一个很重要的概念是`Requirements`，每一个Requirement都代表一个授权条件。
 Requirement需要继承接口IAuthorizationRequirement。
 在 ASP.NET Core 中已经内置了一些常用的实现：

- AssertionRequirement ：使用最原始的断言形式来声明授权策略。
- DenyAnonymousAuthorizationRequirement ：用于表示禁止匿名用户访问的授权策略，并在AuthorizationOptions中将其设置为默认策略。
- ClaimsAuthorizationRequirement ：用于表示判断Cliams中是否包含预期的Claims的授权策略。
- RolesAuthorizationRequirement ：用于表示使用ClaimsPrincipal.IsInRole来判断是否包含预期的Role的授权策略。
- NameAuthorizationRequirement：用于表示使用ClaimsPrincipal.Identities.Name来判断是否包含预期的Name的授权策略。
- OperationAuthorizationRequirement：用于表示基于操作的授权策略。

除了OperationAuthorizationRequirement外，都有对应的快捷添加方法，比如`RequireClaim`，`RequireRole`，`RequireUserName`等。

当内置的Requirement不能满足需求时，可以定义自己的Requirement. 下面基于图中所示的用户-角色-功能权限设计来实现一个自定义的验证策略。
 ![用户权限表定义](assets/clipboard.png)

1. 添加一个静态类 TestUsers 用于模拟用户数据
    这里只是模拟, 实际使用当中肯定是从数据库取数据, 同时也应该有类似于User, Role, Function, UserRole, RoleFunction等几张表保存这些数据.

   ```
   public static class TestUsers
   {
       public static List<User> Users = new List<User>
       {
           new User{ Id = Guid.NewGuid(), UserName = "Paul", Password = "Paul123", Roles = new List<string>{ "administrator", "api_access" }, Urls = new List<string>{ "/api/values/getadminvalue", "/api/values/getguestvalue" }},
           new User{ Id = Guid.NewGuid(), UserName = "Young", Password = "Young123", Roles = new List<string>{ "api_access" }, Urls = new List<string>{ "/api/values/getguestvalue" }},
           new User{ Id = Guid.NewGuid(), UserName = "Roy", Password = "Roy123", Roles = new List<string>{ "administrator" }, Urls = new List<string>{ "/api/values/getadminvalue" }},
       };
   }
   
   public class User
   {
       public Guid Id { get; set; }
       public string UserName { get; set; }
       public string Password { get; set; }
       public List<string> Roles { get; set; }
       public List<string> Urls { get; set; }
   }
   ```

2. 创建类 UserService 用于获取用户已授权的功能列表.

   ```
   public interface IUserService
   {
       List<string> GetFunctionsByUserId(Guid id);
   }
   
   public class UserService : IUserService
   {
       public List<string> GetFunctionsByUserId(Guid id)
       {
           var user = TestUsers.Users.SingleOrDefault(r => r.Id.Equals(id));
           return user?.Urls;
       }
   }
   ```

3. 创建 PermissionRequirement

   ```
   public class PermissionRequirement : IAuthorizationRequirement
   {
   }
   ```

4. 创建 PermissionHandler
    获取当前的URL, 并去当前用户已授权的URL List里查看. 如果匹配就验证成功.

   ```
   public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
   {
       private readonly IUserService _userService;
   
       public PermissionHandler(IUserService userService)
       {
           _userService = userService;
       }
   
       protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
       {
           var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext).HttpContext;
   
           var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
           if (isAuthenticated)
           {
               Guid userId;
               if (!Guid.TryParse(httpContext.User.Claims.SingleOrDefault(s => s.Type == "id").Value, out userId))
               {
                   return Task.CompletedTask;
               }
               var functions = _userService.GetFunctionsByUserId(userId);
               var requestUrl = httpContext.Request.Path.Value.ToLower();
               if (functions != null && functions.Count > 0 && functions.Contains(requestUrl))
               {
                   context.Succeed(requirement);
               }
           }
           return Task.CompletedTask;
       }
   }
   ```

5. 在Startup.cs 的 ConfigureServices 里面注册 PermissionHandler 并添加 Policy.

   ```
   services.AddAuthorization(options =>
   {
       options.AddPolicy("Permission", policy => policy.Requirements.Add(new PermissionRequirement()));
   });
   services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
   ```

6. 添加测试代码并测试
    注意这里Controller, Action需要和用户功能表里的URL一致

   ```
   [Route("api/[controller]")]
   [ApiController]
   public class ValuesController : ControllerBase
   {
       [HttpGet("[action]")]
       [Authorize(Policy = "Permission")]
       public ActionResult<IEnumerable<string>> GetAdminValue()
       {
           return new string[] { "use Policy = Permission" };
       }
   
       [HttpGet("[action]")]
       [Authorize(Policy = "Permission")]
       public ActionResult<IEnumerable<string>> GetGuestValue()
       {
           return new string[] { "use Policy = Permission" };
       }
   }
   ```

   使用我们的模拟数据, 用户 Paul 两个Action GetAdminValue 和 GetGuestValue 都可以访问; Young 只有权限访问 GetGuestValue; 而 Roy 只可以访问 GetAdminValue.

## 基于资源的授权

有些时候, 授权需要依赖于要访问的资源, 比如:只允许作者自己编辑和删除所写的博客.
 这种场景是无法通过Authorize特性来指定授权的, 因为授权过滤器会在MVC的模型绑定之前执行，无法确定所访问的资源。此时，我们需要使用基于资源的授权。
 在基于资源的授权中, 我们要判断的是用户是否具有针对该资源的某项操作, 而系统预置的`OperationAuthorizationRequirement`就是用于这种场景中的.

```
public class OperationAuthorizationRequirement : IAuthorizationRequirement
{
    public string Name { get; set; }
}
```

1. 定义一些常用操作, 方便业务调用.

   ```
   public static class ResourceOperations
   {
       public static OperationAuthorizationRequirement Create = new OperationAuthorizationRequirement { Name = "Create" };
       public static OperationAuthorizationRequirement Read = new OperationAuthorizationRequirement { Name = "Read" };
       public static OperationAuthorizationRequirement Update = new OperationAuthorizationRequirement { Name = "Update" };
       public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement { Name = "Delete" };
   }
   ```

2. 我们是根据资源的创建者来判断用户是否具有操作权限，因此，定义一个资源实体的接口, 包含一个字段 Creator

   ```
   public interface IResourceWithCreator
   {
       string Creator { get; set; }
   }
   ```

3. 定义测试数据用于模拟

   ```
   public static class TestBlogs
   {
       public static List<Blog> Blogs = new List<Blog>
       {
           new Blog{ Id = Guid.Parse("CA4A3FC9-42CA-47F4-B651-36A863023E75"), Name = "Paul_Blog_1", BlogUrl = "blogs/paul/1", Creator = "Paul" },
           new Blog{ Id = Guid.Parse("9C03EDA8-FBCD-4C33-B5C8-E4DFC40258D7"), Name = "Paul_Blog_2", BlogUrl = "blogs/paul/2", Creator = "Paul" },
           new Blog{ Id = Guid.Parse("E05E3625-1885-49A5-87D0-54F7EAF90C88"), Name = "Young_Blog_1", BlogUrl = "blogs/young/1", Creator = "Young" },
           new Blog{ Id = Guid.Parse("E97D5DF4-AE50-4258-84F8-0B3052EB2CB8"), Name = "Roy_Blog_1", BlogUrl = "blogs/roy/1", Creator = "Roy" },
       };
   }
   
   public class Blog : IResourceWithCreator
   {
       public Guid Id { get; set; }
       public string Name { get; set; }
       public string BlogUrl { get; set; }
   
       public string Creator { get; set; }
   }
   ```

4. 定义 ResourceAuthorizationHandler
    允许任何人创建或查看资源, 有只有资源的创建者才可以修改和删除资源.

   ```
   public class ResourceAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, IResourceWithCreator>
   {
       protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, IResourceWithCreator resource)
       {
           if (requirement == ResourceOperations.Create || requirement == ResourceOperations.Read)
           {
               context.Succeed(requirement);
           }
           else
           {
               if (context.User.Identity.Name == resource.Creator)
               {
                   context.Succeed(requirement);
               }
           }
           return Task.CompletedTask;
       }
   }
   ```

5. 在ConfigureServices里注册Handler.

   ```
   services.AddSingleton<IAuthorizationHandler, ResourceAuthorizationHandler>();
   ```

6. 添加控制器并引入IAuthorizationService进行验证

   ```
   [Authorize]
   public class BlogsController : ControllerBase
   {
       private readonly IAuthorizationService _authorizationService;
       private readonly IBlogService _blogService;
   
       public BlogsController(IAuthorizationService authorizationService, IBlogService blogService)
       {
           _authorizationService = authorizationService;
           _blogService = blogService;
       }
   
       [HttpGet("{id}", Name = "Get")]
       public async Task<ActionResult<Blog>> Get(Guid id)
       {
           var blog = _blogService.GetBlogById(id);
           if ((await _authorizationService.AuthorizeAsync(User, blog, ResourceOperations.Read)).Succeeded)
           {
               return Ok(blog);
           }
           else
           {
               return Forbid();
           }
       }
   
       [HttpPut("{id}")]
       public async Task<ActionResult> Put(Guid id, [FromBody] Blog newBlog)
       {
           var blog = _blogService.GetBlogById(id);
           if ((await _authorizationService.AuthorizeAsync(User, blog, ResourceOperations.Update)).Succeeded)
           {
               bool result = _blogService.Update(newBlog);
               return Ok(result);
           }
           else
           {
               return Forbid();
           }
       }
   }
   ```

   在实际使用当中, 可以通过EF Core拦截或AOP来实现授权验证与业务代码的分离。

# 源代码

[github](https://github.com/zdz72113/NETCore_BasicKnowledge.Examples/tree/master/SecurityDemo/SecurityDemo.Authentication.JWT)

# 参考

- [Overview of ASP.NET Core Security](https://docs.microsoft.com/en-us/aspnet/core/security)

- [AngularASPNETCore2WebApiAuth](https://github.com/mmacneil/AngularASPNETCore2WebApiAuth)

- [ASP.NET Core 认证与授权[1\]: 初识认证(https://www.cnblogs.com/RainingNight/archive/2017/09/26/7512903.html)

- [asp.net core策略授权](https://www.cnblogs.com/axzxs2001/p/7482777.html)

- [ASP.NET Core 使用 JWT 搭建分布式无状态身份验证系统](http://www.cnblogs.com/JacZhu/p/6837676.html)

- [JWT权限验证](https://www.cnblogs.com/laozhang-is-phi/p/9511869.html)

- [Handle Refresh Token Using ASP.NET Core 2.0 And JSON Web Token](https://www.c-sharpcorner.com/article/handle-refresh-token-using-asp-net-core-2-0-and-json-web-token/)



分类: [NET Core Basic](https://www.cnblogs.com/royzshare/category/1244163.html)