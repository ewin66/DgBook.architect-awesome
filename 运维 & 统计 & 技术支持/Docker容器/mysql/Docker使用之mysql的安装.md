# docker安装[mysql遇到的问题](https://blog.csdn.net/zhaokejin521/article/details/80468908)

mysql                             MySQL is a widely used, open-source relation…   8289                [OK] mariadb                           MariaDB is a community-developed fork of MyS…   2834                [OK] mysql/mysql-server                Optimized MySQL Server Docker images. Create…   619                                     [OK] centurylink/mysql

### 拉下镜像

​	docker pull mysql

docker run -d -P mysql :8.0.11

docker run -d -P mariadb

```
docker ps -a
CONTAINER ID        IMAGE                 COMMAND                  CREATED             STATUS                      PORTS               NAMES
f6aa9080e011        mysql                 "docker-entrypoint.s…"   11 seconds ago      Exited (1) 8 seconds ago                        elated_heisenberg
fe19135fd984        mariadb:10.2.16       "docker-entrypoint.s…"   24 hours ago        Exited (1) 23 hours ago                         mariadb
3dd3f2636fae        jenkins:2.60.3        "/bin/tini -- /usr/l…"   5 days ago          Exited (143) 5 days ago                         jenkins
03f00b234f26        elasticsearch:7.1.1   "/usr/local/bin/dock…"   5 days ago          Exited (78) 5 days ago                          elasticsearch
076d21ab539a        redis:latest          "docker-entrypoint.s…"   5 days ago          Exited (0) 29 seconds ago                       redis
docker ps -a
```

###	 查看

docker ps -a

CONTAINER ID        IMAGE                 COMMAND                   CREATED             STATUS                      PORTS                NAMES f6aa9080e011        mysql                 "docker-entrypoint.s…"   11  seconds ago      Exited (1) 8 seconds ago                         elated_heisenberg fe19135fd984        mariadb:10.2.16       "docker-entrypoint.s…"   24  hours ago        Exited (1) 23 hours ago                         mariadb 3dd3f2636fae        jenkins:2.60.3        "/bin/tini -- /usr/l…"   5  days ago          Exited (143) 5 days ago                          jenkins 03f00b234f26        elasticsearch:7.1.1   "/usr/local/bin/dock…"   5  days ago          Exited (78) 5 days ago                           elasticsearch 076d21ab539a        redis:latest          "docker-entrypoint.s…"   5  days ago          Exited (0) 29 seconds ago                       redis

```
 docker run --name mysql  -d -p 3309:3309 -e MYSQL_ROOT_PASSWORD=root mysql 
  docker run --name mariadb  -d -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root mariadb
```

docker run --name mysql  -d -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root mysql    	暂时pwd为root

1689416065b294a25a5b6562bc807c6ce11547a7ffac3c49eea82bb47381ddea

```
 docker exec -it mysql01 mysql -uroot -p123456
-- 下方为真的
 docker exec -it mysql mysql -uroot -proot
 ----
//  docker exec -it mysql mysql -uroot -pwsx1001
docker exec -it mysql mysql -uroot -proot
docker exec -it mariadb mariadb -uroot -proot
```

### 启动进入

docker exec -it mysql mysql -uroot -proot

mysql: [Warning] Using a password on the command line interface can be insecure. Welcome to the MySQL monitor.  Commands end with ; or \g. Your MySQL connection id is 11 Server version: 8.0.16 MySQL Community Server - GPL

Copyright (c) 2000, 2019, Oracle and/or its affiliates. All rights reserved.

Oracle is a registered trademark of Oracle Corporation and/or its affiliates. Other names may be trademarks of their respective owners.

Type 'help;' or '\h' for help. Type '\c' to clear the current input statement.

###	 再执行连接

mysql> mysql -u root -p

```
select host,user,plugin,authentication_string from mysql.user;
```

