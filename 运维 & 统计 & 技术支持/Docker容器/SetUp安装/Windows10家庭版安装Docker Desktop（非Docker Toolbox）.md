

# Windows10家庭版安装Docker Desktop（非Docker Toolbox）



*现在大部分笔记本预装的都是win10家庭版，而家庭版又不支持Hyper-V，Docker Desktop是无法直接安装的。但其实家庭版是可以通过脚本开启Hyper-V来安装Docker Desktop的。下面就教大家如何操作。*

### 开启Hyper-V

添加方法非常简单，把以下内容保存为.cmd文件，然后以管理员身份打开这个文件。提示重启时保存好文件重启吧，重启完成就能使用功能完整的Hyper-V了。

```
pushd "%~dp0"

dir /b %SystemRoot%\servicing\Packages\*Hyper-V*.mum >hyper-v.txt

for /f %%i in ('findstr /i . hyper-v.txt 2^>nul') do dism /online /norestart /add-package:"%SystemRoot%\servicing\Packages\%%i"

del hyper-v.txt

Dism /online /enable-feature /featurename:Microsoft-Hyper-V-All /LimitAccess /ALL
```

参考教程:[ <https://www.ithome.com/html/win10/374942.htm>]

### 伪装成专业版绕过安装检测

如图，由于Docker Desktop会在安装的时候检测系统版本，直接安装会显示安装失败。所以需要改下注册表绕过安装检测。



![img](https:////upload-images.jianshu.io/upload_images/1023246-e004df35ece3ca83.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/884)

直接安装会报错

打开注册表，定位到HKEY_LOCAL_MACHINE\software\Microsoft\Windows NT\CurrentVersion，点击current version，在右侧找到EditionId，右键点击EditionId 选择“修改“，在弹出的对话框中将第二项”数值数据“的内容改为Professional，然后点击确定



![img](https:////upload-images.jianshu.io/upload_images/1023246-8c2724b59318851d.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000)

注册表修改

如果嫌改注册表麻烦，可以用管理员权限运行如下cmd命令

```
REG ADD "HKEY_LOCAL_MACHINE\software\Microsoft\Windows NT\CurrentVersion" /v EditionId /T REG_EXPAND_SZ /d Professional /F
```

注意: 1、修改前先备份注册表。2、重启后此项注册表值会自动还原，但不影响docker运行。

### 其他事项

在[官网下载](https://store.docker.com/editions/community/docker-ce-desktop-windows)docker-ce-desktop-windows后直接安装，安装时取消勾选window容器。经过测试，linux容器运行正常，切换到windows容器会检测windows版本而无法启动。不过一般也不会用到windows容器。
 



![img](https:////upload-images.jianshu.io/upload_images/1023246-130dbe1350aa25c9.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/863)

切换windows容器报错



本人安装硬件规格、系统版本与docker版本



![img](https:////upload-images.jianshu.io/upload_images/1023246-04089603f6994d53.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000)

我的运行环境

简单搭了个基于alpine的laravel开发环境，包含php、nginx、mysql、redis、node，经测试运行正常。配合win10的linux子系统开发起来美滋滋。



![img](https:////upload-images.jianshu.io/upload_images/1023246-7eb805ad13721d3a.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000)

实际运行效果

作者：donglc

链接：https://www.jianshu.com/p/1329954aa329/

来源：简书

简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。

