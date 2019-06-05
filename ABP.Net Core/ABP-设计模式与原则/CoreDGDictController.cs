using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using Abp.Domain.Repositories;
using AutoMapper;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using RQCore.Controllers;
using RQCore.RQDtos;
using RQCore.RQEnitity;
using RQCore.Authorization.Roles;
using RQCore.Authorization.Users;
using Microsoft.AspNetCore.Identity;
using RQCore.Users;
using RQCore.Users.Dto;
using Abp.Authorization.Users;
using RQCore.MultiTenancy; 
using Abp.Authorization; 
using Abp.MultiTenancy;
using Abp.Runtime.Security; 
using RQCore.Authentication.External;
using RQCore.Authentication.JwtBearer;
using RQCore.Authorization;
using RQCore.Models.TokenAuth;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Abp;
using Abp.Collections.Extensions;
using RQCore.Roles.Dto;
using Abp.Application.Services.Dto;
using RQCore.Sessions.Dto;
using Abp.Runtime.Session;

using Microsoft.EntityFrameworkCore; 

 
using Abp.Linq.Extensions;
using Abp.Extensions;

namespace RQCore.Web.Host.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CoreDGDictController : RQCoreControllerBase
    {
        #region constructor 构造方法
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        private readonly LogInManager _logInManager;
        private readonly ITenantCache _tenantCache;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly TokenAuthConfiguration _configuration;
        private readonly IExternalAuthConfiguration _externalAuthConfiguration;
        private readonly IExternalAuthManager _externalAuthManager;
        private readonly UserRegistrationManager _userRegistrationManager;






        public CoreDGDictController(

                IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,

        
            LogInManager logInManager,
            ITenantCache tenantCache,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            TokenAuthConfiguration configuration,
            IExternalAuthConfiguration externalAuthConfiguration,
            IExternalAuthManager externalAuthManager,
            UserRegistrationManager userRegistrationManager)
        //: base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;


            _logInManager = logInManager;
            _tenantCache = tenantCache;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _configuration = configuration;
            _externalAuthConfiguration = externalAuthConfiguration;
            _externalAuthManager = externalAuthManager;
            _userRegistrationManager = userRegistrationManager;
        }

        #endregion


        #region PermissionTree权限树
        public class TreeClass
        {
            public PermissionDto Permission { get; set; }
            public int parentId { get; set; }
            public int id { get; set; }
            public string label { get; set; }
        }
        List<TreeClass> Treelist = new List<TreeClass>();
        int num = 1;
        public List<TreeClass> Recursion(Permission tree, int id)
        {
            if (tree.Children.Count > 0)
            {
                foreach (var item in tree.Children)
                {
                    TreeClass treeClass2 = new TreeClass();
                    treeClass2.id = ++num;
                    treeClass2.parentId = id;
                    treeClass2.label = Mapper.Map<PermissionDto>(item).DisplayName;
                    treeClass2.Permission = Mapper.Map<PermissionDto>(item);
                    Treelist.Add(treeClass2);
                    Recursion(item, treeClass2.id);
                }
            }
            return Treelist;
        }


        #endregion

         
        #region 登录接口

       

        [HttpPost("api/Authenticate")]
        public async Task<JsonResult> Authenticate([FromBody] AuthenticateModel model)
        { 
            //AuthenticateResultModel 返回的字典类型
            SortedDictionary<string, object> DgDict = new SortedDictionary<string, object>();
            //返回登录结果
            var loginResult = await GetLoginResultAsync(
                model.UserNameOrEmailAddress,
                model.Password,
                GetTenancyNameOrNull()
            );
            AbpClaimTypes.UserId = loginResult.User.Id.ToString();
            #region 角色列表版本  暂时没用
            DgDict.Add("permissionsToRolesVersion", 1001);
            #endregion
           
            #region 获取所有权限列表
            var Permissions = PermissionManager.GetAllPermissions();
            Treelist = new List<TreeClass>();
            TreeClass treeClass1 = new TreeClass();
            treeClass1.id = 1;
            treeClass1.parentId = 0;
            treeClass1.label = Mapper.Map<PermissionDto>(Permissions.Where(x => x.Name == PermissionNames.Pages_Staff).FirstOrDefault()).DisplayName;
            treeClass1.Permission = Mapper.Map<PermissionDto>(Permissions.Where(x => x.Name == PermissionNames.Pages_Staff).FirstOrDefault());           
            Treelist = Recursion(Permissions.Where(x => x.Name == PermissionNames.Pages_Staff).FirstOrDefault(), 1);
            Treelist.Add(treeClass1);
            DgDict.Add("allPermissions", new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(Permissions)
            ));
            try
            { 
                DgDict.Add("allPermissionsForTree", Treelist);
            }
            catch (Exception ex)
            {

            }
            num = 1;
            //权限列表
            List<Permission> allPermissions = new List<Permission>();
            foreach (var item in Permissions)
            {
                allPermissions.Add(item);
            }
            #endregion
           
            #region 角色列表
            //var allRoles = await _roleRepository.GetAllListAsync();
            var allRoleIReadOnlyList = await _roleRepository.GetAllListAsync();
            List<Role> allRoles = new List<Role>();
            foreach (var item in allRoleIReadOnlyList.Where(x=>x.IsDeleted==false).ToList())
            {
                allRoles.Add(item);
            }
            DgDict.Add("allRoles", Mapper.Map<List<RoleListDto>>(allRoles));
            //DgDict.Add("allRoles", new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(allRoles)));

            //new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(allRoles));

            #endregion
            //获取登录用户的所拥有的所有权限
            var grantedPermissionNames = new List<string>();
            if (loginResult.User.Id>0)
            {
                foreach (var permissionName in allPermissions)
                {
                   Abp.UserIdentifier Identifier=UserIdentifier.Parse(loginResult.User.Id.ToString());
                    if (await PermissionChecker.IsGrantedAsync(Identifier, permissionName.Name))
                    {
                        grantedPermissionNames.Add(permissionName.Name);  // 获取当前用户的权限
                    }
                }
            }
          
            #region  是否有审核权 canAssignInspectionToOther
            bool canAssignInspectionToOther = await PermissionChecker.IsGrantedAsync(UserIdentifier.Parse(loginResult.User.Id.ToString()), PermissionNames.Pages_Inspection);
            if (!canAssignInspectionToOther)
            {
                DgDict.Add("canAssignInspectionToOther", "没有Pages_Inspection");
                DgDict.Add("canAssignInspectionToOtherValue", false);
            }
            else
            {
                DgDict.Add("canAssignInspectionToOther", "拥有Pages_Inspection");
                DgDict.Add("canAssignInspectionToOtherValue", true);
            }
            #endregion

            bool canAssignRolesFromAdmin = await PermissionChecker.IsGrantedAsync(UserIdentifier.Parse(loginResult.User.Id.ToString()), PermissionNames.Pages_Tenants);
            bool canAssignRolesFromRQAdmin = await PermissionChecker.IsGrantedAsync(UserIdentifier.Parse(loginResult.User.Id.ToString()), PermissionNames.Pages_Admin);
            bool canAssignRolesFromRQAssitant = await PermissionChecker.IsGrantedAsync(UserIdentifier.Parse(loginResult.User.Id.ToString()), PermissionNames.Pages_RQAssitant);
 
            #region 可分配角色列表--针对员工管理

            List<Role> RolescanAssigned = allRoles;
            List<string> RolescanAssignedString = new List<string>();//角色名数组初始化
            foreach (var item in allRoleIReadOnlyList.Where(x => x.IsDeleted == false).ToList())
            {
                RolescanAssignedString.Add(item.Name);
            }
            //如果任务已经分配且未分配给自己，且不具有分配任务权限，则抛出异常
            if (canAssignRolesFromAdmin) 
            {
                List<Role> allmyRoles = new List<Role>();//当前用户可分配的角色 初始化
                string[] outAdmin = { "Admin"};
                foreach (var item in outAdmin)
                {
                    if (RolescanAssignedString.Contains(item))
                    {
                        RolescanAssignedString.Remove(item);
                    }  //item.SetNormalizedName in
                }
                foreach (var itemStr in RolescanAssignedString)
                {
                    foreach (var item in allRoles)
                    {
                        if (item.Name == itemStr)
                        {
                            allmyRoles.Add(item);
                        }
                    }
                }
                DgDict.Add("RolescanAssigned", Mapper.Map<List<RoleDto>>(allmyRoles));
            }
            else if (canAssignRolesFromRQAdmin)
            {
                List<Role> allmyRoles = new List<Role>();//当前用户可分配的角色 初始化
                string[] outAdmin = { "Admin","RQAdmin","RQAdminPermissions"};
                foreach (var item in outAdmin)
                {
                    if (RolescanAssignedString.Contains(item))
                    {
                        RolescanAssignedString.Remove(item);
                    }  //item.SetNormalizedName in
                }
                foreach (var itemStr in RolescanAssignedString)
                {
                    foreach (var item in allRoles)
                    {
                        if (item.Name == itemStr)
                        {
                            allmyRoles.Add(item);
                        }
                    }
                }
                DgDict.Add("RolescanAssigned", Mapper.Map<List<RoleDto>>(allmyRoles));

            }
            else if (canAssignRolesFromRQAssitant)
            {
                string[] outAdmin = { "Admin", "RQAssitantPermissions",
                    "RQAdmin" , "RQAssitant" ,"RQAdminPermissions"};
                List<Role> allmyRoles = new List<Role>();//当前用户可分配的角色 初始化
                foreach (var item in outAdmin)
                {
                    if (RolescanAssignedString.Contains(item))
                    {
                        RolescanAssignedString.Remove(item);
                    }  //item.SetNormalizedName in
                }
                foreach (var itemStr in RolescanAssignedString)
                {
                    foreach (var item in allRoles)
                    {
                        if (item.Name == itemStr)
                        {
                            allmyRoles.Add(item);
                        }
                    }
                }
                DgDict.Add("RolescanAssigned", Mapper.Map<List<RoleDto>>(allmyRoles));

            }
            else
            { 
                DgDict.Add("RolescanAssigned", null);
            }
            #endregion
            
            #region 可分配权限列表【角色管理-分配权限】
            var PermissionscanAssigned = allPermissions;
            List<string> PermissionscanAssignedString = new List<string>();
            foreach (var item in PermissionscanAssigned)
            {
                PermissionscanAssignedString.Add(item.Name);
            }

            if (canAssignRolesFromAdmin) 
            {
                DgDict.Add("PermissionscanAssigned", Mapper.Map<List<PermissionDto>>(PermissionscanAssigned));
            }
            else if (canAssignRolesFromRQAdmin)
            {
                List<Permission> allMyPermission = new List<Permission>();//当前用户可分配的权限 初始化
                string[] outAdmin = {"Pages","Pages.Tenants",
                    "Pages.Users", "Pages.Roles", "Pages.Admin",
                    "Pages.Admin.Users","Pages.Admin.Roles"
                };
                foreach (var item in outAdmin)
                {
                    if (PermissionscanAssignedString.Contains(item))
                    {
                        PermissionscanAssignedString.Remove(item);
                    }  
                }
                foreach (var itemStr in PermissionscanAssignedString)
                {
                    foreach (var item in PermissionscanAssigned)
                    {
                        if (item.Name == itemStr)
                        {
                            allMyPermission.Add(item);
                        }
                    }
                }
                DgDict.Add("PermissionscanAssigned", Mapper.Map<List<PermissionDto>>(allMyPermission));

            }
            else if (canAssignRolesFromRQAssitant)
            {
                List<Permission> allMyPermission = new List<Permission>();//当前用户可分配的权限 初始化
                string[] outAdmin = {"Pages","Pages.Tenants",
                    "Pages.Users", "Pages.Roles", "Pages.Admin",
                    "Pages.Admin.Users","Pages.Admin.Roles",
                    "Pages.RQAssitant.Roles", "Pages.RQAssitant.Users",
                    "Pages.RQAssitant"
                };
                foreach (var item in outAdmin)
                {
                    if (PermissionscanAssignedString.Contains(item))
                    {
                        PermissionscanAssignedString.Remove(item);
                    }
                }
                foreach (var itemStr in PermissionscanAssignedString)
                {
                    foreach (var item in PermissionscanAssigned)
                    {
                        if (item.Name == itemStr)
                        {
                            allMyPermission.Add(item);
                        }
                    }
                }
                DgDict.Add("PermissionscanAssigned", Mapper.Map<List<PermissionDto>>(allMyPermission));

            }
            else
            {
                DgDict.Add("PermissionscanAssigned", null);
            }

            #endregion
            
            #region 登录返回UserId-accessToken--EncryptedAccessToken-ExpireInSeconds
            var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
            DgDict.Add("AuthenticateResultModel",
                   new AuthenticateResultModel
                   {
                       AccessToken = accessToken,
                       EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                       ExpireInSeconds = (int)_configuration.Expiration.TotalSeconds,
                       UserId = loginResult.User.Id
                   }
                );
            #endregion

            return Json(DgDict);
        }




        #endregion


        #region 支持方法

        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

        private string GetEncrpyedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, AppConsts.DefaultPassPhrase);
        }


        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }
 
        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }








        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }


        #endregion
    }
}