mysql>  select host,user,plugin,authentication_string from  mysql.user +-----------+------------------+-----------------------+------------------------------------------------------------------------+ | host      | user             | plugin                |  authentication_string                                                  | +-----------+------------------+-----------------------+------------------------------------------------------------------------+ | %         | root             | caching_sha2_password |  $A$005$�z�\V0jM�0GxOB/^eNNgkRdCBMnmrybdXeH02Vfq9Rw..hR27FLrJdatP0KW7 | | localhost | mysql.infoschema | caching_sha2_password |  $A$005$THISISACOMBINATIONOFINVALIDSALTANDPASSWORDTHATMUSTNEVERBRBEUSED | | localhost | mysql.session    | caching_sha2_password |  $A$005$THISISACOMBINATIONOFINVALIDSALTANDPASSWORDTHATMUSTNEVERBRBEUSED | | localhost | mysql.sys        | caching_sha2_password |  $A$005$THISISACOMBINATIONOFINVALIDSALTANDPASSWORDTHATMUSTNEVERBRBEUSED | | localhost | root             | caching_sha2_password |  $A$005$dT0|_h?U�XK0exg�" x�QwLSxPzKALDAGMVR9qOflTLYV3U1YQ4cvOwtpgt7Mn5 | +-----------+------------------+-----------------------+------------------------------------------------------------------------+ 5 rows in set (0.00 sec)

```
ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY 'wsx1001';
```

mysql> ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY 'wsx1001'; Query OK, 0 rows affected (0.02 sec)

ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY 'newpassword'; #更新一下用户的密码 root用户密码为newpassword

```
ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY 'wsx1001';

ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY 'wsx1001';

【Access denied for user 'root'@'172.17.0.1' (using password】【俊哥V】
解决办法：将验证方式修改为“mysql_native_password”

    USE mysql; 
     
    ALTER USER 'root'@'%' IDENTIFIED WITH mysql_native_password BY '<password>';
     
    FLUSH PRIVILEGES;
--------------------- 
作者：俊哥V 
来源：CSDN 
原文：https://blog.csdn.net/debug_fan/article/details/85247851 
版权声明：本文为博主原创文章，转载请附上博文链接！
【error1045】https://www.cnblogs.com/SZxiaochun/p/6962910.html
【Access denied for user 'root'】	https://blog.csdn.net/van_brilliant/article/details/82114532

```

##  -- 【[sky灬鹏少](https://blog.csdn.net/u010789532)】使用Docker[安装Mysql](https://blog.csdn.net/u010789532/article/details/80545228)

