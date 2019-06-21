# 在Windows上体验Docker（四）运行mysql镜像

​                                                   2018年08月01日 14:29:53           [MaxWoods](https://me.csdn.net/MaxWoods)           阅读数 987                                                                  

​                   

​                  版权声明：本文为博主原创文章，未经博主允许不得转载。         

​            https://blog.csdn.net/MaxWoods/article/details/81329953                

在windows命令行中输入以下命令，启动mysql容器：

```bash
docker run -d --name mysql -e MYSQL_ROOT_HOST=% -e MYSQL_ROOT_PASSWORD=root -e MYSQL_USER=mysql -e MYSQL_PASSWORD=mysql -p 3306:3306 -v /d/docker/mysql:/var/lib/mysql mysql
```

参数“-v /d/docker/mysql:/var/lib/mysql”表示将windows目录d:\docker\mysql映射到/var/lib/mysql，这里存放了数据库文件。

> 参数“-e MYSQL_ROOT_PASSWORD" 指定了root用户的密码
>
> 参数“-e MYSQL_USER=mysql"  指定了用户名
>
> 参数“-e MYSQL_PASSWORD=mysql“ 指定了用户密码
>

 

输入下面命令进入容器命令行交互:

```
$ docker exec -it mysql /bin/bash
root@c50445d8481d:/#
```

java连接时提示示com.mysql.jdbc.exceptions.jdbc4.MySQLNonTransientConnectionException:  Public Key Retrieval is not allowed,最简单的解决方法是在连接后面添加  allowPublicKeyRetrieval=true

连接数据库报错plugin caching_sha2_password could not be loaded，执行下面操作：

```sql
ALTER USER 'root'@'localhost' IDENTIFIED BY 'password' PASSWORD EXPIRE NEVER; 

ALTER USER 'root'@'localhost' IDENTIFIED WITH mysql_native_password BY 'password'; 

FLUSH PRIVILEGES; 

ALTER USER 'root'@'localhost' IDENTIFIED BY 'password';
```

指定mysql 8版本的容器创建例子：

```
docker run --name mysql -e MYSQL_ROOT_PASSWORD=woods -e MYSQL_USER=test -e MYSQL_PASSWO
```