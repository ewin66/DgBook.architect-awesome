# Docker安装MariaDB和Mysql





https://www.jianshu.com/p/7028681d7f0f



![img](https:////upload-images.jianshu.io/upload_images/12128565-688477ecbe25c4a4.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/740)

image.png

MariaDB是MySQL源代码的一个分支，随着Oracle买下Sun，MySQL也落入了关系型数据库王者之手。在意识到Oracle会对MySQL许可做什么后便分离了出来（MySQL先后被Sun、Oracle收购），MySQL之父的Michael以他女儿Maria的名字开始了MySQL的另外一个衍生版本：MariaDB。这两个数据库究竟有什么本质的区别没有？(我感觉区别不大)

区别一：
 MariaDB不仅仅是Mysql的一个替代品，MariaDB包括的一些新特性使它优于MySQL。
 区别二：
 MariaDB跟MySQL在绝大多数方面是兼容的，对于开发者来说，几乎感觉不到任何不同。目前MariaDB是发展最快的MySQL分支版本，新版本发布速度已经超过了Oracle官方的MySQL版本。
 MariaDB 是一个采用Aria存储引擎的MySQL分支版本， 这个项目的更多的代码都改编于 MySQL 6.0
 区别三：
 通过全面测试发现，MariaDB的查询效率提升了3%-15%，平均提升了8%，而且没有任何异常发生；以qp为单位，吞吐量提升了2%-10%。由于缺少数据支持，现在还不能得出任何结论，但单从测试结果看来还是非常积极的。join中索引的使用以及查询优化，特别是子查询方面，MariaDB都有不少提升。此外，MariaDB对MySQL导入导出有良好支持。

其实在真正的开发过程中你会发现MariaDB和Mysql区别并不大。下面我们就介绍一下docker安装的步骤：
 (1) MariaDB的安装
 1: docker search mariadb



![img](https:////upload-images.jianshu.io/upload_images/12128565-1d4278e6b6a86e7c.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/894)

image.png

拉取 mariadb 镜像：
 2:docker pull mariadb



![img](https:////upload-images.jianshu.io/upload_images/12128565-1a72c2ae3b80494a.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/877)

image.png

1. 配置数据库文件路径

# 创建本地数据库文件路径

[root@localhost~]mkdir -p /data/mysql

# 修改对象（文件）的安全上下文。比如：用户：角色：类型：安全级别(此处会有人采坑)

Docker 挂载权限 chcon: can't apply partial context to unlabeled file
 参考链接 :  <http://www.linuxidc.com/Linux/2012-09/70199.htm>

[root@localhost~] chcon -Rt svirt_sandbox_file_t /data/mysql

1. 启动镜像（设置为自启动）具体命令自己可自行百度(莫拿来用原则)
    docker run -v /data/mysql/:/var/lib/mysql -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root123  --privileged=true --restart unless-stopped   --name mariadbs -d mariadb:latest
    docker ps -a 查看运行中的镜像

   

   ![img](https:////upload-images.jianshu.io/upload_images/12128565-19e03dafc768d42a.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1000)

   image.png

   docker logs mariadbs 查看mariadbs的日志

   

   ![img](https:////upload-images.jianshu.io/upload_images/12128565-69747ede873b542d.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/971)

   image.png



![img](https:////upload-images.jianshu.io/upload_images/12128565-373a7b7275d8ae2d.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/783)

image.png



![img](https:////upload-images.jianshu.io/upload_images/12128565-23abc02d35a6c5b7.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/775)

image.png

默认使用3306端口 修改端口需要更改mariadb 的配置文件,和mysql略微不一致,建议采坑的同学使用3306.
 可参考文档链接:
 <http://www.ptbird.cn/docker-mariadb.html>

小礼物走一走，来简书关注我

作者：程序员豪哥

链接：https://www.jianshu.com/p/7028681d7f0f

来源：简书

简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。