 		[docker版mysql的使用和配置（1）——docker的基本操作](https://www.cnblogs.com/wangzhao765/p/9533003.html) 	

最近实在是忙成狗，其他的内容等稍微闲一点了一起更新。

这篇主要是讲docker版的mysql的使用和配置信息。因为实习公司需要搞一个docker做测试环境用，还需要包括基本的依赖。最重要的是，因为这个docker是作为jenkins的slave使用的，所以有可能不能在启动的时候加参数。这就导致我得把docker版的mysql的整个使用和配置详细过一遍，看看是否有代替启动参数的设置方法。

文中涉及到的mysql的基本信息见以下链接：

https://hub.docker.com/r/mysql/mysql-server/

https://github.com/mysql/mysql-docker

文章省略了docker的安装。到官网安一下就好了。

那么林可死大特。

 

\1. 常规的基本docker版mysql的使用（docker的基本操作）：

下载（community版）：

```
docker pull mysql/mysql-server:tag
```

这里的tag是指mysql的版本号，比如5.5～5.7，8.0，latest。

```
docker images
```

这个是用来查看当前的镜像的清单。

```
docker run --name=mysql1 -d mysql/mysql-server:tag
```

接下来就是运行镜像，一个最基本的运行语句大概长这个样子。

--name制定了运行该镜像的容器（container）的名称。如果不声明的话会随机生成一个。

接着是镜像的名字。

-d是指明镜像的运行是扔在后台的。

```
docker ps
```

可以用这个语句来查看当前的image的运行情况。

整个流程简单来说，就是：pull镜像，run镜像，ps看看镜像是不是起来了。

```
docker logs mysql1
```

logs加上容器的名称，可以看这个容器在运行镜像时产生的日志信息。如果ps看到了镜像没有正常运行，就可以logs检查一下。

```
docker exec -it mysql1 mysql -uroot -p
```

exec就是在镜像中执行后面的操作，比如上面就是执行了mysql -uroot -p。

```
docker exec -it mysql1 bash 
```

为了方便在镜像中执行bash命令，也可以执行这样的命令来开一个bash。

```
docker stop mysql1
```

这样可以停止container的运行。想删除container首先要停止container。想删除image首先要删除container。总之是一环扣一环的。

\2. docker版mysql和普通版mysql的区别

Docker images for MySQL are optimized for code size, which means they  only include crucial components that are expected to be relevant for  the majority of users who run MySQL instances in Docker containers.

按官网的说法，docker版的mysql只包含了必要的部分。清单如下：

[![复制代码](assets/copycode.gif)](javascript:void(0);)

```
/usr/bin/my_print_defaults

/usr/bin/mysql

/usr/bin/mysql_config

/usr/bin/mysql_install_db

/usr/bin/mysql_tzinfo_to_sql

/usr/bin/mysql_upgrade

/usr/bin/mysqladmin

/usr/bin/mysqlcheck

/usr/bin/mysqldump

/usr/bin/mysqlpump

/usr/sbin/mysqld
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

\3. docker版mysql的使用

前面提到了查看log的方法。docker版mysql的默认配置是：第一次的root密码是随机生成的。所以想用root账户登陆，首先要查看随机生成的密码。

```
shell> docker logs mysql1 2>&1 | grep GENERATED
GENERATED ROOT PASSWORD: Axegh3kAJyDLaRuBemecis&EShOs
```

然后就可以-uroot -p登录了。

登录之后的第一件事，就是改root密码。

```
mysql> ALTER USER 'root'@'localhost' IDENTIFIED BY 'password';
```

其他的就跟本地运行mysql差不多了。

注意的一点是，我们刚刚run docker的命令中，并没有把docker中的端口和本机的端口进行映射。所以虽然mysql虽然启动了，但是不能通过3306或者其他端口进行访问。

想跟在本机一样的话，就要在启动docker容器的时候对docker和本机的端口进行映射。

```
docker run -p ip:hostPort:containerPort
```

 





标签: [docker](https://www.cnblogs.com/wangzhao765/tag/docker/), [MySQL](https://www.cnblogs.com/wangzhao765/tag/MySQL/)