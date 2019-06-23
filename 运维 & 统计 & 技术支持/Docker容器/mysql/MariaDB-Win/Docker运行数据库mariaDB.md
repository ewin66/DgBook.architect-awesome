# Docker运行数据库mariaDB

作者：灼灼2015

链接：https://www.jianshu.com/p/d9ddc63de30c



需求：docker容器运行mariaDB数据库

1. 进入容器

```
[root@wxtest1607 ~]# docker ps -a
CONTAINER ID        IMAGE               COMMAND                CREATED             STATUS                     PORTS                     NAMES
8122ab04139a        aa79                "/root/tomcatrun.sh"   4 minutes ago          Exited (0) 8 minutes ago                             goofy_bartik
[root@wxtest1607 ~]# docker exec -ti 8122  /bin/bash
[root@8122ab04139a /]# 
```

1. 安装mariaDB

```
#yum -y install mariadb mariadb-server
# mysql_install_db --user=mysql
# mysqld_safe   
#/usr/bin/mysqladmin -u root password '123456'
#mysql -uroot -p123456
Welcome to the MariaDB monitor.  Commands end with ; or \g.
Your MariaDB connection id is 4
Server version: 5.5.50-MariaDB MariaDB Server
Copyright (c) 2000, 2016, Oracle, MariaDB Corporation Ab and others.
Type 'help;' or '\h' for help. Type '\c' to clear the current input statement.
MariaDB [(none)]> use mysql;
MariaDB [mysql]> grant all PRIVILEGES on *.* to admin@'%'  identified by '123456';
Query OK, 0 rows affected (0.00 sec)
MariaDB [mysql]> flush PRIVILEGES;
Query OK, 0 rows affected (0.00 sec)
```

1. 提交mariadb镜像

```
[root@wxtest1607 mnt]# docker ps -a
CONTAINER ID        IMAGE               COMMAND                CREATED             STATUS                      PORTS                     NAMES
8122ab04139a        aa79                "/root/tomcatrun.sh"   32 minutes ago      Up 32 minutes               0.0.0.0:58080->8080/tcp   admiring_allen
[root@wxtest1607 mnt]# docker commit 8122 mariadb:1.0
sha256:8c212b8a0f7d93f895599ec5c36370617b6806e610627efd9103be6e9f49ed4d
[root@wxtest1607 mnt]# docker images
REPOSITORY                TAG                 IMAGE ID            CREATED             SIZE
mariadb                   1.0                 8c212b8a0f7d        7 seconds ago       912.4 MB
```

1. 启动mariadb

```
docker run -d -p 53306:3306 8c21 mysqld_safe
```

1. 登录容器mariadb

```
 mysql -uadmin -p123456 --port=53306 -h192.168.220.123
Welcome to the MariaDB monitor.  Commands end with ; or \g.
Your MariaDB connection id is 6
Server version: 5.5.50-MariaDB MariaDB Server
Copyright (c) 2000, 2016, Oracle, MariaDB Corporation Ab and others.
Type 'help;' or '\h' for help. Type '\c' to clear the current input statement.
```

放慢脚步，从心出发。

作者：灼灼2015

链接：https://www.jianshu.com/p/d9ddc63de30c

来源：简书

简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。