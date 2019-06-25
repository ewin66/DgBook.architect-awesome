IsGrantedAsync(long userId, Permission permission)

 IsGrantedAsync(TUser user, Permission permission)

IsGrantedAsync(long userId, string permissionName)







Task<IReadOnlyList<Permission>> GetGrantedPermissionsAsync(TUser user)