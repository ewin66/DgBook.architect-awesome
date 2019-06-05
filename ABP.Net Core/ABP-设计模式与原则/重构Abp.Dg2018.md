### 	Microsoft.AspNet.Identity;

```csharp
using Microsoft.AspNet.Identity;

private async Task<UserPermissionCacheItem> GetUserPermissionCacheItemAsync(long userId)
```

###	策略权限==》Bool判定



```csharp
//控制器or类的权限
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AbpAuthorizeAttribute : Attribute, IAbpAuthorizeAttribute
{
 public virtual async Task AuthorizeAsync(IEnumerable<IAbpAuthorizeAttribute> authorizeAttributes)
    {
       ......
        foreach (var authorizeAttribute in authorizeAttributes)
        {//【原始方案】遍历所有授权特性
        //通过 IPermissionChecker 来验证用户是否拥有这些特性所标注的权限
            await PermissionChecker.AuthorizeAsync
            (authorizeAttribute.RequireAllPermissions, authorizeAttribute.Permissions);
        } 
        //using代码块限定，决定对操作的执行
        using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant, AbpDataFilters.MustHaveTenant))
           {
               var usersInfo = UserManager.Users;
           } 
    }

}
```



![1559713647569](assets/1559713647569.png)

![1559717648691](assets/1559717648691.png)





### 分页GetAll()

![1559718260099](assets/1559718260099.png)



![1559718229955](assets/1559718229955.png)





### 	两种Mapper

#####	Plan.A	AutoMapper



![1559718414019](assets/1559718414019.png)



#####	Plan.B



![1559718451589](assets/1559718451589.png)

![1559718550292](assets/1559718550292.png)



![1559718598394](assets/1559718598394.png)



##### Plan.C	匿名

![1559718664203](assets/1559718664203.png)