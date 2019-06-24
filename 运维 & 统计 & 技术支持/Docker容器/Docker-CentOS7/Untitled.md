



# Windwos 10 安装Docker

​                                                   2016年12月15日 18:50:27           [lfendo](https://me.csdn.net/u011781521)           阅读数 730                                                                  

​                   

   版权声明：本文为博主原创文章，未经博主允许不得转载。             

​        https://blog.csdn.net/u011781521/article/details/53673737                 

**一、下载安装包**



下载地址



官网地址: <https://github.com/boot2docker/windows-installer/releases>



![img](assets/20161215180458698-1561168765179)



国内地址: <https://get.daocloud.io/toolbox/> 或者这个 <http://get.daocloud.io/>

![img](assets/20161215180407946-1561168765191)

找到你想要的版本进行下载。



![img](assets/20161215180532964-1561168765217)



**二、安装**



下载之后进行安装。



![img](assets/20161215180729092-1561168765331)



点击安装



![img](assets/20161215180818807-1561168765506)



![img](assets/20161215180839703-1561168765580)



安装成功，点击Finsh。



注意: Windows 用户如果没有开启过 Hyper-V，将会提示

![img](assets/20161215181001386-1561168765623)



它会弹出一个这样的界面，点击OK,确认后将会自动安装 Hyper-V 并重启，重启后安装完成。



重新启动之后点击桌面上的docker图标运行软件，运行成功后，在右下角会有个图标，显示Docker正在运行中



![img](assets/20161215183222316-1561168765630)



在Docker 图标右键->Settings->Shared Driver，勾选上你工作区的目录，点击 Apply (这是为了在创建容器的时候挂载目录而设置的，如果你不清楚这代表什么可勾选全部盘符，以免使用中引发困惑)。



![img](assets/20161215183432663-1561168765631)



如果你的电脑设置了密码的话他会要你输入用户名密码



![img](assets/20161215183550009-1561168765771)



应用成功



![img](assets/20161215183709476-1561168765772)



**三、设置加速器**



DockerHub 的镜像在国外，国内访问速度非常慢，在使用前先要配置好加速器：



○ [网易加速器](https://c.163.com/wiki/index.php?title=DockerHub镜像加速) ：http://hub-mirror.c.163.com
○ [中科大加速器](https://lug.ustc.edu.cn/wiki/mirrors/help/docker) ：https://docker.mirrors.ustc.edu.cn
○ 另外还有 [ Daocloud](https://www.daocloud.io/mirror.html#accelerator-doc) 、[Aliyun](https://dev.aliyun.com/search.html) 等多家私有加速器，需要注册后使用



选择好加速器后在 Docker 设置中添加 Registry mirrors 后应用。



![img](assets/20161215184245236-1561168765786)



把它修改成这样



![img](assets/20161215184444755-1561168765794)



**四、验证是否安装成功**



在CMD下输入: docker -v



![img](assets/20161215184801137-1561168765795)



显示的版本是1.12.3。