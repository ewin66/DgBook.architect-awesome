







## IdentityServer4.Models-GrantTypes

```csharp
using System.Collections.Generic;

namespace IdentityServer4.Models
{
    public class GrantTypes
    {
        public GrantTypes();

        public static ICollection<string> Implicit { get; }
        public static ICollection<string> ImplicitAndClientCredentials { get; }
        public static ICollection<string> Code { get; }
        public static ICollection<string> CodeAndClientCredentials { get; }
        public static ICollection<string> Hybrid { get; }
        public static ICollection<string> HybridAndClientCredentials { get; }
        public static ICollection<string> ClientCredentials { get; }
        public static ICollection<string> ResourceOwnerPassword { get; }
        public static ICollection<string> ResourceOwnerPasswordAndClientCredentials { get; }
        public static ICollection<string> DeviceFlow { get; }
    }
}
```









ClientId要和MvcClient里面指定的名称一致.

**OAuth是使用Scopes**来划分Api的, 而**OpenId Connect则使用Scopes来限制**信息, 例如使用offline access时的Profile信息, 还有用户的其他细节信息.

这里**GrantType要改为Implicit**. 使用Implicit flow时, 首先会重定向到Authorization  Server, 然后登陆, 然后Identity Server需要知道是否可以重定向回到网站, 如果不指定重定向返回的地址的话,  我们的Session有可能就会被劫持. 