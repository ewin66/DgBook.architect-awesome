



# dotnet new

https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22



-  								2019/05/06 							
-  								[ 									 												![img](https://github.com/mairaw.png?size=32) 												![img](https://github.com/olprod.png?size=32) 												![img](assets/14702782.png) 									 								](https://github.com/dotnet/docs.zh-cn/blob/live/docs/core/tools/dotnet-new.md) 							

**本主题适用于：✓** .NET Core 1.x SDK **✓** .NET Core 2.x SDK

## name

`dotnet new` - 根据指定的模板，创建新的项目、配置文件或解决方案。

## 摘要

-  [.NET Core 2.2](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#tabpanel_CeZOj-G++Q_netcore22) 
-  [.NET Core 2.1](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#tabpanel_CeZOj-G++Q_netcore21) 
-  [.NET Core 2.0](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#tabpanel_CeZOj-G++Q_netcore20) 
-  [.NET Core 1.x](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#tabpanel_CeZOj-G++Q_netcore1x) 

console 			

```console
dotnet new <TEMPLATE> [--dry-run] [--force] [-i|--install] [-lang|--language] [-n|--name] [--nuget-source] [-o|--output] [-u|--uninstall] [Template options]
dotnet new <TEMPLATE> [-l|--list] [--type]
dotnet new [-h|--help]
```

## 说明

`dotnet new` 命令为初始化有效的 .NET Core 项目提供了便捷方法。

命令调用[模板引擎](https://github.com/dotnet/templating)，以根据指定的模板和选项在磁盘上创建项目。

## 自变量

```
TEMPLATE
```

调用命令时要实例化的模板。 每个模板可能具有可传递的特定选项。 有关详细信息，请参阅[模板选项](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#template-options)。

如果 `TEMPLATE` 值与“模板”或“短名称”列中的文本不完全匹配，则会对这两列执行 substring 匹配。

-  [.NET Core 2.2](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#tabpanel_CeZOj-G++Q-1_netcore22) 
-  [.NET Core 2.1](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#tabpanel_CeZOj-G++Q-1_netcore21) 
-  [.NET Core 2.0](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#tabpanel_CeZOj-G++Q-1_netcore20) 
-  [.NET Core 1.x](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#tabpanel_CeZOj-G++Q-1_netcore1x) 

此命令包含默认的模板列表。 使用 `dotnet new -l` 获取可用模板的列表。 下表显示了随 .NET Core SDK 2.2.100 一起预安装的模板。 模板的默认语言显示在括号内。

| 模板                                              | 短名称             | 语言         | Tags                    |
| ------------------------------------------------- | ------------------ | ------------ | ----------------------- |
| 控制台应用程序                                    | `console`          | [C#]、F#、VB | 常用/控制台             |
| 类库                                              | `classlib`         | [C#]、F#、VB | 常用/库                 |
| 单元测试项目                                      | `mstest`           | [C#]、F#、VB | 测试/MSTest             |
| NUnit 3 测试项目                                  | `nunit`            | [C#]、F#、VB | 测试/NUnit              |
| NUnit 3 测试项                                    | `nunit-test`       | [C#]、F#、VB | 测试/NUnit              |
| xUnit 测试项目                                    | `xunit`            | [C#]、F#、VB | 测试/xUnit              |
| Razor 页                                          | `page`             | [C#]         | Web/ASP.NET             |
| MVC ViewImports                                   | `viewimports`      | [C#]         | Web/ASP.NET             |
| MVC ViewStart                                     | `viewstart`        | [C#]         | Web/ASP.NET             |
| ASP.NET Core 空                                   | `web`              | [C#]，F#     | Web/空                  |
| ASP.NET Core Web 应用程序 (Model-View-Controller) | `mvc`              | [C#]，F#     | Web/MVC                 |
| ASP.NET Core Web 应用程序                         | `webapp`， `razor` | [C#]         | Web/MVC/Razor Pages     |
| 含 Angular 的 ASP.NET Core                        | `angular`          | [C#]         | Web/MVC/SPA             |
| 含 React.js 的 ASP.NET Core                       | `react`            | [C#]         | Web/MVC/SPA             |
| 含 React.js 和 Redux 的 ASP.NET Core              | `reactredux`       | [C#]         | Web/MVC/SPA             |
| Razor 类库                                        | `razorclasslib`    | [C#]         | Web/Razor/库/Razor 类库 |
| ASP.NET Core Web API                              | `webapi`           | [C#]，F#     | Web/WebAPI              |
| global.json 文件                                  | `globaljson`       |              | 配置                    |
| NuGet 配置                                        | `nugetconfig`      |              | 配置                    |
| Web 配置                                          | `webconfig`        |              | 配置                    |
| 解决方案文件                                      | `sln`              |              | 解决方案                |

## 选项

-  [.NET Core 2.2](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#tabpanel_CeZOj-G++Q-2_netcore22) 
-  [.NET Core 2.1](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#tabpanel_CeZOj-G++Q-2_netcore21) 
-  [.NET Core 2.0](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#tabpanel_CeZOj-G++Q-2_netcore20) 
-  [.NET Core 1.x](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#tabpanel_CeZOj-G++Q-2_netcore1x) 

```
--dry-run
```

显示有关以下内容的摘要：给定命令运行导致模板创建时发生的情况。

```
--force
```

强制生成内容，即使会更改现有文件，也不例外。 输出目录已包含一个项目时，可能需要此操作。

```
-h|--help
```

打印命令帮助。 可针对 `dotnet new` 命令本身或任何模板（如 `dotnet new mvc --help`）调用它。

```
-i|--install <PATH|NUGET_ID>
```

从提供的 `PATH` 或 `NUGET_ID` 安装源或模板包。 若要安装模板包的预发布版本，需要以 `<package-name>::<package-version>` 格式指定该版本。 默认情况下，`dotnet new` 为该版本传递 *，表示最后一个稳定的包版本。 请在[示例](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#examples)部分查看示例。

若要了解如何创建自定义模板，请参阅 [dotnet new 自定义模板](https://docs.microsoft.com/zh-cn/dotnet/core/tools/custom-templates)。

```
-l|--list
```

列出包含指定名称的模板。 如果针对 `dotnet new` 命令调用，则它列出可能对给定的目录可用的模板。 例如，如果该目录已包含一个项目，则它不会列出所有项目模板。

```
-lang|--language {C#|F#|VB}
```

要创建的模板的语言。 接受的语言因模板而异（请参阅[参数](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#arguments)部分中的默认值）。 对于某些模板无效。

 备注

某些 shell 将 `#` 解释为特殊字符。 在这些情况下，需要括住语言参数值，如 `dotnet new console -lang "F#"`。

```
-n|--name <OUTPUT_NAME>
```

所创建的输出的名称。 如果未指定名称，使用的是当前目录的名称。

```
--nuget-source
```

指定在安装期间要使用的 NuGet 源。

```
-o|--output <OUTPUT_DIRECTORY>
```

用于放置生成的输出的位置。 默认为当前目录。

```
--type
```

根据可用类型筛选模板。 预定义的值为“项目”、“项”或“其他”。

```
-u|--uninstall <PATH|NUGET_ID>
```

从提供的 `PATH` 或 `NUGET_ID` 卸载源或模板包。 如果排除 `<PATH|NUGET_ID>` 值，将显示所有当前安装的模板包及其关联的模板。

 备注

若要使用 `PATH` 卸载模板，需要完全限定路径。 例如，C:/Users/<USER>/Documents/Templates/GarciaSoftware.ConsoleTemplate.CSharp  有效，但是包含文件夹中的 ./GarciaSoftware.ConsoleTemplate.CSharp 无效。 此外，模板路径中不要包含最后的终止目录斜杠。

## 模板选项

每个项目模板都可能有附加选项。 核心模板有以下附加选项：

-  [.NET Core 2.2](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#tabpanel_CeZOj-G++Q-3_netcore22) 
-  [.NET Core 2.1](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#tabpanel_CeZOj-G++Q-3_netcore21) 
-  [.NET Core 2.0](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#tabpanel_CeZOj-G++Q-3_netcore20) 
-  [.NET Core 1.x](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-new?tabs=netcore22#tabpanel_CeZOj-G++Q-3_netcore1x) 

**控制台**

`--langVersion <VERSION_NUMBER>` - 在已创建的项目文件中设置 `LangVersion` 属性。 例如，使用 `--langVersion 7.3` 以使用 C# 7.3。 不支持 F#。

`--no-restore` - 在项目创建期间不执行隐式还原。

**angular、react、reactredux**

`--exclude-launch-settings` - 从生成的模板中排除 launchSettings.json。

`--no-restore` - 在项目创建期间不执行隐式还原。

`--no-https` - 项目不需要 HTTPS。 此选项仅适用于未使用 `IndividualAuth` 或 `OrganizationalAuth` 的情况。

**razorclasslib**

`--no-restore` - 在项目创建期间不执行隐式还原。

**classlib**

`-f|--framework <FRAMEWORK>` - 指定目标[框架](https://docs.microsoft.com/zh-cn/dotnet/standard/frameworks)。 值：`netcoreapp2.2`（要创建 .NET Core 类库的话）或 `netstandard2.0`（要创建 .NET Standard 类库的话）。 默认值为 `netstandard2.0`。

`--langVersion <VERSION_NUMBER>` - 在已创建的项目文件中设置 `LangVersion` 属性。 例如，使用 `--langVersion 7.3` 以使用 C# 7.3。 不支持 F#。

`--no-restore` - 在项目创建期间不执行隐式还原。

**mstest、xunit**

`-p|--enable-pack` - 允许使用 [dotnet pack](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-pack) 为项目打包。

`--no-restore` - 在项目创建期间不执行隐式还原。

**nunit**

`-f|--framework <FRAMEWORK>` - 指定目标[框架](https://docs.microsoft.com/zh-cn/dotnet/standard/frameworks)。 默认值为 `netcoreapp2.1`。

`-p|--enable-pack` - 允许使用 [dotnet pack](https://docs.microsoft.com/zh-cn/dotnet/core/tools/dotnet-pack) 为项目打包。

`--no-restore` - 在项目创建期间不执行隐式还原。

**page**

`-na|--namespace <NAMESPACE_NAME>` - 已生成代码的命名空间。 默认值为 `MyApp.Namespace`。

`-np|--no-pagemodel` - 创建不含 PageModel 的页。

**viewimports**

`-na|--namespace <NAMESPACE_NAME>` - 已生成代码的命名空间。 默认值为 `MyApp.Namespace`。

**web**

`--exclude-launch-settings` - 从生成的模板中排除 launchSettings.json。

`--no-restore` - 在项目创建期间不执行隐式还原。

`--no-https` - 项目不需要 HTTPS。 此选项仅适用于未使用 `IndividualAuth` 或 `OrganizationalAuth` 的情况。

**mvc、webapp**

`-au|--auth <AUTHENTICATION_TYPE>` - 要使用的身份验证类型。 可能的值为：

- `None` - 不进行身份验证（默认）。
- `Individual` - 个人身份验证。
- `IndividualB2C` - 使用 Azure AD B2C 进行个人身份验证。
- `SingleOrg` - 对一个租户进行组织身份验证。
- `MultiOrg` - 对多个租户进行组织身份验证。
- `Windows` - Windows 身份验证。

`--aad-b2c-instance <INSTANCE>` - 要连接到的 Azure Active Directory B2C 实例。 与 `IndividualB2C` 身份验证结合使用。 默认值为 `https://login.microsoftonline.com/tfp/`。

`-ssp|--susi-policy-id <ID>` - 此项目的登录和注册策略 ID。 与 `IndividualB2C` 身份验证结合使用。

`-rp|--reset-password-policy-id <ID>` - 此项目的重置密码策略 ID。 与 `IndividualB2C` 身份验证结合使用。

`-ep|--edit-profile-policy-id <ID>` - 此项目的编辑配置文件策略 ID。 与 `IndividualB2C` 身份验证结合使用。

`--aad-instance <INSTANCE>` - 要连接到的 Azure Active Directory 实例。 与 `SingleOrg` 或 `MultiOrg` 身份验证结合使用。 默认值为 `https://login.microsoftonline.com/`。

`--client-id <ID>` - 此项目的客户端 ID。 与 `IndividualB2C`、`SingleOrg` 或 `MultiOrg` 身份验证结合使用。 默认值为 `11111111-1111-1111-11111111111111111`。

`--domain <DOMAIN>` - 目录租户的域。 与 `SingleOrg` 或 `IndividualB2C` 身份验证结合使用。 默认值为 `qualified.domain.name`。

`--tenant-id <ID>` - 要连接到的目录的 TenantId ID。 与 `SingleOrg` 身份验证结合使用。 默认值为 `22222222-2222-2222-2222-222222222222`。

`--callback-path <PATH>` - 重定向 URI 的应用程序基路径中的请求路径。 与 `SingleOrg` 或 `IndividualB2C` 身份验证结合使用。 默认值为 `/signin-oidc`。

`-r|--org-read-access` - 允许此应用程序对目录进行读取访问。 仅适用于 `SingleOrg` 或 `MultiOrg` 身份验证。

`--exclude-launch-settings` - 从生成的模板中排除 launchSettings.json。

`--no-https` - 项目不需要 HTTPS。 `app.UseHsts` 和 `app.UseHttpsRedirection` 未添加到 `Startup.Configure` 中。 此选项仅适用于未使用 `Individual`、`IndividualB2C``SingleOrg` 和 `MultiOrg` 的情况。

`-uld|--use-local-db` - 指定应使用 LocalDB，而不使用 SQLite。 仅适用于 `Individual` 或 `IndividualB2C` 身份验证。

`--no-restore` - 在项目创建期间不执行隐式还原。

**webapi**

`-au|--auth <AUTHENTICATION_TYPE>` - 要使用的身份验证类型。 可能的值为：

- `None` - 不进行身份验证（默认）。
- `IndividualB2C` - 使用 Azure AD B2C 进行个人身份验证。
- `SingleOrg` - 对一个租户进行组织身份验证。
- `Windows` - Windows 身份验证。

`--aad-b2c-instance <INSTANCE>` - 要连接到的 Azure Active Directory B2C 实例。 与 `IndividualB2C` 身份验证结合使用。 默认值为 `https://login.microsoftonline.com/tfp/`。

`-ssp|--susi-policy-id <ID>` - 此项目的登录和注册策略 ID。 与 `IndividualB2C` 身份验证结合使用。

`--aad-instance <INSTANCE>` - 要连接到的 Azure Active Directory 实例。 与 `SingleOrg` 身份验证结合使用。 默认值为 `https://login.microsoftonline.com/`。

`--client-id <ID>` - 此项目的客户端 ID。 与 `IndividualB2C` 或 `SingleOrg` 身份验证结合使用。 默认值为 `11111111-1111-1111-11111111111111111`。

`--domain <DOMAIN>` - 目录租户的域。 与 `SingleOrg` 或 `IndividualB2C` 身份验证结合使用。 默认值为 `qualified.domain.name`。

`--tenant-id <ID>` - 要连接到的目录的 TenantId ID。 与 `SingleOrg` 身份验证结合使用。 默认值为 `22222222-2222-2222-2222-222222222222`。

`-r|--org-read-access` - 允许此应用程序对目录进行读取访问。 仅适用于 `SingleOrg` 或 `MultiOrg` 身份验证。

`--exclude-launch-settings` - 从生成的模板中排除 launchSettings.json。

`--no-https` - 项目不需要 HTTPS。 `app.UseHsts` 和 `app.UseHttpsRedirection` 未添加到 `Startup.Configure` 中。 此选项仅适用于未使用 `Individual`、`IndividualB2C``SingleOrg` 和 `MultiOrg` 的情况。

`-uld|--use-local-db` - 指定应使用 LocalDB，而不使用 SQLite。 仅适用于 `Individual` 或 `IndividualB2C` 身份验证。

`--no-restore` - 在项目创建期间不执行隐式还原。

**globaljson**

`--sdk-version <VERSION_NUMBER>` - 指定要在 global.json 文件中使用的 .NET Core SDK 版本。

## 示例

通过指定模板名称，创建 C# 控制台应用程序项目：

```
dotnet new "Console Application"
```

在当前目录中创建 F# 控制台应用程序项目：

```
dotnet new console -lang F#
```

在指定目录中创建 .NET Standard 类库项目（仅适用于 .NET Core SDK 2.0 或更高版本）：

```
dotnet new classlib -lang VB -o MyLibrary
```

在当前目录中新建没有设置身份验证的 ASP.NET Core C# MVC 项目：

```
dotnet new mvc -au None
```

创建新的 xUnit 项目：

```
dotnet new xunit
```

列出适用于 MVC 的所有模板：

```
dotnet new mvc -l
```

列出与“we”子字符串匹配的所有模板。 找不到完全匹配，因此子字符串匹配针对短名称和名称列运行。

```
dotnet new we -l
```

尝试调用与 ng 匹配的模板。 如果无法确定单个匹配项，请列出部分匹配项的模板。

```
dotnet new ng
```

安装适用于 ASP.NET Core 的单页应用程序模板 2.0 版（命令选项仅可用于 .NET Core SDK 1.1 及更高版本）：

```
dotnet new -i Microsoft.DotNet.Web.Spa.ProjectTemplates::2.0.0
```

在当前目录中创建 global.json，将 SDK 版本设为 2.0.0（仅适用于 .NET Core SDK 2.0 或更高版本）：

```
dotnet new globaljson --sdk-version 2.0.0
```