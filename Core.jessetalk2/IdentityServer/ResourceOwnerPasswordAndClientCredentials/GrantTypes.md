









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

