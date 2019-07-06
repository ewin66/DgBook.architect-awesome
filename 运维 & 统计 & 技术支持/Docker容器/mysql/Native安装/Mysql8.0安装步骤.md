# Mysql8.0[安装步骤](https://blog.csdn.net/qq_33144861/article/details/80267462)





第一步：下载安装包
MYSQL官方下载地址：官方下载
地址：https://dev.mysql.com/downloads/windows/installer/

这里写图片描述
这里第一项是在线安装，第二项是离线包安装，我选择的是第二项(不用管你电脑是多少位的操作系统)，因为：
Note: MySQL Installer is 32 bit, but will install both 32 bit and 64 bit binaries.
这里写图片描述
不用注册、登录，直接选择左下按钮下载：No thanks,just start my download

第二步，安装
我这里选择的是Custom自定义安装，也可以选择Default默认
这里写图片描述
因为我是自定义安装，所以我只选择了下面这2个安装内容
这里写图片描述
这时如果出现下面的提示，代表机器没有VC的环境，使安装无法进行下去，这时需要先下载好VC环境
这里写图片描述
下载VC环境：官方地址
如果地址失效，也可以百度：microsoft visual c++ 2015 进行下载

环境下载安装完后，我们再回来重新安装，发现刚才此步会变成
这里写图片描述
接下去一路安装下去
这里写图片描述
这里写图片描述
这里写图片描述
这里写图片描述
上面这步是创建一个用户，然后接着往下走
这里写图片描述
这里写图片描述
这里写图片描述

安装完成后，可以再服务里，看到MYSQL服务已经启动，bin目录(默认为：C:\Program Files\MySQL\MySQL Server 8.0\bin)

但此时如果我们用Navicat等第三方应用登录，会提示：
Authentication plugin 'caching_sha2_password' cannot be loaded

经查看发现，8.0改变了 身份验证插件 ，
解决方式

ALTER USER stocker@localhost IDENTIFIED WITH mysql_native_password BY 'stocker';

    1

如果是最新版本的第三方软件，可能已经自动改变了身份验证，无需手动修改
--------------------- 
作者：93年的香槟 
来源：CSDN 
原文：https://blog.csdn.net/qq_33144861/article/details/80267462 
版权声明：本文为博主原创文章，转载请附上博文链接！