> 再执行mysql时，发现还是报错，查询原因是启动项不在/usr/bin下面。
>
> ```
> ln -s /usr/local/mysql/bin/mysql /usr/bin　做个链接即可
> ```
>
>  后来发现环境变量修改了，但是好像没有保存好，mysqladmin 命令还是不能用，所以又重新保存环境变量
>
> ![复制代码](assets/copycode-1561130109161.gif)
>
> ```
> [centos@liujun ~]$ vim ~/.bash_profile
> 
> #PATH=$PATH:$HOME/bin:/usr/local/apache/bin
> #添加以下列
> PATH=$PATH:$HOME/bin:/usr/local/mysql/bin:/usr/local/mysql/lib
> #:wq 保存退出
> [centos@liujun ~]$ source ~/.bash_profile
> ```
>
> 参考：[
> ](http://www.cnblogs.com/aomi/p/7590887.html)
>
> <http://blog.sina.com.cn/s/blog_7f2ac7b70102vpyl.html>
>
> <http://www.cnblogs.com/xiohao/p/5377609.html>
>
> <https://help.aliyun.com/document_detail/50774.html>
>
> 关于IP的设置
>
> <https://severalnines.com/blog/mysql-docker-containers-understanding-basics>



------

------

#

# Docker安装[MariaDB](https://www.jianshu.com/p/7028681d7f0f)和Mysql

docker run -v /data/mysql/:/var/lib/mysql -p 3306:3306 -e  MYSQL_ROOT_PASSWORD=root123  --privileged=true --restart unless-stopped    --name mariadbs -d mariadb:latest docker ps -a 查看运行中的镜像

作者：程序员豪哥

链接：<https://www.jianshu.com/p/7028681d7f0f>

来源：简书

简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。

# 

# Docker使用[MariaDB](https://blog.csdn.net/weixin_37272286/article/details/78016176)

# 

# Docker[使用MariaDB](https://www.jianshu.com/p/32542630c2bd)

------

------

\--

# 

# -d mysql：需要启动的容器的名称

docker pull docker.io/mysql:8.0.15

2.5：使用 docker run 启动镜像，以Mysql为例，出现下方字符串，启动成功

```
#  docker run --name mysql -p 3306:3306 -v /mysql/datadir:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=123456  -d mysql
787b06122b723e547b1216d499b69d99725a703217cd09f49e9ce548f9ba27dc
```

--name: 以什么名字启动容器

-p 3306:3306 :将容器端口映射到服务器端口

-v /mysql/datadir:/var/lib/mysql :将mysql的配置路径映射到本地datadir上

-e MYSQL_ROOT_PASSWORD=123456 :设置服务器密码为123456

docker run --name mysql -p 3306:3306   -e MYSQL_ROOT_PASSWORD=wsx1001  -d mysql

787b06122b723e547b1216d499b69d99725a703217cd09f49e9ce548f9ba27dc

## 

# -d mysql：需要启动的容器的名称

作者：mdw5521 来源：CSDN 原文：<https://blog.csdn.net/mdw5521/article/details/79174094> 版权声明：本文为博主原创文章，转载请附上博文链接！

------

------

------

[docker@xcbeyond mysql]$

docker run -p 3306:3306 --name mysql -v $PWD/conf:/etc/mysql/conf.d  -v $PWD/logs:/logs -v $PWD/data:/var/lib/mysql -e  MYSQL_ROOT_PASSWORD=wsx1001  -d mysql:8.0.11

docker run -p 3306:3306 --name mysql  -e MYSQL_ROOT_PASSWORD=wsx1001  -d mysql:8.0.11

306803948d307424b509abde434a9972b239a02aa0a8ba0945b05b0052613372 [docker@xcbeyond mysql]$ ll total 12 drwxr-xr-x 2 root root 4096 Aug 31 23:39 conf drwxr-xr-x 2 polkitd input 4096 Aug 31 23:39 data drwxr-xr-x 2 root root 4096 Aug 31 23:39 logs









---

---

# Docker使用之mysql的安装

Docker，官方解释的很高大上，一般初次接触看不怎么明白，下面我用方言介绍下，docker就像一个大仓库，仓库里有许许多多的配置好的工具镜像，比如mysql、activemq、zookeeper等等，你可以去拉取你需要的工具，然后运行他们，就OK了。就这么简单，比如大家都都使用过的Mysql，如果需要安装的话，首先我们要下载安装包，然后一步一步下一步的去安装，安装完可能还要配置些什么，万一在安装过程中出点什么问题，弄了半天安装失败，是不是很悲催，使用Docker ，第一步拉取mysql，第二步开启mysql,就这样完了，简单吧。下面我们就来说下Docker的安装与使用吧。

一：Docker的安装，这里给出了官方的安装方式，就不在赘述。

https://docs.docker.com/engine/installation/linux/docker-ce/ubuntu/#install-docker-ce-1

二：docker的官方仓库：https://hub.docker.com/explore/


2.1：官方仓库有很多各种各样的工具，下图中红色圈住的就是获取Mysql的命令，我们在Linux输入这条命令就可以下载到Mysql的镜像。



2.2：第二种获取镜像方式，使用 docker search 命令可以直接搜索你想要的镜像。


2.3：使用 docker pull 命令下载镜像（由于已经安装mysql，就以redis为例）




2.4：使用 docker images 查看下载镜像列表


REPOSITORY:镜像名称 TAG:镜像标签(最新) IMAGE ID:镜像ID，唯一标识 CREATED:创建时间 SIZE:镜像大小


2.5：使用 docker run 启动镜像，以Mysql为例，出现下方字符串，启动成功

    #  docker run --name mysql -p 3306:3306 -v /mysql/datadir:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=123456  -d mysql
    787b06122b723e547b1216d499b69d99725a703217cd09f49e9ce548f9ba27dc

--name: 以什么名字启动容器

-p 3306:3306 :将容器端口映射到服务器端口

-v /mysql/datadir:/var/lib/mysql :将mysql的配置路径映射到本地datadir上

-e MYSQL_ROOT_PASSWORD=123456 :设置服务器密码为123456

-d mysql：需要启动的容器的名称

2.5：使用 docker ps 查看镜像启动情况如下图可以看到mysql已经起来了。


2.6：使用 docker stop 关闭镜像，返回镜像名就是完成了。stop后既可以跟镜像名称也可以跟镜像的ID.

    # docker stop  mysql
    mysql

2.7：使用 docker ps -a 查看容器状态，如图mysql已经退出



---------------------
作者：mdw5521 
来源：CSDN 
原文：https://blog.csdn.net/mdw5521/article/details/79174094 
版权声明：本文为博主原创文章，转载请附上博文链接！

