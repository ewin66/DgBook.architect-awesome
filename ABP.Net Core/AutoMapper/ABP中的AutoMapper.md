



# ABP中的AutoMapper

2019-04-27 22:26:19[左手是笋右手是窝头](https://me.csdn.net/sinat_29009169)阅读数 113

​                                分类专栏：                                                                                                            [                                             C#                                        ](https://blog.csdn.net/sinat_29009169/article/category/8406254)                                                                                                

​                                    

​                [                     ](http://creativecommons.org/licenses/by-sa/4.0/)                            版权声明：本文为博主原创文章，遵循[ CC 4.0 BY-SA ](http://creativecommons.org/licenses/by-sa/4.0/)版权协议，转载请附上原文出处链接和本声明。                                               本文链接：https://blog.csdn.net/sinat_29009169/article/details/89608154                            

一、什么是AutoMapper

```csharp
AutoMapper is a simple little library built to solve a deceptively complex problem - getting rid of code that mapped one object to another. 
谷歌翻译过来：
AutoMapper是一个简单的小型库，用于解决一个看似复杂的问题 - 摆脱将一个对象映射到另一个对象的代码。
————————————————
版权声明：本文为CSDN博主「左手是笋右手是窝头」的原创文章，遵循 CC 4.0 BY-SA 版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/sinat_29009169/article/details/89608154
```

个人理解是一个实例到另一个实例的映射。

使用方法如下

```csharp
    AutoMapper.Mapper.CreateMap<User, UserDto>();
    var model = AutoMapper.Mapper.Map<UserDto>(user);
```

以上代码会根据相同参数名进行映射，如果有额外的要求，如不同参数映射，忽略某个参数，如下

```csharp
//将User.Role.Id赋值给UserDto.RoleId
public class User
{
public Role Roler;
	public long Id;	
}
public class UserDto
{
    public long RoleId;
    public long Id;
}
AutoMapper.Mapper.CreateMap<User, UserDto>().ForMember(dest => dest.RoleId,
	opts => opts.MapFrom(src => src.Roler.Id));
//忽略UserDto.Id的映射
CreateMap<User, UserDto>().ForMember(dto => dto.id, opt => opt.Ignore());
————————————————
版权声明：本文为CSDN博主「左手是笋右手是窝头」的原创文章，遵循 CC 4.0 BY-SA 版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/sinat_29009169/article/details/89608154
```

假如UserDto中RoleId改为RolerId，则不需要手动设置映射规则也可以自动映射。

映射只需要创建一次

二、在ABP框架中实体之间进行映射有两种办法

1继承Profile

```
public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<UserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore())
                .ForMember(x => x.LastLoginTime, opt => opt.Ignore());
 
            CreateMap<CreateUserDto, User>();
            CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());
        }
    }
————————————————
版权声明：本文为CSDN博主「左手是笋右手是窝头」的原创文章，遵循 CC 4.0 BY-SA 版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/sinat_29009169/article/details/89608154


```

2.创建映射接口，然后在ApplicationModule中的Initialize()进行声明

```csharp
internal static class AccuseMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Accuse,AccuseListDto>();
            configuration.CreateMap <AccuseListDto,Accuse>();
 
            configuration.CreateMap <AccuseEditDto,Accuse>();
            configuration.CreateMap <Accuse,AccuseEditDto>();
 
        }
	}
————————————————
版权声明：本文为CSDN博主「左手是笋右手是窝头」的原创文章，遵循 CC 4.0 BY-SA 版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/sinat_29009169/article/details/89608154
public override void Initialize()
        {
            var thisAssembly = typeof(ADCsmlzApplicationModule).GetAssembly();
 
            IocManager.RegisterAssemblyByConvention(thisAssembly);
 
            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => {
                    cfg.AddProfiles(thisAssembly);
                    AccuseMapper.CreateMappings(cfg);
                }
            );
        }
————————————————
版权声明：本文为CSDN博主「左手是笋右手是窝头」的原创文章，遵循 CC 4.0 BY-SA 版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/sinat_29009169/article/details/89608154
```

三、MapTo

MapTo是ABP中封装好的方法，根据我们规定的两个实体之间的映射，将值一一映射。null也会被更新到字段中。