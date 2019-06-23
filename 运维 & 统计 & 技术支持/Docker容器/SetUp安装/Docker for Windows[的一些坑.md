https://cr.console.aliyun.com/cn-hangzhou/instances/mirrors?accounttraceid=8b127bf4-7a90-480b-9d94-f9d3751b309d









----





# Docker for Windows[的一些坑（不断更新，亲测有效、）](https://www.jianshu.com/p/a0a6decf1f5a)







Windows版本：Windows 10专业版
 重要提示：如果安装docker toolbox ，直接去这里：[http://mirrors.aliyun.com/docker-toolbox/windows/docker-toolbox/](https://links.jianshu.com/go?to=http%3A%2F%2Fmirrors.aliyun.com%2Fdocker-toolbox%2Fwindows%2Fdocker-toolbox%2F)，别继续看下去了，下面的坑很多哦

第一步：开启Hyper-V
 在“启用或关闭Windows功能”里，钩选Hyper-V,然后确定。




![img](https:////upload-images.jianshu.io/upload_images/1923942-a95dffc43de53c12.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/460)

开启Hyper-V

 如果找不到hyper-v（企业版）

https://www.microsoft.com/zh-cn/download/confirmation.aspx?id=45520



第二步：安装Docker for Windows
 下载链接：[https://download.docker.com/win/stable/Docker%20for%20Windows%20Installer.exe](https://links.jianshu.com/go?to=https%3A%2F%2Fdownload.docker.com%2Fwin%2Fstable%2FDocker%20for%20Windows%20Installer.exe)



![img](https:////upload-images.jianshu.io/upload_images/1923942-6b25e8b27ae092f0.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/594)

软件版本信息

## 其中Kitematic的下载地址：

[https://github.com/docker/kitematic/releases/](https://links.jianshu.com/go?to=https%3A%2F%2Fgithub.com%2Fdocker%2Fkitematic%2Freleases%2F)

将下载下来的文件夹解压缩到相应的目录。
 （此处右键Docker desktop的图标，点击Kitematic会有相关提示）

> C:\Program Files\Docker\Kitematic

## Kitematic的一些坑

> (HTTP code 500) server error - Get [https://docker.mirrors.ustc.edu.cn/v2/](https://links.jianshu.com/go?to=https%3A%2F%2Fdocker.mirrors.ustc.edu.cn%2Fv2%2F): net/http: TLS handshake timeout

**!这个问题不是每次出现，一些参考认为多试几次就好**

> context canceled

**!一些解释认为这是因为Kitematic的版本问题，删掉刚刚下载的Kitematic，去GitHub Kitematic 项目的Release下，下载最新发布的版本**

> VirtualBox is not installed. Please install it via the Docker Toolbox.
>
> https://docker.mirrors.ustc.edu.cn/
>
> https://anuzyij8.mirror.aliyuncs.com/

*！下载并安装https://download.docker.com/win/stable/DockerToolbox.exe*

## 关于docker镜像国内地址：

此处右键Docker desktop的图标，选择Settings-Daemon，在Registry mirrors:里添加[https://docker.mirrors.ustc.edu.cn/](https://links.jianshu.com/go?to=https%3A%2F%2Fdocker.mirrors.ustc.edu.cn%2F)然后点击确定，服务会重启。**这个更改会同时改变Kitematic的镜像源**

## 一些常用的命令：

```
docker version
#显示docker版本
docker pull mysql
#下载名为mysql的镜像
```

作者：Magna

链接：https://www.jianshu.com/p/a0a6decf1f5a

来源：简书

简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。