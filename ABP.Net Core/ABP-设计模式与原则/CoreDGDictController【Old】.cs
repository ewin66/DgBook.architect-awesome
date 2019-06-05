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

        public class TreeClass
        {
            public PermissionDto Permission { get; set; }
            public int parentId { get; set; }
            public int id { get; set; }
            public string label { get; set; }
        }
        List<TreeClass> Treelist = new List<TreeClass>();
        int num = 1;
        public List<TreeClass> Recursion(Permission tree,int id)
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
            #region 可分配角色列表--针对员工管理
            bool canAssignRolesFromAdmin = await PermissionChecker.IsGrantedAsync(UserIdentifier.Parse(loginResult.User.Id.ToString()),PermissionNames.Pages_Tenants);
            bool canAssignRolesFromRQAdmin = await PermissionChecker.IsGrantedAsync(UserIdentifier.Parse(loginResult.User.Id.ToString()), PermissionNames.Pages_Admin);
            bool canAssignRolesFromRQAssitant = await PermissionChecker.IsGrantedAsync(UserIdentifier.Parse(loginResult.User.Id.ToString()), PermissionNames.Pages_RQAssitant);
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











        [HttpPost("api/AuthenticateAllWithOut")]
        public async Task<JsonResult> AllWithOut([FromBody]string userid)
        {
            if (!userid.IsNullOrEmpty())
            {
                AbpClaimTypes.UserId = userid;
            }           
            //AuthenticateResultModel
            SortedDictionary<string, object> DgDict = new SortedDictionary<string, object>();
            #region 角色列表版本
            DgDict.Add("permissionsToRolesVersion", 1001);
            #endregion
            
            #region 权限列表
            var Permissions = PermissionManager.GetAllPermissions();
            DgDict.Add("allPermissions", new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(Permissions)
            ));
            Treelist = new List<TreeClass>();
            TreeClass treeClass1 = new TreeClass();
            treeClass1.id = 1;
            treeClass1.parentId = 0;
            treeClass1.label = Mapper.Map<PermissionDto>(Permissions.Where(x => x.Name == PermissionNames.Pages_Staff).FirstOrDefault()).DisplayName;
            treeClass1.Permission = Mapper.Map<PermissionDto>(Permissions.Where(x => x.Name == PermissionNames.Pages_Staff).FirstOrDefault());
            Treelist = Recursion(Permissions.Where(x => x.Name == PermissionNames.Pages_Staff).FirstOrDefault(), 1);
            Treelist.Add(treeClass1);
          
            try
            {
                DgDict.Add("allPermissionsForTree", Treelist);
            }
            catch (Exception ex)
            {

            }
            num = 1;
            List<Permission> allPermissions = new List<Permission>();
            foreach (var item in Permissions)
            {
                allPermissions.Add(item);
            }
            #endregion
            #region 角色列表
            var allRoleIReadOnlyList = await _roleRepository.GetAllListAsync();
            List<Role> allRoles = new List<Role>();
            allRoles = allRoleIReadOnlyList.Where(x => x.IsDeleted == false).ToList();
            DgDict.Add("allRoles", Mapper.Map<List<RoleListDto>>(allRoleIReadOnlyList.Where(x=>x.IsDeleted==false).ToList()));
            #endregion
           

            #region  是否有审核权 canAssignInspectionToOther
            bool canAssignInspectionToOther = await PermissionChecker.IsGrantedAsync(UserIdentifier.Parse(AbpClaimTypes.UserId), PermissionNames.Pages_Inspection);
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


            #region 可分配角色列表--针对员工管理
            bool canAssignRolesFromAdmin = await PermissionChecker.IsGrantedAsync(UserIdentifier.Parse(AbpClaimTypes.UserId), PermissionNames.Pages_Tenants);
            bool canAssignRolesFromRQAdmin = await PermissionChecker.IsGrantedAsync(UserIdentifier.Parse(AbpClaimTypes.UserId), PermissionNames.Pages_Admin);
            bool canAssignRolesFromRQAssitant = await PermissionChecker.IsGrantedAsync(UserIdentifier.Parse(AbpClaimTypes.UserId), PermissionNames.Pages_RQAssitant);
            if (canAssignRolesFromAdmin) //var RolesUnderYouers = allRoles;
            {
                DgDict.Add("myTopRoleBack", "canAssignRolesFromAdmin");
            }
            else if (canAssignRolesFromRQAdmin)
            {
                DgDict.Add("myTopRoleBack", "canAssignRolesFromRQAdmin");
            }
            else if (canAssignRolesFromRQAssitant)
            {
                DgDict.Add("myTopRoleBack", "canAssignRolesFromRQAssitant");
            }
            else
            {
                DgDict.Add("myTopRoleBack", null);

            }

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
                string[] outAdmin = { "Admin" };
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
                string[] outAdmin = { "Admin", "RQAdmin", "RQAdminPermissions" };
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
            return Json(DgDict);
        } 
           











        #region 其他功能测试接口

        [HttpPost("api/CreateUser")]
        public async Task<JsonResult> Create(CreateUserDto input)
        {
            //CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return Json(Mapper.Map<UserDto>(user));

        }


        [HttpPost("api/AuthenticateTest3")]
        public async Task<JsonResult> All3(IQueryable<Role> query, PagedResultRequestDto input)
        {
            //AuthenticateResultModel
            SortedDictionary<string, object> DgDict = new SortedDictionary<string, object>();
            //return 
                query.OrderBy(r => r.DisplayName);

            DgDict.Add("allllllllllllll",
             query.OrderBy(r => r.DisplayName));

            //        GetRolesInput input = new GetRolesInput();

            //        var roles = await _roleManager
            //.Roles
            //.WhereIf(

            //    !input.Permission.IsNullOrWhiteSpace(),
            //    r => r.Permissions.Any(rp => rp.Name == input.Permission && rp.IsGranted)
            //)
            //.ToListAsync();

            //        DgDict.Add("allllllllllllll",
            //            new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(roles)));

            //        //return new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(roles));






            //IList<object> DgDict = new List<object>();

            //EntityDto input = new EntityDto();

            //var permissions = PermissionManager.GetAllPermissions();
            //var role = await _roleManager.GetRoleByIdAsync(input.Id);
            //var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
            //var roleEditDto = ObjectMapper.Map<RoleEditDto>(role);

            ////return new GetRoleForEditOutput
            ////{
            ////    Role = roleEditDto,
            ////    Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
            ////    GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            ////};



            //DgDict.Add("allllllllllllll", new GetRoleForEditOutput
            //{
            //    Role = roleEditDto,
            //    Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
            //    GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            //}


            //);
            return Json(DgDict);
        }


        [HttpPost("api/AuthenticateTest2")]
        public async Task<JsonResult> All2()
        {//AuthenticateResultModel
            SortedDictionary<string, object> DgDict = new SortedDictionary<string, object>();
            //IList<object> DgDict = new List<object>();

            EntityDto input = new EntityDto();

            var permissions = PermissionManager.GetAllPermissions();
            var role = await _roleManager.GetRoleByIdAsync(input.Id);
            var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
            var roleEditDto = ObjectMapper.Map<RoleEditDto>(role);

            //return new GetRoleForEditOutput
            //{
            //    Role = roleEditDto,
            //    Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
            //    GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            //};



            DgDict.Add("allllllllllllll", new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            }
);

            #region 角色列表版本

            DgDict.Add("permissionsToRolesVersion", 1001);

            #endregion

            #region 用户信息【暂无】

            //DgDict.Add("permissionsToRolesVersion", 1001);

            //  ////用户信息
            //  //UserAppService userAppService = new UserAppService();
            //  //var user = await userAppService.GetEntityByIdAsync( loginResult.User.Id)
            //  //.FirstOrDefaultAsync(x => x.Id == loginResult.User.Id);

            //  var user = await _userManager.GetUserByIdAsync(loginResult.User.Id);
            //  //DgDict.Add(loginResult.User.EmailAddress);
            //  //ObjectMapper.Map<User>(user);
            //  //      {
            //  //var user = await Repository.GetAllIncluding(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);



            //  DgDict.Add("CurrentLogin", Mapper.Map<CreateUserDto>(user)
            ////ObjectMapper.Map<UserDto> (loginResult.User)    
            //  ); 

            //IList<object> DgDict = new List<object>();

            //new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(allRoles));

            //ICollection<UserRole> roles = loginResult.User.Roles;
            ////ICollection<UserRole> roles = user.Roles;
            //DgDict.Add(roles);

            //var allPermissions = PermissionManager.GetAllPermissions();

            //DgDict.Add(allPermissions);
            //var allRoles = await _roleRepository.GetAllListAsync();

            //DgDict.Add(allRoles);
            //.Roles
            //.WhereIf(
            //    !input.Permission.IsNullOrWhiteSpace(),
            //    r => r.Permissions.Any(rp => rp.Name == input.Permission && rp.IsGranted)
            //)
            //.ToListAsync();

            //return new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(roles));



            #endregion

            #region 权限列表
            var allPermissions = PermissionManager.GetAllPermissions();
            DgDict.Add("allPermissions", new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(allPermissions)
            ));

            #endregion

            #region 角色列表
            //var allRoles = await _roleRepository.GetAllListAsync();
            var allRoleIReadOnlyList = await _roleRepository.GetAllListAsync();
            List<Role> allRoles = new List<Role>();
            foreach (var item in allRoleIReadOnlyList)
            {
                allRoles.Add(item);
            }

            DgDict.Add("allRoles", Mapper.Map<List<RoleDto>>(allRoles));
            //DgDict.Add("allRoles", new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(allRoles)));

            //new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(allRoles));

            #endregion

            #region  是否有审核权 canAssignInspectionToOther
            bool canAssignInspectionToOther = PermissionChecker.IsGranted(PermissionNames.Pages_Inspection);
            //如果任务已经分配且未分配给自己，且不具有分配任务权限，则抛出异常
            //if (input.AssignedPersonId.Value != AbpSession.GetUserId() && !canAssignInspectionToOther)
            //{
            //    throw new AbpAuthorizationException("没有分配任务给他人的权限！");
            //}

            //var UserId = AbpSession.GetUserId();
            //DgDict.Add("UserId", UserId);

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

            //return Json(DgDict);

            #endregion

            #region 可分配角色列表--针对员工管理
            bool canAssignRolesFromAdmin = PermissionChecker.IsGranted(PermissionNames.Pages_Inspection);
            bool canAssignRolesFromRQAdmin = PermissionChecker.IsGranted(PermissionNames.Pages_Inspection);
            bool canAssignRolesFromRQAssitant = PermissionChecker.IsGranted(PermissionNames.Pages_Inspection);
            List<Role> RolescanAssigned = allRoles;

            //List<Role> RolesSource = allRoles;
            //如果任务已经分配且未分配给自己，且不具有分配任务权限，则抛出异常
            if (canAssignRolesFromAdmin) //var RolesUnderYouers = allRoles;
            {
                string[] outAdmin = { "" };
                foreach (Role item in RolescanAssigned)
                {
                    if (outAdmin.Contains(item.Name))
                    {
                        RolescanAssigned.Remove(item);
                    }  //item.SetNormalizedName in
                }
                //DgDict.Add("allRoles", Mapper.Map<List<RoleDto>>(allRoles));
                DgDict.Add("RolescanAssigned", Mapper.Map<List<RoleDto>>(RolescanAssigned));
                //throw new AbpAuthorizationException("没有分配任务给他人的权限！");
            }
            else if (canAssignRolesFromRQAdmin)
            {
                string[] outAdmin = { "Admin", "RQAdminPermissions", "wangyuanqing" };
                foreach (Role item in RolescanAssigned)
                {
                    if (outAdmin.Contains(item.Name))
                    {
                        RolescanAssigned.Remove(item);
                    }  //item.SetNormalizedName in
                }
                DgDict.Add("RolescanAssigned", Mapper.Map<List<RoleDto>>(RolescanAssigned));

            }
            else if (canAssignRolesFromRQAssitant)
            {
                string[] outAdmin = { "Admin", "RQAssitantPermissions",
                    "RQAdmin" , "RQAssitant" ,"RQAdminPermissions","wangyuanqing"};
                foreach (Role item in RolescanAssigned)
                {
                    if (outAdmin.Contains(item.Name))
                    {
                        RolescanAssigned.Remove(item);
                    }  //item.SetNormalizedName in
                }
                DgDict.Add("RolescanAssigned", Mapper.Map<List<RoleDto>>(RolescanAssigned));

            }
            else
            {
                DgDict.Add("RolescanAssigned", null);
                DgDict.Add("RolescanAssignedDec", "您没有角色管理的权限");
                //DgDict.Add("RolescanAssigned", Mapper.Map<List<RoleDto>>(allRoles)); 
            }
            //    = await _roleRepository.GetAllListAsync();
            //DgDict.Add("allRoles", new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(allRoles)));
            #endregion

            #region 可分配权限列表【角色管理-分配权限】
            //List<Authorization.Roles.Permission> PermissionscanAssigned
            //    = (List<Authorization.Roles.Permission>)allPermissions;


            #endregion


            #region 
            #endregion

            #region TrafficLogDict物流状态字典
            TrafficLog[] TrafficLogDict =
            {

                new TrafficLog(0, " 1", "2"),
                new TrafficLog(1, " 1", "2"),
                new TrafficLog(2, " 1", "2"),
                new TrafficLog(3, " 1", "2"),
                new TrafficLog(4, " 1", "2"),
                new TrafficLog(5, " 1", "2"),
                new TrafficLog(6, " 1", "2"),
                new TrafficLog(7, " 1", "2"),
                new TrafficLog(10086, " 1", "2")


            };
            DgDict.Add("TrafficLogDict", TrafficLogDict);

            #endregion

            return Json(DgDict);
        }


        [HttpPost("api/AuthenticateTest")]
        public async Task<JsonResult> All()
        {//AuthenticateResultModel
            SortedDictionary<string, object> DgDict = new SortedDictionary<string, object>();
            //IList<object> DgDict = new List<object>();

            #region 权限列表
            var permissionIReadOnlyList = PermissionManager.GetAllPermissions();
            //IReadOnlyList<Permission> permissionIReadOnlyList = allPermissions;
            List<Permission> allPermissions = new List<Permission> ();
            foreach (var item in permissionIReadOnlyList)
            {
                allPermissions.Add(item);
            }
            //DgDict.Add("allPermissions", new ListResultDto<PermissionDto>(
            //    ObjectMapper.Map<List<PermissionDto>>(allPermissions)
            //));
            //DgDict.Add("allPermissions", allPermissions);

            #endregion

            #region 角色列表

            //var allRoles = await _roleRepository.GetAllListAsync();
            ////RoleListDto a;
            var allRoleIReadOnlyList = await _roleRepository.GetAllListAsync();
            List<Role> allRoles = new List<Role>();
            foreach (var item in allRoleIReadOnlyList)
            {
                allRoles.Add(item);
            }
            //DgDict.Add("allRoles", new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(allRoles)));
            //DgDict.Add("allRoles", allRoles);
            //Mapper.Map<List<RoleDto>>(allRoles);

            DgDict.Add("allRoles", Mapper.Map<List<RoleDto>>(allRoles));
            //new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleDto>>(allRoles));

            //new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleDto>>(allRoles));

            #endregion

            #region 可分配角色列表
            bool canAssignRolesFromAdmin = PermissionChecker.IsGranted(PermissionNames.Pages_Inspection);
            bool canAssignRolesFromRQAdmin = PermissionChecker.IsGranted(PermissionNames.Pages_Inspection);
            bool canAssignRolesFromRQAssitant = PermissionChecker.IsGranted(PermissionNames.Pages_Inspection);
            List<Role> RolescanAssigned = allRoles;

            string[] outAdmin = { "" };
            //List<Role> RolesSource = allRoles;
            //如果任务已经分配且未分配给自己，且不具有分配任务权限，则抛出异常
            if (canAssignRolesFromAdmin) //var RolesUnderYouers = allRoles;
            {
                foreach (Role item in RolescanAssigned)
                {
                    if (outAdmin.Contains(item.Name))
                    {
                        RolescanAssigned.Remove(item);
                    }  //item.SetNormalizedName in
                }
                DgDict.Add("RolescanAssigned", new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(RolescanAssigned)));
                //throw new AbpAuthorizationException("没有分配任务给他人的权限！");
            }
            else if (canAssignRolesFromRQAdmin)
            {
                DgDict.Add("RolescanAssigned", new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(RolescanAssigned)));

            }
            else if (canAssignRolesFromRQAssitant)
            {
                DgDict.Add("RolescanAssigned", new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(RolescanAssigned)));

            }
            else
            {
                DgDict.Add("RolescanAssigned", new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(RolescanAssigned)));

            }
            //    = await _roleRepository.GetAllListAsync();
            //DgDict.Add("allRoles", new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(allRoles)));
            #endregion

            #region 
            #endregion


            #region 
            #endregion

            #region TrafficLogDict物流状态字典
            TrafficLog[] TrafficLogDict =
            {

                new TrafficLog(0, " 1", "2"),
                new TrafficLog(1, " 1", "2"),
                new TrafficLog(2, " 1", "2"),
                new TrafficLog(3, " 1", "2"),
                new TrafficLog(4, " 1", "2"),
                new TrafficLog(5, " 1", "2"),
                new TrafficLog(6, " 1", "2"),
                new TrafficLog(7, " 1", "2"),
                new TrafficLog(10086, " 1", "2")


            };
            DgDict.Add("TrafficLogDict", TrafficLogDict);

            #endregion

            return Json(DgDict);
        }


        [HttpPost("api/AuthenticateLogin")]
        public async Task<JsonResult> AuthenticateLogin([FromBody] AuthenticateModel model)
        {//AuthenticateResultModel
            //IList<object> DgDict = new List<object>();

            var loginResult = await GetLoginResultAsync(
                model.UserNameOrEmailAddress,
                model.Password,
                GetTenancyNameOrNull()
            );

            SortedDictionary<string, object> DgDict = new SortedDictionary<string, object>();
            bool canAssignInspectionToOther = PermissionChecker.IsGranted(PermissionNames.Pages_Inspection);
            //如果任务已经分配且未分配给自己，且不具有分配任务权限，则抛出异常
            //if (input.AssignedPersonId.Value != AbpSession.GetUserId() && !canAssignInspectionToOther)
            //{
            //    throw new AbpAuthorizationException("没有分配任务给他人的权限！");
            //}

            var UserId = AbpSession.GetUserId();
            DgDict.Add("UserId", UserId);

            if (!canAssignInspectionToOther)
            {
                DgDict.Add("canAssignInspectionToOther", "没有Pages_Inspection");
            }
            else { DgDict.Add("canAssignInspectionToOther", "拥有Pages_Inspection"); }

            return Json(DgDict);
        }


        #endregion


        #region 支持方法

        public async Task testAsync()
        {
            SortedDictionary<string, object> DgDict = new SortedDictionary<string, object>();
            #region 角色列表版本

            DgDict.Add("permissionsToRolesVersion", 1001);

            #endregion

            #region 权限列表
            var Permissions = PermissionManager.GetAllPermissions();
            DgDict.Add("allPermissions", new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(Permissions)
            ));
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


            DgDict.Add("allRoles", Mapper.Map<List<RoleListDto>>(allRoles));
            //DgDict.Add("allRoles", new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(allRoles)));

            //new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(allRoles));

            #endregion

            #region  是否有审核权 canAssignInspectionToOther
            bool canAssignInspectionToOther = PermissionChecker.IsGranted(PermissionNames.Pages_Inspection);
            //如果任务已经分配且未分配给自己，且不具有分配任务权限，则抛出异常
            //if (input.AssignedPersonId.Value != AbpSession.GetUserId() && !canAssignInspectionToOther)
            //{
            //    throw new AbpAuthorizationException("没有分配任务给他人的权限！");
            //}

            //var UserId = AbpSession.GetUserId();
            //DgDict.Add("UserId", UserId);

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

            //return Json(DgDict);

            #endregion


            //bool canAssignRolesFromAdmin = PermissionChecker.IsGranted(PermissionNames.Pages_Inspection);
            //bool canAssignRolesFromRQAdmin = PermissionChecker.IsGranted(PermissionNames.Pages_Inspection);
            //bool canAssignRolesFromRQAssitant = PermissionChecker.IsGranted(PermissionNames.Pages_Inspection);


            bool canAssignRolesFromAdmin = PermissionChecker.IsGranted(PermissionNames.Pages_Tenants);
            bool canAssignRolesFromRQAdmin = PermissionChecker.IsGranted(PermissionNames.Pages_Admin);
            bool canAssignRolesFromRQAssitant = PermissionChecker.IsGranted(PermissionNames.Pages_RQAssitant);
            List<Role> RolescanAssigned = allRoles;


            if (canAssignRolesFromAdmin) //var RolesUnderYouers = allRoles;
            {
                DgDict.Add("myTopRoleBack", "canAssignRolesFromAdmin");
            }
            else if (canAssignRolesFromRQAdmin)
            {
                DgDict.Add("myTopRoleBack", "canAssignRolesFromRQAdmin");
            }
            else if (canAssignRolesFromRQAssitant)
            {
                DgDict.Add("myTopRoleBack", "canAssignRolesFromRQAssitant");
            }
            else
            {
                DgDict.Add("myTopRoleBack", null);

            }

            #region 可分配角色列表--针对员工管理
            List<Role> allmyRoles = new List<Role>();//当前用户可分配的角色

            List<string> RolescanAssignedString = new List<string>();
            foreach (var item in allRoleIReadOnlyList)
            {
                allRoles.Add(item);
                RolescanAssignedString.Add(item.Name);
            }
            //string[] outAdmin = { "" };
            //List<Role> RolesSource = allRoles;
            //如果任务已经分配且未分配给自己，且不具有分配任务权限，则抛出异常
            if (canAssignRolesFromAdmin) //var RolesUnderYouers = allRoles;
            {
                //string[] outAdmin = { "" };
                //foreach (Role item in RolescanAssigned)
                //{
                //    if (outAdmin.Contains(item.Name))
                //    {
                //        RolescanAssigned.Remove(item);
                //    }  //item.SetNormalizedName in
                //} 
                //DgDict.Add("allRoles", Mapper.Map<List<RoleDto>>(allRoles));
                DgDict.Add("RolescanAssigned", Mapper.Map<List<RoleDto>>(RolescanAssigned));
                //throw new AbpAuthorizationException("没有分配任务给他人的权限！");
            }
            else if (canAssignRolesFromRQAdmin)
            {
                string[] outAdmin =  { "Admin", "RQAdmin",
                    "RQAdminPermissions", "RQAssitantPermissions","wangyuanqing" };
                //foreach (Role item in RolescanAssigned)
                //{
                //    if (outAdmin.Contains(item.Name))
                //    {
                //        RolescanAssigned.Remove(item);
                //    }  //item.SetNormalizedName in
                //}



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
                string[] outAdmin =  { "Admin", "RQAdminPermissions",
                    "RQAdmin" , "RQAssitant" ,"RQAssitantPermissions","wangyuanqing"};

                //foreach (Role item in RolescanAssigned)
                //{
                //    if (outAdmin.Contains(item.Name))
                //    {
                //        RolescanAssigned.Remove(item);
                //    }  //item.SetNormalizedName in
                //}
                //DgDict.Add("RolescanAssigned", Mapper.Map<List<RoleDto>>(RolescanAssigned));
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
                //DgDict.Add("RolescanAssigned", Mapper.Map<List<RoleDto>>(allRoles)); 
            }


            //    = await _roleRepository.GetAllListAsync();
            //DgDict.Add("allRoles", new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(allRoles)));
            #endregion

            #region 可分配权限列表【角色管理-分配权限】
            var PermissionscanAssigned = allPermissions;
            List<Permission> myPermissions = new List<Permission>();
            List<string> PermissionscanAssignedString = new List<string>();

            foreach (var item in PermissionscanAssigned)
            {
                PermissionscanAssignedString.Add(item.Name);
            }

            //DgDict.Add("PermissionscanAssignedString22", PermissionscanAssignedString);


            //List<Authorization.Roles.Permission> PermissionscanAssigned
            //    = (List<Authorization.Roles.Permission>)allPermissions;


            if (canAssignRolesFromAdmin) //var RolesUnderYouers = allRoles;
            {
                DgDict.Add("PermissionscanAssigned", Mapper.Map<List<PermissionDto>>(PermissionscanAssigned));
                DgDict.Add("PermissionscanAssignedString", PermissionscanAssignedString);
                //throw new AbpAuthorizationException("没有分配任务给他人的权限！");
            }
            else if (canAssignRolesFromRQAdmin)
            {
                string[] outAdmin = {"Pages","Pages.Tenants",
                    "Pages.Users", "Pages.Roles", "Pages.Admin",
                    "Pages.Admin.Users","Pages.Admin.Roles"
                };
                //foreach (var item in PermissionscanAssignedString)
                //{
                //    if (outAdmin.Contains(item))
                //    {
                //        PermissionscanAssignedString.Remove(item);
                //    }  //item.SetNormalizedName in
                //}

                foreach (var item in outAdmin)
                {
                    if (PermissionscanAssignedString.Contains(item))
                    {
                        PermissionscanAssignedString.Remove(item);
                    }  //item.SetNormalizedName in
                }
                foreach (var itemStr in PermissionscanAssignedString)
                {
                    foreach (var item in PermissionscanAssigned)
                    {
                        if (item.Name == itemStr)
                        {
                            myPermissions.Add(item);
                        }
                    }
                }
                //Mapper.Map<List<PermissionDto>>(PermissionscanAssigned); 
                DgDict.Add("PermissionscanAssigned", Mapper.Map<List<PermissionDto>>(myPermissions));
                DgDict.Add("PermissionscanAssignedString", PermissionscanAssignedString);
            }
            else if (canAssignRolesFromRQAssitant)
            {
                //string[] outAdmin = { "Admin", "RQAdminPermissions" };
                foreach (Role item in RolescanAssigned)
                {
                    if (item.Name == "RQAssitantPermissions")
                    {
                        //DgDict.Add("PermissionscanAssignedRoles",
                        //   Convert.ToInt32(item.Permissions));
                        DgDict.Add("PermissionscanAssignedRoles",
                                item.Permissions);
                        //item.Permissions
                        //List<Permission> allPermissionsTemp = new List<Permission>();
                        //foreach (var itemTemp in item.Permissions)
                        //{
                        //    DgDict.Add("PermissionscanAssignedID",
                        //        Convert.ToInt32(itemTemp.RoleId));

                        //    //allPermissions.Add(itemTemp);
                        //}

                        //PermissionscanAssigned = 
                        //RolescanAssigned.Remove(item);
                    }  //item.SetNormalizedName in
                }

                string[] outAdmin = {"Pages","Pages.Tenants",
                    "Pages.Users", "Pages.Roles", "Pages.Admin",
                    "Pages.Admin.Users","Pages.Admin.Roles",
                    "Pages.RQAssitant.Roles", "Pages.RQAssitant.Users",
                    "Pages.RQAssitant",
                };
                //foreach (Permission item in PermissionscanAssigned)
                //{
                //    if (outAdmin.Contains(item.Name))
                //    {
                //        PermissionscanAssigned.Remove(item);
                //    }  //item.SetNormalizedName in 
                //}

                //foreach (var item in PermissionscanAssignedString)
                //{
                //    if (outAdmin.Contains(item))
                //    {
                //        PermissionscanAssignedString.Remove(item);
                //    }  //item.SetNormalizedName in
                //}

                foreach (var item in outAdmin)
                {
                    if (PermissionscanAssignedString.Contains(item))
                    {
                        PermissionscanAssignedString.Remove(item);
                    }  //item.SetNormalizedName in
                }

                foreach (var itemStr in PermissionscanAssignedString)
                {
                    foreach (var item in PermissionscanAssigned)
                    {
                        if (item.Name == itemStr)
                        {
                            myPermissions.Add(item);
                        }
                    }
                }

                DgDict.Add("PermissionscanAssigned", Mapper.Map<List<PermissionDto>>(myPermissions));

                DgDict.Add("PermissionscanAssignedString", PermissionscanAssignedString);
            }
            else
            {

                DgDict.Add("PermissionscanAssignedString", null);
                DgDict.Add("PermissionscanAssigned", null);
                //DgDict.Add("PermissionscanAssigned", Mapper.Map<List<RoleDto>>(allRoles)); 
            }

            #endregion


            //return Json(DgDict);

        }
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