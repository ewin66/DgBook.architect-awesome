2018-01-16 08:50:16
中国科技大学的镜像加速器：中科大的加速器不用注册，直接使用地址 https://docker.mirrors.ustc.edu.cn/ 配置加速器即可。进一步的信息可以访问：http://mirrors.ustc.edu.cn/help/dockerhub.html?highlight=docker

阿里云加速器：注册阿里云开发账户(免费的)后，访问这个链接就可以看到加速器地址： https://cr.console.aliyun.com/#/accelerator
--------------------- 
作者：EgretLu 
来源：CSDN 
原文：https://blog.csdn.net/weixin_43466473/article/details/86155079 
版权声明：本文为博主原创文章，转载请附上博文链接！

---



# 国内的[Docker Hub镜像服务](https://blog.csdn.net/liubenq/article/details/78449126)

​                                                   2016年08月22日 20:06:36           [crabdave](https://me.csdn.net/crabdave)           阅读数 68                   

​                   

**国内的Docker Hub镜像服务**

 

DaoCloud在国内提供了首个Docker Hub镜像服务，而且免费。

vi /etc/sysconfig/docker

OPTIONS="--registry-mirror=http://aad0405c.m.daocloud.io"

 

修改后最终是：

OPTIONS='--selinux-enabled --log-driver=journald --graph=/data/docker/images --registry-mirror=http://aad0405c.m.daocloud.io'

 

重启docker  

 

 

阿里云内部网络里也有自己的镜像

https://********.mirror.aliyuncs.com\



----









# docker hub切换国内镜像

​                                                   2017年11月05日 14:04:04           [一些和风旭日的日子](https://me.csdn.net/liubenq)           阅读数 12440                                                                  





官方docker hub访问非常的慢，安装之后最好先切换国内镜像：

执行：

curl -sSL https://get.daocloud.io/daotools/set_mirror.sh | sh -s http://ef017c13.m.daocloud.io


然后再重启：

systemctl restart docker
--------------------- 
作者：一些和风旭日的日子 
来源：CSDN 
原文：https://blog.csdn.net/liubenq/article/details/78449126 
版权声明：本文为博主原创文章，转载请附上博文链接！



