这篇文章主要为大家详细介绍了基于.net4.0实现IdentityServer4客户端JWT解密，具有一定的参考价值，感兴趣的小伙伴们可以参考一下

**情景：**公司项目基于.net4.0,web客户端实现单点登录需要自己解密id_token，对于jwt解密，.net提供了IdentityModel类库，但是4.0中该类库不可用，所以自己实现了解密方法..

使用了类库：链接地址

下面直接贴代码，直接调用DecodeJWT方法就行，参数为id_token，key默认为空字符串"",

**代码**





```csharp


    public static IDictionary<string, object> DecodeJWT(string jwttoken,string key)
    {
     
    //从/.well-known/openid-configuration路径获取jwks_uri
    var webClient = new WebClient();
     
    var endpoint = "http://localhost:5000/.well-known/openid-configuration";
     
    var json = webClient.DownloadString(endpoint);
     
    JObject metadata = JsonConvert.DeserializeObject<JObject>(json);
     
    var jwksUri = metadata["jwks_uri"].ToString();
     
    //从jwks_uri获取keys
    json = webClient.DownloadString(jwksUri);
     
    var keys = JsonConvert.DeserializeObject<CustomJWKs>(json);
     
     
    //从jwt获取头部kid,并从keys中找到匹配kid的key
    string[] tokenParts = jwttoken.Split('.');
    byte[] bytes = FromBase64Url(tokenParts[0]);
    string head= Encoding.UTF8.GetString(bytes);
    string kid = JsonConvert.DeserializeObject<JObject>(head)["kid"].ToString();
     
    var defaultkey=keys.keys.Where(t => t.kid == kid).FirstOrDefault();
     
    if(defaultkey==null)
    {
    throw new Exception("未找到匹配的kid");
    }
     
    //jwt解密
    return RS256Decode(jwttoken, key, defaultkey.e, defaultkey.n);
    }
     
     
    public static IDictionary<string, object> RS256Decode(string token, string secret, string exponent,string modulus)
    {
    try
    {
    IJsonSerializer serializer = new JsonNetSerializer();
    IDateTimeProvider provider = new UtcDateTimeProvider();
    IJwtValidator validator = new JwtValidator(serializer, provider);
    IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
    RSAlgorithmFactory rS256Algorithm = new RSAlgorithmFactory(() =>
    {
    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
    rsa.ImportParameters(
    new RSAParameters()
    {
    Modulus = FromBase64Url(modulus),
    Exponent = FromBase64Url(exponent)
    });
     
     
    byte[] rsaBytes = rsa.ExportCspBlob(true);
     
    X509Certificate2 cert = new X509Certificate2(rsaBytes);
    return cert;
    });
     
    IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, rS256Algorithm);
    var json = decoder.DecodeToObject(token, secret, verify: false);
    return json;
    }
    catch (TokenExpiredException)
    {
    throw new Exception("token已过期");
    //Console.WriteLine("Token has expired");
    //return null;
    }
    catch (SignatureVerificationException)
    {
    throw new Exception("token验证失败");
    //Console.WriteLine("Token has invalid signature");
    //return null;
    }
    }
     
     
    public static byte[] FromBase64Url(string base64Url)
    {
    string padded = base64Url.Length % 4 == 0
    ? base64Url : base64Url + "====".Substring(base64Url.Length % 4);
    string base64 = padded.Replace("_", "/")
    .Replace("-", "+");
    return Convert.FromBase64String(base64);
    }


```


 

以上就是本文的全部内容，希望对大家的学习有所帮助，也希望大家多多支持我们。

本文标题: 基于.net4.0实现IdentityServer4客户端JWT解密

本文地址: http://www.cppcns.com/wangluo/aspnet/240173.html