# 安装和管理包在 VS 中使用 [PowerShell](https://docs.microsoft.com/zh-cn/nuget/tools/package-manager-console#setting-up-a-nuget-powershell-profile)

# 安装包Install-Package

找到想要安装的程序包。 	运行安装命令



```powershell
# Find packages containing the keyword "elmah"
Find-Package elmah
# Install the Elmah package to the project named MyProject.
Install-Package Elmah -ProjectName MyProject


Install-Package [-Id] <string> [-IgnoreDependencies] [-ProjectName <string>] [[-Source] <string>] 
    [[-Version] <string>] [-IncludePrerelease] [-FileConflictAction] [-DependencyVersion]
    [-WhatIf] [<CommonParameters>]

```



## 参数

| 参数               | 描述                                                         |
| ------------------ | ------------------------------------------------------------ |
| Id                 | （必需）要安装的包的标识符。 (*3.0 +*) 的路径或 URL 的标识符可以是`packages.config`文件或`.nupkg`文件。 -Id 开关本身是可选的。 |
| IgnoreDependencies | 安装此包仅不及其依赖项。                                     |
| ProjectName        | 要在其中安装包，默认值为默认项目项目。                       |
| 源                 | 要搜索的包源 URL 或文件夹路径。 本地文件夹路径可以是绝对的或相对于当前文件夹。 如果省略，`Install-Package`搜索当前所选的包源。 |
| 版本               | 若要安装，包的版本默认为最新版本。                           |
| IncludePrerelease  | 考虑安装的预发行包。 如果省略，则被视为仅稳定的包。          |
| FileConflictAction | 当要求您覆盖或忽略现有的项目所引用的文件时要执行的操作。 可能的值为*覆盖、 忽略、 None、 OverwriteAll*，并 *（3.0 +）* *IgnoreAll*。 |
| DependencyVersion  | 版本的依赖项包使用，可以是以下值之一： *最低*（默认值）： 最低版本*HighestPatch*： 具有最低主要、 次要最低、 最高的修补程序版本*HighestMinor*： 具有最低主要版本、 最小的、 最高的修补程序*最高*（默认值为更新包不带任何参数）： 最高版本您可以设置默认值使用[ `dependencyVersion` ](https://docs.microsoft.com/zh-cn/nuget/reference/nuget-config-file#config-section)中设置`Nuget.Config`文件。 |
| WhatIf             | 显示运行该命令而不实际执行安装时，会发生什么情况。           |

任何这些参数接受管道输入或通配符字符。

## 通用参数

`Install-Package` 支持以下[常见的 PowerShell 参数](http://go.microsoft.com/fwlink/?LinkID=113216)： 调试、 错误操作、 ErrorVariable、 OutBuffer、 OutVariable、 PipelineVariable、 Verbose、 WarningAction 和 WarningVariable。

# 卸载包Uninstall-Package

```powershell
# Uninstalls the Elmah package from the default project
Uninstall-Package Elmah
# Uninstalls the Elmah package and all its unused dependencies
Uninstall-Package Elmah -RemoveDependencies 
# Uninstalls the Elmah package even if another package depends on it
Uninstall-Package Elmah -Force

Uninstall-Package [-Id] <string> [-RemoveDependencies] [-ProjectName <string>] [-Force]
    [-Version <string>] [-WhatIf] [<CommonParameters>]
```



## 参数

| 参数               | 描述                                                         |
| ------------------ | ------------------------------------------------------------ |
| Id                 | （必需）要卸载的包的标识符。 -Id 开关本身是可选的。          |
| 版本               | 若要卸载，包的版本默认为当前安装的版本。                     |
| RemoveDependencies | 卸载程序包及其未使用的依赖项。 也就是说，如果任何依赖项具有依赖于它的另一个包，它会跳过。 |
| ProjectName        | 要从中卸载程序包，默认值为默认项目项目。                     |
| 强制               | 即使其他程序包依赖于它强制要卸载的包。                       |
| WhatIf             | 显示无需实际执行卸载运行命令时，会发生什么情况。             |

任何这些参数接受管道输入或通配符字符。

## 通用参数

`Uninstall-Package` 支持以下[常见的 PowerShell 参数](http://go.microsoft.com/fwlink/?LinkID=113216)： 调试、 错误操作、 ErrorVariable、 OutBuffer、 OutVariable、 PipelineVariable、 Verbose、 WarningAction 和 WarningVariable。







# 更新包Get-Package



```bash
# Checks if there are newer versions available for any installed packages
Get-Package -updates

# Updates a specific package using its identifier, in this case jQuery
Update-Package jQuery

# Update all packages in the project named MyProject (as it appears in Solution Explorer)
Update-Package -ProjectName MyProject

# Update all packages in the solution
Update-Package
Get-Package -Source <string> [-ListAvailable] [-Updates] [-ProjectName <string>]
    [-Filter <string>] [-First <int>] [-Skip <int>] [-AllVersions] [-IncludePrerelease]
    [-PageSize] [<CommonParameters>]

```

## 参数

| 参数              | 描述                                                         |
| ----------------- | ------------------------------------------------------------ |
| 源                | 包的 URL 或文件夹路径。 本地文件夹路径可以是绝对的或相对于当前文件夹。 如果省略，`Get-Package`搜索当前所选的包源。 与-ListAvailable，一起使用时默认为 nuget.org。 |
| ListAvailable     | 列出了默认值为 nuget.org 的包源中可用的包。除非另有指定的 PageSize 和/或-第一个显示默认值为 50 的包。 |
| 更新              | 列出包源中有可用更新的包。                                   |
| ProjectName       | 获取已安装的包的项目。 如果省略，则返回安装整个解决方案的项目。 |
| 筛选器            | 用来将其应用到包 ID、 说明和标记来缩小包列表的筛选器字符串。 |
| First             | 若要从列表开头返回的包数。 如果未指定，默认为 50。           |
| Skip              | 省略了第一个<int>显示的列表中的包。                          |
| AllVersions       | 显示所有可用版本的每个包而不是仅最新版本。                   |
| IncludePrerelease | 在结果中包括预发行包。                                       |
| PageSize          | *（3.0 +)* 与一起使用时-ListAvailable （必需），包的数量以列出提供提示以继续之前。 |

任何这些参数接受管道输入或通配符字符。

## 通用参数

`Get-Package` 支持以下[常见的 PowerShell 参数](http://go.microsoft.com/fwlink/?LinkID=113216)： 调试、 错误操作、 ErrorVariable、 OutBuffer、 OutVariable、 PipelineVariable、 Verbose、 WarningAction 和 WarningVariable。



# 查找包Find-Package 

```bash
# Find packages containing keywords
Find-Package elmah
Find-Package logging
# List packages whose ID begins with Elmah
Find-Package Elmah -StartWith
# By default, Get-Package returns a list of 20 packages; use -First to show more
Find-Package logging -First 100
# List all versions of the package with the ID of "jquery"
Find-Package jquery -AllVersions -ExactMatch

Find-Package [-Id] <keywords> -Source <string> [-AllVersions] [-First [<int>]]
    [-Skip <int>] [-IncludePrerelease] [-ExactMatch] [-StartWith] [<CommonParameters>]
```

## 参数

| 参数              | 描述                                                         |
| ----------------- | ------------------------------------------------------------ |
| Id<关键字>        | （必需）搜索包源时要使用的关键字。 使用-ExactMatch 返回关键字匹配的包 ID 这些包。 如果不给定了任何关键字，`Find-Package`返回首次为-指定数或下载的前 20 个包的列表。 请注意，-Id 是可选的执行任何操作。 |
| 源                | 要搜索的包源 URL 或文件夹路径。 本地文件夹路径可以是绝对的或相对于当前文件夹。 如果省略，`Find-Package`搜索当前所选的包源。 |
| AllVersions       | 显示所有可用版本的每个包而不是仅最新版本。                   |
| First             | 若要从列表中; 开头返回的包数默认值为 20。                    |
| Skip              | 省略了第一个<int>显示的列表中的包。                          |
| IncludePrerelease | 在结果中包括预发行包。                                       |
| ExactMatch        | 指定要使用<关键字>作为一个区分大小写的包 id。                |
| StartWith         | 返回包的包 ID 开头<关键字>。                                 |

任何这些参数接受管道输入或通配符字符。

## 通用参数

`Find-Package` 支持以下[常见的 PowerShell 参数](http://go.microsoft.com/fwlink/?LinkID=113216)： 调试、 错误操作、 ErrorVariable、 OutBuffer、 OutVariable、 PipelineVariable、 Verbose、 WarningAction 和 WarningVariable。





#  使用 nuget.exe CLI 在控制台中

若要使[ `nuget.exe` CLI](https://docs.microsoft.com/zh-cn/nuget/tools/nuget-exe-cli-reference)程序包管理器控制台中提供安装[NuGet.CommandLine](http://www.nuget.org/packages/NuGet.CommandLine/)从控制台的包：

ps 			

```ps
# Other versions are available, see http://www.nuget.org/packages/NuGet.CommandLine/
Install-Package NuGet.CommandLine -Version 4.4.1
```